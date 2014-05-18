<?php
	namespace Objectify\Tenant\Pages;
	
	use WebFX\System;
	
	use Objectify\Tenant\MasterPages\WebPage;
	use Objectify\Objects\Tenant;
	
	class MainPage extends WebPage
	{
		protected function RenderContent()
		{
			$tenants = Tenant::Get();
		?>
		<h1>Tenant Management</h1>
		<p>There are <?php echo(count($tenants)); ?> tenants in total.</p>
		
		<div class="Toolbar">
			<a class="Button" href="<?php echo(System::ExpandRelativePath("~/tenant/create")); ?>">Create New Tenant</a>
		</div>
		<?php
			$countActive = 0;
			$countExpired = 0;
			foreach ($tenants as $tenant)
			{
				if ($tenant->IsExpired())
				{
					$countExpired++;
				}
				else
				{
					$countActive++;
				}
			}
		?>
		<h2>Active Tenants (<?php echo($countActive); ?>)</h2>
		<table class="ListView" style="width: 100%;" border="1">
			<tr>
				<th>Tenant Name</th>
				<th>Tenant Type</th>
				<th>Data Centers</th>
				<th>Payment Plan</th>
				<th>Activation Date</th>
				<th>Termination Date</th>
				<th>Description</th>
				<th>Actions</th>
			</tr>
			<?php
				foreach ($tenants as $tenant)
				{
					if ($tenant->IsExpired()) continue;
					?>
					<tr>
						<td><a href="<?php echo(System::ExpandRelativePath("~/tenant/launch/" . $tenant->URL)); ?>" target="_blank"><?php echo($tenant->URL); ?></a></td>
						<td><?php echo($tenant->Type->Title); ?></td>
						<td><?php
							foreach ($tenant->DataCenters->Items as $item)
							{
								echo("<a href=\"http://" . $item->HostName . "/" . $tenant->URL . "\">" . $item->Title . " (" . $item->HostName . ")</a><br />");
							}
						?></td>
						<td><?php echo($tenant->PaymentPlan->Title); ?></td>
						<td><?php echo($tenant->BeginTimestamp == null ? "(indefinite)" : $tenant->BeginTimestamp); ?></td>
						<td><?php echo($tenant->EndTimestamp == null ? "(indefinite)" : $tenant->EndTimestamp); ?></td>
						<td><?php echo($tenant->Description); ?></td>
						<td style="text-align: center;">
							<a href="<?php echo(System::ExpandRelativePath("~/tenant/manage/" . $tenant->URL)); ?>">Manage</a> |
							<a href="<?php echo(System::ExpandRelativePath("~/tenant/modify/" . $tenant->URL)); ?>">Edit</a> |
							<a href="<?php echo(System::ExpandRelativePath("~/tenant/clone/" . $tenant->URL)); ?>">Clone</a> |
							<a href="<?php echo(System::ExpandRelativePath("~/tenant/delete/" . $tenant->URL)); ?>">Delete</a>
						</td>
					</tr>
					<?php
				}
			?>
		</table>
		<h2>Inactive Tenants (<?php echo($countExpired); ?>)</h2>
		<table class="ListView" style="width: 100%;" border="1">
			<tr>
				<th>Tenant Name</th>
				<th>Tenant Type</th>
				<th>Data Centers</th>
				<th>Payment Plan</th>
				<th>Activation Date</th>
				<th>Termination Date</th>
				<th>Description</th>
				<th>Actions</th>
			</tr>
			<?php
				foreach ($tenants as $tenant)
				{
					if (!$tenant->IsExpired()) continue;
					?>
					<tr>
						<td><a href="<?php echo(System::ExpandRelativePath("~/tenant/launch/" . $tenant->URL)); ?>" target="_blank"><?php echo($tenant->URL); ?></a></td>
						<td><?php echo($tenant->Type->Title); ?></td>
						<td><?php echo($tenant->DataCenter->Title); ?></td>
						<td><?php echo($tenant->PaymentPlan->Title); ?></td>
						<td><?php echo($tenant->BeginTimestamp == null ? "(indefinite)" : $tenant->BeginTimestamp); ?></td>
						<td><?php echo($tenant->EndTimestamp == null ? "(indefinite)" : $tenant->EndTimestamp); ?></td>
						<td><?php echo($tenant->Description); ?></td>
						<td style="text-align: center;">
							<a href="<?php echo(System::ExpandRelativePath("~/tenant/manage/" . $tenant->URL)); ?>">Manage</a> |
							<a href="<?php echo(System::ExpandRelativePath("~/tenant/modify/" . $tenant->URL)); ?>">Edit</a> |
							<a href="<?php echo(System::ExpandRelativePath("~/tenant/clone/" . $tenant->URL)); ?>">Clone</a> |
							<a href="<?php echo(System::ExpandRelativePath("~/tenant/delete/" . $tenant->URL)); ?>">Delete</a>
						</td>
					</tr>
					<?php
				}
			?>
		</table>
		<?php
		}
	}
?>