<?php
	namespace Objectify\Objects;
	use WebFX\System;
	
	\Enum::Create("Objectify\\Objects\\TenantStatus", "Enabled", "Disabled");
	
	class Tenant
	{
		public $ID;
		public $URL;
		public $Description;
		public $Status;
		public $Type;
		public $DataCenters;
		public $PaymentPlan;
		public $BeginTimestamp;
		public $EndTimestamp;
		
		public function __construct()
		{
			$this->DataCenters = new DataCenterCollection();
		}
		
		public function IsExpired()
		{
			$date = date_create();
			if ($this->BeginTimestamp == null)
			{
				$dateBegin = null;
			}
			else
			{
				$dateBegin = date_create($this->BeginTimestamp);
			}
			if ($this->EndTimestamp == null)
			{
				$dateEnd = null;
			}
			else
			{
				$dateEnd = date_create($this->EndTimestamp);
			}
			
			return (!(($dateBegin == null || $dateBegin <= $date) && ($dateEnd == null || $dateEnd >= $date)));
		}
		
		public static function Create($url, $description = null, $status = TenantStatus::Enabled, $type = null, $paymentPlan = null, $beginTimestamp = null, $endTimestamp = null, $dataCenters = null)
		{
			$item = new Tenant();
			$item->URL = $url;
			$item->Description = $description;
			$item->Status = $status;
			$item->Type = $type;
			$item->PaymentPlan = $paymentPlan;
			$item->BeginTimestamp = $beginTimestamp;
			$item->EndTimestamp = $endTimestamp;
			
			if ($dataCenters == null) $dataCenters = array();
			foreach ($dataCenters as $datacenter)
			{
				$item->DataCenters->Add($datacenter);
			}
			
			if ($item->Update())
			{
				return $item;
			}
			return null;
		}
		
		public static function GetByAssoc($values)
		{
			$item = new Tenant();
			$item->ID = $values["tenant_ID"];
			$item->URL = $values["tenant_URL"];
			$item->Description = $values["tenant_Description"];
			switch ($values["tenant_Status"])
			{
				case 1:
				{
					$item->Status = TenantStatus::Enabled;
					break;
				}
				case 0:
				{
					$item->Status = TenantStatus::Disabled;
					break;
				}
			}
			$item->Type = TenantType::GetByID($values["tenant_TypeID"]);
			$item->PaymentPlan = PaymentPlan::GetByID($values["tenant_PaymentPlanID"]);
			$item->BeginTimestamp = $values["tenant_BeginTimestamp"];
			$item->EndTimestamp = $values["tenant_EndTimestamp"];
			
			
			// get the data centers associated with this tenant
			global $MySQL;
			$query = "SELECT " . System::$Configuration["Database.TablePrefix"] . "DataCenters.* FROM " . System::$Configuration["Database.TablePrefix"] . "DataCenters, " . System::$Configuration["Database.TablePrefix"] . "TenantDataCenters WHERE " . System::$Configuration["Database.TablePrefix"] . "TenantDataCenters.tdc_TenantID = " . $item->ID . " AND " . System::$Configuration["Database.TablePrefix"] . "TenantDataCenters.tdc_DataCenterID = " . System::$Configuration["Database.TablePrefix"] . "DataCenters.datacenter_ID";
			$result = $MySQL->query($query);
			$count = $result->num_rows;
			$retval = array();
			for ($i = 0; $i < $count; $i++)
			{
				$values = $result->fetch_assoc();
				$retval[] = DataCenter::GetByAssoc($values);
			}
			$item->DataCenters->Items = $retval;
			
			return $item;
		}
		public static function Get($max = null)
		{
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "Tenants";
			$result = $MySQL->query($query);
			$count = $result->num_rows;
			$retval = array();
			for ($i = 0; $i < $count; $i++)
			{
				$values = $result->fetch_assoc();
				$retval[] = Tenant::GetByAssoc($values);
			}
			return $retval;
		}
		public static function GetByID($id)
		{
			if (!is_numeric($id)) return null;
			
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "Tenants WHERE tenant_ID = " . $id;
			$result = $MySQL->query($query);
			$count = $result->num_rows;
			if ($count == 0) return null;
			
			$values = $result->fetch_assoc();
			return Tenant::GetByAssoc($values);
		}
		public static function GetByURL($url)
		{
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "Tenants WHERE tenant_URL = '" . $MySQL->real_escape_string($url) . "'";
			$result = $MySQL->query($query);
			if ($result === false)
			{
				echo("<html><head><title>Initialization Failure</title></head><body><h1>Initialization Failure</h1><p>A fatal error occurred when attempting to initialize the Objectify runtime.  Please make sure Objectify has been installed correctly on the server.</p><p>The Objectify runtime cannot be loaded (1001). Please contact the Web site administrator to inform them of this problem.</p><hr /><h3>System information</h3><table><tr><td>Tenant:</td><td>" . $url . "</td></tr><tr><td>Server: </td><td>" . $_SERVER["HTTP_HOST"] . "</td></tr></table></body></html>");
				die();
				return null;
			}
			
			$count = $result->num_rows;
			if ($count == 0)
			{
				echo("<html><head><title>Initialization Failure</title></head><body><h1>Initialization Failure</h1><p>A fatal error occurred when attempting to initialize the Objectify runtime.  Please make sure Objectify has been installed correctly on the server.</p><p>The Objectify runtime cannot find the requested tenant (1002). Please contact the Web site administrator to inform them of this problem.</p><hr /><h3>System information</h3><table><tr><td>Tenant:</td><td>" . $url . "</td></tr><tr><td>Server: </td><td>" . $_SERVER["HTTP_HOST"] . "</td></tr></table></body></html>");
				die();
				return null;
			}
			
			$values = $result->fetch_assoc();
			return Tenant::GetByAssoc($values);
		}
		
		public static function GetCurrent()
		{
			if (System::$TenantName == "") return null;
			return Tenant::GetByURL(System::$TenantName);
		}
		
		public function Update()
		{
			global $MySQL;
			if ($this->ID != null)
			{
				$query = "UPDATE " . System::$Configuration["Database.TablePrefix"] . "Tenants SET ";
				$query .= "tenant_URL = '" . $MySQL->real_escape_string($this->URL) . "', ";
				$query .= "tenant_Description = '" . $MySQL->real_escape_string($this->Description) . "', ";
				$query .= "tenant_Status = " . ($this->Status == TenantStatus::Enabled ? "1" : "0") . ", ";
				$query .= "tenant_TypeID = " . ($this->Type != null ? $this->Type->ID : "NULL") . ", ";
				$query .= "tenant_PaymentPlanID = " . ($this->PaymentPlan != null ? $this->PaymentPlan->ID : "NULL") . ", ";
				$query .= "tenant_BeginTimestamp = " . ($this->BeginTimestamp != null ? ("'" . $this->BeginTimestamp . "'") : "NULL") . ", ";
				$query .= "tenant_EndTimestamp = " . ($this->EndTimestamp != null ? ("'" . $this->EndTimestamp . "'") : "NULL");
				$query .= " WHERE tenant_ID = " . $this->ID;
			}
			else
			{
				$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "Tenants (tenant_URL, tenant_Description, tenant_Status, tenant_TypeID, tenant_PaymentPlanID, tenant_BeginTimestamp, tenant_EndTimestamp) VALUES (";
				$query .= "'" . $MySQL->real_escape_string($this->URL) . "', ";
				$query .= "'" . $MySQL->real_escape_string($this->Description) . "', ";
				$query .= ($this->Status == TenantStatus::Enabled ? "1" : "0") . ", ";
				$query .= ($this->Type != null ? $this->Type->ID : "NULL") . ", ";
				$query .= ($this->PaymentPlan != null ? $this->PaymentPlan->ID : "NULL") . ", ";
				$query .= ($this->BeginTimestamp != null ? ("'" . $this->BeginTimestamp . "'") : "NULL") . ", ";
				$query .= ($this->EndTimestamp != null ? ("'" . $this->EndTimestamp . "'") : "NULL");
				$query .= ")";
			}
			
			$result = $MySQL->query($query);
			if ($MySQL->errno != 0) return false;
			
			if ($this->ID == null)
			{
				$this->ID = $MySQL->insert_id;
			}
			
			// clearing the data centers
			$query = "DELETE FROM " . System::$Configuration["Database.TablePrefix"] . "TenantDataCenters WHERE tdc_TenantID = " . $this->ID;
			$result = $MySQL->query($query);
			if ($MySQL->errno != 0) return false;
			
			// inserting the data centers
			foreach ($this->DataCenters->Items as $item)
			{
				$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "TenantDataCenters (tdc_TenantID, tdc_DataCenterID) VALUES (";
				$query .= $this->ID . ", ";
				$query .= $item->ID;
				$query .= ")";
			
				$result = $MySQL->query($query);
				if ($MySQL->errno != 0) return false;
			}
			
			return true;
		}
		
		public function Delete()
		{
			global $MySQL;
			if ($this->ID == null) return false;
			
			// Relationships should cause all associated tenant data to be deleted.
			$query = "DELETE FROM " . System::$Configuration["Database.TablePrefix"] . "Tenants WHERE tenant_ID = " . $this->ID;
			$result = $MySQL->query($query);
			if ($MySQL->errno != 0) return false;
			
			return true;
		}
		
		/// <summary>
		/// Determines if an Objectify object with the specified name exists on the current tenant.
		/// </summary>
		public function HasObject($name)
		{
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjects WHERE (object_TenantID IS NULL OR object_TenantID = " . $this->ID . ") AND object_Name = '" . $MySQL->real_escape_string($name) . "'";
			
			$result = $MySQL->query($query);
			$count = $result->num_rows;
			return ($count != 0);
		}
		
		/// <summary>
		/// Gets an Objectify object from the current tenant.
		/// </summary>
		public function GetObject($name)
		{
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjects WHERE (object_TenantID IS NULL OR object_TenantID = " . $this->ID . ") AND object_Name = '" . $MySQL->real_escape_string($name) . "'";
			
			$result = $MySQL->query($query);
			$count = $result->num_rows;
			if ($count == 0)
			{
				Objectify::Log("No object with the specified name was found.", array
				(
					"Tenant" => $this->URL,
					"Object" => $name
				));
				return null;
			}
			$values = $result->fetch_assoc();
			$object = TenantObject::GetByAssoc($values);
			return $object;
		}
		
		public function CreateObject($name, $titles = null, $descriptions = null, $properties = null, $parentObject = null, $instances = null)
		{
			global $MySQL;
			if ($titles == null) $titles = array($name);
			if ($descriptions == null) $descriptions = array();
			if ($properties == null) $properties = array();
			if ($instances == null) $instances = array();
			
			// do not create the object if the object with the same name already exists
			if ($this->HasObject($name))
			{
				$bt = debug_backtrace();
				trigger_error("Object '" . $name . "' already exists on tenant '" . $this->URL . "' in " . $bt[0]["file"] . "::" . $bt[0]["function"] . " on line " . $bt[0]["line"] . "; ", E_USER_WARNING);
				return $this->GetObject($name);
			}
			
			$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "TenantObjects (object_TenantID, object_ModuleID, object_ParentObjectID, object_Name) VALUES (";
			$query .= $this->ID . ", ";
			$query .= "NULL" . ", ";
			$query .= ($parentObject == null ? "NULL" : $parentObject->ID) . ", ";
			$query .= "'" . $MySQL->real_escape_string($name) . "', ";
			$query .= ")";
			
			$result = $MySQL->query($query);
			if ($result === false) return false;
			
			$id = $MySQL->insert_id;
			$object = TenantObject::GetByID($id);
			
			$object->SetTitles($titles);
			$object->SetDescriptions($descriptions);
			
			foreach ($properties as $property)
			{
				$object->CreateInstanceProperty($property);
			}
			
			foreach ($instances as $instance)
			{
				$object->CreateInstance($instance);
			}
			
			return $object;
		}
		
		public function CreateEnumeration($name, $description = null, $choices = null)
		{
			global $MySQL;
			if ($choices == null) $choices = array();
			
			$item = new TenantEnumeration($name, $description, $choices);
			$item->Tenant = $this;
			$item->Choices = $choices;
			$item->Update();
			
			return $item;
		}
		
		public function GetProperties()
		{
			global $MySQL;
			
			$retval = array();
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantProperties WHERE (property_TenantID = " . $this->ID . " OR property_TenantID IS NULL)";
			$result = $MySQL->query($query);
			$count = $result->num_rows;
			for ($i = 0; $i < $count; $i++)
			{
				$values = $result->fetch_assoc();
				$retval[] = TenantProperty::GetByAssoc($values);
			}
			return $retval;
		}
		
		public function CreateProperty($property)
		{
			return TenantProperty::Create($property, $this);
		}
		
		public function GetProperty($propertyName)
		{
			global $MySQL;
			
			$retval = array();
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantProperties WHERE (property_TenantID = " . $this->ID . " OR property_TenantID IS NULL) AND property_Name = '" . $MySQL->real_escape_string($propertyName) . "'";
			$result = $MySQL->query($query);
			if ($result === false)
			{
				Objectify::Log("Database error when trying to obtain a reference to a property on the tenant.", array
				(
					"DatabaseError" => $MySQL->error . " (" . $MySQL->errno . ")",
					"Query" => $query
				));
				return null;
			}
			
			$count = $result->num_rows;
			if ($count == 0) return null;
			
			$values = $result->fetch_assoc();
			return TenantProperty::GetByAssoc($values);
		}
		
		public function GetPropertyValue($property, $defaultValue = null)
		{
			global $MySQL;
			
			// if we passed in a string to this property (because it's easier) then let's get a reference to that property
			if (is_string($property))
			{
				$propname = $property;
				$property = $this->GetProperty($property);
			}
			
			$query = "SELECT (CASE WHEN (propval_Value IS NULL) THEN property_DefaultValue ELSE propval_Value END) FROM " . System::$Configuration["Database.TablePrefix"] . "TenantPropertyValues, " . System::$Configuration["Database.TablePrefix"] . "TenantProperties WHERE " . System::$Configuration["Database.TablePrefix"] . "TenantProperties.property_ID = " . $property->ID . " AND " . System::$Configuration["Database.TablePrefix"] . "TenantProperties.property_ID = " . System::$Configuration["Database.TablePrefix"] . "TenantPropertyValues.propval_PropertyID";
			
			$result = $MySQL->query($query);
			if ($result === false)
			{
				Objectify::Log("Database error when trying to look up a value of a property on the tenant.", array
				(
					"DatabaseError" => $MySQL->error . " (" . $MySQL->errno . ")",
					"Query" => $query,
					"Property" => ($property == null ? ($propname == null ? "(null)" : $propname) : (is_string($property) ? $property : $property->Name))
				));
				return null;
			}
			
			$count = $result->num_rows;
			if ($count == 0)
			{
				Objectify::Log("The property has no defined values on the specified tenant.", array
				(
					"Property" => ($property == null ? ($propname == null ? "(null)" : $propname) : (is_string($property) ? $property : $property->Name))
				), LogMessageSeverity::Warning);
				if ($defaultValue != null) return $defaultValue;
				return $property->DefaultValue;
			}
			
			$values = $result->fetch_array();
			return $property->Decode($values[0]);
		}
		public function SetPropertyValue($property, $value)
		{
			global $MySQL;
			
			// if we passed in a string to this property (because it's easier) then let's get a reference to that property
			if (is_string($property))
			{
				$propname = $property;
				$property = $this->GetProperty($property);
			}
			
			$query = "UPDATE " . System::$Configuration["Database.TablePrefix"] . "TenantPropertyValues SET propval_Value = '" . $MySQL->real_escape_string($property->Encode($value)) . "' WHERE propval_PropertyID = " . $property->ID;
			$result = $MySQL->query($query);
			if ($result === false)
			{
				Objectify::Log("Database error when trying to update the value of a property on the tenant.", array
				(
					"DatabaseError" => $MySQL->error . " (" . $MySQL->errno . ")",
					"Query" => $query,
					"Property" => ($property == null ? ($propname == null ? "(null)" : $propname) : (is_string($property) ? $property : $property->Name))
				));
			}
			return ($MySQL->errno == 0);
		}
		
		public function ToJSON()
		{
			echo("{");
			echo("\"ID\":" . $this->ID . ",");
			echo("\"URL\":\"" . $this->URL . "\",");
			echo("\"Description\":\"" . $this->Description . "\"");
			echo("\"Status\":\"" . $this->Status . "\"");
			echo("\"Type\":" . $this->Type->ToJSON() . "");
			echo("\"PaymentPlan\":" . $this->PaymentPlan->ToJSON() . "");
			echo("\"BeginTimestamp\":\"" . $this->BeginTimestamp . "\"");
			echo("\"EndTimestamp\":\"" . $this->EndTimestamp . "\"");
			echo("}");
		}
	}
?>