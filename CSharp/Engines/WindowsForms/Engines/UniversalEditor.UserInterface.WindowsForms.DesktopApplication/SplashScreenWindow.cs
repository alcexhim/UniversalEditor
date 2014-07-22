using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms
{
	public partial class SplashScreenWindow : Form
	{
		public SplashScreenWindow()
		{
			InitializeComponent();

			// base.Font = SystemFonts.MenuFont;
			// label1.Font = new Font(base.Font.FontFamily, 18, FontStyle.Bold);
			// label1.Text = Configuration.ApplicationName;

			// label2.Font = base.Font;

			/*
			string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
			Icon icn = IconMethods.ExtractAssociatedIcon(path, 0);
			if (icn != null)
			{
				pictureBox1.Image = icn.ToBitmap();
			}
			*/

			lblTitle.Text = Engine.CurrentEngine.DefaultLanguage.GetStringTableEntry("ApplicationTitle", "Universal Editor");

			if (LocalConfiguration.SplashScreen.Image != null)
			{
				pictureBox1.Image = LocalConfiguration.SplashScreen.Image;
			}
			else
			{
				if (!String.IsNullOrEmpty(LocalConfiguration.SplashScreen.ImageFileName))
				{
					if (System.IO.File.Exists(LocalConfiguration.SplashScreen.ImageFileName))
					{
						pictureBox1.Image = Image.FromFile(LocalConfiguration.SplashScreen.ImageFileName);
					}
				}
			}
			// Size = pictureBox1.Image.Size;
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			if (LocalConfiguration.SplashScreen.Sound != null)
			{
				System.Media.SoundPlayer sp = new System.Media.SoundPlayer(LocalConfiguration.SplashScreen.Sound);
				sp.Play();
			}
			else
			{
				if (System.IO.File.Exists(LocalConfiguration.SplashScreen.SoundFileName))
				{
					System.Media.SoundPlayer sp = new System.Media.SoundPlayer(LocalConfiguration.SplashScreen.SoundFileName);
					sp.Play();
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, base.Width - 1, base.Height - 1));
		}

		internal void InvokeClose()
		{
			Invoke(new Action(_InvokeClose));
		}
		private void _InvokeClose()
		{
			Close();
		}

		internal void InvokeUpdateStatus(string p)
		{
			Invoke(new Action<string>(_InvokeUpdateStatus), p);
		}
		private void _InvokeUpdateStatus(string p)
		{
			lblStatus.Text = p;
		}
	}
	public class SplashScreenSettings
	{
		private bool mvarEnabled = true;
		public bool Enabled { get { return mvarEnabled; } set { mvarEnabled = value; } }

		private string mvarImageFileName = String.Empty;
		public string ImageFileName { get { return mvarImageFileName; } set { mvarImageFileName = value; } }

		private string mvarSoundFileName = String.Empty;
		public string SoundFileName { get { return mvarSoundFileName; } set { mvarSoundFileName = value; } }

		private Image mvarImage = null;
		public Image Image { get { return mvarImage; } set { mvarImage = value; } }

		private System.IO.MemoryStream mvarSound = null;
		public System.IO.MemoryStream Sound { get { return mvarSound; } set { mvarSound = value; } }
	}
}
