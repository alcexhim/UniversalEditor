<?php
	namespace Objectify\Tenant\Pages;
	
	use WebFX\System;
	
	use Objectify\Tenant\MasterPages\WebPage;
	
	use Objectify\Objects\DataCenter;
	use Objectify\Objects\PaymentPlan;
	use Objectify\Objects\Tenant;
	use Objectify\Objects\TenantStatus;
	use Objectify\Objects\TenantType;
	
	class TenantPropertiesPage extends WebPage
	{
		public $Tenant;
		
		protected function Initialize()
		{
			if ($this->Tenant != null)
			{
				$this->Title = "Edit Tenant Configuration: " . $this->Tenant->URL;
			}
			else
			{
				$this->Title = "Create New Tenant";
			}
		}
		
		protected function RenderContent()
		{
			?>
			<h1>Edit Tenant Configuration</h1>
			<form method="POST">
				<table style="margin-left: auto; margin-right: auto; width: 500px;">
					<tr>
						<td style="width: 160px;"><label for="txtTenantURL">Name:</label></td>
						<td colspan="2"><input type="text" style="width: 100%;" id="txtTenantURL" name="tenant_URL" value="<?php if ($this->Tenant != null) echo($this->Tenant->URL); ?>" /></td>
					</tr>
					<tr>
						<td><label for="cboTenantType">Type:</label></td>
						<td colspan="2">
							<select id="cboTenantType" name="tenant_TypeID">
							<?php
								$tenanttypes = TenantType::Get();
								foreach ($tenanttypes as $tenanttype)
								{
									?><option<?php if ($this->Tenant != null && $this->Tenant->Type != null && $tenanttype->ID == $this->Tenant->Type->ID) { echo(" selected=\"selected\""); } ?> value="<?php echo($tenanttype->ID); ?>"><?php echo($tenanttype->Title); ?></option><?php
								}
							?>
							</select>
						</td>
					</tr>
					<tr>
						<td><label for="cboDataCenter">Data center:</label></td>
						<td colspan="2">
							<?php
								$datacenters = DataCenter::Get();
								foreach ($datacenters as $datacenter)
								{
									?><div><input type="checkbox" value="1" <?php if ($this->Tenant != null && $this->Tenant->DataCenters->Contains($datacenter)) { echo(" checked=\"checked\""); } ?> name="tenant_DataCenter_<?php echo($datacenter->ID); ?>" id="txtTenantDataCenter_<?php echo($datacenter->ID); ?>" /><label for="txtTenantDataCenter_<?php echo($datacenter->ID); ?>"><?php echo($datacenter->Title); ?></label><?php
								}
							?>
							</select>
						</td>
					</tr>
					<tr>
						<td><label for="cboPaymentPlan">Payment plan:</label></td>
						<td colspan="2">
							<select id="cboPaymentPlan" name="tenant_PaymentPlanID">
							<?php
								$paymentplans = PaymentPlan::Get();
								foreach ($paymentplans as $paymentplan)
								{
									?><option<?php if ($this->Tenant != null && $this->Tenant->PaymentPlan != null && $paymentplan->ID == $this->Tenant->PaymentPlan->ID) { echo(" selected=\"selected\""); } ?> value="<?php echo($paymentplan->ID); ?>"><?php echo($paymentplan->Title); ?></option><?php
								}
							?>
							</select>
						</td>
					</tr>
					<tr>
						<td><label for="txtActivationDate">Activation date:</label></td>
						<td><input type="text" style="width: 100%;" id="txtActivationDate" name="tenant_BeginTimestamp" value="<?php if ($this->Tenant != null) echo($this->Tenant->BeginTimestamp); ?>" /></td>
						<td style="width: 96px"><input type="checkbox" id="chkActivationDateValid" name="tenant_BeginTimestampValid" value="1" /><label for="chkActivationDateValid">Indefinite</label></td>
					</tr>
					<tr>
						<td><label for="txtTerminationDate">Termination date:</label></td>
						<td><input type="text" style="width: 100%;" id="txtTerminationDate" name="tenant_EndTimestamp" value="<?php if ($this->Tenant != null) echo($this->Tenant->EndTimestamp); ?>" /></td>
						<td style="width: 96px"><input type="checkbox" id="chkTerminationDateValid" name="tenant_EndTimestampValid" value="1" /><label for="chkTerminationDateValid">Indefinite</label></td>
					</tr>
					<tr>
						<td style="vertical-align: top;"><label for="txtDescription">Description:</label></td>
						<td colspan="2"><textarea style="width: 100%;" rows="5" id="txtDescription" name="tenant_Description"><?php if ($this->Tenant != null) echo($this->Tenant->Description); ?></textarea></td>
					</tr>
				</table>
				<div class="Buttons">
					<input type="submit" value="Save Changes" />
					<a class="Button" href="<?php echo(System::ExpandRelativePath("~/tenant")); ?>">Cancel</a>
				</div>
			</form>
			<?php
		}
	}
?>