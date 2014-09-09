using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Controls
{
	public partial class ErrorList : UserControl
	{
		public ErrorList()
		{
			InitializeComponent();
		}

		private HostApplicationMessage.HostApplicationMessageCollection mvarMessages = new HostApplicationMessage.HostApplicationMessageCollection();
		public HostApplicationMessage.HostApplicationMessageCollection Messages { get { return mvarMessages; } }

		public void RefreshList()
		{
			lv.Items.Clear();
			for (int i = 0; i < mvarMessages.Count; i++)
			{
				HostApplicationMessage message = mvarMessages[i];

				AwesomeControls.ListView.ListViewItem lvi = new AwesomeControls.ListView.ListViewItem();
				lvi.Text = i.ToString();
				lvi.Details.Add(message.Description);
				lvi.Details.Add(message.FileName);
				if (message.LineNumber != null)
				{
					lvi.Details.Add(message.LineNumber.Value.ToString());
				}
				else
				{
					lvi.Details.Add(String.Empty);
				}
				if (message.ColumnNumber != null)
				{
					lvi.Details.Add(message.ColumnNumber.Value.ToString());
				}
				else
				{
					lvi.Details.Add(String.Empty);
				}
				lvi.Details.Add(message.ProjectName);
				lv.Items.Add(lvi);
			}
		}
	}
}
