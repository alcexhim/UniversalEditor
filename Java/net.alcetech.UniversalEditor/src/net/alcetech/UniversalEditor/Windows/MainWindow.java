package net.alcetech.UniversalEditor.Windows;

import java.awt.BorderLayout;
import java.awt.Cursor;
import java.awt.MouseInfo;
import java.awt.Point;
import java.awt.event.*;

import javax.swing.*;

import net.alcetech.Core.*;
import net.alcetech.UserInterface.*;
import net.alcetech.UserInterface.Controls.*;
import net.alcetech.UserInterface.Theming.ThemeManager;

public class MainWindow extends Window
{
	private CommandBarContainer commandBarContainer = new CommandBarContainer();
	
	private DockingWindowContainer dwc = new DockingWindowContainer();
	
	private void InitializeComponent()
	{
		this.setIconImages(ThemeManager.GetThemedIconImages("MainIcon"));
		this.setSize(800, 600);
		this.setTitle("Universal Editor");
		
		CommandBar cbMenuBar = new CommandBar();
		
		// this is a menu bar, so set the default display style to text only
		cbMenuBar.setDefaultCommandDisplayStyle(CommandDisplayStyle.TextOnly);
		
		cbMenuBar.getCommandCollection().add(new CommandReferenceCommandItem("mnuFile"));
		cbMenuBar.getCommandCollection().add(new CommandReferenceCommandItem("mnuEdit"));
		cbMenuBar.getCommandCollection().add(new CommandReferenceCommandItem("mnuView"));
		cbMenuBar.getCommandCollection().add(new CommandReferenceCommandItem("mnuProject"));
		cbMenuBar.getCommandCollection().add(new CommandReferenceCommandItem("mnuBuild"));
		cbMenuBar.getCommandCollection().add(new CommandReferenceCommandItem("mnuDebug"));
		cbMenuBar.getCommandCollection().add(new CommandReferenceCommandItem("mnuTools"));
		cbMenuBar.getCommandCollection().add(new CommandReferenceCommandItem("mnuWindow"));
		cbMenuBar.getCommandCollection().add(new CommandReferenceCommandItem("mnuHelp"));
		
		this.commandBarContainer.add(cbMenuBar, BorderLayout.NORTH);
		
		this.commandBarContainer.add(dwc, BorderLayout.CENTER);
		
		this.add(this.commandBarContainer);
		
		dwc.getWindowCollection().add("Start Page", new JTextArea());
		dwc.getWindowCollection().add("X11R2", new JButton());
	}
	
	public MainWindow()
	{
		InitializeComponent();
	}
	
	protected void OnClosing(CancelEventArgs e)
	{
		MessageDialogResult result = MessageDialog.ShowDialog(this, "You have unsaved changes. Do you wish to save your changes before closing this window?", "Close Program", MessageDialogButtons.YesNoCancel);
		switch (result)
		{
			case Yes:
			{
				break;
			}
			case No:
			{
				break;
			}
			case Cancel:
			{
				e.cancel();
				break;
			}
		}
	}
}
