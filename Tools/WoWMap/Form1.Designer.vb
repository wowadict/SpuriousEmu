<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmMain
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
	Public WithEvents shpLocation As System.Windows.Forms.Label
	Public WithEvents imgOutlands As System.Windows.Forms.PictureBox
	Public WithEvents imgAzeroth As System.Windows.Forms.PictureBox
	Public WithEvents mnuKalimdor As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuEasternK As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuOutlands As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuNorthrend As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuCoords As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents MainMenu1 As System.Windows.Forms.MenuStrip
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.shpLocation = New System.Windows.Forms.Label
        Me.imgOutlands = New System.Windows.Forms.PictureBox
        Me.imgAzeroth = New System.Windows.Forms.PictureBox
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuKalimdor = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuEasternK = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuOutlands = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuNorthrend = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuCoords = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.imgOutlands, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgAzeroth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainMenu1.SuspendLayout()
        Me.SuspendLayout()
        '
        'shpLocation
        '
        Me.shpLocation.BackColor = System.Drawing.Color.Red
        Me.shpLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.shpLocation.ForeColor = System.Drawing.Color.Black
        Me.shpLocation.Location = New System.Drawing.Point(168, 240)
        Me.shpLocation.Name = "shpLocation"
        Me.shpLocation.Size = New System.Drawing.Size(9, 9)
        Me.shpLocation.TabIndex = 0
        Me.shpLocation.Text = "shpLocation"
        Me.shpLocation.Visible = False
        '
        'imgOutlands
        '
        Me.imgOutlands.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgOutlands.Image = CType(resources.GetObject("imgOutlands.Image"), System.Drawing.Image)
        Me.imgOutlands.Location = New System.Drawing.Point(0, 0)
        Me.imgOutlands.Name = "imgOutlands"
        Me.imgOutlands.Size = New System.Drawing.Size(784, 525)
        Me.imgOutlands.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgOutlands.TabIndex = 1
        Me.imgOutlands.TabStop = False
        Me.imgOutlands.Visible = False
        '
        'imgAzeroth
        '
        Me.imgAzeroth.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgAzeroth.Image = CType(resources.GetObject("imgAzeroth.Image"), System.Drawing.Image)
        Me.imgAzeroth.Location = New System.Drawing.Point(0, 0)
        Me.imgAzeroth.Name = "imgAzeroth"
        Me.imgAzeroth.Size = New System.Drawing.Size(784, 525)
        Me.imgAzeroth.TabIndex = 2
        Me.imgAzeroth.TabStop = False
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuCoords})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(783, 24)
        Me.MainMenu1.TabIndex = 3
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuKalimdor, Me.mnuEasternK, Me.mnuOutlands, Me.mnuNorthrend})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuFile.Text = "File"
        '
        'mnuKalimdor
        '
        Me.mnuKalimdor.Name = "mnuKalimdor"
        Me.mnuKalimdor.Size = New System.Drawing.Size(169, 22)
        Me.mnuKalimdor.Text = "Kalimdor"
        '
        'mnuEasternK
        '
        Me.mnuEasternK.Name = "mnuEasternK"
        Me.mnuEasternK.Size = New System.Drawing.Size(169, 22)
        Me.mnuEasternK.Text = "Eastern Kingdoms"
        '
        'mnuOutlands
        '
        Me.mnuOutlands.Name = "mnuOutlands"
        Me.mnuOutlands.Size = New System.Drawing.Size(169, 22)
        Me.mnuOutlands.Text = "Outlands"
        '
        'mnuNorthrend
        '
        Me.mnuNorthrend.Enabled = False
        Me.mnuNorthrend.Name = "mnuNorthrend"
        Me.mnuNorthrend.Size = New System.Drawing.Size(169, 22)
        Me.mnuNorthrend.Text = "Northrend"
        '
        'mnuCoords
        '
        Me.mnuCoords.Name = "mnuCoords"
        Me.mnuCoords.Size = New System.Drawing.Size(57, 20)
        Me.mnuCoords.Text = "Coords"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(783, 524)
        Me.Controls.Add(Me.shpLocation)
        Me.Controls.Add(Me.MainMenu1)
        Me.Controls.Add(Me.imgOutlands)
        Me.Controls.Add(Me.imgAzeroth)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(11, 57)
        Me.Name = "frmMain"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "WoWMap by UniX"
        CType(Me.imgOutlands, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgAzeroth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region 
End Class