//
//  WindowsFormsCommandLine.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using System.Collections.Generic;

namespace MBS.Framework.UserInterface.Engines.WindowsForms
{
	public class WindowsFormsCommandLine : CommandLine
	{
		internal WindowsFormsCommandLine(string[] arguments) : base(arguments)
		{
			bool processFileList = false;
			for (int i = 0; i < Application.CommandLine.Options.Count; i++)
			{
				// reset the value of options
				Application.CommandLine.Options[i].Value = null;
			}

			for (int i = 1; i < arguments.Length; i++) // not off-by-one, first argument is full path to exe
			{
				if (!processFileList)
				{
					if (arguments[i].StartsWith("/", StringComparison.InvariantCulture) && arguments[i].Length > 2)
					{
						// expecting full name invocation
						string argname = arguments[i].Substring(1);
						if (Application.CommandLine.Options.Contains(argname))
						{
							CommandLineOption option = Application.CommandLine.Options[argname];
							if (option.Type != CommandLineOptionValueType.None && i < arguments.Length - 1)
							{
								if (option.Type == CommandLineOptionValueType.Multiple)
								{
									List<string> list = (option.Value as List<string>);
									if (list == null)
									{
										list = new List<string>();
										option.Value = list;
									}
									list.Add(arguments[i + 1]);
								}
								else
								{
									option.Value = arguments[i + 1];
								}
								i++;
							}
							else
							{
								option.Value = true;
							}
						}
					}
					else if (arguments[i].StartsWith("/", StringComparison.InvariantCulture) && arguments[i].Length == 2)
					{
						// expecting single-character invocation
						if (Application.CommandLine.Options.Contains(arguments[i].Substring(1)))
						{
						}
						else
						{
							Console.WriteLine("uwt: warning: specified switch '{0}' not found; ignoring", arguments[i]);
						}
					}
					else
					{
						Console.WriteLine("uwt: warning: specified argument '{0}' not found; assuming filename", arguments[i]);
						FileNames.Add(arguments[i]);

						// first file name after switches is file name list on windows
						processFileList = true;
					}
				}
				else
				{
					FileNames.Add(arguments[i]);
				}
			}
		}
	}
}
