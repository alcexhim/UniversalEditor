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

WD=$(pwd)

# ================ INSTALL PACKAGE DEPENDENCIES FROM OFFICIAL REPOSITORY ================
sudo apt install libcairo2 libgdl-3-5

# ================ INSTALL LATEST MONO-COMPLETE FROM OFFICIAL REPOSITORY ================
if ! command -v msbuild
then
	whiptail --title "Update Mono from official repository?" --backtitle "Universal Editor Configure Script" --yesno "The version of Mono installed by default on this system does not ship msbuild or the required targets files to complete the build process.  Can I install the appropriate files from the Mono official repository?\n\nThis will add the Mono repository to your sources.list.d and install the mono-complete package." 15 60
	if [ $? -eq 1 ]; then
		echo "user skipped installing updated mono-complete; cannot continue"
		exit
	fi

	MONO_REPO="ubuntu"
	MONO_FLAVOR="stable-focal"
	MONO_SUBFLAVOR="main"

	echo "installing prerequisites"
	sudo apt install -y gnupg ca-certificates

	echo "receiving public key"
	sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF

	echo "adding repository to source list"
	echo "deb https://download.mono-project.com/repo/$MONO_REPO $MONO_FLAVOR $MONO_SUBFLAVOR" | sudo tee /etc/apt/sources.list.d/mono-official-stable.list
	sudo apt update

	sudo apt install -y mono-complete
fi

# ================ CLONE DEPENDENT PROJECTS FROM AUTHORS REPOSITORY ================
echo "fetching dependent projects from author repository"

GIT_USE_PUBKEY="false"
if [ "$GIT_USE_PUBKEY" = "true" ]; then
	GIT_PREFIX=git@github.com
else
	GIT_PREFIX=git://github.com
fi

GIT_USERNAME=alcexhim
GIT_PROJECT_NAMES="MBS.Framework MBS.Framework.UserInterface MBS.Audio MBS.Framework.Rendering"

cd ..

for projName in ${GIT_PROJECT_NAMES}; do

	git clone $GIT_PREFIX/$GIT_USERNAME/$projName

done

# generate strong name key file
if [ -e Production.snk ]; then
	whiptail --title "Generate new strong name key file?" --backtitle "Universal Editor Configure Script" --yesno "A strong name key file Production.snk already exists.  Do you want to overwrite it?  (If you don't know, you should probably say NO)" 15 60
	if [ $? -eq 0 ]; then
		sn -k Production.snk
	else
		echo "user skipped generating a strong name key file"
	fi
else
	sn -k Production.snk
fi

cd $WD

# install other junk
sudo cp MainIcon.png /usr/share/icons/universal-editor.png

# start the build
msbuild

# copy engine
./fxtool install gtk

# ================ INSTALL PRE-COMMIT HOOKS MANAGER ================
sudo apt install python3-pip
pip install pre-commit
~/.local/bin/pre-commit install

echo "Build finished! Now type ./universal-editor to"
echo "run the program, or type ./install.sh to install to your local"
echo "machine."
