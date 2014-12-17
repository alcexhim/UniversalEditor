<?php
	use Objectify\Objects\DataType;
	
	use Objectify\Objects\MultipleInstanceProperty;
	use Objectify\Objects\SingleInstanceProperty;
	
	use Objectify\Objects\TenantObjectProperty;
	use Objectify\Objects\TenantObjectInstanceProperty;
	use Objectify\Objects\TenantObjectInstancePropertyValue;
	use Objectify\Objects\TenantObjectMethodParameter;
	
	$objUserAgent = $tenant->CreateObject("UserAgent", "A User Agent that can determine what browser or client the guest is using to access the Web site.", array
	(
		new TenantObjectInstanceProperty("Name"),
		new TenantObjectInstanceProperty("Value")
	));
	$objUserAgentBehavior = $tenant->CreateObject("UserAgentBehavior", "The behavior to use when determining whether a list of user agents is allowed (whitelist) or denied (blacklist).", array
	(
		new TenantObjectInstanceProperty("Name")
	));
	
	$propUserAgents = new TenantObjectInstanceProperty("UserAgents", DataType::GetByName("MultipleInstance"), new MultipleInstanceProperty(array(), array($objUserAgent)));
	$propUserAgents->Description = "If one or more UserAgents are specified in this property, depending on the UserAgentBehavior this PageSection will display ONLY on these UserAgents.";
	
	$propUserAgentBehavior = new TenantObjectInstanceProperty("UserAgentBehavior", DataType::GetByName("SingleInstance"), new SingleInstanceProperty(array(), array($objUserAgentBehavior)));
	
	$objPageSection = $tenant->CreateObject("PageSection", "Represents a section of a Page on the tenant. Sections can be assigned to one or more Pages, and can be duplicated within the same Page.", array
	(
		new TenantObjectInstanceProperty("Name"),
		new TenantObjectInstanceProperty("Content"),
		$propUserAgents,
		$propUserAgentBehavior
	));
	
	$object = $tenant->CreateObject("Page", "Represents an individual Page on the tenant. Pages have URLs (such as ~/dashboard or ~/account/login) and host one or more PageSections.", array
	(
		// property name, data type, default value, value required?, enumeration, require choice from enumeration?
		new TenantObjectInstanceProperty("Name"),
		new TenantObjectInstanceProperty("URL"),
		new TenantObjectInstanceProperty("Sections", DataType::GetByName("MultipleInstance"), new MultipleInstanceProperty(array(), array($objPageSection)))
	));
	
	$instSectGuestMainPageDashboardPanel = $objPageSection->CreateInstance(array
	(
		new TenantObjectInstancePropertyValue("Name", "GuestMainPageDashboardPanel"),
		new TenantObjectInstancePropertyValue("Content", <<<'EOD'
<?php
	use Objectify\Objects\Tenant;
	
	// get the dashboard posts
	$tenant = Tenant::GetCurrent();
	$objDashboardPost = $tenant->GetObject("GuestMainPageDashboardPost");
	$posts = $objDashboardPost->GetInstances();
	
	if (count($posts) == 0)
	{
?>
<div class="Card">
	<div class="Title">There's nothing here!</div>
	<div class="Content">
		If you are the site administrator, please make sure you have configured your Objectify installation correctly. Log
		in to your Administrator Control Panel to set up your installation and add content.
	</div>
</div>
<?php
	}
	else
	{
		foreach ($posts as $post)
		{
			print_r($post);
			echo("\n\n");
		}
?>
<?php
	}
?>
EOD
)
	));
	
	$instSectGuestMainPageRegisterPanel = $objPageSection->CreateInstance(array
	(
		new TenantObjectInstancePropertyValue("Name", "GuestMainPageRegisterPanel"),
		new TenantObjectInstancePropertyValue("Content", <<<'EOD'
<?php
	use WebFX\System;
?>
<div class="Card">
	<form id="frmRegisterDefault" action="<?php echo(System::ExpandRelativePath("~/account/register.page")); ?>" method="POST" style="width: 100%;">
		<div class="Title">Don't have an account yet?</div>
		<div class="Content">
			<h1>Sign up for free right now!</h1>
			<div class="Form">
				<?php  
					// Fields required by Objectify (login ID, password, display name, and URL name) come first. After the user
					// provides the initial required information, you may follow up with custom Objectify sign-up pages.
				?>
				<div class="FormItemGroup" style="display: table-row">
				<?php
					if (System::GetConfigurationValue("Security.Credentials.UserLoginIDIsEmailAddress") == "true")
					{
				?>
					<div class="FormItem" style="display: table-cell">
						<div class="FormItemTitle"><label for="txtEmailAddress">E-mail address:</label></div>
						<div class="FormItemEntry"><input type="text" id="txtEmailAddress" placeholder="phenix@example.com" /></div>
					</div>
				<?php
					}
					else
					{
				?>
					<div class="FormItem" style="display: table-cell">
						<div class="FormItemTitle"><label for="txtUserName">Private login ID:</label></div>
						<div class="FormItemEntry"><input type="text" id="txtUserName" name="un" placeholder="ph3n1xxx" /></div>
					</div>
				<?php
					}
				?>
				</div>
				<div class="FormItemGroup" style="display: table-row">
					<div class="FormItem" style="display: table-cell">
						<div class="FormItemTitle"><label for="txtPassword">Private password:</label></div>
						<div class="FormItemEntry"><input type="password" id="txtPassword" name="pw" placeholder="S0me*Sup3r/SecRET_passWord!" /></div>
					</div>
					<div class="FormItem" style="display: table-cell">
						<div class="FormItemTitle"><label for="txtPasswordConfirm">Confirm password:</label></div>
						<div class="FormItemEntry"><input type="password" id="txtPasswordConfirm" name="pwc" placeholder="" /></div>
					</div>
				</div>
				<div class="FormItemGroup" style="display: table-row">
					<div class="FormItem" style="display: table-cell">
						<div class="FormItemTitle"><label for="txtDisplayName">Display name:</label></div>
						<div class="FormItemEntry"><input type="text" id="txtDisplayName" name="ln" placeholder="Phenix the Great" /></div>
					</div>
				</div>
				<div class="FormItemGroup" style="display: table-row">
					<div class="FormItem" style="display: table-cell">
						<div class="FormItemTitle"><label for="txtShortURLName">Short URL name:</label></div>
						<div class="FormItemEntry"><input type="text" id="txtShortURLName" name="sn" placeholder="phenix" /></div>
					</div>
				</div>
			</div>
		</div>
		<div class="Actions Horizontal">
			<a href="#" onclick="document.getElementById('frmRegisterDefault').submit(); return false;">Register my account</a>
		</div>
	</form>
</div>
EOD
)
	));
	
	$instSectGuestMainPageLoginPanel = $objPageSection->CreateInstance(array
	(
		new TenantObjectInstancePropertyValue("Name", "GuestMainPageLoginPanel"),
		new TenantObjectInstancePropertyValue("Content", <<<'EOD'
<?php
	use WebFX\System;
?>
<form id="frmLoginDefault" action="<?php echo(System::ExpandRelativePath("~/account/login.page")); ?>" method="POST" style="width: 100%;">
	<div class="Card">
		<div class="Title">Already have an account?</div>
		<div class="Content">
			<div class="FormItemGroup" style="display: table-row">
			<?php
				if (System::GetConfigurationValue("Security.Credentials.UserLoginIDIsEmailAddress") == "true")
				{
			?>
				<div class="FormItem" style="display: table-cell">
					<div class="FormItemEntry"><input type="text" name="member_email" id="txtLoginEmailAddress" placeholder="E-mail address" /></div>
				</div>
			<?php
				}
				else
				{
			?>
				<div class="FormItem" style="display: table-cell">
					<div class="FormItemEntry"><input type="text" name="member_username" id="txtLoginUserName" placeholder="Private login ID" /></div>
				</div>
			<?php
				}
			?>
			</div>
			<div class="FormItemGroup" style="display: table-row">
				<div class="FormItem" style="display: table-cell">
					<div class="FormItemEntry"><input type="password" name="member_password" id="txtLoginPassword" placeholder="Password" /></div>
				</div>
			</div>
		</div>
		<div class="Actions Horizontal">
			<a href="#" onclick="document.getElementById('frmLoginDefault').submit(); return false;">Log In</a>
		</div>
	</div>
</form>
EOD
)
	));
	
	$instGuestMainPage = $object->CreateInstance(array
	(
		new TenantObjectInstancePropertyValue("Name", "GuestMainPage"),
		new TenantObjectInstancePropertyValue("Sections", new MultipleInstanceProperty
		(
			array
			(
				$instSectGuestMainPageDashboardPanel,
				$instSectGuestMainPageLoginPanel,
				$instSectGuestMainPageRegisterPanel
			),
			array($objPageSection)
		))
	));
	
	$object->CreateInstanceMethod("GetSections", array(),
	
	// code goes here... you cannot "use" namespaces here; please put them in NamespaceReferences!!!
<<<'EOD'
	$thisTenant = Tenant::GetCurrent();
	$objPageSection = $thisTenant->GetObject("PageSection");
	$insts = $objPageSection->GetInstances(array
	(
		new TenantQueryParameter("Page", $thisInstance)
	));
EOD
, "Gets all of the PageSections associated with the current Page.", array(
	"Tenant",
	"TenantQueryParameter"
));
	
?>