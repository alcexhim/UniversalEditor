<?php
	namespace Objectify\Tenant\Pages;
	
	use WebFX\System;
	
	use Objectify\Tenant\MasterPages\WebPage;
	use Objectify\Objects\DataCenter;
	
	class DataCenterMainPage extends WebPage
	{
		protected function RenderContent()
		{
			$datacenters = DataCenter::Get();
		?>
		<h1>Data Center Management</h1>
		<p>There are <?php echo(count($datacenters)); ?> data centers in total.  Click a data center name to configure that data center.</p>
		
		<div class="Toolbar">
			<a class="Button" href="<?php echo(System::ExpandRelativePath("~/datacenter/create")); ?>">Create New Data Center</a>
		</div>
		<table class="ListView" style="width: 100%;">
			<tr>
				<th>Data Center</th>
				<th>Description</th>
				<th>Hostname</th>
			</tr>
			<?php
				foreach ($datacenters as $datacenter)
				{
					?>
					<tr>
						<td><a href="<?php echo(System::ExpandRelativePath("~/datacenter/modify/" . $datacenter->ID)); ?>"><?php echo($datacenter->Title); ?></a></td>
						<td><?php echo($datacenter->Description); ?></td>
						<td><a href="http://<?php echo($datacenter->HostName); ?>"><?php echo($datacenter->HostName); ?></a></td>
					</tr>
					<?php
				}
			?>
		</table>
		<?php
		}
	}
?>