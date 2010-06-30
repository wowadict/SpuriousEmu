/*
MySQL Data Transfer
Source Host: localhost
Source Database: spurious
Target Host: localhost
Target Database: spurious
Date: 11/25/2008 4:25:55 PM
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for areatrigger_involvedrelation
-- ----------------------------
DROP TABLE IF EXISTS `areatrigger_involvedrelation`;
CREATE TABLE `areatrigger_involvedrelation` (
  `id` int(11) unsigned NOT NULL default '0',
  `quest` int(11) unsigned NOT NULL default '0',
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for areatrigger_tavern
-- ----------------------------
DROP TABLE IF EXISTS `areatrigger_tavern`;
CREATE TABLE `areatrigger_tavern` (
  `id` mediumint(8) unsigned NOT NULL default '0',
  `name` text,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for areatrigger_teleport
-- ----------------------------
DROP TABLE IF EXISTS `areatrigger_teleport`;
CREATE TABLE `areatrigger_teleport` (
  `id` mediumint(8) unsigned NOT NULL default '0',
  `name` text,
  `required_level` tinyint(3) unsigned NOT NULL default '0',
  `required_item` mediumint(8) unsigned NOT NULL default '0',
  `required_item2` mediumint(8) unsigned NOT NULL default '0',
  `heroic_key` mediumint(8) unsigned NOT NULL default '0',
  `heroic_key2` mediumint(8) unsigned NOT NULL default '0',
  `required_quest_done` int(11) unsigned NOT NULL default '0',
  `required_failed_text` text,
  `target_map` smallint(5) unsigned NOT NULL default '0',
  `target_position_x` float NOT NULL default '0',
  `target_position_y` float NOT NULL default '0',
  `target_position_z` float NOT NULL default '0',
  `target_orientation` float NOT NULL default '0',
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for battleground_battlemaster
-- ----------------------------
DROP TABLE IF EXISTS `battleground_battlemaster`;
CREATE TABLE `battleground_battlemaster` (
  `entry` mediumint(9) NOT NULL COMMENT 'Entry of a creature',
  `battleground_entry` smallint(6) NOT NULL COMMENT 'BattlemasterList.dbc Identifier',
  PRIMARY KEY  (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COMMENT='Battleground System (Templates)';

-- ----------------------------
-- Table structure for battleground_template
-- ----------------------------
DROP TABLE IF EXISTS `battleground_template`;
CREATE TABLE `battleground_template` (
  `id` smallint(6) NOT NULL COMMENT 'BattlemasterList.dbc Identifier',
  `Name` varchar(50) NOT NULL COMMENT 'Map Name',
  `Type` tinyint(3) unsigned NOT NULL,
  `Map1` smallint(5) unsigned NOT NULL,
  `Map2` smallint(5) unsigned NOT NULL,
  `Map3` smallint(5) unsigned NOT NULL,
  `MinPlayersPerTeam` tinyint(4) unsigned NOT NULL COMMENT 'Minimum number of player before battleground will be closed.',
  `MaxPlayersPerTeam` tinyint(4) unsigned NOT NULL COMMENT 'Maximum number of players before battleground get closed for new joins.',
  `MinLvl` tinyint(4) NOT NULL COMMENT 'Minimum level required.',
  `MaxLvl` tinyint(4) NOT NULL COMMENT 'Maximum level before player can''t get in.',
  `Band` tinyint(3) unsigned NOT NULL,
  `AllianceStartLoc` smallint(6) NOT NULL COMMENT 'X y Z location from entry at WorldMapArea.dbc',
  `AllianceStartO` float NOT NULL COMMENT 'Where the player will be faced into',
  `HordeStartLoc` smallint(6) NOT NULL COMMENT 'X y Z location from entry at WorldMapArea.dbc',
  `HordeStartO` float NOT NULL COMMENT 'Where the player will be faced into',
  `IsActive` tinyint(3) unsigned NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for creatures
-- ----------------------------
DROP TABLE IF EXISTS `creatures`;
CREATE TABLE `creatures` (
  `creature_id` int(30) NOT NULL default '0',
  `creature_name` varchar(100) NOT NULL default '',
  `creature_guild` varchar(100) NOT NULL default '',
  `info_str` varchar(500) default '',
  `creature_model` int(30) unsigned NOT NULL default '0',
  `creature_femalemodel` int(30) unsigned NOT NULL default '0',
  `creature_size` float NOT NULL default '0',
  `creature_life` int(30) NOT NULL default '0',
  `creature_mana` int(30) NOT NULL default '0',
  `creature_manaType` int(30) unsigned NOT NULL default '0',
  `creature_elite` int(11) unsigned NOT NULL default '0',
  `creature_leader` int(11) default '0',
  `creature_faction` int(30) NOT NULL default '0',
  `creature_family` int(10) unsigned NOT NULL default '0',
  `creature_type` int(10) unsigned NOT NULL default '0',
  `creature_spelldataid` int(30) default '0',
  `creature_minDamage` float NOT NULL default '0',
  `creature_maxDamage` float NOT NULL default '0',
  `creature_minRangedDamage` float NOT NULL default '0',
  `creature_maxRangedDamage` float NOT NULL default '0',
  `creature_armor` int(30) unsigned NOT NULL default '0',
  `creature_resHoly` int(30) unsigned NOT NULL default '0',
  `creature_resFire` int(30) unsigned NOT NULL default '0',
  `creature_resNature` int(30) unsigned NOT NULL default '0',
  `creature_resFrost` int(30) unsigned NOT NULL default '0',
  `creature_resShadow` int(30) unsigned NOT NULL default '0',
  `creature_resArcane` int(30) unsigned NOT NULL default '0',
  `creature_walkSpeed` float NOT NULL default '0',
  `creature_runSpeed` float NOT NULL default '0',
  `creature_flySpeed` float NOT NULL default '0',
  `creature_respawnTime` int(10) NOT NULL default '0',
  `creature_baseAttackSpeed` int(30) NOT NULL default '0',
  `creature_baseRangedAttackSpeed` int(30) NOT NULL default '0',
  `creature_combatReach` float NOT NULL default '0',
  `creature_bondingRadius` float NOT NULL default '0',
  `creature_npcFlags` int(30) NOT NULL default '0',
  `creature_flags` int(10) NOT NULL default '0',
  `creature_minLevel` int(30) unsigned NOT NULL default '0',
  `creature_maxLevel` int(30) unsigned NOT NULL default '0',
  `creature_loot` int(30) unsigned NOT NULL default '0',
  `creature_skinloot` int(30) unsigned NOT NULL default '0',
  `creature_pickpocketloot` int(30) unsigned NOT NULL default '0',
  `creature_equipmententry` int(30) unsigned NOT NULL default '0',
  `creature_unkfloat1` float default '0',
  `creature_unkfloat2` float default '0',
  `creature_aiScript` varchar(255) NOT NULL default '',
  PRIMARY KEY  (`creature_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for creature_movement
-- ----------------------------
DROP TABLE IF EXISTS `creature_movement`;
CREATE TABLE `creature_movement` (                                                       
  `spawnid` int(11) NOT NULL,                                                            
  `waypointid` smallint(6) NOT NULL,                                                     
  `position_x` float NOT NULL,                                                           
  `position_y` float NOT NULL,                                                           
  `position_z` float NOT NULL,                                                           
  `waittime` mediumint(9) NOT NULL DEFAULT '0',                                          
  `flags` smallint(6) NOT NULL DEFAULT '0',                                              
  `emote` smallint(6) NOT NULL DEFAULT '0',                                              
  `orientation` float NOT NULL DEFAULT '0',                                              
  PRIMARY KEY (`spawnid`,`waypointid`)                                                   
) ENGINE=MyISAM DEFAULT CHARSET=latin1  COMMENT='Creature System (Creature''s Movement)';

-- ----------------------------
-- Table structure for gameobject_quest_association
-- ----------------------------
DROP TABLE IF EXISTS `gameobject_quest_association`;
CREATE TABLE `gameobject_quest_association` (
  `entry` int(11) NOT NULL,
  `quest` mediumint(9) NOT NULL,
  `item` mediumint(9) NOT NULL,
  `item_count` tinyint(4) NOT NULL,
  PRIMARY KEY  (`entry`,`item`,`quest`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='Gameobject System (Quest loot link)';

-- ----------------------------
-- Table structure for gameobjects
-- ----------------------------
DROP TABLE IF EXISTS `gameobjects`;
CREATE TABLE `gameobjects` (
  `gameObject_ID` int(10) NOT NULL default '0',
  `gameObject_Model` int(10) NOT NULL default '0',
  `gameObject_Name` varchar(255) NOT NULL default '',
  `gameObject_Type` int(10) unsigned NOT NULL default '0',
  `gameObject_RespawnTime` int(10) NOT NULL default '0',
  `gameObject_Field0` int(11) NOT NULL default '0',
  `gameObject_Field1` int(11) NOT NULL default '0',
  `gameObject_Field2` int(11) NOT NULL default '0',
  `gameObject_Field3` int(11) NOT NULL default '0',
  `gameObject_Field4` mediumint(9) NOT NULL default '0',
  `gameObject_Field5` mediumint(9) NOT NULL default '0',
  `gameObject_Field6` int(11) NOT NULL default '0',
  `gameObject_Field7` mediumint(9) NOT NULL default '0',
  `gameObject_Field8` mediumint(9) NOT NULL default '0',
  `gameObject_Field9` mediumint(9) NOT NULL default '0',
  `gameObject_Field10` mediumint(9) NOT NULL default '0',
  `gameObject_Field11` mediumint(9) NOT NULL default '0',
  `gameObject_Field12` mediumint(9) NOT NULL default '0',
  `gameObject_Field13` mediumint(9) NOT NULL default '0',
  `gameObject_Field14` mediumint(9) NOT NULL default '0',
  `gameObject_Field15` smallint(6) NOT NULL default '0',
  `gameObject_Field16` smallint(6) NOT NULL default '0',
  `gameObject_Field17` smallint(6) NOT NULL default '0',
  `gameObject_Field18` smallint(6) NOT NULL default '0',
  `gameObject_Field19` int(11) NOT NULL default '0',
  `gameObject_Field20` int(11) NOT NULL default '0',
  `gameObject_Field21` int(11) NOT NULL default '0',
  `gameObject_Field22` int(11) NOT NULL default '0',
  `gameObject_Field23` int(11) NOT NULL default '0',
  PRIMARY KEY  (`gameObject_ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for guard_gossip_menuitems
-- ----------------------------
DROP TABLE IF EXISTS `guard_gossip_menuitems`;
CREATE TABLE `guard_gossip_menuitems` (
  `MenuItem_ID` int(30) NOT NULL auto_increment,
  `MenuItem_Text` varchar(50) default NULL,
  PRIMARY KEY  (`MenuItem_ID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Table structure for guard_gossip_menus
-- ----------------------------
DROP TABLE IF EXISTS `guard_gossip_menus`;
CREATE TABLE `guard_gossip_menus` (
  `entry` int(30) default NULL,
  `Menu_Number` int(10) default NULL,
  `Menu_Header_TextID` int(10) default '0',
  `Menu_Data` text COMMENT 'Option:Menu_Icon:MenuItem_ID:TEXT_ID:POI_ID:SubMenu_Number'
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Table structure for guard_gossip_poi
-- ----------------------------
DROP TABLE IF EXISTS `guard_gossip_poi`;
CREATE TABLE `guard_gossip_poi` (
  `PoI_ID` int(30) NOT NULL auto_increment,
  `PoI_X` float default NULL,
  `PoI_Y` float default NULL,
  `PoI_Icon` int(10) default NULL,
  `PoI_Flags` int(10) default NULL,
  `PoI_Data` int(10) default NULL,
  `PoI_Name` varchar(50) default NULL,
  PRIMARY KEY  (`PoI_ID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Table structure for item_quest_association
-- ----------------------------
DROP TABLE IF EXISTS `item_quest_association`;
CREATE TABLE `item_quest_association` (
  `item` int(11) NOT NULL default '0',
  `quest` int(11) NOT NULL default '0',
  `item_count` int(11) NOT NULL default '0',
  PRIMARY KEY  (`item`,`quest`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='Quest System';

-- ----------------------------
-- Table structure for item_randomprop_groups
-- ----------------------------
DROP TABLE IF EXISTS `item_randomprop_groups`;
CREATE TABLE `item_randomprop_groups` (
  `entry_id` int(30) NOT NULL,
  `randomprops_entryid` int(30) NOT NULL,
  `chance` float NOT NULL,
  PRIMARY KEY  (`entry_id`,`randomprops_entryid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 ROW_FORMAT=FIXED COMMENT='Item System';

-- ----------------------------
-- Table structure for item_randomsuffix_groups
-- ----------------------------
DROP TABLE IF EXISTS `item_randomsuffix_groups`;
CREATE TABLE `item_randomsuffix_groups` (
  `entry_id` int(30) NOT NULL,
  `randomsuffix_entryid` int(30) NOT NULL,
  `chance` float NOT NULL,
  PRIMARY KEY  (`entry_id`,`randomsuffix_entryid`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='Item System';

-- ----------------------------
-- Table structure for itemloot
-- ----------------------------
DROP TABLE IF EXISTS `itemloot`;
CREATE TABLE `itemloot` (
  `index` int(11) unsigned NOT NULL auto_increment,
  `entryid` int(11) unsigned NOT NULL default '0',
  `itemid` int(11) unsigned NOT NULL default '25',
  `percentchance` float NOT NULL default '0',
  `heroicpercentchance` float NOT NULL default '0',
  `mincount` int(11) unsigned NOT NULL default '1',
  `maxcount` int(11) unsigned NOT NULL default '1',
  `ffa_loot` int(10) unsigned NOT NULL default '0',
  PRIMARY KEY  (`entryid`,`itemid`),
  UNIQUE KEY `index` (`index`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='Loot System';

-- ----------------------------
-- Table structure for itempages
-- ----------------------------
DROP TABLE IF EXISTS `itempages`;
CREATE TABLE `itempages` (
  `entry` int(10) unsigned NOT NULL default '0',
  `text` longtext character set utf8 collate utf8_unicode_ci NOT NULL,
  `next_page` int(10) unsigned NOT NULL default '0',
  PRIMARY KEY  (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='Item System';

-- ----------------------------
-- Table structure for itempages_localized
-- ----------------------------
DROP TABLE IF EXISTS `itempages_localized`;
CREATE TABLE `itempages_localized` (
  `entry` int(30) NOT NULL,
  `language_code` varchar(5) character set latin1 NOT NULL,
  `text` text character set latin1 NOT NULL,
  PRIMARY KEY  (`entry`,`language_code`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Table structure for itempetfood
-- ----------------------------
DROP TABLE IF EXISTS `itempetfood`;
CREATE TABLE `itempetfood` (
  `entry` int(11) NOT NULL,
  `food_type` int(11) NOT NULL,
  PRIMARY KEY  (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Table structure for items
-- ----------------------------
DROP TABLE IF EXISTS `items`;
CREATE TABLE `items` (
  `entry` mediumint(9) NOT NULL,
  `class` tinyint(4) NOT NULL,
  `subclass` tinyint(4) NOT NULL,
  `field4` tinyint(4) NOT NULL,
  `name1` varchar(255) NOT NULL,
  `name2` varchar(255) NOT NULL default '',
  `name3` varchar(255) NOT NULL default '',
  `name4` varchar(255) NOT NULL default '',
  `displayid` mediumint(9) NOT NULL,
  `quality` tinyint(4) NOT NULL,
  `flags` int(11) NOT NULL,
  `buyprice` int(11) NOT NULL,
  `sellprice` int(11) NOT NULL,
  `inventorytype` tinyint(4) NOT NULL,
  `allowableclass` int(30) NOT NULL,
  `allowablerace` int(30) NOT NULL,
  `itemlevel` smallint(6) NOT NULL,
  `requiredlevel` smallint(6) NOT NULL,
  `RequiredSkill` smallint(6) NOT NULL,
  `RequiredSkillRank` smallint(6) NOT NULL,
  `RequiredSkillSubRank` mediumint(9) NOT NULL,
  `RequiredPlayerRank1` smallint(6) NOT NULL,
  `RequiredPlayerRank2` smallint(6) NOT NULL,
  `RequiredFaction` smallint(6) NOT NULL,
  `RequiredFactionStanding` tinyint(4) NOT NULL,
  `Unique` tinyint(4) NOT NULL,
  `maxcount` smallint(6) NOT NULL,
  `ContainerSlots` tinyint(4) NOT NULL,
  `stat_type1` smallint(6) NOT NULL,
  `stat_value1` smallint(6) NOT NULL,
  `stat_type2` smallint(6) NOT NULL,
  `stat_value2` smallint(6) NOT NULL,
  `stat_type3` smallint(6) NOT NULL,
  `stat_value3` smallint(6) NOT NULL,
  `stat_type4` smallint(6) NOT NULL,
  `stat_value4` smallint(6) NOT NULL,
  `stat_type5` smallint(6) NOT NULL,
  `stat_value5` smallint(6) NOT NULL,
  `stat_type6` smallint(6) NOT NULL,
  `stat_value6` smallint(6) NOT NULL,
  `stat_type7` smallint(6) NOT NULL,
  `stat_value7` smallint(6) NOT NULL,
  `stat_type8` smallint(6) NOT NULL,
  `stat_value8` smallint(6) NOT NULL,
  `stat_type9` smallint(6) NOT NULL,
  `stat_value9` smallint(6) NOT NULL,
  `stat_type10` smallint(6) NOT NULL,
  `stat_value10` smallint(6) NOT NULL,
  `dmg_min1` float NOT NULL,
  `dmg_max1` float NOT NULL,
  `dmg_type1` smallint(6) NOT NULL,
  `dmg_min2` float NOT NULL,
  `dmg_max2` float NOT NULL,
  `dmg_type2` smallint(6) NOT NULL,
  `dmg_min3` float NOT NULL,
  `dmg_max3` float NOT NULL,
  `dmg_type3` smallint(6) NOT NULL,
  `dmg_min4` float NOT NULL,
  `dmg_max4` float NOT NULL,
  `dmg_type4` smallint(6) NOT NULL,
  `dmg_min5` float NOT NULL,
  `dmg_max5` float NOT NULL,
  `dmg_type5` smallint(6) NOT NULL,
  `armor` smallint(6) NOT NULL,
  `holy_res` smallint(6) NOT NULL,
  `fire_res` smallint(6) NOT NULL,
  `nature_res` smallint(6) NOT NULL,
  `frost_res` smallint(6) NOT NULL,
  `shadow_res` smallint(6) NOT NULL,
  `arcane_res` smallint(6) NOT NULL,
  `delay` smallint(6) NOT NULL,
  `ammo_type` tinyint(4) NOT NULL,
  `range` float NOT NULL,
  `spellid_1` mediumint(9) NOT NULL,
  `spelltrigger_1` tinyint(4) NOT NULL,
  `spellcharges_1` smallint(6) NOT NULL,
  `spellcooldown_1` int(30) NOT NULL default '0',
  `spellcategory_1` smallint(6) NOT NULL,
  `spellcategorycooldown_1` int(30) NOT NULL default '0',
  `spellid_2` mediumint(9) NOT NULL,
  `spelltrigger_2` tinyint(4) NOT NULL,
  `spellcharges_2` smallint(6) NOT NULL,
  `spellcooldown_2` int(30) NOT NULL default '0',
  `spellcategory_2` smallint(6) NOT NULL,
  `spellcategorycooldown_2` int(30) NOT NULL default '0',
  `spellid_3` mediumint(9) NOT NULL,
  `spelltrigger_3` tinyint(4) NOT NULL,
  `spellcharges_3` smallint(6) NOT NULL,
  `spellcooldown_3` int(30) NOT NULL default '0',
  `spellcategory_3` smallint(6) NOT NULL,
  `spellcategorycooldown_3` int(30) NOT NULL default '0',
  `spellid_4` mediumint(9) NOT NULL,
  `spelltrigger_4` tinyint(4) NOT NULL,
  `spellcharges_4` smallint(6) NOT NULL,
  `spellcooldown_4` int(30) NOT NULL default '0',
  `spellcategory_4` smallint(6) NOT NULL,
  `spellcategorycooldown_4` int(30) NOT NULL default '0',
  `spellid_5` mediumint(9) NOT NULL,
  `spelltrigger_5` tinyint(4) NOT NULL,
  `spellcharges_5` smallint(6) NOT NULL,
  `spellcooldown_5` int(30) NOT NULL default '0',
  `spellcategory_5` smallint(6) NOT NULL,
  `spellcategorycooldown_5` int(30) NOT NULL default '0',
  `bonding` tinyint(4) NOT NULL,
  `description` varchar(255) NOT NULL default '',
  `page_id` smallint(6) NOT NULL,
  `page_language` tinyint(4) NOT NULL,
  `page_material` tinyint(4) NOT NULL,
  `quest_id` smallint(6) NOT NULL,
  `lock_id` smallint(6) NOT NULL,
  `lock_material` tinyint(4) NOT NULL,
  `sheathID` tinyint(4) NOT NULL,
  `randomprop` smallint(6) NOT NULL,
  `randomsuffix` smallint(6) NOT NULL,
  `block` smallint(6) NOT NULL,
  `itemset` smallint(6) NOT NULL,
  `MaxDurability` smallint(6) NOT NULL,
  `ZoneNameID` smallint(6) NOT NULL,
  `mapid` smallint(6) NOT NULL,
  `bagfamily` smallint(6) NOT NULL,
  `TotemCategory` tinyint(4) NOT NULL,
  `socket_color_1` tinyint(4) NOT NULL,
  `socket_content_1` int(30) NOT NULL default '0',
  `socket_color_2` tinyint(4) NOT NULL,
  `socket_content_2` int(30) NOT NULL default '0',
  `socket_color_3` tinyint(4) NOT NULL,
  `socket_content_3` int(30) NOT NULL default '0',
  `socket_bonus` smallint(6) NOT NULL default '0',
  `GemProperties` smallint(6) NOT NULL,
  `ReqDisenchantSkill` smallint(6) NOT NULL default '-1',
  `armorDamageModifier` float NOT NULL,
  `ExistingDuration` int(10) unsigned NOT NULL default '0',
  PRIMARY KEY  (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='Item System (WDB Data)';

-- ----------------------------
-- Table structure for items_extendedcost
-- ----------------------------
DROP TABLE IF EXISTS `items_extendedcost`;
CREATE TABLE `items_extendedcost` (
  `ItemId` int(30) NOT NULL,
  `CostId` int(30) NOT NULL,
  PRIMARY KEY  (`ItemId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for items_localized
-- ----------------------------
DROP TABLE IF EXISTS `items_localized`;
CREATE TABLE `items_localized` (
  `entry` int(30) NOT NULL,
  `language_code` varchar(5) NOT NULL,
  `name` varchar(255) NOT NULL,
  `description` varchar(255) NOT NULL,
  PRIMARY KEY  (`entry`,`language_code`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for loots
-- ----------------------------
DROP TABLE IF EXISTS `loots`;
CREATE TABLE `loots` (
  `loot_id` mediumint(6) NOT NULL auto_increment,
  `loot_creature` int(6) NOT NULL default '0',
  `loot_item` int(6) NOT NULL default '0',
  `loot_chance` float NOT NULL default '0',
  `loot_heroicchance` float NOT NULL default '0',
  `loot_min` smallint(6) NOT NULL default '0',
  `loot_max` smallint(6) NOT NULL default '0',
  `loot_ffa` tinyint(1) NOT NULL default '0',
  PRIMARY KEY  (`loot_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for loots_gameobject
-- ----------------------------
DROP TABLE IF EXISTS `loots_gameobject`;
CREATE TABLE `loots_gameobject` (
  `loot_id` mediumint(6) NOT NULL auto_increment,
  `loot_object` int(6) NOT NULL,
  `loot_item` int(6) NOT NULL,
  `loot_chance` float NOT NULL,
  `loot_heroicchance` float NOT NULL,
  `loot_min` smallint(6) NOT NULL,
  `loot_max` smallint(6) NOT NULL,
  `loot_ffa` tinyint(1) NOT NULL,
  PRIMARY KEY  (`loot_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for loots_skinning
-- ----------------------------
DROP TABLE IF EXISTS `loots_skinning`;
CREATE TABLE `loots_skinning` (
  `loot_id` mediumint(6) NOT NULL auto_increment,
  `loot_creature` int(6) NOT NULL,
  `loot_item` int(6) NOT NULL,
  `loot_chance` float NOT NULL,
  `loot_heroicchance` float NOT NULL,
  `loot_min` smallint(6) NOT NULL,
  `loot_max` smallint(6) NOT NULL,
  `loot_ffa` tinyint(1) NOT NULL,
  PRIMARY KEY  (`loot_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for npc_gossip_textid
-- ----------------------------
DROP TABLE IF EXISTS `npc_gossip_textid`;
CREATE TABLE `npc_gossip_textid` (
  `creatureid` smallint(6) NOT NULL,
  `textid` smallint(6) NOT NULL,
  PRIMARY KEY  (`creatureid`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='NPC System (Gossip Link)';

-- ----------------------------
-- Table structure for npc_monstersay
-- ----------------------------
DROP TABLE IF EXISTS `npc_monstersay`;
CREATE TABLE `npc_monstersay` (
  `entry` smallint(6) NOT NULL,
  `event` tinyint(4) NOT NULL default '0',
  `chance` float NOT NULL,
  `language` tinyint(4) NOT NULL default '0',
  `type` tinyint(4) NOT NULL default '0',
  `monstername` longtext character set utf8 collate utf8_unicode_ci,
  `text0` longtext character set utf8 collate utf8_unicode_ci,
  `text1` longtext character set utf8 collate utf8_unicode_ci,
  `text2` longtext character set utf8 collate utf8_unicode_ci,
  `text3` longtext character set utf8 collate utf8_unicode_ci,
  `text4` longtext character set utf8 collate utf8_unicode_ci,
  PRIMARY KEY  (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='NPC System (Monster Say)';

-- ----------------------------
-- Table structure for npc_vendor
-- ----------------------------
DROP TABLE IF EXISTS `npc_vendor`;
CREATE TABLE `npc_vendor` (
  `entry` mediumint(8) unsigned NOT NULL default '0',
  `item` mediumint(8) unsigned NOT NULL default '0',
  `sellamount` smallint(6) NOT NULL default '0',
  `maxcount` smallint(6) NOT NULL default '0',
  `incrtime` int(10) NOT NULL default '0',
  `extendedcost` mediumint(8) unsigned NOT NULL default '0',
  `currentcount` smallint(6) NOT NULL default '0',
  `lastrefill` int(10) unsigned NOT NULL default '0',
  PRIMARY KEY  (`entry`,`item`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 ROW_FORMAT=FIXED COMMENT='Npc System';

-- ----------------------------
-- Table structure for npctext
-- ----------------------------
DROP TABLE IF EXISTS `npctext`;
CREATE TABLE `npctext` (
  `entry` int(10) unsigned NOT NULL default '0',
  `prob0` float NOT NULL default '0',
  `text0_0` longtext character set utf8 collate utf8_unicode_ci NOT NULL,
  `text0_1` longtext character set utf8 collate utf8_unicode_ci NOT NULL,
  `lang0` int(10) unsigned NOT NULL default '0',
  `em0_0` int(10) unsigned NOT NULL default '0',
  `em0_1` int(10) unsigned NOT NULL default '0',
  `em0_2` int(10) unsigned NOT NULL default '0',
  `em0_3` int(10) unsigned NOT NULL default '0',
  `em0_4` int(10) unsigned NOT NULL default '0',
  `em0_5` int(10) unsigned NOT NULL default '0',
  `prob1` float NOT NULL default '0',
  `text1_0` longtext character set utf8 collate utf8_unicode_ci NOT NULL,
  `text1_1` longtext character set utf8 collate utf8_unicode_ci NOT NULL,
  `lang1` int(10) unsigned NOT NULL default '0',
  `em1_0` int(10) unsigned NOT NULL default '0',
  `em1_1` int(10) unsigned NOT NULL default '0',
  `em1_2` int(10) unsigned NOT NULL default '0',
  `em1_3` int(10) unsigned NOT NULL default '0',
  `em1_4` int(10) unsigned NOT NULL default '0',
  `em1_5` int(10) unsigned NOT NULL default '0',
  `prob2` float NOT NULL default '0',
  `text2_0` longtext character set utf8 collate utf8_unicode_ci NOT NULL,
  `text2_1` longtext character set utf8 collate utf8_unicode_ci NOT NULL,
  `lang2` int(10) unsigned NOT NULL default '0',
  `em2_0` int(10) unsigned NOT NULL default '0',
  `em2_1` int(10) unsigned NOT NULL default '0',
  `em2_2` int(10) unsigned NOT NULL default '0',
  `em2_3` int(10) unsigned NOT NULL default '0',
  `em2_4` int(10) unsigned NOT NULL default '0',
  `em2_5` int(10) unsigned NOT NULL default '0',
  `prob3` float NOT NULL default '0',
  `text3_0` longtext character set utf8 collate utf8_unicode_ci NOT NULL,
  `text3_1` longtext character set utf8 collate utf8_unicode_ci NOT NULL,
  `lang3` int(10) unsigned NOT NULL default '0',
  `em3_0` int(10) unsigned NOT NULL default '0',
  `em3_1` int(10) unsigned NOT NULL default '0',
  `em3_2` int(10) unsigned NOT NULL default '0',
  `em3_3` int(10) unsigned NOT NULL default '0',
  `em3_4` int(10) unsigned NOT NULL default '0',
  `em3_5` int(10) unsigned NOT NULL default '0',
  `prob4` float NOT NULL default '0',
  `text4_0` longtext character set utf8 collate utf8_unicode_ci NOT NULL,
  `text4_1` longtext character set utf8 collate utf8_unicode_ci NOT NULL,
  `lang4` int(10) unsigned NOT NULL default '0',
  `em4_0` int(10) unsigned NOT NULL default '0',
  `em4_1` int(10) unsigned NOT NULL default '0',
  `em4_2` int(10) unsigned NOT NULL default '0',
  `em4_3` int(10) unsigned NOT NULL default '0',
  `em4_4` int(10) unsigned NOT NULL default '0',
  `em4_5` int(10) unsigned NOT NULL default '0',
  `prob5` float NOT NULL default '0',
  `text5_0` longtext character set utf8 collate utf8_unicode_ci NOT NULL,
  `text5_1` longtext character set utf8 collate utf8_unicode_ci NOT NULL,
  `lang5` int(10) unsigned NOT NULL default '0',
  `em5_0` int(10) unsigned NOT NULL default '0',
  `em5_1` int(10) unsigned NOT NULL default '0',
  `em5_2` int(10) unsigned NOT NULL default '0',
  `em5_3` int(10) unsigned NOT NULL default '0',
  `em5_4` int(10) unsigned NOT NULL default '0',
  `em5_5` int(10) unsigned NOT NULL default '0',
  `prob6` float NOT NULL default '0',
  `text6_0` longtext character set utf8 collate utf8_unicode_ci NOT NULL,
  `text6_1` longtext character set utf8 collate utf8_unicode_ci NOT NULL,
  `lang6` int(10) unsigned NOT NULL default '0',
  `em6_0` int(10) unsigned NOT NULL default '0',
  `em6_1` int(10) unsigned NOT NULL default '0',
  `em6_2` int(10) unsigned NOT NULL default '0',
  `em6_3` int(10) unsigned NOT NULL default '0',
  `em6_4` int(10) unsigned NOT NULL default '0',
  `em6_5` int(10) unsigned NOT NULL default '0',
  `prob7` float NOT NULL default '0',
  `text7_0` longtext character set utf8 collate utf8_unicode_ci NOT NULL,
  `text7_1` longtext character set utf8 collate utf8_unicode_ci NOT NULL,
  `lang7` int(10) unsigned NOT NULL default '0',
  `em7_0` int(10) unsigned NOT NULL default '0',
  `em7_1` int(10) unsigned NOT NULL default '0',
  `em7_2` int(10) unsigned NOT NULL default '0',
  `em7_3` int(10) unsigned NOT NULL default '0',
  `em7_4` int(10) unsigned NOT NULL default '0',
  `em7_5` int(10) unsigned NOT NULL default '0',
  UNIQUE KEY `entry` (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='NPC System';

-- ----------------------------
-- Table structure for playercreateinfo
-- ----------------------------
DROP TABLE IF EXISTS `playercreateinfo`;
CREATE TABLE `playercreateinfo` (
  `Index` tinyint(3) unsigned NOT NULL auto_increment,
  `race` tinyint(3) unsigned NOT NULL default '0',
  `factiontemplate` int(10) unsigned NOT NULL default '0',
  `class` tinyint(3) unsigned NOT NULL default '0',
  `mapID` int(10) unsigned NOT NULL default '0',
  `zoneID` int(10) unsigned NOT NULL default '0',
  `positionX` float NOT NULL default '0',
  `positionY` float NOT NULL default '0',
  `positionZ` float NOT NULL default '0',
  `displayID` smallint(5) unsigned NOT NULL default '0',
  `PowerType` tinyint(3) unsigned NOT NULL default '0',
  `BaseStrength` tinyint(3) unsigned NOT NULL default '0',
  `BaseAgility` tinyint(3) unsigned NOT NULL default '0',
  `BaseStamina` tinyint(3) unsigned NOT NULL default '0',
  `BaseIntellect` tinyint(3) unsigned NOT NULL default '0',
  `BaseSpirit` tinyint(3) unsigned NOT NULL default '0',
  `BaseHealth` int(10) unsigned NOT NULL default '0',
  `BasePower` int(10) unsigned NOT NULL default '0',
  `mindmg` float NOT NULL default '0',
  `maxdmg` float NOT NULL default '0',
  PRIMARY KEY  (`Index`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='Player System';

-- ----------------------------
-- Table structure for playercreateinfo_bars
-- ----------------------------
DROP TABLE IF EXISTS `playercreateinfo_bars`;
CREATE TABLE `playercreateinfo_bars` (
  `race` tinyint(3) unsigned default NULL,
  `class` tinyint(3) unsigned default NULL,
  `button` int(10) unsigned default NULL,
  `action` int(10) unsigned default NULL,
  `type` int(10) unsigned default NULL,
  `misc` int(10) unsigned default NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='Player System';

-- ----------------------------
-- Table structure for playercreateinfo_items
-- ----------------------------
DROP TABLE IF EXISTS `playercreateinfo_items`;
CREATE TABLE `playercreateinfo_items` (
  `race` tinyint(3) unsigned NOT NULL default '0',
  `class` tinyint(3) unsigned NOT NULL default '0',
  `protoid` int(10) unsigned NOT NULL default '0',
  `slotid` tinyint(3) unsigned NOT NULL default '0',
  `amount` int(10) unsigned NOT NULL default '0'
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='Player System';

-- ----------------------------
-- Table structure for playercreateinfo_skills
-- ----------------------------
DROP TABLE IF EXISTS `playercreateinfo_skills`;
CREATE TABLE `playercreateinfo_skills` (
  `race` tinyint(3) unsigned NOT NULL default '0',
  `class` tinyint(3) unsigned NOT NULL default '0',
  `skillid` int(10) unsigned NOT NULL default '0',
  `level` int(10) unsigned NOT NULL default '0',
  `maxlevel` int(10) unsigned NOT NULL default '0'
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='Player System';

-- ----------------------------
-- Table structure for playercreateinfo_spells
-- ----------------------------
DROP TABLE IF EXISTS `playercreateinfo_spells`;
CREATE TABLE `playercreateinfo_spells` (
  `race` tinyint(3) unsigned NOT NULL default '0',
  `class` tinyint(3) unsigned NOT NULL default '0',
  `spellid` smallint(5) unsigned NOT NULL default '0'
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='Player System';

-- ----------------------------
-- Table structure for questfinishers
-- ----------------------------
DROP TABLE IF EXISTS `questfinishers`;
CREATE TABLE `questfinishers` (
  `entry` bigint(20) NOT NULL auto_increment,
  `type` mediumint(9) NOT NULL,
  `typeid` mediumint(9) NOT NULL,
  `questid` mediumint(9) NOT NULL,
  PRIMARY KEY  (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='InnoDB free: 351232 kB';

-- ----------------------------
-- Table structure for quests
-- ----------------------------
DROP TABLE IF EXISTS `quests`;
CREATE TABLE `quests` (
  `id` int(11) NOT NULL default '0',
  `NextQuest` int(11) NOT NULL default '0',
  `Title` varchar(255) NOT NULL default 'No Quest Title',
  `Zone` int(11) NOT NULL default '0',
  `Type` int(11) NOT NULL default '0',
  `Flags` int(11) NOT NULL default '0',
  `SpecialFlags` int(11) NOT NULL default '0',
  `Level_Start` smallint(3) NOT NULL default '0',
  `Level_Normal` smallint(6) NOT NULL default '0',
  `Required_Quest1` int(11) NOT NULL default '0',
  `Required_Quest2` int(11) NOT NULL default '0',
  `Required_Quest3` int(11) NOT NULL default '0',
  `Required_Quest4` int(11) NOT NULL default '0',
  `Required_Race` int(11) NOT NULL default '0',
  `Required_Class` int(11) NOT NULL default '0',
  `Required_TradeSkill` int(11) NOT NULL default '0',
  `Required_TradeSkillValue` int(11) NOT NULL default '0',
  `Required_Reputation1` int(11) NOT NULL default '0',
  `Required_Reputation1_Faction` int(11) NOT NULL default '0',
  `Required_Reputation2` int(11) NOT NULL default '0',
  `Required_Reputation2_Faction` int(11) NOT NULL default '0',
  `Text_Objectives` text NOT NULL,
  `Text_Description` text NOT NULL,
  `Text_End` text NOT NULL,
  `Text_Incomplete` text NOT NULL,
  `Text_Complete` text NOT NULL,
  `Reward_XP` int(11) NOT NULL default '0',
  `Reward_Gold` int(11) NOT NULL default '0',
  `Reward_Spell` int(11) NOT NULL default '0',
  `Reward_SpellCast` int(11) NOT NULL default '0',
  `Reward_Reputation1` int(11) NOT NULL default '0',
  `Reward_Reputation1_Faction` int(11) NOT NULL default '0',
  `Reward_Reputation2` int(11) NOT NULL default '0',
  `Reward_Reputation2_Faction` int(11) NOT NULL default '0',
  `Reward_Reputation3` int(11) NOT NULL default '0',
  `Reward_Reputation3_Faction` int(11) NOT NULL default '0',
  `Reward_Item1` int(11) NOT NULL default '0',
  `Reward_Item1_Count` smallint(3) unsigned NOT NULL default '0',
  `Reward_Item2` int(11) NOT NULL default '0',
  `Reward_Item2_Count` smallint(3) unsigned NOT NULL default '0',
  `Reward_Item3` int(11) NOT NULL default '0',
  `Reward_Item3_Count` smallint(3) unsigned NOT NULL default '0',
  `Reward_Item4` int(11) NOT NULL default '0',
  `Reward_Item4_Count` smallint(3) unsigned NOT NULL default '0',
  `Reward_Item5` int(11) NOT NULL default '0',
  `Reward_Item5_Count` smallint(3) unsigned NOT NULL default '0',
  `Reward_Item6` int(11) NOT NULL default '0',
  `Reward_Item6_Count` smallint(3) unsigned NOT NULL default '0',
  `Reward_StaticItem1` int(11) NOT NULL default '0',
  `Reward_StaticItem1_Count` smallint(3) unsigned NOT NULL default '0',
  `Reward_StaticItem2` int(11) NOT NULL default '0',
  `Reward_StaticItem2_Count` smallint(3) unsigned NOT NULL default '0',
  `Reward_StaticItem3` int(11) NOT NULL default '0',
  `Reward_StaticItem3_Count` smallint(3) unsigned NOT NULL default '0',
  `Reward_StaticItem4` int(11) NOT NULL default '0',
  `Reward_StaticItem4_Count` smallint(3) unsigned NOT NULL default '0',
  `Time_Limit` int(11) NOT NULL default '0',
  `Objective_Trigger1` int(11) NOT NULL default '0',
  `Objective_Trigger2` int(11) NOT NULL default '0',
  `Objective_Trigger3` int(11) NOT NULL default '0',
  `Objective_Trigger4` int(11) NOT NULL default '0',
  `Objective_Cast1` int(11) NOT NULL default '0',
  `Objective_Cast2` int(11) NOT NULL default '0',
  `Objective_Cast3` int(11) NOT NULL default '0',
  `Objective_Cast4` int(11) NOT NULL default '0',
  `Objective_Kill1` int(11) NOT NULL default '0',
  `Objective_Kill1_Count` smallint(6) NOT NULL default '0',
  `Objective_Kill2` int(11) NOT NULL default '0',
  `Objective_Kill2_Count` smallint(6) NOT NULL default '0',
  `Objective_Kill3` int(11) NOT NULL default '0',
  `Objective_Kill3_Count` smallint(6) NOT NULL default '0',
  `Objective_Kill4` int(11) NOT NULL default '0',
  `Objective_Kill4_Count` smallint(6) NOT NULL default '0',
  `Objective_Item1` int(11) NOT NULL default '0',
  `Objective_Item1_Count` smallint(6) NOT NULL default '0',
  `Objective_Item2` int(11) NOT NULL default '0',
  `Objective_Item2_Count` smallint(6) NOT NULL default '0',
  `Objective_Item3` int(11) NOT NULL default '0',
  `Objective_Item3_Count` smallint(6) NOT NULL default '0',
  `Objective_Item4` int(11) NOT NULL default '0',
  `Objective_Item4_Count` smallint(6) NOT NULL default '0',
  `Objective_Deliver1` int(11) NOT NULL default '0',
  `Objective_Text1` varchar(255) NOT NULL default '',
  `Objective_Text2` varchar(255) NOT NULL default '',
  `Objective_Text3` varchar(255) NOT NULL default '',
  `Objective_Text4` varchar(255) NOT NULL default '',
  `SuggestedPlayers` tinyint(3) NOT NULL,
  `PointMap` int(11) NOT NULL,
  `PointX` float NOT NULL,
  `PointY` float NOT NULL,
  `PointOpt` int(11) NOT NULL,
  `MoneyAtMaxLevel` int(11) NOT NULL,
  `IsActive` tinyint(3) NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for queststarters
-- ----------------------------
DROP TABLE IF EXISTS `queststarters`;
CREATE TABLE `queststarters` (
  `entry` bigint(20) NOT NULL auto_increment,
  `type` mediumint(9) NOT NULL,
  `typeid` mediumint(9) NOT NULL,
  `questid` mediumint(9) default NULL,
  PRIMARY KEY  (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Table structure for spawns_creatures
-- ----------------------------
DROP TABLE IF EXISTS `spawns_creatures`;
CREATE TABLE `spawns_creatures` (
  `spawn_id` int(11) NOT NULL auto_increment,
  `spawn_entry` int(10) NOT NULL default '0',
  `spawn_positionX` float NOT NULL default '0',
  `spawn_positionY` float NOT NULL default '0',
  `spawn_positionZ` float NOT NULL default '0',
  `spawn_orientation` float NOT NULL default '0',
  `spawn_range` int(10) NOT NULL default '0',
  `spawn_map` int(10) NOT NULL default '0',
  `spawn_movetype` smallint(6) NOT NULL default '0',
  `spawn_displayid` smallint(6) NOT NULL default '0',
  `spawn_faction` smallint(6) NOT NULL,
  `spawn_mount` smallint(6) NOT NULL default '0',
  `spawn_flags` bigint(20) NOT NULL default '0',
  `spawn_bytes0` int(11) NOT NULL default '0',
  `spawn_bytes1` int(11) NOT NULL default '0',
  `spawn_bytes2` int(11) NOT NULL default '0',
  `spawn_emotestate` smallint(6) NOT NULL default '0',
  `spawn_standstate` tinyint(4) NOT NULL default '0',
  PRIMARY KEY  (`spawn_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for spawns_gameobjects
-- ----------------------------
DROP TABLE IF EXISTS `spawns_gameobjects`;
CREATE TABLE `spawns_gameobjects` (
  `spawn_id` int(11) NOT NULL auto_increment,
  `spawn_entry` int(10) NOT NULL default '0',
  `spawn_positionX` float NOT NULL default '0',
  `spawn_positionY` float NOT NULL default '0',
  `spawn_positionZ` float NOT NULL default '0',
  `spawn_orientation` float NOT NULL default '0',
  `spawn_orientation1` float NOT NULL default '0',
  `spawn_orientation2` float NOT NULL default '0',
  `spawn_orientation3` float NOT NULL default '0',
  `spawn_orientation4` float NOT NULL default '0',
  `spawn_map` int(10) NOT NULL default '0',
  `spawn_state` tinyint(4) unsigned NOT NULL default '0',
  `spawn_flags` int(6) NOT NULL default '0',
  `spawn_faction` int(30) NOT NULL default '0',
  `spawn_scale` float NOT NULL default '0',
  PRIMARY KEY  (`spawn_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for spells_teleport_coords
-- ----------------------------
DROP TABLE IF EXISTS `spells_teleport_coords`;
CREATE TABLE `spells_teleport_coords` (
  `id` mediumint(9) NOT NULL,
  `name` char(255) character set utf8 collate utf8_unicode_ci NOT NULL,
  `mapId` smallint(5) unsigned NOT NULL,
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `position_z` float NOT NULL,
  `totrigger` smallint(6) NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='World System (Spell Teleport Coords)';

-- ----------------------------
-- Table structure for trainer_defs
-- ----------------------------
DROP TABLE IF EXISTS `trainer_defs`;
CREATE TABLE `trainer_defs` (
  `entry` int(11) unsigned NOT NULL default '0',
  `required_skill` int(11) unsigned NOT NULL default '0',
  `required_skillvalue` int(11) unsigned default '0',
  `req_class` int(11) unsigned NOT NULL default '0',
  `trainer_type` int(11) unsigned NOT NULL default '0',
  `trainer_ui_window_message` text,
  `can_train_gossip_textid` int(11) NOT NULL,
  `cannot_train_gossip_textid` int(11) NOT NULL,
  PRIMARY KEY  (`entry`),
  UNIQUE KEY `entry` (`entry`),
  KEY `entry_2` (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COMMENT='Trainer System';

-- ----------------------------
-- Table structure for trainer_spells
-- ----------------------------
DROP TABLE IF EXISTS `trainer_spells`;
CREATE TABLE `trainer_spells` (
  `entry` int(11) unsigned NOT NULL default '0',
  `spellid` mediumint(9) unsigned NOT NULL default '0',
  `spellcost` int(11) unsigned NOT NULL default '0',
  `reqspell` int(11) unsigned NOT NULL default '0',
  `reqskill` int(11) unsigned NOT NULL default '0',
  `reqskillvalue` int(11) unsigned NOT NULL default '0',
  `reqlevel` int(11) unsigned NOT NULL default '0',
  `deletespell` int(11) unsigned NOT NULL default '0',
  `is_prof` tinyint(4) unsigned NOT NULL default '0',
  `is_cast` tinyint(3) unsigned NOT NULL default '0',
  PRIMARY KEY  (`entry`,`spellid`),
  KEY `entry` (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COMMENT='Trainer System';

-- ----------------------------
-- Table structure for weather
-- ----------------------------
DROP TABLE IF EXISTS `weather`;
CREATE TABLE `weather` (
  `weather_zone` smallint(6) NOT NULL default '0',
  `weather_type` tinyint(3) unsigned NOT NULL default '0',
  `weather_intensity` double NOT NULL default '0',
  `weather_aviableTypes` varchar(255) NOT NULL default '0',
  PRIMARY KEY  (`weather_zone`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for world_cmdteleports
-- ----------------------------
DROP TABLE IF EXISTS `world_cmdteleports`;
CREATE TABLE `world_cmdteleports` (
  `id` smallint(6) NOT NULL auto_increment,
  `name` varchar(100) NOT NULL,
  `MapId` smallint(6) NOT NULL,
  `positionX` float NOT NULL,
  `positionY` float NOT NULL,
  `positionZ` float NOT NULL,
  PRIMARY KEY  (`id`),
  UNIQUE KEY `Name` (`name`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COMMENT='World System (Teleport Locations)';
