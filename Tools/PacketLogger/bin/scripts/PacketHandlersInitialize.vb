Imports System
Imports Microsoft.VisualBasic

Namespace PacketLogger
	Public Module ScriptedHandlers

		Public Sub Initialize()
			
			PacketLogger.OpcodeHandlers.AddPacketHandler("SamplePacketHandler.vb",-1)

			PacketLogger.OpcodeHandlers.AddPacketHandler("SMSG_UPDATE_OBJECT.vb",				169)
			PacketLogger.OpcodeHandlers.AddPacketHandler("SMSG_COMPRESSED_UPDATE_OBJECT.vb",	502)
		
			Console.WriteLine("[{0}] Scripting: Scripted packet handlers initialized.",Format(TimeOfDay, "hh:mm:ss"))
		End Sub

	End Module
End Namespace
