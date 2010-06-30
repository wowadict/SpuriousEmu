ALTER TABLE `quests` ADD `rewtalents` INT(10) DEFAULT '0' NOT NULL AFTER `Flags`;
ALTER TABLE `quests` ADD `RewTitleId` INT(10) DEFAULT '0' NOT NULL AFTER `Flags`;
ALTER TABLE `spawns_creatures` ADD `spawn_equipslot1` mediumint(8) DEFAULT '0' NOT NULL AFTER `spawn_standstate`;
ALTER TABLE `spawns_creatures` ADD `spawn_equipslot2` mediumint(8) DEFAULT '0' NOT NULL AFTER `spawn_equipslot1`;
ALTER TABLE `spawns_creatures` ADD `spawn_equipslot3` mediumint(8) DEFAULT '0' NOT NULL AFTER `spawn_equipslot2`;
ALTER TABLE `playercreateinfo` ROW_FORMAT=FIXED;
