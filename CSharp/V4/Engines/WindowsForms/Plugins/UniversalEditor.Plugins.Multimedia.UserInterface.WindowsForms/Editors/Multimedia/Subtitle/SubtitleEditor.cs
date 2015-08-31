using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.UserInterface.WindowsForms;
using UniversalEditor.ObjectModels.Multimedia.Subtitle;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.Multimedia.Subtitle
{
	public partial class SubtitleEditor : Editor
	{
		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(SubtitleObjectModel));
			}
			return _er;
		}
		public SubtitleEditor()
		{
			InitializeComponent();
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			lvStyles.Items.Clear();
			lvActors.Items.Clear();
			lvEvents.Items.Clear();

			SubtitleObjectModel subtitle = (ObjectModel as SubtitleObjectModel);
			if (subtitle == null) return;

			foreach (Style style in subtitle.Styles)
			{
				ListViewItem lvi = new ListViewItem();
				lvi.Text = style.Name;
				lvi.Tag = style;
				lvStyles.Items.Add(lvi);
			}

			foreach (Actor actor in subtitle.Actors)
			{
				ListViewItem lvi = new ListViewItem();
				lvi.Text = actor.Name;
				lvi.Tag = actor;
				lvStyles.Items.Add(lvi);
			}

			foreach (Event evt in subtitle.Events)
			{
				ListViewItem lvi = new ListViewItem();
				if (evt.Actor != null)
				{
					lvi.Text = evt.Actor.Name;
				}
				lvi.SubItems.Add(evt.StartTimestamp.ToString());
				lvi.SubItems.Add(evt.EndTimestamp.ToString());
				lvi.SubItems.Add((evt.EndTimestamp - evt.StartTimestamp).ToString());
				lvi.SubItems.Add(evt.Text);
				lvEvents.Items.Add(lvi);
			}
		}
	}
}
