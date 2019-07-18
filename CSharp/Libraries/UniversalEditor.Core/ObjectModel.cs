//
//  ObjectModel.cs - stores user-friendly, DataFormat-agnostic in-memory representation
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor
{
	public abstract class ObjectModel : ICloneable, References<ObjectModelReference>
	{
		public class ObjectModelCollection
			: System.Collections.ObjectModel.Collection<ObjectModel>
		{

		}

		public ObjectModelReference MakeReference()
		{
			ObjectModelReference omr = MakeReferenceInternal();
			ObjectModelReference.Register(omr);
			return omr;
		}
		protected virtual ObjectModelReference MakeReferenceInternal()
		{
			ObjectModelReference omr = new ObjectModelReference(GetType());
			return omr;
		}

		private Accessor mvarAccessor = null;
		public Accessor Accessor { get { return mvarAccessor; } internal set { mvarAccessor = value; } }

		public abstract void Clear();
		public abstract void CopyTo(ObjectModel where);
		public void CopyTo(ObjectModel where, bool append)
		{
			if (!append) where.Clear();
			CopyTo(where);
		}
		public void CopyFrom(ObjectModel where)
		{
			where.CopyTo(this);
		}
		public void CopyFrom(ObjectModel where, bool append)
		{
			if (!append) Clear();
			CopyFrom(where);
		}
		public object Clone()
		{
			Type type = this.GetType();
			ObjectModel clone = (type.Assembly.CreateInstance(type.FullName) as ObjectModel);
			CopyTo(clone);
			return clone;
		}

		public virtual void Replace(string FindWhat, string ReplaceWith)
		{
			Type type = GetType();
			System.Reflection.PropertyInfo[] pis = type.GetProperties(System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
			foreach (System.Reflection.PropertyInfo pi in pis)
			{
				object obj = pi.GetValue(this, null);
				if (obj is string)
				{
					string str = (obj as string);
					pi.SetValue(this, str.Replace(FindWhat, ReplaceWith), null);
				}
			}
		}

		private Dictionary<DataFormatReference, Dictionary<string, object>> _customProperties = new Dictionary<DataFormatReference, Dictionary<string, object>>();
		public T GetCustomProperty<T>(DataFormatReference dfr, string name, T defaultValue = default(T))
		{
			if (_customProperties.ContainsKey(dfr))
			{
				if (_customProperties[dfr].ContainsKey(name))
				{
					return (T)_customProperties[dfr][name];
				}
			}
			return defaultValue;
		}
		public object GetCustomProperty(DataFormatReference dfr, string name, object defaultValue = null)
		{
			return GetCustomProperty<object>(dfr, name, defaultValue);
		}
		public void SetCustomProperty<T>(DataFormatReference dfr, string name, T value)
		{
			if (!_customProperties.ContainsKey(dfr))
			{
				_customProperties.Add(dfr, new Dictionary<string, object>());
			}
			_customProperties[dfr][name] = value;
		}
		public void SetCustomProperty(DataFormatReference dfr, string name, object value)
		{
			SetCustomProperty<object>(dfr, name, value);
		}
	}
}
