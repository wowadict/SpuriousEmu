Option Strict Off
Option Explicit On
Friend Class frmCoords
	Inherits System.Windows.Forms.Form
	Private Sub frmCoords_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		Me.Hide()
	End Sub
	
	Private Function GetSingle(ByRef sText As String) As Single
		Dim tmpTxt As String
		tmpTxt = sText
		tmpTxt = Replace(tmpTxt, ".", ",")
		GetSingle = CSng(tmpTxt)
	End Function
	
	Private Sub ChangeCoord()
		On Error GoTo ErrorChange
		Dim MapID As Integer
		Dim X, PosX, PosY, Y As Single
		
		MapID = frmMain.CurrentMap
		PosX = GetSingle((txtX.Text))
		PosY = GetSingle((txtY.Text))
		
		frmMain.CalculateMapCoords(PosX, PosY, X, Y)
		
		frmMain.shpLocation.Visible = True
		frmMain.shpLocation.Left = VB6.TwipsToPixelsX(CInt(X) - (VB6.PixelsToTwipsX(frmMain.shpLocation.Width) / 2))
		frmMain.shpLocation.Top = VB6.TwipsToPixelsY(CInt(Y) - (VB6.PixelsToTwipsY(frmMain.shpLocation.Height) / 2))
ErrorChange: 
	End Sub
	
	Private Sub txtX_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtX.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Call ChangeCoord()
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	Private Sub txtY_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtY.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Call ChangeCoord()
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub

    Private Sub frmCoords_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.TopMost = True
    End Sub
End Class