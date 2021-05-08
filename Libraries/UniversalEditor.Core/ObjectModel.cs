//
//  ObjectModel.cs - stores user-friendly, DataFormat-agnostic in-memory representation
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
	/// <summary>
	/// The in-memory representation of data serialized to and from an <see cref="Accessor" /> using a particular <see cref="DataFormat" />.
	/// </summary>
	public abstract class ObjectModel : ICloneable, References<ObjectModelReference>
	{
		/// <summary>
		/// Represents a collection of <see cref="ObjectModel" /> objects.
		/// </summary>
		public class ObjectModelCollection
			: System.Collections.ObjectModel.Collection<ObjectModel>
		{

		}

		/// <summary>
		/// Creates a <see cref="ObjectModelReference" /> for this <see cref="ObjectModel" /> and registers it for future use.
		/// </summary>
		/// <returns>The <see cref="ObjectModelReference" /> that provides metadata and other information about this <see cref="ObjectModel" />.</returns>
		public ObjectModelReference MakeReference()
		{
			ObjectModelReference omr = MakeReferenceInternal();
			ObjectModelReference.Register(omr);
			return omr;
		}
		/// <summary>
		/// Creates a new <see cref="ObjectModelReference" />. The returned <see cref="ObjectModelReference" /> is not cached. It is recommended that subclasses
		/// override this method and cache their own personal instances of <see cref="ObjectModelReference" /> containing the appropriate metadata for their
		/// subclassed implementations.
		/// </summary>
		/// <returns>The <see cref="ObjectModelReference" /> that provides metadata and other information about this <see cref="ObjectModel" />.</returns>
		protected virtual ObjectModelReference MakeReferenceInternal()
		{
			ObjectModelReference omr = new ObjectModelReference(GetType());
			return omr;
		}

		/// <summary>
		/// The <see cref="Accessor" /> that was last used to read or write this <see cref="ObjectModel" />.
		/// </summary>
		/// <value>The accessor.</value>
		[Obsolete("ObjectModels should be Accessor-agnostic and not rely on being able to communicate with the Accessor"), NonSerializedProperty]
		public Accessor Accessor { get; internal set; }

		/// <summary>
		/// Clears all data from this <see cref="ObjectModel" /> and returns it to a pristine state.
		/// </summary>
		public abstract void Clear();
		/// <summary>
		/// Copies all data from this <see cref="ObjectModel" /> to the specified <see cref="ObjectModel" />.
		/// </summary>
		/// <param name="where">The <see cref="ObjectModel" /> into which to copy the data of this <see cref="ObjectModel" />.</param>
		/// <exception cref="ObjectModelNotSupportedException">The conversion between this <see cref="ObjectModel" /> and the given <see cref="ObjectModel" /> is not supported.</exception>
		public abstract void CopyTo(ObjectModel where);
		/// <summary>
		/// Copies all data from this <see cref="ObjectModel" /> to the specified <see cref="ObjectModel" />.
		/// </summary>
		/// <param name="where">The <see cref="ObjectModel" /> into which to copy the data of this <see cref="ObjectModel" />.</param>
		/// <param name="append">When <c>false</c>, the <see cref="Clear" /> method is called on the destination <see cref="ObjectModel" /> before the copy is performed.</param>
		/// <exception cref="ObjectModelNotSupportedException">The conversion between this <see cref="ObjectModel" /> and the given <see cref="ObjectModel" /> is not supported.</exception>
		public void CopyTo(ObjectModel where, bool append)
		{
			if (!append) where.Clear();
			CopyTo(where);
		}

		protected virtual CriteriaResult[] FindInternal(CriteriaQuery query)
		{
			return null;
		}
		public CriteriaResult[] Find(CriteriaQuery query)
		{
			return FindInternal(query);
		}

		/// <summary>
		/// Copies all data from the given <see cref="ObjectModel" /> into this <see cref="ObjectModel" />.
		/// </summary>
		/// <param name="where">The <see cref="ObjectModel" /> from which to copy the data.</param>
		/// <exception cref="ObjectModelNotSupportedException">The conversion between this <see cref="ObjectModel" /> and the given <see cref="ObjectModel" /> is not supported.</exception>
		public void CopyFrom(ObjectModel where)
		{
			where.CopyTo(this);
		}
		/// <summary>
		/// Copies all data from the given <see cref="ObjectModel" /> into this <see cref="ObjectModel" />.
		/// </summary>
		/// <param name="where">The <see cref="ObjectModel" /> from which to copy the data.</param>
		/// <param name="append">When <c>false</c>, the <see cref="Clear" /> method is called on this <see cref="ObjectModel" /> before the copy is performed.</param>
		/// <exception cref="ObjectModelNotSupportedException">The conversion between this <see cref="ObjectModel" /> and the given <see cref="ObjectModel" /> is not supported.</exception>
		public void CopyFrom(ObjectModel where, bool append)
		{
			if (!append) Clear();
			CopyFrom(where);
		}
		/// <summary>
		/// Creates a clone of this <see cref="ObjectModel" /> and returns it. This is normally implemented as creating a new instance of this
		/// <see cref="ObjectModel" />, then calling the original instance's <see cref="CopyTo(ObjectModel)" /> method passing in the new instance as the target.
		/// </summary>
		/// <returns>The cloned <see cref="ObjectModel" />.</returns>
		public object Clone()
		{
			Type type = this.GetType();
			ObjectModel clone = (type.Assembly.CreateInstance(type.FullName) as ObjectModel);
			CopyTo(clone);
			return clone;
		}

		/// <summary>
		/// Performs a simple find and replace on the public properties of this <see cref="ObjectModel" /> using reflection. For a more in-depth find and replace
		/// solution, individual <see cref="ObjectModel" /> authors should annotate individual parameters that should participate in the find-and-replace feature.
		/// </summary>
		/// <param name="FindWhat">The string to search for.</param>
		/// <param name="ReplaceWith">The string with which the found string should be replaced.</param>
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

		// TODO: should this use the MBS.Framework "ISupportsExtraData" interface? Or is this specifically UE functionality (with DataFormatReference)?
		/// <summary>
		/// Gets the value of the custom property with the specified name for the given <see cref="DataFormatReference" />. If no custom property with the given
		/// name is registered for the specified <see cref="DataFormatReference" />, return the value specified as <paramref name="defaultValue" />.
		/// </summary>
		/// <returns>The value of the custom property with the specified name for the given <see cref="DataFormatReference" />.</returns>
		/// <param name="dfr">The <see cref="DataFormatReference" /> for which to look up custom properties.</param>
		/// <param name="name">The name of the custom property to search for.</param>
		/// <param name="defaultValue">The value that should be returned if no custom property with the given name is registered for the specified <see cref="DataFormatReference" />.</param>
		/// <typeparam name="T">The type of custom property that should be returned.</typeparam>
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
		/// <summary>
		/// Gets the value of the custom property with the specified name for the given <see cref="DataFormatReference" />. If no custom property with the given
		/// name is registered for the specified <see cref="DataFormatReference" />, return the value specified as <paramref name="defaultValue" />.
		/// </summary>
		/// <returns>The value of the custom property with the specified name for the given <see cref="DataFormatReference" />.</returns>
		/// <param name="dfr">The <see cref="DataFormatReference" /> for which to look up custom properties.</param>
		/// <param name="name">The name of the custom property to search for.</param>
		/// <param name="defaultValue">The value that should be returned if no custom property with the given name is registered for the specified <see cref="DataFormatReference" />.</param>
		public object GetCustomProperty(DataFormatReference dfr, string name, object defaultValue = null)
		{
			return GetCustomProperty<object>(dfr, name, defaultValue);
		}
		/// <summary>
		/// Sets the value of the custom property with the specified name for the given <see cref="DataFormatReference" />. If no custom property with the given
		/// name is registered for the specified <see cref="DataFormatReference" />, a new property is registered.
		/// </summary>
		/// <param name="dfr">The <see cref="DataFormatReference" /> for which to look up or register custom properties.</param>
		/// <param name="name">The name of the custom property to set.</param>
		/// <param name="value">The value that should be assigned to the property.</param>
		/// <typeparam name="T">The type of custom property that should be set.</typeparam>
		public void SetCustomProperty<T>(DataFormatReference dfr, string name, T value)
		{
			if (!_customProperties.ContainsKey(dfr))
			{
				_customProperties.Add(dfr, new Dictionary<string, object>());
			}
			_customProperties[dfr][name] = value;
		}
		/// <summary>
		/// Sets the value of the custom property with the specified name for the given <see cref="DataFormatReference" />. If no custom property with the given
		/// name is registered for the specified <see cref="DataFormatReference" />, a new property is registered.
		/// </summary>
		/// <param name="dfr">The <see cref="DataFormatReference" /> for which to look up or register custom properties.</param>
		/// <param name="name">The name of the custom property to set.</param>
		/// <param name="value">The value that should be assigned to the property.</param>
		public void SetCustomProperty(DataFormatReference dfr, string name, object value)
		{
			SetCustomProperty<object>(dfr, name, value);
		}



		protected virtual CriteriaObject[] GetCriteriaObjectsInternal()
		{
			return new CriteriaObject[0];
		}
		public CriteriaObject[] GetCriteriaObjects()
		{
			return GetCriteriaObjectsInternal();
		}
	}
}
