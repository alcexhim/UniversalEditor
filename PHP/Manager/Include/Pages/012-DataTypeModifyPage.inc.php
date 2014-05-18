<?php
	namespace Objectify\Tenant\Pages;
	
	use WebFX\ModulePage;
	
	use WebFX\System;
	
	use WebFX\WebScript;
	use WebFX\WebStyleSheet;
	
	use WebFX\Controls\FormView;
	use WebFX\Controls\FormViewItem;
	use WebFX\Controls\FormViewItemText;
	use WebFX\Controls\FormViewItemMemo;
	
	use WebFX\Controls\TabContainer;
	use WebFX\Controls\TabPage;
	
	use Objectify\Tenant\MasterPages\WebPage;
	
	use Objectify\Objects\DataType;
	use Objectify\Objects\Module;
	use Objectify\Objects\PaymentPlan;
	use Objectify\Objects\Tenant;
	use Objectify\Objects\TenantObject;
	use Objectify\Objects\TenantProperty;
	use Objectify\Objects\TenantPropertyValue;
	use Objectify\Objects\TenantStatus;
	use Objectify\Objects\TenantType;
	
	class DataTypeModifyPage extends WebPage
	{
		public $CurrentDataType;
		
		public function __construct()
		{
			parent::__construct();
			
			$this->StyleSheets[] = new WebStyleSheet("http://static.alcehosting.net/dropins/CodeMirror/StyleSheets/CodeMirror.css");
			$this->Scripts[] = new WebScript("http://static.alcehosting.net/dropins/CodeMirror/Scripts/Addons/Edit/MatchBrackets.js");
			
			$this->Scripts[] = new WebScript("http://static.alcehosting.net/dropins/CodeMirror/Scripts/Modes/clike/clike.js");
			$this->Scripts[] = new WebScript("http://static.alcehosting.net/dropins/CodeMirror/Scripts/Modes/php/php.js");
		}
		
		protected function Initialize()
		{
			if ($this->CurrentDataType != null)
			{
				$this->Title = "Manage Data Type: " . $this->CurrentDataType->Name;
			}
			else
			{
				$this->Title = "Manage Data Types";
			}
		}
		
		protected function RenderContent()
		{
			if ($this->CurrentDataType != null)
			{
			?>
			<h1>Modify Data Type: <?php echo($this->CurrentDataType->Name); ?></h1>
			<?php
			}
			else
			{
				?><h1>Create Data Type</h1><?php
			}
			?>
			<style type="text/css">
			.CodeMirror
			{
				border: dotted 1px #AAAAAA;
				line-height: 18px;
			}
			.CodeMirror .CodeMirror-linenumbers
			{
				width: 48px;
			}
			</style>
			<script type="text/javascript">
			var CodeMirrorDefaultParameters = 
			{
				lineNumbers: true,
				matchBrackets: true,
				mode: "text/x-php",
				indentUnit: 4,
				indentWithTabs: true
			};
			
			function InitializeCodeMirror(id)
			{
				var txt = document.getElementById(id);
				if (txt.CodeMirrorInitialized) return;
				
				var edt = CodeMirror.fromTextArea(txt, CodeMirrorDefaultParameters);
				txt.CodeMirrorInitialized = true;
			}
			function tbs_OnClientTabChanged()
			{
				switch (tbs.SelectedTabID)
				{
					case "tabEncoder":
					{
						InitializeCodeMirror("txtEncoderCodeBlob");
						break;
					}
					case "tabDecoder":
					{
						InitializeCodeMirror("txtDecoderCodeBlob");
						break;
					}
					case "tabColumnRenderer":
					{
						InitializeCodeMirror("txtColumnRendererCodeBlob");
						break;
					}
					case "tabEditorRenderer":
					{
						InitializeCodeMirror("txtEditorRendererCodeBlob");
						break;
					}
				}
			}
			</script>
			<form method="POST">
				<?php
					if ($this->CurrentDataType != null)
					{
						echo("<input type=\"hidden\" name=\"datatype_ID\" value=\"" . $this->CurrentDataType->ID . "\" />");
					}
				
					$fv = new FormView("fv");
					$fv->Items[] = new FormViewItemText("txtDataTypeName", "datatype_Name", "Name", $this->CurrentDataType->Name);
					$fv->Items[0]->Required = true;
					
					$fv->Items[] = new FormViewItemMemo("txtDataTypeDescription", "datatype_Description", "Description", $this->CurrentDataType->Description);
					$fv->Render();
				?>
				<?php
					$tbs = new TabContainer("tbs");
					$tbs->OnClientTabChanged = "tbs_OnClientTabChanged();";
					$tbs->TabPages[] = new TabPage("tabEncoder", "Encoder", null, null, null, function()
					{
						?>
						<textarea id="txtEncoderCodeBlob" name="datatype_EncoderCodeBlob" style="width: 100%;" rows="20"><?php echo($this->CurrentDataType->EncoderCodeBlob); ?></textarea>
						<?php
					});
					$tbs->TabPages[] = new TabPage("tabDecoder", "Decoder", null, null, null, function()
					{
						?>
						<textarea id="txtDecoderCodeBlob" name="datatype_DecoderCodeBlob" style="width: 100%;" rows="20"><?php echo($this->CurrentDataType->DecoderCodeBlob); ?></textarea>
						<?php
					});
					$tbs->TabPages[] = new TabPage("tabColumnRenderer", "Column Renderer", null, null, null, function()
					{
						?>
						<textarea id="txtColumnRendererCodeBlob" name="datatype_ColumnRendererCodeBlob" style="width: 100%;" rows="20"><?php echo($this->CurrentDataType->ColumnRendererCodeBlob); ?></textarea>
						<?php
					});
					$tbs->TabPages[] = new TabPage("tabEditorRenderer", "Editor Renderer", null, null, null, function()
					{
						?>
						<textarea id="txtEditorRendererCodeBlob" name="datatype_EditorRendererCodeBlob" style="width: 100%;" rows="20"><?php echo($this->CurrentDataType->EditorRendererCodeBlob); ?></textarea>
						<?php
					});
					$tbs->Render();
				?>
				<div class="Buttons">
					<input type="submit" value="Save Changes" />
					<a class="Button" href="<?php echo(System::ExpandRelativePath("~/datatype")); ?>">Cancel</a>
				</div>
			</form>
			<?php
		}
	}
	
	class DataTypeBrowsePage extends WebPage
	{
		protected function RenderContent()
		{
			$items = DataType::Get();
			?>
			<table class="ListView">
				<tr>
					<th>Name</th>
					<th>Description</th>
				</tr>
				<?php
				foreach ($items as $item)
				{
					?>
					<tr>
						<td><a href="<?php echo(System::ExpandRelativePath("~/datatype/modify/" . $item->ID)); ?>"><?php echo($item->Name); ?></a></td>
						<td><?php echo($item->Description); ?></td>
					</tr>
					<?php
				}
				?>
			</table>
			<div class="Buttons">
				<a class="Button Emphasis" href="<?php echo(System::ExpandRelativePath("~/datatype/modify")); ?>">Create New Data Type</a>
				<a class="Button" href="<?php echo(System::ExpandRelativePath("~/")); ?>">Exit</a>
			</div>
			<?php
		}
	}
	
	System::$Modules[] = new \WebFX\Module("net.Objectify.TenantManager.DataType", array
	(
		new ModulePage("datatype", array
		(
			new ModulePage("", function($path)
			{
				$page = new DataTypeBrowsePage();
				$page->Render();
				return true;
			}),
			new ModulePage("modify", function($path)
			{
				if ($_SERVER["REQUEST_METHOD"] == "POST")
				{
					if (isset($_POST["datatype_ID"]))
					{
						$datatype = DataType::GetByID($_POST["datatype_ID"]);
					}
					else
					{
						$datatype = new DataType();
					}
					$datatype->Name = $_POST["datatype_Name"];
					$datatype->Description = $_POST["datatype_Description"];
					$datatype->EncoderCodeBlob = $_POST["datatype_EncoderCodeBlob"];
					$datatype->DecoderCodeBlob = $_POST["datatype_DecoderCodeBlob"];
					$datatype->ColumnRendererCodeBlob = $_POST["datatype_ColumnRendererCodeBlob"];
					$datatype->EditorRendererCodeBlob = $_POST["datatype_EditorRendererCodeBlob"];
					$datatype->Update();
					
					System::Redirect("~/datatype");
				}
				else
				{
					$page = new DataTypeModifyPage();
					$page->CurrentDataType = DataType::GetByID($path[0]);
					$page->Render();
				}
				return true;
			})
		))
	));
?>