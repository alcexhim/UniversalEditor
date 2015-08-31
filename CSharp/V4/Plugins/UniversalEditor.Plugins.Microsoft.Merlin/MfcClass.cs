using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public class MfcClass
	{
		public string Name { get; set; }
		public int SchemaVersion { get; set; }
		public Type RealType { get; set; }

		internal T CreateNewObject<T>() where T : IMfcSerialisable<T>
		{
			if (RealType.IsAssignableFrom(typeof(T)))
			{
				throw new ArgumentException("Cannot assign " + RealType.Name + " from " + typeof(T).Name);
			}
			return (T)RealType.Assembly.CreateInstance(RealType.FullName);
		}
	}
}
