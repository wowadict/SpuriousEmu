Imports System
Imports Microsoft.VisualBasic
Imports System.Collections

Namespace PacketLogger
    Public Class ScriptedPacketHandler
        Inherits PacketLogger.BasePacketHandler

        Dim log As PacketLogger.BaseFileLogger = Nothing

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
































        Public Enum EObjectFields
            OBJECT_FIELD_GUID = 0       '  2  UINT64
            OBJECT_FIELD_TYPE = 2       '  1  UINT32
            OBJECT_FIELD_ENTRY = 3      '  1  UINT32
            OBJECT_FIELD_SCALE_X = 4    '  1  UINT32
            OBJECT_FIELD_PADDING = 5    '  1  UINT32
            OBJECT_END = 6              '  0  INTERNALMARKER
        End Enum
        Public Enum EItemFields
            ITEM_FIELD_OWNER = 6                    '  2  UINT64
            ITEM_FIELD_CONTAINED = 8                '  2  UINT64
            ITEM_FIELD_CREATOR = 10                 '  2  UINT64
            ITEM_FIELD_GIFTCREATOR = 12             '  2  UINT64
            ITEM_FIELD_STACK_COUNT = 14             '  1  UINT32
            ITEM_FIELD_DURATION = 15                '  1  UINT32
            ITEM_FIELD_SPELL_CHARGES = 16           '  5  SPELLCHARGES
            ITEM_FIELD_FLAGS = 21                   '  1  UINT32
            ITEM_FIELD_ENCHANTMENT = 22             '  21 ENCHANTMENT
            ITEM_FIELD_PROPERTY_SEED = 43           '  1  UINT32
            ITEM_FIELD_RANDOM_PROPERTIES_ID = 44    '  1  UINT32
            ITEM_FIELD_ITEM_TEXT_ID = 45            '  1  UINT32
            ITEM_FIELD_DURABILITY = 46              '  1  UINT32
            ITEM_FIELD_MAXDURABILITY = 47           '  1  UINT32
            ITEM_END = 48                           '  0  INTERNALMARKER
        End Enum
        Public Enum EContainerFields
            CONTAINER_FIELD_NUM_SLOTS = 48          '  1  UINT32
            CONTAINER_ALIGN_PAD = 49                '  1  UINT32
            CONTAINER_FIELD_SLOT_1 = 50             '  40 CONTAINERSLOTS
            CONTAINER_END = 90                      '  0  INTERNALMARKER
        End Enum
        Public Enum EUnitFields
            UNIT_FIELD_CHARM = 0 + EObjectFields.OBJECT_END
            UNIT_FIELD_SUMMON = &H2 + EObjectFields.OBJECT_END
            UNIT_FIELD_CHARMEDBY = &H4 + EObjectFields.OBJECT_END
            UNIT_FIELD_SUMMONEDBY = &H6 + EObjectFields.OBJECT_END
            UNIT_FIELD_CREATEDBY = &H8 + EObjectFields.OBJECT_END
            UNIT_FIELD_TARGET = &HA + EObjectFields.OBJECT_END
            UNIT_FIELD_PERSUADED = &HC + EObjectFields.OBJECT_END
            UNIT_FIELD_CHANNEL_OBJECT = &HE + EObjectFields.OBJECT_END
            UNIT_FIELD_HEALTH = &H10 + EObjectFields.OBJECT_END
            UNIT_FIELD_POWER1 = &H11 + EObjectFields.OBJECT_END
            UNIT_FIELD_POWER2 = &H12 + EObjectFields.OBJECT_END
            UNIT_FIELD_POWER3 = &H13 + EObjectFields.OBJECT_END
            UNIT_FIELD_POWER4 = &H14 + EObjectFields.OBJECT_END
            UNIT_FIELD_POWER5 = &H15 + EObjectFields.OBJECT_END
            UNIT_FIELD_MAXHEALTH = &H16 + EObjectFields.OBJECT_END
            UNIT_FIELD_MAXPOWER1 = &H17 + EObjectFields.OBJECT_END
            UNIT_FIELD_MAXPOWER2 = &H18 + EObjectFields.OBJECT_END
            UNIT_FIELD_MAXPOWER3 = &H19 + EObjectFields.OBJECT_END
            UNIT_FIELD_MAXPOWER4 = &H1A + EObjectFields.OBJECT_END
            UNIT_FIELD_MAXPOWER5 = &H1B + EObjectFields.OBJECT_END
            UNIT_FIELD_LEVEL = &H1C + EObjectFields.OBJECT_END
            UNIT_FIELD_FACTIONTEMPLATE = &H1D + EObjectFields.OBJECT_END
            UNIT_FIELD_BYTES_0 = &H1E + EObjectFields.OBJECT_END
            UNIT_VIRTUAL_ITEM_SLOT_DISPLAY = &H1F + EObjectFields.OBJECT_END
            UNIT_VIRTUAL_ITEM_SLOT_DISPLAY_01 = &H20 + EObjectFields.OBJECT_END
            UNIT_VIRTUAL_ITEM_SLOT_DISPLAY_02 = &H21 + EObjectFields.OBJECT_END
            UNIT_VIRTUAL_ITEM_INFO = &H22 + EObjectFields.OBJECT_END
            UNIT_VIRTUAL_ITEM_INFO_01 = &H23 + EObjectFields.OBJECT_END
            UNIT_VIRTUAL_ITEM_INFO_02 = &H24 + EObjectFields.OBJECT_END
            UNIT_VIRTUAL_ITEM_INFO_03 = &H25 + EObjectFields.OBJECT_END
            UNIT_VIRTUAL_ITEM_INFO_04 = &H26 + EObjectFields.OBJECT_END
            UNIT_VIRTUAL_ITEM_INFO_05 = &H27 + EObjectFields.OBJECT_END

            UNIT_FIELD_FLAGS = &H28 + EObjectFields.OBJECT_END
            UNIT_FIELD_AURA = &H29 + EObjectFields.OBJECT_END

            UNIT_FIELD_AURAFLAGS = &H59 + EObjectFields.OBJECT_END
            UNIT_FIELD_AURALEVELS = &H5F + EObjectFields.OBJECT_END
            UNIT_FIELD_AURAAPPLICATIONS = &H6B + EObjectFields.OBJECT_END
            UNIT_FIELD_AURASTATE = &H77 + EObjectFields.OBJECT_END
            UNIT_FIELD_BASEATTACKTIME = &H78 + EObjectFields.OBJECT_END
            UNIT_FIELD_RANGEDATTACKTIME = &H7A + EObjectFields.OBJECT_END
            UNIT_FIELD_BOUNDINGRADIUS = &H7B + EObjectFields.OBJECT_END
            UNIT_FIELD_COMBATREACH = &H7C + EObjectFields.OBJECT_END
            UNIT_FIELD_DISPLAYID = &H7D + EObjectFields.OBJECT_END
            UNIT_FIELD_NATIVEDISPLAYID = &H7E + EObjectFields.OBJECT_END
            UNIT_FIELD_MOUNTDISPLAYID = &H7F + EObjectFields.OBJECT_END
            UNIT_FIELD_MINDAMAGE = &H80 + EObjectFields.OBJECT_END
            UNIT_FIELD_MAXDAMAGE = &H81 + EObjectFields.OBJECT_END
            UNIT_FIELD_MINOFFHANDDAMAGE = &H82 + EObjectFields.OBJECT_END
            UNIT_FIELD_MAXOFFHANDDAMAGE = &H83 + EObjectFields.OBJECT_END
            UNIT_FIELD_BYTES_1 = &H84 + EObjectFields.OBJECT_END
            UNIT_FIELD_PETNUMBER = &H85 + EObjectFields.OBJECT_END
            UNIT_FIELD_PET_NAME_TIMESTAMP = &H86 + EObjectFields.OBJECT_END
            UNIT_FIELD_PETEXPERIENCE = &H87 + EObjectFields.OBJECT_END
            UNIT_FIELD_PETNEXTLEVELEXP = &H88 + EObjectFields.OBJECT_END
            UNIT_DYNAMIC_FLAGS = &H89 + EObjectFields.OBJECT_END
            UNIT_CHANNEL_SPELL = &H8A + EObjectFields.OBJECT_END
            UNIT_MOD_CAST_SPEED = &H8B + EObjectFields.OBJECT_END
            UNIT_CREATED_BY_SPELL = &H8C + EObjectFields.OBJECT_END
            UNIT_NPC_FLAGS = &H8D + EObjectFields.OBJECT_END
            UNIT_NPC_EMOTESTATE = &H8E + EObjectFields.OBJECT_END
            UNIT_TRAINING_POINTS = &H8F + EObjectFields.OBJECT_END
            UNIT_FIELD_STAT0 = &H90 + EObjectFields.OBJECT_END
            UNIT_FIELD_STRENGTH = UNIT_FIELD_STAT0
            UNIT_FIELD_STAT1 = &H91 + EObjectFields.OBJECT_END
            UNIT_FIELD_AGILITY = UNIT_FIELD_STAT1
            UNIT_FIELD_STAT2 = &H92 + EObjectFields.OBJECT_END
            UNIT_FIELD_STAMINA = UNIT_FIELD_STAT2
            UNIT_FIELD_STAT3 = &H93 + EObjectFields.OBJECT_END
            UNIT_FIELD_INTELLECT = UNIT_FIELD_STAT3
            UNIT_FIELD_STAT4 = &H94 + EObjectFields.OBJECT_END
            UNIT_FIELD_SPIRIT = UNIT_FIELD_STAT4
            UNIT_FIELD_RESISTANCES = &H95 + EObjectFields.OBJECT_END
            UNIT_FIELD_ARMOR = UNIT_FIELD_RESISTANCES
            UNIT_FIELD_RESISTANCES_01 = &H96 + EObjectFields.OBJECT_END
            UNIT_FIELD_RESISTANCES_02 = &H97 + EObjectFields.OBJECT_END
            UNIT_FIELD_RESISTANCES_03 = &H98 + EObjectFields.OBJECT_END
            UNIT_FIELD_RESISTANCES_04 = &H99 + EObjectFields.OBJECT_END
            UNIT_FIELD_RESISTANCES_05 = &H9A + EObjectFields.OBJECT_END
            UNIT_FIELD_RESISTANCES_06 = &H9B + EObjectFields.OBJECT_END

            UNIT_FIELD_BASE_MANA = &H9C + EObjectFields.OBJECT_END
            UNIT_FIELD_BASE_HEALTH = &H9D + EObjectFields.OBJECT_END
            UNIT_FIELD_BYTES_2 = &H9E + EObjectFields.OBJECT_END
            UNIT_FIELD_ATTACK_POWER = &H9F + EObjectFields.OBJECT_END
            UNIT_FIELD_ATTACK_POWER_MODS = &HA0 + EObjectFields.OBJECT_END
            UNIT_FIELD_ATTACK_POWER_MULTIPLIER = &HA1 + EObjectFields.OBJECT_END
            UNIT_FIELD_RANGED_ATTACK_POWER = &HA2 + EObjectFields.OBJECT_END
            UNIT_FIELD_RANGED_ATTACK_POWER_MODS = &HA3 + EObjectFields.OBJECT_END
            UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER = &HA4 + EObjectFields.OBJECT_END
            UNIT_FIELD_MINRANGEDDAMAGE = &HA5 + EObjectFields.OBJECT_END
            UNIT_FIELD_MAXRANGEDDAMAGE = &HA6 + EObjectFields.OBJECT_END
            UNIT_FIELD_POWER_COST_MODIFIER = &HA7 + EObjectFields.OBJECT_END
            UNIT_FIELD_POWER_COST_MULTIPLIER = &HAE + EObjectFields.OBJECT_END
            UNIT_FIELD_PADDING = &HB5 + EObjectFields.OBJECT_END
            UNIT_END = &HB6 + EObjectFields.OBJECT_END
        End Enum
        Public Enum EPlayerFields
            PLAYER_SELECTION = &H0 + EUnitFields.UNIT_END
            PLAYER_DUEL_ARBITER = &H2 + EUnitFields.UNIT_END
            PLAYER_FLAGS = &H4 + EUnitFields.UNIT_END
            PLAYER_GUILDID = &H5 + EUnitFields.UNIT_END
            PLAYER_GUILDRANK = &H6 + EUnitFields.UNIT_END
            PLAYER_BYTES = &H7 + EUnitFields.UNIT_END
            PLAYER_BYTES_2 = &H8 + EUnitFields.UNIT_END
            PLAYER_BYTES_3 = &H9 + EUnitFields.UNIT_END
            PLAYER_DUEL_TEAM = &HA + EUnitFields.UNIT_END
            PLAYER_GUILD_TIMESTAMP = &HB + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_1_1 = &HC + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_1_2 = &HD + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_1_3 = &HE + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_2_1 = &HF + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_2_2 = &H10 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_3_1 = &H12 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_3_2 = &H13 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_4_1 = &H15 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_4_2 = &H16 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_5_1 = &H18 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_5_2 = &H19 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_6_1 = &H1B + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_6_2 = &H1C + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_7_1 = &H1E + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_7_2 = &H1F + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_8_1 = &H21 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_8_2 = &H22 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_9_1 = &H24 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_9_2 = &H25 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_10_1 = &H27 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_10_2 = &H28 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_11_1 = &H2A + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_11_2 = &H2B + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_12_1 = &H2D + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_12_2 = &H2E + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_13_1 = &H30 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_13_2 = &H31 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_14_1 = &H33 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_14_2 = &H34 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_15_1 = &H36 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_15_2 = &H37 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_16_1 = &H39 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_16_2 = &H3A + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_17_1 = &H3C + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_17_2 = &H3D + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_18_1 = &H3F + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_18_2 = &H40 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_19_1 = &H42 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_19_2 = &H43 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_20_1 = &H45 + EUnitFields.UNIT_END
            PLAYER_QUEST_LOG_20_2 = &H46 + EUnitFields.UNIT_END

            '260
            PLAYER_VISIBLE_ITEM_1_CREATOR = &H48 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_1_0 = &H4A + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_1_PROPERTIES = &H52 + EUnitFields.UNIT_END
            '271
            PLAYER_VISIBLE_ITEM_1_PAD = &H53 + EUnitFields.UNIT_END

            '272
            PLAYER_VISIBLE_ITEM_2_CREATOR = &H54 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_2_0 = &H56 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_2_PROPERTIES = &H5E + EUnitFields.UNIT_END
            '283
            PLAYER_VISIBLE_ITEM_2_PAD = &H5F + EUnitFields.UNIT_END

            '284
            PLAYER_VISIBLE_ITEM_3_CREATOR = &H60 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_3_0 = &H62 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_3_PROPERTIES = &H6A + EUnitFields.UNIT_END
            '295
            PLAYER_VISIBLE_ITEM_3_PAD = &H6B + EUnitFields.UNIT_END

            '296
            PLAYER_VISIBLE_ITEM_4_CREATOR = &H6C + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_4_0 = &H6E + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_4_PROPERTIES = &H76 + EUnitFields.UNIT_END
            '307
            PLAYER_VISIBLE_ITEM_4_PAD = &H77 + EUnitFields.UNIT_END

            '308
            PLAYER_VISIBLE_ITEM_5_CREATOR = &H78 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_5_0 = &H7A + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_5_PROPERTIES = &H82 + EUnitFields.UNIT_END
            '319
            PLAYER_VISIBLE_ITEM_5_PAD = &H83 + EUnitFields.UNIT_END

            '320
            PLAYER_VISIBLE_ITEM_6_CREATOR = &H84 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_6_0 = &H86 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_6_PROPERTIES = &H8E + EUnitFields.UNIT_END
            '331
            PLAYER_VISIBLE_ITEM_6_PAD = &H8F + EUnitFields.UNIT_END

            '332
            PLAYER_VISIBLE_ITEM_7_CREATOR = &H90 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_7_0 = &H92 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_7_PROPERTIES = &H9A + EUnitFields.UNIT_END
            '343
            PLAYER_VISIBLE_ITEM_7_PAD = &H9B + EUnitFields.UNIT_END

            '344
            PLAYER_VISIBLE_ITEM_8_CREATOR = &H9C + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_8_0 = &H9E + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_8_PROPERTIES = &HA6 + EUnitFields.UNIT_END
            '355
            PLAYER_VISIBLE_ITEM_8_PAD = &HA7 + EUnitFields.UNIT_END

            '356
            PLAYER_VISIBLE_ITEM_9_CREATOR = &HA8 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_9_0 = &HAA + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_9_PROPERTIES = &HB2 + EUnitFields.UNIT_END
            '367
            PLAYER_VISIBLE_ITEM_9_PAD = &HB3 + EUnitFields.UNIT_END

            '368
            PLAYER_VISIBLE_ITEM_10_CREATOR = &HB4 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_10_0 = &HB6 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_10_PROPERTIES = &HBE + EUnitFields.UNIT_END
            '379
            PLAYER_VISIBLE_ITEM_10_PAD = &HBF + EUnitFields.UNIT_END

            '380
            PLAYER_VISIBLE_ITEM_11_CREATOR = &HC0 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_11_0 = &HC2 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_11_PROPERTIES = &HCA + EUnitFields.UNIT_END
            '391
            PLAYER_VISIBLE_ITEM_11_PAD = &HCB + EUnitFields.UNIT_END

            '392
            PLAYER_VISIBLE_ITEM_12_CREATOR = &HCC + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_12_0 = &HCE + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_12_PROPERTIES = &HD6 + EUnitFields.UNIT_END
            '403
            PLAYER_VISIBLE_ITEM_12_PAD = &HD7 + EUnitFields.UNIT_END

            '404
            PLAYER_VISIBLE_ITEM_13_CREATOR = &HD8 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_13_0 = &HDA + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_13_PROPERTIES = &HE2 + EUnitFields.UNIT_END
            '415
            PLAYER_VISIBLE_ITEM_13_PAD = &HE3 + EUnitFields.UNIT_END

            '416
            PLAYER_VISIBLE_ITEM_14_CREATOR = &HE4 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_14_0 = &HE6 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_14_PROPERTIES = &HEE + EUnitFields.UNIT_END
            '427
            PLAYER_VISIBLE_ITEM_14_PAD = &HEF + EUnitFields.UNIT_END

            '428
            PLAYER_VISIBLE_ITEM_15_CREATOR = &HF0 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_15_0 = &HF2 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_15_PROPERTIES = &HFA + EUnitFields.UNIT_END
            '439
            PLAYER_VISIBLE_ITEM_15_PAD = &HFB + EUnitFields.UNIT_END

            '440
            PLAYER_VISIBLE_ITEM_16_CREATOR = &HFC + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_16_0 = &HFE + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_16_PROPERTIES = &H106 + EUnitFields.UNIT_END
            '451
            PLAYER_VISIBLE_ITEM_16_PAD = &H107 + EUnitFields.UNIT_END

            '452
            PLAYER_VISIBLE_ITEM_17_CREATOR = &H108 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_17_0 = &H10A + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_17_PROPERTIES = &H112 + EUnitFields.UNIT_END
            '463
            PLAYER_VISIBLE_ITEM_17_PAD = &H113 + EUnitFields.UNIT_END

            '464
            PLAYER_VISIBLE_ITEM_18_CREATOR = &H114 + EUnitFields.UNIT_END
            '466 ranged
            PLAYER_VISIBLE_ITEM_18_0 = &H116 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_18_PROPERTIES = &H11E + EUnitFields.UNIT_END
            '475
            PLAYER_VISIBLE_ITEM_18_PAD = &H11F + EUnitFields.UNIT_END

            '476
            PLAYER_VISIBLE_ITEM_19_CREATOR = &H120 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_19_0 = &H122 + EUnitFields.UNIT_END
            PLAYER_VISIBLE_ITEM_19_PROPERTIES = &H12A + EUnitFields.UNIT_END
            '487
            PLAYER_VISIBLE_ITEM_19_PAD = &H12B + EUnitFields.UNIT_END

            PLAYER_FIELD_INV_SLOT_HEAD = &H12C + EUnitFields.UNIT_END

            PLAYER_FIELD_PACK_SLOT_1 = &H15A + EUnitFields.UNIT_END
            PLAYER_FIELD_PACK_SLOT_2 = &H15B + EUnitFields.UNIT_END

            PLAYER_FIELD_BANK_SLOT_1 = &H17A + EUnitFields.UNIT_END
            PLAYER_FIELD_BANKBAG_SLOT_1 = &H1AA + EUnitFields.UNIT_END
            PLAYER_FIELD_VENDORBUYBACK_SLOT_1 = &H1B6 + EUnitFields.UNIT_END

            PLAYER_FARSIGHT = &H1CE + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_COMBO_TARGET = &H1D0 + EUnitFields.UNIT_END + 64
            PLAYER_XP = &H1D2 + EUnitFields.UNIT_END + 64
            PLAYER_NEXT_LEVEL_XP = &H1D3 + EUnitFields.UNIT_END + 64
            PLAYER_SKILL_INFO_START = &H1D4 + EUnitFields.UNIT_END + 64

            PLAYER_CHARACTER_POINTS1 = &H354 + EUnitFields.UNIT_END + 64
            PLAYER_CHARACTER_POINTS2 = &H355 + EUnitFields.UNIT_END + 64
            PLAYER_TRACK_CREATURES = &H356 + EUnitFields.UNIT_END + 64
            PLAYER_TRACK_RESOURCES = &H357 + +EUnitFields.UNIT_END + 64
            PLAYER_BLOCK_PERCENTAGE = &H358 + EUnitFields.UNIT_END + 64
            PLAYER_DODGE_PERCENTAGE = &H359 + EUnitFields.UNIT_END + 64
            PLAYER_PARRY_PERCENTAGE = &H35A + EUnitFields.UNIT_END + 64
            PLAYER_CRIT_PERCENTAGE = &H35B + EUnitFields.UNIT_END + 64
            PLAYER_RANGED_CRIT_PERCENTAGE = &H35C + EUnitFields.UNIT_END + 64
            PLAYER_EXPLORED_ZONES_1 = &H35D + EUnitFields.UNIT_END + 64
            PLAYER_REST_STATE_EXPERIENCE = &H39D + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_COINAGE = &H39E + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_POSSTAT0 = &H39F + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_POSSTAT1 = &H3A0 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_POSSTAT2 = &H3A1 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_POSSTAT3 = &H3A2 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_POSSTAT4 = &H3A3 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_NEGSTAT0 = &H3A4 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_NEGSTAT1 = &H3A5 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_NEGSTAT2 = &H3A6 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_NEGSTAT3 = &H3A7 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_NEGSTAT4 = &H3A8 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_RESISTANCEBUFFMODSPOSITIVE = &H3A9 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_RESISTANCEBUFFMODSPOSITIVE_01 = &H3AA + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_RESISTANCEBUFFMODSPOSITIVE_02 = &H3AB + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_RESISTANCEBUFFMODSPOSITIVE_03 = &H3AC + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_RESISTANCEBUFFMODSPOSITIVE_04 = &H3AD + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_RESISTANCEBUFFMODSPOSITIVE_05 = &H3AE + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_RESISTANCEBUFFMODSPOSITIVE_06 = &H3AF + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_RESISTANCEBUFFMODSNEGATIVE = &H3B0 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_RESISTANCEBUFFMODSNEGATIVE_01 = &H3B1 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_RESISTANCEBUFFMODSNEGATIVE_02 = &H3B2 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_RESISTANCEBUFFMODSNEGATIVE_03 = &H3B3 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_RESISTANCEBUFFMODSNEGATIVE_04 = &H3B4 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_RESISTANCEBUFFMODSNEGATIVE_05 = &H3B5 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_RESISTANCEBUFFMODSNEGATIVE_06 = &H3B6 + EUnitFields.UNIT_END + 64
            'float 1.0
            PLAYER_FIELD_MOD_DAMAGE_DONE_POS = &H3B7 + EUnitFields.UNIT_END + 64
            'float 1.0
            PLAYER_FIELD_MOD_DAMAGE_DONE_NEG = &H3BE + EUnitFields.UNIT_END + 64
            'float 1.0
            PLAYER_FIELD_MOD_DAMAGE_DONE_PCT = &H3C5 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_BYTES = &H3CC + EUnitFields.UNIT_END + 64
            PLAYER_AMMO_ID = &H3CD + EUnitFields.UNIT_END + 64
            PLAYER_SELF_RES_SPELL = &H3CE + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_PVP_MEDALS = &H3CF + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_BUYBACK_PRICE_1 = &H3D0 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_BUYBACK_TIMESTAMP_1 = &H3DC + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_SESSION_KILLS = &H3E8 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_YESTERDAY_KILLS = &H3E9 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_LAST_WEEK_KILLS = &H3EA + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_THIS_WEEK_KILLS = &H3EB + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_THIS_WEEK_CONTRIBUTION = &H3EC + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_LIFETIME_HONORABLE_KILLS = &H3ED + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_LIFETIME_DISHONORABLE_KILLS = &H3EE + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_YESTERDAY_CONTRIBUTION = &H3EF + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_LAST_WEEK_CONTRIBUTION = &H3F0 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_LAST_WEEK_RANK = &H3F1 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_BYTES2 = &H3F2 + EUnitFields.UNIT_END + 64
            PLAYER_FIELD_WATCHED_FACTION_INDEX = &H3F3 + EUnitFields.UNIT_END + 64
            PLAYER_END = &H3F4 + EUnitFields.UNIT_END + 64
        End Enum
        Public Enum EGameObjectFields
            OBJECT_FIELD_CREATED_BY = 6         '  2  UINT64
            GAMEOBJECT_DISPLAYID = 8            '  1  UINT32
            GAMEOBJECT_FLAGS = 9                '  1  UINT32
            GAMEOBJECT_ROTATION = 10            '  4  ROTATION
            GAMEOBJECT_STATE = 14               '  1  UINT32
            GAMEOBJECT_TIMESTAMP = 15           '  1  UINT32
            GAMEOBJECT_POS_X = 16               '  1  FLOAT
            GAMEOBJECT_POS_Y = 17               '  1  FLOAT
            GAMEOBJECT_POS_Z = 18               '  1  FLOAT
            GAMEOBJECT_FACING = 19              '  1  FLOAT
            GAMEOBJECT_DYN_FLAGS = 20           '  1  UINT32
            GAMEOBJECT_FACTION = 21             '  1  UINT32
            GAMEOBJECT_TYPE_ID = 22             '  1  UINT32
            GAMEOBJECT_LEVEL = 23               '  1  UINT32
            GAMEOBJECT_END = 24                 '  0  INTERNALMARKER
        End Enum
        Public Enum EDynamicObjectFields
            DYNAMICOBJECT_CASTER = 6        '  2  UINT64
            DYNAMICOBJECT_BYTES = 8         '  1  UINT32
            DYNAMICOBJECT_SPELLID = 9       '  1  UINT32
            DYNAMICOBJECT_RADIUS = 10       '  1  UINT32
            DYNAMICOBJECT_POS_X = 11        '  1  UINT32
            DYNAMICOBJECT_POS_Y = 12        '  1  UINT32
            DYNAMICOBJECT_POS_Z = 13        '  1  UINT32
            DYNAMICOBJECT_FACING = 14       '  1  UINT32
            DYNAMICOBJECT_PAD = 15          '  1  UINT32
            DYNAMICOBJECT_END = 16          '  0  INTERNALMARKER
        End Enum
        Public Enum ECorpseFields
            CORPSE_FIELD_OWNER = 6          '  2  UINT64
            CORPSE_FIELD_FACING = 8         '  1  FLOAT
            CORPSE_FIELD_POS_X = 9          '  1  FLOAT
            CORPSE_FIELD_POS_Y = 10         '  1  FLOAT
            CORPSE_FIELD_POS_Z = 11         '  1  FLOAT
            CORPSE_FIELD_DISPLAY_ID = 12    '  1  UINT32
            CORPSE_FIELD_ITEM = 13          '  19 CORPSEITEMS
            CORPSE_FIELD_BYTES_1 = 32       '  1  UINT32
            CORPSE_FIELD_BYTES_2 = 33       '  1  UINT32
            CORPSE_FIELD_GUILD = 34         '  1  UINT32
            CORPSE_FIELD_FLAGS = 35         '  1  UINT32
            CORPSE_FIELD_DYNAMIC_FLAGS = 36 '  1  UINT32
            CORPSE_FIELD_PAD = 37           '  1  UINT32
            CORPSE_END = 38                 '  0  INTERNALMARKER
        End Enum


    End Class
End Namespace
