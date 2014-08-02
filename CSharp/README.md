UniversalEditor
===============

A free, cross-platform modular data manipulation framework.

Prerequisites
-------------
* Glue (if building UserInterface)
* Sanjigen (if building Multimedia3D plugin)
* AwesomeControls (if building Windows Forms Engine)
* Surodoine (if building Windows Forms Engine)

Customization and Branding
--------------------------
To customize Universal Editor, look in the Content folder in the UniversalEditor.Content.PlatformIndependent project. All folders included within this project will be copied to the Output folder upon build.

* The *Branding* folder contains the application icon (on Windows) and image used for the splash screen. In the future the splash screen will have additional customization options, including the ability to modify parts of the splash screen (such as progress bars and status labels) using only configuration files.
* The *Configuration* folder contains XML files that are used to configure various properties of the software (although XML configuration files are actually read from anywhere in the application Output directory).
* The *Editors* folder contains editor-specific, platform-independent resources. You should not need to touch these unless you want to change the appearance of a particular editor (for example, providing different icons for folders in the FileSystem editor).

Translating the Software
------------------------
The *Languages* folder contains language translation string tables. The easiest way to translate the software is to copy the English.xml, translate all the strings, save it as a new language, and set it as the default language by specifying the DefaultLanguageID attribute on the UniversalEditor/Application/Languages element. Currently there is no way to change/save/load the default language at runtime, but that is trivial to implement and should be arriving shortly.

Project Types and Templates
---------------------------
* The *ProjectTypes* folder contains project type definitions. In the future it will be possible to provide project types via compiled assemblies (DLLs) and gain more control over the project process. ProjectTypes usually define build tasks that can be selected for each file in the project (for example, Compile/Content/EmbeddedResource) and provide other customization options.
* The *Templates* folder contains template definitions.

Philosophy
----------
UniversalEditor 4 has four components:

* *Editor* is responsible for modifying the content of the ObjectModel from a user's point of view. 
* *ObjectModel* contains the user-friendly, DataFormat-agnostic representation of the data.
* *DataFormat* is responsible for converting on-disk representations of data to and from user-friendly ObjectModel representations.
* *Accessor* is responsible for reading from and writing to actual sources of data using the DataFormat as the translator. Therefore, any Accessor is irrevocably paired with exactly one DataFormat.

However, starting with Universal Editor 5, multiple Accessor/DataFormat pairs can be associated with an ObjectModel. This would allow an ObjectModel to store data simultaneously in one format in one file, and in another format in another file.

Therefore, UniversalEditor 5 introduces a new component, called an *Endpoint*, which pairs an Accessor and a DataFormat. If more than one Input Endpoint is specified, the resulting data gets concatenated to the ObjectModel. If more than one Output Endpoint is specified, the resulting data is written to each Accessor using the associated DataFormat.
