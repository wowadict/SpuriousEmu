Imports System
Imports Microsoft.VisualBasic
Imports System.Collections

Namespace PacketLogger
    Public Class ScriptedPacketHandler
        Inherits PacketLogger.BasePacketHandler

        Dim log As PacketLogger.BaseFileLogger = Nothing

        Public Overrides Sub OnRecv(ByRef Packet As PacketLogger.PacketClass)
            Try

                If log Is Nothing Then
                    log = New PacketLogger.BaseFileLogger("SMSG_UPDATE_OBJECT")
                End If

                log.LogPacket(packet, 0)
                ParceUpdateObject(Packet)


            Catch e As Exception
                Console.WriteLine(e.ToString)
            End Try
        End Sub





















        Enum updateType As Byte
            UPDATETYPE_VALUES = 0
            UPDATETYPE_MOVEMENT = 1
            UPDATETYPE_CREATE_OBJECT = 2
            UPDATETYPE_CREATE_OBJECT_SELF = 3
            UPDATETYPE_OUT_OF_RANGE_OBJECTS = 4
            UPDATETYPE_NEAR_OBJECTS = 5
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
                            log.Log(String.Format("{1}:{0} [GUID={2:X}]", updateType, j, ReadPacketGUID(Packet)))
                            ReadValuesUpdate(Packet)

                        Case updateType.UPDATETYPE_MOVEMENT
                            log.Log(String.Format("{1}:{0} [GUID={2:X}]", updateType, j, ReadPacketGUID(Packet)))
                            ReadMovementUpdate(Packet)

                        Case updateType.UPDATETYPE_OUT_OF_RANGE_OBJECTS, updateType.UPDATETYPE_NEAR_OBJECTS
                            log.Log(String.Format("{1}:{0}]", updateType, j))

                            Dim GUIDsCount As Integer = Packet.GetInt32
                            Dim result As String = vbTab & " Reading " & GUIDsCount & " GUIDs: " & vbNewLine & vbTab
                            Dim i As Integer
                            For i = 1 To GUIDsCount
                                result &= String.Format("{0:X} ", ReadPacketGUID(Packet))
                            Next
                            log.Log(result)

                        Case updateType.UPDATETYPE_CREATE_OBJECT, updateType.UPDATETYPE_CREATE_OBJECT_SELF
                            log.Log(String.Format("{1}:{0} [GUID={2:X}]", updateType, j, ReadPacketGUID(Packet)))

                            Dim objectType As ObjectTypeID = Packet.GetInt8
                            log.Log(String.Format(" ObjectType: {0}", objectType))

                            ReadMovementUpdate(Packet)
                            ReadValuesUpdate(Packet)
                        Case Else
                            log.Log("ERROR: Unknown updateType " & updateType)
                    End Select
                Next

                log.Log("END")

            Catch e As Exception
                Console.WriteLine(e.ToString)
                log.Log("ERROR: " & e.ToString)
            End Try
        End Sub

        Public Sub ReadMovementUpdate(ByRef Packet As PacketLogger.PacketClass)
            Dim updateFlag As Byte = Packet.GetInt8
            Dim movementFlags As Integer = 0
            log.Log(String.Format(" Reading Movement Update, flags: 0x{0:X}", updateFlag))

            If ((updateFlag And &H20) = &H20) Then
                movementFlags = Packet.GetInt32()
                Dim unk0 As Integer = Packet.GetInt32
                log.Log(String.Format("  0x20:MovementFlags1: 0x{0:X} 0x{1:X}", movementFlags, unk0))
            End If

            If ((updateFlag And &H40) = &H40) Then
                Dim x As Single = Packet.GetFloat
                Dim y As Single = Packet.GetFloat
                Dim z As Single = Packet.GetFloat
                Dim o As Single = Packet.GetFloat
                log.Log(String.Format("  0x40:PositionInfo: x={0} y={1} z={2} o={3}", x, y, z, o))

                If ((movementFlags And &H2000000) = &H2000000) Then
                    Dim TransportGUID As Long = Packet.GetInt64
                    x = Packet.GetFloat
                    y = Packet.GetFloat
                    z = Packet.GetFloat
                    o = Packet.GetFloat
                    log.Log(String.Format("   0x2000000:mfTransportInfo: x={0} y={1} z={2} o={3} TransportGUID={4:X}", x, y, z, o, TransportGUID))
                End If
            End If

            If ((updateFlag And &H20) = &H20) Then
                Dim unk0 As Integer = Packet.GetInt32
                log.Log(String.Format("  0x20:MovementFlags2: 0x{0:X}", unk0))
            End If

            If ((movementFlags And &H2000) = &H2000) Then
                Dim unk0 As Single = Packet.GetFloat
                Dim unk1 As Single = Packet.GetFloat
                Dim unk2 As Single = Packet.GetFloat
                Dim unk3 As Single = Packet.GetFloat
                log.Log(String.Format("   0x00002000:mfPlayerCharacter: {0} {1} {2} {3}", unk0, unk1, unk2, unk3))
            End If

            If ((updateFlag And &H20) = &H20) Then
                Dim WalkSpeed As Single = Packet.GetFloat
                Dim RunningSpeed As Single = Packet.GetFloat
                Dim SwimSpeed As Single = Packet.GetFloat
                Dim SwimBackSpeed As Single = Packet.GetFloat
                Dim RunBackSpeed As Single = Packet.GetFloat
                Dim FlySpeed As Single = Packet.GetFloat
                Dim FlyBackSpeed As Single = Packet.GetFloat
                Dim TurnRate As Single = Packet.GetFloat
                log.Log(String.Format("  0x20:SpeedInformation: ws:{0} rs:{1} ss:{3} sbs:{4} rbs:{2} fs:{6} fbs:{7} tr:{5}", WalkSpeed, RunningSpeed, RunBackSpeed, SwimSpeed, SwimBackSpeed, TurnRate, FlySpeed, FlyBackSpeed))
            End If

            If ((movementFlags And &H8000000) = &H8000000) Then
                Dim unk0 As Integer = Packet.GetInt32
                Dim unk1 As Integer = Packet.GetInt32
                Dim unk2 As Integer = Packet.GetInt32
                Dim unk3 As Integer = Packet.GetInt32
                Dim posCount As Integer = Packet.GetInt32
                log.Log(String.Format("   0x08000000:mfUnitCharacter: {0} {1} {2} {3}, count = {4}", unk0, unk1, unk2, unk3, posCount))

                For i As Integer = 0 To posCount
                    Dim posX As Single = Packet.GetFloat
                    Dim posY As Single = Packet.GetFloat
                    Dim posZ As Single = Packet.GetFloat
                    log.Log(String.Format("    {0}:mfUnkCoordinates: {1} {2} {3}", i, posX, posY, posZ))
                Next
            End If

            If ((updateFlag And &H10) = &H10) Then
                Dim guid As Integer = Packet.GetInt32
                log.Log(String.Format("  0x10:ObjectGUIDLow: 0x{0:X}", guid))
            End If

            If ((updateFlag And &H8) = &H8) Then
                Dim guid As Integer = Packet.GetInt32
                log.Log(String.Format("  0x08:ObjectGUIDHigh: 0x{0:X}", guid))
            End If

            If ((updateFlag And &H4) = &H4) Then
                Dim unk0 As Integer = Packet.GetInt32
                log.Log(String.Format("  0x02:UnknownFlags0: 0x{0:X}", unk0))
            End If

            If ((updateFlag And &H2) = &H2) Then
                Dim unk0 As Integer = Packet.GetInt32
                log.Log(String.Format("  0x02:UnknownFlags1: 0x{0:X}", unk0))
            End If

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
