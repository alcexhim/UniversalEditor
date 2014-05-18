<?php
	namespace Objectify\Tenant\Pages;
	
	use WebFX\System;
	
	use WebFX\Controls\TabContainer;
	use WebFX\Controls\TabPage;
	
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
	
	class TenantObjectManagementPage extends WebPage
	{
		public $CurrentTenant;
		public $CurrentObject;
		
		protected function Initialize()
		{
			if ($this->CurrentObject != null)
			{
				$this->Title = "Manage Tenant Object: " . $this->CurrentObject->Name . " on " . $this->CurrentTenant->URL;
			}
			else
			{
				$this->Title = "Manage Tenant Objects: " . $this->CurrentTenant->URL;
			}
		}
		
		protected function RenderContent()
		{
			if ($this->CurrentObject != null)
			{
			?>
			<h1>Object: <?php echo($this->CurrentObject->Name); ?> on <?php echo($this->CurrentTenant->URL); ?></h1>
			<form method="POST">
			<?php
				$tbs = new TabContainer("tbsObject");
				//								ID						title			imageurl	targeturl	script	contentfunc
				$tbs->TabPages[] = new TabPage("tabStaticProperties", "Static Properties", null, null, null, function()
				{
					?>
					<table class="ListView" style="width: 100%;">
						<tr id="Property_Header">
							<th style="width: 32px;"><a href="#" onclick="AddPropertyBelow('Header');" title="Add Below">[+]</a></th>
							<th style="width: 256px;">Property</th>
							<th style="width: 128px;">Data Type</th>
							<th>Default Value</th>
						</tr>
						<?php
							$properties = $this->CurrentObject->GetProperties();
							foreach ($properties as $property)
							{
								?>
								<tr id="StaticProperty_<?php echo($property->ID); ?>">
									<td style="width: 32px;"><a href="#" onclick="AddStaticPropertyBelow('<?php echo($property->ID); ?>');" title="Add Below">[+]</a></td>
									<td><?php echo($property->Name); ?></td>
									<td><?php echo($property->DataType == null ? "(undefined)" : "<a href=\"" . System::ExpandRelativePath("~/datatype/modify/" . $property->DataType->ID) . "\">" . $property->DataType->Name . "</a>"); ?></td>
									<td>
									<?php
									if ($property->DataType == null || $property->DataType->ColumnRendererCodeBlob == null)
									{
										?>
										<input style="width: 100%;" type="text" id="txtStaticProperty_<?php echo($property->ID); ?>" name="StaticProperty_<?php echo($property->ID); ?>" value="<?php echo($property->DefaultValue); ?>" />
										<?php
									}
									else
									{
										call_user_func($property->DataType->ColumnRendererCodeBlob, $property->Value);
									}
									?>
									</td>
								</tr>
								<?php
							}
						?>
					</table>
					<?php
				});
				$tbs->TabPages[] = new TabPage("tabInstanceProperties", "Instance Properties", null, null, null, function()
				{
					?>
					<script type="text/javascript">
					function AddInstancePropertyBelow(id)
					{
						var hdnNewPropertyCount = document.getElementById("hdnNewPropertyCount");
						hdnNewPropertyCount.value = parseInt(hdnNewPropertyCount.value) + 1;
						
						var parentRow = document.getElementById("InstanceProperty_" + id);
						var table = parentRow.parentElement;
						
						var tr = table.insertRow(parentRow.sectionRowIndex + 1);
						
						var tdAdd = tr.insertCell(-1);
						tdAdd.innerHTML = "<a href=\"#\" onclick=\"AddInstancePropertyBelow('');\" title=\"Add Below\">[+]</a>";
						
						var tdProperty = tr.insertCell(-1);
						tdProperty.innerHTML = "<input type=\"text\" name=\"InstanceProperty_" + hdnNewPropertyCount.value + "_Name\" />";
						
						var tdDataType = tr.insertCell(-1);
						tdDataType.innerHTML = "<input type=\"text\" name=\"InstanceProperty_" + hdnNewPropertyCount.value + "_DataTypeID\" />";
						
						var tdDefaultValue = tr.insertCell(-1);
						tdDefaultValue.innerHTML = "<input type=\"text\" name=\"InstanceProperty_" + hdnNewPropertyCount.value + "_DefaultValue\" />";
					}
					</script>
					<input type="hidden" id="hdnNewPropertyCount" name="InstanceProperty_NewPropertyCount" value="0" />
					<table class="ListView" style="width: 100%;">
						<tr id="InstanceProperty_Header">
							<th style="width: 32px;"><a href="#" onclick="AddInstancePropertyBelow('Header'); return false;" title="Add Below">[+]</a></th>
							<th style="width: 256px;">Property</th>
							<th style="width: 128px;">Data Type</th>
							<th>Default Value</th>
						</tr>
						<?php
							$properties = $this->CurrentObject->GetInstanceProperties();
							foreach ($properties as $property)
							{
								?>
								<tr id="InstanceProperty_<?php echo($property->ID); ?>">
									<td style="width: 32px;"><a href="#" onclick="AddInstancePropertyBelow('<?php echo($property->ID); ?>'); return false;" title="Add Below">[+]</a></td>
									<td><?php echo($property->Name); ?></td>
									<td><?php echo($property->DataType == null ? "(undefined)" : "<a href=\"" . System::ExpandRelativePath("~/datatype/modify/" . $property->DataType->ID) . "\">" . $property->DataType->Name . "</a>"); ?></td>
									<td>
									<?php
									$property->RenderColumn();
									?>
									</td>
								</tr>
								<?php
							}
						?>
					</table>
					<?php
				});
				$tbs->TabPages[] = new TabPage("tabStaticMethods", "Static Methods", null, null, null, function()
				{
					?>
					<table class="ListView" style="width: 100%;">
						<tr id="StaticMethod_Header">
							<th style="width: 32px;"><a href="#" onclick="AddStaticMethodBelow('Header');" title="Add Below">[+]</a></th>
							<th style="width: 256px;">Method</th>
							<th>Description</th>
							<th style="width: 128px;">Return Data Type</th>
						</tr>
						<?php
							$methods = $this->CurrentObject->GetMethods();
							foreach ($methods as $method)
							{
								?>
								<tr id="StaticMethod_<?php echo($method->ID); ?>">
									<td style="width: 32px;"><a href="#" onclick="AddStaticMethodBelow('<?php echo($method->ID); ?>');" title="Add Below">[+]</a></td>
									<td><a href="<?php echo(System::ExpandRelativePath("~/tenant/manage/" . $this->CurrentTenant->URL . "/objects/" . $this->CurrentObject->ID . "/methods/static/" . $method->ID)); ?>"><?php echo($method->Name); ?></a></td>
									<td><?php echo($method->Description); ?></td>
									<td><?php /* echo($method->DataType == null ? "(undefined)" : "<a href=\"#\">" . $method->DataType->Name . "</a>"); */ ?></td>
								</tr>
								<?php
							}
						?>
					</table>
					<?php
				});
				$tbs->TabPages[] = new TabPage("tabInstances", "Instances", null, null, null, function()
				{
					?>
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
				});
				$tbs->Render();
				?>
			
					<div class="Buttons">
						<input type="submit" value="Save Changes" />
						<a class="Button" href="<?php echo(System::ExpandRelativePath("~/tenant/manage/" . $this->CurrentTenant->URL)); ?>">Cancel</a>
					</div>
				</form>
				<?php
			}
		}
	}
?>