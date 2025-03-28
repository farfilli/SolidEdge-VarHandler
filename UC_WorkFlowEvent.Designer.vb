﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UC_WorkFlowEvent
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TLP = New System.Windows.Forms.TableLayoutPanel()
        Me.DG_Variables = New System.Windows.Forms.DataGridView()
        Me.Check = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.VarName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VarValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BT_Close = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.LB_SEQ = New System.Windows.Forms.Label()
        Me.BT_Steps = New System.Windows.Forms.Button()
        Me.TLP.SuspendLayout()
        CType(Me.DG_Variables, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TLP
        '
        Me.TLP.ColumnCount = 2
        Me.TLP.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLP.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLP.Controls.Add(Me.DG_Variables, 1, 0)
        Me.TLP.Controls.Add(Me.BT_Close, 0, 0)
        Me.TLP.Controls.Add(Me.TableLayoutPanel1, 0, 1)
        Me.TLP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLP.Location = New System.Drawing.Point(0, 0)
        Me.TLP.Name = "TLP"
        Me.TLP.RowCount = 2
        Me.TLP.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLP.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLP.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLP.Size = New System.Drawing.Size(281, 148)
        Me.TLP.TabIndex = 0
        '
        'DG_Variables
        '
        Me.DG_Variables.AllowUserToAddRows = False
        Me.DG_Variables.AllowUserToDeleteRows = False
        Me.DG_Variables.AllowUserToResizeRows = False
        Me.DG_Variables.BackgroundColor = System.Drawing.SystemColors.ControlDark
        Me.DG_Variables.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DG_Variables.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.DG_Variables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DG_Variables.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Check, Me.VarName, Me.VarValue})
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Variables.DefaultCellStyle = DataGridViewCellStyle1
        Me.DG_Variables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DG_Variables.GridColor = System.Drawing.SystemColors.Control
        Me.DG_Variables.Location = New System.Drawing.Point(30, 0)
        Me.DG_Variables.Margin = New System.Windows.Forms.Padding(0)
        Me.DG_Variables.MultiSelect = False
        Me.DG_Variables.Name = "DG_Variables"
        Me.DG_Variables.RowHeadersVisible = False
        Me.TLP.SetRowSpan(Me.DG_Variables, 2)
        Me.DG_Variables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DG_Variables.Size = New System.Drawing.Size(251, 148)
        Me.DG_Variables.TabIndex = 0
        '
        'Check
        '
        Me.Check.DataPropertyName = "Check"
        Me.Check.Frozen = True
        Me.Check.HeaderText = "C"
        Me.Check.Name = "Check"
        Me.Check.Width = 25
        '
        'VarName
        '
        Me.VarName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.VarName.DataPropertyName = "Name"
        Me.VarName.HeaderText = "Name"
        Me.VarName.Name = "VarName"
        '
        'VarValue
        '
        Me.VarValue.DataPropertyName = "Value"
        Me.VarValue.HeaderText = "Value"
        Me.VarValue.Name = "VarValue"
        Me.VarValue.Width = 50
        '
        'BT_Close
        '
        Me.BT_Close.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BT_Close.FlatAppearance.BorderSize = 0
        Me.BT_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BT_Close.Image = Global.SolidEdge_VarHandler.My.Resources.Resources.Close
        Me.BT_Close.Location = New System.Drawing.Point(0, 0)
        Me.BT_Close.Margin = New System.Windows.Forms.Padding(0)
        Me.BT_Close.Name = "BT_Close"
        Me.BT_Close.Size = New System.Drawing.Size(30, 20)
        Me.BT_Close.TabIndex = 2
        Me.BT_Close.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.LB_SEQ, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.BT_Steps, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 20)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(30, 128)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'LB_SEQ
        '
        Me.LB_SEQ.AutoSize = True
        Me.LB_SEQ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LB_SEQ.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LB_SEQ.ForeColor = System.Drawing.Color.DarkGray
        Me.LB_SEQ.Location = New System.Drawing.Point(3, 25)
        Me.LB_SEQ.Name = "LB_SEQ"
        Me.LB_SEQ.Size = New System.Drawing.Size(24, 103)
        Me.LB_SEQ.TabIndex = 1
        Me.LB_SEQ.Text = "1"
        Me.LB_SEQ.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'BT_Steps
        '
        Me.BT_Steps.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BT_Steps.Image = Global.SolidEdge_VarHandler.My.Resources.Resources.Settings
        Me.BT_Steps.Location = New System.Drawing.Point(3, 3)
        Me.BT_Steps.Name = "BT_Steps"
        Me.BT_Steps.Size = New System.Drawing.Size(24, 19)
        Me.BT_Steps.TabIndex = 2
        Me.BT_Steps.UseVisualStyleBackColor = True
        '
        'UC_WorkFlowEvent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.TLP)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Name = "UC_WorkFlowEvent"
        Me.Size = New System.Drawing.Size(281, 148)
        Me.TLP.ResumeLayout(False)
        CType(Me.DG_Variables, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TLP As TableLayoutPanel
    Friend WithEvents DG_Variables As DataGridView
    Friend WithEvents LB_SEQ As Label
    Friend WithEvents Check As DataGridViewCheckBoxColumn
    Friend WithEvents VarName As DataGridViewTextBoxColumn
    Friend WithEvents VarValue As DataGridViewTextBoxColumn
    Friend WithEvents BT_Close As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents BT_Steps As Button
End Class
