<?php
	namespace Objectify\Tenant\Pages;
	
	use WebFX\System;
	
	use Objectify\Tenant\MasterPages\WebPage;
	use Objectify\Objects\Module;
	
	class ModuleMainPage extends WebPage
	{
		protected function RenderContent()
		{
			$modules = Module::Get();
		?>
		<h1>Module Management</h1>
		<p>There are <?php echo(count($modules)); ?> modules in total.  Click a module name to configure that module.</p>
		
		<div class="Toolbar">
			<a class="Button" href="<?php echo(System::ExpandRelativePath("~/module/create")); ?>">Create New Module</a>
		</div>
		<table style="width: 100%;" border="1">
			<tr>
				<th>Module</th>
				<th>Description</th>
			</tr>
			<?php
				foreach ($modules as $module)
				{
					?>
					<tr>
						<td><a href="<?php echo(System::ExpandRelativePath("~/module/modify/" . $module->ID)); ?>"><?php echo($module->Title); ?></a></td>
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