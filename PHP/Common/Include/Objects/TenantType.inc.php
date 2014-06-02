<?php
	namespace Objectify\Objects;
	use WebFX\System;
	
	class TenantType
	{
		public $ID;
		public $Title;
		public $Description;
		
		public static function GetByAssoc($values)
		{
			$item = new TenantType();
			$item->ID = $values["tenanttype_ID"];
			$item->URL = $values["tenanttype_Title"];
			$item->Description = $values["tenanttype_Description"];
			return $item;
		}
		public static function Get($max = null)
		{
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantTypes";
			$result = $MySQL->query($query);
			$count = $result->num_rows;
			$retval = array();
			for ($i = 0; $i < $count; $i++)
			{
				$values = $result->fetch_assoc();
				$retval[] = TenantType::GetByAssoc($values);
			}
			return $retval;
		}
		public static function GetByID($id)
		{
			if (!is_numeric($id)) return null;
			
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantTypes WHERE tenanttype_ID = " . $id;
			$result = $MySQL->query($query);
			$count = $result->num_rows;
			if ($count == 0) return null;
			
			$values = $result->fetch_assoc();
			return TenantType::GetByAssoc($values);
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