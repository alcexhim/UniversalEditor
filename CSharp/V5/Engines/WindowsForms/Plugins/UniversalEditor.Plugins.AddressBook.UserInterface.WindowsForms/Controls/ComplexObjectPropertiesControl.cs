using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.Controls
{
	public partial class ComplexObjectPropertiesControl : UserControl
	{
		public ComplexObjectPropertiesControl()
		{
			InitializeComponent();
		}

		public Guid ElementID
		{
			get { return txtElementID.Value; }
			set { txtElementID.Value = value; }
		}

		public DateTime? ModificationDate
		{
			get
			{
				if (txtModificationDate.Checked)
				{
					return txtModificationDate.Value;
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					txtModificationDate.Value = value.Value;
					txtModificationDate.Checked = true;
				}
				else
				{
					txtModificationDate.Checked = false;
				}
			}
		}

		public bool IsEmpty
		{
			get { return chkIsEmpty.Checked; }
			set { chkIsEmpty.Checked = value; }
		}
	}
}
