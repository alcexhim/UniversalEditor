@echo off

rem del "Output\Debug\ueditor.exe"

rem Compile the UniversalEditor application
rem gcc -o "Output/Debug/ueditor.exe" -mwindows -mms-bitfields -IC:/GTK+/include -IC:/GTK+/include/atk-1.0 -IC:/GTK+/include/gtk-2.0 -IC:/GTK+/lib/gtk-2.0/include -IC:/GTK+/include/gdk-pixbuf-2.0 -IC:/GTK+/include/gio-win32-2.0 -IC:/GTK+/include/cairo -IC:/GTK+/include/glib-2.0 -IC:/GTK+/lib/glib-2.0/include -IC:/GTK+/include/freetype2 -IC:/GTK+/include/pango-1.0 -IC:/GTK+/include/libpng14 Applications\UniversalEditor\*.c -LC:/GTK+/lib -lgtk-win32-2.0 -lgdk-win32-2.0 -latk-1.0 -lgio-2.0 -lpangowin32-1.0 -lgdi32 -lpangocairo-1.0 -lgdk_pixbuf-2.0 -lpango-1.0 -lcairo -lgmodule-2.0 -lgobject-2.0 -lgthread-2.0 -lglib-2.0 -lintl
gcc -o "Output/Debug/libuedit.dll" --shared Libraries/libuedit/*.c

rem pause

rem "Output/Debug/ueditor.exe"
