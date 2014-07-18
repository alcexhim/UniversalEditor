using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	/// <summary>
	/// Represents a combination of <see cref="InputDataFormat" />, <see cref="ObjectModel" />, and <see cref="Accessor" />
	/// that allows you to easily manipulate documents. The Accessor determines WHERE the data is read from and written
	/// to, the DataFormat determines HOW the data is written, and the ObjectModel contains the actual data in a format-
	/// agnostic representation.
	/// </summary>
	public class Document
	{
		private Accessor mvarInputAccessor = null;
		/// <summary>
		/// The <see cref="Accessor" /> which determines where the data is read from.
		/// </summary>
		public Accessor InputAccessor { get { return mvarInputAccessor; } set { mvarInputAccessor = value; } }

		private Accessor mvarOutputAccessor = null;
		/// <summary>
		/// The <see cref="Accessor" />, which determines where the data is written to.
		/// </summary>
		public Accessor OutputAccessor { get { return mvarOutputAccessor; } set { mvarOutputAccessor = value; } }

		private DataFormat mvarInputDataFormat = null;
		/// <summary>
		/// The <see cref="DataFormat" /> which determines how the data is read from the accessor.
		/// </summary>
		public DataFormat InputDataFormat { get { return mvarInputDataFormat; } set { mvarInputDataFormat = value; } }

		private DataFormat mvarOutputDataFormat = null;
		/// <summary>
		/// The <see cref="DataFormat" /> which determines how the data is written to the accessor.
		/// </summary>
		public DataFormat OutputDataFormat { get { return mvarOutputDataFormat; } set { mvarOutputDataFormat = value; } }

		private ObjectModel mvarObjectModel = null;
		/// <summary>
		/// The <see cref="ObjectModel" />, which stores the actual data in a format-agnostic representation.
		/// </summary>
		public ObjectModel ObjectModel { get { return mvarObjectModel; } set { mvarObjectModel = value; } }

		/// <summary>
		/// Reads data into the current <see cref="ObjectModel" /> from the <see cref="Accessor" /> using the
		/// current <see cref="InputDataFormat" />.
		/// </summary>
		public void Load()
		{
			mvarInputDataFormat.Accessor = mvarInputAccessor;
			mvarObjectModel.Accessor = mvarInputAccessor;
			mvarInputDataFormat.Load(ref mvarObjectModel);
			mvarLastUsedAccessor = LastUsedAccessor.Input;
		}
		/// <summary>
		/// Writes the data contained in the <see cref="ObjectModel" /> to the <see cref="Accessor" /> using the
		/// current <see cref="OutputDataFormat" />.
		/// </summary>
		public void Save()
		{
			mvarOutputDataFormat.Accessor = mvarOutputAccessor;
			mvarObjectModel.Accessor = mvarOutputAccessor;
			mvarOutputDataFormat.Save(mvarObjectModel);
			mvarLastUsedAccessor = LastUsedAccessor.Output;
		}

		public Document(ObjectModel objectModel, DataFormat dataFormat) : this(objectModel, dataFormat, null)
		{
		}
		public Document(ObjectModel objectModel, DataFormat dataFormat, Accessor accessor) : this(objectModel, dataFormat, dataFormat, accessor)
		{
		}
		public Document(ObjectModel objectModel, DataFormat inputDataFormat, DataFormat outputDataFormat, Accessor accessor) : this(objectModel, inputDataFormat, outputDataFormat, accessor, accessor)
		{
		}
		public Document(ObjectModel objectModel, DataFormat inputDataFormat, DataFormat outputDataFormat, Accessor inputAccessor, Accessor outputAccessor)
		{
			mvarObjectModel = objectModel;
			mvarInputDataFormat = inputDataFormat;
			mvarOutputDataFormat = outputDataFormat;
			mvarInputAccessor = inputAccessor;
			mvarOutputAccessor = outputAccessor;
		}

		public static Document Load(ObjectModel objectModel, DataFormat dataFormat, Accessor accessor, bool autoClose = true)
		{
			Document document = new Document(objectModel, dataFormat, accessor);
			objectModel.Accessor = document.InputAccessor;
			document.InputAccessor.Open();
			document.Load();
			if (autoClose) document.InputAccessor.Close();
			return document;
		}
		public static Document Save(ObjectModel objectModel, DataFormat dataFormat, Accessor accessor, bool autoClose = true)
		{
			Document document = new Document(objectModel, dataFormat, accessor);
			objectModel.Accessor = document.OutputAccessor;
			document.OutputAccessor.Open();
			document.Save();
			if (autoClose) document.OutputAccessor.Close();
			return document;
		}
		public static Document Convert(ObjectModel objectModel, DataFormat inputDataFormat, DataFormat outputDataFormat, Accessor inputAccessor, Accessor outputAccessor)
		{
			Document document = new Document(objectModel, inputDataFormat, outputDataFormat, inputAccessor, outputAccessor);
			document.InputAccessor.Open();
			document.Load();
			document.InputAccessor.Close();

			document.OutputAccessor.Open();
			document.Save();
			document.OutputAccessor.Close();
			return document;
		}

		private bool mvarIsSaved = false;
		/// <summary>
		/// Determines whether the document has been saved or not.
		/// </summary>
		public bool IsSaved { get { return mvarIsSaved; } set { mvarIsSaved = value; } }

		public void Close()
		{
			if (mvarInputAccessor != null)
			{
				if (mvarInputAccessor.IsOpen)
				{
					mvarInputAccessor.Close();
					mvarTitle = mvarOutputAccessor.Title;
				}
			}
			if (mvarOutputAccessor != null)
			{
				if (mvarOutputAccessor.IsOpen)
				{
					mvarOutputAccessor.Close();
					mvarTitle = mvarOutputAccessor.Title;
				}
			}
		}

		private LastUsedAccessor mvarLastUsedAccessor = LastUsedAccessor.Input;
		private String mvarTitle = String.Empty;
		public string Title
		{
			get
			{
				if (mvarLastUsedAccessor == LastUsedAccessor.Input && mvarInputAccessor != null)
				{
					return mvarInputAccessor.Title;
				}
				else if (mvarLastUsedAccessor == LastUsedAccessor.Output && mvarOutputAccessor != null)
				{
					return mvarOutputAccessor.Title;
				}
				return mvarTitle;
			}
		}

		public bool IsChanged { get; set; }

		public Accessor Accessor
		{
			get
			{
				switch (mvarLastUsedAccessor)
				{
					case LastUsedAccessor.Input:
					{
						return mvarInputAccessor;
					}
					case LastUsedAccessor.Output:
					{
						return mvarOutputAccessor;
					}
				}
				return null;
			}
		}

		public DataFormat DataFormat
		{
			get
			{
				switch (mvarLastUsedAccessor)
				{
					case LastUsedAccessor.Input:
					{
						return mvarInputDataFormat;
					}
					case LastUsedAccessor.Output:
					{
						return mvarOutputDataFormat;
					}
				}
				return null;
			}
			set
			{
				mvarInputDataFormat = value;
				mvarOutputDataFormat = value;
			}
		}
	}
}
