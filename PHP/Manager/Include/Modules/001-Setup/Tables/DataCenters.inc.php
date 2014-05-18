<?php
	use DataFX\DataFX;
	use DataFX\Table;
	use DataFX\Column;
	use DataFX\ColumnValue;
	use DataFX\Record;
	use DataFX\RecordColumn;
	
	$tables[] = new Table("DataCenters", "datacenter_", array
	(
		// 			$name, $dataType, $size, $value, $allowNull, $primaryKey, $autoIncrement
		new Column("ID", "INT", null, null, false, true, true),
		new Column("Title", "VARCHAR", 256, null, true),
		new Column("Description", "LONGTEXT", null, null, true),
		new Column("HostName", "VARCHAR", 256, null, true)
	),
	array
	(
		new Record(array
		(
			new RecordColumn("Title", "Default"),
			new RecordColumn("Description", "The initial data center configured for PhoenixSNS tenanted hosting."),
			new RecordColumn("HostName", "www.yourdomain.com")
		))
	));
?>