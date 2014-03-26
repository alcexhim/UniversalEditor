using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
    /// <summary>
    /// Represents a combination of <see cref="DataFormat" />, <see cref="ObjectModel" />, and <see cref="Accessor" />
    /// that allows you to easily manipulate documents. The Accessor determines WHERE the data is read from and written
    /// to, the DataFormat determines HOW the data is written, and the ObjectModel contains the actual data in a format-
    /// agnostic representation.
    /// </summary>
    public class Document
    {
        private Accessor mvarAccessor = null;
        /// <summary>
        /// The <see cref="Accessor" />, which determines where the data is read from and written to.
        /// </summary>
        public Accessor Accessor { get { return mvarAccessor; } set { mvarAccessor = value; } }

        private DataFormat mvarDataFormat = null;
        /// <summary>
        /// The <see cref="DataFormat" />, which determines how the data is written to the accessor.
        /// </summary>
        public DataFormat DataFormat { get { return mvarDataFormat; } set { mvarDataFormat = value; } }

        private ObjectModel mvarObjectModel = null;
        /// <summary>
        /// The <see cref="ObjectModel" />, which stores the actual data in a format-agnostic representation.
        /// </summary>
        public ObjectModel ObjectModel { get { return mvarObjectModel; } set { mvarObjectModel = value; } }

        /// <summary>
        /// Reads data into the current <see cref="ObjectModel" /> from the <see cref="Accessor" /> using the
        /// current <see cref="DataFormat" />.
        /// </summary>
        public void Load()
        {
            mvarDataFormat.Accessor = mvarAccessor;
            mvarDataFormat.Load(ref mvarObjectModel);
        }
        /// <summary>
        /// Writes the data contained in the <see cref="ObjectModel" /> to the <see cref="Accessor" /> using the
        /// current <see cref="DataFormat" />.
        /// </summary>
        public void Save()
        {
            mvarDataFormat.Accessor = mvarAccessor;
            mvarDataFormat.Save(mvarObjectModel);
        }

        public Document(ObjectModel objectModel, DataFormat dataFormat) : this(objectModel, dataFormat, null)
        {
        }
        public Document(ObjectModel objectModel, DataFormat dataFormat, Accessor accessor)
        {
            mvarObjectModel = objectModel;
            mvarDataFormat = dataFormat;
            mvarAccessor = accessor;
        }

		public static Document Load(ObjectModel objectModel, DataFormat dataFormat, Accessor accessor, bool autoClose = false)
		{
			Document document = new Document(objectModel, dataFormat, accessor);
			document.Accessor.Open();
			document.Load();
			if (autoClose) document.Accessor.Close();
			return document;
		}
	}
}
