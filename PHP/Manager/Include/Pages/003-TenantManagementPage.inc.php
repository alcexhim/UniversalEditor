<?php
	namespace Objectify\Tenant\Pages;
	
	use WebFX\System;
	use WebFX\Controls\TabContainer;
	use WebFX\Controls\TabPage;
	
	use Objectify\Tenant\MasterPages\WebPage;
	
	use Objectify\Objects\Module;
	use Objectify\Objects\PaymentPlan;
	use Objectify\Objects\Tenant;
	use Objectify\Objects\TenantObject;
	use Objectify\Objects\TenantProperty;
	use Objectify\Objects\TenantPropertyValue;
	use Objectify\Objects\TenantStatus;
	use Objectify\Objects\TenantType;
	
	class TenantManagementPage extends WebPage
	{
		public $Tenant;
		
		protected function Initialize()
		{
			$this->Title = "Manage Tenant: " . $this->Tenant->URL;
		}
		
		protected function RenderContent()
		{
			?>
			<h1>Tenant: <?php echo($this->Tenant->URL); ?></h1>
			<form method="POST">
				<?php
				$tbs = new TabContainer("tbsTabs");
				$tbs->TabPages[] = new TabPage("tabCustomProperties", "Custom Properties", null, null, null, function()
				{
					?>
					<table class="ListView" style="width: 100%;">
						<tr id="Property_Header">
							<th style="width: 32px;"><a href="#" onclick="AddPropertyBelow('Header');" title="Add Below">[+]</a></th>
							<th style="width: 256px;">Property</th>
							<th>Description</th>
							<th style="width: 256px;">Value</th>
						</tr>
						<?php
							$properties = $this->Tenant->GetProperties();
							foreach ($properties as $property)
							{
								?>
								<tr id="Property_<?php echo($property->ID); ?>">
									<td style="width: 32px;"><a href="#" onclick="AddPropertyBelow('<?php echo($property->ID); ?>');" title="Add Below">[+]</a></td>
									<td><?php echo($property->Name); ?></td>
									<td><?php echo($property->Description); ?></td>
									<td><?php $property->RenderEditor($this->Tenant->GetPropertyValue($property)); ?></td>
								</tr>
								<?php
							}
						?>
					</table>
					<?php
				});
				
				$tbs->TabPages[] = new TabPage("tabEnabledModules", "Enabled Modules", null, null, null, function()
				{
					?>
					<p>Click on a module name to configure the module on this tenant.</p>
					<table class="ListView" style="width: 100%;">
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
				});
				
				$tbs->TabPages[] = new TabPage("tabGlobalObjects", "Global Objects", null, null, null, function()
				{
					?>
					<p>Lists all of the objects that are available on this tenant that are not associated with a particular Module.</p>
					<table class="ListView" style="width: 100%;">
						<tr>
							<th style="width: 128px;">Object</th>
							<th>Description</th>
							<th>Instances</th>
						</tr>
						<?php
							$objects = TenantObject::Get(null, $this->Tenant);
							foreach ($objects as $object)
							{
								?>
								<tr>
									<td><a href="<?php echo(System::ExpandRelativePath("~/tenant/manage/" . $this->Tenant->URL . "/objects/" . $object->ID)); ?>"><?php echo($object->Name); ?></a></td>
									<td><?php echo($object->Description); ?></td>
									<td><a href="<?php echo(System::ExpandRelativePath("~/tenant/manage/" . $this->Tenant->URL . "/objects/" . $object->ID . "/instances")); ?>"><?php echo($object->CountInstances()); ?></a></td>
								</tr>
								<?php
							}
						?>
					</table>
				<?php
				});
				
				$tbs->Render();
				?>
				<div class="Buttons">
					<input type="submit" value="Save Changes" />
					<a class="Button" href="<?php echo(System::ExpandRelativePath("~/tenant")); ?>">Cancel</a>
				</div>
			</form>
			<?php
		}
	}
?>