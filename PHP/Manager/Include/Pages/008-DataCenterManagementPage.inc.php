<?php
	namespace Objectify\Tenant\Pages;
	
	use WebFX\System;
	
	use Objectify\Tenant\MasterPages\WebPage;
	use Objectify\Objects\DataCenter;
	
	class DataCenterManagementPage extends WebPage
	{
		public $DataCenter;
		
		protected function Initialize()
		{
			$this->Title = "Manage Data Center: " . $this->DataCenter->Title;
		}
		
		protected function RenderContent()
		{
		?>
		<h1>Data Center Management</h1>
		<h2><?php echo($this->DataCenter->Title); ?></h2>
		
		<form method="POST">
			<h3>Information</h3>
			<table style="width: 100%;" border="1">
				<tr>
					<td style="width: 100px;">Title: </td>
					<td><input name="datacenter_Title" type="text" value="<?php echo($this->DataCenter->Title); ?>" /></td>
				</tr>
				<tr>
					<td style="vertical-align: top;">Description: </td>
					<td><textarea name="datacenter_Description" style="width: 100%;" rows="5"><?php echo($this->DataCenter->Description); ?></textarea></td>
				</tr>
				<tr>
					<td style="vertical-align: top;">Hostname: </td>
					<td><input name="datacenter_HostName" type="text" value="<?php echo($this->DataCenter->HostName); ?>" /></td>
				</tr>
			</table>
			
			<div class="Buttons" style="text-align: right;">
				<input type="submit" value="Save Changes" /> <a href="<?php echo(System::ExpandRelativePath("~/datacenter")); ?>">Back to Data Centers</a>
			</div>
		</form>
		<?php
		}
	}
?>