<?php
	namespace Objectify\Objects;
	use WebFX\System;
	
	class TenantObject
	{
		public $ID;
		public $Tenant;
		public $Module;
		public $ParentObject;
		public $Name;
		public $Description;
		
		public static function GetByAssoc($values)
		{
			$item = new TenantObject();
			$item->ID = $values["object_ID"];
			$item->Tenant = Tenant::GetByID($values["object_TenantID"]);
			$item->Module = Module::GetByID($values["object_ModuleID"]);
			$item->ParentObject = TenantObject::GetByID($values["object_ParentObjectID"]);
			$item->Name = $values["object_Name"];
			$item->Description = $values["object_Description"];
			return $item;
		}
		
		public static function Get($max = null, $tenant = null)
		{
			global $MySQL;
			
			$retval = array();
			if ($tenant == null) $tenant = Tenant::GetCurrent();
			if ($tenant == null) return $retval;
			
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjects WHERE object_TenantID = " . $tenant->ID;
			if (is_numeric($max)) $query .= " LIMIT " . $max;
			
			$result = $MySQL->query($query);
			if ($result === false) return $retval;
			
			$count = $result->num_rows;
			for ($i = 0; $i < $count; $i++)
			{
				$values = $result->fetch_assoc();
				$retval[] = TenantObject::GetByAssoc($values);
			}
			return $retval;
		}
		
		public static function GetByID($id)
		{
			if (!is_numeric($id)) return null;
			
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjects WHERE object_ID = " . $id;
			$result = $MySQL->query($query);
			if ($result === false) return null;
			
			$count = $result->num_rows;
			if ($count == 0) return null;
			
			$values = $result->fetch_assoc();
			return TenantObject::GetByAssoc($values);
		}
		
		/// <summary>
		/// Creates an instance of this Objectify object with the specified properties.
		/// </summary>
		public function CreateInstance($properties)
		{
			if (!is_array($properties)) return false;
			
			$inst = new TenantObjectInstance($this);
			$inst->Update();
			
			foreach ($properties as $instprop)
			{
				$inst->SetPropertyValue($instprop->Property, $instprop->Value);
			}
			return $inst;
		}
		
		public function GetPropertyValue($property, $defaultValue = null)
		{
			global $MySQL;
			
			if (is_string($property))
			{
				$property = $this->GetProperty($property);
			}
			if ($property == null) return $defaultValue;
			
			$query = "SELECT propval_Value FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectPropertyValues WHERE propval_PropertyID = " . $property->ID;
			
			$result = $MySQL->query($query);
			if ($result === false) return $defaultValue;
			
			$count = $result->num_rows;
			if ($count == 0) return $defaultValue;
			
			$values = $result->fetch_array();
			return $property->DataType->Decode($values[0]);
		}
		public function SetPropertyValue($property, $value)
		{
			global $MySQL;
			
			if (is_string($property))
			{
				$property = $this->GetProperty($property);
			}
			if ($property == null) return false;
			
			$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "TenantObjectPropertyValues (propval_PropertyID, propval_Value) VALUES (";
			$query .= $property->ID . ", ";
			$query .= "'" . $MySQL->real_escape_string($property->DataType->Encode($value)) . "'";
			$query .= ")";
			$query .= " ON DUPLICATE KEY UPDATE ";
			$query .= "propval_PropertyID = values(propval_PropertyID), ";
			$query .= "propval_Value = values(propval_Value)";
			
			$result = $MySQL->query($query);
			if ($result === false)
			{
				Objectify::Log("Database error when trying to update a property value for the specified object.", array
				(
					"DatabaseError" => $MySQL->error . " (" . $MySQL->errno . ")",
					"Query" => $query
				));
				return false;
			}
			
			return true;
		}
		
		
		public function CreateInstanceProperty($property)
		{
			global $MySQL;
			
			$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstanceProperties (property_ObjectID, property_Name, property_Description, property_DataTypeID, property_DefaultValue, property_IsRequired) VALUES (";
			$query .= $this->ID . ", ";
			$query .= "'" . $MySQL->real_escape_string($property->Name) . "', ";
			$query .= "'" . $MySQL->real_escape_string($property->Description) . "', ";
			$query .= ($property->DataType == null ? "NULL" : $property->DataType->ID) . ", ";
			$query .= "'" . $MySQL->real_escape_string($property->Encode($property->DefaultValue)) . "', ";
			$query .= ($property->Required ? "1" : "0");
			$query .= ")";
			$result = $MySQL->query($query);
			if ($result === false)
			{
				Objectify::Log("Database error when trying to create an instance property for the specified tenant object.", array
				(
					"DatabaseError" => $MySQL->error . " (" . $MySQL->errno . ")",
					"Query" => $query
				));
				return false;
			}
		}
		
		
		public function CreateMethod($name, $parameters, $codeblob, $description = null, $namespaceReferences = null)
		{
			global $MySQL;
			
			$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "TenantObjectMethods (method_ObjectID, method_Name, method_Description, method_CodeBlob) VALUES (";
			$query .= $this->ID . ", ";
			$query .= "'" . $MySQL->real_escape_string($name) . "', ";
			$query .= ($description == null ? "NULL" : ("'" . $MySQL->real_escape_string($description) . "'")) . ", ";
			$query .= "'" . $MySQL->real_escape_string($codeblob) . "'";
			$query .= ")";
			$result = $MySQL->query($query);
			if ($result === false)
			{
				Objectify::Log("Database error when trying to create a static method for the specified tenant object.", array
				(
					"DatabaseError" => $MySQL->error . " (" . $MySQL->errno . ")",
					"Query" => $query
				));
				return false;
			}
			
			$method = TenantObjectMethod::GetByID($MySQL->insert_id);
			
			if (is_array($namespaceReferences))
			{
				foreach ($namespaceReferences as $ref)
				{
					$method->AddNamespaceReference($ref);
				}
			}
			return $method;
		}
		public function CreateInstanceMethod($name, $parameters, $codeblob, $description = null, $namespaceReferences = null)
		{
			global $MySQL;
			
			$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstanceMethods (method_ObjectID, method_Name, method_Description, method_CodeBlob) VALUES (";
			$query .= $this->ID . ", ";
			$query .= "'" . $MySQL->real_escape_string($name) . "', ";
			$query .= ($description == null ? "NULL" : ("'" . $MySQL->real_escape_string($description) . "'")) . ", ";
			$query .= "'" . $MySQL->real_escape_string($codeblob) . "'";
			$query .= ")";
			$result = $MySQL->query($query);
			if ($result === false)
			{
				Objectify::Log("Database error when trying to create an instance method for the specified tenant object.", array
				(
					"DatabaseError" => $MySQL->error . " (" . $MySQL->errno . ")",
					"Query" => $query
				));
				return false;
			}
			
			$method = TenantObjectInstanceMethod::GetByID($MySQL->insert_id);
			
			if (is_array($namespaceReferences))
			{
				foreach ($namespaceReferences as $ref)
				{
					$method->AddNamespaceReference($ref);
				}
			}
			return $method;
		}
		
		public function GetProperty($propertyName)
		{
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectProperties WHERE property_ObjectID = " . $this->ID . " AND property_Name = '" . $MySQL->real_escape_string($propertyName) . "'";
			$result = $MySQL->query($query);
			if ($result === false) return null;
			$count = $result->num_rows;
			if ($count == 0) return null;
			
			$values = $result->fetch_assoc();
			return TenantObjectProperty::GetByAssoc($values);
		}
		public function GetProperties($max = null)
		{
			global $MySQL;
			
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectProperties WHERE property_ObjectID = " . $this->ID;
			if (is_numeric($max)) $query .= " LIMIT " . $max;
			
			$result = $MySQL->query($query);
			$retval = array();
			
			if ($result === false) return $retval;
			
			$count = $result->num_rows;
			for ($i = 0; $i < $count; $i++)
			{
				$values = $result->fetch_assoc();
				$retval[] = TenantObjectProperty::GetByAssoc($values);
			}
			return $retval;
		}
		public function GetInstanceProperty($propertyName)
		{
			global $MySQL;
			
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstanceProperties WHERE property_ObjectID = " . $this->ID . " AND property_Name = '" . $MySQL->real_escape_string($propertyName) . "'";
			
			$result = $MySQL->query($query);
			if ($result === false) return null;
			$count = $result->num_rows;
			if ($count == 0)
			{
				Objectify::Log("Could not fetch the specified instance property on the object.", array
				(
					"Object" => $this->Name,
					"Property" => $propertyName
				));
				return null;
			}
			
			$values = $result->fetch_assoc();
			
			return TenantObjectInstanceProperty::GetByAssoc($values);
		}
		public function GetInstanceProperties($max = null)
		{
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstanceProperties WHERE property_ObjectID = " . $this->ID;
			
			$result = $MySQL->query($query);
			$retval = array();
			
			if ($result === false) return $retval;
			
			$count = $result->num_rows;
			for ($i = 0; $i < $count; $i++)
			{
				$values = $result->fetch_assoc();
				$retval[] = TenantObjectInstanceProperty::GetByAssoc($values);
			}
			return $retval;
		}
		
		public function GetMethod($name)
		{
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectMethods WHERE method_ObjectID = " . $this->ID . " AND method_Name = '" . $MySQL->real_escape_string($name) . "'";
			$result = $MySQL->query($query);
			
			if ($result === false) return null;
			$count = $result->num_rows;
			if ($count == 0) return null;
			
			$values = $result->fetch_assoc();
			return TenantObjectMethod::GetByAssoc($values);
		}
		public function GetMethods($max = null)
		{
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectMethods WHERE method_ObjectID = " . $this->ID;
			if (is_numeric($max)) $query .= " LIMIT " . $max;
			$result = $MySQL->query($query);
			
			$retval = array();
			if ($result === false) return $retval;
			
			$count = $result->num_rows;
			for ($i = 0; $i < $count; $i++)
			{
				$values = $result->fetch_assoc();
				$retval[] = TenantObjectMethod::GetByAssoc($values);
			}
			return $retval;
		}
		
		public function GetInstanceMethod($name)
		{
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstanceMethods WHERE method_ObjectID = " . $this->ID . " AND method_Name = '" . $MySQL->real_escape_string($name) . "'";
			$result = $MySQL->query($query);
			if ($result === false) return null;
			$count = $result->num_rows;
			if ($count == 0) return null;
			
			$values = $result->fetch_assoc();
			
			return TenantObjectInstanceMethod::GetByAssoc($values);
		}
		public function GetInstanceMethods($max = null)
		{
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstanceMethods WHERE method_ObjectID = " . $this->ID;
			if (is_numeric($max)) $query .= " LIMIT " . $max;
			$result = $MySQL->query($query);
			
			$retval = array();
			if ($result === false) return $retval;
			
			$count = $result->num_rows;
			for ($i = 0; $i < $count; $i++)
			{
				$values = $result->fetch_assoc();
				$retval[] = TenantObjectInstanceMethod::GetByAssoc($values);
			}
			return $retval;
		}
		
		public function CountInstances($max = null)
		{
			global $MySQL;
			$query = "SELECT COUNT(instance_ID) FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstances WHERE instance_ObjectID = " . $this->ID;
			$result = $MySQL->query($query);
			
			if ($result === false) return 0;
			$count = $result->num_rows;
			if ($count == 0) return 0;
			
			$values = $result->fetch_array();
			return $values[0];
		}
		
		public function GetInstance($parameters)
		{
			if (!is_array($parameters))
			{
				Objectify::Log("No parameters were specified by which to extract a single instance of the object.", array
				(
					"Object" => $this->Name,
					"Property" => $propertyName
				));
				return null;
			}
			
			global $MySQL;
			
			$query = "SELECT " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstances.* FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstances, " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstanceProperties, " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstancePropertyValues";
			$result = $MySQL->query($query);
			if ($result === false)
			{
				Objectify::Log("Database error when trying to obtain an instance of an object on the tenant.", array
				(
					"DatabaseError" => $MySQL->error . " (" . $MySQL->errno . ")",
					"Query" => $query
				));
				return null;
			}
			
			$count = $result->num_rows;
			if ($count == 0)
			{
				Objectify::Log("Could not obtain an instance of the object with the specified parameters.", array
				(
					"Object" => $this->Name,
					"Query" => $query
				));
				return null;
			}
			
			for ($i = 0; $i < $count; $i++)
			{
				$values = $result->fetch_assoc();
				$inst = TenantObjectInstance::GetByAssoc($values);
				$found = true;
				foreach ($parameters as $parameter)
				{
					if ($inst->GetPropertyValue($this->GetInstanceProperty($parameter->Name)) != $parameter->Value)
					{
						$found = false;
						break;
					}
				}
				if ($found) return $inst;
			}
			return null;
		}
		
		public function GetInstances($max = null)
		{
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstances WHERE instance_ObjectID = " . $this->ID;
			$result = $MySQL->query($query);
			$retval = array();
			
			if ($result === false) return $retval;
			$count = $result->num_rows;
			for ($i = 0; $i < $count; $i++)
			{
				$values = $result->fetch_assoc();
				$retval[] = TenantObjectInstance::GetByAssoc($values);
			}
			return $retval;
		}
		
		public function GetTitleOrName($language = null)
		{
			$title = $this->GetTitle($language);
			if ($title == null) return $this->Name;
			return $title;
		}
		
		public function GetTitle($language = null)
		{
			if ($language == null) return Language::GetCurrent();
			
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectTitles WHERE entry_LanguageID = " . $language->ID . " AND entry_ObjectID = " . $this->ID;
			$result = $MySQL->query($query);
			if ($result === false) return null;
			
			$count = $result->num_rows;
			if ($count == 0) return null;
			
			$values = $result->fetch_assoc();
			return $values["entry_Value"];
		}
		public function SetTitle($language, $value)
		{
			if ($language == null) return Language::GetCurrent();
			
			global $MySQL;
			$query = "SELECT COUNT(*) FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectDescriptions WHERE entry_LanguageID = " . $language->ID . " AND entry_ObjectID = " . $this->ID;
			$result = $MySQL->query($query);
			if ($result === false) return false;
			
			$values = $result->fetch_array();
			if (is_numeric($values[0]) && $values[0] > 0)
			{
				$query = "UPDATE " . System::$Configuration["Database.TablePrefix"] . "TenantObjectDescriptions SET entry_Value = '" . $MySQL->real_escape_string($value) . "' WHERE entry_LanguageID = " . $language->ID . " AND entry_ObjectID = " . $this->ID;
				$result = $MySQL->query($query);
				if ($result === false) return false;
			}
			else
			{
				$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "TenantObjectDescriptions (entry_LanguageID, entry_ObjectID, entry_Value) VALUES (" . $language->ID . ", " . $this->ID . ", '" . $MySQL->real_escape_string($value) . "')";
				$result = $MySQL->query($query);
				if ($result === false) return false;
			}
			return true;
		}
		public function SetTitles($items)
		{
			foreach ($items as $item)
			{
				$object->SetTitle($item->Language, $item->Value);
			}
		}
		public function GetDescription($language = null)
		{
			if ($language == null) return Language::GetCurrent();
			
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectDescriptions WHERE entry_LanguageID = " . $language->ID . " AND entry_ObjectID = " . $this->ID;
			$result = $MySQL->query($query);
			if ($result === false) return null;
			
			$count = $result->num_rows;
			if ($count == 0) return null;
			
			$values = $result->fetch_assoc();
			return $values["entry_Value"];
		}
		public function SetDescription($language, $value)
		{
		}
		public function SetDescriptions($items)
		{
			foreach ($items as $item)
			{
				$object->SetDescription($item->Language, $item->Value);
			}
		}
	}
?>