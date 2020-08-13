//
//  ZIPExtraDataField.cs - describes information for an extra data field in a ZIP archive
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

namespace UniversalEditor.DataFormats.FileSystem.ZIP
{
	/// <summary>
	/// Describes information for an extra data field in a ZIP archive.
	/// </summary>
	public class ZIPExtraDataField
	{
		public class ZIPExtraDataFieldCollection
			: System.Collections.ObjectModel.Collection<ZIPExtraDataField>
		{
		}

		public byte[] LocalData
		{
			get { return GetLocalDataInternal(); }
			set { SetLocalDataInternal(value); }
		}
		public byte[] CentralData
		{
			get { return GetCentralDataInternal(); }
			set { SetCentralDataInternal(value); }
		}

		private byte[] _LocalData = new byte[0];
		protected virtual byte[] GetLocalDataInternal()
		{
			return _LocalData;
		}
		protected virtual void SetLocalDataInternal(byte[] value)
		{
			_LocalData = value;
		}

		private byte[] _CentralData = new byte[0];
		protected virtual byte[] GetCentralDataInternal()
		{
			return _CentralData;
		}
		protected virtual void SetCentralDataInternal(byte[] value)
		{
			_CentralData = value;
		}

		public short TypeCode { get; set; }
		public ZIPExtraDataFieldType Type { get { return (ZIPExtraDataFieldType)TypeCode; } set { TypeCode = (short)value; } }
	}
}
