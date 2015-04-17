<?php
	namespace Objectify\Objects;
	
	use WebFX\System;
	
	class TenantObjectProperty
	{
		public $ID;
		public $Tenant;
		public $ParentObject;
		
		public $Name;
		public $Description;
		public $DataType;
		public $DefaultValue;
		public $Required;
		public $Enumeration;
		public $RequireChoiceFromEnumeration;
		
		public function RenderColumn($value = null)
		{
			if ($this->DataType == null || $this->DataType->ColumnRendererCodeBlob == null)
			{
				?>
				<input style="width: 100%;" type="text" id="txtProperty_<?php echo($property->ID); ?>" name="Property_<?php echo($property->ID); ?>" value="<?php
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
					$this->DataType->RenderColumn($this->DefaultValue);
				}
				else
				{
					$this->DataType->RenderColumn($value);
				}
			}
		}
		
		public function __construct($name, $description = null, $dataType = null, $defaultValue = null, $required = false, $enumeration = null, $requireChoiceFromEnumeration = false)
		{
			$this->Name = $name;
			$this->Description = $description;
			$this->DataType = $dataType;
			$this->DefaultValue = $defaultValue;
			$this->Required = $required;
			$this->Enumeration = $enumeration;
			$this->RequireChoiceFromEnumeration = $requireChoiceFromEnumeration;
		}
		
		public static function GetByAssoc($values)
		{
			$item = new TenantObjectProperty();
			$item->ID = $values["property_ID"];
			$item->Tenant = Tenant::GetByID($values["property_TenantID"]);
			$item->ParentObject = TenantObject::GetByID($values["property_ObjectID"]);
			$item->Name = $values["property_Name"];
			$item->Description = $values["property_Description"];
			$item->DataType = DataType::GetByID($values["property_DataTypeID"]);
			$item->DefaultValue = $values["property_DefaultValue"];
			$item->Required = ($values["property_IsRequired"] == 1);
			$item->Enumeration = TenantEnumeration::GetByID($values["property_EnumerationID"]);
			$item->RequireChoiceFromEnumeration = ($values["property_RequireChoiceFromEnumeration"] == 1);
			return $item;
		}
		
		public function Update()
		{
			global $MySQL;
			
			if ($this->ID == null)
			{
				$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "TenantObjectProperties (property_TenantID, property_ObjectID, property_Name, property_Description, property_DataTypeID, property_DefaultValue, property_IsRequired) VALUES (";
				$query .= ($this->Tenant == null ? "NULL" : $this->Tenant->ID) . ", ";
				$query .= ($this->ParentObject == null ? "NULL" : $this->ParentObject->ID) . ", ";
				$query .= "'" . $MySQL->real_escape_string($this->Name) . "', ";
				$query .= "'" . $MySQL->real_escape_string($this->Description) . "', ";
				$query .= ($this->DataType == null ? "NULL" : $this->DataType->ID) . ", ";
				$query .= $this->DefaultValue == null ? "NULL" : ("'" . $this->DefaultValue . "'") . ", ";
				$query .= ($this->Required ? "1" : "0") . ", ";
			}
			else
			{
				$query = "UPDATE " . System::$Configuration["Database.TablePrefix"] . "TenantObjectProperties SET ";
				$query .= "property_TenantID = " . ($this->Tenant == null ? "NULL" : $this->Tenant->ID) . ", ";
				$query .= "property_ObjectID = " . ($this->ParentObject == null ? "NULL" : $this->ParentObject->ID) . ", ";
				$query .= "property_Name = '" . $MySQL->real_escape_string($this->Name) . "', ";
				$query .= "property_Description = '" . $MySQL->real_escape_string($this->Description) . "', ";
				$query .= "property_DataTypeID = " . ($this->DataType == null ? "NULL" : $this->DataType->ID) . ", ";
				$query .= "property_DefaultValue = " . $this->DefaultValue == null ? "NULL" : ("'" . $this->DefaultValue . "'") . ", ";
				$query .= "property_IsRequired = " . ($this->Required ? "1" : "0") . ", ";
				$query .= " WHERE property_ID = " . $this->ID;
			}
			
			$result = $MySQL->query($query);
			if ($result === false) return false;
			
			if ($this->ID == null)
			{
				$this->ID = $MySQL->insert_id;
			}
			
			return true;
		}
	}
	class TenantObjectPropertyValue
	{
		public $Property;
		public $Value;
		
		public function __construct($property, $value)
		{
			$this->Property = $property;
			$this->Value = $value;
		}
		
		public function Update()
		{
			
		}
	}
?>