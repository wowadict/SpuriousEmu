Public Class WhyDBBaseDB
    Public Enum TableTypes As Byte
        CreatureInfo = 0
        CreatureSpawn
        GameobjectInfo
        GameobjectSpawn
        Items
        CreatureLoot
        GameobjectLoot
        Quests
        BattleGround_BattleMaster
        BattleGround_Template
        AreaTrigger_InvolvedRelation
        AreaTrigger_Tavern
        AreaTrigger_Template
        QuestStartersCreature
        QuestStartersGameobject
        QuestFinishersCreature
        QuestFinishersGameobject
        SpellTeleportCoords
        GameObjectQuestAssociation
        NpcText
        NpcVendor
        NpcGossipTextID
        NpcTrainerDefs
        NpcTrainerSpells
        ItemRandomPropGroups
        ItemSuffixGroups
        ItemPages
        ItemPetFood
        PlayerCreateInfo
        PlayerCreateInfo_Bars
        PlayerCreateInfo_Items
        PlayerCreateInfo_Skills
        PlayerCreateInfo_Spells
        MonsterSay
        LootsSkinning
        LootsItem
        LootsFishing
        Fishing
        LootsPickPocketing
        CmdTeleports
        creature_movement
    End Enum

    Public Enum CreatureInfo_Columns As Byte
        Entry = 0
        Name
        SubName
        InfoStr
        KillCredit1
        KillCredit2
        MaleDisplayID
        FemaleDisplayID
        MaleDisplayID2
        FemaleDisplayID2
        Size
        MinLife
        MaxLife
        MinMana
        MaxMana
        ManaType
        Boss
        Leader
        Faction
        Family
        Type
        SpellDataID
        MinDamage
        MaxDamage
        MinRangedDamage
        MaxRangedDamage
        Armor
        ResHoly
        ResFire
        ResNature
        ResFrost
        ResShadow
        ResArcane
        WalkSpeed
        RunSpeed
        FlySpeed
        RespawnTime
        BaseAttackSpeed
        BaseRangedAttackSpeed
        CombatReach
        BondingRadius
        NpcFlags
        Flags
        MinLevel
        MaxLevel
        Loot
        SkinLoot
        PickpocketLoot
        EquipmentEntry
        EquipModel1
        EquipModel2
        EquipModel3
        EquipInfo1
        EquipInfo2
        EquipInfo3
        EquipSlot1
        EquipSlot2
        EquipSlot3
        Rank
        UnknownFloat1
        UnknownFloat2
        AiScript
        MinGold
        MaxGold
        TrainerType
        TrainerSpell
        TrainerClass
        TrainerRace
        MovementType
        RegenHealth
        QuestItem1
        QuestItem2
        QuestItem3
        QuestItem4
        QuestItem5
        QuestItem6
        ExtraA9Flags
        ModImmunities
    End Enum

    Public Enum CreatureSpawn_Columns As Byte
        ID = 0
        Entry
        PositionX
        PositionY
        PositionZ
        Orientation
        Range
        Map
        MoveType
        Model
        Faction
        Mount
        Flags
        Bytes0
        Bytes1
        Bytes2
        EmoteState
        StandState
        equipslot1
        equipslot2
        equipslot3
        CanFly
    End Enum

    Public Enum GameobjectInfo_Columns As Byte
        Entry = 0
        Model
        Name
        Category
        Type
        RespawnTime
        Field0
        Field1
        Field2
        Field3
        Field4
        Field5
        Field6
        Field7
        Field8
        Field9
        Field10
        Field11
        Field12
        Field13
        Field14
        Field15
        Field16
        Field17
        Field18
        Field19
        Field20
        Field21
        Field22
        Field23
        CastBarCaption
        Scale
        QuestItem1
        QuestItem2
        QuestItem3
        QuestItem4
        QuestItem5
        QuestItem6
    End Enum

    Public Enum GameobjectSpawn_Columns As Byte
        ID = 0
        Entry
        PositionX
        PositionY
        PositionZ
        Orientation
        Rotation1
        Rotation2
        Rotation3
        Rotation4
        Map
        State
        Flags
        Faction
        Scale
    End Enum

    Public Enum Item_Columns As Byte
        Entry = 0
        Classe
        SubClass
        Field4
        Name1
        Name2
        Name3
        Name4
        DisplayID
        Quality
        Flags
        Faction
        BuyCount
        Buyprice
        Sellprice
        InventoryType
        AllowableClass
        AllowableRace
        ItemLevel
        RequiredLevel
        RequiredSkill
        RequiredSubRank
        RequiredSpell
        RequiredSkillRank
        RequiredPlayerRank1
        RequiredPlayerRank2
        RequiredFaction
        RequiredFactionStanding
        Unique
        MaxCount
        ContainerSlots
        StatsCount
        Stat_Type1
        Stat_Value1
        Stat_Type2
        Stat_Value2
        Stat_Type3
        Stat_Value3
        Stat_Type4
        Stat_Value4
        Stat_Type5
        Stat_Value5
        Stat_Type6
        Stat_Value6
        Stat_Type7
        Stat_Value7
        Stat_Type8
        Stat_Value8
        Stat_Type9
        Stat_Value9
        Stat_Type10
        Stat_Value10
        ScaledStatsDistributionID
        ScaledStatsDistributionFlags
        Dmg_Min1
        Dmg_Max1
        Dmg_Type1
        Dmg_Min2
        Dmg_Max2
        Dmg_Type2
        Dmg_Min3
        Dmg_Max3
        Dmg_Type3
        Dmg_Min4
        Dmg_Max4
        Dmg_Type4
        Dmg_Min5
        Dmg_Max5
        Dmg_Type5
        Armor
        HolyRes
        FireRes
        NatureRes
        FrostRes
        ShadowRes
        ArcaneRes
        Delay
        AmmoType
        Range
        SpellID_1
        SpellTrigger_1
        SpellCharges_1
        SpellCooldown_1
        SpellCategory_1
        SpellCategoryCooldown_1
        SpellID_2
        SpellTrigger_2
        SpellCharges_2
        SpellCooldown_2
        SpellCategory_2
        SpellCategoryCooldown_2
        SpellID_3
        SpellTrigger_3
        SpellCharges_3
        SpellCooldown_3
        SpellCategory_3
        SpellCategoryCooldown_3
        SpellID_4
        SpellTrigger_4
        SpellCharges_4
        SpellCooldown_4
        SpellCategory_4
        SpellCategoryCooldown_4
        SpellID_5
        SpellTrigger_5
        SpellCharges_5
        SpellCooldown_5
        SpellCategory_5
        SpellCategoryCooldown_5
        Bonding
        Description
        PageID
        PageLanguage
        PageMaterial
        QuestID
        LockID
        LockMaterial
        SheathID
        RandomProp
        RandomSuffix
        Block
        ItemSet
        MaxDurability
        ZoneNameID
        MapID
        BagFamily
        TotemCategory
        SocketColor_1
        SocketContent_1
        SocketColor_2
        SocketContent_2
        SocketColor_3
        SocketContent_3
        SocketBonus
        GemProperties
        ReqDisenchantSkill
        ArmorDamageModifier
        ExistingDuration
        ItemLimitCategory
        HolidayId
        DisenchantID
        FoodType
        MinMoneyLoot
        MaxMoneyLoot
        ExtraFlags
    End Enum

    Public Enum ItemsAmounts_Columns As Byte
        Item_Entry = 0
        SellAmount
        Stock
        StockRefill
    End Enum

    Public Enum CreatureLoot_Columns As Byte
        Index = 0
        Entry
        Item
        Chance
        HeroicChance
        Min
        Max
        FFA
    End Enum

    Public Enum GameobjectLoot_Columns As Byte
        Index = 0
        Entry
        Item
        Chance
        HeroicChance
        Min
        Max
        FFA
    End Enum

    Public Enum ItemsLoot_Columns As Byte
        Index = 0
        Entry
        Item
        Chance
        HeroicChance
        Min
        Max
        FFA
    End Enum

    Public Enum SkinningLoot_Columns As Byte
        Index = 0
        Entry
        Item
        Chance
        HeroicChance
        Min
        Max
        FFA
    End Enum

    Public Enum FishingLoot_Columns As Byte
        Index = 0
        Entry
        Item
        Chance
        HeroicChance
        Min
        Max
        FFA
    End Enum

    Public Enum Fishing_Columns As Byte
        Zone = 0
        MinSkill
        MaxSkill
    End Enum

    Public Enum PickPocketingLoot_Columns As Byte
        Index = 0
        Entry
        Item
        Chance
        HeroicChance
        Min
        Max
        FFA
    End Enum

    Public Enum Quest_Columns As Byte
        ID = 0
        Zone
        Flags
        LevelStart
        LevelNormal
        Type
        RequiredRace
        RequiredClass
        RequiredTradeSkill
        RequiredTradeSkillValue
        RequiredReputation1Faction
        RequiredReputation1
        RequiredReputation2Faction
        RequiredReputation2
        TimeLimit
        SpecialFlags
        PrevQuest
        NextQuest
        ObjectiveDeliver1
        ObjectiveDeliver1Count
        Title
        TextDescription
        TextObjectives
        TextComplete
        TextIncomplete
        TextEnd
        ObjectiveText1
        ObjectiveText2
        ObjectiveText3
        ObjectiveText4
        ObjectiveItem1
        ObjectiveItem2
        ObjectiveItem3
        ObjectiveItem4
        ObjectiveItem5
        ObjectiveItem6
        ObjectiveItem1Count
        ObjectiveItem2Count
        ObjectiveItem3Count
        ObjectiveItem4Count
        ObjectiveItem5Count
        ObjectiveItem6Count
        ObjectiveKill1
        ObjectiveKill2
        ObjectiveKill3
        ObjectiveKill4
        ObjectiveKill1Count
        ObjectiveKill2Count
        ObjectiveKill3Count
        ObjectiveKill4Count
        ObjectiveCast1
        ObjectiveCast2
        ObjectiveCast3
        ObjectiveCast4
        RequiredEmoteId1
        RequiredEmoteId2
        RequiredEmoteId3
        RequiredEmoteId4
        RewardItem1
        RewardItem2
        RewardItem3
        RewardItem4
        RewardItem5
        RewardItem6
        RewardItem1Count
        RewardItem2Count
        RewardItem3Count
        RewardItem4Count
        RewardItem5Count
        RewardItem6Count
        RewardStaticItem1
        RewardStaticItem2
        RewardStaticItem3
        RewardStaticItem4
        RewardStaticItem1Count
        RewardStaticItem2Count
        RewardStaticItem3Count
        RewardStaticItem4Count
        RewardReputation1Faction
        RewardReputation2Faction
        RewardReputation3Faction
        RewardReputation4Faction
        RewardReputation5Faction
        RewardReputation6Faction
        RewardReputation1
        RewardReputation2
        RewardReputation3
        RewardReputation4
        RewardReputation5
        RewardReputation6
        RewardRepLimit
        RewardGold
        RewardXP
        RewardSpell
        RewardSpellCast
        PointMap
        PointX
        PointY
        PointZ
        PointOpt
        MoneyAtMaxLevel
        ObjectiveTrigger1
        ObjectiveTrigger2
        ObjectiveTrigger3
        ObjectiveTrigger4
        RequiredOneOfQuest
        RequiredQuest1
        RequiredQuest2
        RequiredQuest3
        RequiredQuest4
        RemoveQuests
        ReceiveItemId1
        ReceiveItemId2
        ReceiveItemId3
        ReceiveItemId4
        ReceiveItem1Count
        ReceiveItem2Count
        ReceiveItem3Count
        ReceiveItem4Count
        IsRepeatable
        BonusHonor
        RewTitleId
        RewardTalents
        SuggestedPlayers
        DetailEmotecount
        DetailEmote1
        DetailEmote2
        DetailEmote3
        DetailEmote4
        DetailEmoteDelay1
        DetailEmoteDelay2
        DetailEmoteDelay3
        DetailEmoteDelay4
        CompletionEmoteCount
        CompletionEmote1
        CompletionEmote2
        CompletionEmote3
        CompletionEmote4
        CompletionEmoteDelay1
        CompletionEmoteDelay2
        CompletionEmoteDelay3
        CompletionEmoteDelay4
        CompleteEmote
        InCompleteEmote
        isCompletedBySpellEffect
        IsActive
    End Enum

    Public Enum BattleGround_Columns
        Entry = 0
        Battleground_Entry
    End Enum

    Public Enum AreaTrigger_Columns
        TriggerID = 0
        quest
        type
        name
        position_map
        position_x
        position_y
        position_z
        target_map
        target_screen
        target_position_x
        target_position_y
        target_position_z
        target_orientation
        required_item
        required_item2
        heroic_key
        heroic_key2
        required_quest_done
        required_failed_text
        required_level
    End Enum

    Public Enum QuestStarter_Columns
        Type = 0
        TypeID
        QuestID
    End Enum

    Public Enum QuestFinisher_Columns
        Type = 0
        TypeID
        QuestID
    End Enum

    Public Enum SpellTeleportCoords_Columns
        ID = 0
        Name
        Map
        PosX
        PosY
        PosZ
        orientation
        ToTrigger
    End Enum

    Public Enum GameObjectQuestAssociation_Columns
        Entry = 0
        Quest
        Item
        ItemCount
    End Enum

    Public Enum NpcText_Columns
        Entry = 0
        Prob0
        Text0_0
        Text0_1
        Lang0
        em0_0
        em0_1
        em0_2
        em0_3
        em0_4
        em0_5
        Prob1
        Text1_0
        Text1_1
        Lang1
        em1_0
        em1_1
        em1_2
        em1_3
        em1_4
        em1_5
        Prob2
        Text2_0
        Text2_1
        Lang2
        em2_0
        em2_1
        em2_2
        em2_3
        em2_4
        em2_5
        Prob3
        Text3_0
        Text3_1
        Lang3
        em3_0
        em3_1
        em3_2
        em3_3
        em3_4
        em3_5
        Prob4
        Text4_0
        Text4_1
        Lang4
        em4_0
        em4_1
        em4_2
        em4_3
        em4_4
        em4_5
        Prob5
        Text5_0
        Text5_1
        Lang5
        em5_0
        em5_1
        em5_2
        em5_3
        em5_4
        em5_5
        Prob6
        Text6_0
        Text6_1
        Lang6
        em6_0
        em6_1
        em6_2
        em6_3
        em6_4
        em6_5
        Prob7
        Text7_0
        Text7_1
        Lang7
        em7_0
        em7_1
        em7_2
        em7_3
        em7_4
        em7_5
    End Enum

    Public Enum NpcVendor_Columns
        Entry = 0
        Item
        Amount
        Stock
        Refill
        ExtendedCost
    End Enum

    Public Enum NpcGossipTextID_Columns
        Entry = 0
        TextID
    End Enum

    Public Enum NpcTrainerDef_Columns
        Entry = 0
        ReqSkill
        ReqSkillValue
        ReqClass
        ReqRace
        RequiredReputation
        RequiredReputationValue
        TrainerType
        WindowMessage
        CanTrainMessage
        CanNotTrainMessage
    End Enum

    Public Enum NpcTrainerSpell_Columns
        Spell = 0
        Entry
        SpellCost
        ReqSpell
        ReqSkill
        ReqSkillValue
        ReqLevel
        DeleteSpell
        IsProf
        IsCast
    End Enum

    Public Enum ItemRandom_Props_Columns
        ItemEntry = 0
        RandomPropsEntryID
        Chance
    End Enum

    Public Enum ItemRandom_Suffix_Columns
        ItemEntry = 0
        RandomSuffixEntryID
        Chance
    End Enum

    Public Enum PetFood_Columns
        ItemEntry = 0
        FoodType
    End Enum

    Public Enum ItemPages_Columns
        ItemEntry = 0
        Text
        NextPage
    End Enum

    Public Enum PlayerCreateInfo_Columns
        Index
        Race
        FactionTemplate
        Classe
        MapID
        ZoneID
        PositionX
        PositionY
        PositionZ
        DisplayID
        BaseStrength
        BaseAgility
        BaseStamina
        BaseIntellect
        BaseSpirit
        BaseHealth
        BaseMana
        BaseRage
        BaseFocus
        BaseEnergy
        AttackPower
        MinDmg
        MaxDmg
        IntroID
        TaxiMask
    End Enum

    Public Enum PlayerCreateInfoBar_Columns
        Race = 0
        Classe
        Button
        Action
        Type
        Misc
    End Enum

    Public Enum PlayerCreateInfoItem_Columns
        IndexID
        ProtoID
        SlotID
        Amount
    End Enum

    Public Enum PlayerCreateInfoSkill_Columns
        IndexID
        SkillID
        Level
        MaxLevel
    End Enum

    Public Enum PlayerCreateInfoSpell_Columns
        IndexID
        SpellID
    End Enum

    Public Enum MonsterSay_Columns
        Entry = 0
        Evente
        Chance
        Language
        Type
        MonsterName
        Text0
        Text1
        Text2
        Text3
        Text4
    End Enum

    Public Enum CmdTeleports_Columns
        Id = 0
        Name
        MapID
        PositionX
        PositionY
        PositionZ
        Orientation
    End Enum

    Public Enum Creature_Movement_Columns As Byte
        spawnid = 0
        waypointid
        position_x
        position_y
        position_z
        waittime
        flags
        emote
        orientation
    End Enum

    Public Class TTable
        Public Name As String
        Public Type As TableTypes
        Public Columns As New Dictionary(Of Byte, String)

        Public Sub New(ByVal Name_ As String, ByVal Type_ As TableTypes)
            Name = Name_
            Type = Type_
        End Sub
    End Class

    Public Tables As New List(Of TTable)
End Class
