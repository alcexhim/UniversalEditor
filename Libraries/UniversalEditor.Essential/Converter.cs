//
//  Converter.cs - provides a structured mechanism for converting from one ObjectModel to another
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
using System.Collections.Generic;

namespace UniversalEditor
{
	public struct ConverterCapability
	{
		public Type from;
		public Type to;
		public ConverterCapability(Type from, Type to)
		{
			this.from = from;
			this.to = to;
		}
	}
	public class ConverterCapabilityCollection
	{
		private List<ConverterCapability> caps = new List<ConverterCapability>();

		public void Add(Type from, Type to)
		{
			ConverterCapability cap = new ConverterCapability(from, to);
			caps.Add(cap);
		}
		public bool Contains(Type from, Type to)
		{
			ConverterCapability cap = new ConverterCapability(from, to);
			return caps.Contains(cap);
		}
		public bool Remove(Type from, Type to)
		{
			ConverterCapability cap = new ConverterCapability(from, to);
			if (caps.Contains(cap))
			{
				caps.Remove(cap);
				return true;
			}
			return false;
		}
	}
	public class ConverterReference : ReferencedBy<Converter>
	{
		private Type mvarType = null;
		public Type Type { get { return mvarType; } set { mvarType = value; } }

		private ConverterCapabilityCollection mvarCapabilities = new ConverterCapabilityCollection();
		public ConverterCapabilityCollection Capabilities { get { return mvarCapabilities; } }

		public ConverterReference(Type type)
		{
			if (!type.IsSubclassOf(typeof(Converter)))
			{
				throw new InvalidCastException("Cannot create a converter reference to a non-Converter type");
			}
			else if (type.IsAbstract)
			{
				throw new InvalidOperationException("Cannot create a converter reference to an abstract type");
			}

			mvarType = type;
		}

		public Converter Create()
		{
			if (mvarType != null) return (Converter)mvarType.Assembly.CreateInstance(mvarType.FullName);
			return null;
		}

		public string[] GetDetails()
		{
			throw new NotImplementedException();
		}

		public bool ShouldFilterObject(string filter)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>
	/// Provides a structured mechanism for converting from one <see cref="ObjectModel" /> to another.
	/// </summary>
	public abstract class Converter : References<ConverterReference>
	{
		public abstract void Convert(ObjectModel from, ObjectModel to);

		public ConverterReference MakeReference()
		{
			return MakeReferenceInternal();
		}
		protected virtual ConverterReference MakeReferenceInternal()
		{
			ConverterReference _cr = new ConverterReference(this.GetType());
			return _cr;
		}
	}
}
