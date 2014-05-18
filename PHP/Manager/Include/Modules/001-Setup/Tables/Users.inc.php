<?php
	use DataFX\DataFX;
	use DataFX\Table;
	use DataFX\Column;
	use DataFX\ColumnValue;
	use DataFX\Record;
	use DataFX\RecordColumn;
	
	$tables[] = new Table("Users", "user_", array
	(
		// 			$name, $dataType, $size, $value, $allowNull, $primaryKey, $autoIncrement
		new Column("ID", "INT", null, null, false, true, true),
		new Column("LoginID", "VARCHAR", 50, null, false),
		new Column("PasswordHash", "VARCHAR", 256, null, false),
		new Column("PasswordSalt", "VARCHAR", 32, null, false)
	));
?>