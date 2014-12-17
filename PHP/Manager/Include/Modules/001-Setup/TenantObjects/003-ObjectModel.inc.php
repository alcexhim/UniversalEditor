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
	
	$tenant->CreateObject("ObjectModel",
	array
	(
		new TenantStringTableEntry($langEnglish, "Object Model")
	),
	array
	(
		new TenantStringTableEntry($langEnglish, "A model that provides the in-memory, DataFormat-agnostic representation of document data.")
	),
	array
	(
		new TenantObjectInstanceProperty("Name", DataType::GetByName("Text"))
	));
?>