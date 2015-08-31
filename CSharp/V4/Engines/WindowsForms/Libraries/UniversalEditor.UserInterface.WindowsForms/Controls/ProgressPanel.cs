/*
 * Created by SharpDevelop.
 * User: Mike Becker
 * Date: 5/4/2013
 * Time: 3:12 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UniversalEditor.Controls
{
	/// <summary>
	/// Description of ProgressPanel.
	/// </summary>
	public partial class ProgressPanel : UserControl
	{
		public ProgressPanel()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public int Minimum { get { return pb.Minimum; } set { pb.Minimum = value; UpdateProgress(); } }
		public int Maximum { get { return pb.Maximum; } set { pb.Maximum = value; UpdateProgress(); } }
		public int Value { get { return pb.Value; } set { pb.Value = value; UpdateProgress(); } }
		
		private void UpdateProgress()
		{
			double pct = ((double)pb.Value / ((double)pb.Maximum - (double)pb.Minimum));
			pct *= 100;
			pct = Math.Round(pct);
			
			lblProgress.Text = pct.ToString() + "%";
		}
	}
}
