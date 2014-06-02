<?php
	namespace Objectify\Tenant\MasterPages;
	
	use WebFX\WebStyleSheet;
	use WebFX\System;
	
	class WebPage extends \WebFX\WebPage
	{
		public function __construct()
		{
			parent::__construct();
			
			$this->StyleSheets[] = new WebStyleSheet("http://static.alcehosting.net/dropins/WebFramework/StyleSheets/Workday/Main.css");
		}
		
		protected function BeforeContent()
		{
			?>
			<div style="text-align: right;"><a href="<?php echo(System::ExpandRelativePath("~/account/logout.page")); ?>">Log Out</a></div>
			<?php
		}
	}
?>