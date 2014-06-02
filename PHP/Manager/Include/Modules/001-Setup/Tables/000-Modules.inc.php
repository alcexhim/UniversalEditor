<?php
	use DataFX\DataFX;
	use DataFX\Table;
	use DataFX\Column;
	use DataFX\ColumnValue;
	use DataFX\Record;
	use DataFX\RecordColumn;
	use DataFX\TableForeignKey;
	use DataFX\TableForeignKeyColumn;
	
	$tblModules = new Table("Modules", "module_", array
	(
		// 			$name, $dataType, $size, $value, $allowNull, $primaryKey, $autoIncrement
		new Column("ID", "INT", null, null, false, true, true),
		new Column("Title", "VARCHAR", 256, null, true),
		new Column("Description", "LONGTEXT", null, null, true),
		new Column("Enabled", "INT", null, 1, false)
	),
	array
	(
		// Discussions (V2) provides all the functionality of V1 Pages, Groups, and Forums
		new Record(array
		(
			new RecordColumn("Title", "Discussions"),
			new RecordColumn("Description", "Provides various types of discussion boards, pages, groups, and forums that allow your users to connect and communicate about specific topics.")
		)),
		new Record(array
		(
			new RecordColumn("Title", "Market"),
			new RecordColumn("Description", "Provides a virtual market system that allows users to buy and sell virtual items, create their own brands and stores, and item trading.")
		)),
		new Record(array
		(
			new RecordColumn("Title", "Brands"),
			new RecordColumn("Description", "Allows users to create their own brands and stores to sell their own virtual items.")
		)),
		new Record(array
		(
			new RecordColumn("Title", "Trading"),
			new RecordColumn("Description", "Allows users to trade items with each other. Items can be set to not be tradable, or only be tradable under certain conditions.")
		)),
		new Record(array
		(
			new RecordColumn("Title", "World"),
			new RecordColumn("Description", "Provides a virtual world that is divided into Places. Users can have one or more personal Places, and complete access control is allowed.")
		)),
		new Record(array
		(
			new RecordColumn("Title", "Partners"),
			new RecordColumn("Description", "Allows you to promote other organizations and display advertisements on your social network.")
		)),
		new Record(array
		(
			new RecordColumn("Title", "Arcade"),
			new RecordColumn("Description", "Enables the hosting of games inside the social network, with integrated redemption rewards and leaderboard statistics tracking.")
		))
	));
	$tables[] = $tblModules;
	
	$tblModuleMenuItems = new Table("ModuleMenuItems", "menuitem_", array
	(
		// 			$name, $dataType, $size, $value, $allowNull, $primaryKey, $autoIncrement
		new Column("ID", "INT", null, null, false, true, true),
		new Column("ModuleID", "INT", null, null, false),
		new Column("Title", "VARCHAR", 256, null, false),
		new Column("Description", "LONGTEXT", null, null, true),
		new Column("TargetURL", "LONGTEXT", null, null, true),
		new Column("TargetScript", "LONGTEXT", null, null, true)
	),
	array
	(
		new Record(array
		(
			new RecordColumn("ModuleID", 2),
			new RecordColumn("Title", "Market"),
			new RecordColumn("Description", "Buy, sell, and trade resources and items, and play Chance to win mystery rewards"),
			new RecordColumn("TargetURL", "~/market"),
			new RecordColumn("TargetScript", ColumnValue::Undefined)
		)),
		new Record(array
		(
			new RecordColumn("ModuleID", 5),
			new RecordColumn("Title", "World"),
			new RecordColumn("Description", "Explore a virtual world with your very own animated avatar"),
			new RecordColumn("TargetURL", "~/world"),
			new RecordColumn("TargetScript", ColumnValue::Undefined)
		)),
		new Record(array
		(
			new RecordColumn("ModuleID", 6),
			new RecordColumn("Title", "Partners"),
			new RecordColumn("Description", "See who we've partnered with to bring you even more awesome experiences, and learn about becoming a partner yourself"),
			new RecordColumn("TargetURL", "~/partners"),
			new RecordColumn("TargetScript", ColumnValue::Undefined)
		)),
		new Record(array
		(
			new RecordColumn("ModuleID", 7),
			new RecordColumn("Title", "Arcade"),
			new RecordColumn("Description", "Play games and make friends in the Arcade."),
			new RecordColumn("TargetURL", "~/arcade"),
			new RecordColumn("TargetScript", ColumnValue::Undefined)
		))
	));
	$tblModuleMenuItems->ForeignKeys = array
	(
		new TableForeignKey("ModuleID", new TableForeignKeyColumn($tblModules, $tblModules->GetColumnByName("ID")))
	);
	$tables[] = $tblModuleMenuItems;
	
	$tblModuleObjects = new Table("ModuleObjects", "object_", array
	(
		// 			$name, $dataType, $size, $value, $allowNull, $primaryKey, $autoIncrement
		new Column("ID", "INT", null, null, false, true, true),
		new Column("ModuleID", "INT", null, null, true),
		new Column("ParentObjectID", "INT", null, null, true),
		new Column("Title", "VARCHAR", 256, null, true),
		new Column("Description", "LONGTEXT", null, null, true)
	),
	array
	(
		new Record(array
		(
			new RecordColumn("ModuleID", 7),
			new RecordColumn("Title", "Game")
		))
	));
	$tblModuleObjects->ForeignKeys = array
	(
		new TableForeignKey("ModuleID", new TableForeignKeyColumn($tblModules, $tblModules->GetColumnByName("ID"))),
		new TableForeignKey("ParentObjectID", new TableForeignKeyColumn($tblModuleObjects, $tblModuleObjects->GetColumnByName("ID")))
	);
	$tables[] = $tblModuleObjects;
	
	$tblModulePages = new Table("ModulePages", "modulepage_", array
	(
		// 			$name, $dataType, $size, $value, $allowNull, $primaryKey, $autoIncrement
		new Column("ID", "INT", null, null, false, true, true),
		new Column("ModuleID", "INT", null, null, true),
		new Column("ParentPageID", "INT", null, null, true),
		new Column("URL", "VARCHAR", 1024, null, true),
		new Column("Content", "LONGTEXT", null, null, true)
	),
	array
	(
		new Record(array
		(
			new RecordColumn("ModuleID", 1),
			new RecordColumn("URL", "groups")
		)),
		new Record(array
		(
			new RecordColumn("ModuleID", 1),
			new RecordColumn("URL", "pages")
		)),
		new Record(array
		(
			new RecordColumn("ModuleID", 1),
			new RecordColumn("URL", "forums")
		))
	));
	$tblModulePages->ForeignKeys = array
	(
		new TableForeignKey("ModuleID", new TableForeignKeyColumn($tblModules, $tblModules->GetColumnByName("ID"))),
		new TableForeignKey("ParentPageID", new TableForeignKeyColumn($tblModulePages, $tblModulePages->GetColumnByName("ID")))
	);
	$tables[] = $tblModulePages;
?>