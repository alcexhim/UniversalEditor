#!/bin/sh

cp MainIcon.png /usr/share/icons/universal-editor.png
cp net.alcetech.UniversalEditor.desktop /usr/share/applications

if [ ! -d /usr/lib/universal-editor ]; then
	mkdir /usr/lib/universal-editor
fi

cp -r Output/Debug/* /usr/lib/universal-editor

cp universal-editor /usr/lib/universal-editor

echo "#!/bin/sh

cd /usr/lib/universal-editor
./universal-editor \${1+\"\$@\"}
" > /usr/bin/universal-editor

chmod a+x /usr/bin/universal-editor
chmod a+x /usr/share/applications/net.alcetech.UniversalEditor.desktop
