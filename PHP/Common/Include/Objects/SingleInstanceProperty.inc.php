<?php
	namespace Objectify\Objects;
	
	class SingleInstanceProperty
	{
		private $mvarInstance;
		public function GetInstance()
		{
			return $this->mvarInstance;
		}
		public function SetInstance($value)
		{
			foreach ($this->ValidObjects as $obj)
			{
				if ($obj->ID != $value->ParentObject->ID) return false;
			}
			$this->mvarInstance = $value;
			return true;
		}
		
		public $ValidObjects;
		
		public function __construct($instance = null, $validObjects = null)
		{
			$this->mvarInstance = $instance;
			
			if ($validObjects == null) $validObjects = array();
			$this->ValidObjects = $validObjects;
		}
	}
?>