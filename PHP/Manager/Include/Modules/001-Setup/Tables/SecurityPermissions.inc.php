<?php
	use DataFX\DataFX;
	use DataFX\Table;
	use DataFX\Column;
	use DataFX\ColumnValue;
	use DataFX\Record;
	use DataFX\RecordColumn;
	
	use PhoenixSNS\Objects\UserPresenceStatus;
	use PhoenixSNS\Objects\UserProfileVisibility;
	
	$tables[] = new Table("SecurityPermissions", "permission_", array
	(
		// 			$name, $dataType, $size, $value, $allowNull, $primaryKey, $autoIncrement
		new Column("ID", "INT", null, null, false, true, true),
		new Column("TenantID", "INT", null, null, false),
		new Column("Name", "VARCHAR", 50, null, false),
		new Column("Title", "LONGTEXT", null, null, false)
	), array
	(
		// 1
		new Record(array
		(
			new RecordColumn("Name", "GroupCreate"),
			new RecordColumn("Title", "Create a group")
		)),
		new Record(array
		(
			new RecordColumn("Name", "GroupModify"),
			new RecordColumn("Title", "Modify groups created by others")
		)),
		new Record(array
		(
			new RecordColumn("Name", "GroupDelete"),
			new RecordColumn("Title", "Delete groups created by others")
		)),
		new Record(array
		(
			new RecordColumn("Name", "UserBan"),
			new RecordColumn("Title", "Ban a user from the network")
		)),
		// 5
		new Record(array
		(
			new RecordColumn("Name", "PlaceCreate"),
			new RecordColumn("Title", "Create a Place as a normal user")
		)),
		new Record(array
		(
			new RecordColumn("Name", "PlaceCreateOfficial"),
			new RecordColumn("Title", "Create a Place as the official administrative user")
		)),
		new Record(array
		(
			new RecordColumn("Name", "PersonalDefaultPageModify"),
			new RecordColumn("Title", "Modify your own Default Page")
		))
	));
	
	$tables[] = new Table("SecurityGroups", "group_", array
	(
		// 			$name, $dataType, $size, $value, $allowNull, $primaryKey, $autoIncrement
		new Column("ID", "INT", null, null, false, true, true),
		new Column("Title", "VARCHAR", 50, null, false),
		new Column("ParentGroupID", "INT", null, null, true)
	), array
	(
		new Record(array
		(
			new RecordColumn("Title", "Administrator"),
			new RecordColumn("ParentGroupID", ColumnValue::Undefined)
		)),
		new Record(array
		(
			new RecordColumn("Title", "Moderator"),
			new RecordColumn("ParentGroupID", 3)
		)),
		new Record(array
		(
			new RecordColumn("Title", "Member")
		))
	));
	
	$tables[] = new Table("SecurityGroupPermissions", "grouppermission_", array
	(
		// 			$name, $dataType, $size, $value, $allowNull, $primaryKey, $autoIncrement
		new Column("GroupID", "INT", null, null, false),
		new Column("PermissionID", "INT", null, null, false)
	), array
	(
		new Record(array
		(
			new RecordColumn("GroupID", 1), // Administrator
			new RecordColumn("PermissionID", 6) // (all moderator permissions) + PlaceCreateOfficial
		)),
		
		// MODERATOR PERMISSIONS
		new Record(array
		(
			new RecordColumn("GroupID", 2), // moderator
			new RecordColumn("PermissionID", 2) // (all member permissions) + GroupModify
		)),
		new Record(array
		(
			new RecordColumn("GroupID", 2), // moderator
			new RecordColumn("PermissionID", 4) // (all member permissions) + UserBan
		)),
		
		// MEMBER PERMISSIONS
		new Record(array
		(
			new RecordColumn("GroupID", 3), // member
			new RecordColumn("PermissionID", 1) // GroupCreate
		)),
		new Record(array
		(
			new RecordColumn("GroupID", 3), // member
			new RecordColumn("PermissionID", 5) // PlaceCreate
		)),
		new Record(array
		(
			new RecordColumn("GroupID", 3), // member
			new RecordColumn("PermissionID", 7) // PersonalDefaultPageModify
		))
	));
?>