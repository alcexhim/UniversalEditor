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
	
	$tblTenants = new Table("Tenants", "tenant_", array
	(
		// 			$name, $dataType, $size, $value, $allowNull, $primaryKey, $autoIncrement
		new Column("ID", "INT", null, null, false, true, true),
		new Column("URL", "VARCHAR", 30, null, true),
		new Column("Description", "LONGTEXT", null, null, false),
		new Column("Status", "INT", null, null, true),
		new Column("TypeID", "INT", null, null, true),
		new Column("PaymentPlanID", "INT", null, null, true),
		new Column("BeginTimestamp", "DATETIME", null, null, true),
		new Column("EndTimestamp", "DATETIME", null, null, true)
	),
	array
	(
	));
	
	$tblTenants->UniqueKeys = array
	(
		new TableKey(array
		(
			new TableKeyColumn("URL")
		))
	);
	$tables[] = $tblTenants;
	
	$tblTenantProperties = new Table("TenantProperties", "property_", array
	(
		// 			$name, $dataType, $size, $value, $allowNull, $primaryKey, $autoIncrement
		new Column("ID", "INT", null, null, false, true, true),
		new Column("TenantID", "INT", null, null, true),
		new Column("Name", "VARCHAR", 256, null, false),
		new Column("Description", "LONGTEXT", null, null, true),
		new Column("DataTypeID", "INT", null, null, false),
		new Column("DefaultValue", "LONGBLOB", null, null, true)
	));
	$tblTenantProperties->ForeignKeys = array
	(
		new TableForeignKey("TenantID", new TableForeignKeyColumn($tblTenants, "ID")),
		new TableForeignKey("DataTypeID", new TableForeignKeyColumn($tblDataTypes, "ID"))
	);
	$tables[] = $tblTenantProperties;
	
	$tblTenantPropertyValues = new Table("TenantPropertyValues", "propval_", array
	(
		// 			$name, $dataType, $size, $value, $allowNull, $primaryKey, $autoIncrement
		new Column("PropertyID", "INT", null, null, false, true, true),
		new Column("Value", "LONGTEXT", null, null, true)
	));
	$tblTenantPropertyValues->ForeignKeys = array
	(
		new TableForeignKey("PropertyID", new TableForeignKeyColumn($tblTenantProperties, "ID"))
	);
	$tables[] = $tblTenantPropertyValues;
	
	$tblTenantModules = new Table("TenantModules", "tenantmodule_", array
	(
		// 			$name, $dataType, $size, $value, $allowNull, $primaryKey, $autoIncrement
		new Column("TenantID", "INT", null, null, false),
		new Column("ModuleID", "INT", null, null, false)
	));
	$tblTenantModules->ForeignKeys = array
	(
		new TableForeignKey("TenantID", new TableForeignKeyColumn($tblTenants, "ID"))
	);
	$tables[] = $tblTenantModules;
?>