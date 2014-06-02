<?php
	namespace Objectify\Tenant\Pages;
	
	use WebFX\System;
	
	use Objectify\Tenant\MasterPages\WebPage;
	use Objectify\Objects\Module;
	
	class ModuleManagementPage extends WebPage
	{
		public $Module;
		
		protected function Initialize()
		{
			$this->Title = "Manage Module: " . $this->Module->Title;
		}
		
		protected function RenderContent()
		{
		?>
		<h1>Module Management</h1>
		<h2><?php echo($this->Module->Title); ?></h2>
		
		<form method="POST">
			<h3>Information</h3>
			<table style="width: 100%;" border="1">
				<tr>
					<td style="width: 100px;">Title: </td>
					<td><input name="module_Title" type="text" value="<?php echo($this->Module->Title); ?>" /></td>
				</tr>
				<tr>
					<td style="vertical-align: top;">Description: </td>
					<td><textarea name="module_Description" style="width: 100%;" rows="5"><?php echo($this->Module->Description); ?></textarea></td>
				</tr>
			</table>
		
			<h3>Application Menu Items</h3>
			<table>
				<tr>
					<th>Title</th>
					<th>Description</th>
					<th>Target</th>
				</tr>
				<?php
				$menuitems = $this->Module->GetMainMenuItems();
				foreach ($menuitems as $menuitem)
				{
					?>
					<tr>
						<td><?php echo($menuitem->Title); ?></td>
						<td><?php echo($menuitem->Description); ?></td>
						<td><?php echo($menuitem->TargetURL); ?></td>
					</tr>
					<?php
				}
				?>
			</table>
			
			<h3>Module Pages</h3>
			<table>
				<tr>
					<th>URL</th>
				</tr>
				<?php
				/*
					foreach ($modules as $module)
					{
						?>
						<tr>
							<td><a href="<?php echo(System::ExpandRelativePath("~/module/modify/" . $module->ID)); ?>"><?php echo($module->Title); ?></a></td>
						</tr>
						<?php
					}
				*/
				?>
			</table>
			<div class="Buttons" style="text-align: right;">
				<input type="submit" value="Save Changes" /> <a href="<?php echo(System::ExpandRelativePath("~/module")); ?>">Back to Modules</a>
			</div>
		</form>
		<?php
		}
	}
?>