﻿//
//  IcarusTextDataFormat.cs - provides a DataFormat for handling ICARUS script files in plain-text format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Icarus;
using UniversalEditor.ObjectModels.Icarus.Commands;
using UniversalEditor.ObjectModels.Icarus.Expressions;
using UniversalEditor.ObjectModels.Icarus.Parameters;
using UniversalEditor.UserInterface;

namespace UniversalEditor.DataFormats.Icarus
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for handling ICARUS script files in plain-text format.
	/// </summary>
	public class IcarusTextDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(IcarusScriptObjectModel), DataFormatCapabilities.All);
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            IcarusScriptObjectModel script = (objectModel as IcarusScriptObjectModel);
            if (script == null) throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;

			string signatureLine = reader.ReadLine();
			if (!signatureLine.StartsWith("//Generated by BehavEd"))
			{
				// we may or may not wish to throw InvalidDataFormatException here;
				// since it's plain text we could still continue reading it like normal but all hell could break loose if we encounter something weird
				(Application.Instance as IHostApplication).Messages.Add(HostApplicationMessageSeverity.Warning, "This file has NOT ben written by a BehavEd-compatible program! This can mess up big-style if you load a programmer-script with nested statements, etc.", Accessor.GetFileName());
			}

			while (!reader.EndOfStream)
			{
				IcarusCommand cmd = ReadCommand(reader);
				script.Commands.Add(cmd);
			}
        }

		private IcarusCommand ReadCommand(Reader reader)
		{
			string line = null;
			while (!reader.EndOfStream)
			{
				line = reader.ReadLine();
				line = line.Trim();

				if (String.IsNullOrEmpty(line)) continue;
				if (line.StartsWith("//") && !line.StartsWith("//$") && !line.StartsWith("//(BHVDREM)")) continue;
				break;
			}

			if (line.StartsWith("//$"))
			{
				// this is a group marker, format //$"group-name"@num-items
				string[] groupDef = line.Substring(3).Split(new char[] { '@' });
				if (groupDef.Length == 2)
				{
					if (groupDef[0].StartsWith("\"") && groupDef[0].EndsWith("\""))
					{
						string groupName = groupDef[0].Substring(1, groupDef[0].Length - 2);
						int groupItemCount = 0;
						if (Int32.TryParse(groupDef[1], out groupItemCount))
						{
							IcarusCommandMacro macro = new IcarusCommandMacro();
							macro.MacroName = groupName;

							for (int i = 0; i < groupItemCount; i++)
							{
								IcarusCommand cmd = ReadCommand(reader);
								macro.Commands.Add(cmd);
							}

							return macro;
						}
					}
				}
			}
			else
			{
				bool commented = false;
				if (line.StartsWith("//(BHVDREM)"))
				{
					commented = true;
					line = line.Substring(11);
				}

				int indexOfParen = FindToken(reader, ref line, '(');

				string funcName = line.Substring(0, line.IndexOf('('));
				funcName = funcName.Trim();

				indexOfParen = FindToken(reader, ref line, ')');
				int start = line.IndexOf('(') + 1;
				string parmList = line.Substring(start, indexOfParen - start);
				parmList = parmList.Trim();

				string[] parms = parmList.Split(new string[] { "," }, "\"", "\"", StringSplitOptions.None, -1, false);

				IcarusCommand cmd = IcarusCommand.CreateFromName(funcName);
				// TODO: handle tabs within name
				if (cmd == null) return null;

				cmd.IsCommented = commented;

				for (int i = 0; i < parms.Length; i++)
				{
					string parm = parms[i];
					string comment = null;
					if (parm.Contains("/*"))
					{
						int comment_start = parm.IndexOf("/*");
						int comment_end = parm.IndexOf("*/");
						comment = parm.Substring(comment_start + 2, comment_end - comment_start - 2);
						parm = parm.Substring(0, comment_start) + parm.Substring(comment_end + 2);
					}
					parm = parm.Trim();

					IcarusExpression expr = IcarusExpression.Parse(parm);
					if (i < cmd.Parameters.Count)
					{
						cmd.Parameters[i].Value = expr;
					}
					else
					{
						cmd.Parameters.Add(new IcarusGenericParameter(null, expr));
					}

					if (comment != null)
					{
						if (comment == "!")
						{
							cmd.Parameters[i].ReadOnly = true;
						}
						else if (comment.StartsWith("@"))
						{
							cmd.Parameters[i].EnumerationName = comment.Substring(1);
						}
					}
				}
				return cmd;
			}
			return null;
		}

		private int FindToken(Reader reader, ref string line, char token)
		{
			int indexOfQuote = line.IndexOf('"');
			int indexOfNextQuote = line.IndexOf('"', indexOfQuote + 1);
			int index = line.IndexOf(token);

			while (index == -1)
			{
				// try the next line
				if (reader.EndOfStream) return -1;

				line = line + reader.ReadLine();
				index = line.IndexOf(token);
			}

			while (index != -1 && (index > indexOfQuote && index < indexOfNextQuote))
			{
				// we are inside quotes, so ignore it and move to the next occurrence
				index = line.IndexOf(token, index + 1);

				indexOfQuote = line.IndexOf('"', index);
				indexOfNextQuote = line.IndexOf('"', indexOfQuote + 1);
			}
			return index;
		}

		protected override void SaveInternal(ObjectModel objectModel)
        {
            IcarusScriptObjectModel script = (objectModel as IcarusScriptObjectModel);
            if (script == null) throw new ObjectModelNotSupportedException();

            Writer tw = base.Accessor.Writer;
            tw.WriteLine("//Generated by BehavEd");
			tw.WriteLine();

            foreach (IcarusCommand command in script.Commands)
            {
                WriteCommand(tw, command);
            }
        }

        private void WriteCommand(Writer tw, IcarusCommand command, int indentLength = 0)
        {
            string indent = new string(' ', indentLength * 4);

			if (command is IcarusPredefinedCommand)
			{
				IcarusPredefinedCommand cmd = (command as IcarusPredefinedCommand);
				if (cmd.IsCommented)
				{
					tw.Write("//(BHVDREM)  ");
				}

				if (cmd is IcarusCommandMacro)
				{
					IcarusCommandMacro macro = (cmd as IcarusCommandMacro);
					tw.WriteLine(String.Format("//$\"{0}\"@{1}", macro.MacroName, macro.Commands.Count));
					for (int i = 0; i < macro.Commands.Count; i++)
					{
						WriteCommand(tw, macro.Commands[i]);
					}
				}
				else
				{
					tw.Write(cmd.Name);
					tw.Write(" ( ");

					for (int i = 0; i < cmd.Parameters.Count; i++)
					{
						if (cmd.Parameters[i].ReadOnly)
						{
							tw.Write("/*!*/ ");
						}
						else if (cmd.Parameters[i].EnumerationName != null)
						{
							tw.Write(String.Format("/*@{0}*/ ", cmd.Parameters[i].EnumerationName));
						}
						tw.Write(cmd.Parameters[i].Value?.ToString());
						if (i < cmd.Parameters.Count - 1)
						{
							tw.Write(", ");
						}
					}

					tw.Write(" );");
				}
				tw.WriteLine();
			}
        }
    }
}
