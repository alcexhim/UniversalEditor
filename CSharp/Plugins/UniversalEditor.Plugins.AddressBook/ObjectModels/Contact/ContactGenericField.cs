using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Contact
{
	public struct ContactGenericField<T>
	{
		private DateTime mvarModificationDate;
		public DateTime ModificationDate { get { return mvarModificationDate; } set { mvarModificationDate = value; } }

		private T mvarValue;
		public T Value { get { return mvarValue; } set { mvarValue = value; } }

		public static readonly ContactGenericField<T> Empty = new ContactGenericField<T>(default(T));

		public ContactGenericField(T value, DateTime? modificationDate = null)
		{
			mvarValue = value;
			if (modificationDate != null)
			{
				mvarModificationDate = modificationDate.Value;
			}
			else
			{
				mvarModificationDate = DateTime.Now;
			}
		}
	}
}
