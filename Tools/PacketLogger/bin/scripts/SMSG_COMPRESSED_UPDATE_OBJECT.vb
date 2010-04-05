Imports System
Imports Microsoft.VisualBasic
Imports System.Collections

Namespace PacketLogger
    Public Class ScriptedPacketHandler
        Inherits PacketLogger.BasePacketHandler

        Dim log As PacketLogger.BaseFileLogger = Nothing
        Dim logPositions As PacketLogger.BaseFileLogger = Nothing

        Public Overrides Sub OnRecv(ByRef Packet As PacketLogger.PacketClass)
            Try
                Packet.Offset = 8
                Dim decompressBuffer(Packet.Data.Length - Packet.Offset) As Byte
                Array.Copy(Packet.Data, Packet.Offset, decompressBuffer, 0, Packet.Data.Length - Packet.Offset)
                Packet.Data = PacketLogger.DeCompress(decompressBuffer)
                Packet.Offset = 0

                If log Is Nothing Then
                    log = New PacketLogger.BaseFileLogger("SMSG_COMPRESSED_UPDATE_OBJECT")
                End If
                If logPositions Is Nothing Then
                    logPositions = New PacketLogger.BaseFileLogger("Spawns")
                End If

                log.LogPacket(Packet, 0)
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

        Public readAsCreate As Integer = -1
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
                            log.Log(String.Format("{1}:{0}", updateType, j))

                            Dim GUIDsCount As Integer = Packet.GetInt32
                            Dim result As String = vbTab & " Reading " & GUIDsCount & " GUIDs: " & vbNewLine & vbTab
                            Dim i As Integer
                            For i = 1 To GUIDsCount
                                result &= String.Format("{0:X} ", ReadPacketGUID(Packet))
                            Next
                            log.Log(result)

                        Case updateType.UPDATETYPE_CREATE_OBJECT, updateType.UPDATETYPE_CREATE_OBJECT_SELF
                            Dim GUID As Long = ReadPacketGUID(Packet)
                            log.Log(String.Format("{1}:{0} [GUID={2:X}]", updateType, j, GUID))

                            Dim objectType As ObjectTypeID = Packet.GetInt8
                            log.Log(String.Format(" ObjectType: {0}", objectType))
                            If objectType <> ObjectTypeID.TYPEID_PLAYER And objectType <> ObjectTypeID.TYPEID_ITEM Then
                                logPositions.Log(String.Format("[{0:X}]", GUID))
                                logPositions.Log("TYPE=" & CType(objectType, Integer))
                                readAsCreate = objectType
                            End If

                            ReadMovementUpdate(Packet)
                            ReadValuesUpdate(Packet)
                            readAsCreate = -1
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

                If readAsCreate <> -1 Then
                    logPositions.Log(String.Format("XYZ={0} {1} {2} {3}", x, y, z, o))
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

                    If readAsCreate <> -1 And i = 3 Then
                        logPositions.Log(String.Format("ENTRY={0}", tmp))
                    End If
                    'if readAsCreate <> -1 and i = 4 then
                    '    logPositions.Log(String.Format("SIZE={0}", BitConverter.ToSingle(BitConverter.GetBytes(tmp), 0))))
                    'end if
                    'if readAsCreate = 5 and i = 10 then
                    '    logPositions.Log(String.Format("ROTATION1={0}", BitConverter.ToSingle(BitConverter.GetBytes(tmp), 0))))
                    'end if
                    'if readAsCreate = 5 and i = 11 then
                    '    logPositions.Log(String.Format("ROTATION2={0}", BitConverter.ToSingle(BitConverter.GetBytes(tmp), 0))))
                    'end if
                    'if readAsCreate = 5 and i = 12 then
                    '    logPositions.Log(String.Format("ROTATION3={0}", BitConverter.ToSingle(BitConverter.GetBytes(tmp), 0))))
                    'end if
                    'if readAsCreate = 5 and i = 13 then
                    '    logPositions.Log(String.Format("ROTATION4={0}", BitConverter.ToSingle(BitConverter.GetBytes(tmp), 0))))
                    'end if
                    'if readAsCreate = 5 and i = 9 then
                    '    logPositions.Log(String.Format("FLAGS={0}", tmp))
                    'end if
                    'if readAsCreate = 5 and i = 14 then
                    '    logPositions.Log(String.Format("STATE={0}", tmp))
                    'end if
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
































    Public Enum EUpdateFields
        OBJECT_FIELD_GUID = 0       '  2  UINT64
        OBJECT_FIELD_TYPE = 2       '  1  UINT32
        OBJECT_FIELD_ENTRY = 3      '  1  UINT32
        OBJECT_FIELD_SCALE_X = 4    '  1  UINT32
        OBJECT_FIELD_PADDING = 5    '  1  UINT32

		UNIT_FIELD_CHARM = 0 + 6                             '  2  UINT64
        UNIT_FIELD_SUMMON = 2 + 6                            '  2  UINT64
        UNIT_FIELD_CHARMEDBY = 4 + 6                         '  2  UINT64
        UNIT_FIELD_SUMMONEDBY = 6 + 6                        '  2  UINT64
        UNIT_FIELD_CREATEDBY = 8 + 6                         '  2  UINT64
        UNIT_FIELD_TARGET = 10 + 6                           '  2  UINT64
        UNIT_FIELD_PERSUADED = 12 + 6                        '  2  UINT64
        UNIT_FIELD_CHANNEL_OBJECT = 14 + 6                   '  2  UINT64
        UNIT_FIELD_HEALTH = 16 + 6                           '  1  UINT32
        UNIT_FIELD_POWER1 = 17 + 6                           '  1  UINT32
        UNIT_FIELD_POWER2 = 18 + 6                           '  1  UINT32
        UNIT_FIELD_POWER3 = 19 + 6                           '  1  UINT32
        UNIT_FIELD_POWER4 = 20 + 6                           '  1  UINT32
        UNIT_FIELD_POWER5 = 21 + 6                           '  1  UINT32
        UNIT_FIELD_MAXHEALTH = 22 + 6                        '  1  UINT32
        UNIT_FIELD_MAXPOWER1 = 23 + 6                        '  1  UINT32
        UNIT_FIELD_MAXPOWER2 = 24 + 6                        '  1  UINT32
        UNIT_FIELD_MAXPOWER3 = 25 + 6                        '  1  UINT32
        UNIT_FIELD_MAXPOWER4 = 26 + 6                        '  1  UINT32
        UNIT_FIELD_MAXPOWER5 = 27 + 6                        '  1  UINT32
        UNIT_FIELD_LEVEL = 28 + 6                            '  1  UINT32
        UNIT_FIELD_FACTIONTEMPLATE = 29 + 6                  '  1  UINT32
        UNIT_FIELD_BYTES_0 = 30 + 6                          '  1  UINT32
        UNIT_VIRTUAL_ITEM_SLOT_DISPLAY = 31 + 6              '  1  UINT32
        UNIT_VIRTUAL_ITEM_SLOT_DISPLAY_01 = 32 + 6           '  1  UINT32
        UNIT_VIRTUAL_ITEM_SLOT_DISPLAY_02 = 33 + 6           '  1  UINT32
        UNIT_VIRTUAL_ITEM_INFO = 34 + 6                      '  1  UINT32
        UNIT_VIRTUAL_ITEM_INFO_01 = 35 + 6                   '  1  UINT32
        UNIT_VIRTUAL_ITEM_INFO_02 = 36 + 6                   '  1  UINT32
        UNIT_VIRTUAL_ITEM_INFO_03 = 37 + 6                   '  1  UINT32
        UNIT_VIRTUAL_ITEM_INFO_04 = 38 + 6                   '  1  UINT32
        UNIT_VIRTUAL_ITEM_INFO_05 = 39 + 6                   '  1  UINT32
        UNIT_FIELD_FLAGS = 40 + 6                            '  1  UINT32
        UNIT_FIELD_FLAGS_2 = 41 + 6                          '  1  UINT32
        UNIT_FIELD_AURA = 42 + 6                             '  56 UINT32
        UNIT_FIELD_AURAFLAGS = 98 + 6                        '  7  UINT32    /56 BYTE/
        UNIT_FIELD_AURALEVELS = 105 + 6                      '  14 UINT32    /56 WORD/
        UNIT_FIELD_AURAAPPLICATIONS = 119 + 6                '  14 UINT32    /56 WORD/
        UNIT_FIELD_AURASTATE = 133 + 6                       '  1  UINT32
        UNIT_FIELD_BASEATTACKTIME = 134 + 6                  '  1  UINT32
        UNIT_FIELD_OFFHANDATTACKTIME = 135 + 6               '  1  UINT32
        UNIT_FIELD_RANGEDATTACKTIME = 136 + 6                '  1  UINT32
        UNIT_FIELD_BOUNDINGRADIUS = 137 + 6                  '  1  FLOAT
        UNIT_FIELD_COMBATREACH = 138 + 6                     '  1  UINT32
        UNIT_FIELD_DISPLAYID = 139 + 6                       '  1  UINT32
        UNIT_FIELD_NATIVEDISPLAYID = 140 + 6                 '  1  UINT32
        UNIT_FIELD_MOUNTDISPLAYID = 141 + 6                  '  1  UINT32
        UNIT_FIELD_MINDAMAGE = 142 + 6                       '  1  FLOAT
        UNIT_FIELD_MAXDAMAGE = 143 + 6                       '  1  FLOAT
        UNIT_FIELD_MINOFFHANDDAMAGE = 144 + 6                '  1  FLOAT
        UNIT_FIELD_MAXOFFHANDDAMAGE = 145 + 6                '  1  FLOAT
        UNIT_FIELD_BYTES_1 = 146 + 6                         '  1  UINT32
        UNIT_FIELD_PETNUMBER = 147 + 6                       '  1  UINT32
        UNIT_FIELD_PET_NAME_TIMESTAMP = 148 + 6              '  1  UINT32
        UNIT_FIELD_PETEXPERIENCE = 149 + 6                   '  1  UINT32
        UNIT_FIELD_PETNEXTLEVELEXP = 150 + 6                 '  1  UINT32
        UNIT_DYNAMIC_FLAGS = 151 + 6                         '  1  UINT32
        UNIT_CHANNEL_SPELL = 152 + 6                         '  1  UINT32
        UNIT_MOD_CAST_SPEED = 153 + 6                        '  1  FLOAT
        UNIT_CREATED_BY_SPELL = 154 + 6                      '  1  UINT32
        UNIT_NPC_FLAGS = 155 + 6                             '  1  UINT32
        UNIT_NPC_EMOTESTATE = 156 + 6                        '  1  UINT32
        UNIT_TRAINING_POINTS = 157 + 6                       '  1  UINT32
        UNIT_FIELD_STAT0 = 158 + 6                           '  1  UINT32
        UNIT_FIELD_STAT1 = 159 + 6                           '  1  UINT32
        UNIT_FIELD_STAT2 = 160 + 6                           '  1  UINT32
        UNIT_FIELD_STAT3 = 161 + 6                           '  1  UINT32
        UNIT_FIELD_STAT4 = 162 + 6                           '  1  UINT32
        UNIT_FIELD_POSSTAT0 = 163 + 6                        '  1  UINT32
        UNIT_FIELD_POSSTAT1 = 164 + 6                        '  1  UINT32
        UNIT_FIELD_POSSTAT2 = 165 + 6                        '  1  UINT32
        UNIT_FIELD_POSSTAT3 = 166 + 6                        '  1  UINT32
        UNIT_FIELD_POSSTAT4 = 167 + 6                        '  1  UINT32
        UNIT_FIELD_NEGSTAT0 = 168 + 6                        '  1  UINT32
        UNIT_FIELD_NEGSTAT1 = 169 + 6                        '  1  UINT32
        UNIT_FIELD_NEGSTAT2 = 170 + 6                        '  1  UINT32
        UNIT_FIELD_NEGSTAT3 = 171 + 6                        '  1  UINT32
        UNIT_FIELD_NEGSTAT4 = 172 + 6                        '  1  UINT32
        UNIT_FIELD_RESISTANCES = 173 + 6                     '  1  UINT32
        UNIT_FIELD_RESISTANCES_01 = 174 + 6                  '  1  UINT32
        UNIT_FIELD_RESISTANCES_02 = 175 + 6                  '  1  UINT32
        UNIT_FIELD_RESISTANCES_03 = 176 + 6                  '  1  UINT32
        UNIT_FIELD_RESISTANCES_04 = 177 + 6                  '  1  UINT32
        UNIT_FIELD_RESISTANCES_05 = 178 + 6                  '  1  UINT32
        UNIT_FIELD_RESISTANCES_06 = 179 + 6                  '  1  UINT32
        UNIT_FIELD_RESISTANCEBUFFMODSPOSITIVE = 180 + 6      '  1  UINT32
        UNIT_FIELD_RESISTANCEBUFFMODSPOSITIVE_01 = 181 + 6   '  1  UINT32
        UNIT_FIELD_RESISTANCEBUFFMODSPOSITIVE_02 = 182 + 6   '  1  UINT32
        UNIT_FIELD_RESISTANCEBUFFMODSPOSITIVE_03 = 183 + 6   '  1  UINT32
        UNIT_FIELD_RESISTANCEBUFFMODSPOSITIVE_04 = 184 + 6   '  1  UINT32
        UNIT_FIELD_RESISTANCEBUFFMODSPOSITIVE_05 = 185 + 6   '  1  UINT32
        UNIT_FIELD_RESISTANCEBUFFMODSPOSITIVE_06 = 186 + 6   '  1  UINT32
        UNIT_FIELD_RESISTANCEBUFFMODSNEGATIVE = 187 + 6      '  1  UINT32
        UNIT_FIELD_RESISTANCEBUFFMODSNEGATIVE_01 = 188 + 6   '  1  UINT32
        UNIT_FIELD_RESISTANCEBUFFMODSNEGATIVE_02 = 189 + 6   '  1  UINT32
        UNIT_FIELD_RESISTANCEBUFFMODSNEGATIVE_03 = 190 + 6   '  1  UINT32
        UNIT_FIELD_RESISTANCEBUFFMODSNEGATIVE_04 = 191 + 6   '  1  UINT32
        UNIT_FIELD_RESISTANCEBUFFMODSNEGATIVE_05 = 192 + 6   '  1  UINT32
        UNIT_FIELD_RESISTANCEBUFFMODSNEGATIVE_06 = 193 + 6   '  1  UINT32

        UNIT_FIELD_BASE_MANA = 194 + 6                       '  1  UINT32
        UNIT_FIELD_BASE_HEALTH = 195 + 6                     '  1  UINT32
        UNIT_FIELD_BYTES_2 = 196 + 6                         '  1  UINT32
        UNIT_FIELD_ATTACK_POWER = 197 + 6                    '  1  UINT32
        UNIT_FIELD_ATTACK_POWER_MODS = 198 + 6               '  1  UINT32
        UNIT_FIELD_ATTACK_POWER_MULTIPLIER = 199 + 6         '  1  FLOAT
        UNIT_FIELD_RANGED_ATTACK_POWER = 200 + 6             '  1  UINT32
        UNIT_FIELD_RANGED_ATTACK_POWER_MODS = 201 + 6        '  1  UINT32
        UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER = 202 + 6  '  1  FLOAT
        UNIT_FIELD_MINRANGEDDAMAGE = 203 + 6                 '  1  FLOAT
        UNIT_FIELD_MAXRANGEDDAMAGE = 204 + 6                 '  1  FLOAT
        UNIT_FIELD_POWER_COST_MODIFIER = 205 + 6             '  7  UINT32
        UNIT_FIELD_POWER_COST_MULTIPLIER = 212 + 6           '  7  FLOAT
        UNIT_FIELD_PADDING = 219 + 6

		PLAYER_DUEL_ARBITER = 0 + 226                                                          '  1  UINT64
        PLAYER_FLAGS = 2 + 226                                                                 '  1  UINT32
        PLAYER_GUILDID = 3 + 226                                                               '  1  UINT32
        PLAYER_GUILDRANK = 4 + 226                                                             '  1  UINT32
        PLAYER_BYTES = 5 + 226                                                                 '  1  UINT32
        PLAYER_BYTES_2 = 6 + 226                                                               '  1  UINT32
        PLAYER_BYTES_3 = 7 + 226                                                               '  1  UINT32
        PLAYER_DUEL_TEAM = 8 + 226                                                             '  1  UINT32
        PLAYER_GUILD_TIMESTAMP = 9 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_1_1 = 10 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_1_2 = 11 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_1_3 = 12 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_2_1 = 13 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_2_2 = 14 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_2_3 = 15 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_3_1 = 16 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_3_2 = 17 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_3_3 = 18 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_4_1 = 19 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_4_2 = 20 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_4_3 = 21 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_5_1 = 22 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_5_2 = 23 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_5_3 = 24 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_6_1 = 25 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_6_2 = 26 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_6_3 = 27 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_7_1 = 28 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_7_2 = 29 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_7_3 = 30 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_8_1 = 31 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_8_2 = 32 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_8_3 = 33 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_9_1 = 34 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_9_2 = 35 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_9_3 = 36 + 226                                                        '  1  UINT32
        PLAYER_QUEST_LOG_10_1 = 37 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_10_2 = 38 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_10_3 = 39 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_11_1 = 40 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_11_2 = 41 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_11_3 = 42 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_12_1 = 43 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_12_2 = 44 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_12_3 = 45 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_13_1 = 46 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_13_2 = 47 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_13_3 = 48 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_14_1 = 49 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_14_2 = 50 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_14_3 = 51 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_15_1 = 52 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_15_2 = 53 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_15_3 = 54 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_16_1 = 55 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_16_2 = 56 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_16_3 = 57 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_17_1 = 58 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_17_2 = 59 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_17_3 = 60 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_18_1 = 61 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_18_2 = 62 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_18_3 = 63 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_19_1 = 64 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_19_2 = 65 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_19_3 = 66 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_20_1 = 67 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_20_2 = 68 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_20_3 = 69 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_21_1 = 70 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_21_2 = 71 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_21_3 = 72 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_22_1 = 73 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_22_2 = 74 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_22_3 = 75 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_23_1 = 76 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_23_2 = 77 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_23_3 = 78 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_24_1 = 79 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_24_2 = 80 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_24_3 = 81 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_25_1 = 82 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_25_2 = 83 + 226                                                       '  1  UINT32
        PLAYER_QUEST_LOG_25_3 = 84 + 226                                                       '  1  UINT32

        PLAYER_VISIBLE_ITEM_1_CREATOR = 85 + 226                                               '  1  UINT64
        PLAYER_VISIBLE_ITEM_1_0 = 87 + 226                                                     '  1  UINT32
        PLAYER_VISIBLE_ITEM_1_1 = 88 + 226                                                     '  1  UINT32
        PLAYER_VISIBLE_ITEM_1_2 = 89 + 226                                                     '  1  UINT32
        PLAYER_VISIBLE_ITEM_1_3 = 90 + 226                                                     '  1  UINT32
        PLAYER_VISIBLE_ITEM_1_4 = 91 + 226                                                     '  1  UINT32
        PLAYER_VISIBLE_ITEM_1_5 = 92 + 226                                                     '  1  UINT32
        PLAYER_VISIBLE_ITEM_1_6 = 93 + 226                                                     '  1  UINT32
        PLAYER_VISIBLE_ITEM_1_7 = 94 + 226                                                     '  1  UINT32
        PLAYER_VISIBLE_ITEM_1_8 = 95 + 226                                                     '  1  UINT32
        PLAYER_VISIBLE_ITEM_1_9 = 96 + 226                                                     '  1  UINT32
        PLAYER_VISIBLE_ITEM_1_10 = 97 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_1_11 = 98 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_1_PROPERTIES = 99 + 226                                            '  1  UINT32
        PLAYER_VISIBLE_ITEM_1_PAD = 100 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_2_CREATOR = 101 + 226                                              '  1  UINT64
        PLAYER_VISIBLE_ITEM_2_0 = 103 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_2_1 = 104 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_2_2 = 105 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_2_3 = 106 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_2_4 = 107 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_2_5 = 108 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_2_6 = 109 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_2_7 = 110 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_2_8 = 111 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_2_9 = 112 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_2_10 = 113 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_2_11 = 114 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_2_PROPERTIES = 115 + 226                                           '  1  UINT32
        PLAYER_VISIBLE_ITEM_2_PAD = 116 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_3_CREATOR = 117 + 226                                              '  1  UINT64
        PLAYER_VISIBLE_ITEM_3_0 = 119 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_3_1 = 120 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_3_2 = 121 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_3_3 = 122 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_3_4 = 123 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_3_5 = 124 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_3_6 = 125 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_3_7 = 126 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_3_8 = 127 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_3_9 = 128 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_3_10 = 129 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_3_11 = 130 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_3_PROPERTIES = 131 + 226                                           '  1  UINT32
        PLAYER_VISIBLE_ITEM_3_PAD = 132 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_4_CREATOR = 133 + 226                                              '  1  UINT64
        PLAYER_VISIBLE_ITEM_4_0 = 135 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_4_1 = 136 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_4_2 = 137 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_4_3 = 138 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_4_4 = 139 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_4_5 = 140 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_4_6 = 141 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_4_7 = 142 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_4_8 = 143 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_4_9 = 144 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_4_10 = 145 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_4_11 = 146 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_4_PROPERTIES = 147 + 226                                           '  1  UINT32
        PLAYER_VISIBLE_ITEM_4_PAD = 148 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_5_CREATOR = 149 + 226                                              '  1  UINT64
        PLAYER_VISIBLE_ITEM_5_0 = 151 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_5_1 = 152 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_5_2 = 153 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_5_3 = 154 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_5_4 = 155 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_5_5 = 156 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_5_6 = 157 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_5_7 = 158 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_5_8 = 159 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_5_9 = 160 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_5_10 = 161 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_5_11 = 162 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_5_PROPERTIES = 163 + 226                                           '  1  UINT32
        PLAYER_VISIBLE_ITEM_5_PAD = 164 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_6_CREATOR = 165 + 226                                              '  1  UINT64
        PLAYER_VISIBLE_ITEM_6_0 = 167 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_6_1 = 168 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_6_2 = 169 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_6_3 = 170 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_6_4 = 171 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_6_5 = 172 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_6_6 = 173 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_6_7 = 174 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_6_8 = 175 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_6_9 = 176 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_6_10 = 177 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_6_11 = 178 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_6_PROPERTIES = 179 + 226                                           '  1  UINT32
        PLAYER_VISIBLE_ITEM_6_PAD = 180 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_7_CREATOR = 181 + 226                                              '  1  UINT64
        PLAYER_VISIBLE_ITEM_7_0 = 183 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_7_1 = 184 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_7_2 = 185 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_7_3 = 186 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_7_4 = 187 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_7_5 = 188 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_7_6 = 189 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_7_7 = 190 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_7_8 = 191 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_7_9 = 192 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_7_10 = 193 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_7_11 = 194 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_7_PROPERTIES = 195 + 226                                           '  1  UINT32
        PLAYER_VISIBLE_ITEM_7_PAD = 196 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_8_CREATOR = 197 + 226                                              '  1  UINT64
        PLAYER_VISIBLE_ITEM_8_0 = 199 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_8_1 = 200 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_8_2 = 201 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_8_3 = 202 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_8_4 = 203 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_8_5 = 204 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_8_6 = 205 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_8_7 = 206 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_8_8 = 207 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_8_9 = 208 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_8_10 = 209 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_8_11 = 210 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_8_PROPERTIES = 211 + 226                                           '  1  UINT32
        PLAYER_VISIBLE_ITEM_8_PAD = 212 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_9_CREATOR = 213 + 226                                              '  1  UINT64
        PLAYER_VISIBLE_ITEM_9_0 = 215 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_9_1 = 216 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_9_2 = 217 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_9_3 = 218 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_9_4 = 219 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_9_5 = 220 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_9_6 = 221 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_9_7 = 222 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_9_8 = 223 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_9_9 = 224 + 226                                                    '  1  UINT32
        PLAYER_VISIBLE_ITEM_9_10 = 225 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_9_11 = 226 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_9_PROPERTIES = 227 + 226                                           '  1  UINT32
        PLAYER_VISIBLE_ITEM_9_PAD = 228 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_10_CREATOR = 229 + 226                                             '  1  UINT64
        PLAYER_VISIBLE_ITEM_10_0 = 231 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_10_1 = 232 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_10_2 = 233 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_10_3 = 234 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_10_4 = 235 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_10_5 = 236 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_10_6 = 237 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_10_7 = 238 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_10_8 = 239 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_10_9 = 240 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_10_10 = 241 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_10_11 = 242 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_10_PROPERTIES = 243 + 226                                          '  1  UINT32
        PLAYER_VISIBLE_ITEM_10_PAD = 244 + 226                                                 '  1  UINT32
        PLAYER_VISIBLE_ITEM_11_CREATOR = 245 + 226                                             '  1  UINT64
        PLAYER_VISIBLE_ITEM_11_0 = 247 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_11_1 = 248 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_11_2 = 249 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_11_3 = 250 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_11_4 = 251 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_11_5 = 252 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_11_6 = 253 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_11_7 = 254 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_11_8 = 255 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_11_9 = 256 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_11_10 = 257 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_11_11 = 258 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_11_PROPERTIES = 259 + 226                                          '  1  UINT32
        PLAYER_VISIBLE_ITEM_11_PAD = 260 + 226                                                 '  1  UINT32
        PLAYER_VISIBLE_ITEM_12_CREATOR = 261 + 226                                             '  1  UINT64
        PLAYER_VISIBLE_ITEM_12_0 = 263 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_12_1 = 264 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_12_2 = 265 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_12_3 = 266 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_12_4 = 267 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_12_5 = 268 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_12_6 = 269 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_12_7 = 270 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_12_8 = 271 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_12_9 = 272 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_12_10 = 273 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_12_11 = 274 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_12_PROPERTIES = 275 + 226                                          '  1  UINT32
        PLAYER_VISIBLE_ITEM_12_PAD = 276 + 226                                                 '  1  UINT32
        PLAYER_VISIBLE_ITEM_13_CREATOR = 277 + 226                                             '  1  UINT64
        PLAYER_VISIBLE_ITEM_13_0 = 279 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_13_1 = 280 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_13_2 = 281 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_13_3 = 282 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_13_4 = 283 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_13_5 = 284 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_13_6 = 285 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_13_7 = 286 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_13_8 = 287 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_13_9 = 288 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_13_10 = 289 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_13_11 = 290 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_13_PROPERTIES = 291 + 226                                          '  1  UINT32
        PLAYER_VISIBLE_ITEM_13_PAD = 292 + 226                                                 '  1  UINT32
        PLAYER_VISIBLE_ITEM_14_CREATOR = 293 + 226                                             '  1  UINT64
        PLAYER_VISIBLE_ITEM_14_0 = 295 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_14_1 = 296 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_14_2 = 297 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_14_3 = 298 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_14_4 = 299 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_14_5 = 300 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_14_6 = 301 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_14_7 = 302 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_14_8 = 303 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_14_9 = 304 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_14_10 = 305 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_14_11 = 306 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_14_PROPERTIES = 307 + 226                                          '  1  UINT32
        PLAYER_VISIBLE_ITEM_14_PAD = 308 + 226                                                 '  1  UINT32
        PLAYER_VISIBLE_ITEM_15_CREATOR = 309 + 226                                             '  1  UINT64
        PLAYER_VISIBLE_ITEM_15_0 = 311 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_15_1 = 312 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_15_2 = 313 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_15_3 = 314 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_15_4 = 315 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_15_5 = 316 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_15_6 = 317 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_15_7 = 318 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_15_8 = 319 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_15_9 = 320 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_15_10 = 321 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_15_11 = 322 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_15_PROPERTIES = 323 + 226                                          '  1  UINT32
        PLAYER_VISIBLE_ITEM_15_PAD = 324 + 226                                                 '  1  UINT32
        PLAYER_VISIBLE_ITEM_16_CREATOR = 325 + 226                                             '  1  UINT64
        PLAYER_VISIBLE_ITEM_16_0 = 327 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_16_1 = 328 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_16_2 = 329 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_16_3 = 330 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_16_4 = 331 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_16_5 = 332 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_16_6 = 333 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_16_7 = 334 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_16_8 = 335 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_16_9 = 336 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_16_10 = 337 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_16_11 = 338 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_16_PROPERTIES = 339 + 226                                          '  1  UINT32
        PLAYER_VISIBLE_ITEM_16_PAD = 340 + 226                                                 '  1  UINT32
        PLAYER_VISIBLE_ITEM_17_CREATOR = 341 + 226                                             '  1  UINT64
        PLAYER_VISIBLE_ITEM_17_0 = 343 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_17_1 = 344 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_17_2 = 345 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_17_3 = 346 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_17_4 = 347 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_17_5 = 348 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_17_6 = 349 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_17_7 = 350 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_17_8 = 351 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_17_9 = 352 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_17_10 = 353 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_17_11 = 354 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_17_PROPERTIES = 355 + 226                                          '  1  UINT32
        PLAYER_VISIBLE_ITEM_17_PAD = 356 + 226                                                 '  1  UINT32
        PLAYER_VISIBLE_ITEM_18_CREATOR = 357 + 226                                             '  1  UINT64
        PLAYER_VISIBLE_ITEM_18_0 = 359 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_18_1 = 360 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_18_2 = 361 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_18_3 = 362 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_18_4 = 363 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_18_5 = 364 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_18_6 = 365 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_18_7 = 366 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_18_8 = 367 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_18_9 = 368 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_18_10 = 369 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_18_11 = 370 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_18_PROPERTIES = 371 + 226                                          '  1  UINT32
        PLAYER_VISIBLE_ITEM_18_PAD = 372 + 226                                                 '  1  UINT32
        PLAYER_VISIBLE_ITEM_19_CREATOR = 373 + 226                                             '  1  UINT64
        PLAYER_VISIBLE_ITEM_19_0 = 375 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_19_1 = 376 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_19_2 = 377 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_19_3 = 378 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_19_4 = 379 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_19_5 = 380 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_19_6 = 381 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_19_7 = 382 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_19_8 = 383 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_19_9 = 384 + 226                                                   '  1  UINT32
        PLAYER_VISIBLE_ITEM_19_10 = 385 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_19_11 = 386 + 226                                                  '  1  UINT32
        PLAYER_VISIBLE_ITEM_19_PROPERTIES = 387 + 226                                          '  1  UINT32
        PLAYER_VISIBLE_ITEM_19_PAD = 388 + 226                                                 '  1  UINT32

        PLAYER_CHOSEN_TITLE = 389 + 226                                                        '  1  UINT32
        PLAYER_FIELD_INV_SLOT_HEAD = 390 + 226                                                 '  23 UINT64    /19 x EQUIPMENT SLOTS, 4 x BAG SLOTS/
        'PLAYER_FIELD_INV_SLOT_BAG_1 = PLAYER_FIELD_INV_SLOT_HEAD + INVENTORY_SLOT_BAG_1 * 2 + 226
        'PLAYER_FIELD_INV_SLOT_BAG_2 = PLAYER_FIELD_INV_SLOT_HEAD + INVENTORY_SLOT_BAG_2 * 2 + 226
        'PLAYER_FIELD_INV_SLOT_BAG_3 = PLAYER_FIELD_INV_SLOT_HEAD + INVENTORY_SLOT_BAG_3 * 2 + 226
        'PLAYER_FIELD_INV_SLOT_BAG_4 = PLAYER_FIELD_INV_SLOT_HEAD + INVENTORY_SLOT_BAG_4 * 2 + 226
        PLAYER_FIELD_PACK_SLOT_1 = 436 + 226                                                   '  16 UINT64    /INVENTORY BAG/
        PLAYER_FIELD_BANK_SLOT_1 = 468 + 226                                                   '  28 UINT64    /BANK         /
        PLAYER_FIELD_BANKBAG_SLOT_1 = 524 + 226                                                '  7  UINT64    /BANK BAGS    /
        PLAYER_FIELD_VENDORBUYBACK_SLOT_1 = 538 + 226                                          '  12 UINT64    /BUYBACK SLOTS/
        PLAYER_FIELD_KEYRING_SLOT_1 = 562 + 226                                                '  32 UINT64    /KEYRING SLOTS/

        PLAYER_FARSIGHT = 626 + 226                                                            '  1  UINT64
        PLAYER_FIELD_COMBO_TARGET = 628 + 226                                                  '  1  UINT64
        PLAYER_FIELD_KNOWN_TITLES = 630 + 226                                                  '  1  UINT64
        PLAYER_XP = 632 + 226                                                                  '  1  UINT32
        PLAYER_NEXT_LEVEL_XP = 633 + 226                                                       '  1  UINT32
        PLAYER_SKILL_INFO_START = 634 + 226                                                    ' 384 UINT32

        PLAYER_CHARACTER_POINTS1 = 1018 + 226                                                  '  1  UINT32
        PLAYER_CHARACTER_POINTS2 = 1019 + 226                                                  '  1  UINT32
        PLAYER_TRACK_CREATURES = 1020 + 226                                                    '  1  UINT32
        PLAYER_TRACK_RESOURCES = 1021 + 226                                                    '  1  UINT32
        PLAYER_BLOCK_PERCENTAGE = 1022 + 226                                                   '  1  FLOAT
        PLAYER_DODGE_PERCENTAGE = 1023 + 226                                                   '  1  FLOAT
        PLAYER_PARRY_PERCENTAGE = 1024 + 226                                                   '  1  FLOAT
        PLAYER_CRIT_PERCENTAGE = 1025 + 226                                                    '  1  FLOAT
        PLAYER_RANGED_CRIT_PERCENTAGE = 1026 + 226                                             '  1  FLOAT
        PLAYER_OFFHAND_CRIT_PERCENTAGE = 1027 + 226                                            '  1  FLOAT
        PLAYER_SPELL_CRIT_PERCENTAGE = 1028 + 226                                              '  1  FLOAT
        PLAYER_SPELL_CRIT_PERCENTAGE01 = 1029 + 226                                            '  1  FLOAT
        PLAYER_SPELL_CRIT_PERCENTAGE02 = 1030 + 226                                            '  1  FLOAT
        PLAYER_SPELL_CRIT_PERCENTAGE03 = 1031 + 226                                            '  1  FLOAT
        PLAYER_SPELL_CRIT_PERCENTAGE04 = 1032 + 226                                            '  1  FLOAT
        PLAYER_SPELL_CRIT_PERCENTAGE05 = 1033 + 226                                            '  1  FLOAT
        PLAYER_SPELL_CRIT_PERCENTAGE06 = 1034 + 226                                            '  1  FLOAT

        PLAYER_EXPLORED_ZONES_1 = 1035 + 226                                                   '  64 UINT32
        PLAYER_REST_STATE_EXPERIENCE = 1099 + 226                                              '  1  UINT32
        PLAYER_FIELD_COINAGE = 1100 + 226                                                      '  1  UINT32
        PLAYER_FIELD_MOD_DAMAGE_DONE_POS = 1101 + 226                                          '  7  UINT32
        PLAYER_FIELD_MOD_DAMAGE_DONE_NEG = 1108 + 226                                          '  7  UINT32
        PLAYER_FIELD_MOD_DAMAGE_DONE_PCT = 1115 + 226                                          '  7  UINT32
        PLAYER_FIELD_MOD_HEALING_DONE_POS = 1122 + 226                                         '  1  UINT32
        PLAYER_FIELD_MOD_TARGET_RESISTANCE = 1123 + 226                                        '  1  UINT32

        PLAYER_FIELD_BYTES = 1124 + 226                                                        '  1  UINT32
        PLAYER_AMMO_ID = 1125 + 226                                                            '  1  UINT32
        PLAYER_SELF_RES_SPELL = 1126 + 226                                                     '  1  UINT32
        PLAYER_FIELD_PVP_MEDALS = 1127 + 226                                                   '  1  UINT32
        PLAYER_FIELD_BUYBACK_PRICE_1 = 1128 + 226                                              '  12 UINT32
        PLAYER_FIELD_BUYBACK_TIMESTAMP_1 = 1140 + 226                                          '  12 UINT32
        PLAYER_FIELD_KILLS = 1152 + 226                                                        '  1  UINT32
        PLAYER_FIELD_TODAY_CONTRIBUTION = 1153 + 226                                           '  1  UINT32
        PLAYER_FIELD_YESTERDAY_CONTRIBUTION = 1154 + 226                                       '  1  UINT32
        PLAYER_FIELD_LIFETIME_HONORBALE_KILLS = 1155 + 226                                     '  1  UINT32
        PLAYER_FIELD_BYTES2 = 1156 + 226                                                       '  1  UINT32
        PLAYER_FIELD_WATCHED_FACTION_INDEX = 1157 + 226                                        '  1  UINT32
        PLAYER_FIELD_COMBAT_RATING_1 = 1158 + 226                                              '  23 UINT32
        PLAYER_FIELD_COMBAT_RATING_01 = 1159 + 226
        PLAYER_FIELD_COMBAT_RATING_02 = 1160 + 226
        PLAYER_FIELD_COMBAT_RATING_03 = 1161 + 226
        PLAYER_FIELD_COMBAT_RATING_04 = 1162 + 226
        PLAYER_FIELD_COMBAT_RATING_05 = 1163 + 226
        PLAYER_FIELD_COMBAT_RATING_06 = 1164 + 226
        PLAYER_FIELD_COMBAT_RATING_07 = 1165 + 226
        PLAYER_FIELD_COMBAT_RATING_08 = 1166 + 226
        PLAYER_FIELD_COMBAT_RATING_09 = 1167 + 226
        PLAYER_FIELD_COMBAT_RATING_10 = 1168 + 226
        PLAYER_FIELD_COMBAT_RATING_11 = 1169 + 226
        PLAYER_FIELD_COMBAT_RATING_12 = 1170 + 226
        PLAYER_FIELD_COMBAT_RATING_13 = 1171 + 226
        PLAYER_FIELD_COMBAT_RATING_14 = 1172 + 226
        PLAYER_FIELD_COMBAT_RATING_15 = 1173 + 226
        PLAYER_FIELD_COMBAT_RATING_16 = 1174 + 226
        PLAYER_FIELD_COMBAT_RATING_17 = 1175 + 226
        PLAYER_FIELD_COMBAT_RATING_18 = 1176 + 226
        PLAYER_FIELD_COMBAT_RATING_19 = 1177 + 226
        PLAYER_FIELD_COMBAT_RATING_20 = 1178 + 226
        PLAYER_FIELD_COMBAT_RATING_21 = 1179 + 226
        PLAYER_FIELD_COMBAT_RATING_22 = 1180 + 226
        PLAYER_FIELD_ARENA_TEAM_INFO_1_1 = 1181 + 226                                          '  9  UINT32
        PLAYER_FIELD_ARENA_TEAM_INFO_1_01 = 1182 + 226
        PLAYER_FIELD_ARENA_TEAM_INFO_1_02 = 1183 + 226
        PLAYER_FIELD_ARENA_TEAM_INFO_1_03 = 1184 + 226
        PLAYER_FIELD_ARENA_TEAM_INFO_1_04 = 1185 + 226
        PLAYER_FIELD_ARENA_TEAM_INFO_1_05 = 1186 + 226
        PLAYER_FIELD_ARENA_TEAM_INFO_1_06 = 1187 + 226
        PLAYER_FIELD_ARENA_TEAM_INFO_1_07 = 1188 + 226
        PLAYER_FIELD_ARENA_TEAM_INFO_1_08 = 1189 + 226
        PLAYER_FIELD_HONOR_CURRENCY = 1190 + 226                                              '  1  UINT32
        PLAYER_FIELD_ARENA_CURRENCY = 1191 + 226                                               '  1  UINT32
        PLAYER_FIELD_MOD_MANA_REGEN = 1192 + 226                                               '  1  FLOAT
        PLAYER_FIELD_MOD_MANA_REGEN_INTERRUPT = 1193 + 226                                     '  1  FLOAT
        PLAYER_FIELD_MAX_LEVEL = 1194 + 226                                                    '  1  UINT32
        PLAYER_END = 1195 + 226                                                                '  0  INTERNALMARKER
    End Enum


    End Class
End Namespace
