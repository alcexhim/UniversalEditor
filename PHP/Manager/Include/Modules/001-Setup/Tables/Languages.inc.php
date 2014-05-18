<?php
	use DataFX\DataFX;
	use DataFX\Table;
	use DataFX\Column;
	use DataFX\ColumnValue;
	use DataFX\Record;
	use DataFX\RecordColumn;
	
	$tables[] = new Table("Languages", "language_", array
	(
		// 			$name, $dataType, $size, $value, $allowNull, $primaryKey, $autoIncrement
		new Column("ID", "INT", null, null, false, true, true),
		new Column("TenantID", "INT", null, null, false),
		new Column("Name", "VARCHAR", 5, null, false),
		new Column("Title", "VARCHAR", 50, null, false)
	),
	array
	(
		new Record(array
		(
			new RecordColumn("Name", "en-US"),
			new RecordColumn("Title", "English (United States)")
		))
	));
	
	$tables[] = new Table("LanguageStrings", "languagestring_", array
	(
		// 			$name, $dataType, $size, $value, $allowNull, $primaryKey, $autoIncrement
		new Column("TenantID", "INT", null, null, false),
		new Column("LanguageID", "INT", null, null, false),
		new Column("StringName", "VARCHAR", 50, null, false),
		new Column("StringValue", "LONGTEXT", null, null, false)
	),
	array
	(
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "like"),
			new RecordColumn("StringValue", "Like")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "comment"),
			new RecordColumn("StringValue", "Comment")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "download"),
			new RecordColumn("StringValue", "Download")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "share"),
			new RecordColumn("StringValue", "Share")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "groups"),
			new RecordColumn("StringValue", "Groups")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "friends"),
			new RecordColumn("StringValue", "Friends")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "nogroups"),
			new RecordColumn("StringValue", "This user is not a member of any groups.")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "nofriends"),
			new RecordColumn("StringValue", "This user does not have any friends. <a href=\"#\" onclick=\"AddFriendDialog.ShowDialog(); return false;\">Introduce yourself!</a>")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "invitegroup"),
			new RecordColumn("StringValue", "Invite this user to join a group")
		)),
		
		// === account/settings
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "personal"),
			new RecordColumn("StringValue", "Personal Information")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "realname_label"),
			new RecordColumn("StringValue", "What's your real name?")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "realname_example"),
			new RecordColumn("StringValue", "Johnny Test")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "birthdate_label"),
			new RecordColumn("StringValue", "When were you born?")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "birthdate_example"),
			new RecordColumn("StringValue", "1994-03-25")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "email_label"),
			new RecordColumn("StringValue", "What's your e-mail address?")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "email_example"),
			new RecordColumn("StringValue", "somebody@phoenixsns.net")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "appearance"),
			new RecordColumn("StringValue", "Appearance and Personalization")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "longname_label"),
			new RecordColumn("StringValue", "What do you want to be called?")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "longname_example"),
			new RecordColumn("StringValue", "Phenix the Great")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "startpage_label"),
			new RecordColumn("StringValue", "When I log in, take me to:")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "shortname_label"),
			new RecordColumn("StringValue", "Your site URL name:")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "shortname_example"),
			new RecordColumn("StringValue", "phenix")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "language_label"),
			new RecordColumn("StringValue", "Default language:")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "security"),
			new RecordColumn("StringValue", "Security and Authentication")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "profile_visibility_label"),
			new RecordColumn("StringValue", "Who can see my profile?")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "deactivate_account"),
			new RecordColumn("StringValue", "Deactivate Account")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "use_with_caution"),
			new RecordColumn("StringValue", "Please exercise great care when considering this option. Once done, it cannot be un-done.")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "deactivate_account_warning"),
			new RecordColumn("StringValue", "Would you like to deactivate your account and lose all your items, resources, conversation history, friends, group memberships, and other site features? If so, click the link.")
		)),
		new Record(array
		(
			new RecordColumn("LanguageID", 1),
			new RecordColumn("StringName", "deactivate_account_button"),
			new RecordColumn("StringValue", "Yes, please deactivate my account now.")
		))
	));
?>