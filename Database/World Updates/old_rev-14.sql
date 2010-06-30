ALTER TABLE items MODIFY COLUMN TotemCategory int(10) NOT NULL default '0';
ALTER TABLE items MODIFY COLUMN maxcount int(10) NOT NULL default '0';
ALTER TABLE items MODIFY COLUMN `Unique` int(10) NOT NULL default '0';