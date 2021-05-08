//
//  ExportTableEntry.cs - represents an entry in the export table of an Unreal Engine package file
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
using System.Text;

namespace UniversalEditor.ObjectModels.UnrealEngine
{
	/// <summary>
	/// Represents an entry in the export table of an Unreal Engine package file.
	/// </summary>
	public class ExportTableEntry : ICloneable
	{
		public class ExportTableEntryCollection
			: System.Collections.ObjectModel.Collection<ExportTableEntry>
		{
		}

		internal Accessor _acc = null;

		public event FileSystem.DataRequestEventHandler DataRequest;
		protected virtual void OnDataRequest(FileSystem.DataRequestEventArgs e)
		{
			DataRequest?.Invoke(this, e);
		}

		private byte[] _Data = null;
		public void SetData(byte[] data)
		{
			_Data = data;
		}

		public byte[] GetData()
		{
			if (_Data != null) return _Data;

			FileSystem.DataRequestEventArgs e = new FileSystem.DataRequestEventArgs();
			OnDataRequest(e);
			return e.Data;
		}

		/// <summary>
		/// Gets or sets the name of this <see cref="ExportTableEntry" /> as a <see cref="NameTableEntry" /> reference.
		/// </summary>
		/// <value>The name of this <see cref="ExportTableEntry" />.</value>
		public NameTableEntry Name { get; set; } = null;
		/// <summary>
		/// Gets or sets the class of the object exported by this <see cref="ExportTableEntry" />.
		/// </summary>
		/// <value>The class of the object exported by this <see cref="ExportTableEntry" />.</value>
		public ObjectReference ObjectClass { get; set; } = null;
		/// <summary>
		/// Gets or sets the parent class of the object exported by this <see cref="ExportTableEntry" />.
		/// </summary>
		/// <value>The parent class of the object exported by this <see cref="ExportTableEntry" />.</value>
		public ObjectReference ObjectParent { get; set; } = null;
		/// <summary>
		/// Gets or sets the group of the object exported by this <see cref="ExportTableEntry" />.
		/// </summary>
		/// <value>The group of the object exported by this <see cref="ExportTableEntry" />.</value>
		public ObjectReference Group { get; set; } = null;

		public long GetDataLength()
		{
			if (_Data != null)
				return _Data.Length;
			return (GetData()?.Length).GetValueOrDefault(0);
		}

		/// <summary>
		/// Gets or sets the attributes for the object exported by this <see cref="ExportTableEntry" />.
		/// </summary>
		/// <value>The attributes for the object exported by this <see cref="ExportTableEntry" />.</value>
		public ObjectFlags Flags { get; set; } = ObjectFlags.None;
		/// <summary>
		/// Gets or sets the size in bytes of the object exported by this <see cref="ExportTableEntry" />.
		/// </summary>
		/// <value>The size in bytes of the object exported by this <see cref="ExportTableEntry" />.</value>
		public int Size { get; set; } = 0;
		/// <summary>
		/// Gets or sets the offset in the package file for the object exported by this <see cref="ExportTableEntry" />.
		/// </summary>
		/// <value>The offset in the package file for the object exported by this <see cref="ExportTableEntry" />.</value>
		public int Offset { get; set; } = 0;

		public object Clone()
		{
			ExportTableEntry clone = new ExportTableEntry();
			return clone;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (Group != null)
			{
				sb.Append(Group.ToString());
				sb.Append('.');
			}
			if (Name == null)
			{
				sb.Append("(invalid name)");
			}
			else
			{
				sb.Append(Name.ToString(false));
			}
			return sb.ToString();
		}
	}
}
