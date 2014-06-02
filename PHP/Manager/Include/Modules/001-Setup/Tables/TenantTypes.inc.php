<?php
	use DataFX\DataFX;
	use DataFX\Table;
	use DataFX\Column;
	use DataFX\ColumnValue;
	use DataFX\Record;
	use DataFX\RecordColumn;
	
	$tables[] = new Table("TenantTypes", "tenanttype_", array
	(
		// 			$name, $dataType, $size, $value, $allowNull, $primaryKey, $autoIncrement
		new Column("ID", "INT", null, null, true),
		new Column("Title", "VARCHAR", 256, null, true),
		new Column("Description", "LONGTEXT", null, null, true)
	),
	array
	(
		new Record(array
		(
			new RecordColumn("Title", "Production"),
			new RecordColumn("Description", "The production tenant type. Usually the only tenant type visible to regular users. Add more (such as production, development, sandbox, sandbox preview, implementation preview, implementation) as needed.")
		)),
		new Record(array
		(
			new RecordColumn("Title", "Development"),
			new RecordColumn("Description", "The development tenant type. Usually used to create new features and modules before pushing them to production. Add more (such as production, development, sandbox, sandbox preview, implementation preview, implementation) as needed.")
		))
	));
?>