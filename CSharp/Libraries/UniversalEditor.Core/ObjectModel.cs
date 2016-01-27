using System;
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
