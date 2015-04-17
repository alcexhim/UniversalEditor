<?php
	namespace Objectify\Objects;
	use WebFX\System;
	
	// \Enum::Create("Objectify\\Objects\\ModuleStatus", "Enabled", "Disabled");
	
	class Module
	{
		public $ID;
		public $Title;
		public $Description;
		public $Enabled;
		
		public static function GetByAssoc($values)
		{
			$item = new Module();
			$item->ID = $values["module_ID"];
			$item->Title = $values["module_Title"];
			$item->Description = $values["module_Description"];
			$item->Enabled = $values["module_Enabled"];
			return $item;
		}
		public static function Get($max = null, $tenant = null)
		{
			global $MySQL;
			
			$query = "SELECT module_ID, module_Title, module_Description";
			if ($tenant != null)
			{
				$query .= ", (tenantmodule_ModuleID = module_ID) AS module_Enabled";
			}
			else
			{
				$query .= ", 1 AS module_Enabled";
			}
			$query .= " FROM " . System::$Configuration["Database.TablePrefix"] . "Modules";
			if ($tenant != null)
			{
				$query .= ", " . System::$Configuration["Database.TablePrefix"] . "TenantModules";
				$query .= "  WHERE tenantmodule_ModuleID = module_ID AND tenantmodule_TenantID = " . $tenant->ID;
			}
			
			$result = $MySQL->query($query);
			$count = $result->num_rows;
			$retval = array();
			for ($i = 0; $i < $count; $i++)
			{
				$values = $result->fetch_assoc();
				$retval[] = Module::GetByAssoc($values);
			}
			return $retval;
		}
		public static function GetByID($id, $forAllTenants = false)
		{
			if (!is_numeric($id)) return null;
			
			global $MySQL;
			$query = "SELECT module_ID, module_Title, module_Description FROM " . System::$Configuration["Database.TablePrefix"] . "Modules WHERE module_ID = " . $id;
			if (!$forAllTenants)
			{
				$query = "SELECT module_ID, module_Title, module_Description FROM " . System::$Configuration["Database.TablePrefix"] . "Modules, " . System::$Configuration["Database.TablePrefix"] . "TenantModules WHERE tenantmodule_ModuleID = module_ID AND tenantmodule_TenantID = " . $CurrentTenant->ID . " AND module_ID = " . $id;
			}
			
			$result = $MySQL->query($query);
			$count = $result->num_rows;
			if ($count == 0) return null;
			
			$values = $result->fetch_assoc();
			return Module::GetByAssoc($values);
		}
		
		public function Update()
		{
			global $MySQL;
			
			if (is_numeric($this->ID))
			{
				// id is set, so update
				$query = "UPDATE " . System::$Configuration["Database.TablePrefix"] . "Modules SET ";
				$query .= "module_Title = '" . $MySQL->real_escape_string($this->Title) . "', ";
				$query .= "module_Description = '" . $MySQL->real_escape_string($this->Description) . "'";
				$query .= " WHERE module_ID = " . $this->ID;
			}
			else
			{
				// id is not set, so insert
				$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "Modules (module_Title, module_Description) VALUES (";
				$query .= "'" . $MySQL->real_escape_string($this->Title) . "', ";
				$query .= "'" . $MySQL->real_escape_string($this->Description) . "'";
				$query .= ")";
			}
			
			$result = $MySQL->query($query);
			if ($MySQL->errno != 0) return false;
			
			if (!is_numeric($this->ID))
			{
				// id is not set, so set it
				$this->ID = $MySQL->insert_id;
			}
			return true;
		}
		
		public function ToJSON()
		{
			echo("{");
			echo("\"ID\":" . $this->ID . ",");
			echo("\"Title\":\"" . \JH\Utilities::JavaScriptDecode($this->Title, "\"") . "\",");
			echo("\"Description\":\"" . \JH\Utilities::JavaScriptDecode($this->Description, "\"") . "\"");
			echo("}");
		}
	}
?>