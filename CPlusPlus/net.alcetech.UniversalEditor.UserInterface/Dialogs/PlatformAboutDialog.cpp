/*
 * PlatformAboutDialog.cpp
 *
 *  Created on: Apr 26, 2016
 *      Author: beckermj
 */

#include "PlatformAboutDialog.h"

namespace UniversalEditor {
namespace UserInterface {
namespace Dialogs {

PlatformAboutDialog* PlatformAboutDialog::_instance = NULL;

PlatformAboutDialog::PlatformAboutDialog() {
	// TODO Auto-generated constructor stub

}

PlatformAboutDialog::~PlatformAboutDialog() {
	// TODO Auto-generated destructor stub
}

void PlatformAboutDialog::showDialog() {
	if (PlatformAboutDialog::_instance == NULL) {
		PlatformAboutDialog::_instance = new PlatformAboutDialog();
	}
	PlatformAboutDialog::_instance->setModal(true);
	PlatformAboutDialog::_instance->show();
}

} /* namespace Dialogs */
} /* namespace UserInterface */
} /* namespace UniversalEditor */
