using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

using AwesomeControls.ObjectModels.Theming;
using AwesomeControls.ListView;

namespace UniversalEditor.Plugins.AwesomeControls.UserInterface.WindowsForms.Editors.ThemePack
{
	public partial class ThemePackEditor : Editor
	{
		public ThemePackEditor()
		{
			InitializeComponent();
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(ThemeObjectModel));
			}
			return _er;
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			tv.Items.Clear();
			lv.Items.Clear();

			ThemeObjectModel themePack = (ObjectModel as ThemeObjectModel);
			if (themePack == null) return;

			foreach (Theme theme in themePack.Themes)
			{
				ListViewItem lvi = new ListViewItem();
				lvi.Text = theme.Title;
				tv.Items.Add(lvi);
			}
		}
	}
}
