<?php
	use DataFX\DataFX;
	use DataFX\Table;
	use DataFX\Column;
	use DataFX\ColumnValue;
	use DataFX\Record;
	use DataFX\RecordColumn;
	
	$tables[] = new Table("TenantDataCenters", "tdc_", array
	(
		// 			$name, $dataType, $size, $value, $allowNull, $primaryKey, $autoIncrement
		new Column("TenantID", "INT", null, null, true),
		new Column("DataCenterID", "INT", null, null, true)
	),
	array
	(
		new Record(array
		(
			new RecordColumn("TenantID", 1),
			new RecordColumn("DataCenterID", 1)
		))
	));
?>