Imports System
Imports Microsoft.VisualBasic
Imports System.Collections

Namespace PacketLogger
	Public Class ScriptedPacketHandler
		Inherits PacketLogger.BasePacketHandler

		Dim log as PacketLogger.BaseFileLogger = Nothing

		Public Overrides Sub OnRecv(ByRef Packet As PacketLogger.PacketClass)
			Try

				if log is nothing then
					log = New PacketLogger.BaseFileLogger("SMSG_UPDATE_OBJECT")
				end if

				log.LogPacket(packet, 0)
                ParceUpdateObject(Packet)


			Catch e as Exception
				Console.WriteLine(e.ToString)
			end try
		End Sub
		

		


















        Enum updateType As Byte
            UPDATETYPE_VALUES = 0
            UPDATETYPE_MOVEMENT = 1
            UPDATETYPE_CREATE_OBJECT = 2
            UPDATETYPE_CREATE_OBJECT_SELF = 2
            UPDATETYPE_OUT_OF_RANGE_OBJECTS = 3
            UPDATETYPE_NEAR_OBJECTS = 4
        End Enum
        Enum ObjectTypeID
            TYPEID_OBJECT = 0
            TYPEID_ITEM = 1
            TYPEID_CONTAINER = 2
            TYPEID_UNIT = 3
            TYPEID_PLAYER = 4
            TYPEID_GAMEOBJECT = 5
            TYPEID_DYNAMICOBJECT = 6
            TYPEID_CORPSE = 7
            TYPEID_AIGROUP = 8
            TYPEID_AREATRIGGER = 9
        End Enum

        Public Sub ParceUpdateObject(ByRef Packet As PacketLogger.PacketClass)
            'NOTE: This is cut-down packet witout OPCODE and LENGTH headers
            Try

                Dim count As Integer = Packet.GetInt32()
                Dim unknown1 As Byte = Packet.GetInt8
                log.Log(String.Format("UpdatePacket: count = {0} unk1 = {1} ", count, unknown1))

                Dim j As Integer
                For j = 1 To count

                    Dim updateType As updateType = Packet.GetInt8

                    Select Case updateType
                        Case updateType.UPDATETYPE_VALUES
                            log.Log(String.Format("{1}:{0} [GUID={2}]", updateType, j, Packet.GetInt64))
                            ReadValuesUpdate(Packet)

                        Case updateType.UPDATETYPE_MOVEMENT
                            log.Log(String.Format("{1}:{0} [GUID={2}]", updateType, j, Packet.GetInt64))
                            ReadMovementUpdate(Packet)

                        Case updateType.UPDATETYPE_OUT_OF_RANGE_OBJECTS, updateType.UPDATETYPE_NEAR_OBJECTS
                            log.Log(String.Format("{1}:{0}", updateType, j))

                            Dim GUIDsCount As Integer = Packet.GetInt32
                            Dim result As String = vbTab & " Reading " & GUIDsCount & " GUIDs: " & vbNewLine & vbTab
                            Dim i As Integer
                            For i = 1 To GUIDsCount
                                result &= Packet.GetInt64 & " "
                            Next

                        Case updateType.UPDATETYPE_CREATE_OBJECT, updateType.UPDATETYPE_CREATE_OBJECT_SELF
                            Dim GUID As Long = Packet.GetInt64
                            log.Log(String.Format("{1}:{0} [GUID={2}]", updateType, j, GUID))

                            Dim objectType As ObjectTypeID = Packet.GetInt8
                            log.Log(String.Format(" ObjectType: {0}", objectType))

                            ReadMovementUpdate(Packet)
                            ReadValuesUpdate(Packet)
                        Case Else
                            log.Log("ERROR: Unknown updateType " & updateType)
                    End Select
                Next

            Catch e As Exception
                Console.WriteLine(e.ToString)
                log.LogPacket(Packet, 0)
            End Try
        End Sub

        Public Sub ReadMovementUpdate(ByRef Packet As PacketLogger.PacketClass)
            log.Log(" Reading Movement Update")

            Dim movementFlags As Integer = Packet.GetInt32()
            Dim unk0 As Integer = Packet.GetInt32
            log.Log(String.Format("  0x20:MovementFlags1: {0} {1}", movementFlags, unk0))


                Dim x As Single = Packet.GetFloat
                Dim y As Single = Packet.GetFloat
                Dim z As Single = Packet.GetFloat
                Dim o As Single = Packet.GetFloat
                log.Log(String.Format("  0x40:PositionInfo: x={0} y={1} z={2} o={3}", x, y, z, o))



                unk0 = Packet.GetInt32
                log.Log(String.Format("  0x20:MovementFlags2: {0}", unk0))


                Dim WalkSpeed As Single = Packet.GetFloat
                Dim RunningSpeed As Single = Packet.GetFloat
                Dim RunBackSpeed As Single = Packet.GetFloat
                Dim SwimSpeed As Single = Packet.GetFloat
                Dim SwimBackSpeed As Single = Packet.GetFloat
                Dim TurnRate As Single = Packet.GetFloat
                log.Log(String.Format("  0x20:SpeedInformation: ws:{0} rs:{1} rbs:{2} ss:{3} sbs:{4} tr:{5}", WalkSpeed, RunningSpeed, RunBackSpeed, SwimSpeed, SwimBackSpeed, TurnRate))
 


                unk0 = Packet.GetInt32
                log.Log(String.Format("  0x10:UnknownFlags1: {0}", unk0))



                unk0 = Packet.GetInt32
				Dim unk1 As Integer = Packet.GetInt32
				Dim unk2 As Integer = Packet.GetInt32
				Dim unk3 As Integer = Packet.GetInt32
                log.Log(String.Format("  0x02:UnknownFlags1: {0} {1} {2} {3}", unk0, unk1 ,unk2 ,unk3))


        End Sub
        Public Sub ReadValuesUpdate(ByRef Packet As PacketLogger.PacketClass)
            Dim integersCount As Byte = Packet.GetInt8

            'DONE: Read mask into byte array
            Dim buffer(integersCount * 4 - 1) As Byte
            Dim i As Integer
            For i = 0 To integersCount * 4 - 1
                buffer(i) = Packet.GetInt8
            Next

            'DONE: Create bitmask
            Dim mask As BitArray = New BitArray(buffer)
            log.Log(String.Format(" Listing {0} values contained in {1} bytes mask:", mask.Count, integersCount * 4))


            'DONE: Now read values
            For i = 0 To mask.Count - 1
                If mask.Get(i) Then
                    'TODO: Log and determine value type
                    Dim tmp As Integer = Packet.GetInt32
                    log.Log(String.Format("  UpdateField {0}    = {1}   = {2}", i, tmp, BitConverter.ToSingle(BitConverter.GetBytes(tmp), 0)))
                End If
            Next
        End Sub
        Public Function ReadPacketGUID(ByRef packet As PacketLogger.PacketClass) As Long
            Dim flags As Byte = packet.GetInt8
            Dim GUID() As Byte = {0, 0, 0, 0, 0, 0, 0, 0}

            If (flags And 1) = 1 Then GUID(0) = packet.GetInt8
            If (flags And 2) = 2 Then GUID(1) = packet.GetInt8
            If (flags And 4) = 4 Then GUID(2) = packet.GetInt8
            If (flags And 8) = 8 Then GUID(3) = packet.GetInt8
            If (flags And 16) = 16 Then GUID(4) = packet.GetInt8
            If (flags And 32) = 32 Then GUID(5) = packet.GetInt8
            If (flags And 64) = 64 Then GUID(6) = packet.GetInt8
            If (flags And 128) = 128 Then GUID(7) = packet.GetInt8

            Return CType(BitConverter.ToInt64(GUID, 0), Long)
        End Function

	End Class
End Namespace
