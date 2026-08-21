<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_WorkFlow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
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
        Me.BT_Skip = New System.Windows.Forms.ToolStripButton()
        Me.BT_Step = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.LabelStatus = New System.Windows.Forms.Label()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'FLP_Events
        '
        Me.FLP_Events.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FLP_Events.AutoScroll = True
        Me.FLP_Events.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FLP_Events.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FLP_Events.Location = New System.Drawing.Point(0, 0)
        Me.FLP_Events.Name = "FLP_Events"
        Me.FLP_Events.Size = New System.Drawing.Size(291, 405)
        Me.FLP_Events.TabIndex = 3
        Me.FLP_Events.WrapContents = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BT_Reload, Me.ToolStripSeparator1, Me.BT_Open, Me.BT_Save, Me.BT_Close, Me.ToolStripSeparator2, Me.Add_Event, Me.ToolStripSeparator3, Me.BT_Play, Me.BT_Skip, Me.BT_Step})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(293, 25)
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
        Me.Add_Event.Size = New System.Drawing.Size(49, 22)
        Me.Add_Event.Text = "Add"
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
        'BT_Skip
        '
        Me.BT_Skip.Image = Global.SolidEdge_VarHandler.My.Resources.Resources.skip
        Me.BT_Skip.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_Skip.Name = "BT_Skip"
        Me.BT_Skip.Size = New System.Drawing.Size(49, 22)
        Me.BT_Skip.Text = "Skip"
        Me.BT_Skip.ToolTipText = "Skip"
        '
        'BT_Step
        '
        Me.BT_Step.Image = Global.SolidEdge_VarHandler.My.Resources.Resources._step
        Me.BT_Step.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BT_Step.Name = "BT_Step"
        Me.BT_Step.Size = New System.Drawing.Size(50, 20)
        Me.BT_Step.Text = "Step"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.FLP_Events)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 25)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(293, 436)
        Me.Panel1.TabIndex = 4
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.LabelStatus)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 431)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(293, 30)
        Me.Panel2.TabIndex = 5
        '
        'LabelStatus
        '
        Me.LabelStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelStatus.AutoSize = True
        Me.LabelStatus.Location = New System.Drawing.Point(3, 8)
        Me.LabelStatus.Name = "LabelStatus"
        Me.LabelStatus.Size = New System.Drawing.Size(16, 13)
        Me.LabelStatus.TabIndex = 0
        Me.LabelStatus.Text = "   "
        '
        'Form_WorkFlow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(293, 461)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(300, 250)
        Me.Name = "Form_WorkFlow"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "WorkFlow"
        Me.TopMost = True
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
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
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents LabelStatus As Label
    Friend WithEvents BT_Step As ToolStripButton
    Friend WithEvents BT_Skip As ToolStripButton
End Class
