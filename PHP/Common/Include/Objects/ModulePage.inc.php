<?php
	namespace Objectify\Objects;
	use WebFX\System;
	
	// \Enum::Create("Objectify\\Objects\\ModuleStatus", "Enabled", "Disabled");
	
	class ModulePage
	{
		public $ID;
		public $Module;
		public $ParentPage;
		public $URL;
		public $Content;
		
		public static function GetByAssoc($values)
		{
			$item = new ModulePage();
			$item->ID = $values["modulepage_ID"];
			$item->Module = Module::GetByID($values["modulepage_ModuleID"]);
			$item->ParentPage = ModulePage::GetByID($values["modulepage_ParentPageID"]);
			$item->URL = $values["modulepage_URL"];
			$item->Content = $values["modulepage_Content"];
			return $item;
		}
		public static function Get($max = null)
		{
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "ModulePages";
			$result = $MySQL->query($query);
			$count = $result->num_rows;
			$retval = array();
			for ($i = 0; $i < $count; $i++)
			{
				$values = $result->fetch_assoc();
				$retval[] = ModulePage::GetByAssoc($values);
			}
			return $retval;
		}
		public static function GetByID($id)
		{
			if (!is_numeric($id)) return null;
			
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "Modules WHERE module_ID = " . $id;
			$result = $MySQL->query($query);
			$count = $result->num_rows;
			if ($count == 0) return null;
			
			$values = $result->fetch_assoc();
			return ModulePage::GetByAssoc($values);
		}
		
		public function ToJSON()
		{
			echo("{");
			echo("\"ID\":" . $this->ID . ",");
			if ($this->Module == null)
			{
				echo("\"Module\":null,");
			}
			else
			{
				echo("\"Module\":" . $this->Module->ToJSON() . ",");
			}
			if ($this->ParentPage == null)
			{
				echo("\"ParentPage\":null,");
			}
			else
			{
				echo("\"ParentPage\":" . $this->ParentPage->ToJSON() . ",");
			}
			echo("\"URL\":\"" . \JH\Utilities::JavaScriptDecode($this->URL, "\"") . "\",");
			echo("\"Content\":\"" . \JH\Utilities::JavaScriptDecode($this->Content, "\"") . "\"");
			echo("}");
		}
	}
?>