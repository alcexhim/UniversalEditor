<?php
	use DataFX\DataFX;
	use DataFX\Table;
	use DataFX\Column;
	use DataFX\ColumnValue;
	use DataFX\Record;
	use DataFX\RecordColumn;
	
	$tblDataTypes = new Table("DataTypes", "datatype_", array
	(
		// 			$name, $dataType, $size, $value, $allowNull, $primaryKey, $autoIncrement
		new Column("ID", "INT", null, null, false, true, true),
		new Column("Name", "VARCHAR", 256, null, true),
		new Column("Description", "LONGTEXT", null, null, true),
		new Column("EncoderCodeBlob", "LONGBLOB", null, ColumnValue::Undefined, true),
		new Column("DecoderCodeBlob", "LONGBLOB", null, ColumnValue::Undefined, true),
		new Column("ColumnRendererCodeBlob", "LONGBLOB", null, ColumnValue::Undefined, true),
		new Column("EditorRendererCodeBlob", "LONGBLOB", null, ColumnValue::Undefined, true)
	),
	array
	(
		new Record(array
		(
			new RecordColumn("Name", "Text"),
			new RecordColumn("Description", "Allows you to enter a single line of text."),
			new RecordColumn("EncoderCodeBlob", ColumnValue::Undefined),
			new RecordColumn("DecoderCodeBlob", ColumnValue::Undefined),
			new RecordColumn("ColumnRendererCodeBlob", ColumnValue::Undefined),
			new RecordColumn("EditorRendererCodeBlob", ColumnValue::Undefined)
		)),
		new Record(array
		(
			new RecordColumn("Name", "Memo"),
			new RecordColumn("Description", "Allows you to enter large amounts of text."),
			new RecordColumn("EncoderCodeBlob", ColumnValue::Undefined),
			new RecordColumn("DecoderCodeBlob", ColumnValue::Undefined),
			new RecordColumn("ColumnRendererCodeBlob", <<<'EOD'
			echo("<div style=\"width: 100%; overflow: scroll; overflow-x: hidden;\">" . $input . "</div>");
EOD
),
			new RecordColumn("EditorRendererCodeBlob",  <<<'EOD'
			echo("<textarea id=\"" . $name . "\" name=\"" . $name . "\">" . $input . "</textarea>");
EOD
)
		)),
		new Record(array
		(
			new RecordColumn("Name", "CodeBlob"),
			new RecordColumn("Description", "Allows you to enter script code with a special text editor."),
			new RecordColumn("EncoderCodeBlob", null),
			new RecordColumn("DecoderCodeBlob", null),
			new RecordColumn("ColumnRendererCodeBlob", <<<'EOD'
			echo("<div style=\"width: 100%; overflow: scroll; overflow-x: hidden;\">" . $input . "</div>");
EOD
),
			new RecordColumn("EditorRendererCodeBlob",  <<<'EOD'
			echo("<textarea id=\"" . $name . "\" name=\"" . $name . "\">" . $input . "</textarea>");
EOD
)
		)),
		new Record(array
		(
			new RecordColumn("Name", "Number"),
			new RecordColumn("Description", "Stores numeric data."),
			new RecordColumn("EncoderCodeBlob", null),
			new RecordColumn("DecoderCodeBlob", null),
			new RecordColumn("ColumnRendererCodeBlob", null),
			new RecordColumn("EditorRendererCodeBlob", null)
		)),
		new Record(array
		(
			new RecordColumn("Name", "Boolean"),
			new RecordColumn("Description", "Stores a true/false or yes/no value."),
			new RecordColumn("EncoderCodeBlob", null),
			new RecordColumn("DecoderCodeBlob", null),
			new RecordColumn("ColumnRendererCodeBlob", <<<'EOD'
echo("<input type=\"checkbox\" disabled=\"disabled\" />");
EOD
),
			new RecordColumn("EditorRendererCodeBlob", <<<'EOD'
echo("<input type=\"checkbox\" id=\"" . $name . "\" name=\"" . $name . "\" />");
EOD
)
		)),
		new Record(array
		(
			new RecordColumn("Name", "Measurement"),
			new RecordColumn("Description", "Stores measurement data, which is a double-precision floating-point number followed by a unit of measurement."),
			new RecordColumn("EncoderCodeBlob", null),
			new RecordColumn("DecoderCodeBlob", null),
			new RecordColumn("ColumnRendererCodeBlob", null),
			new RecordColumn("EditorRendererCodeBlob", <<<'EOD'
echo("<input type=\"text\" id=\"Measurement_" . $name . "_Value\" /> <select id=\"Measurement_" . $name . "_Unit\">");
echo("<option value=\"px\" selected=\"selected\">px</option>");
echo("<option value=\"pt\" selected=\"selected\">pt</option>");
echo("<option value=\"em\" selected=\"selected\">em</option>");
echo("<option value=\"%\" selected=\"selected\">%</option>");
echo("</select>");
EOD
)
		)),
		new Record(array
		(
			new RecordColumn("Name", "DateTime"),
			new RecordColumn("Description", "Stores a date and time value."),
			new RecordColumn("EncoderCodeBlob", null),
			new RecordColumn("DecoderCodeBlob", null),
			new RecordColumn("ColumnRendererCodeBlob", null),
			new RecordColumn("EditorRendererCodeBlob", null)
		)),
		new Record(array
		(
			new RecordColumn("Name", "SingleInstance"),
			new RecordColumn("Description", "Represents a property that returns a single TenantObjectInstance object."),
			new RecordColumn("EncoderCodeBlob", <<<'EOD'
// $input should be a TenantObjectInstance
if ($input == null)
{
	$bt = debug_backtrace();
	trigger_error("SingleInstance::Encoder - input is null, did you mean to pass in a blank SingleInstanceProperty?, in " . $bt["file"] . "::" . $bt["function"] . " on line " . $bt["line"] . "; ", E_USER_WARNING);
	return "";
}
// encode the property by simply storing the instance ID in the property value.
$output = "";
$count = count($input->ValidObjects);
for ($i = 0; $i < $count; $i++)
{
	$output .= $input->ValidObjects[$i]->ID;
	if ($i < $count - 1) $output .= ",";
}
$output .= ":";
if ($input != null)
{
	if ($input->GetInstance() != null) $output .= $input->GetInstance()->ID;
}
return $output;
EOD
),
			new RecordColumn("DecoderCodeBlob", <<<'EOD'
// $input should be a String in the format t0,t1,t2:i0,i1,i2,i3... where tx is an ID of a TenantObject that is valid in the property and ix is an ID of a TenantObjectInstance
// encode the property by simply storing the instance ID of each instance, separated by commas, in the property value.
$dcb = explode(":", $input);
$validObjects = explode(",", $dcb[0]);
$instance = $dcb[1];
$output = new SingleInstanceProperty();

// loop through all the valid objects and add them to the MultipleInstanceProperty
$count = count($validObjects);
for ($i = 0; $i < $count; $i++)
{
	$output->ValidObjects[] = TenantObject::GetByID($validObjects[$i]);
}

// assign the instance
$output->Instance = TenantObjectInstance::GetByID($instance);
return $output;
EOD
),
			new RecordColumn("ColumnRendererCodeBlob", <<<'EOD'
$inst = $input->Instance;
echo("<div class=\"InstanceEditor SingleInstance ReadOnly\">");
	echo("<input type=\"hidden\" id=\"" . $name . "_Instances\" name=\"" . $name . "_Instances\" />");
	echo("<div class=\"InstanceList\">");
if ($inst != null)
{
	echo ("<a href=\"#\">" . $inst->ToString() . "</a>");
}
	echo("</div>");
echo("</div>");
EOD
),
			new RecordColumn("EditorRendererCodeBlob", <<<'EOD'
$inst = $input->Instance;
echo("<div class=\"InstanceEditor SingleInstance\">");
$insts_text = $inst->ID;
	echo("<input type=\"hidden\" id=\"" . $name . "_Instances\" name=\"" . $name . "_Instances\" value=\"" . $insts_text . "\" />");
	echo("<input type=\"text\" id=\"" . $name . "_CurrentInstance\" name=\"" . $name . "_CurrentInstance\" />");
	echo("<div class=\"InstanceList\">");
if ($inst != null)
{
	echo ("<a href=\"#\">" . $inst->ToString() . "</a>");
}
	echo("</div>");
	echo("<script type=\"text/javascript\">");
	echo("var t = document.getElementById('" . $name . "_CurrentInstance');");
	echo("t.onkeydown = function(e) { ");
	echo("alert(e.keyCode);");
	echo("};");
	echo("</script>");
echo("</div>");
EOD
)
		)),
		new Record(array
		(
			new RecordColumn("Name", "MultipleInstance"),
			new RecordColumn("Description", "Represents a property that returns an array of TenantObjectInstance objects."),
			new RecordColumn("EncoderCodeBlob", <<<'EOD'
// $input should be an array of TenantObjectInstance objects
// encode the property by simply storing the instance ID of each instance, separated by commas, in the property value. the list of valid
// object types is stored in the first part of the property, separated by a colon.
if ($input == null)
{
	PhoenixSNS::Log("MultipleInstance::Encoder input is null - did you mean to pass in a blank MultipleInstanceProperty?");
	return "";
}

$output = "";
$count = count($input->ValidObjects);
for ($i = 0; $i < $count; $i++)
{
	$output .= $input->ValidObjects[$i]->ID;
	if ($i < $count - 1) $output .= ",";
}
$output .= ":";
$insts = $input->GetInstances();
$i = 0;
$count = count($insts);
foreach ($insts as $inst)
{
	$output .= $inst->ID;
	if ($i < $count - 1) $output .= ",";
	$i++;
}
return $output;
EOD
),
			new RecordColumn("DecoderCodeBlob", <<<'EOD'
// $input should be a String in the format t0,t1,t2:i0,i1,i2,i3... where tx is an ID of a TenantObject that is valid in the property and ix is an ID of a TenantObjectInstance
// encode the property by simply storing the instance ID of each instance, separated by commas, in the property value.
if ($input == "")
{
	$bt = debug_backtrace();
	trigger_error("MultipleInstance::Decoder - input is null, did you mean to pass in a blank MultipleInstanceProperty?, in " . $bt["file"] . "::" . $bt["function"] . " on line " . $bt["line"] . "; ", E_USER_WARNING);
	return null;
}

$dcb = explode(":", $input);
$validObjects = explode(",", $dcb[0]);
$instances = explode(",", $dcb[1]);
$output = new MultipleInstanceProperty();

// loop through all the valid objects and add them to the MultipleInstanceProperty
$count = count($validObjects);
for ($i = 0; $i < $count; $i++)
{
	$output->ValidObjects[] = TenantObject::GetByID($validObjects[$i]);
}

// loop through all of the instances and add them to the MultipleInstanceProperty
$count = count($instances);
for ($i = 0; $i < $count; $i++)
{
	$output->AddInstance(TenantObjectInstance::GetByID($instances[$i]));
}
return $output;
EOD
),
			new RecordColumn("ColumnRendererCodeBlob", <<<'EOD'
if ($input == null) return;
if (!is_object($input) || (get_class($input) != "PhoenixSNS\\Objects\\MultipleInstanceProperty"))
{
	$bt = debug_backtrace();
	trigger_error("Expected MultipleInstanceProperty, got something else in " . $bt[1]["file"] . "::" . $bt[1]["function"] . " at line " . $bt[1]["line"], E_USER_WARNING);
	return;
}
$insts = $input->GetInstances();
echo("<div class=\"InstanceEditor MultipleInstance ReadOnly\">");
	echo("<input type=\"hidden\" id=\"" . $name . "_Instances\" name=\"" . $name . "_Instances\" />");
	echo("<input type=\"text\" id=\"" . $name . "_CurrentInstance\" name=\"" . $name . "_CurrentInstance\" />");
	echo("<div class=\"InstanceList\">");
foreach ($insts as $inst)
{
	if ($inst != null)
	{
		echo ("<a href=\"#\">" . $inst->ToString() . "</a>");
	}
}
	echo("</div>");
echo("</div>");
EOD
),
			new RecordColumn("EditorRendererCodeBlob",  <<<'EOD'
if ($input == null) return;
if (!is_object($input) || (get_class($input) != "PhoenixSNS\\Objects\\MultipleInstanceProperty"))
{
	$bt = debug_backtrace();
	trigger_error("Expected MultipleInstanceProperty, got something else in " . $bt[1]["file"] . "::" . $bt[1]["function"] . " at line " . $bt[1]["line"], E_USER_WARNING);
	return;
}
$insts = $input->GetInstances();
echo("<div class=\"InstanceEditor MultipleInstance\">");
$insts_text = "";
$count = count($insts);
for ($i = 0; $i < $count; $i++)
{
	$inst = $insts[$i];
	$insts_text .= $inst->ID;
	if ($i < $count - 1) $insts_text .= ", ";
}
	echo("<input type=\"hidden\" id=\"" . $name . "_Instances\" name=\"" . $name . "_Instances\" value=\"" . $insts_text . "\" />");
	echo("<input type=\"text\" id=\"" . $name . "_CurrentInstance\" name=\"" . $name . "_CurrentInstance\" />");
	echo("<div class=\"InstanceList\">");
foreach ($insts as $inst)
{
	if ($inst != null)
	{
		echo ("<a href=\"#\">" . $inst->ToString() . "</a>");
	}
}
	echo("</div>");
	echo("<script type=\"text/javascript\">");
	echo("var t = document.getElementById('" . $name . "_CurrentInstance');");
	echo("t.onkeydown = function(e) { ");
	echo("alert(e.keyCode);");
	echo("};");
	echo("</script>");
echo("</div>");
EOD
)
		))
	));
	$tables[] = $tblDataTypes;
?>