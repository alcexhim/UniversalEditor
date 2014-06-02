<?php
	namespace Objectify\Tenant\Pages;
	
	use WebFX\System;
	
	use Objectify\Tenant\MasterPages\WebPage;
	
	// use Objectify\Objects\DataCenter;
	use Objectify\Objects\Module;
	use Objectify\Objects\PaymentPlan;
	use Objectify\Objects\Tenant;
	use Objectify\Objects\TenantObject;
	use Objectify\Objects\TenantProperty;
	use Objectify\Objects\TenantPropertyValue;
	use Objectify\Objects\TenantStatus;
	use Objectify\Objects\TenantType;
	
	class TenantObjectInstanceBrowsePage extends WebPage
	{
		public $CurrentTenant;
		public $CurrentObject;
		
		protected function Initialize()
		{
			$this->Title = "Browse Instances of Tenant Object: " . $this->CurrentObject->Name . " on " . $this->CurrentTenant->URL;
		}
		
		protected function RenderContent()
		{
			if ($this->CurrentObject != null)
			{
			?>
			<h1>Object: <?php echo($this->CurrentObject->Name); ?> on <?php echo($this->CurrentTenant->URL); ?></h1>
			<h2>Object Instances</h2>
			<table style="width: 100%;">
				<tr id="Property_Header">
				<?php
					$properties = $this->CurrentObject->GetInstanceProperties();
					foreach ($properties as $property)
					{
						if ($property->ColumnVisible)
						{
							echo("<th>" . $property->Name . "</th>");
						}
					}
				?>
				</tr>
				<?php
					$instances = $this->CurrentObject->GetInstances();
					foreach ($instances as $instance)
					{
						?>
						<tr id="Property_<?php echo($property->ID); ?>">
						<?php
							foreach ($properties as $property)
							{
								if ($property->ColumnVisible)
								{
									echo("<td>");
									$value = $instance->GetPropertyValue($property);
									$property->RenderColumn($value);
									echo("</td>");
								}
							}
						?>
						</tr>
						<?php
					}
				?>
			</table>
			<?php
			}
		}
	}
?>