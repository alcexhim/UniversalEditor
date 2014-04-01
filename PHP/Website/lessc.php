<?php
require("lessc.inc.php");

header("Content-Type: text/css");

$filename = "StyleSheets/" . $_GET["filename"];

if ((isset($_GET["compile"]) && $_GET["compile"] == "false") || file_exists($filename . ".css"))
{
	readfile($filename . ".css");
}
else
{
	try
	{
		$less = new lessc();
		$less->formatterName = "compressed";
		$v = $less->compileFile($filename . ".less");
		
		echo("/* compiled with lessphp v0.4.0 - GPLv3/MIT - http://leafo.net/lessphp */\n");
		echo("/* for human-readable source of this file, replace .css with .less in the file name */\n");
		echo($v);
	}
	catch (Exception $e)
	{
		echo "/* " . $e->getMessage() . " */\n";
	}
}
?>