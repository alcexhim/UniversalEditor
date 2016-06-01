/*
 * PlatformAboutDialog.h
 *
 *  Created on: Apr 26, 2016
 *      Author: beckermj
 */

#ifndef PLATFORMABOUTDIALOG_H_
#define PLATFORMABOUTDIALOG_H_

#include <QtCore/QtCore>
#include <QtGui/QtGui>
#include <QtWidgets/QtWidgets>

namespace UniversalEditor {
namespace UserInterface {
namespace Dialogs {

class PlatformAboutDialog : public QDialog {
private:
	static PlatformAboutDialog* _instance;
public:
	PlatformAboutDialog();
	virtual ~PlatformAboutDialog();

	static void showDialog();
};

} /* namespace Dialogs */
} /* namespace UserInterface */
} /* namespace UniversalEditor */

#endif /* PLATFORMABOUTDIALOG_H_ */
