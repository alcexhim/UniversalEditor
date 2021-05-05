#!/bin/sh

cp MainIcon.png /usr/share/icons/universal-editor.png
cp net.alcetech.UniversalEditor.desktop /usr/share/applications

mkdir /usr/lib/universal-editor
cp -r Output/Debug/* /usr/lib/universal-editor

cp universal-editor /usr/lib/universal-editor

echo "#!/bin/sh

cd /usr/lib/universal-editor
./universal-editor ${1+\"$@\"}
" > /usr/bin/universal-editor
