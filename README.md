UniversalEditor
===============

A free, cross-platform modular data manipulation framework.

This is Universal Editor 5, which is built using Universal Widget Toolkit to run smoothly on Windows and Linux. Mac OS X support in Universal Widget Toolkit is planned for the distant future.

TL;DR - Tested Working on Ubuntu 20.10
--------------------------------------
* Clone the repository.
* Open a terminal in the directory where you downloaded the repository.
* Run ./firstrun.sh to fetch dependencies and related projects.
* Type ./universal-editor to run the program in Debug mode from the repository folder.

Disclaimer
----------
Despite the version numbering, Universal Editor is far from ready-to-use in a production environment. There are plenty of areas of messy code that need cleaning up, and lots of scaffolding for functionality that is nowhere near complete. This is a free software project, maintained in the authors' free time and leisure. While this project strives to be a shining example of just how powerful and cross-platform .NET technology can be, please take issues with a grain of salt. Don't hesitate to open issues on the project page, but please be patient as the authors attempt to find time to fix bugs, polish existing features, and expand further development.

Philosophy
----------
UniversalEditor has four components:

* *Editor* is responsible for modifying the content of the ObjectModel from a user's point of view.
* *ObjectModel* contains the user-friendly, DataFormat-agnostic representation of the data.
* *DataFormat* is responsible for converting on-disk representations of data to and from user-friendly ObjectModel representations.
* *Accessor* is responsible for reading from and writing to actual sources of data using the DataFormat as the translator. Therefore, any Accessor is irrevocably paired with exactly one DataFormat.

Prerequisites
-------------
* MBS.Framework
* MBS.Rendering (if building Multimedia3D plugin)
* MBS.Framework.UserInterface (if building UserInterface)
* MBS.Audio (if building Multimedia plugin with user interface)

Building
--------
The solution and dependencies need to be cleaned up before we can write a proper tutorial for building. Keep everything in the same parent directory, *git clone* alcexhim/UniversalEditor, alcexhim/MBS.Framework, and alcexhim/MBS.Framework.UserInterface. For 3D rendering support it is also necessary to clone alcexhim/MBS.Framework.Rendering as well as the appropriate rendering engine (e.g. MBS.Framework.Rendering.Engines.OpenGL).

In a terminal window, navigate to the UniversalEditor directory that you just cloned, type `msbuild` (or `MSBuild.exe` on Windows), and press ENTER. This should take care of compiling UniversalEditor and any associated dependencies.

The application *WILL NOT* work properly if the appropriate Universal Widget Toolkit engine, MBS.Framework.UserInterface.(engine).dll, is not in the Output/(configuration) directory! The build process at this time does not automatically copy this file from the MBS.Framework.UserInterface output directory to the Universal Editor output directory. You may use the fxtool script (in bash) to do this: `fxtool install gtk` or `fxtool install wf`.

Currently the most up-to-date User Interface Engine is GTK, but I have made a lot of progress in bringing the Windows Forms one up to par. You might find that the GTK engine only works on Linux. I can't offer any help or support for getting the GTK engine to work on a different operating system. You should be able to use the Windows Forms engine on the latest version of Windows with minimal trouble.

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

Future Goals
------------
In a future release (originally planned for Universal Editor 5), multiple Accessor/DataFormat pairs can be associated with an ObjectModel. This would allow an ObjectModel to store data simultaneously in one format in one file, and in another format in another file. This is achieved through the use of a new component called an *Endpoint*, which pairs an Accessor and a DataFormat.
* If more than one Output Endpoint is specified, the resulting data is written to each Accessor using the associated DataFormat.
* <s>If more than one Input Endpoint is specified, the resulting data gets concatenated to the ObjectModel.</s> Multiple Input Endpoints will not be supported due to concerns about how to handle concatenation of the ObjectModel.
