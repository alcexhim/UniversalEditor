using System;
using Gtk;

using UniversalEditor.UserInterface;

public partial class MainWindow: Gtk.Window
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
}
