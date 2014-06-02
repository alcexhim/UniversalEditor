<?php
	namespace Objectify\Objects;
	use WebFX\System;
	
	class TenantProperty
	{
		public $ID;
		public $Tenant;
		public $Name;
		public $Description;
		public $DataType;
		public $DefaultValue;
		
		public function __construct($name = null, $dataType = null, $description = null, $defaultValue = null)
		{
			$this->Name = $name;
			$this->DataType = $dataType;
			$this->Description = $description;
			$this->DefaultValue = $defaultValue;
		}
		
		public function Decode($value)
		{
			if ($this->DataType == null) return $value;
			return $this->DataType->Decode($value);
		}
		public function Encode($value)
		{
			if ($this->DataType == null) return $value;
			return $this->DataType->Encode($value);
		}
		
		public function RenderColumn($value = null)
		{
			if ($this->DataType == null || $this->DataType->ColumnRendererCodeBlob == null)
			{
				if ($value == null)
				{
					echo($this->DefaultValue);
				}
				else
				{
					echo($value);
				}
			}
			else
			{
				if ($value == null)
				{
					$this->DataType->RenderColumn($this->DefaultValue);
				}
				else
				{
					$this->DataType->RenderColumn($value);
				}
			}
		}
		public function RenderEditor($value = null, $name = null)
		{
			if ($name == null) $name = "Property_" . $this->ID;
			if ($this->DataType == null || $this->DataType->ColumnRendererCodeBlob == null)
			{
				?>
				<input style="width: 100%;" type="text" id="<?php echo($name); ?>" name="<?php echo($name); ?>" value="<?php
				if ($value == null)
				{
					echo($this->DefaultValue);
				}
				else
				{
					echo($value);
				}
				?>" />
				<?php
			}
			else
			{
				if ($value == null)
				{
					$this->DataType->RenderEditor($this->DefaultValue, $name);
				}
				else
				{
					$this->DataType->RenderEditor($value, $name);
				}
			}
		}
		
		public static function GetByAssoc($values)
		{
			$item = new TenantProperty();
			$item->ID = $values["property_ID"];
			$item->Tenant = Tenant::GetByID($values["property_TenantID"]);
			$item->Name = $values["property_Name"];
			$item->Description = $values["property_Description"];
			$item->DataType = DataType::GetByID($values["property_DataTypeID"]);
			$item->DefaultValue = $item->DataType->Decode($values["property_DefaultValue"]);
			return $item;
		}
		
		public static function Create($property, $tenant = null)
		{
			global $MySQL;
			
			$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "TenantProperties (property_TenantID, property_Name, property_Description, property_DataTypeID, property_DefaultValue) VALUES (";
			$query .= ($tenant == null ? "NULL" : $tenant->ID) . ", ";
			$query .= "'" . $MySQL->real_escape_string($property->Name) . "', ";
			$query .= "'" . $MySQL->real_escape_string($property->Description) . "', ";
			$query .= ($property->DataType == null ? "NULL" : $property->DataType->ID) . ", ";
			$query .= ($property->DefaultValue == null ? "NULL" : "'" . $MySQL->real_escape_string($property->DataType->Encode($property->DefaultValue)) . "'");
			$query .= ")";
			
			$result = $MySQL->query($query);
			if ($result === false)
			{
				Objectify::Log("Database error when trying to create a property on the tenant.", array
				(
					"DatabaseError" => $MySQL->error . " (" . $MySQL->errno . ")",
					"Query" => $query,
					"Property" => ($property == null ? ($propname == null ? "(null)" : $propname) : (is_string($property) ? $property : "#" . $property->ID))
				));
				return false;
			}
			return true;
		}
	}
?>