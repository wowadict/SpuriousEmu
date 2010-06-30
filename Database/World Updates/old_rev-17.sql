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
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COMMENT='Creature System (Creature''s Movement)';
