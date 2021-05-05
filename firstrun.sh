#!/bin/sh
#
#  firstrun.sh - stupidly simple script to set up all dependencies
#
#  Author:
#       Michael Becker <alcexhim@gmail.com>
#
#  Copyright (c) 2021 Mike Becker's Software
#
#  This program is free software: you can redistribute it and/or modify
#  it under the terms of the GNU General Public License as published by
#  the Free Software Foundation, either version 3 of the License, or
#  (at your option) any later version.
#
#  This program is distributed in the hope that it will be useful,
#  but WITHOUT ANY WARRANTY; without even the implied warranty of
#  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
#  GNU General Public License for more details.
#
#  You should have received a copy of the GNU General Public License
#  along with this program.  If not, see <http://www.gnu.org/licenses/>.


# ================ INSTALL PACKAGE DEPENDENCIES FROM OFFICIAL REPOSITORY ================
sudo apt install libcairo2 libgdl-3-5


# ================ INSTALL LATEST MONO-COMPLETE FROM OFFICIAL REPOSITORY ================
MONO_REPO="ubuntu"
MONO_FLAVOR="stable-focal"
MONO_SUBFLAVOR="main"

echo "installing prerequisites"
sudo apt install gnupg ca-certificates

echo "receiving public key"
sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF

echo "adding repository to source list"
echo "deb https://download.mono-project.com/repo/$MONO_REPO $MONO_FLAVOR $MONO_SUBFLAVOR" | sudo tee /etc/apt/sources.list.d/mono-official-stable.list
sudo apt update

sudo apt install mono-complete


# ================ CLONE DEPENDENT PROJECTS FROM AUTHORS REPOSITORY ================
echo "fetching dependent projects from author repository"

GIT_USE_PUBKEY=false
if [ "$GIT_USE_PUBKEY" == "true" ]; then
	GIT_PREFIX=git@github.com
else
	GIT_PREFIX=git://github.com
fi

GIT_USERNAME=alcexhim
GIT_PROJECT_NAMES="MBS.Framework MBS.Framework.UserInterface MBS.Audio MBS.Framework.Rendering"

WD=${pwd}

cd ..

for projName in ${GIT_PROJECT_NAMES}; do

	git clone $GIT_PREFIX/$GIT_USERNAME/$projName

done

# generate strong name key file
sn -k Production.snk

cd $WD

# install othr junk
sudo cp MainIcon.png /usr/share/icons/universal-editor.png
sudo cp net.alcetech.UniversalEditor.desktop /usr/share/applications

# start the build
msbuild

# copy engine
./fxtool install gtk

echo "Build finished! Now type ./universal-editor to"
echo "run the program, or type ./install.sh to install to your local"
echo "machine."

