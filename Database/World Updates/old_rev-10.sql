ALTER TABLE quests MODIFY COLUMN Objective_Kill1_Count smallint(6) NOT NULL default '0';
ALTER TABLE quests MODIFY COLUMN Objective_Kill2_Count smallint(6) NOT NULL default '0';
ALTER TABLE quests MODIFY COLUMN Objective_Kill3_Count smallint(6) NOT NULL default '0';
ALTER TABLE quests MODIFY COLUMN Objective_Kill4_Count smallint(6) NOT NULL default '0';
ALTER TABLE quests MODIFY COLUMN Objective_Item1_Count smallint(6) NOT NULL default '0';
ALTER TABLE quests MODIFY COLUMN Objective_Item2_Count smallint(6) NOT NULL default '0';
ALTER TABLE quests MODIFY COLUMN Objective_Item3_Count smallint(6) NOT NULL default '0';
ALTER TABLE quests MODIFY COLUMN Objective_Item4_Count smallint(6) NOT NULL default '0';
ALTER TABLE npc_vendor MODIFY COLUMN item mediumint(9) NOT NULL default '0';
ALTER TABLE npc_vendor MODIFY COLUMN sellamount smallint(6) NOT NULL default '-1';
ALTER TABLE npc_vendor MODIFY COLUMN maxcount tinyint(4) NOT NULL default '-1';
ALTER TABLE npc_vendor MODIFY COLUMN incrtime int(10) NOT NULL default '-1';
DROP TABLE IF EXISTS `trainer_spells`;
CREATE TABLE `trainer_spells` (
  `entry` int(11) unsigned NOT NULL default '0',
  `spellid` int(11) unsigned NOT NULL default '0',
  `spellcost` int(11) unsigned NOT NULL default '0',
  `reqspell` int(11) unsigned NOT NULL default '0',
  `reqskill` int(11) unsigned NOT NULL default '0',
  `reqskillvalue` int(11) unsigned NOT NULL default '0',
  `reqlevel` int(11) unsigned NOT NULL default '0',
  `deletespell` int(11) unsigned NOT NULL default '0',
  `is_prof` tinyint(1) unsigned NOT NULL default '0',
  `is_cast` tinyint(1) unsigned NOT NULL default '0',
  PRIMARY KEY  (`entry`,`spellid`),
  KEY `entry` (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COMMENT='Trainer System';DROP TABLE IF EXISTS `playercreateinfo`;
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

DROP TABLE IF EXISTS `playercreateinfo_items`;
CREATE TABLE `playercreateinfo_items` (
  `race` tinyint(3) unsigned NOT NULL default '0',
  `class` tinyint(3) unsigned NOT NULL default '0',
  `protoid` int(10) unsigned NOT NULL default '0',
  `slotid` tinyint(3) unsigned NOT NULL default '0',
  `amount` int(10) unsigned NOT NULL default '0'
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='Player System';

DROP TABLE IF EXISTS `playercreateinfo_skills`;
CREATE TABLE `playercreateinfo_skills` (
  `race` tinyint(3) unsigned NOT NULL default '0',
  `class` tinyint(3) unsigned NOT NULL default '0',
  `skillid` int(10) unsigned NOT NULL default '0',
  `level` int(10) unsigned NOT NULL default '0',
  `maxlevel` int(10) unsigned NOT NULL default '0'
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='Player System';

DROP TABLE IF EXISTS `playercreateinfo_spells`;
CREATE TABLE `playercreateinfo_spells` (
  `race` tinyint(3) unsigned NOT NULL default '0',
  `class` tinyint(3) unsigned NOT NULL default '0',
  `spellid` smallint(5) unsigned NOT NULL default '0'
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='Player System';

DROP TABLE IF EXISTS `account_data`;
CREATE TABLE `account_data` (
  `account_id` int(4) NOT NULL,
  `account_time0` int(11) unsigned NOT NULL,
  `account_data0` blob NOT NULL,
  `account_time1` int(11) unsigned NOT NULL,
  `account_data1` blob NOT NULL,
  `account_time2` int(11) unsigned NOT NULL,
  `account_data2` blob NOT NULL,
  `account_time3` int(11) unsigned NOT NULL,
  `account_data3` blob NOT NULL,
  `account_time4` int(11) unsigned NOT NULL,
  `account_data4` blob NOT NULL,
  `account_time5` int(11) unsigned NOT NULL,
  `account_data5` blob NOT NULL,
  `account_time6` int(11) unsigned NOT NULL,
  `account_data6` blob NOT NULL,
  `account_time7` int(11) unsigned NOT NULL,
  `account_data7` blob NOT NULL,
  PRIMARY KEY  (`account_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
