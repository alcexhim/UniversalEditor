;--------------------------------
;Include Modern UI

	!include "MUI2.nsh"

;--------------------------------
;General

	!define PRODUCT_COMPANY "Mike Becker's Software"
	!define PRODUCT_TITLE "Universal Editor"
	!define PRODUCT_VERSION "4.0.0.1"
	!define PRODUCT_GUID "{29CCB653-B590-440A-8928-464496599331}"
	
	!define PRODUCT_URL "http://www.alceproject.net/projects/editor"
	!define PRODUCT_URL_UPDATES "http://www.alceproject.net/projects/editor"
	!define PRODUCT_URL_SUPPORT "http://www.alceproject.net/projects/editor"

	!define BUILD_CONFIGURATION "Debug"

	;Name and file
	Name "Universal Editor"
	OutFile "setup.exe"

	;Default installation folder
	InstallDir "$PROGRAMFILES\${PRODUCT_COMPANY}\${PRODUCT_TITLE}"

	;Get installation folder from registry if available
	InstallDirRegKey HKCU "Software\${PRODUCT_COMPANY}\${PRODUCT_TITLE}" ""

	;Request application privileges for Windows Vista
	RequestExecutionLevel admin

;--------------------------------
;Variables

	Var StartMenuFolder

;--------------------------------
;Interface Settings

	!define MUI_HEADERIMAGE

	!define MUI_HEADERIMAGE_BITMAP "header.bmp"
	!define MUI_HEADERIMAGE_UNBITMAP "header-uninstall.bmp"

	!define MUI_WELCOMEFINISHPAGE_BITMAP "sidebar.bmp"
	!define MUI_UNWELCOMEFINISHPAGE_BITMAP "sidebar-uninstall.bmp"

	!define MUI_ABORTWARNING

	!define MUI_ICON "setup.ico"
	!define MUI_UNICON "uninstall.ico"
	
	!define MUI_FINISHPAGE_RUN "$INSTDIR\UniversalEditor.exe"

;--------------------------------
;Language Selection Dialog Settings

	;Remember the installer language
	!define MUI_LANGDLL_REGISTRY_ROOT "HKCU" 
	!define MUI_LANGDLL_REGISTRY_KEY "Software\${PRODUCT_COMPANY}\${PRODUCT_TITLE}" 
	!define MUI_LANGDLL_REGISTRY_VALUENAME "Installer Language"

;--------------------------------
;Pages

	!insertmacro MUI_PAGE_WELCOME
	!insertmacro MUI_PAGE_LICENSE "license.txt"
	!insertmacro MUI_PAGE_COMPONENTS
	!insertmacro MUI_PAGE_DIRECTORY

	;Start Menu Folder Page Configuration
	!define MUI_STARTMENUPAGE_REGISTRY_ROOT "HKCU" 
	!define MUI_STARTMENUPAGE_REGISTRY_KEY "Software\${PRODUCT_COMPANY}\${PRODUCT_TITLE}" 
	!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "Start Menu Folder"

	!insertmacro MUI_PAGE_STARTMENU Application $StartMenuFolder

	!insertmacro MUI_PAGE_INSTFILES
	!insertmacro MUI_PAGE_FINISH

	!insertmacro MUI_UNPAGE_WELCOME
	!insertmacro MUI_UNPAGE_CONFIRM
	!insertmacro MUI_UNPAGE_INSTFILES
	!insertmacro MUI_UNPAGE_FINISH

;--------------------------------
;Languages

	!insertmacro MUI_LANGUAGE "English" ;first language is the default language
	; !insertmacro MUI_LANGUAGE "French"
	; !insertmacro MUI_LANGUAGE "German"
	; !insertmacro MUI_LANGUAGE "Spanish"
	; !insertmacro MUI_LANGUAGE "SpanishInternational"
	; !insertmacro MUI_LANGUAGE "SimpChinese"
	; !insertmacro MUI_LANGUAGE "TradChinese"
	; !insertmacro MUI_LANGUAGE "Japanese"
	; !insertmacro MUI_LANGUAGE "Korean"
	; !insertmacro MUI_LANGUAGE "Italian"
	; !insertmacro MUI_LANGUAGE "Dutch"
	; !insertmacro MUI_LANGUAGE "Danish"
	; !insertmacro MUI_LANGUAGE "Swedish"
	; !insertmacro MUI_LANGUAGE "Norwegian"
	; !insertmacro MUI_LANGUAGE "NorwegianNynorsk"
	; !insertmacro MUI_LANGUAGE "Finnish"
	; !insertmacro MUI_LANGUAGE "Greek"
	; !insertmacro MUI_LANGUAGE "Russian"
	; !insertmacro MUI_LANGUAGE "Portuguese"
	; !insertmacro MUI_LANGUAGE "PortugueseBR"
	; !insertmacro MUI_LANGUAGE "Polish"
	; !insertmacro MUI_LANGUAGE "Ukrainian"
	; !insertmacro MUI_LANGUAGE "Czech"
	; !insertmacro MUI_LANGUAGE "Slovak"
	; !insertmacro MUI_LANGUAGE "Croatian"
	; !insertmacro MUI_LANGUAGE "Bulgarian"
	; !insertmacro MUI_LANGUAGE "Hungarian"
	; !insertmacro MUI_LANGUAGE "Thai"
	; !insertmacro MUI_LANGUAGE "Romanian"
	; !insertmacro MUI_LANGUAGE "Latvian"
	; !insertmacro MUI_LANGUAGE "Macedonian"
	; !insertmacro MUI_LANGUAGE "Estonian"
	; !insertmacro MUI_LANGUAGE "Turkish"
	; !insertmacro MUI_LANGUAGE "Lithuanian"
	; !insertmacro MUI_LANGUAGE "Slovenian"
	; !insertmacro MUI_LANGUAGE "Serbian"
	; !insertmacro MUI_LANGUAGE "SerbianLatin"
	; !insertmacro MUI_LANGUAGE "Arabic"
	; !insertmacro MUI_LANGUAGE "Farsi"
	; !insertmacro MUI_LANGUAGE "Hebrew"
	; !insertmacro MUI_LANGUAGE "Indonesian"
	; !insertmacro MUI_LANGUAGE "Mongolian"
	; !insertmacro MUI_LANGUAGE "Luxembourgish"
	; !insertmacro MUI_LANGUAGE "Albanian"
	; !insertmacro MUI_LANGUAGE "Breton"
	; !insertmacro MUI_LANGUAGE "Belarusian"
	; !insertmacro MUI_LANGUAGE "Icelandic"
	; !insertmacro MUI_LANGUAGE "Malay"
	; !insertmacro MUI_LANGUAGE "Bosnian"
	; !insertmacro MUI_LANGUAGE "Kurdish"
	; !insertmacro MUI_LANGUAGE "Irish"
	; !insertmacro MUI_LANGUAGE "Uzbek"
	; !insertmacro MUI_LANGUAGE "Galician"
	; !insertmacro MUI_LANGUAGE "Afrikaans"
	; !insertmacro MUI_LANGUAGE "Catalan"
	; !insertmacro MUI_LANGUAGE "Georgian"
	; !insertmacro MUI_LANGUAGE "Khmer"
	; !insertmacro MUI_LANGUAGE "Pashto"
	; !insertmacro MUI_LANGUAGE "Vietnamese"
	; !insertmacro MUI_LANGUAGE "Esperanto"
	
;--------------------------------
;Reserve Files
  
	;If you are using solid compression, files that are required before
	;the actual installation should be stored first in the data block,
	;because this will make your installer start faster.

	!insertmacro MUI_RESERVEFILE_LANGDLL

;--------------------------------
;Installer Sections

InstType "Minimal"
InstType "Complete"

InstType "Database Administrator"
InstType "Application Developer"
InstType "Website Developer"
InstType "Game Developer"

Section "Application" SecApplication
	SectionIn RO

	;ADD YOUR OWN FILES HERE...
	SetOutPath "$INSTDIR"
	File "..\Output\${BUILD_CONFIGURATION}\AwesomeControls.dll"
	File "..\Output\${BUILD_CONFIGURATION}\glue.dll"
	File "..\Output\${BUILD_CONFIGURATION}\UniversalEditor.Compression.dll"
	File "..\Output\${BUILD_CONFIGURATION}\UniversalEditor.Core.dll"
	File "..\Output\${BUILD_CONFIGURATION}\UniversalEditor.Engines.WindowsForms.dll"
	File "..\Output\${BUILD_CONFIGURATION}\UniversalEditor.Essential.dll"
	File "..\Output\${BUILD_CONFIGURATION}\UniversalEditor.exe"
	File "..\Output\${BUILD_CONFIGURATION}\UniversalEditor.UserInterface.dll"
	File "..\Output\${BUILD_CONFIGURATION}\UniversalEditor.UserInterface.WindowsForms.dll"
	File "..\Output\${BUILD_CONFIGURATION}\UniversalEditorConsole.exe"

	SetOutPath "$INSTDIR\Configuration"
	File "..\Output\${BUILD_CONFIGURATION}\Configuration\Application.upl"
	File "..\Output\${BUILD_CONFIGURATION}\Configuration\Application.xml"
	File "..\Output\${BUILD_CONFIGURATION}\Configuration\CommandBars.xml"
	File "..\Output\${BUILD_CONFIGURATION}\Configuration\Commands.xml"
	File "..\Output\${BUILD_CONFIGURATION}\Configuration\MainMenu.xml"
	File "..\Output\${BUILD_CONFIGURATION}\Configuration\ObjectModels.xml"
	File "..\Output\${BUILD_CONFIGURATION}\Configuration\SplashScreen.upl"
	File "..\Output\${BUILD_CONFIGURATION}\Configuration\StartPage.xml"

	;Store installation folder
	WriteRegStr HKCU "Software\${PRODUCT_COMPANY}\${PRODUCT_TITLE}" "" $INSTDIR

	;Create uninstaller
	WriteUninstaller "$INSTDIR\uninstall.exe"

	;Write installation data to the Windows Registry
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_GUID}" "DisplayName" "${PRODUCT_TITLE}"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_GUID}" "UninstallString" "$\"$INSTDIR\uninstall.exe$\""
	
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_GUID}" "InstallLocation" "$INSTDIR"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_GUID}" "DisplayIcon" "$INSTDIR\UniversalEditor.exe,0"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_GUID}" "Publisher" "${PRODUCT_COMPANY}"
	
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_GUID}" "DisplayVersion" "${PRODUCT_VERSION}"
	
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_GUID}" "HelpLink" "${PRODUCT_URL_SUPPORT}"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_GUID}" "URLUpdateInfo" "${PRODUCT_URL_UPDATES}"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_GUID}" "URLInfoAbout" "${PRODUCT_URL}"
  
	!insertmacro MUI_STARTMENU_WRITE_BEGIN Application

		;Create shortcuts
		CreateDirectory "$SMPROGRAMS\$StartMenuFolder"
		CreateShortCut "$SMPROGRAMS\$StartMenuFolder\Uninstall.lnk" "$INSTDIR\Uninstall.exe"

	!insertmacro MUI_STARTMENU_WRITE_END

SectionEnd

SectionGroup "Languages" SecLanguages
	Section "English" SecLanguageEnglish
		SectionIn 1 2 3 4 5 6
		
		SetOutPath "$INSTDIR\Languages"
		File "..\Output\${BUILD_CONFIGURATION}\Languages\English.xml"
	SectionEnd
	Section "Japanese" SecLanguageJapanese
		SectionIn 2
		
		SetOutPath "$INSTDIR\Languages"
		File "..\Output\${BUILD_CONFIGURATION}\Languages\Japanese.xml"
	SectionEnd
SectionGroupEnd
SectionGroup "Plugins" SecPlugins
	Section "Software Development" SecPluginSoftwareDevelopment
		SectionIn 2
		SectionIn 4
		
		SetOutPath "$INSTDIR\Plugins"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.Designer.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.Executable.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.Executable.UserInterface.WindowsForms.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.SoftwareDevelopment.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.SoftwareDevelopment.UserInterface.WindowsForms.dll"
	SectionEnd
	Section "File system/archive" SecPluginFileSystem
		SectionIn 2
		
		SetOutPath "$INSTDIR\Plugins"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.FileSystem.dll"
	SectionEnd
	Section "Game Development" SecPluginGameDevelopment
		SectionIn 2
		SectionIn 6
		
		SetOutPath "$INSTDIR\Plugins"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.Avalanche.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.ChaosWorks.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.Cyberlore.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.Descent.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.FileSystem.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.Gainax.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.Icarus.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.Illusion.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.Microsoft.Merlin.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.Multimedia.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.Multimedia.UserInterface.WindowsForms.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.Multimedia3D.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.Multimedia3D.UserInterface.WindowsForms.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.NamcoTales.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.NewWorldComputing.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.NewWorldComputing.UserInterface.WindowsForms.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.Nintendo.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.ReflexiveEntertainment.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.UnrealEngine.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.UnrealEngine.UserInterface.WindowsForms.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.Valve.dll"
	SectionEnd
	Section "Web Development" SecPluginWebDevelopment
		SectionIn 2
		SectionIn 5
		
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.Web.dll"
		File "..\Output\${BUILD_CONFIGURATION}\Plugins\UniversalEditor.Plugins.Web.UserInterface.WindowsForms.dll"
	SectionEnd
SectionGroupEnd
SectionGroup "Project Types" SecProjectTypes
	SectionGroup "Web Development" SecProjectTypeWebDevelopment
		Section "PHP Web Project" SecProjectTypeWebDevelopmentPHP
			SectionIn 2
			SectionIn 5
			
			SetOutPath "$INSTDIR\ProjectTypes"
			File "..\Output\${BUILD_CONFIGURATION}\ProjectTypes\{A0786B88-2ADB-4C21-ABE8-AA2D79766269}.xml"
		SectionEnd
		Section "XML Transformation Project" SecProjectTypeXMLTransformation
			SectionIn 2
			SectionIn 5
			
			SetOutPath "$INSTDIR\ProjectTypes"
			File "..\Output\${BUILD_CONFIGURATION}\ProjectTypes\{AB211699-2C6A-4FCC-97FB-F629B1023277}.xml"
		SectionEnd
	SectionGroupEnd
SectionGroupEnd
SectionGroup "Document Templates" SecDocumentTemplates
	SectionGroup "Database" SecDocumentTemplatesDatabase
		Section "Assets Database" SecDocumentTemplatesDatabaseAssets
			SectionIn 2
			SectionIn 3
			
			SetOutPath "$INSTDIR\Templates\Document\Database"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\database.ico"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\AssetsDatabase.xml"
		SectionEnd
		Section "Contacts Database" SecDocumentTemplatesDatabaseContacts
			SectionIn 2
			SectionIn 3
			
			SetOutPath "$INSTDIR\Templates\Document\Database"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\database.ico"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\ContactsDatabase.xml"
		SectionEnd
		Section "Events Database" SecDocumentTemplatesDatabaseEvents
			SectionIn 2
			SectionIn 3
			
			SetOutPath "$INSTDIR\Templates\Document\Database"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\database.ico"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\EventsDatabase.xml"
		SectionEnd
		Section "Faculty Database" SecDocumentTemplatesDatabaseFaculty
			SectionIn 2
			SectionIn 3
			
			SetOutPath "$INSTDIR\Templates\Document\Database"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\database.ico"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\FacultyDatabase.xml"
		SectionEnd
		Section "Issues Database" SecDocumentTemplatesDatabaseIssues
			SectionIn 2
			SectionIn 3
			
			SetOutPath "$INSTDIR\Templates\Document\Database"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\database.ico"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\IssuesDatabase.xml"
		SectionEnd
		Section "Marketing Projects Database" SecDocumentTemplatesDatabaseMarketingProjects
			SectionIn 2
			SectionIn 3
			
			SetOutPath "$INSTDIR\Templates\Document\Database"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\database.ico"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\MarketingProjectsDatabase.xml"
		SectionEnd
		Section "Northwind Database" SecDocumentTemplatesDatabaseNorthwind
			SectionIn 2
			SectionIn 3
			
			SetOutPath "$INSTDIR\Templates\Document\Database"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\database.ico"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\NorthwindDatabase.xml"
		SectionEnd
		Section "Projects Database" SecDocumentTemplatesDatabaseProjects
			SectionIn 2
			SectionIn 3
			
			SetOutPath "$INSTDIR\Templates\Document\Database"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\database.ico"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\ProjectsDatabase.xml"
		SectionEnd
		Section "Sales Pipeline Database" SecDocumentTemplatesDatabaseSalesPipeline
			SectionIn 2
			SectionIn 3
			
			SetOutPath "$INSTDIR\Templates\Document\Database"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\database.ico"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\SalesPipelineDatabase.xml"
		SectionEnd
		Section "Students Database" SecDocumentTemplatesDatabaseStudents
			SectionIn 2
			SectionIn 3
			
			SetOutPath "$INSTDIR\Templates\Document\Database"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\database.ico"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\StudentsDatabase.xml"
		SectionEnd
		Section "Tasks Database" SecDocumentTemplatesDatabaseTasks
			SectionIn 2
			SectionIn 3
			
			SetOutPath "$INSTDIR\Templates\Document\Database"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\database.ico"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Database\TasksDatabase.xml"
		SectionEnd
	SectionGroupEnd
	SectionGroup "File System/Archive" SecDocumentTemplatesFileSystem
		Section "Windows File System" SecDocumentTemplatesFileSystemWindows
			SectionIn 2
			
			SetOutPath "$INSTDIR\Templates\Document\FileSystem"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\FileSystem\windows_16x16.png"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\FileSystem\windows_32x32.png"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\FileSystem\WindowsFileSystem.xml"
		SectionEnd
	SectionGroupEnd
	SectionGroup "Form" SecDocumentTemplatesForm
		Section "Asset Tracking" SecDocumentTemplatesFormAssetTracking
			SectionIn 2
			
			SetOutPath "$INSTDIR\Templates\Document\Form"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Form\icon_16x16.png"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Form\icon_32x32.png"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Form\icon_48x48.png"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Form\Asset Tracking.xml"
		SectionEnd
		Section "Expense Report" SecDocumentTemplatesFormExpenseReport
			SectionIn 2
			
			SetOutPath "$INSTDIR\Templates\Document\Form"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Form\icon_16x16.png"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Form\icon_32x32.png"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Form\icon_48x48.png"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Form\Expense Report.xml"
		SectionEnd
		Section "Meeting Agenda" SecDocumentTemplatesFormMeetingAgenda
			SectionIn 2
			
			SetOutPath "$INSTDIR\Templates\Document\Form"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Form\icon_16x16.png"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Form\icon_32x32.png"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Form\icon_48x48.png"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Form\Meeting Agenda.xml"
		SectionEnd
		Section "Status Report" SecDocumentTemplatesFormStatusReport
			SectionIn 2
			
			SetOutPath "$INSTDIR\Templates\Document\Form"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Form\icon_16x16.png"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Form\icon_32x32.png"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Form\icon_48x48.png"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Form\Status Report.xml"
		SectionEnd
		Section "Travel Request" SecDocumentTemplatesFormTravelRequest
			SectionIn 2
			
			SetOutPath "$INSTDIR\Templates\Document\Form"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Form\icon_16x16.png"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Form\icon_32x32.png"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Form\icon_48x48.png"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Form\Travel Request.xml"
		SectionEnd
	SectionGroupEnd
	SectionGroup "Website Development" SecDocumentTemplatesWeb
		Section "HTML Page" SecDocumentTemplatesWebHTMLPage
			SectionIn 2 5
			
			SetOutPath "$INSTDIR\Templates\Document\Web"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Web\HTMLPage.ico"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Web\HTMLPage.xml"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Web\MasterPage.ico"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Web\MasterPage.xml"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Web\WebForm.ico"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Web\WebForm.xml"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Web\WebService.ico"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Web\WebService.xml"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Web\WebUserControl.ico"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Document\Web\WebUserControl.xml"
		SectionEnd
	SectionGroupEnd
SectionGroupEnd
SectionGroup "Project Templates" SecProjectTemplates
	SectionGroup "File Distribution" SecProjectTemplatesFileDistribution
		Section "Empty File System" SecProjectTemplatesFileDistributionEmptyFileSystem
			SectionIn 2
			
			SetOutPath "$INSTDIR\Templates\Project\File Distribution"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\File Distribution\EmptyFileSystem.xml"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\File Distribution\FileSystem_16x16.png"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\File Distribution\FileSystem_32x32.png"
		SectionEnd
		Section "Empty Torrent" SecProjectTemplatesFileDistributionEmptyTorrent
			SectionIn 2
			
			SetOutPath "$INSTDIR\Templates\Project\File Distribution"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\File Distribution\EmptyTorrent.xml"
			SetOutPath "$INSTDIR\Templates\Project\File Distribution\Images"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\File Distribution\Images\Torrent_16x16.png"
			File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\File Distribution\Images\Torrent_32x32.png"
		SectionEnd
	SectionGroupEnd
	SectionGroup "Software Development" SecProjectTemplatesSoftwareDevelopment
		SectionGroup "Adobe AIR" SecProjectTemplatesSoftwareDevelopmentAdobeAIR
			Section "Basic Project" SecProjectTemplatesSoftwareDevelopmentAdobeAIRBasicProject
				SectionIn 2 4
				
				SetOutPath "$INSTDIR\Templates\Project\Software Development\Adobe\AIR"
				File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Adobe\AIR\Basic Project.xml"
				SetOutPath "$INSTDIR\Templates\Project\Software Development\Adobe\AIR\Images"
				File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Adobe\AIR\Images\Application_16x16.png"
				File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Adobe\AIR\Images\Application_32x32.png"
			SectionEnd
		SectionGroupEnd
		SectionGroup "Arduino" SecProjectTemplatesSoftwareDevelopmentArduino
			Section "Blank Project" SecProjectTemplatesSoftwareDevelopmentArduinoBlankProject
				SectionIn 2 4
				
				SetOutPath "$INSTDIR\Templates\Project\Software Development\Arduino"
				File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Arduino\BlankProject.xml"
				SetOutPath "$INSTDIR\Templates\Project\Software Development\Arduino\Images"
				File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Arduino\Images\Blank_16x16.png"
				File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Arduino\Images\Blank_32x32.png"
				File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Arduino\Images\Blank.ico"
			SectionEnd
			Section "Blink" SecProjectTemplatesSoftwareDevelopmentArduinoBlink
				SectionIn 2 4
				
				SetOutPath "$INSTDIR\Templates\Project\Software Development\Arduino"
				File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Arduino\Blink.xml"
				SetOutPath "$INSTDIR\Templates\Project\Software Development\Arduino\Images"
				File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Arduino\Images\Blink_16x16.png"
				File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Arduino\Images\Blink_32x32.png"
				File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Arduino\Images\Blink.ico"
			SectionEnd
		SectionGroupEnd
		SectionGroup "C" SecProjectTemplatesSoftwareDevelopmentC
			Section "GTK+ Application" SecProjectTemplatesSoftwareDevelopmentCGTKPlusApplication
				SectionIn 2 4
				
				SetOutPath "$INSTDIR\Templates\Project\Software Development\C"
				File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\C\GTK+ Application.xml"
			SectionEnd
		SectionGroupEnd
		SectionGroup "Common Language Runtime" SecProjectTemplatesSoftwareDevelopmentCLR
			SectionGroup "COBOL" SecProjectTemplatesSoftwareDevelopmentCLRCobol
				Section "Class Library" SecProjectTemplatesSoftwareDevelopmentCLRCobolClassLibrary
					SectionIn 2 4
					
					SetOutPath "$INSTDIR\Templates\Project\Software Development\Common Language Runtime\COBOL"
					File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Common Language Runtime\COBOL\Class Library.xml"
					File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Common Language Runtime\COBOL\Images\Library_16x16.png"
					File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Common Language Runtime\COBOL\Images\Library_32x32.png"
				SectionEnd
				Section "Console Application" SecProjectTemplatesSoftwareDevelopmentCLRCobolConsoleApplication
					SectionIn 2 4
					
					SetOutPath "$INSTDIR\Templates\Project\Software Development\Common Language Runtime\COBOL"
					File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Common Language Runtime\COBOL\Console Application.xml"
					File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Common Language Runtime\COBOL\Images\Console_16x16.png"
					File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Common Language Runtime\COBOL\Images\Console_32x32.png"
				SectionEnd
				Section "Database" SecProjectTemplatesSoftwareDevelopmentCLRCobolDatabase
					SectionIn 2 4
					
					SetOutPath "$INSTDIR\Templates\Project\Software Development\Common Language Runtime\COBOL"
					File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Common Language Runtime\COBOL\Database.xml"
					File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Common Language Runtime\COBOL\Images\Database_16x16.png"
					File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Common Language Runtime\COBOL\Images\Database_32x32.png"
				SectionEnd
				Section "Silverlight" SecProjectTemplatesSoftwareDevelopmentCLRCobolSilverlight
					SectionIn 2 4
					
					SetOutPath "$INSTDIR\Templates\Project\Software Development\Common Language Runtime\COBOL"
					File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Common Language Runtime\COBOL\Silverlight.xml"
					File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Common Language Runtime\COBOL\Images\Silverlight_16x16.png"
					File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Common Language Runtime\COBOL\Images\Silverlight_32x32.png"
				SectionEnd
				Section "Windows Forms Application" SecProjectTemplatesSoftwareDevelopmentCLRCobolWindowsFormsApplication
					SectionIn 2 4
					
					SetOutPath "$INSTDIR\Templates\Project\Software Development\Common Language Runtime\COBOL"
					File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Common Language Runtime\COBOL\Silverlight.xml"
					File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Common Language Runtime\COBOL\Images\Application_16x16.png"
					File "..\Output\${BUILD_CONFIGURATION}\Templates\Project\Software Development\Common Language Runtime\COBOL\Images\Application_32x32.png"
				SectionEnd
			SectionGroupEnd
		SectionGroupEnd
	SectionGroupEnd
SectionGroupEnd

;--------------------------------
;Installer Functions

Function .onInit

	!insertmacro MUI_LANGDLL_DISPLAY

FunctionEnd

;--------------------------------
;Descriptions

	;USE A LANGUAGE STRING IF YOU WANT YOUR DESCRIPTIONS TO BE LANGAUGE SPECIFIC

	;Language strings
	LangString DESC_SecApplication ${LANG_ENGLISH} "Application files required to run Universal Editor."

	LangString DESC_SecLanguages ${LANG_ENGLISH} "Language translation files to provide out-of-the-box support for various languages."
	LangString DESC_SecLanguageEnglish ${LANG_ENGLISH} "Installs the necessary language translation files for English (United States)."
	LangString DESC_SecLanguageJapanese ${LANG_ENGLISH} "Installs the necessary language translation files for Japanese."

	LangString DESC_SecDocumentTemplates ${LANG_ENGLISH} "Useful templates that offer a head start in producing your documents."
	LangString DESC_SecDocumentTemplatesDatabase ${LANG_ENGLISH} "Templates that provide pre-fabricated databases."
	LangString DESC_SecDocumentTemplatesDatabaseAssets ${LANG_ENGLISH} "Keep track of assets, including asset details and owners."
	LangString DESC_SecDocumentTemplatesDatabaseContacts ${LANG_ENGLISH} "Manage information about people that your team works with, such as customers and partners."
	LangString DESC_SecDocumentTemplatesDatabaseEvents ${LANG_ENGLISH} "Track upcoming meetings, deadlines, and other important events."
	LangString DESC_SecDocumentTemplatesDatabaseFaculty ${LANG_ENGLISH} "Keep track of information about faculty members, including emergency contacts and information about their education history."
	LangString DESC_SecDocumentTemplatesDatabaseIssues ${LANG_ENGLISH} "Manage a set of issues or problems. Assign, prioritize, and follow the progress of issues from start to finish."
	LangString DESC_SecDocumentTemplatesDatabaseMarketingProjects ${LANG_ENGLISH} "Track time-sensitive deliverables and vendor status for projects."
	LangString DESC_SecDocumentTemplatesDatabaseNorthwind ${LANG_ENGLISH} "Northwind Sample"
	LangString DESC_SecDocumentTemplatesDatabaseProjects ${LANG_ENGLISH} "Track multiple projects and assign tasks to different people."
	LangString DESC_SecDocumentTemplatesDatabaseSalesPipeline ${LANG_ENGLISH} "Track the progress of prospective sales within a small group of sales professionals."
	LangString DESC_SecDocumentTemplatesDatabaseStudents ${LANG_ENGLISH} "Keep track of information about your students, including emergency contacts, medical information, and information about their guardians."
	LangString DESC_SecDocumentTemplatesDatabaseTasks ${LANG_ENGLISH} "Track a group of work items that you or your team need to complete."

	LangString DESC_SecPlugins ${LANG_ENGLISH} "Plugins to provide out-of-the-box support for object models, data formats, and other functionality to customize Universal Editor."
	LangString DESC_SecPluginFileSystem ${LANG_ENGLISH} "Installs the plugin to support reading and writing popular and obscure file system/archive data formats."

	;Assign language strings to sections
	!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
		!insertmacro MUI_DESCRIPTION_TEXT ${SecApplication} $(DESC_SecApplication)

		!insertmacro MUI_DESCRIPTION_TEXT ${SecLanguages} $(DESC_SecLanguages)
		!insertmacro MUI_DESCRIPTION_TEXT ${SecLanguageEnglish} $(DESC_SecLanguageEnglish)
		!insertmacro MUI_DESCRIPTION_TEXT ${SecLanguageJapanese} $(DESC_SecLanguageJapanese)

		!insertmacro MUI_DESCRIPTION_TEXT ${SecDocumentTemplates} $(DESC_SecDocumentTemplates)
		!insertmacro MUI_DESCRIPTION_TEXT ${SecDocumentTemplatesDatabase} $(DESC_SecDocumentTemplatesDatabase)
		!insertmacro MUI_DESCRIPTION_TEXT ${SecDocumentTemplatesDatabaseAssets} $(DESC_SecDocumentTemplatesDatabaseAssets)
		!insertmacro MUI_DESCRIPTION_TEXT ${SecDocumentTemplatesDatabaseContacts} $(DESC_SecDocumentTemplatesDatabaseContacts)
		!insertmacro MUI_DESCRIPTION_TEXT ${SecDocumentTemplatesDatabaseEvents} $(DESC_SecDocumentTemplatesDatabaseEvents)
		!insertmacro MUI_DESCRIPTION_TEXT ${SecDocumentTemplatesDatabaseFaculty} $(DESC_SecDocumentTemplatesDatabaseFaculty)
		!insertmacro MUI_DESCRIPTION_TEXT ${SecDocumentTemplatesDatabaseIssues} $(DESC_SecDocumentTemplatesDatabaseIssues)
		!insertmacro MUI_DESCRIPTION_TEXT ${SecDocumentTemplatesDatabaseMarketingProjects} $(DESC_SecDocumentTemplatesDatabaseMarketingProjects)
		!insertmacro MUI_DESCRIPTION_TEXT ${SecDocumentTemplatesDatabaseNorthwind} $(DESC_SecDocumentTemplatesDatabaseNorthwind)
		!insertmacro MUI_DESCRIPTION_TEXT ${SecDocumentTemplatesDatabaseProjects} $(DESC_SecDocumentTemplatesDatabaseProjects)
		!insertmacro MUI_DESCRIPTION_TEXT ${SecDocumentTemplatesDatabaseSalesPipeline} $(DESC_SecDocumentTemplatesDatabaseSalesPipeline)
		!insertmacro MUI_DESCRIPTION_TEXT ${SecDocumentTemplatesDatabaseStudents} $(DESC_SecDocumentTemplatesDatabaseStudents)
		!insertmacro MUI_DESCRIPTION_TEXT ${SecDocumentTemplatesDatabaseTasks} $(DESC_SecDocumentTemplatesDatabaseTasks)

		!insertmacro MUI_DESCRIPTION_TEXT ${SecPlugins} $(DESC_SecPlugins)
		!insertmacro MUI_DESCRIPTION_TEXT ${SecPluginFileSystem} $(DESC_SecPluginFileSystem)
	!insertmacro MUI_FUNCTION_DESCRIPTION_END

;--------------------------------
;Uninstaller Section

Section "Uninstall"

	;ADD YOUR OWN FILES HERE...
	Delete "$INSTDIR\AwesomeControls.dll"
	Delete "$INSTDIR\glue.dll"
	Delete "$INSTDIR\UniversalEditor.Compression.dll"
	Delete "$INSTDIR\UniversalEditor.Core.dll"
	Delete "$INSTDIR\UniversalEditor.Engines.WindowsForms.dll"
	Delete "$INSTDIR\UniversalEditor.Essential.dll"
	Delete "$INSTDIR\UniversalEditor.exe"
	Delete "$INSTDIR\UniversalEditor.UserInterface.dll"
	Delete "$INSTDIR\UniversalEditor.UserInterface.WindowsForms.dll"
	Delete "$INSTDIR\UniversalEditor.Plugins.Multimedia.dll"
	Delete "$INSTDIR\UniversalEditor.Plugins.Multimedia.Binders.GdiPlus.dll"
	Delete "$INSTDIR\UniversalEditorConsole.exe"

	Delete "$INSTDIR\Configuration\Application.upl"
	Delete "$INSTDIR\Configuration\Application.xml"
	Delete "$INSTDIR\Configuration\CommandBars.xml"
	Delete "$INSTDIR\Configuration\Commands.xml"
	Delete "$INSTDIR\Configuration\MainMenu.xml"
	Delete "$INSTDIR\Configuration\ObjectModels.xml"
	Delete "$INSTDIR\Configuration\SplashScreen.upl"
	Delete "$INSTDIR\Configuration\StartPage.xml"
	RMDir "$INSTDIR\Configuration"

	Delete "$INSTDIR\Languages\English.xml"
	Delete "$INSTDIR\Languages\Japanese.xml"
	RMDir "$INSTDIR\Languages"

	RMDir "$INSTDIR\Plugins"

	RMDir "$INSTDIR\ProjectTypes"
	RMDir "$INSTDIR\Templates"

	Delete "$INSTDIR\uninstall.exe"

	RMDir "$INSTDIR"

	!insertmacro MUI_STARTMENU_GETFOLDER Application $StartMenuFolder

	Delete "$SMPROGRAMS\$StartMenuFolder\Uninstall.lnk"
	RMDir "$SMPROGRAMS\$StartMenuFolder"

	;Delete installation data from the Windows Registry
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_GUID}"

	DeleteRegKey /ifempty HKCU "Software\${PRODUCT_COMPANY}\${PRODUCT_TITLE}"

SectionEnd

;--------------------------------
;Uninstaller Functions

Function un.onInit

	!insertmacro MUI_UNGETLANGUAGE
  
FunctionEnd