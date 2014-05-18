<?php
	use DataFX\DataFX;
	use DataFX\Table;
	use DataFX\Column;
	use DataFX\ColumnValue;
	use DataFX\Record;
	use DataFX\RecordColumn;
	use DataFX\TableKey;
	use DataFX\TableKeyColumn;
	use DataFX\TableForeignKey;
	use DataFX\TableForeignKeyColumn;
	
	$tblLanguages = new Table("Languages", "language_", array
	(
		// 			$name, $dataType, $size, $value, $allowNull, $primaryKey, $autoIncrement
		new Column("ID", "INT", null, null, false, true, true),
		new Column("Name", "VARCHAR", 64, null, false)
	));
	$tables[] = $tblLanguages;
?>