<?php
	namespace Objectify\Modules;
	
	use WebFX\Controls\ButtonGroup;
	use WebFX\Controls\ButtonGroupButton;
	use WebFX\Controls\ButtonGroupButtonAlignment;
	
	use WebFX\Controls\Panel;
	
	use Objectify\ResourceBundle;
	use Objectify\Pages\DashboardPage;
	use Objectify\Pages\MainPage;
	use Objectify\Pages\ErrorPage;
	use Objectify\Pages\LoginPage;
	
	use Objectify\Objects\Tenant;
	use Objectify\Objects\TenantObjectMethodParameterValue;
	
	use Objectify\Objects\User;
	use Objectify\Objects\UserProfileVisibility;
	
	use WebFX\System;
	use WebFX\Module;
	use WebFX\ModulePage;
	
	require_once("lessc.inc.php");
	require_once("JShrink.inc.php");
	require_once("ResourceBundle.inc.php");
	
	function IsConfigured()
	{
		if (!(
			isset(System::$Configuration["Database.ServerName"]) &&
			isset(System::$Configuration["Database.DatabaseName"]) &&
			isset(System::$Configuration["Database.UserName"]) &&
			isset(System::$Configuration["Database.Password"]) &&
			isset(System::$Configuration["Database.TablePrefix"])
		))
		{
			return false;
		}
		
		global $MySQL;
		$query = "SHOW TABLES LIKE '" . System::$Configuration["Database.TablePrefix"] . "Users'";
		$result = $MySQL->query($query);
		if ($result->num_rows < 1) return false;
		return true;
	}
	
	function GetResourceBundles()
	{
		$ResourceBundles = array
		(
			new ResourceBundle("Common")
		);
		
		$tenant = Tenant::GetCurrent();
		
		// References to ResourceBundle objects are stored in a MultipleInstanceProperty called "ResourceBundles" on the tenant
		$bundles = $tenant->GetPropertyValue("ResourceBundles")->GetInstances();
		foreach ($bundles as $bundle)
		{
			$ResourceBundles[] = new ResourceBundle($bundle->GetPropertyValue("Name"));
		}
		return $ResourceBundles;
	}
	function CompileStyleSheets($compile = true)
	{
		global $RootPath;
		$ResourceBundles = GetResourceBundles();
		
		$FilePaths = array();
		
		$lesstext = "";
		foreach ($ResourceBundles as $bundle)
		{
			$lesstext .= $bundle->CompileStyleSheets();
		}
		if ($compile)
		{
			try
			{
				$less = new \lessc();
				$less->setFormatter("compressed");
				$csstext = $less->compile($lesstext);
				
				echo("/* compiled with lessphp v0.4.0 - GPLv3/MIT - http://leafo.net/lessphp */\n");
				echo("/* for human-readable source of this file, append ?compile=false to the file name */\n");
				echo($csstext);
			}
			catch (\Exception $e)
			{
				echo "/* " . $e->getMessage() . " */\n";
			}
		}
		else
		{
			echo($lesstext);
		}
	}
	function CompileScripts($compile = true)
	{
		global $RootPath;
		$ResourceBundles = GetResourceBundles();
		
		$FilePaths = array();
		
		$lesstext = "";
		foreach ($ResourceBundles as $bundle)
		{
			$lesstext .= $bundle->CompileScripts();
		}
		
		if ($compile)
		{
			try
			{
				$jstext = \JShrink\Minifier::minify($lesstext, array('flaggedComments' => false));
				
				echo("/* compiled with JShrink v0.5.2 - BSD 3-clause - https://github.com/tedivm/JShrink */\n");
				echo("/* for human-readable source of this file, append ?compile=false to the file name */\n");
				echo($jstext);
			}
			catch (\Exception $e)
			{
				echo "/* " . $e->getMessage() . " */\n";
			}
		}
		else
		{
			echo($lesstext);
		}
	}
	
	function IsValidUserOrGuest()
	{
		$CurrentTenant = Tenant::GetCurrent();
		
		if (!isset($_SESSION["CurrentUserName[" . $CurrentTenant->ID . "]"]) && !isset($_SESSION["CurrentPassword[" . $CurrentTenant->ID . "]"])) return true;
		$user = User::GetByLoginID($_SESSION["CurrentUserName[" . $CurrentTenant->ID . "]"]);
		if ($user == null) return true;
		
		return IsAuthenticated();
	}
	function IsAuthenticated()
	{
		$CurrentTenant = Tenant::GetCurrent();
		
		if (isset($_SESSION["CurrentUserName[" . $CurrentTenant->ID . "]"]) && isset($_SESSION["CurrentPassword[" . $CurrentTenant->ID . "]"]))
		{
			$user = $CurrentTenant->GetObject("User")->GetMethod("ValidateCredentials")->Execute(array
			(
				new TenantObjectMethodParameterValue("username", $_SESSION["CurrentUserName[" . $CurrentTenant->ID . "]"]),
				new TenantObjectMethodParameterValue("password", $_SESSION["CurrentPassword[" . $CurrentTenant->ID . "]"])
			));
			return ($user != null);
		}
		return false;
	}
	function IsModuleAuthenticationRequired($path)
	{
		switch ($path)
		{
			case "dashboard":
			case "world":
			{
				return true;
			}
		}
		return false;
	}
	
	System::$BeforeLaunchEventHandler = function($path)
	{
		if ($path[0] == "images" || $path[0] == "StyleSheet.css" || $path[0] == "Script.js" || ($path[0] == "account" && ($path[1] == "login.page" || $path[1] == "register.page"))) return true;
		
		// ensure our tenant has not expired yet
		$tenant = Tenant::GetByURL(System::$TenantName);
		if ($tenant == null || $tenant->IsExpired())
		{
			$page = new ErrorPage();
			$page->Message = "The specified tenant does not exist.  Please contact the site administrator to resolve this problem.";
			$page->Render();
			return false;
		}
		
		if (!IsConfigured() && $path[0] != "setup")
		{
			System::Redirect("~/setup");
			return false;
		}
		if (!IsValidUserOrGuest())
		{
			System::Redirect("~/account/login.page");
			return false;
		}
		
		if (!IsAuthenticated() && IsModuleAuthenticationRequired($path[0]))
		{
			System::Redirect("~/account/login.page");
			return false;
		}
		return true;
	};
	
	System::$Modules[] = new Module("net.Objectify.Default", array
	(
		new ModulePage("", function($path)
		{
			if (IsAuthenticated())
			{
				$tenant = Tenant::GetCurrent();
				$tobjUser = $tenant->GetObject("User");
				$instUser = $tobjUser->GetMethod("GetCurrentUser")->Execute();
				
				$propStartPage = $tobjUser->GetInstanceProperty("StartPage");
				
				$startPageSet = $instUser->HasPropertyValue($propStartPage);
				$startPage = $instUser->GetPropertyValue($propStartPage);
				
				if ($startPageSet)
				{
					/*
					$spi = $startPage->Instance;
					$spio = $startPage->Instance->ParentObject;
					$startPage = $spi->GetPropertyValue($spio->GetProperty("Value"));
					*/
					System::Redirect($startPage);
				}
				else
				{
					System::Redirect("~/dashboard");
				}
				return true;
			}
			
			$page = new MainPage();
			$page->Render();
			return true;
		}),
		new ModulePage("dashboard", function($path)
		{
			$page = new DashboardPage();
			$page->Render();
			return true;
		}),
		new ModulePage("account", array
		(
			new ModulePage("login.page", function($path)
			{
				$CurrentTenant = Tenant::GetCurrent();
				if ($CurrentTenant == null) return false;
				
				$page = new LoginPage();
				if (isset($_POST["member_username"]) && isset($_POST["member_password"]))
				{
					$object = $CurrentTenant->GetObject("User");
					$inst = $object->GetMethod("ValidateCredentials")->Execute(array
					(
						new TenantObjectMethodParameterValue("username", $_POST["member_username"]),
						new TenantObjectMethodParameterValue("password", $_POST["member_password"])
					));
					
					if ($inst != null)
					{
						$_SESSION["CurrentUserName[" . $CurrentTenant->ID . "]"] = $_POST["member_username"];
						$_SESSION["CurrentPassword[" . $CurrentTenant->ID . "]"] = $_POST["member_password"];
						
						if (isset($_SESSION["LoginRedirectURL"]))
						{
							System::Redirect($_SESSION["LoginRedirectURL"]);
						}
						else
						{
							System::Redirect("~/");
						}
						return true;
					}
					else
					{
						$page->InvalidCredentials = true;
					}
				}
				$page->Render();
				return true;
			}),
			new ModulePage("logout.page", function($path)
			{
				$CurrentTenant = Tenant::GetCurrent();
				
				$_SESSION["CurrentUserName[" . $CurrentTenant->ID . "]"] = null;
				$_SESSION["CurrentPassword[" . $CurrentTenant->ID . "]"] = null;
				System::Redirect("~/");
			})
		)),
		new ModulePage("images", function($path)
		{
			// load images from resources object
			global $RootPath;
			
			$bundle = "Common";
			$filename = implode("/", $path);
			if (isset($path[1]))
			{
				if ($path[1] != "")
				{
					$bundle = $path[0];
					array_shift($path);
					$filename = implode("/", $path);
				}
			}
			
			$imagePath = $RootPath . "/Resources/" . $bundle . "/Images/" . implode("/", $path);
			if (file_exists($imagePath))
			{
				header("Content-Type: " . mime_content_type($imagePath));
				readfile($imagePath);
				return true;
			}
			else
			{
				header("HTTP/1.1 404 Not Found");
				echo("The specified resource file was not found on this server.");
				return false;
			}
		}),
		new ModulePage("StyleSheet.css", function($path)
		{
			header("Content-Type: text/css");
			$compile = true;
			if (isset($_GET["compile"])) $compile = ($_GET["compile"] != "false");
			$lesstext = CompileStyleSheets($compile);
			echo($lesstext);
			return true;
		}),
		new ModulePage("Script.js", function($path)
		{
			// load style sheet from resources object
			header("Content-Type: text/javascript");
			$compile = true;
			if (isset($_GET["compile"])) $compile = ($_GET["compile"] != "false");
			$lesstext = CompileScripts($compile);
			echo($lesstext);
			return true;
		})
	));
?>