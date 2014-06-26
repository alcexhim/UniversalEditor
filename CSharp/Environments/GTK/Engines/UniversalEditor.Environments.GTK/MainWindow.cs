using System;
using Gtk;

using UniversalEditor.UserInterface;

public partial class MainWindow: Gtk.Window, IHostApplicationWindow
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		InitializeMenuBar();
	}
	
	private void InitializeMenuBar()
	{
		foreach (CommandItem item in Engine.CurrentEngine.MainMenu.Items)
		{
			CreateCommandItem(item, null);
		}
		menubar1.ShowAll ();
	}
	private void CreateCommandItem(CommandItem item, Menu parentMenu)
	{
		Gtk.MenuItem menuItem = null;
		
		if (item is CommandReferenceCommandItem)
		{
			CommandReferenceCommandItem crci = (item as CommandReferenceCommandItem);
			Command cmd = Engine.CurrentEngine.Commands[crci.CommandID];
			if (cmd == null)
			{
				HostApplication.Messages.Add(HostApplicationMessageSeverity.Warning, "The command '" + crci.CommandID + "' was not found");
				return;
			}
			
			menuItem = new Gtk.MenuItem(cmd.Title);
			if (cmd.Items.Count > 0)
			{
				Menu submenu = CreateCommandItemSubmenu(cmd);
				menuItem.Submenu = submenu;
			}
		}
		else if (item is SeparatorCommandItem)
		{
			menuItem = new Gtk.SeparatorMenuItem();
		}
		
		if (menuItem != null)
		{
			if (parentMenu == null)
			{
				menubar1.Append(menuItem);
			}
			else
			{
				parentMenu.Append(menuItem);
			}
		}
	}
	private Menu CreateCommandItemSubmenu(Command cmd)
	{
		Menu menu = new Menu();
		if (Engine.CurrentEngine.MainMenu.EnableTearoff && cmd.EnableTearoff)
		{
			menu.Append(new TearoffMenuItem());
		}
		foreach (CommandItem item in cmd.Items)
		{
			CreateCommandItem(item, menu);
		}
		return menu;
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	#region IHostApplicationWindow implementation
	public event EventHandler WindowClosed;

	public void NewFile ()
	{
		throw new System.NotImplementedException ();
	}

	public void NewProject (bool combineObjects)
	{
		throw new System.NotImplementedException ();
	}

	public void OpenFile ()
	{
		throw new System.NotImplementedException ();
	}

	public void OpenFile (params string[] FileNames)
	{
		throw new System.NotImplementedException ();
	}

	public void OpenProject (bool combineObjects)
	{
		throw new System.NotImplementedException ();
	}

	public void OpenProject (string FileName, bool combineObjects)
	{
		throw new System.NotImplementedException ();
	}

	public void SaveFile ()
	{
		throw new System.NotImplementedException ();
	}

	public void SaveFileAs ()
	{
		throw new System.NotImplementedException ();
	}

	public void SaveFileAs (string FileName, UniversalEditor.DataFormat df)
	{
		throw new System.NotImplementedException ();
	}

	public void SaveProject ()
	{
		throw new System.NotImplementedException ();
	}

	public void SaveProjectAs ()
	{
		throw new System.NotImplementedException ();
	}

	public void SaveProjectAs (string FileName, UniversalEditor.DataFormat df)
	{
		throw new System.NotImplementedException ();
	}

	public void SaveAll ()
	{
		throw new System.NotImplementedException ();
	}

	public void CloseFile ()
	{
		throw new System.NotImplementedException ();
	}

	public void CloseWindow ()
	{
		throw new System.NotImplementedException ();
	}

	public bool ShowOptionsDialog ()
	{
		throw new System.NotImplementedException ();
	}

	public void ToggleMenuItemEnabled (string menuItemName, bool enabled)
	{
		throw new System.NotImplementedException ();
	}

	public void RefreshCommand (object nativeCommandObject)
	{
		throw new System.NotImplementedException ();
	}

	public void UpdateStatus (string statusText)
	{
		throw new System.NotImplementedException ();
	}

	public void UpdateProgress (bool visible)
	{
		throw new System.NotImplementedException ();
	}

	public void UpdateProgress (int minimum, int maximium, int value)
	{
		throw new System.NotImplementedException ();
	}

	public void ActivateWindow ()
	{
		throw new System.NotImplementedException ();
	}
	#endregion
}
