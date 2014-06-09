using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace UniversalEditor
{
	public class MfcClassRegistry
	{
		private Dictionary<string, MfcClass> _registry;

		public MfcClassRegistry()
		{
			this._registry = new Dictionary<string, MfcClass>();
		}

		/// <summary>
		/// Registers a single serialised class name.
		/// </summary>
		/// <param name="mfcName">The serialised class name.</param>
		/// <param name="realType">The .Net type capable of deserialising the class data.</param>
		public void RegisterClass(string mfcName, Type realType)
		{
			_registry.Add(mfcName, new MfcClass
			{
				Name = mfcName,
				SchemaVersion = 1,
				RealType = realType
			});
		}

		/// <summary>
		/// Registers all exported classes from the specified assembly
		/// that are marked with the MfcSerialisable attribute.
		/// </summary>
		public void AutoRegisterClasses(Assembly assembly)
		{
			foreach (var type in assembly.GetExportedTypes())
			{
				var markers = type.GetCustomAttributes(typeof(MfcSerialisableAttribute), false).Cast<MfcSerialisableAttribute>();
				foreach (var marker in markers)
				{
					RegisterClass(marker.SerialisedName, type);
				}
			}
		}

		public MfcClass GetMfcClass(string mfcName)
		{
			return _registry[mfcName];
		}

		public MfcClass GetMfcClass(Type realType)
		{
			var x = from c in _registry.Values where c.RealType == realType select c;
			return x.Single();
		}
	}
}
