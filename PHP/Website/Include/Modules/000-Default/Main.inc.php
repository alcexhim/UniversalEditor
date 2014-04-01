<?php
	namespace UniversalEditor\PublicWebsite\Modules;
	
	use WebFX\System;
	use WebFX\Module;
	use WebFX\ModulePage;
	
	use UniversalEditor\PublicWebsite\Pages\MainPage;
	
	System::$Modules[] = new Module("com.universaleditor.PublicWebsite.Default", array
	(
		new ModulePage("", function($path)
		{
			$page = new MainPage();
			$page->Render();
			return true;
		})
	));
?>