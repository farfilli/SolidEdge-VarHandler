Imports System.ComponentModel
Imports System.IO

Public Class Form_WorkFlow

    Public Variables As List(Of Object)
    Public UpdateDoc As Boolean = False
    Public SaveImages As Boolean = False
    Public CheckInterference As Boolean = False
    Public LengthUnits As SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants

    Private Sub Add_Event_Click(sender As Object, e As EventArgs) Handles Add_Event.Click

        Dim tmpStep = NewEvent()

        FLP_Events.Controls.Add(tmpStep)

        tmpStep.DG_Variables.ClearSelection()

        SetupAnchors()

        SetPrevious(tmpStep)

    End Sub

    Private Function NewEvent() As UC_WorkFlowEvent

        Dim list = New List(Of EventVariable)
        For Each tmpVar In Variables

            If Not (tmpVar.IsReadOnly Or tmpVar.Formula <> "") Then
                Dim tmpVariable As New EventVariable
                tmpVariable.Check = True
                tmpVariable.Name = tmpVar.DisplayName
                tmpVariable.Value = Math.Round(UC_Slider.CadToValue(tmpVar.value, tmpVar.UnitsType, LengthUnits), 2)
                tmpVariable.objVar = tmpVar

                list.Add(tmpVariable)
            End If

        Next

        Dim bindingList = New BindingList(Of EventVariable)(list)
        Dim source = New BindingSource(bindingList, Nothing)

        Dim tmpStep As New UC_WorkFlowEvent

        tmpStep.DG_Variables.Columns.Clear()
        tmpStep.DG_Variables.AutoGenerateColumns = True
        tmpStep.DG_Variables.DataSource = source
        tmpStep.LB_SEQ.Text = FLP_Events.Controls.Count + 1

        tmpStep.Height = 22 + tmpStep.DG_Variables.RowTemplate.Height * tmpStep.DG_Variables.Rows.Count

        tmpStep.DG_Variables.Columns.Item("check").Visible = False
        tmpStep.DG_Variables.Columns.Item("objVar").Visible = False
        tmpStep.DG_Variables.Columns.Item("Value").Width = 80
        tmpStep.DG_Variables.Columns.Item("Value").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        tmpStep.DG_Variables.Columns.Item("Value").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        tmpStep.DG_Variables.Columns.Item("Name").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        tmpStep.DG_Variables.Columns.Item("Name").ReadOnly = True

        Return tmpStep

    End Function

    Private Sub SetPrevious(StepEvent As UC_WorkFlowEvent)

        If FLP_Events.Controls.Count > 1 Then

            Dim PreviousStep As UC_WorkFlowEvent = FLP_Events.Controls.Item(FLP_Events.Controls.Count - 2)
            For Each tmpRow As DataGridViewRow In PreviousStep.DG_Variables.Rows
                StepEvent.DG_Variables.Rows.Item(tmpRow.Index).Cells("Value").Value = tmpRow.Cells("Value").Value
            Next

        End If

    End Sub

    Private Sub SetupAnchors()

        FLP_Events.SuspendLayout()

        If FLP_Events.Controls.Count > 0 Then
            For i = 0 To FLP_Events.Controls.Count - 1
                Dim c As Control = FLP_Events.Controls(i)
                If i = 0 Then
                    ' Its the first control, all subsequent controls follow
                    ' the anchor behavior of this control.
                    c.Width = FLP_Events.Width - 6
                    c.Anchor = AnchorStyles.Left + AnchorStyles.Top

                    If FLP_Events.VerticalScroll.Visible Then c.Width += -SystemInformation.VerticalScrollBarWidth + 0

                Else

                    ' It is not the first control. Set its anchor to
                    ' copy the width of the first control in the list.
                    c.Anchor = AnchorStyles.Left + AnchorStyles.Right

                End If
            Next
        End If

        FLP_Events.ResumeLayout()

    End Sub

    Private Sub Form_WorkFlow_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        SetupAnchors()
    End Sub

    Private Sub BT_Play_Click(sender As Object, e As EventArgs) Handles BT_Play.Click

        If FLP_Events.Controls.Count > 0 Then

            For Each StepEvent As UC_WorkFlowEvent In FLP_Events.Controls

                StepEvent.LB_SEQ.ForeColor = Color.DarkGreen

                SetSteps(StepEvent)

                'For i = 1 To 20
                For i = 1 To StepEvent.steps

                    Form_VarHandler.objDoc.Parent.DelayCompute = True

                    For Each tmpRow As DataGridViewRow In StepEvent.DG_Variables.Rows

                        Dim tmpVariable As Object = tmpRow.Cells("objVar").Value

                        tmpVariable.Value += UC_Slider.ValueToCad(tmpRow.Tag, tmpVariable.UnitsType, LengthUnits)

                    Next

                    Form_VarHandler.objDoc.Parent.DelayCompute = False

                    'If Form_VarHandler.objDoc.Type = SolidEdgeConstants.DocumentTypeConstants.igAssemblyDocument And UpdateDoc Then Form_VarHandler.objDoc.UpdateDocument

                    If UpdateDoc Then UC_Slider.DoUpdateDoc(Form_VarHandler.objDoc)

                    Form_VarHandler.objDoc.Parent.DoIdle()

                    If SaveImages Then UC_Slider.DoSaveImage(Form_VarHandler.objDoc)

                    If CheckInterference Then If Not UC_Slider.DoCheckInterference(Form_VarHandler.objDoc) Then Exit For

                Next

                StepEvent.LB_SEQ.ForeColor = Color.DarkGray

            Next

        End If

    End Sub

    Private Sub SetSteps(stepEvent As UC_WorkFlowEvent)

        Dim tmpSteps = stepEvent.steps

        For Each tmpRow As DataGridViewRow In stepEvent.DG_Variables.Rows

            Dim tmpVariable As Object = tmpRow.Cells("objVar").Value
            'Dim stepValue As Double = (CDbl(tmpRow.Cells("Value").Value) - UC_Slider.CadToValue(tmpVariable.Value, tmpVariable.UnitsType, LengthUnits)) / 20
            Dim stepValue As Double = (CDbl(tmpRow.Cells("Value").Value) - UC_Slider.CadToValue(tmpVariable.Value, tmpVariable.UnitsType, LengthUnits)) / tmpSteps

            tmpRow.Tag = stepValue

        Next

    End Sub

    Private Sub BT_Save_Click(sender As Object, e As EventArgs) Handles BT_Save.Click

        If FLP_Events.Controls.Count > 0 Then

            Dim tmpRighe As String = ""

            For Each StepEvent As UC_WorkFlowEvent In FLP_Events.Controls

                Dim tmpRiga As String = ""

                For Each tmpRow As DataGridViewRow In StepEvent.DG_Variables.Rows

                    Dim tmpNome As String = tmpRow.Cells("Name").Value
                    Dim tmpValue As String = tmpRow.Cells("Value").Value

                    tmpRiga += (tmpNome & ":" & tmpValue & "|")

                Next

                tmpRiga = tmpRiga.Substring(0, tmpRiga.Length - 1)

                tmpRighe += tmpRiga & vbCrLf

            Next

            tmpRighe = tmpRighe.TrimEnd

            Dim tmpFileDialog As New SaveFileDialog
            tmpFileDialog.Filter = "Text File|*.txt"
            tmpFileDialog.Title = "Save a Text File"
            tmpFileDialog.ShowDialog()

            If tmpFileDialog.FileName <> "" Then

                Dim sw As StreamWriter
                sw = My.Computer.FileSystem.OpenTextFileWriter(tmpFileDialog.FileName, False)
                sw.Write(tmpRighe)
                sw.Close()

            End If

        End If

    End Sub

    Private Sub BT_Open_Click(sender As Object, e As EventArgs) Handles BT_Open.Click

        Dim tmpFileDialog As New OpenFileDialog
        tmpFileDialog.Filter = "Text File|*.txt"
        tmpFileDialog.Title = "Save a Text File"
        tmpFileDialog.ShowDialog()

        If tmpFileDialog.FileName <> "" Then

            Dim prg = My.Computer.FileSystem.ReadAllText(tmpFileDialog.FileName)
            Dim righe = prg.Replace(vbCrLf, vbCr).Split(vbCrLf)

            FLP_Events.SuspendLayout()

            For i = 0 To righe.Count - 1

                Dim tmpStep = NewEvent()

                FLP_Events.Controls.Add(tmpStep)

                tmpStep.DG_Variables.ClearSelection()

                SetValues(tmpStep, righe(i))

            Next

            FLP_Events.ResumeLayout()

            SetupAnchors()

        End If

    End Sub

    Private Sub SetValues(tmpStep As UC_WorkFlowEvent, riga As String)

        Dim variabili = Split(riga, "|")

        For Each item In variabili

            Dim valori = item.Split(":")

            For Each tmpRow As DataGridViewRow In tmpStep.DG_Variables.Rows

                If tmpRow.Cells.Item("Name").Value = valori(0) Then tmpRow.Cells.Item("Value").Value = valori(1)

            Next

        Next

    End Sub

    Private Sub BT_Close_Click(sender As Object, e As EventArgs) Handles BT_Close.Click

        FLP_Events.Controls.Clear()

    End Sub

    Private Sub Form_WorkFlow_Load(sender As Object, e As EventArgs) Handles Me.Load
        '################# Questo risolver il problema del bordo sgrazinato della ToolStrip
        ToolStrip1.Renderer = New MySR()
        '################# rif: https://stackoverflow.com/questions/1918247/how-to-disable-the-line-under-tool-strip-in-winform-c
    End Sub

    Friend Sub ReNumber()

        FLP_Events.SuspendLayout()

        Dim i = 1

        For Each item As UC_WorkFlowEvent In FLP_Events.Controls

            item.LB_SEQ.Text = i.ToString
            i += 1

        Next

        SetupAnchors()

        FLP_Events.ResumeLayout()

    End Sub

End Class

Public Class EventVariable

    Public Property Check As Boolean = True
    Public Property Name As String = ""
    Public Property Value As Double = 0
    Public Property objVar As Object

End Class