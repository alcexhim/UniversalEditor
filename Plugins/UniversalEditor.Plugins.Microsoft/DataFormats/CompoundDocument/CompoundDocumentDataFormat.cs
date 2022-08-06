//
//  CompoundDocumentDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
using MBS.Framework;
using MBS.Framework.Settings;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.FileSystem.Microsoft.CompoundDocument;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.CompoundDocument;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.DataFormats.CompoundDocument
{
	public class CompoundDocumentDataFormat : CompoundDocumentBaseDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());

				SettingsGroup sgSummaryInfo = new SettingsGroup(new string[] { "General", "Summary" }, new Setting[]
				{
					new TextSetting("Title", "_Title"),
					new TextSetting("Subject", "_Subject"),
					new TextSetting("Author", "_Author"),
					new TextSetting("Keywords", "_Keywords"),
					new TextSetting("Comments", "_Comments"),
					new TextSetting("AppName", "App name")
				});
				_dfr.ExportOptions.SettingsGroups.Add(sgSummaryInfo);
			}
			return _dfr;
		}

		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			CompoundDocumentObjectModel cdom = (objectModels.Pop() as CompoundDocumentObjectModel);

			File _CompObj = cdom.Folders["Root Entry"].Files["\u0001CompObj"];
			if (_CompObj != null)
			{
				byte[] _CompObj_data = _CompObj.GetData();
				MemoryAccessor ma = new MemoryAccessor(_CompObj_data);

				int reserved1 = ma.Reader.ReadInt32();
				int version = ma.Reader.ReadInt32();
				byte[] reserved2 = ma.Reader.ReadBytes(20);

				cdom.UserType = ReadString32(ma.Reader);
				object ansiClipboardFormat = ReadClipboardFormatOrAnsiString(ma.Reader);
				if (ansiClipboardFormat is uint)
				{
					cdom.ClipboardFormat = CompoundDocumentClipboardFormat.FromStandard((uint)ansiClipboardFormat);
				}
				else if (ansiClipboardFormat is string)
				{
					cdom.ClipboardFormat = CompoundDocumentClipboardFormat.FromString((string)ansiClipboardFormat);
				}

				cdom.AssociationTypeId = ReadString32(ma.Reader);
				uint unicodeMarker = ma.Reader.ReadUInt32();
				if (unicodeMarker != 0x71B239F4)
				{

				}
			}
			else
			{
				throw new InvalidDataFormatException();
			}

			File _SummaryInformation = cdom.Folders["Root Entry"].Files["\u0005SummaryInformation"];
			if (_SummaryInformation != null)
			{
				byte[] data = _SummaryInformation.GetData();
				MemoryAccessor ma = new MemoryAccessor(data);

				PropertyListObjectModel plom = new PropertyListObjectModel();
				Document.Load(plom, new SummaryInformation.SummaryInformationDataFormat(), ma);
			}

			// we need to remember to push it back onto the stack
			objectModels.Push(cdom);
		}

		private object ReadClipboardFormatOrAnsiString(Reader reader)
		{
			uint markerOrLength = reader.ReadUInt32();
			if (markerOrLength == 0)
			{
				return null;
			}
			else if (markerOrLength == 0xFFFFFFFF || markerOrLength == 0xFFFFFFFE)
			{
				uint format = reader.ReadUInt32();
				return format;
			}
			else
			{
				return reader.ReadFixedLengthString(markerOrLength).TrimNull();
			}
		}

		private string ReadString32(Reader reader)
		{
			uint length = reader.ReadUInt32();
			string value = reader.ReadFixedLengthString(length);
			return value.TrimNull();
		}

	}
}
