﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_WorkFlow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_WorkFlow))
        Me.FLP_Events = New System.Windows.Forms.FlowLayoutPanel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.BT_Reload = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BT_Open = New System.Windows.Forms.ToolStripButton()
        Me.BT_Save = New System.Windows.Forms.ToolStripButton()
        Me.BT_Close = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.Add_Event = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.BT_Play = New System.Windows.Forms.ToolStripButton()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'FLP_Events
        '
        Me.FLP_Events.AutoScroll = True
        Me.FLP_Events.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FLP_Events.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FLP_Events.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FLP_Events.Location = New System.Drawing.Point(0, 25)
        Me.FLP_Events.Name = "FLP_Events"
        Me.FLP_Events.Size = New System.Drawing.Size(284, 436)
        Me.FLP_Events.TabIndex = 3
        Me.FLP_Events.WrapContents = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BT_Reload, Me.ToolStripSeparator1, Me.BT_Open, Me.BT_Save, Me.BT_Close, Me.ToolStripSeparator2, Me.Add_Event, Me.ToolStripSeparator3, Me.BT_Play})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(284, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'BT_Reload
        '
        Me.BT_Reload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_Reload.Image = Global.SolidEdge_VarHandler.My.Resources.Resources.Reload
        Me.BT_Reload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_Reload.Name = "BT_Reload"
        Me.BT_Reload.Size = New System.Drawing.Size(23, 22)
        Me.BT_Reload.Text = "Reload"
        Me.BT_Reload.Visible = False
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        Me.ToolStripSeparator1.Visible = False
        '
        'BT_Open
        '
        Me.BT_Open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_Open.Image = Global.SolidEdge_VarHandler.My.Resources.Resources.Open
        Me.BT_Open.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_Open.Name = "BT_Open"
        Me.BT_Open.Size = New System.Drawing.Size(23, 22)
        Me.BT_Open.Text = "Open"
        '
        'BT_Save
        '
        Me.BT_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_Save.Image = Global.SolidEdge_VarHandler.My.Resources.Resources.Save
        Me.BT_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_Save.Name = "BT_Save"
        Me.BT_Save.Size = New System.Drawing.Size(23, 22)
        Me.BT_Save.Text = "Save"
        '
        'BT_Close
        '
        Me.BT_Close.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BT_Close.Image = Global.SolidEdge_VarHandler.My.Resources.Resources.Close
        Me.BT_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_Close.Name = "BT_Close"
        Me.BT_Close.Size = New System.Drawing.Size(23, 22)
        Me.BT_Close.Text = "Close"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'Add_Event
        '
        Me.Add_Event.Image = Global.SolidEdge_VarHandler.My.Resources.Resources.Add
        Me.Add_Event.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Add_Event.Name = "Add_Event"
        Me.Add_Event.Size = New System.Drawing.Size(81, 22)
        Me.Add_Event.Text = "Add Event"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'BT_Play
        '
        Me.BT_Play.Image = Global.SolidEdge_VarHandler.My.Resources.Resources.Play
        Me.BT_Play.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_Play.Name = "BT_Play"
        Me.BT_Play.Size = New System.Drawing.Size(49, 22)
        Me.BT_Play.Text = "Play"
        '
        'Form_WorkFlow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 461)
        Me.Controls.Add(Me.FLP_Events)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(280, 400)
        Me.Name = "Form_WorkFlow"
        Me.Text = "WorkFlow"
        Me.TopMost = True
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents FLP_Events As FlowLayoutPanel
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents BT_Open As ToolStripButton
    Friend WithEvents BT_Reload As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents BT_Save As ToolStripButton
    Friend WithEvents BT_Close As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents Add_Event As ToolStripButton
    Friend WithEvents BT_Play As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
End Class
