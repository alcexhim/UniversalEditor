<?php
	namespace Objectify\Objects;
	use WebFX\System;
	
	class TenantObjectMethod
	{
		public $ID;
		public $ParentObject;
		public $Name;
		public $Description;
		public $CodeBlob;
		
		public static function GetByAssoc($values)
		{
			$item = new TenantObjectMethod();
			$item->ID = $values["method_ID"];
			$item->ParentObject = TenantObject::GetByID($values["method_ObjectID"]);
			$item->Name = $values["method_Name"];
			$item->Description = $values["method_Description"];
			$item->CodeBlob = $values["method_CodeBlob"];
			return $item;
		}

		public function AddNamespaceReference($value)
		{
			global $MySQL;
			$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "TenantObjectMethodNamespaceReferences (ns_MethodID, ns_Value) VALUES (";
			$query .= $this->ID . ", ";
			$query .= "'" . $MySQL->real_escape_string($value) . "'";
			$query .= ")";
			
			$result = $MySQL->query($query);
			if ($result === false)
			{
				Objectify::Log("Database error when trying to add a namespace reference to the specified object method.", array
				(
					"DatabaseError" => $MySQL->error . " (" . $MySQL->errno . ")",
					"Query" => $query,
					"Method" => $this->Name,
					"Object" => $this->ParentObject == null ? "(null)" : $this->ParentObject->Name
				));
				return false;
			}
			return true;
		}
		public function RemoveNamespaceReference($value)
		{
			global $MySQL;
			$query = "DELETE FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectMethodNamespaceReferences WHERE ";
			$query .= "ns_MethodID = " . $this->ID . " AND ";
			$query .= "ns_Value = '" . $MySQL->real_escape_string($value) . "'";
			
			$result = $MySQL->query($query);
			if ($result === false) return false;
			return true;
		}
		public function GetNamespaceReferences()
		{
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectMethodNamespaceReferences WHERE ns_MethodID = " . $this->ID;
			$retval = array();
			$result = $MySQL->query($query);
			if ($result === false) return $retval;
			$count = $result->num_rows;
			for ($i = 0; $i < $count; $i++)
			{
				$values = $result->fetch_assoc();
				$retval[] = TenantObjectMethodNamespaceReference::GetByAssoc($values);
			}
			return $retval;
		}
		
		public function Update()
		{
			global $MySQL;
			
			if ($this->ID == null)
			{
				$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "TenantObjectMethods (method_ObjectID, method_Name, method_Description, method_CodeBlob) VALUES (";
				$query .= ($this->Tenant == null ? "NULL" : $this->Tenant->ID) . ", ";
				$query .= ($this->ParentObject == null ? "NULL" : $this->ParentObject->ID) . ", ";
				$query .= "'" . $MySQL->real_escape_string($this->Name) . "', ";
				$query .= "'" . $MySQL->real_escape_string($this->Description) . "', ";
				$query .= "'" . $MySQL->real_escape_string($this->CodeBlob) . "'";
				$query .= ")";
			}
			else
			{
				$query = "UPDATE " . System::$Configuration["Database.TablePrefix"] . "TenantObjectMethods SET ";
				$query .= "method_ObjectID = " . ($this->ParentObject == null ? "NULL" : $this->ParentObject->ID) . ", ";
				$query .= "method_Name = '" . $MySQL->real_escape_string($this->Name) . "', ";
				$query .= "method_Description = '" . $MySQL->real_escape_string($this->Description) . "', ";
				$query .= "method_CodeBlob = '" . $MySQL->real_escape_string($this->CodeBlob) . "'";
				$query .= " WHERE method_ID = " . $this->ID;
			}
			
			$result = $MySQL->query($query);
			if ($result === false) return false;
			
			if ($this->ID == null)
			{
				$this->ID = $MySQL->insert_id;
			}
			return true;
		}
		
		public static function GetByID($id)
		{
			if (!is_numeric($id)) return null;
			
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectMethods WHERE method_ID = " . $id;
			$result = $MySQL->query($query);
			if ($result === false) return null;
			
			$count = $result->num_rows;
			if ($count == 0) return null;
			
			$values = $result->fetch_assoc();
			return TenantObjectMethod::GetByAssoc($values);
		}
		
		public function Execute($parameters = null)
		{
			if ($parameters == null) $parameters = array();
			
			$func = "";
			$nses = $this->GetNamespaceReferences();
			foreach ($nses as $ns)
			{
				$func .= "use " . $ns->Value . ";\n";
			}
			
			$func .= "\$x = function(\$thisObject";
			$count = count($parameters);
			if ($count > 0) $func .= ", ";
			
			for ($i = 0; $i < $count; $i++)
			{
				$parameter = $parameters[$i];
				$func .= "\$" . $parameter->ParameterName;
				if ($i < $count - 1)
				{
					$func .= ", ";
				}
			}
			$func .= "){";
			$func .= $this->CodeBlob;
			$func .= "}; return \$x(";
			$func .= "Objectify\\Objects\\TenantObject::GetByID(" . $this->ParentObject->ID . ")";
			if ($count > 0) $func .= ", ";
			
			for ($i = 0; $i < $count; $i++)
			{
				$parameter = $parameters[$i];
				if (is_string($parameter->Value))
				{
					$func .= ("'" . $parameter->Value . "'");
				}
				else
				{
					$func .= $parameter->Value;
				}
				if ($i < $count - 1)
				{
					$func .= ", ";
				}
			}
			$func .= ");";
			
			return eval($func);
		}
	}
	class TenantObjectInstanceMethod
	{
		public $ID;
		public $ParentObject;
		public $Name;
		public $Description;
		public $CodeBlob;
		
		public static function GetByAssoc($values)
		{
			$item = new TenantObjectInstanceMethod();
			$item->ID = $values["method_ID"];
			$item->ParentObject = TenantObject::GetByID($values["method_ObjectID"]);
			$item->Name = $values["method_Name"];
			$item->Description = $values["method_Description"];
			$item->CodeBlob = $values["method_CodeBlob"];
			return $item;
		}
		
		public static function GetByID($id)
		{
			if (!is_numeric($id)) return null;
			
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstanceMethods WHERE method_ID = " . $id;
			$result = $MySQL->query($query);
			if ($result === false) return null;
			
			$count = $result->num_rows;
			if ($count == 0) return null;
			
			$values = $result->fetch_assoc();
			return TenantObjectInstanceMethod::GetByAssoc($values);
		}

		public function AddNamespaceReference($value)
		{
			global $MySQL;
			$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstanceMethodNamespaceReferences (ns_MethodID, ns_Value) VALUES (";
			$query .= $this->ID . ", ";
			$query .= "'" . $MySQL->real_escape_string($value) . "'";
			$query .= ")";
			
			$result = $MySQL->query($query);
			if ($result === false) return false;
			return true;
		}
		public function RemoveNamespaceReference($value)
		{
			global $MySQL;
			$query = "DELETE FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstanceMethodNamespaceReferences WHERE ";
			$query .= "ns_MethodID = " . $this->ID . " AND ";
			$query .= "ns_Value = '" . $MySQL->real_escape_string($value) . "'";
			
			$result = $MySQL->query($query);
			if ($result === false) return false;
			return true;
		}
		public function GetNamespaceReferences()
		{
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "TenantObjectInstanceMethodNamespaceReferences WHERE ns_MethodID = " . $this->ID;
			$retval = array();
			$result = $MySQL->query($query);
			if ($result === false) return $retval;
			$count = $result->num_rows;
			for ($i = 0; $i < $count; $i++)
			{
				$values = $result->fetch_assoc();
				$retval[] = TenantObjectMethodNamespaceReference::GetByAssoc($values);
			}
			return $retval;
		}
		
		public function Execute($parameters)
		{
			if ($parameters == null) $parameters = array();
			
			$func = "";
			$nses = $this->GetNamespaceReferences();
			foreach ($nses as $ns)
			{
				$func .= "use " . $ns->Value . ";\n";
			}
			
			$func .= "\$x = function(";
			$count = count($parameters);
			for ($i = 0; $i < $count; $i++)
			{
				$parameter = $parameters[$i];
				$func .= "\$" . $parameter->ParameterName;
				if ($i < $count - 1)
				{
					$func .= ", ";
				}
			}
			$func .= "){";
			$func .= $this->CodeBlob;
			$func .= "}; return \$x(";
			for ($i = 0; $i < $count; $i++)
			{
				$parameter = $parameters[$i];
				if (is_string($parameter->Value))
				{
					$func .= ("'" . $parameter->Value . "'");
				}
				else
				{
					$func .= $parameter->Value;
				}
				if ($i < $count - 1)
				{
					$func .= ", ";
				}
			}
			$func .= ");";
			return eval($func);
		}
	}
	class TenantObjectMethodParameter
	{
		public $Name;
		public $DefaultValue;
		
		public function __construct($name, $defaultValue = null)
		{
			$this->Name = $name;
			$this->DefaultValue = $defaultValue;
		}
	}
	class TenantObjectMethodParameterValue
	{
		public $ParameterName;
		public $Value;
		
		public function __construct($parameterName, $value = null)
		{
			$this->ParameterName = $parameterName;
			$this->Value = $value;
		}
	}
	class TenantObjectMethodNamespaceReference
	{
		public $ID;
		public $Value;
		
		public static function GetByAssoc($values)
		{
			$item = new TenantObjectMethodNamespaceReference();
			$item->ID = $values["ns_ID"];
			$item->Value = $values["ns_Value"];
			return $item;
		}
	}
?>