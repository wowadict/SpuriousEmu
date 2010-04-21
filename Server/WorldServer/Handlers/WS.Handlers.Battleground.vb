' 
' Copyright (C) 2008-2010 Spurious <http://SpuriousEmu.com>
'
' This program is free software; you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation; either version 2 of the License, or
' (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program; if not, write to the Free Software
' Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
'
Imports System.Diagnostics
Imports Spurious.Common.BaseWriter

Public Module WS_Handlers_Battleground

    Public Sub On_CMSG_BATTLEMASTER_HELLO(ByRef packet As PacketClass, ByRef Client As ClientClass)
        If (packet.Data.Length - 1) < 13 Then Exit Sub
        packet.GetInt16()
        Dim GUID As ULong = packet.GetUInt64

        Log.WriteLine(LogType.DEBUG, "[{0}:{1}] CMSG_BATTLEMASTER_HELLO [{2:X}]", Client.IP, Client.Port, GUID)

        If WORLD_CREATUREs.ContainsKey(GUID) = False Then Exit Sub
        If (WORLD_CREATUREs(GUID).CreatureInfo.cNpcFlags And NPCFlags.UNIT_NPC_FLAG_BATTLEMASTER) = 0 Then Exit Sub
        If Battlemasters.ContainsKey(WORLD_CREATUREs(GUID).ID) = False Then Exit Sub

        Dim BGType As Byte = Battlemasters(WORLD_CREATUREs(GUID).ID)
        If Battlegrounds.ContainsKey(BGType) = False Then Exit Sub

        If Battlegrounds(BGType).MinLevel > Client.Character.Level OrElse Battlegrounds(BGType).MaxLevel < Client.Character.Level Then
            SendMessageNotification(Client, "You don't meet Battleground level requirements")
            Exit Sub
        End If

        'DONE: Send list
        Dim response As New PacketClass(OPCODES.SMSG_BATTLEFIELD_LIST)
        response.AddUInt64(Client.Character.GUID)
        response.AddInt32(BGType)

        If BGType = 6 Then          'Arenas
            response.AddInt8(5)     'Unk
            response.AddInt32(0)    'Unk
        Else
            Dim Battlegrounds As List(Of Integer) = WS.Cluster.BattlefieldList(BGType)
            response.AddInt8(0)                     'Unk
            response.AddInt32(Battlegrounds.Count)  'Number of BG Instances

            For Each Instance As Integer In Battlegrounds
                response.AddInt32(Instance)
            Next
        End If

        Client.Send(response)
        response.Dispose()
    End Sub

    'Not Implement:
    'MSG_BATTLEGROUND_PLAYER_POSITIONS
    'MSG_PVP_LOG_DATA
    'CMSG_AREA_SPIRIT_HEALER_QUERY
    'CMSG_AREA_SPIRIT_HEALER_QUEUE
    'CMSG_REPORT_PVP_AFK

    ' Enums  Set  Be  Hex00010

    Public GlobalMembers As Byte
    Sub New()


        Dim Buff_Entries As UInt32() = {BattleGroundBuffObjects.BG_OBJECTID_SPEEDBUFF_ENTRY, BattleGroundBuffObjects.BG_OBJECTID_REGENBUFF_ENTRY, BattleGroundBuffObjects.BG_OBJECTID_BERSERKERBUFF_ENTRY}
    End Sub
End Module


Public Enum BattleGroundSounds As Integer
    SOUND_HORDE_WINS = 8454
    SOUND_ALLIANCE_WINS = 8455
    SOUND_BG_START = 3439
    SOUND_BG_START_L70ETC = 11803
End Enum

Public Enum BattleGroundQuests As Integer
    SPELL_WS_QUEST_REWARD = 43483
    SPELL_AB_QUEST_REWARD = 43484
    SPELL_AV_QUEST_REWARD = 43475
    SPELL_AV_QUEST_KILLED_BOSS = 23658
    SPELL_EY_QUEST_REWARD = 43477
    SPELL_SA_QUEST_REWARD = 61213
    SPELL_AB_QUEST_REWARD_4_BASES = 24061
    SPELL_AB_QUEST_REWARD_5_BASES = 24064
End Enum

Public Enum BattleGroundMarks As Integer
    SPELL_WS_MARK_LOSER = 24950
    SPELL_WS_MARK_WINNER = 24951
    SPELL_AB_MARK_LOSER = 24952
    SPELL_AB_MARK_WINNER = 24953
    SPELL_AV_MARK_LOSER = 24954
    SPELL_AV_MARK_WINNER = 24955
    SPELL_SA_MARK_WINNER = 61160
    SPELL_SA_MARK_LOSER = 61159
    ITEM_AV_MARK_OF_HONOR = 20560
    ITEM_WS_MARK_OF_HONOR = 20558
    ITEM_AB_MARK_OF_HONOR = 20559
    ITEM_EY_MARK_OF_HONOR = 29024
    ITEM_SA_MARK_OF_HONOR = 42425
End Enum

Public Enum BattleGroundMarksCount As Integer
    ITEM_WINNER_COUNT = 3
    ITEM_LOSER_COUNT = 1
End Enum

Public Enum BattleGroundCreatures As Integer
    BG_CREATURE_ENTRY_A_SPIRITGUIDE = 13116
    ' alliance
    BG_CREATURE_ENTRY_H_SPIRITGUIDE = 13117
    ' horde
End Enum

Public Enum BattleGroundSpells As Integer
    SPELL_WAITING_FOR_RESURRECT = 2584
    ' Waiting to Resurrect
    SPELL_SPIRIT_HEAL_CHANNEL = 22011
    ' Spirit Heal Channel
    SPELL_SPIRIT_HEAL = 22012
    ' Spirit Heal
    SPELL_RESURRECTION_VISUAL = 24171
    ' Resurrection Impact Visual
    SPELL_ARENA_PREPARATION = 32727
    ' use this one, 32728 not correct
    SPELL_ALLIANCE_GOLD_FLAG = 32724
    SPELL_ALLIANCE_GREEN_FLAG = 32725
    SPELL_HORDE_GOLD_FLAG = 35774
    SPELL_HORDE_GREEN_FLAG = 35775
    SPELL_PREPARATION = 44521
    ' Preparation
    SPELL_SPIRIT_HEAL_MANA = 44535
    ' Spirit Heal
    SPELL_RECENTLY_DROPPED_FLAG = 42792
    ' Recently Dropped Flag
    SPELL_AURA_PLAYER_INACTIVE = 43681
    ' Inactive
End Enum

Public Enum BattleGroundTimeIntervals As Integer
    RESURRECTION_INTERVAL = 30000
    ' ms
    'REMIND_INTERVAL                 = 10000,                // ms
    INVITATION_REMIND_TIME = 20000
    ' ms
    INVITE_ACCEPT_WAIT_TIME = 40000
    ' ms
    TIME_TO_AUTOREMOVE = 120000
    ' ms
    MAX_OFFLINE_TIME = 300
    ' secs
    RESPAWN_ONE_DAY = 86400
    ' secs
    RESPAWN_IMMEDIATELY = 0
    ' secs
    BUFF_RESPAWN_TIME = 180
    ' secs
End Enum

Public Enum BattleGroundStartTimeIntervals As Integer
    BG_START_DELAY_2M = 120000
    ' ms (2 minutes)
    BG_START_DELAY_1M = 60000
    ' ms (1 minute)
    BG_START_DELAY_30S = 30000
    ' ms (30 seconds)
    BG_START_DELAY_15S = 15000
    ' ms (15 seconds) Used only in arena
    BG_START_DELAY_NONE = 0
    ' ms
End Enum

Public Enum BattleGroundBuffObjects As Integer
    BG_OBJECTID_SPEEDBUFF_ENTRY = 179871
    BG_OBJECTID_REGENBUFF_ENTRY = 179904
    BG_OBJECTID_BERSERKERBUFF_ENTRY = 179905
End Enum

Public Enum BattleGroundStatus As Integer
    STATUS_NONE = 0
    ' first status, should mean bg is not instance
    STATUS_WAIT_QUEUE = 1
    ' means bg is empty and waiting for queue
    STATUS_WAIT_JOIN = 2
    ' this means, that BG has already started and it is waiting for more players
    STATUS_IN_PROGRESS = 3
    ' means bg is running
    STATUS_WAIT_LEAVE = 4
    ' means some faction has won BG and it is ending
End Enum

Public Class BattleGroundPlayer
    'Public OfflineRemoveTime As time_t
    ' for tracking and removing offline players from queue after 5 minutes
    Public Team As UInt32
    ' Player's team
End Class

Public Class BattleGroundObjectInfo
    Public Sub New()
        [object] = Nothing
        timer = 0
        spellid = 0
    End Sub

    Public [object] As BaseObject
    Public timer As Int32
    Public spellid As UInt32
End Class

' handle the queue types and bg types separately to enable joining queue for different sized arenas at the same time
Public Enum BattleGroundQueueTypeId As Integer
    BATTLEGROUND_QUEUE_NONE = 0
    BATTLEGROUND_QUEUE_AV = 1
    BATTLEGROUND_QUEUE_WS = 2
    BATTLEGROUND_QUEUE_AB = 3
    BATTLEGROUND_QUEUE_EY = 4
    BATTLEGROUND_QUEUE_SA = 5
    BATTLEGROUND_QUEUE_IC = 6
    BATTLEGROUND_QUEUE_2v2 = 7
    BATTLEGROUND_QUEUE_3v3 = 8
    BATTLEGROUND_QUEUE_5v5 = 9
    MAX_BATTLEGROUND_QUEUE_TYPES
End Enum

Public Enum ScoreType As Integer
    SCORE_KILLING_BLOWS = 1
    SCORE_DEATHS = 2
    SCORE_HONORABLE_KILLS = 3
    SCORE_BONUS_HONOR = 4
    'EY, but in MSG_PVP_LOG_DATA opcode!
    SCORE_DAMAGE_DONE = 5
    SCORE_HEALING_DONE = 6
    'WS
    SCORE_FLAG_CAPTURES = 7
    SCORE_FLAG_RETURNS = 8
    'AB
    SCORE_BASES_ASSAULTED = 9
    SCORE_BASES_DEFENDED = 10
    'AV
    SCORE_GRAVEYARDS_ASSAULTED = 11
    SCORE_GRAVEYARDS_DEFENDED = 12
    SCORE_TOWERS_ASSAULTED = 13
    SCORE_TOWERS_DEFENDED = 14
    SCORE_MINES_CAPTURED = 15
    SCORE_LEADERS_KILLED = 16
    SCORE_SECONDARY_OBJECTIVES = 17
    'SOTA
    SCORE_DESTROYED_DEMOLISHER = 18
    SCORE_DESTROYED_WALL = 19
End Enum

Public Enum ArenaType As Integer
    ARENA_TYPE_2v2 = 2
    ARENA_TYPE_3v3 = 3
    ARENA_TYPE_5v5 = 5
End Enum

Public Enum BattleGroundType As Integer
    TYPE_BATTLEGROUND = 3
    TYPE_ARENA = 4
End Enum

Public Enum BattleGroundWinner As Integer
    WINNER_HORDE = 0
    WINNER_ALLIANCE = 1
    WINNER_NONE = 2
End Enum

Public Enum BattleGroundTeamId As Integer
    BG_TEAM_ALLIANCE = 0
    BG_TEAM_HORDE = 1
End Enum
'#Define BG_TEAMS_COUNT

Public Enum BattleGroundStartingEvents As Integer
    BG_STARTING_EVENT_NONE = &H0
    BG_STARTING_EVENT_1 = &H1
    BG_STARTING_EVENT_2 = &H2
    BG_STARTING_EVENT_3 = &H4
    BG_STARTING_EVENT_4 = &H8
End Enum

Public Enum BattleGroundStartingEventsIds As Integer
    BG_STARTING_EVENT_FIRST = 0
    BG_STARTING_EVENT_SECOND = 1
    BG_STARTING_EVENT_THIRD = 2
    BG_STARTING_EVENT_FOURTH = 3
End Enum
'#Define BG_STARTING_EVENT_COUNT

Public Enum GroupJoinBattlegroundResult As Integer
    ERR_GROUP_JOIN_BATTLEGROUND_FAIL = 0
    ' Your group has joined a battleground queue, but you are not eligible (showed for non existing BattlemasterList.dbc indexes)
    ERR_BATTLEGROUND_NONE = -1
    ' not show anything
    ERR_GROUP_JOIN_BATTLEGROUND_DESERTERS = -2
    ' You cannot join the battleground yet because you or one of your party members is flagged as a Deserter.
    ERR_ARENA_TEAM_PARTY_SIZE = -3
    ' Incorrect party size for this arena.
    ERR_BATTLEGROUND_TOO_MANY_QUEUES = -4
    ' You can only be queued for 2 battles at once
    ERR_BATTLEGROUND_CANNOT_QUEUE_FOR_RATED = -5
    ' You cannot queue for a rated match while queued for other battles
    ERR_BATTLEDGROUND_QUEUED_FOR_RATED = -6
    ' You cannot queue for another battle while queued for a rated arena match
    ERR_BATTLEGROUND_TEAM_LEFT_QUEUE = -7
    ' Your team has left the arena queue
    ERR_BATTLEGROUND_NOT_IN_BATTLEGROUND = -8
    ' You can't do that in a battleground.
    ERR_BATTLEGROUND_JOIN_XP_GAIN = -9
    ' wtf, doesn't exist in client...
    ERR_BATTLEGROUND_JOIN_RANGE_INDEX = -10
    ' Cannot join the queue unless all members of your party are in the same battleground level range.
    ERR_BATTLEGROUND_JOIN_TIMED_OUT = -11
    ' %s was unavailable to join the queue. (uint64 guid exist in client cache)
    ERR_BATTLEGROUND_JOIN_FAILED = -12
    ' Join as a group failed (uint64 guid doesn't exist in client cache)
    ERR_LFG_CANT_USE_BATTLEGROUND = -13
    ' You cannot queue for a battleground or arena while using the dungeon system.
    ERR_IN_RANDOM_BG = -14
    ' Can't do that while in a Random Battleground queue.
    ERR_IN_NON_RANDOM_BG = -15
    ' Can't queue for Random Battleground while in another Battleground queue.
End Enum

Public Class BattleGroundScore
    Public Sub New()
        KillingBlows = 0
        Deaths = 0
        HonorableKills = 0
        BonusHonor = 0
        DamageDone = 0
        HealingDone = 0
    End Sub
    Public Overridable Sub Dispose()
        'virtual destructor is used when deleting score from scores map
    End Sub

    Public KillingBlows As UInt32
    Public Deaths As UInt32
    Public HonorableKills As UInt32
    Public BonusHonor As UInt32
    Public DamageDone As UInt32
    Public HealingDone As UInt32
End Class

Public Enum BGHonorMode As Integer
    BG_NORMAL = 0
    BG_HOLIDAY
    BG_HONOR_MODE_NUM
End Enum


'End Module