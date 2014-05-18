<?php
	namespace Objectify\Tenant\Pages;
	
	use WebFX\System;
	
	use Objectify\Tenant\MasterPages\WebPage;
	use Objectify\Objects\Tenant;
	
	class LoginPage extends WebPage
	{
		public $InvalidCredentials;
		
		protected function RenderContent()
		{
		?>
		<h1>Authentication Required</h1>
		<p>You must log in to view this page.</p>
		<form method="POST">
			<table style="margin-left: auto; margin-right: auto;" class="Borderless">
				<tr>
					<td><label for="txtUserName">User <u>N</u>ame</label></td>
					<td><input type="text" name="user_LoginID" id="txtUserName" /></td>
				</tr>
				<tr>
					<td><label for="txtPassword"><u>P</u>assword</label></td>
					<td><input type="password" name="user_Password" id="txtPassword" /></td>
				</tr>
				<tr>
					<td colspan="2" style="color: #FF0000; text-align: center;">
					<?php
						if ($this->InvalidCredentials)
						{
							?>Incorrect user name/password combination.<?php
						}
					?>
					</td>
				</tr>
				<tr>
					<td colspan="2" style="text-align: right;">
						<input type="submit" value="Continue" />
					</td>
				</tr>
			</table>
		</form>
		<?php
		}
	}
?>