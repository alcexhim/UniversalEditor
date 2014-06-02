<?php
	namespace Objectify\Objects;
	
	use WebFX\System;
	
	class TenantObjectInstance
	{
		public $ID;
		public $ParentObject;
		
		public function __construct($parentObject)
		{
			$this->ParentObject = $parentObject;
		}
		
		public static function GetByAssoc($values)
		{
			$item = new TenantObjectInstance(TenantObject::GetByID($values["instance_ObjectID"]));
			$item->ID = $values["instance_ID"];
			return $item;
		}
		
		public static function GetByID($id)
		{
			if (!is_numeric($id)) return null;
			
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstances WHERE instance_ID = " . $id;
			$result = $MySQL->query($query);
			if ($result === false) return null;
			
			$values = $result->fetch_assoc();
			return TenantObjectInstance::GetByAssoc($values);
		}
		
		public function GetPropertyValue($property, $defaultValue = null)
		{
			global $MySQL;
			
			if (is_string($property))
			{
				$property = $this->ParentObject->GetInstanceProperty($property);
			}
			if ($property == null) return $defaultValue;
			
			$query = "SELECT propval_Value FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstancePropertyValues WHERE propval_InstanceID = " . $this->ID . " AND propval_PropertyID = " . $property->ID;
			
			$result = $MySQL->query($query);
			if ($result === false) return $defaultValue;
			
			$count = $result->num_rows;
			if ($count == 0) return $defaultValue;
			
			$values = $result->fetch_array();
			return $property->Decode($values[0]);
		}
		public function SetPropertyValue($property, $value)
		{
			global $MySQL;
			
			if (is_string($property))
			{
				$property = $this->ParentObject->GetInstanceProperty($property);
			}
			if ($property == null) return false;
			
			$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstancePropertyValues (propval_InstanceID, propval_PropertyID, propval_Value) VALUES (";
			$query .= $this->ID . ", ";
			$query .= $property->ID . ", ";
			$query .= "'" . $MySQL->real_escape_string($property->Encode($value)) . "'";
			$query .= ")";
			$query .= " ON DUPLICATE KEY UPDATE ";
			$query .= "propval_PropertyID = values(propval_PropertyID), ";
			$query .= "propval_Value = values(propval_Value)";
			
			$result = $MySQL->query($query);
			if ($result === false) return false;
			
			return true;
		}
		public function HasPropertyValue($property)
		{
			global $MySQL;
			
			if ($property == null) return false;
			
			$query = "SELECT COUNT(propval_Value) FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstancePropertyValues WHERE propval_InstanceID = " . $this->ID . " AND propval_PropertyID = " . $property->ID;
			
			$result = $MySQL->query($query);
			if ($result === false) return false;
			
			$count = $result->num_rows;
			if ($count == 0) return false;
			
			$values = $result->fetch_array();
			return ($values[0] > 0);
		}
		
		public function Update()
		{
			global $MySQL;
			if ($this->ID == null)
			{
				$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstances (instance_ObjectID) VALUES (";
				$query .= $this->ParentObject->ID;
				$query .= ")";
			}
			else
			{
				$query = "UPDATE " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstances SET ";
				$query .= "instance_ObjectID = " . $this->ParentObject->ID;
				$query .= " WHERE instance_ID = " . $this->ID;
			}
			$result = $MySQL->query($query);
			if ($result === false) return false;
			
			if ($this->ID == null)
			{
				$this->ID = $MySQL->insert_id;
			}
			return true;
		}
		
		public function ToString()
		{
			return $this->GetPropertyValue("Name");
		}
	}
	class TenantObjectInstanceProperty
	{
		public $ID;
		public $ParentObject;
		public $Name;
		public $DataType;
		public $DefaultValue;
		public $Required;
		
		/// <summary>
		/// Determines whether this TenantObjectInstanceProperty is visible when rendered as a column in a ListView.
		/// </summary>
		public $ColumnVisible;
		
		public function RenderColumn($value = null)
		{
			if ($this->DataType == null || $this->DataType->ColumnRendererCodeBlob == null)
			{
				?>
				<input style="width: 100%;" type="text" id="txtProperty_<?php echo($this->ID); ?>" name="Property_<?php echo($this->ID); ?>" value="<?php
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
		
		public function Encode($value)
		{
			if ($this->DataType == null) return $value;
			return $this->DataType->Encode($value);
		}
		public function Decode($value)
		{
			if ($this->DataType == null) return $value;
			return $this->DataType->Decode($value);
		}
		
		public function __construct($name = null, $dataType = null, $defaultValue = null, $required = false)
		{
			$this->Name = $name;
			$this->DataType = $dataType;
			$this->DefaultValue = $defaultValue;
			$this->Required = $required;
		}
		
		public static function GetByAssoc($values)
		{
			$item = new TenantObjectInstanceProperty();
			$item->ID = $values["property_ID"];
			$item->ParentObject = TenantObject::GetByID($values["property_ObjectID"]);
			$item->Name = $values["property_Name"];
			$item->Description = $values["property_Description"];
			$item->DataType = DataType::GetByID($values["property_DataTypeID"]);
			if ($item->DataType != null)
			{
				$item->DefaultValue = $item->DataType->Decode($values["property_DefaultValue"]);
			}
			$item->Required = ($values["property_IsRequired"] == 1);
			$item->ColumnVisible = ($values["property_ColumnVisible"] == 1);
			return $item;
		}
	}
	class TenantObjectInstancePropertyValue
	{
		public $Property;
		public $Value;
		
		public function __construct($property, $value = null)
		{
			$this->Property = $property;
			$this->Value = $value;
		}
	}
?>