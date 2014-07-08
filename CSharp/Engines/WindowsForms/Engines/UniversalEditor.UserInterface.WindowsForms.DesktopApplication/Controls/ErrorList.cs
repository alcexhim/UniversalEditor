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
				
				ListViewItem lvi = new ListViewItem();
				lvi.Text = i.ToString();
				lvi.SubItems.Add(message.Description);
				lvi.SubItems.Add(message.FileName);
				if (message.LineNumber != null)
				{
					lvi.SubItems.Add(message.LineNumber.Value.ToString());
				}
				else
				{
					lvi.SubItems.Add(String.Empty);
				}
				if (message.ColumnNumber != null)
				{
					lvi.SubItems.Add(message.ColumnNumber.Value.ToString());
				}
				else
				{
					lvi.SubItems.Add(String.Empty);
				}
				lvi.SubItems.Add(message.ProjectName);
				lv.Items.Add(lvi);
			}
		}
	}
}
