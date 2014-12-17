<?php
	use Objectify\Objects\TenantObjectProperty;
	use Objectify\Objects\TenantObjectInstanceProperty;
	use Objectify\Objects\TenantObjectMethodParameter;
	
	use Objectify\Objects\TenantEnumerationChoice;
	
	$enumUserProfileVisibility = $tenant->CreateEnumeration("UserProfileVisibility", "Specifies the possible values for the ProfileVisibility property on the User object.",
	array
	(
		new TenantEnumerationChoice("Everyone", "Your profile is visible to everyone inside and outside the site."),
		new TenantEnumerationChoice("Sitewide", "Your profile is visible only to other registered users."),
		new TenantEnumerationChoice("FriendsExtended", "Your profile is visible to your friends and friends of your friends."),
		new TenantEnumerationChoice("Friends", "Your profile is visible only to you and your friends."),
		new TenantEnumerationChoice("Private", "Your profile is visible only to you.")
	));
	
	$enumUserPresenceStatus = $tenant->CreateEnumeration("UserPresenceStatus", "Specifies the possible values for the ProfileVisibility property on the User object.",
	array
	(
		new TenantEnumerationChoice("Offline", "You are not online."),
		new TenantEnumerationChoice("Available", "You are available for other people to chat with."),
		new TenantEnumerationChoice("Away", "You are away from your computer at the moment."),
		new TenantEnumerationChoice("ExtendedAway", "You are going to be away for an extended period of time."),
		new TenantEnumerationChoice("Busy", "You are busy and do not want to be bothered."),
		new TenantEnumerationChoice("Hidden", "Your presence status is hidden.")
	));
	
	$object = $tenant->CreateObject("User", "Contains information about a Objectify user account.", array
	(
		// property name, data type, default value, value required?, enumeration, require choice from enumeration?
		new TenantObjectInstanceProperty("LoginID"),
		new TenantObjectInstanceProperty("URL"),
		new TenantObjectInstanceProperty("DisplayName"),
		new TenantObjectInstanceProperty("EmailAddress"),
		new TenantObjectInstanceProperty("EmailConfirmationCode"),
		new TenantObjectInstanceProperty("BirthDate"),
		new TenantObjectInstanceProperty("RealName"),
		new TenantObjectInstanceProperty("PasswordHash"),
		new TenantObjectInstanceProperty("PasswordSalt"),
		new TenantObjectInstanceProperty("Theme"),
		new TenantObjectInstanceProperty("Language"),
		new TenantObjectInstanceProperty("ProfileVisibility", null, null, true, $enumUserProfileVisibility, true),
		new TenantObjectInstanceProperty("ConsecutiveLoginCount"),
		new TenantObjectInstanceProperty("ConsecutiveLoginFailures"),
		new TenantObjectInstanceProperty("LastLoginTimestamp"),
		new TenantObjectInstanceProperty("PresenceStatus", null, null, true, $enumUserPresenceStatus, true),
		new TenantObjectInstanceProperty("PresenceMessage"),
		new TenantObjectInstanceProperty("RegistrationTimestamp"),
		new TenantObjectInstanceProperty("RegistrationIPAddress"),
		new TenantObjectInstanceProperty("StartPage")
	));
	
	$object->CreateMethod("SaltPassword", array(),
	
	// code goes here... you cannot "use" namespaces here; please put them in NamespaceReferences!!!
<<<'EOD'
return \UUID::Generate();
EOD
, "Generates a Objectify password salt using a Universally Unique Identifier (UUID).");
	
	$object->CreateMethod("HashPassword", array
	(
		new TenantObjectMethodParameter("input")
	),
	
	// code goes here... you cannot "use" namespaces here; please put them in NamespaceReferences!!!
<<<'EOD'
return hash("sha512", $input);
EOD
, "Generates a Objectify password hash using the SHA-512 algorithm.");
	
	$object->CreateMethod("ValidateCredentials", array
	(
		new TenantObjectMethodParameter("username"),
		new TenantObjectMethodParameter("password")
	),
	
	// code goes here... you cannot "use" namespaces here; please put them in NamespaceReferences!!!
<<<'EOD'
$tenant = Tenant::GetCurrent();
$inst = $thisObject->GetInstance(array
(
	new TenantQueryParameter("LoginID", $username)
));

// if there is no user with this LoginID, return null
if ($inst == null) return null;

// get the password salt used in the creation of this instance
$salt = $inst->GetPropertyValue($thisObject->GetInstanceProperty("PasswordSalt"));

// generate the salted password hash by concatenating the salt and the password
$pwhash = hash("sha512", $salt . $password);

// try to get an instance with the specified login ID and password hash
$user = $thisObject->GetInstance(array
(
	new TenantQueryParameter("LoginID", $username),
	new TenantQueryParameter("PasswordHash", $pwhash)
));

return $user;
EOD
, "Validates the given user name and password against the database and returns an instance of the User if the validation is successful.", array
(
	'Objectify\Objects\Tenant',
	'Objectify\Objects\TenantObjectMethodParameterValue',
	'Objectify\Objects\TenantQueryParameter'
));
	
	$object->CreateMethod("GetCurrentUser", array(),
	
	// code goes here... you cannot "use" namespaces here; please put them in NamespaceReferences!!!
<<<'EOD'
$tenant = Tenant::GetCurrent();
if (!((isset($_SESSION["CurrentUserName[" . $tenant->ID . "]"])) && (isset($_SESSION["CurrentPassword[" . $tenant->ID . "]"]))))
{
	return null;
}

$username = $_SESSION["CurrentUserName[" . $tenant->ID . "]"];
$password = $_SESSION["CurrentPassword[" . $tenant->ID . "]"];

$inst = $thisObject->GetInstance(array
(
	new TenantQueryParameter("LoginID", $username)
));

// if there is no user with this LoginID, return null
if ($inst == null) return null;

// get the password salt used in the creation of this instance
$salt = $inst->GetPropertyValue($thisObject->GetInstanceProperty("PasswordSalt"));

// generate the salted password hash by concatenating the salt and the password
$pwhash = hash("sha512", $salt . $password);

// try to get an instance with the specified login ID and password hash
$user = $thisObject->GetInstance(array
(
	new TenantQueryParameter("LoginID", $username),
	new TenantQueryParameter("PasswordHash", $pwhash)
));

return $user;
EOD
, "Gets the user that is currently logged into Objectify.", array
(
	// using statements go here
	'Objectify\Objects\Tenant',
	'Objectify\Objects\TenantQueryParameter'
));
	
?>