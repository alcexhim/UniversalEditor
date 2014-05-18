<?php
	namespace Objectify\Tenant\Pages;
	
	use WebFX\System;
	
	use Objectify\Tenant\MasterPages\WebPage;
	
	// use Objectify\Objects\DataCenter;
	use Objectify\Objects\Module;
	use Objectify\Objects\PaymentPlan;
	use Objectify\Objects\Tenant;
	use Objectify\Objects\TenantProperty;
	use Objectify\Objects\TenantPropertyValue;
	use Objectify\Objects\TenantStatus;
	use Objectify\Objects\TenantType;
	
	class TenantModuleManagementPage extends WebPage
	{
		public $Tenant;
		public $Module;
		
		protected function Initialize()
		{
			if ($this->Module != null)
			{
				$this->Title = "Manage Tenant Module: " . $this->Module->Title . " on " . $this->Tenant->URL;
			}
			else
			{
				$this->Title = "Manage Tenant Modules: " . $this->Tenant->URL;
			}
		}
		
		protected function RenderContent()
		{
			if ($this->Module != null)
			{
			?>
			<h1>Module: <?php echo($this->Module->Title); ?> on <?php echo($this->Tenant->URL); ?></h1>
			<h2>Module Configurable Properties</h2>
			<table style="width: 100%;">
				<tr id="Property_Header">
					<th style="width: 32px;"><a href="#" onclick="AddPropertyBelow('Header');" title="Add Below">[+]</a></th>
					<th style="width: 256px;">Property</th>
					<th>Value</th>
				</tr>
				<?php
				/*
					$properties = $this->Module->GetProperties();
					foreach ($properties as $property)
					{
						?>
						<tr id="Property_<?php echo($property->ID); ?>">
							<td style="width: 32px;"><a href="#" onclick="AddPropertyBelow('<?php echo($property->ID); ?>');" title="Add Below">[+]</a></td>
							<td><?php echo($property->Title); ?></td>
							<td><input style="width: 100%;" type="text" id="txtProperty_<?php echo($property->ID); ?>" name="Property_<?php echo($property->ID); ?>" value="<?php echo($this->Tenant->GetPropertyValue($property)); ?>" /></td>
						</tr>
						<?php
					}
				*/
				?>
			</table>
			<?php
			}
			else
			{
				?>
				<h1>Tenant: <?php echo($this->Tenant->URL); ?></h1>
				<?php
			}
			?>
			<h2>Tenant Enabled Modules</h2>
			<p>Click on a module name to configure it</p>
			<table style="width: 100%;">
				<tr>
					<th style="width: 128px;">Module</th>
					<th>Description</th>
				</tr>
				<?php
					$modules = Module::Get(null, $this->Tenant);
					foreach ($modules as $module)
					{
						?>
						<tr>
							<td><input id="chkModule_<?php echo($module->ID); ?>" type="checkbox"<?php if ($module->Enabled) { echo(" checked=\"checked\""); } ?> /> <a href="<?php echo(System::ExpandRelativePath("~/tenant/manage/" . $this->Tenant->URL . "/modules/" . $module->ID)); ?>"><?php echo($module->Title); ?></a></td>
							<td><?php echo($module->Description); ?></td>
						</tr>
						<?php
					}
				?>
			</table>
			<?php
		}
	}
?>