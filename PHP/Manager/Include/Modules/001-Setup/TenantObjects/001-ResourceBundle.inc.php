<?php
	use Objectify\Objects\DataType;
	
	use Objectify\Objects\MultipleInstanceProperty;
	use Objectify\Objects\SingleInstanceProperty;
	
	use Objectify\Objects\TenantObjectProperty;
	use Objectify\Objects\TenantObjectInstanceProperty;
	use Objectify\Objects\TenantObjectInstancePropertyValue;
	use Objectify\Objects\TenantObjectMethodParameter;
	
	use Objectify\Objects\TenantEnumerationChoice;
	
	$objResourceObject = $tenant->CreateObject("ResourceObject", "Represents a single resource object (StyleSheet, Script, etc.) that is loaded in a ResourceBundle on the Objectify tenant.", array
	(
		// property name, data type, default value, value required?, enumeration, require choice from enumeration?
		new TenantObjectInstanceProperty("Name", DataType::GetByName("Text"))
	));
	
	$objStyleSheet = $tenant->CreateObject("StyleSheet", "Represents a style sheet resource object, containing a set of rules that determine how particular elements appear in a page.", array
	(
	), $objResourceObject);
	
	$objScriptResourceObject = $tenant->CreateObject("ScriptResourceObject", "Represents a script resource object, containing a single pointer to a ClientScript (executable code that can be run on the client).", array
	(
		new TenantObjectInstanceProperty("ClientScript", DataType::GetByName("SingleInstance"), new SingleInstanceProperty(array(), array($objClientScript)))
	), $objResourceObject);
	
	$object = $tenant->CreateObject("ResourceBundle", "Contains a bundle of resources (StyleSheets, Scripts, etc.) that are loaded in with the Objectify tenant.", array
	(
		// property name, data type, default value, value required?, enumeration, require choice from enumeration?
		new TenantObjectInstanceProperty("Name", DataType::GetByName("Text")),
		new TenantObjectInstanceProperty("ResourceObjects", DataType::GetByName("MultipleInstance"), new MultipleInstanceProperty(array(), array($objResourceObject)))
	));
?>