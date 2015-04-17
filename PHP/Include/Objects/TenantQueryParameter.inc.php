<?php
	namespace Objectify\Objects;
	
	class TenantQueryParameter
	{
		public $Name;
		public $Value;
		
		public function __construct($name, $value)
		{
			$this->Name = $name;
			$this->Value = $value;
		}
	}
?>