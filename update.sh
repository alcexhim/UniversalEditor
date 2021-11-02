#!/bin/sh

GIT_USER=alcexhim
GIT_URL=git@github.com:$GIT_USER
GIT_PROJECTS="MBS.Audio MBS.Framework MBS.Framework.UserInterface MBS.Framework.Rendering UniversalEditor"

for name in $GIT_PROJECTS; do

	if [ -d ../$name ]; then
		cd ../$name && git pull
	else
		cd ../ && git clone $GIT_URL/$name
		cd $name
	fi

done
