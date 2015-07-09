using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public class ObjectModelCustomProperty
	{
		public class ObjectModelCustomPropertyCollection
		{
			private Dictionary<DataFormat, Dictionary<string, ObjectModelCustomProperty>> _internalCollection = new Dictionary<DataFormat, Dictionary<string, ObjectModelCustomProperty>>();

			public ObjectModelCustomProperty Add(DataFormat dataFormat, string name, object value)
			{
				ObjectModelCustomProperty item = new ObjectModelCustomProperty();
				item.DataFormat = dataFormat;
				item.Name = name;
				item.Value = value;

				Dictionary<string, ObjectModelCustomProperty> values = null;
				if (!_internalCollection.ContainsKey(dataFormat))
				{
					values = new Dictionary<string, ObjectModelCustomProperty>();
					_internalCollection.Add(dataFormat, values);
				}
				else
				{
					values = _internalCollection[dataFormat];
				}
				values[name] = item;
				return item;
			}
			public ObjectModelCustomProperty[] this[DataFormat dataFormat]
			{
				get
				{
					List<ObjectModelCustomProperty> list = new List<ObjectModelCustomProperty>();
					if (_internalCollection.ContainsKey(dataFormat))
					{
						foreach (KeyValuePair<string, ObjectModelCustomProperty> kvp in _internalCollection[dataFormat])
						{
							list.Add(kvp.Value);
						}
					}
					return list.ToArray();
				}
			}
			public ObjectModelCustomProperty this[DataFormat dataFormat, string name]
			{
				get
				{
					if (!_internalCollection.ContainsKey(dataFormat)) return null;
					if (!_internalCollection[dataFormat].ContainsKey(name)) return null;
					return _internalCollection[dataFormat][name];
				}
				set
				{
					if (!_internalCollection.ContainsKey(dataFormat)) _internalCollection.Add(dataFormat, new Dictionary<string,ObjectModelCustomProperty>());
					if (!_internalCollection[dataFormat].ContainsKey(name)) _internalCollection[dataFormat].Add(name, value);
					_internalCollection[dataFormat][name] = value;
				}
			}
		}

		private DataFormat mvarDataFormat = null;
		public DataFormat DataFormat { get { return mvarDataFormat; } set { mvarDataFormat = value; } }

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private object mvarValue = null;
		public object Value { get { return mvarValue; } set { mvarValue = value; } }
	}
}
