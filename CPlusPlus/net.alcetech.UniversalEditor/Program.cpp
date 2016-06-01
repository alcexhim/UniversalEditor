#define LINUX

#include <QtCore/QtCore>
#include <QtGui/QtGui>
#include <QtWidgets/QtWidgets>

#include <Document.h>

#include <IO/Endianness.h>
#include <Accessors/FileAccessor.h>
#include <IO/Writer.h>

#include <IO/Directory.h>
#include <IO/File.h>

// net.alcetech.UniversalEditor.Essential includes
#include <DataFormats/Markup/XML/XMLDataFormat.h>
#include <ObjectModels/Markup/MarkupObjectModel.h>
#include <ObjectModels/Markup/MarkupAttribute.h>
#include <ObjectModels/Markup/MarkupTagElement.h>

#include <ObjectModels/Text/Plain/PlainTextObjectModel.h>
#include <DataFormats/Text/Plain/PlainTextDataFormat.h>

#include <Application.h>
#include <Dimension2D.h>
#include <Console.h>
#include <String.h>
#include <Text/StringBuilder.h>

#include <Command.h>
#include <CommandReferenceCommandItem.h>
#include <GuiApplication.h>
#include <SeparatorCommandItem.h>
#include <Shortcut.h>

#include <DocumentTab.h>
#include <Panel.h>
#include <PanelGroup.h>
#include <PanelArea.h>
#include <PanelReference.h>
#include <Perspective.h>

#include <MainWindow.h>

// net.alcetech.UniversalEditor.UserInterface includes
#include <Dialogs/PlatformAboutDialog.h>
#include <ToolboxManager.h>
#include <ToolboxItem.h>
#include <ToolboxGroup.h>

// net.alcetech.UniversalEditor.Core includes
#include <Association.h>
#include <ObjectModel.h>
#include <ObjectModelReference.h>
#include <DataFormat.h>
#include <DataFormatReference.h>

#include <DataFormatLayoutItems/FieldDataFormatLayoutItem.h>

#include <CommandBar.h>

using ApplicationFramework::Application;
using ApplicationFramework::Console;

using ApplicationFramework::Command;
using ApplicationFramework::CommandItem;
using ApplicationFramework::CommandReferenceCommandItem;
using ApplicationFramework::Dimension2D;
using ApplicationFramework::SeparatorCommandItem;

using ApplicationFramework::Shortcut;
using ApplicationFramework::String;

using ApplicationFramework::IO::Directory;
using ApplicationFramework::IO::File;

using ApplicationFramework::UserInterface::DocumentTab;
using ApplicationFramework::UserInterface::MainWindow;
using ApplicationFramework::UserInterface::GuiApplication;
using ApplicationFramework::UserInterface::Panel;
using ApplicationFramework::UserInterface::PanelGroup;
using ApplicationFramework::UserInterface::PanelArea;
using ApplicationFramework::UserInterface::PanelReference;
using ApplicationFramework::UserInterface::Perspective;

using UniversalEditor::Document;

using UniversalEditor::IO::Endianness;
using UniversalEditor::IO::Writer;

using UniversalEditor::Accessors::FileAccessor;

using UniversalEditor::DataFormats::Markup::XML::XMLDataFormat;
using UniversalEditor::ObjectModels::Markup::MarkupObjectModel;
using UniversalEditor::ObjectModels::Markup::MarkupAttribute;
using UniversalEditor::ObjectModels::Markup::MarkupElement;
using UniversalEditor::ObjectModels::Markup::MarkupTagElement;

using UniversalEditor::ObjectModels::Text::Plain::PlainTextObjectModel;
using UniversalEditor::DataFormats::Text::Plain::PlainTextDataFormat;

using ApplicationFramework::Text::StringBuilder;

using UniversalEditor::UserInterface::ToolboxItem;
using UniversalEditor::UserInterface::ToolboxGroup;
using UniversalEditor::UserInterface::ToolboxManager;
using UniversalEditor::UserInterface::Dialogs::PlatformAboutDialog;

using UniversalEditor::Association;
using UniversalEditor::ObjectModel;
using UniversalEditor::ObjectModelReference;
using UniversalEditor::DataFormat;
using UniversalEditor::DataFormatReference;

using UniversalEditor::DataFormatLayoutItems::FieldDataFormatLayoutItem;

using ApplicationFramework::UserInterface::CommandBar;

void postInitApp(MarkupObjectModel* mom)
{
	List<MarkupTagElement*>* list = mom->getChildTagsList();
	for (int li = 0; li < list->count(); li++)
	{
		MarkupTagElement* tagApplicationFramework = list->get(li);
		if (tagApplicationFramework->getFullName()->equals("ApplicationFramework"))
		{
			MarkupTagElement* tagLanguages = tagApplicationFramework->getChildTag("Languages");
			String* defaultLanguageID = NULL;

			if (tagLanguages != NULL)
			{
				MarkupAttribute* attDefaultLanguageID = tagLanguages->getAttribute("DefaultLanguageID");
				if (attDefaultLanguageID != NULL) defaultLanguageID = attDefaultLanguageID->getValue();

				List<MarkupTagElement*>* tags = tagLanguages->getChildTagsList();
				for (int i = 0; i < tags->count(); i++)
				{
					MarkupTagElement* tagLanguage = tags->get(i);
					if (tagLanguage == NULL) continue;

					if (tagLanguage->getFullName()->equals("Language"))
					{
						MarkupAttribute* attLanguageID = tagLanguage->getAttribute("ID");
						if (attLanguageID == NULL) continue;

						if (defaultLanguageID != NULL)
						{
							if (!attLanguageID->getValue()->equals(defaultLanguageID)) continue;
						}

						MarkupTagElement* tagCommands = tagLanguage->getChildTag("Commands");
						if (tagCommands != NULL)
						{
							List<MarkupTagElement*>* listCommands = tagCommands->getChildTagsList();
							for (int j = 0; j < listCommands->count(); j++)
							{
								MarkupTagElement* tagCommand = listCommands->get(j);

								MarkupAttribute* attCommandID = tagCommand->getAttribute("ID");
								MarkupAttribute* attCommandTitle = tagCommand->getAttribute("Title");

								if (attCommandID != NULL && attCommandTitle != NULL)
								{
									Command* cmd = Application::getCommand(attCommandID->getValue());
									if (cmd != NULL)
									{
										cmd->setTitle(attCommandTitle->getValue());
									}
								}
							}
						}
					}
				}
			}
		}



		else if (tagApplicationFramework->getFullName()->equals("UniversalEditor")) {

			List<MarkupTagElement*>* listChildTags = tagApplicationFramework->getChildTagsList();
			for (int i = 0; i < listChildTags->count(); i++)
			{
				MarkupTagElement* tag = listChildTags->get(i);
				if (tag->getFullName()->equals("Associations")) {
					List<MarkupTagElement*>* listAssociations = tag->getChildTagsList();
					for (int dfi = 0; dfi < listAssociations->count(); dfi++)
					{
						MarkupTagElement* tagAssociation = listAssociations->get(dfi);
						if (!tagAssociation->getFullName()->equals("Association")) continue;

						Association* assoc = new Association();

						MarkupTagElement* tagFilters = tagAssociation->getChildTag("Filters");
						if (tagFilters != NULL)
						{

						}

						MarkupTagElement* tagObjectModels = tagAssociation->getChildTag("ObjectModels");
						if (tagObjectModels != NULL)
						{
							List<MarkupTagElement*>* listObjectModels = tagObjectModels->getChildTagsList();
							for (int j = 0; j < listObjectModels->count(); j++)
							{
								MarkupTagElement* tagObjectModel = listObjectModels->get(j);
								if (!tagObjectModel->getFullName()->equals("ObjectModel")) continue;

								ObjectModelReference* omr = new ObjectModelReference();

								MarkupAttribute* attObjectModelTypeName = tagObjectModel->getAttribute("TypeName");
								if (attObjectModelTypeName != NULL) omr->setTypeName(attObjectModelTypeName->getValue());

								assoc->getObjectModelsList()->add(omr);
							}
						}

						MarkupTagElement* tagDataFormats = tagAssociation->getChildTag("DataFormats");
						if (tagDataFormats != NULL)
						{
							List<MarkupTagElement*>* listDataFormats = tagDataFormats->getChildTagsList();
							for (int j = 0; j < listDataFormats->count(); j++)
							{
								MarkupTagElement* tagDataFormat = listDataFormats->get(j);
								if (!tagDataFormat->getFullName()->equals("DataFormat")) continue;

								DataFormatReference* dfr = new DataFormatReference();

								MarkupAttribute* attDataFormatTypeName = tagDataFormat->getAttribute("TypeName");
								if (attDataFormatTypeName != NULL) dfr->setTypeName(attDataFormatTypeName->getValue());

								assoc->getDataFormatsList()->add(dfr);
							}
						}

						Association::getAssociationsList()->add(assoc);
					}
				}
				else if (tag->getFullName()->equals("ObjectModels")) {
					List<MarkupTagElement*>* listObjectModels = tag->getChildTagsList();
					for (int dfi = 0; dfi < listObjectModels->count(); dfi++)
					{
						MarkupTagElement* tagObjectModel = listObjectModels->get(dfi);
						if (!tagObjectModel->getFullName()->equals("ObjectModel")) continue;

						ObjectModelReference* omr = new ObjectModelReference();

						MarkupAttribute* attObjectModelID = tagObjectModel->getAttribute("ID");
						if (attObjectModelID != NULL) omr->setID(Guid::fromString(attObjectModelID->getValue()));

						MarkupAttribute* attObjectModelTypeName = tagObjectModel->getAttribute("TypeName");
						if (attObjectModelTypeName != NULL) omr->setTypeName(attObjectModelTypeName->getValue());

						MarkupAttribute* attObjectModelVisible = tagObjectModel->getAttribute("Visible");
						if (attObjectModelVisible != NULL)
						{
							omr->setVisible(attObjectModelVisible->getValue()->equals("true"));
						}

						ObjectModelReference::getObjectModelReferencesList()->add(omr);
					}
				}
				else if (tag->getFullName()->equals("DataFormats")) {
					List<MarkupTagElement*>* listDataFormats = tag->getChildTagsList();
					for (int dfi = 0; dfi < listDataFormats->count(); dfi++)
					{
						MarkupTagElement* tagDataFormat = listDataFormats->get(dfi);
						if (!tagDataFormat->getFullName()->equals("DataFormat")) continue;

						DataFormatReference* dfr = new DataFormatReference();
						MarkupAttribute* attTypeName = tagDataFormat->getAttribute("TypeName");
						if (attTypeName != NULL) {
							dfr->setTypeName(attTypeName->getValue());

							printf("found DataFormat '%s'\n", dfr->getTypeName()->toCharArray());

							MarkupTagElement* tagLayout = tagDataFormat->getChildTag("Layout");
							if (tagLayout != NULL)
							{
								List<MarkupTagElement*>* listLayoutItems = tagLayout->getChildTagsList();
								for (int lli = 0; lli < listLayoutItems->count(); lli++)
								{
									MarkupTagElement* tagLayoutItem = listLayoutItems->get(lli);
									if (tagLayoutItem->getFullName()->equals("Field"))
									{
										MarkupAttribute* attID = tagLayoutItem->getAttribute("ID");
										if (attID == NULL) continue;

										MarkupAttribute* attDataType = tagLayoutItem->getAttribute("DataType");
										if (attDataType == NULL) continue;

										FieldDataFormatLayoutItem* field = new FieldDataFormatLayoutItem();
										field->setID(attID->getValue());
										field->setDataType(attDataType->getValue());
										dfr->getLayoutItemsList()->add(field);

										printf("found FIELD with ID='%s', DataType='%s'\n", attID->getValue()->toCharArray(), attDataType->getValue()->toCharArray());
									}
								}
							}
							else
							{
								printf("DataFormat does not have Layout tag\n");
							}
						}

						DataFormatReference::getDataFormatReferencesList()->add(dfr);
					}

				}
			}

		}
	}
}

void initApp(const char* fileName, MarkupObjectModel* mom, XMLDataFormat* xdf) {
	FileAccessor* fa = FileAccessor::fromFileName(fileName);
	fa->open();

	if (!fa->exists()) return;

	Document::load(mom, xdf, fa);
	fa->close();
}

String* defaultPerspectiveID = NULL;

void initUniversalEditorApp(MarkupTagElement* tagUniversalEditor) {

	MarkupTagElement* tagPerspectives = tagUniversalEditor->getChildTag("Perspectives");
	if (tagPerspectives != NULL)
	{
		MarkupAttribute* attDefaultPerspectiveID = tagPerspectives->getAttribute("DefaultPerspectiveID");
		if (attDefaultPerspectiveID != NULL) {
			defaultPerspectiveID = attDefaultPerspectiveID->getValue();
		}

		List<MarkupTagElement*>* listPerspectives = tagPerspectives->getChildTagsList();
		for (int i = 0; i < listPerspectives->count(); i++)
		{
			MarkupTagElement* tag = listPerspectives->get(i);
			if (!tag->getFullName()->equals("Perspective")) continue;

			MarkupAttribute* attID = tag->getAttribute("ID");
			if (attID == NULL) continue;


			Perspective* psq = GuiApplication::getPerspective(attID->getValue());
			if (psq == NULL) {
				psq = new Perspective();
				GuiApplication::addPerspective(psq);
			}
			psq->setID(attID->getValue());

			MarkupAttribute* attTitle = tag->getAttribute("Title");
			if (attTitle != NULL) {
				psq->setTitle(attTitle->getValue());
			}

			MarkupTagElement* tagSupportedObjectModels = tag->getChildTag("SupportedObjectModels");
			if (tagSupportedObjectModels != NULL) {
				printf("Found SupportedObjectModels tag for perspective '%s'\n", psq->getTitle()->toCharArray());
			}

		}
	}
}
void initApplicationFrameworkApp(MarkupTagElement* tagApplicationFramework) {
	MarkupTagElement* tagCommands = tagApplicationFramework->getChildTag("Commands");
	if (tagCommands != NULL)
	{
		List<MarkupTagElement*>* tags = tagCommands->getChildTagsList();
		for (int i = 0; i < tags->count(); i++)
		{
			MarkupTagElement* tag = tags->get(i);
			if (tag == NULL) continue;

			if (tag->getFullName()->equals("Command"))
			{
				MarkupAttribute* attID = tag->getAttribute("ID");
				if (attID == NULL) continue;

				String* commandID = attID->getValue();

				Command* cmd = Application::getCommand(commandID);
				if (cmd == NULL)
				{
					cmd = new Command();
					Application::addCommand(cmd);

					printf("added command '%s'\n", commandID->toCharArray());
				}
				cmd->setID(commandID);

				MarkupAttribute* attTitle = tag->getAttribute("Title");
				if (attTitle != NULL)
				{
					cmd->setTitle(attTitle->getValue());
				}

				MarkupTagElement* tagShortcut = tag->getChildTag("Shortcut");
				if (tagShortcut != NULL)
				{
					Shortcut* shortcut = new Shortcut();

					MarkupAttribute* attModifiers = tagShortcut->getAttribute("Modifiers");
					if (attModifiers != NULL)
					{
						printf("setting command '%s' shortcut modifier '%s'\n", cmd->getID()->toCharArray(), attModifiers->getValue()->toCharArray());
						shortcut->setModifierKeysString(attModifiers->getValue());
					}

					MarkupAttribute* attKey = tagShortcut->getAttribute("Key");
					if (attKey != NULL)
					{
						printf("setting command '%s' shortcut key '%s'\n", cmd->getID()->toCharArray(), attKey->getValue()->toCharArray());
						shortcut->setKeyString(attKey->getValue());
					}

					cmd->setShortcut(shortcut);
				}

				MarkupTagElement* tagItems = tag->getChildTag("Items");
				if (tagItems != NULL)
				{
					List<MarkupTagElement*>* listItems = tagItems->getChildTagsList();
					for (int j = 0; j < listItems->count(); j++)
					{
						MarkupTagElement* tagItem = listItems->get(j);

						if (tagItem->getFullName()->equals("CommandReference"))
						{
							MarkupAttribute* attCommandID = tagItem->getAttribute("CommandID");
							if (attCommandID == NULL) continue;

							// this is a command reference menu item
							String* commandID = attCommandID->getValue();

							CommandReferenceCommandItem* crci = new CommandReferenceCommandItem(commandID);
							cmd->addCommandItem(crci);
						}
						else if (tagItem->getFullName()->equals("Separator"))
						{
							cmd->addCommandItem(new SeparatorCommandItem());
						}
					}
				}
			}
		}
	}
	else
	{
		printf("no Commands tag found in ApplicationFramework tag\n");
	}

	MarkupTagElement* tagMainMenu = tagApplicationFramework->getChildTag("MainMenu");
	if (tagMainMenu != NULL)
	{
		MarkupTagElement* tagItems = tagMainMenu->getChildTag("Items");

		List<MarkupTagElement*>* tags = tagItems->getChildTagsList();
		for (int i = 0; i < tags->count(); i++)
		{
			MarkupTagElement* tag = tags->get(i);
			if (tag == NULL) continue;

			if (tag->getFullName()->equals("CommandReference"))
			{
				MarkupAttribute* attCommandID = tag->getAttribute("CommandID");
				if (attCommandID == NULL) continue;

				// this is a command reference menu item
				String* commandID = attCommandID->getValue();

				CommandReferenceCommandItem* crci = new CommandReferenceCommandItem(commandID);
				Application::addMainMenuCommandItem(crci);
			}
			else if (tag->getFullName()->equals("Separator") == 0)
			{
				Application::addMainMenuCommandItem(new SeparatorCommandItem());
			}
		}
	}

	MarkupTagElement* tagPanels = tagApplicationFramework->getChildTag("Panels");
	if (tagPanels != NULL)
	{
		List<MarkupTagElement*>* listPanels = tagPanels->getChildTagsList();
		for (int i = 0; i < listPanels->count(); i++)
		{
			MarkupTagElement* tag = listPanels->get(i);
			if (!tag->getFullName()->equals("Panel")) continue;

			MarkupAttribute* attID = tag->getAttribute("ID");
			if (attID == NULL) continue;

			Panel* panel = new Panel();
			panel->setID(attID->getValue());

			MarkupAttribute* attTitle = tag->getAttribute("Title");
			if (attTitle != NULL) panel->setTitle(attTitle->getValue());

			GuiApplication::addPanel(panel);
		}
	}

	MarkupTagElement* tagPerspectives = tagApplicationFramework->getChildTag("Perspectives");
	if (tagPerspectives != NULL)
	{
		MarkupAttribute* attDefaultPerspectiveID = tagPerspectives->getAttribute("DefaultPerspectiveID");
		if (attDefaultPerspectiveID != NULL) {
			defaultPerspectiveID = attDefaultPerspectiveID->getValue();
		}

		List<MarkupTagElement*>* listPerspectives = tagPerspectives->getChildTagsList();
		for (int i = 0; i < listPerspectives->count(); i++)
		{
			MarkupTagElement* tag = listPerspectives->get(i);
			if (!tag->getFullName()->equals("Perspective")) continue;

			MarkupAttribute* attID = tag->getAttribute("ID");
			if (attID == NULL) continue;


			Perspective* psq = GuiApplication::getPerspective(attID->getValue());
			if (psq == NULL) {
				psq = new Perspective();
				GuiApplication::addPerspective(psq);
			}
			psq->setID(attID->getValue());

			MarkupAttribute* attTitle = tag->getAttribute("Title");
			if (attTitle != NULL) {
				psq->setTitle(attTitle->getValue());
			}

			MarkupTagElement* tagCommandBars = tag->getChildTag("CommandBars");
			if (tagCommandBars != NULL)
			{
				List<MarkupTagElement*>* listCommandBars = tagCommandBars->getChildTagsList();
				for (int j = 0; j < listCommandBars->count(); j++)
				{
					MarkupTagElement* tagCommandBar = listCommandBars->get(j);
					if (!tagCommandBar->getFullName()->equals("CommandBar")) continue;

					MarkupAttribute* attCommandBarID = tagCommandBar->getAttribute("ID");
					if (attCommandBarID == NULL) continue;

					String* title = attCommandBarID->getValue();

					MarkupAttribute* attCommandBarTitle = tagCommandBar->getAttribute("Title");
					if (attCommandBarTitle != NULL) title = attCommandBarTitle->getValue();

					CommandBar* cb = new CommandBar();
					cb->setID(attCommandBarID->getValue());
					cb->setTitle(title);

					MarkupTagElement* tagCommandBarItems = tagCommandBar->getChildTag("Items");
					if (tagCommandBarItems != NULL)
					{
						List<MarkupTagElement*>* listCommandBarItems = tagCommandBarItems->getChildTagsList();
						for (int k = 0; k < listCommandBarItems->count(); k++)
						{
							MarkupTagElement* tagCommandBarItem = listCommandBarItems->get(k);
							if (tagCommandBarItem->getFullName()->equals("CommandReference"))
							{
								MarkupAttribute* attCommandID = tagCommandBarItem->getAttribute("CommandID");
								if (attCommandID == NULL) continue;

								CommandReferenceCommandItem* crci = new CommandReferenceCommandItem();
								crci->setCommandID(attCommandID->getValue());
								cb->addItem(crci);
							}
							else if (tagCommandBarItem->getFullName()->equals("Separator"))
							{
								cb->addItem(new SeparatorCommandItem());
							}
						}
					}

					psq->getCommandBarsList()->add(cb);
				}
			}

			MarkupTagElement* tagPanelAreas = tag->getChildTag("PanelAreas");
			if (tagPanelAreas != NULL)
			{
				List<MarkupTagElement*>* listPanelAreas = tagPanelAreas->getChildTagsList();
				for (int j = 0; j < listPanelAreas->count(); j++)
				{
					MarkupTagElement* tagPanelArea = listPanelAreas->get(j);
					if (!tagPanelArea->getFullName()->equals("PanelArea")) continue;

					MarkupAttribute* attPanelAreaLocation = tagPanelArea->getAttribute("Location");
					if (attPanelAreaLocation == NULL) continue;

					PanelArea* area = new PanelArea();
					if (attPanelAreaLocation->getValue()->equals("Left"))
					{
						area->setLocation(PanelAreaLocation::Left);
					}
					else if (attPanelAreaLocation->getValue()->equals("Top"))
					{
						area->setLocation(PanelAreaLocation::Top);
					}
					else if (attPanelAreaLocation->getValue()->equals("Bottom"))
					{
						area->setLocation(PanelAreaLocation::Bottom);
					}
					else if (attPanelAreaLocation->getValue()->equals("Right"))
					{
						area->setLocation(PanelAreaLocation::Right);
					}

					List<MarkupTagElement*>* listPanelItems = tagPanelArea->getChildTagsList();
					for (int k = 0; k < listPanelItems->count(); k++)
					{
						MarkupTagElement* tagPanelItem = listPanelItems->get(k);
						if (tagPanelItem->getFullName()->equals("PanelReference"))
						{
							MarkupAttribute* attPanelItemID = tagPanelItem->getAttribute("ID");
							if (attPanelItemID == NULL) continue;

							area->addPanel(new PanelReference(attPanelItemID->getValue()));
						}
						else if (tagPanelItem->getFullName()->equals("PanelGroup"))
						{
							PanelGroup* group = new PanelGroup();

							List<MarkupTagElement*>* listPanelGroupItems = tagPanelItem->getChildTagsList();
							for (int l = 0; l < listPanelGroupItems->count(); l++)
							{
								MarkupTagElement* tagPanelGroupItem = listPanelGroupItems->get(l);
								if (!tagPanelGroupItem->getFullName()->equals("PanelReference")) continue;

								MarkupAttribute* attPanelGroupItemID = tagPanelGroupItem->getAttribute("ID");
								if (attPanelGroupItemID == NULL) continue;

								group->addPanel(new PanelReference(attPanelGroupItemID->getValue()));
							}
							area->addPanel(group);
						}
					}

					psq->addPanelArea(area);
				}
			}
		}
	}
}
void initApp()
{
	MarkupObjectModel* mom = new MarkupObjectModel();
	XMLDataFormat* xdf = new XMLDataFormat();

	Directory* dir = new Directory(new String("/usr/share/universal-editor"));
	List<File*>* listFiles = dir->getFiles(new String("*.uexml"), -1);
	for (int i = 0; i < listFiles->count(); i++)
	{
		File* file = listFiles->get(i);
		printf("loading configuration file '%s'\n", file->getFullPath()->toCharArray());
		if (!file->exists()) printf("file does not exist ???\n");

		initApp(file->getFullPath()->toCharArray(), mom, xdf);
	}

	List<MarkupTagElement*>* list = mom->getChildTagsList();
	for (int li = 0; li < list->count(); li++)
	{
		MarkupTagElement* tagApplicationFramework = list->get(li);
		if (tagApplicationFramework->getFullName()->equals("ApplicationFramework")) {
			initApplicationFrameworkApp(tagApplicationFramework);
		}
		else if (tagApplicationFramework->getFullName()->equals("UniversalEditor")) {
			initUniversalEditorApp(tagApplicationFramework);
		}
	}

	if (defaultPerspectiveID != NULL) {
		GuiApplication::setDefaultPerspective(GuiApplication::getPerspective(defaultPerspectiveID));
	}

	postInitApp(mom);
}
void File_Open_Document(Command* command)
{
	MainWindow* mw = GuiApplication::getCurrentMainWindow();
	if (mw == NULL) return;

	QStringList list = QFileDialog::getOpenFileNames(mw->getHandle());
	for (int i = 0; i < list.count(); i++)
	{
		QString filename = list.value(i);
		StringBuilder* strFileName = new StringBuilder(filename.toStdString().c_str());

		// BEGIN: editor-specific stuff

		FileAccessor* facc = FileAccessor::fromFileName(strFileName->toString()->toCharArray());
		PlainTextDataFormat* ptxt = new PlainTextDataFormat();
		PlainTextObjectModel* ptom = new PlainTextObjectModel();

		// huh, this doesn't actually work
		Document::load(ptom, ptxt, facc);

		QTextEdit* txt = new QTextEdit();
		txt->setFontFamily("Liberation Mono");
		txt->setFontPointSize(8.0);
		txt->setText(ptom->getText()->toCharArray());

		// END: editor specific stuff

		StringBuilder* strFileTitle = strFileName->copy();
		int ichr = strFileTitle->lastIndexOf('/');
		if (ichr == -1)
		{
			ichr = strFileTitle->lastIndexOf('\\');
		}

		if (ichr != -1)
		{
			strFileTitle->set(strFileTitle->substring(ichr + 1));
		}

		mw->addDocumentTab(strFileTitle->toString()->toCharArray(), filename.toStdString().c_str(), txt);
	}
}
void File_Close_Document(Command* command) {
	MainWindow* mw = GuiApplication::getCurrentMainWindow();
	if (mw == NULL) return;

	DocumentTab* dt = mw->getCurrentDocumentTab();
	if (dt != NULL) {
		dt->close();
	}
	else {
		Command* cmd = Application::getCommand("FileCloseWindow");
		if (cmd != NULL) Application::executeCommand(cmd);
	}
}
void File_Close_Window(Command* command) {
	MainWindow* mw = GuiApplication::getCurrentMainWindow();
	if (mw == NULL) return;

	mw->close();
}
void View_Panels_Panel(Command* command) {
	printf("executing View_Panels_Panel for item '%s'\n", command->getID()->toCharArray());
}
void View_Perspectives_Perspective(Command* command) {
	Perspective* persp = dynamic_cast<Perspective*>(command->getExtraData("Perspective"));
	if (persp != NULL) {
		GuiApplication::getCurrentMainWindow()->setPerspective(persp);
	}
	else {
		printf("executing View_Perspectives_Perspective for undefined perspective\n");
	}
}
void View_FullScreen(Command *command) {
	MainWindow* mw = GuiApplication::getCurrentMainWindow();
	if (mw == NULL) return;

	mw->toggleFullScreen();
}
void Window_Close_AllDocuments(Command* command) {
	MainWindow* mw = GuiApplication::getCurrentMainWindow();
	if (mw == NULL) return;

	mw->closeAllDocuments();
}
void Window_NewWindow(Command* command) {
	MainWindow* mw = new MainWindow();
	mw->setVisible(true);
}

void Help_About_Platform(Command* cmd) {
	PlatformAboutDialog::showDialog();
	/*
	QMessageBox::about(GuiApplication::getCurrentMainWindow()->getHandle(), "About Application",
			 "The <b>Application</b> example demonstrates how to "
		   "write modern GUI applications using Qt, with a menu bar, "
		   "toolbars, and a status bar.");
	*/
}

void panelOutputWindow_Initialize(Panel* panel) {
	QTextEdit* txt = new QTextEdit();
	txt->setText("Universal Editor 5.0-cpp-qt4\nCopyright (c) 2011-2016 Mike Becker's Software\nLicensed under the GNU General Public License\n\nOutput window test\n");
	panel->setWidget(txt);
}
void panelToolbox_Initialize(Panel* panel) {
	QToolBox* toolBox = new QToolBox();

	List<ToolboxGroup*>* list = ToolboxManager::getGroupsList();
	for (int i = 0; i < list->count(); i++)
	{
		ToolboxGroup* group = list->get(i);
		QWidget* widget = new QWidget();
		toolBox->addItem(widget, group->getTitle()->toCharArray());
	}

	panel->setWidget(toolBox);
}
void panelSolutionExplorer_Initialize(Panel* panel) {
	QTreeWidget* tree = new QTreeWidget();
	tree->setHeaderHidden(true);
	tree->setColumnCount(1);

	QTreeWidgetItem* item = new QTreeWidgetItem(tree);
	item->setText(0, "Empty Solution");
	tree->addTopLevelItem(item);
	panel->setWidget(tree);
}

int main(int argc, char** argv)
{
	GuiApplication::initialize(argc, argv);

	Application::setTitle(new String("Universal Editor"));

	QApplication* qapp = new QApplication(argc, argv, 0);

	initApp();

	ObjectModelReference* omr = ObjectModelReference::getByTypeName("UniversalEditor.ObjectModels.FileSystem.FileSystemObjectModel");
	if (omr == NULL) {
		printf("could not find OMR\n");
	}
	else
	{
		printf("found OMR with typename '%s'\n", omr->getTypeName()->toCharArray());
		// ObjectModel* om = omr->createInstance();
	}
	// return 0;


	List<Panel*>* listPanels = GuiApplication::getPanelsList();
	Command* cmdViewPanels = Application::getCommand("ViewPanels");
	if (cmdViewPanels != NULL)
	{
		StringBuilder* sb = new StringBuilder();
		for (int i = 0; i < listPanels->count(); i++)
		{
			Panel* panel = listPanels->get(i);

			sb->clear();
			sb->append("ViewPanelsPanel_");
			sb->append(panel->getID());

			Command* cmdViewPanelsPanel = new Command();
			cmdViewPanelsPanel->setID(sb->toString());
			cmdViewPanelsPanel->setTitle(panel->getTitle());
			Application::addCommand(cmdViewPanelsPanel);

			cmdViewPanels->addCommandItem(new CommandReferenceCommandItem(cmdViewPanelsPanel->getID()));
		}
	}
	else
	{
		printf("Command 'ViewPanels' not found; panel menu items will be unavailable\n");
	}

	Panel* panelOutputWindow = GuiApplication::getPanel("panelOutputWindow");
	if (panelOutputWindow != NULL)
	{
		panelOutputWindow->_initializationFunction = &panelOutputWindow_Initialize;
	}
	else
	{
		printf("Panel 'panelOutputWindow' not found; application output will be logged to stdout\n");
	}

	Panel* panelToolbox = GuiApplication::getPanel("panelToolbox");
	if (panelToolbox != NULL)
	{
		panelToolbox->_initializationFunction = &panelToolbox_Initialize;
	}

	Panel* panelSolutionExplorer = GuiApplication::getPanel("panelSolutionExplorer");
	if (panelSolutionExplorer != NULL)
	{
		panelSolutionExplorer->_initializationFunction = &panelSolutionExplorer_Initialize;
	}

	ToolboxGroup* tg = new ToolboxGroup();
	tg->setTitle(new String("General"));
	ToolboxManager::addGroup(tg);

	List<Perspective*>* listPerspectives = GuiApplication::getPerspectivesList();
	Command* cmdViewPerspectives = Application::getCommand("ViewPerspective");
	if (cmdViewPerspectives != NULL)
	{
		StringBuilder* sb = new StringBuilder();
		for (int i = 0; i < listPerspectives->count(); i++)
		{
			Perspective* perspective = listPerspectives->get(i);

			sb->clear();
			sb->append("ViewPerspectivesPerspective_");
			sb->append(perspective->getID());

			Command* cmdViewPerspectivesPerspective = new Command();
			cmdViewPerspectivesPerspective->setID(sb->toString());
			cmdViewPerspectivesPerspective->setTitle(perspective->getTitle());
			cmdViewPerspectivesPerspective->setExtraData("Perspective", perspective);
			Application::addCommand(cmdViewPerspectivesPerspective);

			Application::bindCommand(cmdViewPerspectivesPerspective, &View_Perspectives_Perspective);

			cmdViewPerspectives->addCommandItem(new CommandReferenceCommandItem(cmdViewPerspectivesPerspective->getID()));
		}
	}

	MainWindow* mw = new MainWindow();
	mw->setMinimumSize(new Dimension2D(1000, 700));
	mw->setVisible(true);

	if (cmdViewPanels != NULL)
	{
		StringBuilder* sb = new StringBuilder();
		for (int i = 0; i < listPanels->count(); i++)
		{
			sb->clear();
			sb->append("ViewPanelsPanel_");
			sb->append(listPanels->get(i)->getID());
			Application::bindCommand(Application::getCommand(sb->toString()), &View_Panels_Panel);
		}
	}

	Application::bindCommand(Application::getCommand("FileOpenDocument"), &File_Open_Document);
	Application::bindCommand(Application::getCommand("FileCloseDocument"), &File_Close_Document);
	Application::bindCommand(Application::getCommand("FileCloseWindow"), &File_Close_Window);
	Application::bindCommand(Application::getCommand("ViewFullScreen"), &View_FullScreen);
	Application::bindCommand(Application::getCommand("WindowCloseAllDocuments"), &Window_Close_AllDocuments);
	Application::bindCommand(Application::getCommand("WindowNewWindow"), &Window_NewWindow);

	Application::bindCommand(Application::getCommand("HelpAboutPlatform"), &Help_About_Platform);

	qapp->exec();
}
