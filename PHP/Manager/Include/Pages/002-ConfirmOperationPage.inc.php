<?php
	namespace Objectify\Tenant\Pages;
	
	use WebFX\System;
	
	use Objectify\Tenant\MasterPages\WebPage;
	
	// use Objectify\Objects\DataCenter;
	use Objectify\Objects\PaymentPlan;
	use Objectify\Objects\Tenant;
	use Objectify\Objects\TenantStatus;
	use Objectify\Objects\TenantType;
	
	class ConfirmOperationPage extends WebPage
	{
		public $Message;
		
		public $ReturnButtonURL;
		
		public function __construct()
		{
			parent::__construct();
			$this->ReturnButtonURL = "~/";
		}
		
		protected function RenderContent()
		{
			?>
			<h1>Confirm Operation</h1>
			<p><?php echo($this->Message); ?></p>
			<form method="POST">
				<input type="hidden" name="Confirm" value="1" />
				<input type="submit" value="Continue" />
				<a class="Button" href="<?php echo(System::ExpandRelativePath($this->ReturnButtonURL)); ?>">Cancel</a>
			</form>
			<?php
		}
	}
?>