Imports System
Imports Microsoft.VisualBasic

Namespace PacketLogger
	Public Class ScriptedPacketHandler
		Inherits PacketLogger.BasePacketHandler

		Public Overrides Sub OnRecv(ByRef Packet As PacketLogger.PacketClass)
			Console.WriteLine("Scripting: Sample Packet Handler Invoked!")
		End Sub

	End Class
End Namespace
