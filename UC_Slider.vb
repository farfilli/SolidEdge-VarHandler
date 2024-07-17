﻿
Imports System.ComponentModel
Imports SolidEdgeFramework

Public Class UC_Slider

    Dim VarName As String = ""

    Dim min As Integer = 0
    Dim max As Integer = 0

    Dim steps As Integer = 20
    Dim TrackbarStep As Integer

    Dim UnitType As SolidEdgeFramework.UnitTypeConstants
    Dim objVar As SolidEdgeFramework.variable

    Dim Multiplier As Integer = 10

    Public Function Valid() As Boolean

        If IsNothing(objVar) Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Sub New(objVarV As SolidEdgeFramework.variable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        objVar = objVarV

        UnitType = CType(objVar.UnitsType, UnitTypeConstants)

        If objVar.ExposeName <> "" Then VarName = objVar.ExposeName Else VarName = objVar.Name

        Dim minV As Double
        Dim maxV As Double

        objVar.GetValueRangeHighValue(maxV)
        objVar.GetValueRangeLowValue(minV)

        maxV = CadToValue(maxV, UnitType)
        minV = CadToValue(minV, UnitType)

        If CInt(maxV) <> 0 Or CInt(minV) <> 0 Then
            max = CInt(maxV)
            min = CInt(minV)
        End If

        If min = 0 And max = 0 Then
            min = CInt(CadToValue(objVar.Value, UnitType)) - 10
            max = CInt(CadToValue(objVar.Value, UnitType)) + 10
        End If

        If objVar.IsReadOnly Or objVar.Formula <> "" Then
            TrackBar.Visible = False
            LB_max.Visible = False
            LB_min.Visible = False
            Me.Height = CInt(Me.Height / 2)
            BT_Play.Visible = False
        End If

        SetTrackBar()

    End Sub

    Private Sub SetTrackBar()

        TrackBar.Minimum = min
        TrackBar.Maximum = max

        TrackbarStep = CInt((max - min) / steps)
        TrackBar.TickFrequency = TrackbarStep 'CInt((max - min) / steps)

        TrackBar.SmallChange = TrackBar.TickFrequency / 5
        TrackBar.LargeChange = TrackBar.TickFrequency

        GroupBox_Slider.Text = VarName
        LB_min.Text = min.ToString
        LB_max.Text = max.ToString

        If CadToValue(objVar.Value, UnitType) < TrackBar.Minimum Then
            TrackBar.Value = TrackBar.Minimum
            objVar.Value = ValueToCad(TrackBar.Value, UnitType)
        ElseIf CadToValue(objVar.Value, UnitType) > TrackBar.Maximum Then
            TrackBar.Value = TrackBar.Maximum
            objVar.Value = ValueToCad(TrackBar.Value, UnitType)
        Else
            TrackBar.Value = CInt(CadToValue(objVar.Value, UnitType))
        End If

        LB_value.Text = CadToValue(objVar.Value, UnitType).ToString  'TrackBar.Value.ToString

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

        objVar.Value = ValueToCad(TrackBar.Value, UnitType)

        LB_value.Text = TrackBar.Value.ToString

        UpdateLabel()

    End Sub

    Public Sub UpdateLabel()

        LB_value.Text = CadToValue(objVar.Value, UnitType).ToString

        LB_name.Text = objVar.Name & " = " & LB_value.Text

        If UnitType = SolidEdgeFramework.UnitTypeConstants.igUnitDistance Then
            LB_name.Text = LB_name.Text & " mm"
        ElseIf UnitType = SolidEdgeFramework.UnitTypeConstants.igUnitAngle Then
            LB_name.Text = LB_name.Text & " °"
        End If

    End Sub

    Private Sub LB_min_Click(sender As Object, e As EventArgs) Handles LB_min.Click

        Try
            min = CInt(InputBox("Minimum value"))
        Catch ex As Exception
            Exit Sub
        End Try

        '15 for conditions is (=> ; <=)
        objVar.SetValueRangeValues(min, 15, max)

        SetTrackBar()

    End Sub

    Private Sub LB_max_Click(sender As Object, e As EventArgs) Handles LB_max.Click

        Try
            max = CInt(InputBox("Maximum value"))
        Catch ex As Exception
            Exit Sub
        End Try

        '15 for conditions is (=> ; <=)
        objVar.SetValueRangeValues(min, 15, max)

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

    Public Shared Function CadToValue(Value As Double, UnitType As SolidEdgeFramework.UnitTypeConstants) As Double

        If UnitType = SolidEdgeFramework.UnitTypeConstants.igUnitDistance Then

            CadToValue = Value * 1000

        ElseIf UnitType = SolidEdgeFramework.UnitTypeConstants.igUnitAngle Then

            CadToValue = Value * 180 / Math.PI

        Else

            CadToValue = Value

        End If

    End Function

    Public Shared Function ValueToCad(Value As Double, UnitType As SolidEdgeFramework.UnitTypeConstants) As Double

        If UnitType = SolidEdgeFramework.UnitTypeConstants.igUnitDistance Then

            ValueToCad = Value / 1000

        ElseIf UnitType = SolidEdgeFramework.UnitTypeConstants.igUnitAngle Then

            ValueToCad = Value * Math.PI / 180

        Else

            ValueToCad = Value

        End If

    End Function

    Private Sub BT_Play_Click(sender As Object, e As EventArgs) Handles BT_Play.Click

        If BT_Play.Tag = "Play" Then

            BT_Play.Image = My.Resources._Stop
            BT_Play.Tag = "Stop"

            BG_Play.WorkerSupportsCancellation = True
            BG_Play.WorkerReportsProgress = True

            BG_Play.RunWorkerAsync(TrackBar.Value)

        Else

            BT_Play.Image = My.Resources.Play
            BT_Play.Tag = "Play"

            If BG_Play.IsBusy Then BG_Play.CancelAsync()

        End If

    End Sub

    Private Sub BG_Play_DoWork(sender As Object, e As DoWorkEventArgs) Handles BG_Play.DoWork

        Dim ProgressValue As Integer = CInt(e.Argument)

        Do Until ProgressValue = max
            If ProgressValue + TrackBarStep > max Then
                ProgressValue = max
            Else
                ProgressValue += TrackBarStep
            End If

            objVar.Value = ValueToCad(ProgressValue, UnitType)

            'Example for future point tracking
            'For Each item As UC_Slider In Me.Parent.Controls
            '    If item.objVar.Name = "F" Or item.objVar.Name = "C" Then
            '        Console.WriteLine(item.objVar.Name & " - " & item.objVar.Value.ToString)
            '    End If
            'Next

            BG_Play.ReportProgress((ProgressValue - min / max - min) * 100, ProgressValue.ToString)

            If BG_Play.CancellationPending Then

                e.Cancel = True
                Return

            End If

        Loop

    End Sub

    Private Sub BG_Play_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BG_Play.RunWorkerCompleted

        BT_Play.Image = My.Resources.Play
        BT_Play.Tag = "Play"

        UpdateLabel() '<-- A lavoro completato scateniamo l'aggiornamento di tutta l'interfaccia. I valori restituiti da Solid Edge dovrebbero essere tutti corretti

    End Sub


    Private Sub BG_Play_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BG_Play.ProgressChanged

        TrackBar.Value = CInt(e.UserState)

        'LB_value.Text = "" <--- questo causa l'evento nel form principale che scatena l'aggiornamento di tutti gli Slider e rende l'interfaccia non responsiva.

        LB_name.Text = objVar.Name & " = " & e.UserState

        If UnitType = SolidEdgeFramework.UnitTypeConstants.igUnitDistance Then
            LB_name.Text = LB_name.Text & " mm"
        ElseIf UnitType = SolidEdgeFramework.UnitTypeConstants.igUnitAngle Then
            LB_name.Text = LB_name.Text & " °"
        End If

        'For Each item As UC_Slider In Me.Parent.Controls

        '    item.LB_name.Text = item.objVar.Name & " = " & CadToValue(item.objVar.Value, UnitType).ToString

        '    If UnitType = SolidEdgeFramework.UnitTypeConstants.igUnitDistance Then
        '        LB_name.Text = LB_name.Text & " mm"
        '    ElseIf UnitType = SolidEdgeFramework.UnitTypeConstants.igUnitAngle Then
        '        LB_name.Text = LB_name.Text & " °"
        '    End If

        'Next

    End Sub



    Public Function GetMin() As Integer

        Return min

    End Function

    Public Function GetMax() As Integer

        Return max

    End Function

    Public Function GetSteps() As Integer

        Return steps

    End Function


End Class
