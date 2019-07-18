cd ../../UniversalWidgetToolkit
msbuild UniversalWidgetToolkit.GTK.sln

cp Output/Debug/UniversalWidgetToolkit.Engines.GTK.dll ../UniversalEditor/CSharp/Output/Debug
cd ../UniversalEditor/CSharp
msbuild

