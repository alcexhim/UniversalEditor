//
//  GTKNativeTreeModel.cs
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
namespace MBS.Framework.UserInterface.Engines.GTK
{
	public class GTKNativeTreeModel : NativeTreeModel, IComparable<GTKNativeTreeModel>, IEquatable<GTKNativeTreeModel>
	{
		public IntPtr Handle { get; private set; } = IntPtr.Zero;
		public GTKNativeTreeModel(IntPtr handle)
		{
			Handle = handle;
		}

		public override bool Equals(object obj)
		{
			if ((object)obj == null && (object)this == null)
				return true;
			if ((object)obj == null || (object)this == null)
				return false;

			if (obj is GTKNativeTreeModel)
				return Equals(obj as GTKNativeTreeModel);

			return false;
		}

		public override int GetHashCode()
		{
			return this.Handle.GetHashCode();
		}

		public int CompareTo(GTKNativeTreeModel other)
		{
			return this.Handle.ToInt64().CompareTo(other.Handle.ToInt64());
		}

		public bool Equals(GTKNativeTreeModel other)
		{
			return Handle.Equals(other.Handle);
		}

		public static bool operator ==(GTKNativeTreeModel left, GTKNativeTreeModel right)
		{
			if ((object)left == null && (object)right == null)
				return true;
			if ((object)left == null || (object)right == null)
				return false;

			return left.Equals(right);
		}
		public static bool operator !=(GTKNativeTreeModel left, GTKNativeTreeModel right)
		{
			return !left.Equals(right);
		}

		public override string ToString()
		{
			return Handle.ToString();
		}
	}
}
