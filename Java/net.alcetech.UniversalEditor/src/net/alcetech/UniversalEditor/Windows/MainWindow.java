package net.alcetech.UniversalEditor.Windows;

import java.awt.BorderLayout;
import java.awt.Cursor;
import java.awt.MouseInfo;
import java.awt.Point;
import java.awt.event.*;

import javax.swing.*;

import net.alcetech.ApplicationFramework.*;
import net.alcetech.ApplicationFramework.CommandItems.*;
import net.alcetech.ApplicationFramework.UserInterface.*;
import net.alcetech.ApplicationFramework.UserInterface.Dialogs.*;
import net.alcetech.ApplicationFramework.UserInterface.Theming.ThemeManager;

public class MainWindow extends net.alcetech.ApplicationFramework.UserInterface.MainWindow
{
	// private CommandBarContainer commandBarContainer = new CommandBarContainer();
	// private DockingWindowContainer dwc = new DockingWindowContainer();
	
	private void InitializeComponent()
	{
		// this.setIconImages(ThemeManager.GetThemedIconImages("MainIcon"));
		this.setSize(800, 600);
		this.setTitle("Universal Editor");
		
		// CommandBar cbMenuBar = new CommandBar();
		
		// this is a menu bar, so set the default display style to text only
		/*
		cbMenuBar.setDefaultCommandDisplayStyle(CommandDisplayStyle.TextOnly);
		
		cbMenuBar.addItem(new CommandReferenceCommandItem("mnuFile"));
		cbMenuBar.addItem(new CommandReferenceCommandItem("mnuEdit"));
		cbMenuBar.addItem(new CommandReferenceCommandItem("mnuView"));
		cbMenuBar.addItem(new CommandReferenceCommandItem("mnuProject"));
		cbMenuBar.addItem(new CommandReferenceCommandItem("mnuBuild"));
		cbMenuBar.addItem(new CommandReferenceCommandItem("mnuDebug"));
		cbMenuBar.addItem(new CommandReferenceCommandItem("mnuTools"));
		cbMenuBar.addItem(new CommandReferenceCommandItem("mnuWindow"));
		cbMenuBar.addItem(new CommandReferenceCommandItem("mnuHelp"));
		
		this.commandBarContainer.add(cbMenuBar, BorderLayout.NORTH);
		
		this.commandBarContainer.add(dwc, BorderLayout.CENTER);
		
		this.add(this.commandBarContainer);
		
		dwc.getWindowCollection().add("Start Page", new JTextArea());
		dwc.getWindowCollection().add("X11R2", new JButton());
		*/
	}
	
	public MainWindow()
	{
		InitializeComponent();
	}
	
	protected void OnClosing(CancelEventArgs e)
	{
		DialogResult result = MessageDialog.ShowDialog(this, "You have unsaved changes. Do you wish to save your changes before closing this window?", "Close Program", StockButtonType.YES_NO_CANCEL);
		switch (result)
		{
			case YES:
			{
				break;
			}
			case NO:
			{
				break;
			}
			case CANCEL:
			{
				e.cancel();
				break;
			}
		}
	}
}
