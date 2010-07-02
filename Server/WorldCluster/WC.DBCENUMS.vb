Module WC

    ' Client expected level limitation, like as used in DBC item max levels for "until max player level"
    ' use as default max player level, must be fit max level for used client
    ' also see MAX_LEVEL and STRONG_MAX_LEVEL define
#Const DEFAULT_MAX_LEVEL = 80

    ' client supported max level for player/pets/etc. Avoid overflow or client stability affected.
    ' also see GT_MAX_LEVEL define
#Const MAX_LEVEL = 80

    ' Server side limitation. Base at used code requirements.
    ' also see MAX_LEVEL and GT_MAX_LEVEL define
#Const STRONG_MAX_LEVEL = 255

    Public Enum BattleGroundBracketId As Integer
        ' bracketId for level ranges
        BG_BRACKET_ID_FIRST = 0
        BG_BRACKET_ID_LAST = 15
    End Enum

    ' must be max value in PvPDificulty slot+1
#Const MAX_BATTLEGROUND_BRACKETS = 16

    Public Enum AreaTeams As Integer
        AREATEAM_NONE = 0
        AREATEAM_ALLY = 2
        AREATEAM_HORDE = 4
    End Enum

    Public Enum AchievementFactionFlags As Integer
        ACHIEVEMENT_FACTION_FLAG_HORDE = &H0
        ACHIEVEMENT_FACTION_FLAG_ALLIANCE = &H1
    End Enum

    Public Enum AchievementFlags As Integer
        ACHIEVEMENT_FLAG_COUNTER = &H1
        ' Just count statistic (never stop and complete)
        ACHIEVEMENT_FLAG_UNK2 = &H2
        ' not used
        ACHIEVEMENT_FLAG_STORE_MAX_VALUE = &H4
        ' Store only max value? used only in "Reach level xx"
        ACHIEVEMENT_FLAG_SUMM = &H8
        ' Use summ criteria value from all reqirements (and calculate max value)
        ACHIEVEMENT_FLAG_MAX_USED = &H10
        ' Show max criteria (and calculate max value ??)
        ACHIEVEMENT_FLAG_REQ_COUNT = &H20
        ' Use not zero req count (and calculate max value)
        ACHIEVEMENT_FLAG_AVERAGE = &H40
        ' Show as average value (value / time_in_days) depend from other flag (by def use last criteria value)
        ACHIEVEMENT_FLAG_BAR = &H80
        ' Show as progress bar (value / max vale) depend from other flag (by def use last criteria value)
        ACHIEVEMENT_FLAG_REALM_FIRST_REACH = &H100
        ACHIEVEMENT_FLAG_REALM_FIRST_KILL = &H200
    End Enum

    Public Enum AchievementCriteriaCondition As Integer
        ACHIEVEMENT_CRITERIA_CONDITION_NONE = 0
        ACHIEVEMENT_CRITERIA_CONDITION_NO_DEATH = 1
        ACHIEVEMENT_CRITERIA_CONDITION_UNK1 = 2
        ' only used in "Complete a daily quest every day for five consecutive days"
        ACHIEVEMENT_CRITERIA_CONDITION_MAP = 3
        ' requires you to be on specific map
        ACHIEVEMENT_CRITERIA_CONDITION_NO_LOOSE = 4
        ' only used in "Win 10 arenas without losing"
        ACHIEVEMENT_CRITERIA_CONDITION_UNK2 = 9
        ' unk
        ACHIEVEMENT_CRITERIA_CONDITION_UNK3 = 13
        ' unk
    End Enum

    Public Enum AchievementCriteriaCompletionFlags As Integer
        ACHIEVEMENT_CRITERIA_FLAG_SHOW_PROGRESS_BAR = &H1
        ' Show progress as bar
        ACHIEVEMENT_CRITERIA_FLAG_HIDE_CRITERIA = &H2
        ' Not show criteria in client
        ACHIEVEMENT_CRITERIA_FLAG_UNK3 = &H4
        ' BG related??
        ACHIEVEMENT_CRITERIA_FLAG_UNK4 = &H8
        ACHIEVEMENT_CRITERIA_FLAG_UNK5 = &H10
        ' not used
        ACHIEVEMENT_CRITERIA_FLAG_MONEY_COUNTER = &H20
        ' Displays counter as money
    End Enum

    Public Enum AchievementCriteriaGroupFlags As Integer
        ' you mustn't be in a group while fulfilling this achievement
        ACHIEVEMENT_CRITERIA_GROUP_NOT_IN_GROUP = 2
    End Enum

    Public Enum AchievementCriteriaTypes As Integer
        ACHIEVEMENT_CRITERIA_TYPE_KILL_CREATURE = 0
        ACHIEVEMENT_CRITERIA_TYPE_WIN_BG = 1
        ACHIEVEMENT_CRITERIA_TYPE_REACH_LEVEL = 5
        ACHIEVEMENT_CRITERIA_TYPE_REACH_SKILL_LEVEL = 7
        ACHIEVEMENT_CRITERIA_TYPE_COMPLETE_ACHIEVEMENT = 8
        ACHIEVEMENT_CRITERIA_TYPE_COMPLETE_QUEST_COUNT = 9
        ' you have to complete a daily quest x times in a row
        ACHIEVEMENT_CRITERIA_TYPE_COMPLETE_DAILY_QUEST_DAILY = 10
        ACHIEVEMENT_CRITERIA_TYPE_COMPLETE_QUESTS_IN_ZONE = 11
        ACHIEVEMENT_CRITERIA_TYPE_DAMAGE_DONE = 13
        ACHIEVEMENT_CRITERIA_TYPE_COMPLETE_DAILY_QUEST = 14
        ACHIEVEMENT_CRITERIA_TYPE_COMPLETE_BATTLEGROUND = 15
        ACHIEVEMENT_CRITERIA_TYPE_DEATH_AT_MAP = 16
        ACHIEVEMENT_CRITERIA_TYPE_DEATH = 17
        ACHIEVEMENT_CRITERIA_TYPE_DEATH_IN_DUNGEON = 18
        ACHIEVEMENT_CRITERIA_TYPE_COMPLETE_RAID = 19
        ACHIEVEMENT_CRITERIA_TYPE_KILLED_BY_CREATURE = 20
        ACHIEVEMENT_CRITERIA_TYPE_KILLED_BY_PLAYER = 23
        ACHIEVEMENT_CRITERIA_TYPE_FALL_WITHOUT_DYING = 24
        ACHIEVEMENT_CRITERIA_TYPE_DEATHS_FROM = 26
        ACHIEVEMENT_CRITERIA_TYPE_COMPLETE_QUEST = 27
        ACHIEVEMENT_CRITERIA_TYPE_BE_SPELL_TARGET = 28
        ACHIEVEMENT_CRITERIA_TYPE_CAST_SPELL = 29
        ACHIEVEMENT_CRITERIA_TYPE_BG_OBJECTIVE_CAPTURE = 30
        ACHIEVEMENT_CRITERIA_TYPE_HONORABLE_KILL_AT_AREA = 31
        ACHIEVEMENT_CRITERIA_TYPE_WIN_ARENA = 32
        ACHIEVEMENT_CRITERIA_TYPE_PLAY_ARENA = 33
        ACHIEVEMENT_CRITERIA_TYPE_LEARN_SPELL = 34
        ' TODO: this criteria has additional conditions which can not be found in the dbcs
        ACHIEVEMENT_CRITERIA_TYPE_HONORABLE_KILL = 35
        ACHIEVEMENT_CRITERIA_TYPE_OWN_ITEM = 36
        ' TODO: the archievements 1162 and 1163 requires a special rating which can't be found in the dbc
        ACHIEVEMENT_CRITERIA_TYPE_WIN_RATED_ARENA = 37
        ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_TEAM_RATING = 38
        ACHIEVEMENT_CRITERIA_TYPE_REACH_TEAM_RATING = 39
        ACHIEVEMENT_CRITERIA_TYPE_LEARN_SKILL_LEVEL = 40
        ACHIEVEMENT_CRITERIA_TYPE_USE_ITEM = 41
        ACHIEVEMENT_CRITERIA_TYPE_LOOT_ITEM = 42
        ACHIEVEMENT_CRITERIA_TYPE_EXPLORE_AREA = 43
        ACHIEVEMENT_CRITERIA_TYPE_OWN_RANK = 44
        ACHIEVEMENT_CRITERIA_TYPE_BUY_BANK_SLOT = 45
        ACHIEVEMENT_CRITERIA_TYPE_GAIN_REPUTATION = 46
        ACHIEVEMENT_CRITERIA_TYPE_GAIN_EXALTED_REPUTATION = 47
        ' noted: rewarded as soon as the player payed, not at taking place at the seat
        ACHIEVEMENT_CRITERIA_TYPE_VISIT_BARBER_SHOP = 48
        ACHIEVEMENT_CRITERIA_TYPE_EQUIP_EPIC_ITEM = 49
        ' TODO: itemlevel is mentioned in text but not present in dbc
        ACHIEVEMENT_CRITERIA_TYPE_ROLL_NEED_ON_LOOT = 50
        ACHIEVEMENT_CRITERIA_TYPE_ROLL_GREED_ON_LOOT = 51
        ACHIEVEMENT_CRITERIA_TYPE_HK_CLASS = 52
        ACHIEVEMENT_CRITERIA_TYPE_HK_RACE = 53
        ACHIEVEMENT_CRITERIA_TYPE_DO_EMOTE = 54
        ACHIEVEMENT_CRITERIA_TYPE_HEALING_DONE = 55
        ' TODO: in some cases map not present, and in some cases need do without die
        ACHIEVEMENT_CRITERIA_TYPE_GET_KILLING_BLOWS = 56
        ACHIEVEMENT_CRITERIA_TYPE_EQUIP_ITEM = 57
        ACHIEVEMENT_CRITERIA_TYPE_MONEY_FROM_VENDORS = 59
        ACHIEVEMENT_CRITERIA_TYPE_GOLD_SPENT_FOR_TALENTS = 60
        ACHIEVEMENT_CRITERIA_TYPE_NUMBER_OF_TALENT_RESETS = 61
        ACHIEVEMENT_CRITERIA_TYPE_MONEY_FROM_QUEST_REWARD = 62
        ACHIEVEMENT_CRITERIA_TYPE_GOLD_SPENT_FOR_TRAVELLING = 63
        ACHIEVEMENT_CRITERIA_TYPE_GOLD_SPENT_AT_BARBER = 65
        ACHIEVEMENT_CRITERIA_TYPE_GOLD_SPENT_FOR_MAIL = 66
        ACHIEVEMENT_CRITERIA_TYPE_LOOT_MONEY = 67
        ACHIEVEMENT_CRITERIA_TYPE_USE_GAMEOBJECT = 68
        ACHIEVEMENT_CRITERIA_TYPE_BE_SPELL_TARGET2 = 69
        ACHIEVEMENT_CRITERIA_TYPE_SPECIAL_PVP_KILL = 70
        ACHIEVEMENT_CRITERIA_TYPE_FISH_IN_GAMEOBJECT = 72
        ' TODO: title id is not mentioned in dbc
        ACHIEVEMENT_CRITERIA_TYPE_EARNED_PVP_TITLE = 74
        ACHIEVEMENT_CRITERIA_TYPE_LEARN_SKILLLINE_SPELLS = 75
        ACHIEVEMENT_CRITERIA_TYPE_WIN_DUEL = 76
        ACHIEVEMENT_CRITERIA_TYPE_LOSE_DUEL = 77
        ' TODO: creature type (demon, undead etc.) is not stored in dbc
        ACHIEVEMENT_CRITERIA_TYPE_KILL_CREATURE_TYPE = 78
        ACHIEVEMENT_CRITERIA_TYPE_GOLD_EARNED_BY_AUCTIONS = 80
        ACHIEVEMENT_CRITERIA_TYPE_CREATE_AUCTION = 82
        ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_AUCTION_BID = 83
        ACHIEVEMENT_CRITERIA_TYPE_WON_AUCTIONS = 84
        ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_AUCTION_SOLD = 85
        ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_GOLD_VALUE_OWNED = 86
        ACHIEVEMENT_CRITERIA_TYPE_GAIN_REVERED_REPUTATION = 87
        ACHIEVEMENT_CRITERIA_TYPE_GAIN_HONORED_REPUTATION = 88
        ACHIEVEMENT_CRITERIA_TYPE_KNOWN_FACTIONS = 89
        ACHIEVEMENT_CRITERIA_TYPE_LOOT_EPIC_ITEM = 90
        ACHIEVEMENT_CRITERIA_TYPE_RECEIVE_EPIC_ITEM = 91
        ACHIEVEMENT_CRITERIA_TYPE_ROLL_NEED = 93
        ACHIEVEMENT_CRITERIA_TYPE_ROLL_GREED = 94
        ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_HEALTH = 95
        ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_POWER = 96
        ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_STAT = 97
        ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_SPELLPOWER = 98
        ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_ARMOR = 99
        ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_RATING = 100
        ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_HIT_DEALT = 101
        ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_HIT_RECEIVED = 102
        ACHIEVEMENT_CRITERIA_TYPE_TOTAL_DAMAGE_RECEIVED = 103
        ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_HEAL_CASTED = 104
        ACHIEVEMENT_CRITERIA_TYPE_TOTAL_HEALING_RECEIVED = 105
        ACHIEVEMENT_CRITERIA_TYPE_HIGHEST_HEALING_RECEIVED = 106
        ACHIEVEMENT_CRITERIA_TYPE_QUEST_ABANDONED = 107
        ACHIEVEMENT_CRITERIA_TYPE_FLIGHT_PATHS_TAKEN = 108
        ACHIEVEMENT_CRITERIA_TYPE_LOOT_TYPE = 109
        ' TODO: target entry is missing
        ACHIEVEMENT_CRITERIA_TYPE_CAST_SPELL2 = 110
        ACHIEVEMENT_CRITERIA_TYPE_LEARN_SKILL_LINE = 112
        ACHIEVEMENT_CRITERIA_TYPE_EARN_HONORABLE_KILL = 113
        ACHIEVEMENT_CRITERIA_TYPE_ACCEPTED_SUMMONINGS = 114
        ACHIEVEMENT_CRITERIA_TYPE_EARN_ACHIEVEMENT_POINTS = 115
        ACHIEVEMENT_CRITERIA_TYPE_USE_LFD_TO_GROUP_WITH_PLAYERS = 119
        ' 120
        ' 121
        ' 122
        ' 123
        ' 0..123 => 124 criteria types total
        ACHIEVEMENT_CRITERIA_TYPE_TOTAL = 124
    End Enum

    Public Enum AreaFlags As Integer
        AREA_FLAG_SNOW = &H1
        ' snow (only Dun Morogh, Naxxramas, Razorfen Downs and Winterspring)
        AREA_FLAG_UNK1 = &H2
        ' may be necropolis?
        AREA_FLAG_UNK2 = &H4
        ' Only used for areas on map 571 (development before)
        AREA_FLAG_SLAVE_CAPITAL = &H8
        ' city and city subsones
        AREA_FLAG_UNK3 = &H10
        ' can't find common meaning
        AREA_FLAG_SLAVE_CAPITAL2 = &H20
        ' slave capital city flag?
        AREA_FLAG_UNK4 = &H40
        ' many zones have this flag
        AREA_FLAG_ARENA = &H80
        ' arena, both instanced and world arenas
        AREA_FLAG_CAPITAL = &H100
        ' main capital city flag
        AREA_FLAG_CITY = &H200
        ' only for one zone named "City" (where it located?)
        AREA_FLAG_OUTLAND = &H400
        ' expansion zones? (only Eye of the Storm not have this flag, but have 0x00004000 flag)
        AREA_FLAG_SANCTUARY = &H800
        ' sanctuary area (PvP disabled)
        AREA_FLAG_NEED_FLY = &H1000
        ' only Netherwing Ledge, Socrethar's Seat, Tempest Keep, The Arcatraz, The Botanica, The Mechanar, Sorrow Wing Point, Dragonspine Ridge, Netherwing Mines, Dragonmaw Base Camp, Dragonmaw Skyway
        AREA_FLAG_UNUSED1 = &H2000
        ' not used now (no area/zones with this flag set in 3.0.3)
        AREA_FLAG_OUTLAND2 = &H4000
        ' expansion zones? (only Circle of Blood Arena not have this flag, but have 0x00000400 flag)
        AREA_FLAG_PVP = &H8000
        ' pvp objective area? (Death's Door also has this flag although it's no pvp object area)
        AREA_FLAG_ARENA_INSTANCE = &H10000
        ' used by instanced arenas only
        AREA_FLAG_UNUSED2 = &H20000
        ' not used now (no area/zones with this flag set in 3.0.3)
        AREA_FLAG_UNK5 = &H40000
        ' only used for Amani Pass, Hatchet Hills
        AREA_FLAG_UNK6 = &H80000
        ' Valgarde and Acherus: The Ebon Hold
        AREA_FLAG_LOWLEVEL = &H100000
        ' used for some starting areas with area_level <=15
        AREA_FLAG_TOWN = &H200000
        ' small towns with Inn
        AREA_FLAG_UNK7 = &H400000
        ' Warsong Hold, Acherus: The Ebon Hold, New Agamand Inn, Vengeance Landing Inn
        AREA_FLAG_UNK8 = &H800000
        ' Westguard Inn, Acherus: The Ebon Hold, Valgarde
        AREA_FLAG_OUTDOOR_PVP = &H1000000
        ' Wintergrasp and it's subzones
        AREA_FLAG_UNK9 = &H2000000
        ' unknown
        AREA_FLAG_UNK10 = &H4000000
        ' unknown
        AREA_FLAG_CAN_HEARTH_AND_RES = &H8000000
        ' Wintergrasp and it's subzones
        ' 0x20000000 not flyable?
    End Enum

    Public Enum Difficulty As Integer
        REGULAR_DIFFICULTY = 0

        DUNGEON_DIFFICULTY_NORMAL = 0
        DUNGEON_DIFFICULTY_HEROIC = 1
        'DUNGEON_DIFFICULTY_EPIC    = 2,                       // not used, but exists

        RAID_DIFFICULTY_10MAN_NORMAL = 0
        RAID_DIFFICULTY_25MAN_NORMAL = 1
        RAID_DIFFICULTY_10MAN_HEROIC = 2
        RAID_DIFFICULTY_25MAN_HEROIC = 3
    End Enum

#Const MAX_DUNGEON_DIFFICULTY = 2
#Const MAX_RAID_DIFFICULTY = 4
#Const MAX_DIFFICULTY = 4

    Public Enum SpawnMask As Integer
        SPAWNMASK_REGULAR = (1 << REGULAR_DIFFICULTY)
        ' any any maps without spawn modes (continents/subway) or in minimal spawnmode
        SPAWNMASK_DUNGEON_NORMAL = (1 << DUNGEON_DIFFICULTY_NORMAL)
        SPAWNMASK_DUNGEON_HEROIC = (1 << DUNGEON_DIFFICULTY_HEROIC)
        SPAWNMASK_DUNGEON_ALL = (SPAWNMASK_DUNGEON_NORMAL Or SPAWNMASK_DUNGEON_HEROIC)

        SPAWNMASK_RAID_10MAN_NORMAL = (1 << RAID_DIFFICULTY_10MAN_NORMAL)
        SPAWNMASK_RAID_25MAN_NORMAL = (1 << RAID_DIFFICULTY_25MAN_NORMAL)
        SPAWNMASK_RAID_NORMAL_ALL = (SPAWNMASK_RAID_10MAN_NORMAL Or SPAWNMASK_RAID_25MAN_NORMAL)

        SPAWNMASK_RAID_10MAN_HEROIC = (1 << RAID_DIFFICULTY_10MAN_HEROIC)
        SPAWNMASK_RAID_25MAN_HEROIC = (1 << RAID_DIFFICULTY_25MAN_HEROIC)
        SPAWNMASK_RAID_HEROIC_ALL = (SPAWNMASK_RAID_10MAN_HEROIC Or SPAWNMASK_RAID_25MAN_HEROIC)

        SPAWNMASK_RAID_ALL = (SPAWNMASK_RAID_NORMAL_ALL Or SPAWNMASK_RAID_HEROIC_ALL)
    End Enum

    Public Enum FactionTemplateFlags As Integer
        FACTION_TEMPLATE_FLAG_PVP = &H800
        ' flagged for PvP
        FACTION_TEMPLATE_FLAG_CONTESTED_GUARD = &H1000
        ' faction will attack players that were involved in PvP combats
    End Enum

    Public Enum FactionMasks As Integer
        FACTION_MASK_PLAYER = 1
        ' any player
        FACTION_MASK_ALLIANCE = 2
        ' player or creature from alliance team
        FACTION_MASK_HORDE = 4
        ' player or creature from horde team
        FACTION_MASK_MONSTER = 8
        ' aggressive creature from monster team
        ' if none flags set then non-aggressive creature
    End Enum

    Public Enum MapTypes As Integer
        ' Lua_IsInInstance
        MAP_COMMON = 0
        ' none
        MAP_INSTANCE = 1
        ' party
        MAP_RAID = 2
        ' raid
        MAP_BATTLEGROUND = 3
        ' pvp
        MAP_ARENA = 4
        ' arena
    End Enum

    Public Enum AbilytyLearnType As Integer
        ABILITY_LEARNED_ON_GET_PROFESSION_SKILL = 1
        ABILITY_LEARNED_ON_GET_RACE_OR_CLASS_SKILL = 2
    End Enum

    Public Enum ItemEnchantmentType As Integer
        ITEM_ENCHANTMENT_TYPE_NONE = 0
        ITEM_ENCHANTMENT_TYPE_COMBAT_SPELL = 1
        ITEM_ENCHANTMENT_TYPE_DAMAGE = 2
        ITEM_ENCHANTMENT_TYPE_EQUIP_SPELL = 3
        ITEM_ENCHANTMENT_TYPE_RESISTANCE = 4
        ITEM_ENCHANTMENT_TYPE_STAT = 5
        ITEM_ENCHANTMENT_TYPE_TOTEM = 6
        ITEM_ENCHANTMENT_TYPE_USE_SPELL = 7
        ITEM_ENCHANTMENT_TYPE_PRISMATIC_SOCKET = 8
    End Enum

    Public Enum ItemLimitCategoryMode As Integer
        ITEM_LIMIT_CATEGORY_MODE_HAVE = 0
        ' limit applied to amount items in inventory/bank
        ITEM_LIMIT_CATEGORY_MODE_EQUIP = 1
        ' limit applied to amount equipped items (including used gems)
    End Enum

    ' some used in code cases
    Public Enum ItemLimitCategory As Integer
        ITEM_LIMIT_CATEGORY_MANA_GEM = 4
    End Enum

    Public Enum TotemCategoryType As Integer
        TOTEM_CATEGORY_TYPE_KNIFE = 1
        TOTEM_CATEGORY_TYPE_TOTEM = 2
        TOTEM_CATEGORY_TYPE_ROD = 3
        TOTEM_CATEGORY_TYPE_PICK = 21
        TOTEM_CATEGORY_TYPE_STONE = 22
        TOTEM_CATEGORY_TYPE_HAMMER = 23
        TOTEM_CATEGORY_TYPE_SPANNER = 24
    End Enum

    ' SummonProperties.dbc, col 1
    Public Enum SummonPropGroup As Integer
        SUMMON_PROP_GROUP_WILD = 0
        SUMMON_PROP_GROUP_FRIENDLY = 1
        SUMMON_PROP_GROUP_PETS = 2
        SUMMON_PROP_GROUP_CONTROLLABLE = 3
        SUMMON_PROP_GROUP_VEHICLE = 4
    End Enum

    ' SummonProperties.dbc, col 3
    Public Enum SummonPropType As Integer
        SUMMON_PROP_TYPE_OTHER = 0
        ' different summons, 1330 spells in 3.0.3
        SUMMON_PROP_TYPE_SUMMON = 1
        ' generic summons, 49 spells in 3.0.3
        SUMMON_PROP_TYPE_GUARDIAN = 2
        ' summon guardian, 393 spells in 3.0.3
        SUMMON_PROP_TYPE_ARMY = 3
        ' summon army, 5 spells in 3.0.3
        SUMMON_PROP_TYPE_TOTEM = 4
        ' summon totem, 169 spells in 3.0.3
        SUMMON_PROP_TYPE_CRITTER = 5
        ' critter/minipet, 195 spells in 3.0.3
        SUMMON_PROP_TYPE_DK = 6
        ' summon DRW/Ghoul, 2 spells in 3.0.3 "%s's Runeblade"
        SUMMON_PROP_TYPE_CONSTRUCT = 7
        ' summon bot/bomb, 4 spells in 3.0.3 "%s's Construct"
        SUMMON_PROP_TYPE_PHASING = 8
        ' something todo with DK prequest line, 2 spells in 3.0.3 "%s's Opponent"
        SUMMON_PROP_TYPE_SIEGE_VEH = 9
        ' summon different vehicles, 14 spells in 3.0.3 "%s's Vehicle"
        SUMMON_PROP_TYPE_DRAKE_VEH = 10
        ' summon drake (vehicle), 3 spells
        SUMMON_PROP_TYPE_LIGHTWELL = 11
        ' summon lightwell, 6 spells in 3.0.3
        SUMMON_PROP_TYPE_REPAIR_BOT = 12
        ' summon repir bot, 1 spells in 3.2.2a
    End Enum

    ' SummonProperties.dbc, col 5
    Public Enum SummonPropFlags As Integer
        SUMMON_PROP_FLAG_NONE = &H0
        ' 1342 spells in 3.0.3
        SUMMON_PROP_FLAG_UNK1 = &H1
        ' 75 spells in 3.0.3, something unfriendly
        SUMMON_PROP_FLAG_UNK2 = &H2
        ' 616 spells in 3.0.3, something friendly
        SUMMON_PROP_FLAG_UNK3 = &H4
        ' 22 spells in 3.0.3, no idea...
        SUMMON_PROP_FLAG_UNK4 = &H8
        ' 49 spells in 3.0.3, some mounts
        SUMMON_PROP_FLAG_UNK5 = &H10
        ' 25 spells in 3.0.3, quest related?
        SUMMON_PROP_FLAG_CANT_BE_DISMISSED = &H20
        ' 0 spells in 3.0.3, unused
        SUMMON_PROP_FLAG_UNK7 = &H40
        ' 12 spells in 3.0.3, no idea
        SUMMON_PROP_FLAG_UNK8 = &H80
        ' 4 spells in 3.0.3, no idea
        SUMMON_PROP_FLAG_UNK9 = &H100
        ' 51 spells in 3.0.3, no idea, many quest related
        SUMMON_PROP_FLAG_UNK10 = &H200
        ' 51 spells in 3.0.3, something defensive
        SUMMON_PROP_FLAG_UNK11 = &H400
        ' 3 spells, requires something near?
        SUMMON_PROP_FLAG_UNK12 = &H800
        ' 30 spells in 3.0.3, no idea
        SUMMON_PROP_FLAG_UNK13 = &H1000
        ' 8 spells in 3.0.3, siege vehicle
        SUMMON_PROP_FLAG_UNK14 = &H2000
        ' 2 spells in 3.0.3, escort?
    End Enum

    Public Enum SpellEffectIndex As Integer
        EFFECT_INDEX_0 = 0
        EFFECT_INDEX_1 = 1
        EFFECT_INDEX_2 = 2
    End Enum

#Const MAX_EFFECT_INDEX = 3



    Friend NotInheritable Class DefineConstants
        Public Const DEFAULT_MAX_LEVEL As Integer = 80
        Public Const MAX_LEVEL As Integer = 100
        Public Const STRONG_MAX_LEVEL As Integer = 255
        Public Const MAX_BATTLEGROUND_BRACKETS As Integer = 16
        Public Const MAX_DUNGEON_DIFFICULTY As Integer = 2
        Public Const MAX_RAID_DIFFICULTY As Integer = 4
        Public Const MAX_DIFFICULTY As Integer = 4
        Public Const MAX_EFFECT_INDEX As Integer = 3
    End Class

End Module
