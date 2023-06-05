//
//  CoreObjectDataFormat.cs - provides a DataFormat for manipulating Core Object File Format documents, such as vCard and vCalendar
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
using UniversalEditor.ObjectModels.CoreObject;

namespace UniversalEditor.DataFormats.CoreObject
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> that can be used either standalone or as the base class for <see cref="DataFormat"/>s built on the Core Object File Format,
	/// such as vCard and vCalendar.
	/// </summary>
	public class CoreObjectDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(CoreObjectObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			CoreObjectObjectModel core = (objectModel as CoreObjectObjectModel);
			if (core == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;

			CoreObjectGroup currentGroup = null;

			string lastLine = null;
			reader.Seek(0, SeekOrigin.Begin);

			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine();
				if (line.StartsWith(" "))
				{
					if (lastLine == null)
					{
						throw new InvalidDataFormatException("Cannot continue a property that hasn't been declared");
					}
					line = line.Substring(1);
					line = line.Replace("\\,", ",");
					lastLine += line;
					continue;
				}
				else
				{
					if (lastLine != null)
					{
						// parse the last line
						if (lastLine.Contains(":"))
						{
							string[] splits = lastLine.Split(new char[] { ':' }, 2, StringSplitOptions.None);
							if (splits[0].ToUpper() == "BEGIN")
							{
								if (currentGroup != null)
								{
									currentGroup = currentGroup.Groups.Add(splits[1]);
								}
								else
								{
									currentGroup = core.Groups.Add(splits[1]);
								}
							}
							else if (splits[0].ToUpper() == "END")
							{
								currentGroup = currentGroup.ParentGroup;
							}
							else
							{
								if (splits.Length > 0)
								{
									CoreObjectProperty prop = new CoreObjectProperty();

									string name = null;
									if (splits[0].Contains(";"))
									{
										string[] attrs = splits[0].Split(new char[] { ';' });
										name = attrs[0];

										for (int i = 1; i < attrs.Length; i++)
										{
											string[] attrValues = attrs[i].Split(new char[] { '=' });
											if (attrValues.Length > 0)
											{
												CoreObjectAttribute att = new CoreObjectAttribute();
												att.Name = attrValues[0];
												if (attrValues.Length > 1)
												{
													string[] values = attrValues[1].Split(new char[] { ',' }, "\"");
													for (int j = 0; j < values.Length; j++)
													{
														att.Values.Add(values[j]);
													}
												}
												prop.Attributes.Add(att);
											}
										}
									}
									else
									{
										name = splits[0];
									}

									prop.Name = name;
									if (splits.Length > 1)
									{
										string[] values = splits[1].Split(new char[] { ';' });
										foreach (string value in values)
										{
											prop.Values.Add(value);
										}
									}

									if (currentGroup != null)
									{
										currentGroup.Properties.Add(prop);
									}
									else
									{
										core.Properties.Add(prop);
									}
								}
							}
						}
						lastLine = line;
					}
					else
					{
						lastLine = line;
					}
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			CoreObjectObjectModel plom = (objectModel as CoreObjectObjectModel);
			if (plom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			foreach (CoreObjectProperty property in plom.Properties)
			{
				WriteProperty(writer, property);
			}
			foreach (CoreObjectGroup group in plom.Groups)
			{
				WriteGroup(writer, group);
			}
			writer.Flush();
		}

		private void WriteGroup(Writer writer, CoreObjectGroup group, int indent = 0)
		{
			writer.WriteLine("BEGIN:" + group.Name);
			foreach (CoreObjectProperty property in group.Properties)
			{
				WriteProperty(writer, property, indent + 1);
			}
			foreach (CoreObjectGroup group1 in group.Groups)
			{
				WriteGroup(writer, group1);
			}
			writer.WriteLine("END:" + group.Name);
		}

		private void WriteProperty(Writer writer, CoreObjectProperty property, int indent = 0)
		{
			writer.Write(property.Name);
			foreach (CoreObjectAttribute att in property.Attributes)
			{
				writer.Write(';');
				writer.Write(att.Name);
				if (att.Values.Count > 0)
				{
					writer.Write('=');
					for (int i = 0; i < att.Values.Count; i++)
					{
						writer.Write(att.Values[i]);
						if (i < att.Values.Count - 1) writer.Write(',');
					}
				}
			}
			writer.Write(':');
			for (int i = 0; i < property.Values.Count; i++)
			{
				writer.Write(property.Values[i]);
				if (i < property.Values.Count - 1) writer.Write(';');
			}
			writer.WriteLine();
		}
	}
}
