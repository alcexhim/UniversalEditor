//
//  TransportPlugin.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using MBS.Framework;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Dialogs;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.Plugins.Transport
{
	public class TransportPlugin : UserInterfacePlugin
	{
		public TransportPlugin()
		{
			ID = new Guid("{02e071ec-b2cd-4a72-999f-3b626e06d609}");
			Title = "Transport Plugin";
			Context = new UIContext(ID, "Transport Plugin");

			cbTransport = new CommandBar("cbTransport", "Transport", new CommandItem[]
			{
				new CommandReferenceCommandItem("Transport_Rewind"),
				new CommandReferenceCommandItem("Transport_Back"),
				new CommandReferenceCommandItem("Transport_Play"),
				new CommandReferenceCommandItem("Transport_Forward"),
				new CommandReferenceCommandItem("Transport_End")
			});
		}

		private CommandBar cbTransport = null;
		private CommandReferenceCommandItem miTransport = null;

		void Handle_EditorChanging(object sender, EditorChangingEventArgs e)
		{
			if (e.CurrentEditor == null)
				return;

			Type t = e.CurrentEditor.GetType();
			System.Reflection.PropertyInfo piTransport = t.GetProperty("Transport", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
			if (piTransport != null && piTransport.PropertyType == typeof(MBS.Audio.ITransport))
			{
				if (!e.CurrentEditor.Context.CommandBars.Contains(cbTransport))
				{
					e.CurrentEditor.Context.CommandBars.Add(cbTransport);
				}

				if (!e.CurrentEditor.Context.MenuItems.Contains(miTransport))
				{
					e.CurrentEditor.Context.MenuItems.Add(miTransport);
				}
			}
		}


		protected override void InitializeInternal()
		{
			base.InitializeInternal();

			Context.Commands.Add(new Command("Transport_Rewind", "_Rewind"));
			Context.Commands.Add(new Command("Transport_Back", "_Back"));
			Context.Commands.Add(new Command("Transport_Play", "_Play"));
			Context.Commands.Add(new Command("Transport_Forward", "_Forward"));
			Context.Commands.Add(new Command("Transport_End", "_End"));
			Context.Commands.Add(new Command("Transport_Transport", "P_layback", new CommandItem[]
			{
				new CommandReferenceCommandItem("Transport_Rewind"),
				new CommandReferenceCommandItem("Transport_Back"),
				new CommandReferenceCommandItem("Transport_Play"),
				new CommandReferenceCommandItem("Transport_Forward"),
				new CommandReferenceCommandItem("Transport_End"),
			}));

			Context.AttachCommandEventHandler("Transport_Rewind", Transport_Rewind);
			Context.AttachCommandEventHandler("Transport_Back", Transport_Back);
			Context.AttachCommandEventHandler("Transport_Play", Transport_Play);
			Context.AttachCommandEventHandler("Transport_Forward", Transport_Forward);
			Context.AttachCommandEventHandler("Transport_End", Transport_End);

			((EditorApplication)Application.Instance).EditorChanging += Handle_EditorChanging;


			miTransport = new CommandReferenceCommandItem("Transport_Transport");
			miTransport.InsertAfterID = "Project";
		}

		private void Transport_End(object sender, EventArgs e)
		{
			Editor ed = (((UIApplication)Application.Instance).CurrentWindow as UniversalEditor.UserInterface.MainWindow)?.GetCurrentEditor();
			Type t = ed?.GetType();
			System.Reflection.PropertyInfo piTransport = t?.GetProperty("Transport", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
			MBS.Audio.ITransport transport = (piTransport?.GetValue(ed, null) as MBS.Audio.ITransport);

			if (transport == null)
			{
				MessageDialog.ShowDialog("Transport unavailable", "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
				return;
			}

		}

		private void Transport_Forward(object sender, EventArgs e)
		{
			Editor ed = (((UIApplication)Application.Instance).CurrentWindow as UniversalEditor.UserInterface.MainWindow)?.GetCurrentEditor();
			Type t = ed?.GetType();
			System.Reflection.PropertyInfo piTransport = t?.GetProperty("Transport", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
			MBS.Audio.ITransport transport = (piTransport?.GetValue(ed, null) as MBS.Audio.ITransport);

			if (transport == null)
			{
				MessageDialog.ShowDialog("Transport unavailable", "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
				return;
			}

		}

		private void Transport_Play(object sender, EventArgs e)
		{
			Editor ed = (((UIApplication)Application.Instance).CurrentWindow as UniversalEditor.UserInterface.MainWindow)?.GetCurrentEditor();
			Type t = ed?.GetType();
			System.Reflection.PropertyInfo piTransport = t?.GetProperty("Transport", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
			MBS.Audio.ITransport transport = (piTransport?.GetValue(ed, null) as MBS.Audio.ITransport);

			if (transport == null)
			{
				MessageDialog.ShowDialog("Transport unavailable", "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
				return;
			}

			transport.Play();
		}

		private void Transport_Back(object sender, EventArgs e)
		{
			Editor ed = (((UIApplication)Application.Instance).CurrentWindow as UniversalEditor.UserInterface.MainWindow)?.GetCurrentEditor();
			Type t = ed?.GetType();
			System.Reflection.PropertyInfo piTransport = t?.GetProperty("Transport", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
			MBS.Audio.ITransport transport = (piTransport?.GetValue(ed, null) as MBS.Audio.ITransport);

			if (transport == null)
			{
				MessageDialog.ShowDialog("Transport unavailable", "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
				return;
			}


		}

		private void Transport_Rewind(object sender, EventArgs e)
		{
			Editor ed = (((UIApplication)Application.Instance).CurrentWindow as UniversalEditor.UserInterface.MainWindow)?.GetCurrentEditor();
			Type t = ed?.GetType();
			System.Reflection.PropertyInfo piTransport = t?.GetProperty("Transport", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
			MBS.Audio.ITransport transport = (piTransport?.GetValue(ed, null) as MBS.Audio.ITransport);

			if (transport == null)
			{
				MessageDialog.ShowDialog("Transport unavailable", "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
				return;
			}

			transport.Stop();
		}
	}
}
