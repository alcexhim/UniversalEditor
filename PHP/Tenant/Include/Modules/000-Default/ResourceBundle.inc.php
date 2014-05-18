<?php
	namespace Objectify;
	
	class ResourceBundle
	{
		public $Name;
		public function __construct($name)
		{
			$this->Name = $name;
		}
		
		public function MakeRelativePath($filename)
		{
			global $RootPath;
			if (substr($filename, 0, strlen($RootPath)) == $RootPath)
			{
				return "~/" . substr($filename, strlen($RootPath) + 1);
			}
			return $filename;
		}
		
		// BEGIN: function from Sven Arduwie (http://us2.php.net/manual/en/function.realpath.php#84012)
		public function get_absolute_path($path)
		{
			$path = str_replace(array('/', '\\'), DIRECTORY_SEPARATOR, $path);
			$parts = explode(DIRECTORY_SEPARATOR, $path);
			$absolutes = array();
			foreach ($parts as $part)
			{
				if ('.' == $part) continue;
				if ('..' == $part)
				{
					array_pop($absolutes);
				}
				else
				{
					$absolutes[] = $part;
				}
			}
			return implode(DIRECTORY_SEPARATOR, $absolutes);
		}
		// END: function from Sven Arduwie (http://us2.php.net/manual/en/function.realpath.php#84012)
	
		public function MakeAbsolutePath($filename, $relativeTo = null)
		{
			global $RootPath;
			if (strlen($filename) > 3 && substr($filename, 0, 3) == "../")
			{
				if ($relativeTo != null)
				{
					$path = $relativeTo . "/" . $filename;
					return $this->get_absolute_path($path);
				}
				return $this->get_absolute_path($filename);
			}
			else if (strlen($filename) > 1 && substr($filename, 0, 1) == "/")
			{
				return $filename;
			}
			if ($relativeTo != null)
			{
				return $relativeTo . "/" . $filename;
			}
			return $RootPath . "/" . $filename;
		}
		
		public function CompileImportableFile($filename, $importedFileName = null, $preprocessorToken = "#")
		{
			global $RootPath;
			$filename = $this->MakeAbsolutePath($filename, dirname($importedFileName));
			$importedFileTitle = $this->MakeRelativePath($importedFileName);
			
			$lesstext = "";
			$filetitle = $this->MakeRelativePath($filename);
			$lesstext .= "/* BEGIN FILE: " . $filetitle;
			if ($importedFileTitle != null)
			{
				$lesstext .= " - IMPORTED FROM " . $importedFileTitle;
			}
			$lesstext .= " */\n";
			
			if (file_exists($filename))
			{
				$tmp = file_get_contents($filename);
				if ($tmp === false)
				{
					$lesstext .= "/* ERROR: " . $filename . " */";
				}
				else
				{
					$lesstext .= $tmp;
				}
			}
			else
			{
				$lesstext .= "/*\n\tERROR: file does not exist\n\tFile name: \"" . $filename . "\"";
				if ($importedFileTitle != null)
				{
					$lesstext .= "\n\tImported from: \"" . $importedFileTitle . "\"";
				}
				$lesstext .= "\n*/\n";
			}
			$lesstext .= "\n";
			
			$lesslines = explode("\n", $lesstext);
			$lesstext = "";
			
			$j = strlen($preprocessorToken . "import ");
			foreach ($lesslines as $lessline)
			{
				if (substr(trim($lessline), 0, $j) == $preprocessorToken . "import ")
				{
					$importfilename = trim(substr($lessline, $j));
					$importfilename = substr($importfilename, 1, strlen($importfilename) - 3); // removes " and ";
					
					$lesstext .= $this->CompileImportableFile($importfilename, $filename, $preprocessorToken);
				}
				else
				{
					$lesstext .= $lessline . "\n";
				}
			}
			
			$lesstext .= "/* END FILE: " . $filetitle;
			if ($importedFileTitle != null)
			{
				$lesstext .= " - IMPORTED FROM " . $importedFileTitle;
			}
			$lesstext .= " */\n\n";
			return $lesstext;
		}
		
		public function CompileStyleSheets()
		{
			global $RootPath;
			$StyleSheetPath = $RootPath . "/Resources/" . $this->Name . "/StyleSheets";
			$lesstext = "";
			
			$lesstext .= "/* BEGIN BUNDLE: " . $this->Name . " */\n";
			
			$lesstext .= "/* include path: " . $StyleSheetPath . " - ";
			
			$lessFiles = glob($StyleSheetPath . "/*.less");
			$lesstext .= count($lessFiles) . " *.less files, ";
			$cssFiles = glob($StyleSheetPath . "/*.css");
			$lesstext .= count($cssFiles) . " *.css files";
			$lesstext .= " */\n\n";
			
			foreach ($lessFiles as $filename)
			{
				$lesstext .= $this->CompileImportableFile($filename, null, "@");
			}
			foreach ($cssFiles as $filename)
			{
				$lesstext .= $this->CompileImportableFile($filename, null, "@");
			}
			$lesstext .= "/* END BUNDLE: " . $this->Name . " */\n\n";
			return $lesstext;
		}
		
		public function CompileScripts()
		{
			global $RootPath;
			$ContentPaths = array
			(
				"",
				"Controls",
				"Objects"
			);
			$BasePath = $RootPath . "/Resources/" . $this->Name . "/Scripts";
			$lesstext = "";
			
			$lesstext .= "/* BEGIN BUNDLE: " . $this->Name . " */\n";
			foreach ($ContentPaths as $ContentPath)
			{
				$truepath = $BasePath . "/" . $ContentPath;
				$lesstext .= "/* include path: " . $truepath . " - ";
				
				$jsfiles = glob($truepath . "/*.js");
				$lesstext .= count($jsfiles) . " *.js files";
				$lesstext .= " */\n\n";
				
				foreach ($jsfiles as $filename)
				{
					$lesstext .= $this->CompileImportableFile($filename, null, "@");
				}
			}
			$lesstext .= "/* END BUNDLE: " . $this->Name . " */\n\n";
			return $lesstext;
		}
	}
?>