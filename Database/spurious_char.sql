/*
MySQL Data Transfer
Source Host: localhost
Source Database: accounts
Target Host: localhost
Target Database: accounts
Date: 11/25/2008 4:34:01 PM
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for accounts
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts` (
  `account` varchar(30) NOT NULL default '',
  `password` varchar(30) NOT NULL default '',
  `plevel` mediumint(3) unsigned NOT NULL default '0',
  `email` varchar(50) NOT NULL default '',
  `joindate` varchar(10) NOT NULL default '00-00-0000',
  `last_sshash` varchar(90) NOT NULL default '',
  `last_ip` varchar(15) NOT NULL default '',
  `last_login` varchar(100) NOT NULL default '0000-00-00',
  `banned` tinyint(1) unsigned NOT NULL default '0',
  `expansion` tinyint(1) unsigned NOT NULL default '2',
  `account_id` int(4) NOT NULL auto_increment,
  PRIMARY KEY  (`account_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for addonsinfo
-- ----------------------------
DROP TABLE IF EXISTS `addonsinfo`;
CREATE TABLE `addonsinfo` (
  `id` smallint(6) NOT NULL auto_increment,
  `addOn_Name` varchar(255) NOT NULL default '',
  `addOn_Key` bigint(20) NOT NULL default '0',
  `addOn_State` tinyint(3) unsigned NOT NULL default '0',
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for arena_members
-- ----------------------------
DROP TABLE IF EXISTS `arena_members`;
CREATE TABLE `arena_members` (
  `member_id` int(10) NOT NULL default '0',
  `member_team` int(10) NOT NULL default '0',
  `member_type` tinyint(3) NOT NULL default '0',
  `member_playedweek` int(10) unsigned NOT NULL default '0',
  `member_wonsweek` int(10) unsigned NOT NULL default '0',
  `member_playedseason` int(10) unsigned NOT NULL default '0',
  `member_wonsseason` int(10) unsigned NOT NULL default '0',
  `member_personalrating` int(10) unsigned NOT NULL default '0',
  PRIMARY KEY  (`member_id`,`member_team`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for arena_teams
-- ----------------------------
DROP TABLE IF EXISTS `arena_teams`;
CREATE TABLE `arena_teams` (
  `arena_id` int(10) unsigned NOT NULL auto_increment,
  `arena_name` char(255) NOT NULL,
  `arena_captain` int(10) unsigned NOT NULL default '0',
  `arena_type` tinyint(3) unsigned NOT NULL default '0',
  `arena_emblemstyle` int(10) unsigned NOT NULL default '0',
  `arena_emblemcolor` int(10) unsigned NOT NULL default '0',
  `arena_borderstyle` int(10) unsigned NOT NULL default '0',
  `arena_bordercolor` int(10) unsigned NOT NULL default '0',
  `arena_background` int(10) unsigned NOT NULL default '0',
  `arena_rating` int(10) unsigned NOT NULL default '1500',
  `arena_rank` int(10) unsigned NOT NULL default '0',
  `arena_weekgames` int(10) unsigned NOT NULL default '0',
  `arena_weekwins` int(10) unsigned NOT NULL default '0',
  `arena_seasongames` int(10) unsigned NOT NULL default '0',
  `arena_seasonwins` int(10) unsigned NOT NULL default '0',
  PRIMARY KEY  (`arena_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for auctionhouse
-- ----------------------------
DROP TABLE IF EXISTS `auctionhouse`;
CREATE TABLE `auctionhouse` (
  `auction_id` int(11) NOT NULL auto_increment,
  `auction_bid` int(11) NOT NULL,
  `auction_buyout` int(11) NOT NULL,
  `auction_timeleft` int(11) NOT NULL,
  `auction_bidder` int(11) NOT NULL default '0',
  `auction_owner` int(11) NOT NULL,
  `auction_itemId` mediumint(11) NOT NULL,
  `auction_itemCount` tinyint(4) unsigned NOT NULL default '1',
  `auction_itemGUID` int(11) NOT NULL,
  PRIMARY KEY  (`auction_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for bans
-- ----------------------------
DROP TABLE IF EXISTS `bans`;
CREATE TABLE `bans` (
  `ip` varchar(100) NOT NULL default '',
  `date` date default '0000-00-00',
  `reason` varchar(100) default NULL,
  `who` varchar(100) default NULL,
  PRIMARY KEY  (`ip`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for characters
-- ----------------------------
DROP TABLE IF EXISTS `characters`;
CREATE TABLE `characters` (
  `account_id` mediumint(3) unsigned NOT NULL default '0',
  `char_guid` int(8) NOT NULL auto_increment,
  `char_name` varchar(21) NOT NULL default '',
  `char_level` tinyint(1) unsigned NOT NULL default '0',
  `char_xp` mediumint(3) NOT NULL default '0',
  `char_access` tinyint(1) unsigned NOT NULL default '0',
  `char_online` tinyint(1) unsigned NOT NULL default '0',
  `char_positionX` float NOT NULL default '0',
  `char_positionY` float NOT NULL default '0',
  `char_positionZ` float NOT NULL default '0',
  `char_map_id` smallint(2) NOT NULL default '0',
  `char_zone_id` smallint(2) NOT NULL default '0',
  `char_orientation` float NOT NULL default '0',
  `char_model` smallint(2) NOT NULL default '0',
  `char_moviePlayed` tinyint(1) NOT NULL default '0',
  `bindpoint_positionX` float NOT NULL default '0',
  `bindpoint_positionY` float NOT NULL default '0',
  `bindpoint_positionZ` float NOT NULL default '0',
  `bindpoint_map_id` smallint(2) NOT NULL default '0',
  `bindpoint_zone_id` smallint(2) NOT NULL default '0',
  `char_guildId` int(1) NOT NULL default '0',
  `char_guildRank` tinyint(1) unsigned NOT NULL default '0',
  `char_guildPNote` varchar(255) NOT NULL default '',
  `char_guildOffNote` varchar(255) NOT NULL default '',
  `char_race` tinyint(1) unsigned NOT NULL default '0',
  `char_class` tinyint(1) unsigned NOT NULL default '0',
  `char_gender` tinyint(1) unsigned NOT NULL default '0',
  `char_skin` tinyint(1) unsigned NOT NULL default '0',
  `char_face` tinyint(1) unsigned NOT NULL default '0',
  `char_hairStyle` tinyint(1) unsigned NOT NULL default '0',
  `char_hairColor` tinyint(1) unsigned NOT NULL default '0',
  `char_facialHair` tinyint(1) unsigned NOT NULL default '0',
  `char_outfitId` tinyint(1) unsigned NOT NULL default '0',
  `char_restState` tinyint(1) unsigned NOT NULL default '0',
  `char_mana` smallint(2) NOT NULL default '1',
  `char_energy` smallint(2) NOT NULL default '0',
  `char_rage` smallint(2) NOT NULL default '0',
  `char_life` smallint(2) NOT NULL default '1',
  `char_manaType` tinyint(1) unsigned NOT NULL default '0',
  `char_strength` tinyint(1) unsigned NOT NULL default '0',
  `char_agility` tinyint(1) unsigned NOT NULL default '0',
  `char_stamina` tinyint(1) unsigned NOT NULL default '0',
  `char_intellect` tinyint(1) unsigned NOT NULL default '0',
  `char_spirit` tinyint(1) unsigned NOT NULL default '0',
  `char_copper` int(6) unsigned NOT NULL default '0',
  `char_honorpoints` int(6) unsigned NOT NULL default '0',
  `char_arenapoints` int(6) unsigned NOT NULL default '0',
  `char_watchedFactionIndex` tinyint(1) unsigned NOT NULL default '255',
  `char_reputation` text NOT NULL,
  `char_spellList` text NOT NULL,
  `char_skillList` text NOT NULL,
  `char_tutorialFlags` varchar(255) NOT NULL default '',
  `char_taxiFlags` varchar(255) NOT NULL default '',
  `char_actionBar` text NOT NULL,
  `char_mapExplored` text NOT NULL,
  `force_restrictions` tinyint(1) unsigned NOT NULL default '0',
  `char_talentPoints` tinyint(1) unsigned NOT NULL default '0',
  `char_bankSlots` tinyint(1) unsigned NOT NULL default '0',
  PRIMARY KEY  (`char_guid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for characters_honor
-- ----------------------------
DROP TABLE IF EXISTS `characters_honor`;
CREATE TABLE `characters_honor` (
  `char_guid` bigint(20) NOT NULL default '0',
  `arena_currency` smallint(6) NOT NULL default '0',
  `honor_currency` smallint(6) NOT NULL default '0',
  `honor_title` tinyint(3) unsigned NOT NULL default '0',
  `honor_knownTitles` smallint(4) NOT NULL default '0',
  `honor_killsToday` smallint(11) NOT NULL default '0',
  `honor_killsYesterday` smallint(11) NOT NULL default '0',
  `honor_pointsToday` smallint(11) NOT NULL default '0',
  `honor_pointsYesterday` smallint(11) NOT NULL default '0',
  `honor_kills` mediumint(11) NOT NULL default '0',
  PRIMARY KEY  (`char_guid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for characters_instances
-- ----------------------------
DROP TABLE IF EXISTS `characters_instances`;
CREATE TABLE `characters_instances` (
  `char_guid` int(8) NOT NULL,
  `map` smallint(2) unsigned NOT NULL default '0',
  `instance` smallint(2) unsigned NOT NULL default '0',
  `expire` int(8) unsigned NOT NULL default '0'
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for characters_instances_group
-- ----------------------------
DROP TABLE IF EXISTS `characters_instances_group`;
CREATE TABLE `characters_instances_group` (
  `group_id` int(8) NOT NULL,
  `map` smallint(2) unsigned NOT NULL default '0',
  `instance` smallint(2) unsigned NOT NULL default '0',
  `expire` int(8) unsigned NOT NULL default '0'
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for characters_inventory
-- ----------------------------
DROP TABLE IF EXISTS `characters_inventory`;
CREATE TABLE `characters_inventory` (
  `item_guid` bigint(8) NOT NULL default '0',
  `item_id` smallint(2) unsigned NOT NULL default '0',
  `item_slot` tinyint(6) unsigned NOT NULL default '255',
  `item_bag` bigint(8) NOT NULL default '-1',
  `item_owner` bigint(8) NOT NULL default '0',
  `item_creator` bigint(8) NOT NULL default '0',
  `item_giftCreator` bigint(8) NOT NULL default '0',
  `item_stackCount` tinyint(1) unsigned NOT NULL default '0',
  `item_durability` smallint(2) NOT NULL default '0',
  `item_flags` smallint(11) NOT NULL default '0',
  `item_chargesLeft` tinyint(1) unsigned NOT NULL default '0',
  `item_textId` smallint(6) NOT NULL default '0',
  `item_enchantment` varchar(255) NOT NULL default '',
  `item_random_properties` smallint(6) NOT NULL default '0',
  PRIMARY KEY  (`item_guid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for characters_locations
-- ----------------------------
DROP TABLE IF EXISTS `characters_locations`;
CREATE TABLE `characters_locations` (
  `char_guid` int(8) NOT NULL auto_increment,
  `char_positionX` float NOT NULL default '0',
  `char_positionY` float NOT NULL default '0',
  `char_positionZ` float NOT NULL default '0',
  `char_map_id` smallint(2) NOT NULL default '0',
  `char_zone_id` smallint(2) NOT NULL default '0',
  `char_orientation` float NOT NULL default '0',
  PRIMARY KEY  (`char_guid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for characters_mail
-- ----------------------------
DROP TABLE IF EXISTS `characters_mail`;
CREATE TABLE `characters_mail` (
  `mail_id` smallint(5) NOT NULL auto_increment,
  `mail_sender` bigint(20) NOT NULL default '0',
  `mail_receiver` bigint(20) NOT NULL default '0',
  `mail_type` tinyint(3) unsigned NOT NULL default '0',
  `mail_stationary` smallint(4) NOT NULL default '41',
  `mail_subject` varchar(255) NOT NULL default '',
  `mail_body` varchar(255) NOT NULL default '',
  `mail_money` int(6) NOT NULL default '0',
  `mail_COD` smallint(6) NOT NULL default '0',
  `mail_time` int(6) NOT NULL default '30',
  `mail_read` tinyint(3) unsigned NOT NULL default '0',
  PRIMARY KEY  (`mail_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for characters_quests
-- ----------------------------
DROP TABLE IF EXISTS `characters_quests`;
CREATE TABLE `characters_quests` (
  `id` int(11) NOT NULL auto_increment,
  `char_guid` bigint(20) NOT NULL default '0',
  `quest_id` int(11) NOT NULL default '0',
  `quest_status` int(5) NOT NULL default '0',
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for characters_social
-- ----------------------------
DROP TABLE IF EXISTS `characters_social`;
CREATE TABLE `characters_social` (
  `char_guid` bigint(20) unsigned NOT NULL,
  `guid` bigint(20) unsigned NOT NULL,
  `note` varchar(255) NOT NULL,
  `flags` smallint(6) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for characters_tickets
-- ----------------------------
DROP TABLE IF EXISTS `characters_tickets`;
CREATE TABLE `characters_tickets` (
  `char_guid` bigint(20) NOT NULL default '0',
  `ticket_text` text NOT NULL,
  `ticket_x` float NOT NULL default '0',
  `ticket_y` float NOT NULL default '0',
  `ticket_z` float NOT NULL default '0',
  `ticket_map` int(8) unsigned NOT NULL default '0',
  PRIMARY KEY  (`char_guid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for guildbanktabs
-- ----------------------------
DROP TABLE IF EXISTS `guildbanktabs`;
CREATE TABLE `guildbanktabs` (
  `tab_id` tinyint(1) unsigned NOT NULL,
  `tab_guildid` int(11) unsigned NOT NULL,
  `tab_name` varchar(100) NOT NULL,
  `tab_icon` varchar(100) NOT NULL,
  `tab_text` varchar(500) NOT NULL,
  PRIMARY KEY  (`tab_id`,`tab_guildid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for guilds
-- ----------------------------
DROP TABLE IF EXISTS `guilds`;
CREATE TABLE `guilds` (
  `guild_id` int(11) NOT NULL auto_increment,
  `guild_name` varchar(255) NOT NULL,
  `guild_leader` int(11) NOT NULL default '0',
  `guild_MOTD` varchar(255) NOT NULL default '',
  `guild_info` varchar(255) NOT NULL default '',
  `guild_cYear` tinyint(3) unsigned NOT NULL default '0',
  `guild_cMonth` tinyint(3) unsigned NOT NULL default '0',
  `guild_cDay` tinyint(3) unsigned NOT NULL default '0',
  `guild_tEmblemStyle` tinyint(3) unsigned NOT NULL default '0',
  `guild_tEmblemColor` tinyint(3) unsigned NOT NULL default '0',
  `guild_tBorderStyle` tinyint(3) unsigned NOT NULL default '0',
  `guild_tBorderColor` tinyint(3) unsigned NOT NULL default '0',
  `guild_tBackgroundColor` tinyint(3) unsigned NOT NULL default '0',
  `guild_rank0` varchar(255) NOT NULL default 'Guild Master',
  `guild_rank0_Rights` int(11) NOT NULL default '61951',
  `guild_rank1` varchar(255) NOT NULL default 'Officer',
  `guild_rank1_Rights` int(11) NOT NULL default '67',
  `guild_rank2` varchar(255) NOT NULL default 'Veteran',
  `guild_rank2_Rights` int(11) NOT NULL default '67',
  `guild_rank3` varchar(255) NOT NULL default 'Member',
  `guild_rank3_Rights` int(11) NOT NULL default '67',
  `guild_rank4` varchar(255) NOT NULL default 'Initiate',
  `guild_rank4_Rights` int(11) NOT NULL default '67',
  `guild_rank5` varchar(255) NOT NULL default '',
  `guild_rank5_Rights` int(11) NOT NULL default '0',
  `guild_rank6` varchar(255) NOT NULL default '',
  `guild_rank6_Rights` int(11) NOT NULL default '0',
  `guild_rank7` varchar(255) NOT NULL default '',
  `guild_rank7_Rights` int(11) NOT NULL default '0',
  `guild_rank8` varchar(255) NOT NULL default '',
  `guild_rank8_Rights` int(11) NOT NULL default '0',
  `guild_rank9` varchar(255) NOT NULL default '',
  `guild_rank9_Rights` int(11) NOT NULL default '0',
  `guild_banktabs` tinyint(2) NOT NULL default '0',
  `guild_bankmoney` bigint(20) unsigned NOT NULL,
  PRIMARY KEY  (`guild_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for mail_items
-- ----------------------------
DROP TABLE IF EXISTS `mail_items`;
CREATE TABLE `mail_items` (
  `mail_id` smallint(5) NOT NULL,
  `item_guid` bigint(20) NOT NULL,
  PRIMARY KEY  (`item_guid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for petitions
-- ----------------------------
DROP TABLE IF EXISTS `petitions`;
CREATE TABLE `petitions` (
  `petition_id` int(11) NOT NULL,
  `petition_itemGuid` int(11) NOT NULL,
  `petition_owner` int(11) NOT NULL,
  `petition_name` varchar(255) NOT NULL,
  `petition_type` tinyint(3) unsigned NOT NULL,
  `petition_signedMembers` tinyint(3) unsigned NOT NULL,
  `petition_signedMember1` int(11) NOT NULL default '0',
  `petition_signedMember2` int(11) NOT NULL default '0',
  `petition_signedMember3` int(11) NOT NULL default '0',
  `petition_signedMember4` int(11) NOT NULL default '0',
  `petition_signedMember5` int(11) NOT NULL default '0',
  `petition_signedMember6` int(11) NOT NULL default '0',
  `petition_signedMember7` int(11) NOT NULL default '0',
  `petition_signedMember8` int(11) NOT NULL default '0',
  `petition_signedMember9` int(11) NOT NULL default '0',
  PRIMARY KEY  (`petition_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for playercooldowns
-- ----------------------------
DROP TABLE IF EXISTS `playercooldowns`;
CREATE TABLE `playercooldowns` (
  `char_guid` int(30) NOT NULL,
  `cooldown_type` int(30) NOT NULL COMMENT '0 is spell, 1 is item, 2 is spell category',
  `cooldown_misc` int(30) NOT NULL COMMENT 'spellid/itemid/category',
  `cooldown_expire_time` int(30) NOT NULL COMMENT 'expiring time',
  `cooldown_spellid` int(30) NOT NULL COMMENT 'spell that cast it',
  `cooldown_itemid` int(30) NOT NULL COMMENT 'item that cast it'
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for realms
-- ----------------------------
DROP TABLE IF EXISTS `realms`;
CREATE TABLE `realms` (
  `ws_name` varchar(50) NOT NULL default '',
  `ws_host` varchar(50) NOT NULL default '',
  `ws_port` int(5) NOT NULL default '0',
  `ws_status` tinyint(3) unsigned NOT NULL default '0',
  `ws_id` tinyint(3) unsigned NOT NULL default '0',
  `ws_type` tinyint(3) unsigned NOT NULL default '0',
  `ws_population` float(3,0) unsigned NOT NULL default '0',
  `ws_timezone` tinyint(3) NOT NULL default '1',
  `gmonly` tinyint(1) default '0',
  PRIMARY KEY  (`ws_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for tmpspawnedcorpses
-- ----------------------------
DROP TABLE IF EXISTS `tmpspawnedcorpses`;
CREATE TABLE `tmpspawnedcorpses` (
  `corpse_guid` bigint(20) NOT NULL default '0',
  `corpse_owner` bigint(20) NOT NULL default '0',
  `corpse_positionX` float NOT NULL default '0',
  `corpse_positionY` float NOT NULL default '0',
  `corpse_positionZ` float NOT NULL default '0',
  `corpse_orientation` float NOT NULL default '0',
  `corpse_mapId` int(10) NOT NULL default '0',
  `corpse_bytes1` int(11) NOT NULL default '0',
  `corpse_bytes2` int(11) NOT NULL default '0',
  `corpse_model` int(10) NOT NULL default '0',
  `corpse_guild` int(10) NOT NULL default '0',
  `corpse_items` varchar(255) NOT NULL default '',
  PRIMARY KEY  (`corpse_guid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records 
-- ----------------------------
