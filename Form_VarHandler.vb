﻿Imports System.ComponentModel
Imports System.Net.WebRequestMethods
Imports SolidEdgeFramework

Public Class Form_VarHandler

    Dim objApp As SolidEdgeFramework.Application
    Public objDoc As SolidEdgeFramework.SolidEdgeDocument
    Public LengthUnits As SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants

    Public Tracker_3D As SolidEdgePart.CoordinateSystem
    Public Tracker_2D As SolidEdgeDraft.BlockOccurrence
    Public Tracking As Boolean = False
    Public Trace As Boolean = False
    Private Sub BT_Aggiungi_Click(sender As Object, e As EventArgs) Handles BT_Aggiungi.Click

        Dim tmpForm As New Form_SelectVariable
        tmpForm.objDoc = objDoc
        tmpForm.LengthUnits = LengthUnits
        tmpForm.ShowDialog(Me)

        If tmpForm.Valid Then

            For Each item In tmpForm.ListBox_Variables.SelectedItems

                Dim tmpSlider2 As New UC_Slider(item.objVariable)

                If tmpSlider2.Valid Then

                    tmpSlider2.objDoc = objDoc
                    tmpSlider2.LengthUnits = LengthUnits
                    tmpSlider2.UpdateDoc = BT_Update.Checked
                    AddHandler tmpSlider2.LB_value.TextChanged, AddressOf Slider_Click

                    FLP_Vars.Controls.Add(tmpSlider2)

                    SetupAnchors()

                End If

            Next

        End If


    End Sub

    Private Sub Slider_Click(sender As Object, e As EventArgs)

        For Each tmpSlider As Object In FLP_Vars.Controls

            tmpSlider.UpdateLabel()

        Next

        If objDoc.Type = DocumentTypeConstants.igAssemblyDocument And BT_Update.Checked Then objDoc.UpdateDocument 'objDoc.Parent.StartCommand(11292)

    End Sub

    Private Sub SetupAnchors()

        FLP_Vars.SuspendLayout()

        If FLP_Vars.Controls.Count > 0 Then
            For i = 0 To FLP_Vars.Controls.Count - 1
                Dim c As Control = FLP_Vars.Controls(i)
                If i = 0 Then
                    ' Its the first control, all subsequent controls follow
                    ' the anchor behavior of this control.
                    c.Width = FLP_Vars.Width - 6
                    c.Anchor = AnchorStyles.Left + AnchorStyles.Top

                    If FLP_Vars.VerticalScroll.Visible Then c.Width += -SystemInformation.VerticalScrollBarWidth + 0

                Else

                    ' It is not the first control. Set its anchor to
                    ' copy the width of the first control in the list.
                    c.Anchor = AnchorStyles.Left + AnchorStyles.Right

                End If
            Next
        End If

        FLP_Vars.ResumeLayout()

    End Sub

    Private Sub Form_VarHandler_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        SetupAnchors()
    End Sub

    Private Sub Form_VarHandler_Load(sender As Object, e As EventArgs) Handles Me.Load

        '################# Questo risolver il problema del bordo sgrazinato della ToolStrip
        ToolStrip1.Renderer = New MySR()
        '################# rif: https://stackoverflow.com/questions/1918247/how-to-disable-the-line-under-tool-strip-in-winform-c

        Try
            objApp = GetObject(, "SolidEdge.Application")
        Catch ex As Exception
            MsgBox("Solid Edge must be open", MsgBoxStyle.Critical)
            End
        End Try

        Try
            objDoc = objApp.ActiveDocument
            LengthUnits = GetLengthUnits(objDoc)
        Catch ex As Exception
            MsgBox("A Solid Edge Document must be open", MsgBoxStyle.Critical)
            End
        End Try

        Autotune()

    End Sub

    Private Sub Autotune()

        Dim tmpVars As SolidEdgeFramework.Variables = objDoc.Variables

        Dim tmpVar = tmpVars.Query("*",, 2)

        Dim variables As SolidEdgeFramework.Variables = CType(objDoc.Variables, SolidEdgeFramework.Variables)


        Dim pFindCriterium As String = "*"
        Dim NamedBy As Object = SolidEdgeConstants.VariableNameBy.seVariableNameByBoth
        Dim CaseInsensitive As Object = False
        Dim VarType As Object = SolidEdgeConstants.VariableVarType.SeVariableVarTypeBoth
        Dim variableList As SolidEdgeFramework.VariableList = CType(variables.Query(pFindCriterium, NamedBy, VarType, CaseInsensitive), SolidEdgeFramework.VariableList)

        If variableList.Count > 0 Then

            Dim tmpList As New List(Of Object) 'variable)

            For Each item In variableList

                Try
                    Console.WriteLine(item.Name & " - " & item.GetComment)
                    If item.GetComment = "Autotune" Then

                        tmpList.Add(item)

                    End If
                Catch ex As Exception
                    Console.WriteLine(item.Name)
                End Try

            Next

            For Each item In tmpList

                Dim tmpSlider As New UC_Slider(item)
                AddHandler tmpSlider.LB_value.TextChanged, AddressOf Slider_Click

                FLP_Vars.Controls.Add(tmpSlider)

            Next

            SetupAnchors()

        End If

        BT_Tracker_Click(Me, Nothing)

    End Sub

    Private Sub FLP_Vars_ControlRemoved(sender As Object, e As ControlEventArgs) Handles FLP_Vars.ControlRemoved
        SetupAnchors()
    End Sub

    Private Sub BT_Reload_Click(sender As Object, e As EventArgs) Handles BT_Reload.Click

        FLP_Vars.Controls.Clear()
        Autotune()

    End Sub

    Private Sub BT_Tracker_Click(sender As Object, e As EventArgs) Handles BT_Tracker.Click

        'MsgBox("Not implemented yet! :)", MsgBoxStyle.Information)

        If BT_Tracker.Checked Then

            Select Case objDoc.Type
                Case = DocumentTypeConstants.igDraftDocument
                    For Each item In objDoc.ActiveSheet.BlockOccurrences
                        If item.Block.Name = "Tracker" Then
                            Tracker_2D = item
                        End If
                    Next
                    If IsNothing(Tracker_2D) Then
                        MsgBox("2D Tracker not found!", MsgBoxStyle.Information)
                        BT_Tracker.Checked = False
                        Exit Sub
                    End If
                Case Else
                    For Each item In objDoc.CoordinateSystems
                        If item.name = "Tracker" Then
                            Tracker_3D = item
                        End If
                    Next
                    If IsNothing(Tracker_3D) Then
                        MsgBox("3D Tracker not found!", MsgBoxStyle.Information)
                        BT_Tracker.Checked = False
                        Exit Sub
                    End If
            End Select

            SetupTracker()

        Else

            Trace = False

            For Each item In FLP_Vars.Controls

                If TypeOf item Is UC_Tracker Then FLP_Vars.Controls.Remove(item)

            Next

        End If

        Tracking = BT_Tracker.Checked

    End Sub

    Private Sub SetupTracker()

        Dim tmpTracker As New UC_Tracker("Tracker", LengthUnits)
        If objDoc.Type = DocumentTypeConstants.igDraftDocument Then tmpTracker.Tracker_3D = False

        FLP_Vars.Controls.Add(tmpTracker)
        tmpTracker.UpdateLabel()

        SetupAnchors()

    End Sub

    Private Sub BT_Workflow_Click(sender As Object, e As EventArgs) Handles BT_Workflow.Click

        'MsgBox("Not implemented yet! :)", MsgBoxStyle.Information)

        Dim tmpVariables = New List(Of Object)

        For Each item As UC_Slider In FLP_Vars.Controls.OfType(Of UC_Slider)

            If Not IsNothing(item) Then tmpVariables.Add(item.objVar)

        Next

        Dim tmpWorkFlow As New Form_WorkFlow
        tmpWorkFlow.Variables = tmpVariables
        tmpWorkFlow.UpdateDoc = BT_Update.Checked
        tmpWorkFlow.LengthUnits = LengthUnits

        tmpWorkFlow.ShowDialog(Me)

    End Sub

    Private Sub BT_Update_CheckedChanged(sender As Object, e As EventArgs) Handles BT_Update.CheckedChanged

        For Each tmpSlider In FLP_Vars.Controls

            If TypeOf tmpSlider Is UC_Slider Then tmpSlider.UpdateDoc = BT_Update.Checked

        Next

    End Sub

    Private Function GetLengthUnits(objDoc As SolidEdgeFramework.SolidEdgeDocument) As SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants
        Dim tmpLengthUnits As SolidEdgeConstants.UnitOfMeasureLengthReadoutConstants

        Dim UnitsOfMeasure = objDoc.UnitsOfMeasure

        For Each UnitOfMeasure As SolidEdgeFramework.UnitOfMeasure In UnitsOfMeasure
            If UnitOfMeasure.Type = SolidEdgeConstants.UnitTypeConstants.igUnitDistance Then
                tmpLengthUnits = UnitOfMeasure.Units
                Exit For
            End If
        Next

        Return tmpLengthUnits
    End Function

End Class

Public Class MySR
    Inherits ToolStripSystemRenderer

    Public Sub New()
    End Sub

    Protected Overrides Sub OnRenderToolStripBorder(ByVal e As ToolStripRenderEventArgs)
    End Sub
End Class
