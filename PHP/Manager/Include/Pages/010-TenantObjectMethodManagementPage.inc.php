<?php
	namespace Objectify\Tenant\Pages;
	
	use WebFX\System;
	
	use WebFX\WebScript;
	use WebFX\WebStyleSheet;
	
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
	
	class TenantObjectMethodManagementPage extends WebPage
	{
		public $CurrentMethod;
		public $CurrentTenant;
		public $CurrentObject;
		
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
			if ($this->CurrentMethod != null)
			{
				$this->Title = "Manage Method: " . $this->CurrentMethod->Name . " on " . $this->CurrentObject->Name . "@" . $this->CurrentTenant->URL;
			}
			else
			{
				$this->Title = "Manage Methods for Object: " . $this->CurrentObject->Name . " on " . $this->CurrentTenant->URL;
			}
		}
		
		protected function RenderContent()
		{
			if ($this->CurrentObject != null)
			{
			?>
			<h1>Method: <?php echo($this->CurrentMethod->Name); ?> on <?php echo($this->CurrentObject->Name); ?>@<?php echo($this->CurrentTenant->URL); ?></h1>
			<table class="FormView">
				<tr class="Required">
					<td><label for="txtMethodName">Method Name</label></td>
					<td><input type="text" id="txtMethodName" name="method_Name" value="<?php echo($this->CurrentMethod->Name); ?>" /></td>
				</tr>
			</table>
			<h2>Code Blob</h2>
			<form method="POST">
			<textarea id="txtCodeBlob" name="method_CodeBlob" style="width: 100%;" rows="40"><?php echo($this->CurrentMethod->CodeBlob); ?></textarea>
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
			var txtCodeBlob = document.getElementById("txtCodeBlob");
			var editor = CodeMirror.fromTextArea(txtCodeBlob,
			{
				lineNumbers: true,
				matchBrackets: true,
				mode: "text/x-php",
				indentUnit: 4,
				indentWithTabs: true
			});
			</script>
			<div class="Buttons">
				<input type="submit" value="Save Changes" />
				<a class="Button" href="<?php echo(System::ExpandRelativePath("~/tenant/manage/" . $this->CurrentTenant->URL . "/objects/" . $this->CurrentObject->ID)); ?>">Discard Changes</a>
			</div>
			</form>
			<?php
			}
		}
	}
?>