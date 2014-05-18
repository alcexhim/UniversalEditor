<?php
	namespace Objectify\Objects;
	use WebFX\System;
	
	class DataType
	{
		public $ID;
		public $Name;
		public $Description;
		
		public $EncoderCodeBlob;
		public $DecoderCodeBlob;
		public $ColumnRendererCodeBlob;
		public $EditorRendererCodeBlob;
		
		public static function GetByAssoc($values)
		{
			$item = new DataType();
			$item->ID = $values["datatype_ID"];
			$item->Name = $values["datatype_Name"];
			$item->Description = $values["datatype_Description"];
			
			$item->EncoderCodeBlob = $values["datatype_EncoderCodeBlob"];
			$item->DecoderCodeBlob = $values["datatype_DecoderCodeBlob"];
			$item->ColumnRendererCodeBlob = $values["datatype_ColumnRendererCodeBlob"];
			$item->EditorRendererCodeBlob = $values["datatype_EditorRendererCodeBlob"];
			return $item;
		}
		
		public static function Get($max = null)
		{
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "DataTypes";
			if (is_numeric($max))
			{
				$query .= " LIMIT " . $max;
			}
			$result = $MySQL->query($query);
			$retval = array();
			if ($result === false) return $retval;
			
			$count = $result->num_rows;
			for ($i = 0; $i < $count; $i++)
			{
				$values = $result->fetch_assoc();
				$retval[] = DataType::GetByAssoc($values);
			}
			return $retval;
		}
		
		public static function GetByID($id)
		{
			if (!is_numeric($id)) return null;
			
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "DataTypes WHERE datatype_ID = " . $id;
			
			$result = $MySQL->query($query);
			if ($result === false) return null;
			
			$count = $result->num_rows;
			if ($count == 0) return null;
			
			$values = $result->fetch_assoc();
			return DataType::GetByAssoc($values);
		}
		
		public static function GetByName($name)
		{
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "DataTypes WHERE datatype_Name = '" . $MySQL->real_escape_string($name) . "'";
			
			$result = $MySQL->query($query);
			if ($result === false) return null;
			
			$count = $result->num_rows;
			if ($count == 0)
			{
				Objectify::Log("No data type with the specified name was found.", array
				(
					"DataType" => $name
				));
				return null;
			}
			
			$values = $result->fetch_assoc();
			return DataType::GetByAssoc($values);
		}
		
		public function Update()
		{
			global $MySQL;
			if ($this->ID == null)
			{
				$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "DataTypes (datatype_Name, datatype_Description, datatype_EncoderCodeBlob, datatype_DecoderCodeBlob, datatype_ColumnRendererCodeBlob, datatype_EditorRendererCodeBlob) VALUES (";
				$query .= "'" . $MySQL->real_escape_string($this->Name) . "', ";
				$query .= "'" . $MySQL->real_escape_string($this->Description) . "', ";
				$query .= ($this->EncoderCodeBlob != null ? ("'" . $MySQL->real_escape_string($this->EncoderCodeBlob) . "'") : "NULL") . ", ";
				$query .= ($this->DecoderCodeBlob != null ? ("'" . $MySQL->real_escape_string($this->DecoderCodeBlob) . "'") : "NULL") . ", ";
				$query .= ($this->ColumnRendererCodeBlob != null ? ("'" . $MySQL->real_escape_string($this->ColumnRendererCodeBlob) . "'") : "NULL") . ", ";
				$query .= ($this->EditorRendererCodeBlob != null ? ("'" . $MySQL->real_escape_string($this->EditorRendererCodeBlob) . "'") : "NULL");
				$query .= ")";
			}
			else
			{
				$query = "UPDATE " . System::$Configuration["Database.TablePrefix"] . "DataTypes SET ";
				$query .= "datatype_Name = '" . $MySQL->real_escape_string($this->Name) . "', ";
				$query .= "datatype_Description = '" . $MySQL->real_escape_string($this->Description) . "', ";
				$query .= "datatype_EncoderCodeBlob = '" . ($this->EncoderCodeBlob != null ? ("'" . $MySQL->real_escape_string($this->EncoderCodeBlob) . "'") : "NULL") . "', ";
				$query .= "datatype_DecoderCodeBlob = '" . ($this->DecoderCodeBlob != null ? ("'" . $MySQL->real_escape_string($this->DecoderCodeBlob) . "'") : "NULL") . "', ";
				$query .= "datatype_ColumnRendererCodeBlob = '" . ($this->ColumnRendererCodeBlob != null ? ("'" . $MySQL->real_escape_string($this->ColumnRendererCodeBlob) . "'") : "NULL") . "', ";
				$query .= "datatype_EditorRendererCodeBlob = '" . ($this->EditorRendererCodeBlob != null ? ("'" . $MySQL->real_escape_string($this->EditorRendererCodeBlob) . "'") : "NULL") . "'";
				$query .= " WHERE datatype_ID = " . $this->ID;
			}
			
			$MySQL->query($query);
			if ($MySQL->errno != 0)
			{
				echo($MySQL->error);
				die();
				return false;
			}
			
			if ($this->ID == null)
			{
				$this->ID = $MySQL->insert_id;
			}
			return true;
		}
		
		public function Encode($value)
		{
			if ($this->EncoderCodeBlob == null) return $value;
			$q = '';
			$q .= 'use Objectify\Objects\MultipleInstanceProperty; ';
			$q .= 'use Objectify\Objects\SingleInstanceProperty; ';
			$q .= 'use Objectify\Objects\TenantObject; ';
			$q .= 'use Objectify\Objects\TenantObjectInstance; ';
			$q .= '$x = function($input) { ' . $this->EncoderCodeBlob . ' };';
			// trigger_error("calling EncoderCodeBlob on DataType '" . $this->Name . "'", E_USER_NOTICE);
			eval($q);
			return $x($value);
		}
		public function Decode($value)
		{
			if ($this->DecoderCodeBlob == null) return $value;
			$q = '';
			$q .= 'use Objectify\Objects\MultipleInstanceProperty; ';
			$q .= 'use Objectify\Objects\SingleInstanceProperty; ';
			$q .= 'use Objectify\Objects\TenantObject; ';
			$q .= 'use Objectify\Objects\TenantObjectInstance; ';
			$q .= '$x = function($input) { ' . $this->DecoderCodeBlob . ' };';
			// trigger_error("calling DecoderCodeBlob on DataType '" . $this->Name . "'", E_USER_NOTICE);
			eval($q);
			return $x($value);
		}
		
		public function RenderColumn($value)
		{
			if ($this->ColumnRendererCodeBlob == null) return;
			$q = '';
			$q .= 'use Objectify\Objects\MultipleInstanceProperty; ';
			$q .= 'use Objectify\Objects\SingleInstanceProperty; ';
			$q .= 'use Objectify\Objects\TenantObject; ';
			$q .= 'use Objectify\Objects\TenantObjectInstance; ';
			$q .= '$x = function($input) { ' . $this->ColumnRendererCodeBlob . ' };';
			// trigger_error("calling ColumnRendererCodeBlob on DataType '" . $this->Name . "'", E_USER_NOTICE);
			eval($q);
			$x($value);
		}
		public function RenderEditor($value, $name)
		{
			if ($this->EditorRendererCodeBlob == null) return;
			
			$q = '';
			$q .= 'use Objectify\Objects\MultipleInstanceProperty; ';
			$q .= 'use Objectify\Objects\SingleInstanceProperty; ';
			$q .= 'use Objectify\Objects\TenantObject; ';
			$q .= 'use Objectify\Objects\TenantObjectInstance; ';
			$q .= '$x = function($input, $name) { ' . $this->EditorRendererCodeBlob . ' };';
			// trigger_error("calling EditorRendererCodeBlob on DataType '" . $this->Name . "'", E_USER_NOTICE);
			
			eval($q);
			
			// if $x is not set, then there must have been an error in parsing so stop rendering
			if (!isset($x)) return;
			
			$x($value, $name);
		}
	}
?>