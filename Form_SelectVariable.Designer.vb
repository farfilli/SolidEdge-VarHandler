﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_SelectVariable
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_SelectVariable))
        Me.ListBox_Variables = New System.Windows.Forms.ListBox()
        Me.BT_Cancel = New System.Windows.Forms.Button()
        Me.BT_OK = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.CB_Variables = New System.Windows.Forms.CheckBox()
        Me.CB_Dimensions = New System.Windows.Forms.CheckBox()
        Me.CB_Users = New System.Windows.Forms.CheckBox()
        Me.CB_System = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListBox_Variables
        '
        Me.ListBox_Variables.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TableLayoutPanel1.SetColumnSpan(Me.ListBox_Variables, 2)
        Me.ListBox_Variables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox_Variables.FormattingEnabled = True
        Me.ListBox_Variables.Location = New System.Drawing.Point(0, 50)
        Me.ListBox_Variables.Margin = New System.Windows.Forms.Padding(0)
        Me.ListBox_Variables.Name = "ListBox_Variables"
        Me.ListBox_Variables.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.ListBox_Variables.Size = New System.Drawing.Size(264, 161)
        Me.ListBox_Variables.Sorted = True
        Me.ListBox_Variables.TabIndex = 0
        '
        'BT_Cancel
        '
        Me.BT_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BT_Cancel.FlatAppearance.BorderSize = 0
        Me.BT_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BT_Cancel.Image = Global.SolidEdge_VarHandler.My.Resources.Resources.Cancel
        Me.BT_Cancel.Location = New System.Drawing.Point(132, 211)
        Me.BT_Cancel.Margin = New System.Windows.Forms.Padding(0)
        Me.BT_Cancel.Name = "BT_Cancel"
        Me.BT_Cancel.Size = New System.Drawing.Size(132, 30)
        Me.BT_Cancel.TabIndex = 1
        Me.BT_Cancel.UseVisualStyleBackColor = True
        '
        'BT_OK
        '
        Me.BT_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BT_OK.FlatAppearance.BorderSize = 0
        Me.BT_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BT_OK.Image = Global.SolidEdge_VarHandler.My.Resources.Resources.Ok
        Me.BT_OK.Location = New System.Drawing.Point(0, 211)
        Me.BT_OK.Margin = New System.Windows.Forms.Padding(0)
        Me.BT_OK.Name = "BT_OK"
        Me.BT_OK.Size = New System.Drawing.Size(132, 30)
        Me.BT_OK.TabIndex = 1
        Me.BT_OK.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.BT_OK, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.ListBox_Variables, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.BT_Cancel, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.CB_Variables, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CB_Dimensions, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CB_Users, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CB_System, 1, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(264, 241)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'CB_Variables
        '
        Me.CB_Variables.AutoSize = True
        Me.CB_Variables.Checked = True
        Me.CB_Variables.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CB_Variables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CB_Variables.Location = New System.Drawing.Point(3, 3)
        Me.CB_Variables.Name = "CB_Variables"
        Me.CB_Variables.Size = New System.Drawing.Size(126, 19)
        Me.CB_Variables.TabIndex = 2
        Me.CB_Variables.Text = "Variables"
        Me.CB_Variables.UseVisualStyleBackColor = True
        '
        'CB_Dimensions
        '
        Me.CB_Dimensions.AutoSize = True
        Me.CB_Dimensions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CB_Dimensions.Location = New System.Drawing.Point(3, 28)
        Me.CB_Dimensions.Name = "CB_Dimensions"
        Me.CB_Dimensions.Size = New System.Drawing.Size(126, 19)
        Me.CB_Dimensions.TabIndex = 2
        Me.CB_Dimensions.Text = "Dimensions"
        Me.CB_Dimensions.UseVisualStyleBackColor = True
        '
        'CB_Users
        '
        Me.CB_Users.AutoSize = True
        Me.CB_Users.Checked = True
        Me.CB_Users.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CB_Users.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CB_Users.Location = New System.Drawing.Point(135, 3)
        Me.CB_Users.Name = "CB_Users"
        Me.CB_Users.Size = New System.Drawing.Size(126, 19)
        Me.CB_Users.TabIndex = 2
        Me.CB_Users.Text = "User"
        Me.CB_Users.UseVisualStyleBackColor = True
        '
        'CB_System
        '
        Me.CB_System.AutoSize = True
        Me.CB_System.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CB_System.Location = New System.Drawing.Point(135, 28)
        Me.CB_System.Name = "CB_System"
        Me.CB_System.Size = New System.Drawing.Size(126, 19)
        Me.CB_System.TabIndex = 2
        Me.CB_System.Text = "System"
        Me.CB_System.UseVisualStyleBackColor = True
        '
        'Form_SelectVariable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(264, 241)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(200, 280)
        Me.Name = "Form_SelectVariable"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Variables"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ListBox_Variables As ListBox
    Friend WithEvents BT_OK As Button
    Friend WithEvents BT_Cancel As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents CB_Variables As CheckBox
    Friend WithEvents CB_Dimensions As CheckBox
    Friend WithEvents CB_Users As CheckBox
    Friend WithEvents CB_System As CheckBox
End Class
