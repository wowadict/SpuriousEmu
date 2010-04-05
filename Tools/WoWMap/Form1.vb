Option Strict Off
Option Explicit On
Friend Class frmMain
	Inherits System.Windows.Forms.Form
	Public CurrentMap As Integer
	
	Private Sub frmMain_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		CurrentMap = 0
	End Sub
	
	Private Sub frmMain_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		End
	End Sub
	
	Private Sub imgAzeroth_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles imgAzeroth.MouseDown
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim X As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		PressOnMap(X, Y)
	End Sub
	
	Private Sub imgOutlands_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles imgOutlands.MouseDown
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim X As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		PressOnMap(X, Y)
	End Sub
	
	Private Sub PressOnMap(ByRef X As Single, ByRef Y As Single)
		Dim PosX, PosY As Single
		
		shpLocation.Visible = True
		shpLocation.Left = VB6.TwipsToPixelsX(X - (VB6.PixelsToTwipsX(shpLocation.Width) / 2))
		shpLocation.Top = VB6.TwipsToPixelsY(Y - (VB6.PixelsToTwipsY(shpLocation.Height) / 2))
		
		CalculateCoords(X, Y, PosX, PosY)
		
		frmCoords.txtX.Text = CStr(PosX)
		frmCoords.txtY.Text = CStr(PosY)
	End Sub
	
	Public Sub mnuCoords_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuCoords.Click
		frmCoords.Show()
	End Sub
	
	Public Sub mnuEasternK_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEasternK.Click
		imgAzeroth.Visible = True
		imgOutlands.Visible = False
		CurrentMap = 0
	End Sub
	
	Public Sub mnuKalimdor_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuKalimdor.Click
		imgAzeroth.Visible = True
		imgOutlands.Visible = False
		CurrentMap = 1
	End Sub
	
	Public Sub mnuOutlands_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuOutlands.Click
		imgAzeroth.Visible = False
		imgOutlands.Visible = True
		CurrentMap = 530
	End Sub
	
    Public Sub CalculateCoords(ByRef X As Single, ByRef Y As Single, ByRef PosX As Single, ByRef PosY As Single)
        Select Case CurrentMap
            Case 0
                PosX = ((((174 * 15) - Y) / 15) / 17.7) * 1000
                PosY = ((((572 * 15) - X) / 15) / 17.7) * 1000
            Case 1
                PosX = ((((258 * 15) - Y) / 15) / 17.7) * 1000
                PosY = ((((154 * 15) - X) / 15) / 17.7) * 1000
            Case 530
                PosX = (((((271 * 15) - Y) / 15) / (800 / 784)) / 17.7) * 380
                PosY = (((((686 * 15) - X) / 15) / (533 / 525)) / 17.7) * 305
        End Select
    End Sub
	
    Public Sub CalculateMapCoords(ByRef X As Single, ByRef Y As Single, ByRef PosX As Single, ByRef PosY As Single)
        Select Case CurrentMap
            Case 0
                X = System.Math.Round((X / 1000) * 17.7, 0)
                Y = System.Math.Round((Y / 1000) * 17.7, 0)
                X = X * 15
                Y = Y * 15
                PosX = (572 * 15) - Y
                PosY = (174 * 15) - X
            Case 1
                X = System.Math.Round((X / 1000) * 17.7, 0)
                Y = System.Math.Round((Y / 1000) * 17.7, 0)
                X = X * 15
                Y = Y * 15
                PosX = (154 * 15) - Y
                PosY = (258 * 15) - X
            Case 530
                PosX = System.Math.Round((Y / 305) * 17.7, 0)
                PosY = System.Math.Round((X / 380) * 17.7, 0)
                PosX = PosX * (800 / 784)
                PosY = PosY * (533 / 525)
                X = PosX * 15
                Y = PosY * 15
                PosX = (686 * 15) - X
                PosY = (271 * 15) - Y
        End Select
    End Sub
End Class