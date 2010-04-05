<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmCoords
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents txtY As System.Windows.Forms.TextBox
	Public WithEvents txtX As System.Windows.Forms.TextBox
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtY = New System.Windows.Forms.TextBox
        Me.txtX = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'txtY
        '
        Me.txtY.AcceptsReturn = True
        Me.txtY.BackColor = System.Drawing.SystemColors.Window
        Me.txtY.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtY.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtY.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtY.Location = New System.Drawing.Point(168, 8)
        Me.txtY.MaxLength = 0
        Me.txtY.Name = "txtY"
        Me.txtY.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtY.Size = New System.Drawing.Size(137, 19)
        Me.txtY.TabIndex = 3
        Me.txtY.Text = "0"
        '
        'txtX
        '
        Me.txtX.AcceptsReturn = True
        Me.txtX.BackColor = System.Drawing.SystemColors.Window
        Me.txtX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtX.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtX.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtX.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtX.Location = New System.Drawing.Point(24, 8)
        Me.txtX.MaxLength = 0
        Me.txtX.Name = "txtX"
        Me.txtX.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtX.Size = New System.Drawing.Size(113, 19)
        Me.txtX.TabIndex = 0
        Me.txtX.Text = "0"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(152, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(25, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Y:"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(25, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "X:"
        '
        'frmCoords
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(312, 37)
        Me.Controls.Add(Me.txtY)
        Me.Controls.Add(Me.txtX)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Location = New System.Drawing.Point(4, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCoords"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Coords"
        Me.ResumeLayout(False)

    End Sub
#End Region 
End Class