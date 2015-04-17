<?php
	namespace Objectify\Objects;
	use WebFX\System;
	
	class DataCenterCollection
	{
		public $Items;
		public function __construct()
		{
			$this->Items = array();
		}
		
		public function Add($item)
		{
			$this->Items[] = $item;
		}
		public function Contains($item)
		{
			foreach ($this->Items as $itm)
			{
				if ($itm->ID == $item->ID) return true;
			}
			return false;
		}
		public function Get($item)
		{
			foreach ($this->Items as $itm)
			{
				if ($itm->ID == $item->ID) return $item;
			}
			return null;
		}
	}
	class DataCenter
	{
		public $ID;
		public $Title;
		public $Description;
		public $HostName;
		
		public static function Create($title, $hostname, $description = null)
		{
			$item = new DataCenter();
			$item->Title = $title;
			$item->HostName = $hostname;
			$item->Description = $description;
			if ($item->Update())
			{
				return $item;
			}
			return null;
		}
		
		public static function GetByAssoc($values)
		{
			$item = new DataCenter();
			$item->ID = $values["datacenter_ID"];
			$item->Title = $values["datacenter_Title"];
			$item->Description = $values["datacenter_Description"];
			$item->HostName = $values["datacenter_HostName"];
			return $item;
		}
		public static function Get($max = null)
		{
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "DataCenters";
			$result = $MySQL->query($query);
			$count = $result->num_rows;
			$retval = array();
			for ($i = 0; $i < $count; $i++)
			{
				$values = $result->fetch_assoc();
				$retval[] = DataCenter::GetByAssoc($values);
			}
			return $retval;
		}
		public static function GetByID($id)
		{
			if (!is_numeric($id)) return null;
			
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "DataCenters WHERE datacenter_ID = " . $id;
			$result = $MySQL->query($query);
			$count = $result->num_rows;
			if ($count == 0) return null;
			
			$values = $result->fetch_assoc();
			return DataCenter::GetByAssoc($values);
		}
		
		public function Update()
		{
			global $MySQL;
			if ($this->ID != null)
			{
				$query = "UPDATE " . System::$Configuration["Database.TablePrefix"] . "DataCenters SET ";
				$query .= "datacenter_Title = '" . $MySQL->real_escape_string($this->Title) . "', ";
				$query .= "datacenter_Description = '" . $MySQL->real_escape_string($this->Description) . "', ";
				$query .= "datacenter_HostName = '" . $MySQL->real_escape_string($this->HostName) . "'";
				$query .= " WHERE datacenter_ID = " . $this->ID;
			}
			else
			{
				$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "DataCenters (datacenter_Title, datacenter_Description, datacenter_HostName) VALUES (";
				$query .= "'" . $MySQL->real_escape_string($this->Title) . "', ";
				$query .= "'" . $MySQL->real_escape_string($this->Description) . "', ";
				$query .= "'" . $MySQL->real_escape_string($this->HostName) . "'";
				$query .= ")";
			}
			$result = $MySQL->query($query);
			if ($this->ID == null)
			{
				$this->ID = $MySQL->insert_id;
			}
			return ($MySQL->errno == 0);
		}
		
		public function Delete()
		{
			global $MySQL;
			if ($this->ID == null) return false;
			
			$query = "DELETE FROM " . System::$Configuration["Database.TablePrefix"] . "DataCenters WHERE datacenter_ID = " . $this->ID;
			$result = $MySQL->query($query);
			if ($MySQL->errno != 0) return false;
			
			return true;
		}
		
		public function ToJSON()
		{
			echo("{");
			echo("\"ID\":" . $this->ID . ",");
			echo("\"Title\":\"" . \JH\Utilities::JavaScriptEncode($this->Title, "\"") . "\",");
			echo("\"Description\":\"" . \JH\Utilities::JavaScriptEncode($this->Description, "\"") . "\",");
			echo("\"HostName\":\"" . \JH\Utilities::JavaScriptEncode($this->HostName, "\"") . "\"");
			echo("}");
		}
	}
?>