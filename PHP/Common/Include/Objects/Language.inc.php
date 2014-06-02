<?php
	namespace Objectify\Objects;
	
	use WebFX\System;
	
	class Language
	{
		public $ID;
		public $Name;
		
		public static function GetByAssoc($values)
		{
			$item = new Language();
			$item->ID = $values["language_ID"];
			$item->Name = $values["language_Name"];
			return $item;
		}
		public static function Get()
		{
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "Languages";
			$result = $MySQL->query($query);
			$retval = array();
			if ($result === false) return $retval;
			
			$count = $result->num_rows;
			for ($i = 0; $i < $count; $i++)
			{
				$values = $result->fetch_assoc();
				$item = Language::GetByAssoc($values);
				if ($item != null) $retval[] = $item;
			}
			return $retval;
		}
		public static function GetByID($id)
		{
			if (!is_numeric($id)) return null;
			
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "Languages WHERE language_ID = " . $id;
			$result = $MySQL->query($query);
			if ($result === false) return null;
			
			$count = $result->num_rows;
			if ($count < 1) return null;
			
			$values = $result->fetch_assoc();
			return Language::GetByAssoc($values);
		}
		public static function GetCurrent()
		{
			return Language::GetByID(1);
		}
	}
?>