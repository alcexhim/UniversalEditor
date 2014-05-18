<?php
	use WebFX\System;
	use WebFX\Module;
	use WebFX\ModulePage;
	
	use WebFX\Controls\ButtonGroup;
	use WebFX\Controls\ButtonGroupButton;
	
	use Objectify\Tenant\MasterPages\WebPage;
	
	use Objectify\Tenant\Pages\LoginPage;
	use Objectify\Tenant\Pages\MainPage;
	use Objectify\Tenant\Pages\ModuleMainPage;
	use Objectify\Tenant\Pages\ModuleManagementPage;
	
	use Objectify\Tenant\Pages\TenantPropertiesPage;
	use Objectify\Tenant\Pages\TenantManagementPage;
	use Objectify\Tenant\Pages\TenantModuleManagementPage;
	
	use Objectify\Tenant\Pages\TenantObjectManagementPage;
	
	use Objectify\Tenant\Pages\TenantObjectInstanceBrowsePage;
	
	use Objectify\Tenant\Pages\DataCenterMainPage;
	use Objectify\Tenant\Pages\DataCenterManagementPage;
	
	use Objectify\Tenant\Pages\TenantObjectMethodManagementPage;
	
	use Objectify\Tenant\Pages\ConfirmOperationPage;
	
	use Objectify\Objects\DataCenter;
	use Objectify\Objects\DataType;
	use Objectify\Objects\PaymentPlan;
	use Objectify\Objects\Tenant;
	use Objectify\Objects\TenantObject;
	use Objectify\Objects\TenantObjectMethod;
	use Objectify\Objects\TenantObjectInstanceMethod;
	use Objectify\Objects\TenantObjectInstanceProperty;
	use Objectify\Objects\TenantStatus;
	use Objectify\Objects\TenantType;
	use Objectify\Objects\TenantObjectMethodParameterValue;
	
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
		$query = "SHOW TABLES LIKE '" . System::$Configuration["Database.TablePrefix"] . "Tenants'";
		$result = $MySQL->query($query);
		if ($result->num_rows < 1) return false;
		return true;
	}
	
	function CheckCredentials($admun, $admpw)
	{
		return ($admun == System::GetConfigurationValue("Administration.UserName") && $admpw == System::GetConfigurationValue("Administration.Password"));
	}
	function IsAdministrator()
	{
		if (!isset($_SESSION["admun"]) || !isset($_SESSION["admpw"])) return false;
		
		$admun = $_SESSION["admun"];
		$admpw = $_SESSION["admpw"];
		
		return CheckCredentials($admun, $admpw);
	}
	
	System::$BeforeLaunchEventHandler = function($path)
	{
		if (!IsConfigured() && (!($path[0] == "setup")))
		{
			System::Redirect("~/setup");
			return true;
		}
		
		if (!IsAdministrator() && (!($path[0] == "account" && $path[1] == "login.page")) && (!($path[0] == "setup")) && (!($path[0] == "favicon.ico")))
		{
			$path1 = implode("/", $path);
			$_SESSION["LoginRedirectURL"] = "~/" . $path1;
			
			System::Redirect("~/account/login.page");
			return true;
		}
		return true;
	};
	
	System::$Modules[] = new Module("net.Objectify.TenantManager.Default", array
	(
		new ModulePage("", function($path)
		{
			$page = new WebPage();
			$page->BeginContent();
			
			$btng = new ButtonGroup("btng1");
			$btng->Items[] = new ButtonGroupButton("btnDataCenters", "Data Centers", null, "~/Images/Buttons/DataCenters.png", "~/datacenter");
			$btng->Items[] = new ButtonGroupButton("btnDataTypes", "Data Types", null, "~/Images/Buttons/DataTypes.png", "~/datatype");
			$btng->Items[] = new ButtonGroupButton("btnTenantTypes", "Tenant Types", null, "~/Images/Buttons/TenantTypes.png", "~/tenanttype");
			$btng->Items[] = new ButtonGroupButton("btnTenants", "Tenants", null, "~/Images/Buttons/Tenants.png", "~/tenant");
			$btng->Items[] = new ButtonGroupButton("btnModules", "Modules", null, "~/Images/Buttons/Modules.png", "~/module");
			$btng->Render();
			
			$page->EndContent();
		}),
		new ModulePage("debug", function($path)
		{
			global $MySQL;
			
			$page = new WebPage();
			$page->BeginContent();
			if (is_numeric($path[0]))
			{
				if ($_SERVER["REQUEST_METHOD"] == "POST" && $_POST["action"] == "delete")
				{
					$query = "DELETE FROM " . System::GetConfigurationValue("Database.TablePrefix") . "DebugMessages WHERE message_ID = " . $path[0];
					$result = $MySQL->query($query);
					System::Redirect("~/debug");
				}
				else
				{
					$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "DebugMessages WHERE message_ID = " . $path[0];
					$result = $MySQL->query($query);
					$values = $result->fetch_assoc();
					
					echo("<h1>Error Details</h1>");
					echo("<p>" . $values["message_Content"] . "</p>");
					
					echo("<h2>Parameters</h2>");
					echo("<table class=\"ListView\">");
					$query1 = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "DebugMessageParameters WHERE mp_MessageID = " . $values["message_ID"];
					$result1 = $MySQL->query($query1);
					$count1 = $result1->num_rows;
					echo("<tr>");
					echo("<th>Name</th>");
					echo("<th>Value</th>");
					echo("</tr>");
					for ($j = 0; $j < $count1; $j++)
					{
						$values1 = $result1->fetch_assoc();
						echo("<tr>");
						echo("<td>");
						echo($values1["mp_Name"]);
						echo("</td>");
						echo("<td>");
						echo($values1["mp_Value"]);
						echo("</td>");
						echo("</tr>");
					}
					echo("</table>");
					
					echo("<h2>Backtrace</h2>");
					echo("<table class=\"ListView\">");
					$query1 = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "DebugMessageBacktraces WHERE bt_MessageID = " . $values["message_ID"];
					$result1 = $MySQL->query($query1);
					$count1 = $result1->num_rows;
					echo("<tr>");
					echo("<th>File name</th>");
					echo("<th>Line number</th>");
					echo("</tr>");
					for ($j = 0; $j < $count1; $j++)
					{
						$values1 = $result1->fetch_assoc();
						echo("<tr>");
						echo("<td>");
						echo($values1["bt_FileName"]);
						echo("</td>");
						echo("<td>");
						echo($values1["bt_LineNumber"]);
						echo("</td>");
						echo("</tr>");
					}
					echo("</table>");
					echo("<div class=\"Buttons\">");
					echo("<form method=\"post\">");
					echo("<input type=\"hidden\" name=\"action\" value=\"delete\" />");
					echo("<input type=\"submit\" value=\"Delete Message\" />");
					echo("<a class=\"Button\" href=\"" . System::ExpandRelativePath("~/debug") . "\">Back to Messages</a>");
					echo("</form>");
					echo("</div>");
				}
			}
			else
			{
				if ($_SERVER["REQUEST_METHOD"] == "POST" && $_POST["action"] == "delete")
				{
					$query = "DELETE FROM " . System::GetConfigurationValue("Database.TablePrefix") . "DebugMessages";
					$result = $MySQL->query($query);
					System::Redirect("~/debug");
				}
				else
				{
					echo("<form method=\"post\">");
					echo("<input type=\"hidden\" name=\"action\" value=\"delete\" />");
					echo("<input type=\"submit\" value=\"Clear Messages\" />");
					echo("</form>");
					
					echo("<table class=\"ListView\">");
					echo("<tr>");
					echo("<th>Tenant</th>");
					echo("<th>Severity</th>");
					echo("<th>Message</th>");
					echo("<th>Timestamp</th>");
					echo("<th>IP Address</th>");
					echo("</tr>");
					
					$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "DebugMessages";
					$result = $MySQL->query($query);
					$count = $result->num_rows;
					for ($i = 0; $i < $count; $i++)
					{
						$values = $result->fetch_assoc();
						echo("<tr>");
						echo("<td>");
						$tenant = Tenant::GetByID($values["message_TenantID"]);
						if ($tenant != null)
						{
							echo("<a href=\"" . System::ExpandRelativePath("~/tenant/manage/" . $tenant->URL . "/") . "\">" . $tenant->URL . "</a>");
						}
						echo("</td>");
						echo("<td>");
						switch ($values["message_SeverityID"])
						{
						}
						echo("</td>");
						echo("<td>");
						echo("<a href=\"" . System::ExpandRelativePath("~/debug/" . $values["message_ID"]) . "\">");
						echo($values["message_Content"]);
						echo("</a>");
						echo("</td>");
						echo("<td>");
						echo($values["message_Timestamp"]);
						echo("</td>");
						echo("<td>");
						echo($values["message_IPAddress"]);
						echo("</td>");
						echo("</tr>");
					}
					echo("</table>");
				}
			}
			$page->EndContent();
			return true;
		}),
		new ModulePage("account", array
		(
			new ModulePage("login.page", function($path)
			{
				$page = new LoginPage();
				if ($_SERVER["REQUEST_METHOD"] == "POST")
				{
					if (isset($_POST["user_LoginID"]) && isset($_POST["user_Password"]))
					{
						$admun = $_POST["user_LoginID"];
						$admpw = $_POST["user_Password"];
						
						if (CheckCredentials($admun, $admpw))
						{
							$_SESSION["admun"] = $admun;
							$_SESSION["admpw"] = $admpw;
							
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
				}
				$page->Render();
				return true;
			}),
			new ModulePage("logout.page", function($path)
			{
				$_SESSION["admun"] = null;
				$_SESSION["admpw"] = null;
				System::Redirect("~/");
				return true;
			})
		)),
		new ModulePage("tenant", array
		(
			new ModulePage("", function($path)
			{
				$page = new MainPage();
				$page->Render();
				return true;
			}),
			new ModulePage("create", function($path)
			{
				if ($_SERVER["REQUEST_METHOD"] === "POST")
				{
					$tenant_URL = $_POST["tenant_URL"];
					$tenant_Description = $_POST["tenant_Description"];
					
					$tenant_DataCenters = array();
					foreach ($_POST as $key => $value)
					{
						if (substr($key, 0, strlen("tenant_DataCenter_")) == "tenant_DataCenter_")
						{
							$id = substr($key, strlen("tenant_DataCenter_") + 1);
							$tenant_DataCenters[] = DataCenter::GetByID($id);
						}
					}
					
					$tenant_Status = ($_POST["tenant_Status"] == 1 ? TenantStatus::Enabled : TenantStatus::Disabled);
					$tenant_Type = TenantType::GetByID($_POST["tenant_TypeID"]);
					$tenant_PaymentPlan = PaymentPlan::GetByID($_POST["tenant_PaymentPlanID"]);
					$tenant_BeginTimestamp = ($_POST["tenant_BeginTimestampValid"] == "1" ? null : $_POST["tenant_BeginTimestamp"]);
					$tenant_EndTimestamp = ($_POST["tenant_EndTimestampValid"] == "1" ? null : $_POST["tenant_EndTimestamp"]);
					
					$retval = Tenant::Create($tenant_URL, $tenant_Description, $tenant_Status, $tenant_Type, $tenant_PaymentPlan, $tenant_BeginTimestamp, $tenant_EndTimestamp, $tenant_DataCenters);
					
					if ($retval == null)
					{
						global $MySQL;
						echo($MySQL->error . " (" . $MySQL->errno . ")");
					}
					else
					{
						System::Redirect("~/tenant");
					}
				}
				else
				{
					$page = new TenantPropertiesPage();
					$page->Render();
					return true;
				}
			}),
			new ModulePage("modify", function($path)
			{
				if ($_SERVER["REQUEST_METHOD"] === "POST")
				{
					$tenant_URL = $_POST["tenant_URL"];
					
					$tenant = Tenant::GetByURL($path[0]);
					
					$tenant->URL = $_POST["tenant_URL"];
					$tenant->Description = $_POST["tenant_Description"];
					$tenant->Status = ($_POST["tenant_Status"] == 1 ? TenantStatus::Enabled : TenantStatus::Disabled);
					$tenant->Type = TenantType::GetByID($_POST["tenant_TypeID"]);
					$tenant->PaymentPlan = PaymentPlan::GetByID($_POST["tenant_PaymentPlanID"]);
					$tenant->BeginTimestamp = ($_POST["tenant_BeginTimestampValid"] == "1" ? null : $_POST["tenant_BeginTimestamp"]);
					$tenant->EndTimestamp = ($_POST["tenant_EndTimestampValid"] == "1" ? null : $_POST["tenant_EndTimestamp"]);
					
					$retval = $tenant->Update();
					
					if (!$retval)
					{
						global $MySQL;
						echo($MySQL->error . " (" . $MySQL->errno . ")");
					}
					else
					{
						System::Redirect("~/tenant");
					}
					return true;
				}
				else
				{
					$page = new TenantPropertiesPage();
					$page->Tenant = Tenant::GetByURL($path[0]);
					$page->Render();
					return true;
				}
			}),
			new ModulePage("clone", function($path)
			{
				if ($_SERVER["REQUEST_METHOD"] === "POST")
				{
					$tenant_URL = $_POST["tenant_URL"];
					$tenant_Description = $_POST["tenant_Description"];
					$tenant_Status = ($_POST["tenant_Status"] == 1 ? TenantStatus::Enabled : TenantStatus::Disabled);
					$tenant_Type = TenantType::GetByID($_POST["tenant_TypeID"]);
					$tenant_PaymentPlan = PaymentPlan::GetByID($_POST["tenant_PaymentPlanID"]);
					$tenant_BeginTimestamp = ($_POST["tenant_BeginTimestampValid"] == "1" ? null : $_POST["tenant_BeginTimestamp"]);
					$tenant_EndTimestamp = ($_POST["tenant_EndTimestampValid"] == "1" ? null : $_POST["tenant_EndTimestamp"]);
					
					$retval = Tenant::Create($tenant_URL, $tenant_Description, $tenant_Status, $tenant_Type, $tenant_PaymentPlan, $tenant_BeginTimestamp, $tenant_EndTimestamp);
					
					if ($retval == null)
					{
						global $MySQL;
						echo($MySQL->error . " (" . $MySQL->errno . ")");
					}
					else
					{
						System::Redirect("~/tenant");
					}
				}
				else
				{
					$page = new TenantPropertiesPage();
					$page->Tenant = Tenant::GetByURL($path[0]);
					$page->Render();
					return true;
				}
			}),
			new ModulePage("delete", function($path)
			{
				if ($_SERVER["REQUEST_METHOD"] === "POST")
				{
					if ($_POST["Confirm"] == "1")
					{
						$tenant = Tenant::GetByURL($path[0]);
						if ($tenant->Delete())
						{
							System::Redirect("~/tenant");
						}
						else
						{
							global $MySQL;
							echo($MySQL->error . " (" . $MySQL->errno . ")");
						}
					}
				}
				else
				{
					$page = new ConfirmOperationPage();
					$page->ReturnButtonURL = "~/tenant";
					$page->Message = "Are you sure you want to delete the tenant '" . $path[0] . "'? This action cannot be undone, and will destroy any and all data associated with that tenant.";
					$page->Render();
					return true;
				}
			}),
			new ModulePage("manage", function($path)
			{
				if ($path[1] == "")
				{
					$tenant = Tenant::GetByURL($path[0]);
					if ($_SERVER["REQUEST_METHOD"] == "POST")
					{
						$properties = $tenant->GetProperties();
						foreach ($properties as $property)
						{
							$tenant->SetPropertyValue($property, $_POST["Property_" . $property->ID]);
						}
						System::Redirect("~/tenant/manage/" . $path[0]);
						return true;
					}
					else
					{
						$page = new TenantManagementPage();
						$page->Tenant = $tenant;
						$page->Render();
						return true;
					}
				}
				else
				{
					switch ($path[1])
					{
						case "modules":
						{
							$page = new TenantModuleManagementPage();
							$page->Tenant = Tenant::GetByURL($path[0]);
							$page->Module = \Objectify\Objects\Module::GetByID($path[2]);
							$page->Render();
							break;
						}
						case "objects":
						{
							if ($path[2] == "")
							{
								// $page = new TenantObjectBrowsePage();
								// $page->CurrentTenant = Tenant::GetByURL($path[0]);
								// $page->Render();
							}
							else
							{
								switch ($path[3])
								{
									case "instances":
									{
										switch ($path[4])
										{
											case "":
											{
												$tenant = Tenant::GetByURL($path[0]);
												$object = TenantObject::GetByID($path[2]);
												
												$page = new TenantObjectInstanceBrowsePage();
												$page->CurrentTenant = $tenant;
												$page->CurrentObject = $object;
												$page->Render();
												break;
											}
										}
									}
									case "methods":
									{
										switch ($path[4])
										{
											case "static":
											{
												$tenant = Tenant::GetByURL($path[0]);
												$object = TenantObject::GetByID($path[2]);
												$method = TenantObjectMethod::GetByID($path[5]);
												
												if ($_SERVER["REQUEST_METHOD"] == "POST")
												{
													$method->CodeBlob = $_POST["method_CodeBlob"];
													$method->Update();
													
													System::Redirect("~/tenant/manage/" . $tenant->URL . "/objects/" . $object->ID);
													return true;
												}
												
												$page = new TenantObjectMethodManagementPage();
												$page->CurrentTenant = $tenant;
												$page->CurrentObject = $object;
												$page->CurrentMethod = $method;
												$page->Render();
												break;
											}
											case "instance":
											{
												$page = new TenantObjectMethodManagementPage();
												$page->CurrentTenant = Tenant::GetByURL($path[0]);
												$page->CurrentObject = TenantObject::GetByID($path[2]);
												$page->CurrentMethod = TenantObjectInstanceMethod::GetByID($path[5]);
												$page->Render();
												break;
											}
										}
										break;
									}
									case "":
									{
										$tenant = Tenant::GetByURL($path[0]);
										$object = TenantObject::GetByID($path[2]);
										
										if ($_SERVER["REQUEST_METHOD"] == "POST")
										{
											$count = $_POST["InstanceProperty_NewPropertyCount"];
											for ($i = $count; $i > 0; $i--)
											{
												$name = $_POST["InstanceProperty_" . $i . "_Name"];
												$dataType = DataType::GetByID($_POST["InstanceProperty_" . $i . "_DataTypeID"]);
												$defaultValue = $_POST["InstanceProperty_" . $i . "_DefaultValue"];
												
												$object->CreateInstanceProperty(new TenantObjectInstanceProperty($name, $dataType, $defaultValue));
											}
											
											System::Redirect("~/tenant/manage/" . $tenant->URL . "/objects/" . $object->ID);
											return true;
										}
										else
										{
											$page = new TenantObjectManagementPage();
											$page->CurrentTenant = $tenant;
											$page->CurrentObject = $object;
											$page->Render();
										}
										break;
									}
								}
							}
							break;
						}
					}
				}
				return true;
			}),
			new ModulePage("launch", function($path)
			{
				$tenant = Tenant::GetByURL($path[0]);
				header("Location: http://" . $tenant->DataCenters->Items[0]->HostName . "/" . $tenant->URL);
			})
		)),
		new ModulePage("module", array
		(
			new ModulePage("", function($path)
			{
				$page = new ModuleMainPage();
				$page->Render();
				return true;
			}),
			new ModulePage("modify", function($path)
			{
				$module = \Objectify\Objects\Module::GetByID($path[0], true);
				if ($_SERVER["REQUEST_METHOD"] == "POST")
				{
					$module->Title = $_POST["module_Title"];
					$module->Description = $_POST["module_Description"];
					$module->Update();
					
					System::Redirect("~/module/modify/" . $path[0]);
				}
				else
				{
					$page = new ModuleManagementPage();
					$page->Module = $module;
					$page->Render();
				}
				return true;
			})
		)),
		new ModulePage("datacenter", array
		(
			new ModulePage("", function($path)
			{
				$page = new DataCenterMainPage();
				$page->Render();
				return true;
			}),
			new ModulePage("create", function($path)
			{
				$datacenter = new DataCenter();
				if ($_SERVER["REQUEST_METHOD"] == "POST")
				{
					$datacenter->Title = $_POST["datacenter_Title"];
					$datacenter->Description = $_POST["datacenter_Description"];
					$datacenter->HostName = $_POST["datacenter_HostName"];
					$datacenter->Update();
					
					System::Redirect("~/datacenter");
				}
				else
				{
					$page = new DataCenterManagementPage();
					$page->DataCenter = null;
					$page->Render();
				}
				return true;
			}),
			new ModulePage("modify", function($path)
			{
				$datacenter = DataCenter::GetByID($path[0]);
				if ($_SERVER["REQUEST_METHOD"] == "POST")
				{
					$datacenter->Title = $_POST["datacenter_Title"];
					$datacenter->Description = $_POST["datacenter_Description"];
					$datacenter->HostName = $_POST["datacenter_HostName"];
					$datacenter->Update();
					
					System::Redirect("~/datacenter/modify/" . $path[0]);
				}
				else
				{
					$page = new DataCenterManagementPage();
					$page->DataCenter = $datacenter;
					$page->Render();
				}
				return true;
			})
		))
	));
?>