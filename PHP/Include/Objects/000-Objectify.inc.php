<?php
	namespace Objectify\Objects;
	use WebFX\System;
	
	\Enum::Create("Objectify\\Objects\\LogMessageSeverity", "Notice", "Warning", "Error");
	
	class Objectify
	{
		public static function Log($message, $params = null, $severity = LogMessageSeverity::Error)
		{
			$bt = debug_backtrace(DEBUG_BACKTRACE_IGNORE_ARGS);
			
			if ($params == null) $params = array();
			
			global $MySQL;
			
			$tenant = Tenant::GetCurrent();
			
			$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "DebugMessages (message_TenantID, message_Content, message_SeverityID, message_Timestamp, message_IPAddress) VALUES (";
			$query .= ($tenant == null ? "NULL" : $tenant->ID) . ", ";
			$query .= "'" . $MySQL->real_escape_string($message) . "', ";
			$query .= $severity . ", ";
			$query .= "NOW(), ";
			$query .= "'" . $MySQL->real_escape_string($_SERVER["REMOTE_ADDR"]) . "'";
			$query .= ")";
			$MySQL->query($query);
			
			$msgid = $MySQL->insert_id;
			
			foreach ($bt as $bti)
			{
				$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "DebugMessageBacktraces (bt_MessageID, bt_FileName, bt_LineNumber) VALUES (";
				$query .= $msgid . ", ";
				$query .= "'" . $MySQL->real_escape_string($bti["file"]) . "', ";
				$query .= $bti["line"];
				$query .= ")";
				$MySQL->query($query);
			}
			
			foreach ($params as $key => $value)
			{
				$query = "INSERT INTO " . System::$Configuration["Database.TablePrefix"] . "DebugMessageParameters (mp_MessageID, mp_Name, mp_Value) VALUES (";
				$query .= $msgid . ", ";
				$query .= "'" . $MySQL->real_escape_string($key) . "', ";
				$query .= "'" . $MySQL->real_escape_string($value) . "'";
				$query .= ")";
				$MySQL->query($query);
			}
		}
	}
?>