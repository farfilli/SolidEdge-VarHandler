
Imports System.ComponentModel
Imports System.IO
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports SolidEdgeConstants
Imports SolidEdgeFramework
Imports SolidEdgeFrameworkSupport
Imports SolidEdgePart

Public Class UC_Slider

    Dim VarName As String = ""

    'Dim min As Integer = 0
    'Dim max As Integer = 0
    Dim min As Double = 0
    Dim max As Double = 0
    Dim StepWidth As Double
    Dim steps As Integer = 20
    Dim TrackbarStep As Integer

    Dim UnitType As SolidEdgeFramework.UnitTypeConstants
    Public objVar As Object 'SolidEdgeFramework.variable
    Public objDoc As SolidEdgeDocument
    Public LengthUnits As SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants

    Dim Multiplier As Integer = 10
    Dim PlayLoop As Boolean = False
    Dim Forward As Boolean = True
    Dim Export As Boolean = False
    Dim ExportSteps As List(Of Object)
    Public UpdateDoc As Boolean = False
    Public SaveImages As Boolean = False
    Public CheckInterference As Boolean = False
    Dim ViewOnly As Boolean = False

    Public Function Valid() As Boolean

        If IsNothing(objVar) Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Sub New(objVarV As Object) 'SolidEdgeFramework.variable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        objVar = objVarV

        UnitType = CType(objVar.UnitsType, SolidEdgeFramework.UnitTypeConstants)

        If objVar.ExposeName <> "" Then VarName = objVar.ExposeName Else VarName = objVar.Name

        Dim minV As Double
        Dim maxV As Double

        objVar.GetValueRangeHighValue(maxV)
        objVar.GetValueRangeLowValue(minV)

        maxV = CadToValue(maxV, UnitType, LengthUnits)
        minV = CadToValue(minV, UnitType, LengthUnits)

        'Try
        '    If CInt(maxV) <> 0 Or CInt(minV) <> 0 Then
        '        max = CInt(maxV)
        '        min = CInt(minV)
        '    End If
        'Catch ex As Exception
        '    max = 0
        '    min = 0
        'End Try

        max = maxV
        min = minV


        If min = 0 And max = 0 Then
            'min = CInt(CadToValue(objVar.Value, UnitType, LengthUnits)) - 10
            'max = CInt(CadToValue(objVar.Value, UnitType, LengthUnits)) + 10
            min = CadToValue(objVar.Value, UnitType, LengthUnits) - 10
            max = CadToValue(objVar.Value, UnitType, LengthUnits) + 10
        End If

        StepWidth = (max - min) / steps

        If objVar.IsReadOnly Or objVar.Formula <> "" Then

            Me.BackColor = Color.WhiteSmoke

            ViewOnly = True

            TrackBar.Visible = False
            LB_max.Visible = False
            BT_Play.Visible = False
            BT_Loop.Visible = False
            BT_Settings.Visible = False

            If objVar.Formula <> "" Then
                LB_min.Text = "Formula " & objVar.Formula
                LB_min.Enabled = False
                Me.Height = CInt(Me.Height / 1.6)
            Else
                LB_min.Visible = False
                Me.Height = CInt(Me.Height / 2.2)
            End If

        End If

        SetTrackBar()

    End Sub

    Private Sub SetTrackBar()

        'TrackBar.Minimum = min
        'TrackBar.Maximum = max
        TrackBar.Minimum = 0
        TrackBar.Maximum = 100

        TrackbarStep = Math.Round((TrackBar.Maximum - TrackBar.Minimum) / steps)
        TrackBar.TickFrequency = TrackbarStep 'CInt((max - min) / steps)

        TrackBar.SmallChange = TrackBar.TickFrequency ' / 5
        TrackBar.LargeChange = TrackBar.TickFrequency

        GroupBox_Slider.Text = VarName
        If Not ViewOnly Then LB_min.Text = min.ToString
        LB_max.Text = max.ToString

        'If CadToValue(objVar.Value, UnitType, LengthUnits) < TrackBar.Minimum Then
        '    TrackBar.Value = TrackBar.Minimum
        '    objVar.Value = ValueToCad(TrackBar.Value, UnitType, LengthUnits)
        'ElseIf CadToValue(objVar.Value, UnitType, LengthUnits) > TrackBar.Maximum Then
        '    TrackBar.Value = TrackBar.Maximum
        '    objVar.Value = ValueToCad(TrackBar.Value, UnitType, LengthUnits)
        'Else
        '    TrackBar.Value = CInt(CadToValue(objVar.Value, UnitType, LengthUnits))
        'End If

        Dim tmpValue As Double = CadToValue(objVar.Value, UnitType, LengthUnits)
        Dim Percentile = (tmpValue - min) / (max - min)
        TrackBar.Value = Math.Round((TrackBar.Maximum - TrackBar.Minimum) * Percentile + TrackBar.Minimum)


        LB_value.Text = tmpValue.ToString  'TrackBar.Value.ToString

        LB_name.Text = objVar.Name

        If objVar.GetComment = "Autotune" Then
            BT_Pinned.Tag = "Checked"
            BT_Pinned.Image = My.Resources.Checked
        End If

        LB_value.ForeColor = Color.Transparent

        UpdateLabel()

    End Sub

    Private Sub BT_Delete_Click(sender As Object, e As EventArgs) Handles BT_Delete.Click

        Dim tmpFLP As FlowLayoutPanel = CType(Me.Parent, FlowLayoutPanel)
        If tmpFLP.Controls.Contains(Me) Then tmpFLP.Controls.Remove(Me)

    End Sub

    Private Sub TrackBar_Scroll(sender As Object, e As EventArgs) Handles TrackBar.Scroll

        Try
            Dim Percentile = (TrackBar.Value - TrackBar.Minimum) / (TrackBar.Maximum - TrackBar.Minimum)
            objVar.Value = ValueToCad((max - min) * Percentile + min, UnitType, LengthUnits)

            'objVar.Value = ValueToCad(TrackBar.Value, UnitType, LengthUnits)
            LB_value.Text = CadToValue(objVar.Value, UnitType, LengthUnits).ToString
            UpdateLabel()

        Catch ex As Exception

        End Try

    End Sub

    Public Sub UpdateLabel()

        LB_value.Text = CadToValue(objVar.Value, UnitType, LengthUnits).ToString

        LB_name.Text = objVar.Name & " = " & LB_value.Text

        If UnitType = SolidEdgeFramework.UnitTypeConstants.igUnitDistance Then
            If LengthUnits = SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants.seLengthInch Then
                LB_name.Text = LB_name.Text & " in"
            ElseIf LengthUnits = SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants.seLengthMillimeter Then
                LB_name.Text = LB_name.Text & " mm"
            End If
        ElseIf UnitType = SolidEdgeFramework.UnitTypeConstants.igUnitAngle Then
            LB_name.Text = LB_name.Text & " °"
        End If

    End Sub

    Private Sub LB_min_Click(sender As Object, e As EventArgs) Handles LB_min.Click

        Try
            'min = CInt(InputBox("Minimum value"))
            min = InputBox("Minimum value")
        Catch ex As Exception
            Exit Sub
        End Try

        '15 for conditions is (=> ; <=)
        objVar.SetValueRangeValues(ValueToCad(min, objVar.UnitsType, LengthUnits), 15, ValueToCad(max, objVar.UnitsType, LengthUnits))

        SetTrackBar()

    End Sub

    Private Sub LB_max_Click(sender As Object, e As EventArgs) Handles LB_max.Click

        Try
            'max = CInt(InputBox("Maximum value"))
            max = InputBox("Maximum value")
        Catch ex As Exception
            Exit Sub
        End Try

        '15 for conditions is (=> ; <=)
        objVar.SetValueRangeValues(ValueToCad(min, objVar.UnitsType, LengthUnits), 15, ValueToCad(max, objVar.UnitsType, LengthUnits))

        SetTrackBar()

    End Sub

    Private Sub GroupBox_Slider_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles GroupBox_Slider.MouseDoubleClick

        Try
            VarName = InputBox("Control name",, objVar.ExposeName)
        Catch ex As Exception
            Exit Sub
        End Try

        Dim tmpExposed As Integer = objVar.Expose

        objVar.ExposeName = VarName
        objVar.Expose = tmpExposed

        SetTrackBar()

    End Sub

    Private Sub BT_Pinned_Click(sender As Object, e As EventArgs) Handles BT_Pinned.Click

        If BT_Pinned.Tag.ToString = "Unchecked" Then

            BT_Pinned.Tag = "Checked"
            BT_Pinned.Image = My.Resources.Checked
            objVar.SetComment("Autotune")

        Else

            BT_Pinned.Tag = "Unchecked"
            BT_Pinned.Image = My.Resources.Unchecked
            objVar.SetComment("")

        End If

    End Sub

    Public Shared Function CadToValue(
        Value As Double,
        UnitType As SolidEdgeFramework.UnitTypeConstants,
        LengthUnits As SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants
        ) As Double

        If UnitType = SolidEdgeFramework.UnitTypeConstants.igUnitDistance Then

            If LengthUnits = SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants.seLengthInch Then

                CadToValue = Value * 1000 / 25.4

            ElseIf LengthUnits = SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants.seLengthMillimeter Then

                CadToValue = Value * 1000

            Else

                MsgBox(String.Format("Unrecognized length units '{0}'", LengthUnits.ToString), MsgBoxStyle.Critical)

                CadToValue = Value

            End If

        ElseIf UnitType = SolidEdgeFramework.UnitTypeConstants.igUnitAngle Then

            CadToValue = Value * 180 / Math.PI

        Else

            CadToValue = Value

        End If

    End Function

    Public Shared Function ValueToCad(
        Value As Double,
        UnitType As SolidEdgeFramework.UnitTypeConstants,
        LengthUnits As SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants
        ) As Double

        If UnitType = SolidEdgeFramework.UnitTypeConstants.igUnitDistance Then

            If LengthUnits = SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants.seLengthInch Then

                ValueToCad = Value * 25.4 / 1000

            ElseIf LengthUnits = SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants.seLengthMillimeter Then

                ValueToCad = Value / 1000

            Else

                MsgBox(String.Format("Unrecognized length units '{0}'", LengthUnits.ToString), MsgBoxStyle.Critical)

                ValueToCad = Value

            End If

        ElseIf UnitType = SolidEdgeFramework.UnitTypeConstants.igUnitAngle Then

            ValueToCad = Value * Math.PI / 180

        Else

            ValueToCad = Value

        End If

    End Function

    Private Sub BT_Play_Click(sender As Object, e As EventArgs) Handles BT_Play.Click

        If BT_Play.Tag = "Play" Then

            Export = CheckExport()

            BT_Play.Image = My.Resources._Stop
            BT_Play.Tag = "Stop"

            BG_Play.WorkerSupportsCancellation = True
            BG_Play.WorkerReportsProgress = True

            'BG_Play.RunWorkerAsync(TrackBar.Value)
            BG_Play.RunWorkerAsync(CadToValue(objVar.Value, UnitType, LengthUnits))

            For Each item As Object In Me.Parent.Controls

                If TypeOf (item) Is UC_Slider Then
                    Dim tmpSlider As UC_Slider = item
                    If item IsNot Me Then tmpSlider.BT_Play.Enabled = False
                End If

            Next

        Else

            BT_Play.Image = My.Resources.Play
            BT_Play.Tag = "Play"

            If BG_Play.IsBusy Then BG_Play.CancelAsync()

        End If

    End Sub

    Private Function CheckExport() As Boolean

        Dim tmpMe As Form_VarHandler = Me.Parent.Parent

        If tmpMe.BT_Export.Checked Then

            For Each item As Object In Me.Parent.Controls

                If TypeOf item Is UC_Slider Then

                    Dim tmpItem As UC_Slider = CType(item, UC_Slider)

                    If tmpItem.BT_Loop.Tag = "Checked" Then

                        tmpItem.BT_Loop_Click(Me, Nothing)

                    End If

                End If

            Next

            Return True

        Else

            Return False

        End If

    End Function

    Private Function CloseEnough(This As Double, That As Double, Threshold As Double) As Boolean
        Dim Result As Boolean = True

        If This = 0 And That = 0 Then Return True

        If Not This = 0 Then
            Result = Math.Abs(Threshold) > Math.Abs((This - That) / This)
        Else
            Result = Math.Abs(Threshold) > Math.Abs((This - That) / That)
        End If

        Return Result
    End Function

    Private Sub BG_Play_DoWork(sender As Object, e As DoWorkEventArgs) Handles BG_Play.DoWork

        'Dim ProgressValue As Integer = CInt(e.Argument)
        Dim ProgressValue As Double = e.Argument

        ExportSteps = New List(Of Object) From {
            GenerateStep()
        }

        Dim Idx As Integer = 0

        Do 'Until ProgressValue = max

            ' Process the initial state
            If Idx = 0 Then
                If UpdateDoc Then DoUpdateDoc(objDoc)

                If SaveImages Then DoSaveImage(objDoc)

                If CheckInterference Then If Not DoCheckInterference(objDoc) Then Return
            End If

            If Forward Then

                'If ProgressValue + TrackbarStep >= max Then
                '    ProgressValue = max
                'Else
                '    ProgressValue += TrackbarStep
                'End If

                If CloseEnough(ProgressValue + StepWidth, max, Threshold:=0.001) Then
                    ProgressValue = max
                Else
                    ProgressValue += StepWidth
                End If
            Else
                'If ProgressValue - TrackbarStep <= min Then
                '    ProgressValue = min
                'Else
                '    ProgressValue -= TrackbarStep
                'End If

                If CloseEnough(ProgressValue - StepWidth, min, Threshold:=0.001) Then
                    ProgressValue = min
                Else
                    ProgressValue -= StepWidth
                End If
            End If

            objVar.Value = ValueToCad(ProgressValue, UnitType, LengthUnits)


            'If objDoc.Type = SolidEdgeConstants.DocumentTypeConstants.igAssemblyDocument And UpdateDoc Then objDoc.UpdateDocument 'objDoc.Parent.StartCommand(11292)

            If UpdateDoc Then DoUpdateDoc(objDoc)

            If SaveImages Then DoSaveImage(objDoc)

            If CheckInterference Then If Not DoCheckInterference(objDoc) Then Return

            'Example for future point tracking ################################################
            'If Export Then

            ExportSteps.Add(GenerateStep)

            'End If




            BG_Play.ReportProgress(((ProgressValue - min) / (max - min)) * 100, ProgressValue.ToString)

            If BG_Play.CancellationPending Then

                e.Cancel = True
                Return

            End If

            If Forward Then

                If ProgressValue = max Then
                    Forward = False
                    If Not PlayLoop Then Return
                End If

            Else

                If ProgressValue = min Then
                    Forward = True
                    If Not PlayLoop Then Return
                End If

            End If

            Idx += 1
        Loop

    End Sub

    Private Function GenerateStep() As List(Of (Nome As String, Valore As Double))

        Dim tmpStep As New List(Of (Nome As String, Valore As Double))
        Dim tmpForm = CType(Me.Parent.Parent, Form_VarHandler)

        For Each item As Object In Me.Parent.Controls

            If TypeOf (item) Is UC_Slider Then

                Dim tmpItem = CType(item, UC_Slider)

                Dim tmpValue As (Nome As String, Valore As Double)
                tmpValue.Nome = tmpItem.objVar.name.ToString
                tmpValue.Valore = CadToValue(tmpItem.objVar.value, tmpItem.UnitType, LengthUnits)

                tmpStep.Add(tmpValue)

            End If

        Next

        If tmpForm.Tracking Then

            If Not IsNothing(tmpForm.Tracker_3D) Then

                Dim objXOffset As Double = Nothing
                Dim objYOffset As Double = Nothing
                Dim objZOffset As Double = Nothing
                Dim objXRotation As Double = Nothing
                Dim objYRotation As Double = Nothing
                Dim objZRotation As Double = Nothing
                Dim objRtP As Boolean = Nothing
                Dim objZFirstRot As Double = Nothing
                tmpForm.Tracker_3D.GetOrientation(objXOffset, objYOffset, objZOffset, objXRotation, objYRotation, objZRotation, objRtP, objZFirstRot)

                Dim tmpValueX As (Nome As String, Valore As Double)
                tmpValueX.Nome = "Tracker X"
                tmpValueX.Valore = CadToValue(objXOffset, SolidEdgeFramework.UnitTypeConstants.igUnitDistance, LengthUnits)
                tmpStep.Add(tmpValueX)

                Dim tmpValueY As (Nome As String, Valore As Double)
                tmpValueY.Nome = "Tracker Y"
                tmpValueY.Valore = CadToValue(objYOffset, SolidEdgeFramework.UnitTypeConstants.igUnitDistance, LengthUnits)
                tmpStep.Add(tmpValueY)

                Dim tmpValueZ As (Nome As String, Valore As Double)
                tmpValueZ.Nome = "Tracker Z"
                tmpValueZ.Valore = CadToValue(objZOffset, SolidEdgeFramework.UnitTypeConstants.igUnitDistance, LengthUnits)
                tmpStep.Add(tmpValueZ)

            ElseIf Not IsNothing(tmpForm.Tracker_2D) Then

                Dim objX As Double = Nothing
                Dim objY As Double = Nothing
                tmpForm.Tracker_2D.GetOrigin(objX, objY)

                Dim tmpValueX As (Nome As String, Valore As Double)
                tmpValueX.Nome = "Tracker X"
                tmpValueX.Valore = CadToValue(objX, SolidEdgeFramework.UnitTypeConstants.igUnitDistance, LengthUnits)
                tmpStep.Add(tmpValueX)

                Dim tmpValueY As (Nome As String, Valore As Double)
                tmpValueY.Nome = "Tracker Y"
                tmpValueY.Valore = CadToValue(objY, SolidEdgeFramework.UnitTypeConstants.igUnitDistance, LengthUnits)
                tmpStep.Add(tmpValueY)

            End If

        End If

        Return tmpStep

    End Function

    Private Sub BG_Play_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BG_Play.RunWorkerCompleted

        BT_Play.Image = My.Resources.Play
        BT_Play.Tag = "Play"

        UpdateLabel() '<-- A lavoro completato scateniamo l'aggiornamento di tutta l'interfaccia. I valori restituiti da Solid Edge dovrebbero essere tutti corretti

        Dim ClosedCurve As Boolean = False

        For Each item As Object In Me.Parent.Controls

            If TypeOf (item) Is UC_Slider Then

                Dim tmpItem As UC_Slider = CType(item, UC_Slider)
                If tmpItem IsNot Me Then tmpItem.BT_Play.Enabled = True
            ElseIf TypeOf (item) Is UC_Tracker Then

                Dim tmpItem As UC_Tracker = CType(item, UC_Tracker)
                ClosedCurve = tmpItem.ClosedCurve

            End If

        Next

        If Export Then ExportResult()

        Dim tmpForm = CType(Me.Parent.Parent, Form_VarHandler)
        If tmpForm.Trace Then Trace(Not IsNothing(tmpForm.Tracker_2D), ClosedCurve)

    End Sub

    Private Sub Trace(Trace2D As Boolean, ClosedCurve As Boolean)

        Dim tmpList As New List(Of Double)

        For Each item In ExportSteps

            For Each stepItem As (Nome As String, Valore As Double) In item

                Select Case stepItem.Nome

                    Case = "Tracker X", "Tracker Y", "Tracker Z"
                        tmpList.Add(ValueToCad(stepItem.Valore, SolidEdgeFramework.UnitTypeConstants.igUnitDistance, LengthUnits))

                End Select

            Next

        Next

        'If tmpList.Count <> 0 Then

        If Trace2D Then

            Dim tmpForm = CType(Me.Parent.Parent, Form_VarHandler)
            Dim objDoc As SolidEdgeDraft.DraftDocument = tmpForm.objDoc
            Dim tmpBsplineCurves2d = objDoc.ActiveSheet.BsplineCurves2d
            Dim bSplineCurve2d As SolidEdgeFrameworkSupport.BSplineCurve2d = Nothing

            Dim Points = tmpList.ToArray

            Try
                bSplineCurve2d = tmpBsplineCurves2d.AddByPointsWithCloseOption(4, Points.Length \ 2, Points, ClosedCurve)
            Catch ex As Exception
                MessageBox.Show("Error while drawing path. Possibly too many steps or trace points too close together.", "VarHandler")
            End Try

        Else

            Dim tmpForm = CType(Me.Parent.Parent, Form_VarHandler)
            Dim objDoc As SolidEdgeDocument = tmpForm.objDoc
            Dim bSplineCurve3d As SolidEdgePart.BSplineCurve3D = Nothing
            Dim Points = tmpList.ToArray


            Dim objSketches3D = objDoc.Sketches3D
            Dim objSketch3D = objSketches3D.Add()
            Dim objBspLines3D = objSketch3D.BSplineCurves3D


            Try
                bSplineCurve3d = objBspLines3D.AddByPoints(Points.Length \ 3, Points, ClosedCurve)
            Catch ex As Exception
                MessageBox.Show("Error while drawing path. Possibly too many steps or trace points too close together.", "VarHandler")
                objSketch3D.Delete
            End Try

        End If

        'End If

    End Sub


    Private Sub ExportResult()

        'Me.Cursor = Cursors.WaitCursor

        'Dim objApp As Excel.Application
        'Dim objBook As Excel._Workbook
        'Dim objBooks As Excel.Workbooks
        'Dim objSheets As Excel.Sheets
        'Dim objSheet As Excel._Worksheet

        'objApp = New Excel.Application()
        'objBooks = objApp.Workbooks
        'objBook = objBooks.Add
        'objSheets = objBook.Worksheets
        'objSheet = objSheets(1)

        'Dim Riga = 2
        'For Each item In ExportSteps

        '    Dim Colonna = 2
        '    For Each stepItem As (Nome As String, Valore As Double) In item

        '        objSheet.Cells(1, Colonna).value = stepItem.Nome
        '        objSheet.Cells(Riga, 1).value = Riga - 1
        '        objSheet.Cells(Riga, Colonna).value = stepItem.Valore

        '        Colonna += 1

        '    Next

        '    Riga += 1

        'Next

        ''objSheet.Range("A1:X1").EntireColumn.AutoFit()
        'objSheet.Cells.Select()

        ''With objApp.Selection.Font
        ''    .Name = "Calibri"
        ''    .Size = 10
        ''End With

        'objSheet.Range("A1").Select()

        'objApp.Visible = True
        'objBook.Activate()

        'Me.Cursor = Cursors.Default

    End Sub

    Private Sub BG_Play_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BG_Play.ProgressChanged

        'TrackBar.Value = CInt(e.UserState)
        Dim tmpValue = CDbl(e.UserState)
        Dim Percentile As Double = (tmpValue - min) / (max - min)
        TrackBar.Value = Math.Round((TrackBar.Maximum - TrackBar.Minimum) * Percentile - TrackBar.Minimum)

        'LB_value.Text = "" <--- questo causa l'evento nel form principale che scatena l'aggiornamento di tutti gli Slider e rende l'interfaccia non responsiva.

        Try
            LB_name.Text = objVar.Name & " = " & e.UserState

            If UnitType = SolidEdgeFramework.UnitTypeConstants.igUnitDistance Then
                If LengthUnits = SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants.seLengthMillimeter Then
                    LB_name.Text = LB_name.Text & " mm"
                ElseIf LengthUnits = SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants.seLengthInch Then
                    LB_name.Text = LB_name.Text & " in"
                End If
            ElseIf UnitType = SolidEdgeFramework.UnitTypeConstants.igUnitAngle Then
                LB_name.Text = LB_name.Text & " °"
            End If
        Catch ex As Exception

        End Try

    End Sub



    'Public Function GetMin() As Integer

    '    Return min

    'End Function

    'Public Function GetMax() As Integer

    '    Return max

    'End Function

    'Public Function GetSteps() As Integer

    '    Return steps

    'End Function

    Private Sub BT_Loop_Click(sender As Object, e As EventArgs) Handles BT_Loop.Click

        If BT_Loop.Tag = "Checked" Then

            BT_Loop.Tag = "Unchecked"
            BT_Loop.BackColor = Color.Transparent
            PlayLoop = False

        Else

            BT_Loop.Tag = "Checked"
            BT_Loop.BackColor = Color.Gainsboro
            PlayLoop = True

        End If

    End Sub

    Private Sub LB_name_Click(sender As Object, e As EventArgs) Handles LB_name.Click

        If objVar.IsReadOnly Or objVar.Formula <> "" Then Return

        'Try
        '    TrackBar.Value = InputBox("Set current value",, LB_value.Text)
        'Catch ex As Exception
        '    Exit Sub
        'End Try

        'objVar.Value = ValueToCad(TrackBar.Value, UnitType, LengthUnits)

        'LB_value.Text = TrackBar.Value.ToString

        'UpdateLabel()

        Try
            Dim tmpValue As Double = CDbl(InputBox("Set current value",, LB_value.Text))

            objVar.Value = ValueToCad(tmpValue, UnitType, LengthUnits)

            Dim Percentile As Double = (tmpValue - min) / (max - min)

            TrackBar.Value = Math.Round((TrackBar.Maximum - TrackBar.Minimum) * Percentile - TrackBar.Minimum)

            LB_value.Text = tmpValue.ToString

            UpdateLabel()
        Catch ex As Exception
            Exit Sub
        End Try


    End Sub

    Private Sub LB_MouseHover(sender As Object, e As EventArgs) Handles LB_min.MouseHover, LB_max.MouseHover, LB_name.MouseHover

        If objVar.IsReadOnly Or objVar.Formula <> "" Then Return
        sender.ForeColor = Color.DarkBlue
        sender.font = New System.Drawing.Font(LB_min.Font, FontStyle.Bold)

    End Sub

    Private Sub LB_MouseLeave(sender As Object, e As EventArgs) Handles LB_min.MouseLeave, LB_max.MouseLeave, LB_name.MouseLeave

        If objVar.IsReadOnly Or objVar.Formula <> "" Then Return
        sender.ForeColor = Color.Black
        sender.font = New System.Drawing.Font(LB_min.Font, FontStyle.Regular)

    End Sub

    Private Sub BT_Settings_Click(sender As Object, e As EventArgs) Handles BT_Settings.Click

        If objVar.IsReadOnly Or objVar.Formula <> "" Then Return

        Try
            steps = InputBox("Set number of steps",, steps)
            StepWidth = (max - min) / steps
        Catch ex As Exception
            Exit Sub
        End Try

        SetTrackBar()

    End Sub

    Public Sub DisablePlay()

        BT_Play.Enabled = False

    End Sub

    Public Sub EnablePlay()

        BT_Play.Enabled = True

    End Sub

    Public Shared Sub DoUpdateDoc(_objDoc As SolidEdgeFramework.SolidEdgeDocument)
        If Not _objDoc.Type = SolidEdgeConstants.DocumentTypeConstants.igAssemblyDocument Then
            ' Nothing to do here
            Return
        End If

        CType(_objDoc, SolidEdgeAssembly.AssemblyDocument).UpdateDocument()
    End Sub

    Public Shared Sub DoSaveImage(_objDoc As SolidEdgeFramework.SolidEdgeDocument)

        If _objDoc.Type = SolidEdgeConstants.DocumentTypeConstants.igDraftDocument Then
            ' Nothing to do here
            Return
        End If

        Dim Dirname = System.IO.Path.GetDirectoryName(_objDoc.FullName)
        Dirname = String.Format("{0}\images", Dirname)
        If Not FileIO.FileSystem.DirectoryExists(Dirname) Then
            FileIO.FileSystem.CreateDirectory(Dirname)
        End If

        Dim Idx As Integer
        Dim IdxMax As Integer = 0

        Dim DI As New DirectoryInfo(Dirname)

        If DI.GetFiles.Count = 0 Then
            Idx = 0
        Else
            For Each File As FileInfo In DI.GetFiles
                If System.IO.Path.GetExtension(File.Name) = ".jpg" Then
                    Dim i = CInt(System.IO.Path.GetFileNameWithoutExtension(File.Name))
                    If i > IdxMax Then IdxMax = i
                End If
            Next
            Idx = IdxMax + 1
        End If

        Dim Filename As String = String.Format("{0}\{1:D5}.jpg", Dirname, Idx)

        Dim Window As SolidEdgeFramework.Window
        Dim View As SolidEdgeFramework.View

        Window = CType(_objDoc.Application.ActiveWindow, SolidEdgeFramework.Window)
        View = Window.View

        View.SaveAsImage(Filename)

    End Sub

    Public Shared Function DoCheckInterference(_objDoc As SolidEdgeFramework.SolidEdgeDocument) As Boolean

        Dim Proceed As Boolean = True

        If Not _objDoc.Type = SolidEdgeConstants.DocumentTypeConstants.igAssemblyDocument Then
            ' Nothing to do here
            Return True
        End If

        Dim tmpSEDoc = CType(_objDoc, SolidEdgeAssembly.AssemblyDocument)

        Dim ComparisonMethod = SolidEdgeConstants.InterferenceComparisonConstants.seInterferenceComparisonSet1vsItself
        Dim Status As SolidEdgeAssembly.InterferenceStatusConstants
        Dim Occurrences As SolidEdgeAssembly.Occurrences = tmpSEDoc.Occurrences
        Dim Occurrence As SolidEdgeAssembly.Occurrence = Nothing
        Dim i As Integer
        Dim NumInterferences As Object = Nothing
        Dim IgnoreT = SolidEdgeConstants.InterferenceOptionsConstants.seIntfOptIgnoreThreadVsNonThreaded
        Dim IgnoreD = SolidEdgeConstants.InterferenceOptionsConstants.seIntfOptIgnoreSameNominalDia


        Dim SetList As New List(Of Object)

        For i = 1 To Occurrences.Count
            SetList.Add(Occurrences.Item(i))
        Next

        If SetList.Count > 0 Then
            Try
                tmpSEDoc.CheckInterference2(
                    NumElementsSet1:=SetList.Count,
                    Set1:=SetList.ToArray,
                    Status:=Status,
                    ComparisonMethod:=ComparisonMethod,
                    AddInterferenceAsOccurrence:=False,
                    NumInterferences:=NumInterferences,
                    IgnoreSameNominalDiaConstant:=IgnoreD,
                    IgnoreNonThreadVsThreadConstant:=IgnoreT)

                If Not Status = SolidEdgeAssembly.InterferenceStatusConstants.seInterferenceStatusNoInterference Then
                    Dim s As String = "Interference detected."
                    s = String.Format("{0}{1} Click OK to continue, Cancel to quit.", s, vbCrLf)
                    Dim Result As MsgBoxResult = MsgBox(s, vbOKCancel)
                    If Result = MsgBoxResult.Cancel Then
                        Proceed = False
                    End If
                End If

            Catch ex As Exception
                Dim s As String = "Error on interference check."
                s = String.Format("{0}{1} Click OK to continue, Cancel to quit.", s, vbCrLf)
                Dim Result As MsgBoxResult = MsgBox(s, vbOKCancel)
                If Result = MsgBoxResult.Cancel Then
                    Proceed = False
                End If
            End Try
        End If

        Return Proceed
    End Function

End Class
