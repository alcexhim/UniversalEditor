<?php
	use Objectify\Objects\DataType;
	
	use Objectify\Objects\MultipleInstanceProperty;
	use Objectify\Objects\SingleInstanceProperty;
	
	use Objectify\Objects\TenantProperty;
	
	use Objectify\Objects\TenantObjectProperty;
	use Objectify\Objects\TenantObjectInstanceProperty;
	use Objectify\Objects\TenantObjectInstancePropertyValue;
	use Objectify\Objects\TenantObjectMethodParameter;
	use Objectify\Objects\TenantQueryParameter;
	
	use Objectify\Objects\TenantEnumerationChoice;
	
	// Set up the basic configuration
	$tenant->CreateProperty(new TenantProperty("ApplicationTitle", DataType::GetByName("Text"), "The title of your application. This is displayed in various areas around the site.", "My Application"));
	$tenant->CreateProperty(new TenantProperty("ApplicationDescription", DataType::GetByName("Text"), "A short description of your application. This will appear in search results and other areas that use the HTML META description attribute.", "A versatile, modern, data-driven Web application powered by Objectify."));
	
	// Install the resource bundles
	$objResourceBundle = $tenant->GetObject("ResourceBundle");
	$instRBCommon = $objResourceBundle->CreateInstance(array
	(
		new TenantObjectInstancePropertyValue("Name", "Common")
	));
	$instRBDefault = $objResourceBundle->CreateInstance(array
	(
		new TenantObjectInstancePropertyValue("Name", "Default")
	));
	
	$tenant->CreateProperty(new TenantProperty
	(
		"ResourceBundles", DataType::GetByName("MultipleInstance"), "The resource bundles that are loaded with this tenant.", new MultipleInstanceProperty
		(
			array($instRBDefault),
			array($objResourceBundle)
		)
	));
	
?>