<?php
	use Objectify\Objects\DataType;
	
	use Objectify\Objects\MultipleInstanceProperty;
	use Objectify\Objects\SingleInstanceProperty;
	
	use Objectify\Objects\TenantObjectProperty;
	use Objectify\Objects\TenantObjectInstanceProperty;
	use Objectify\Objects\TenantObjectInstancePropertyValue;
	use Objectify\Objects\TenantObjectMethodParameter;
	use Objectify\Objects\TenantStringTableEntry;
	
	use Objectify\Objects\TenantEnumerationChoice;
	
	$tenant->CreateObject("DataFormat",
	array
	(
		new TenantStringTableEntry($langEnglish, "Data Format")
	),
	array
	(
		new TenantStringTableEntry($langEnglish, "A format used to serialize and deserialize documents.")
	),
	array
	(
		new TenantObjectInstanceProperty("Name", DataType::GetByName("Text"))
	));
?>