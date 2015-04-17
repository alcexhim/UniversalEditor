<?php
	namespace Objectify\Objects;
	use WebFX\System;
	
	class PaymentPlan
	{
		public $ID;
		public $Title;
		public $Description;
		
		public static function GetByAssoc($values)
		{
			$item = new PaymentPlan();
			$item->ID = $values["paymentplan_ID"];
			$item->URL = $values["paymentplan_Title"];
			$item->Description = $values["paymentplan_Description"];
			return $item;
		}
		public static function Get($max = null)
		{
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "PaymentPlans";
			$result = $MySQL->query($query);
			$count = $result->num_rows;
			$retval = array();
			for ($i = 0; $i < $count; $i++)
			{
				$values = $result->fetch_assoc();
				$retval[] = PaymentPlan::GetByAssoc($values);
			}
			return $retval;
		}
		public static function GetByID($id)
		{
			if (!is_numeric($id)) return null;
			
			global $MySQL;
			$query = "SELECT * FROM " . System::$Configuration["Database.TablePrefix"] . "PaymentPlans WHERE paymentplan_ID = " . $id;
			$result = $MySQL->query($query);
			$count = $result->num_rows;
			if ($count == 0) return null;
			
			$values = $result->fetch_assoc();
			return PaymentPlan::GetByAssoc($values);
		}
		
		public function ToJSON()
		{
			echo("{");
			echo("\"ID\":" . $this->ID . ",");
			echo("\"Title\":\"" . \JH\Utilities::JavaScriptDecode($this->Title, "\"") . "\",");
			echo("\"Description\":\"" . \JH\Utilities::JavaScriptDecode($this->Description, "\"") . "\"");
			echo("}");
		}
	}
?>