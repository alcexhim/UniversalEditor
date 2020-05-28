//
//  Document.cs - provide convenient way of loading OM from accessor with a given DF
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
using System.Diagnostics;

namespace UniversalEditor
{
	/// <summary>
	/// Represents a combination of <see cref="InputDataFormat" />, <see cref="ObjectModel" />, and <see cref="Accessor" />
	/// that allows you to easily manipulate documents. The Accessor determines WHERE the data is read from and written
	/// to, the DataFormat determines HOW the data is written, and the ObjectModel contains the actual data in a format-
	/// agnostic representation.
	/// </summary>
	public class Document : IDisposable
	{
		/// <summary>
		/// The <see cref="Accessor" /> which determines where the data is read from.
		/// </summary>
		public Accessor InputAccessor { get; set; } = null;
		/// <summary>
		/// The <see cref="Accessor" />, which determines where the data is written to.
		/// </summary>
		public Accessor OutputAccessor { get; set; } = null;
		/// <summary>
		/// The <see cref="DataFormat" /> which determines how the data is read from the accessor.
		/// </summary>
		public DataFormat InputDataFormat { get; set; } = null;
		/// <summary>
		/// The <see cref="DataFormat" /> which determines how the data is written to the accessor.
		/// </summary>
		public DataFormat OutputDataFormat { get; set; } = null;

		private ObjectModel mvarObjectModel = null;
		/// <summary>
		/// The <see cref="ObjectModel" />, which stores the actual data in a format-agnostic representation.
		/// </summary>
		public ObjectModel ObjectModel { get { return mvarObjectModel; } set { mvarObjectModel = value; } }

		public void Dispose()
		{
			Close();
		}

		/// <summary>
		/// Reads data into the current <see cref="ObjectModel" /> from the <see cref="Accessor" /> using the
		/// current <see cref="InputDataFormat" />.
		/// </summary>
		[DebuggerNonUserCode()]
		public void Load()
		{
			InputDataFormat.Accessor = InputAccessor;
			mvarObjectModel.Accessor = InputAccessor;
			InputDataFormat.Load(ref mvarObjectModel);
			mvarLastUsedAccessor = LastUsedAccessor.Input;
		}
		/// <summary>
		/// Writes the data contained in the <see cref="ObjectModel" /> to the <see cref="Accessor" /> using the
		/// current <see cref="OutputDataFormat" />.
		/// </summary>
		public void Save()
		{
			OutputDataFormat.Accessor = OutputAccessor;
			mvarObjectModel.Accessor = OutputAccessor;

			bool opened = false;
			if (!OutputAccessor.IsOpen)
			{
				OutputAccessor.Open();
				opened = true;
			}
			OutputDataFormat.Save(mvarObjectModel);
			if (opened)
			{
				OutputAccessor.Close();
			}
			mvarLastUsedAccessor = LastUsedAccessor.Output;

			IsSaved = true;
			IsChanged = false;
		}

		public Document(ObjectModel objectModel, DataFormat dataFormat) : this(objectModel, dataFormat, null)
		{
		}
		public Document(ObjectModel objectModel, DataFormat dataFormat, Accessor accessor) : this(objectModel, dataFormat, dataFormat, accessor)
		{
		}
		public Document(Accessor accessor) : this(null, null, accessor)
		{
		}
		public Document(ObjectModel objectModel, DataFormat inputDataFormat, DataFormat outputDataFormat, Accessor accessor) : this(objectModel, inputDataFormat, outputDataFormat, accessor, accessor)
		{
		}
		public Document(ObjectModel objectModel, DataFormat inputDataFormat, DataFormat outputDataFormat, Accessor inputAccessor, Accessor outputAccessor)
		{
			mvarObjectModel = objectModel;
			InputDataFormat = inputDataFormat;
			OutputDataFormat = outputDataFormat;
			InputAccessor = inputAccessor;
			OutputAccessor = outputAccessor;
		}

		[DebuggerNonUserCode()]
		public static Document Load(ObjectModel objectModel, DataFormat dataFormat, Accessor accessor, bool autoClose = true, bool append = false)
		{
			if (!append) objectModel.Clear();

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
			document.OutputAccessor.Flush();
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
			if (InputAccessor != null)
			{
				if (InputAccessor.IsOpen)
				{
					InputAccessor.Close();
					mvarTitle = OutputAccessor.GetFileTitle();
				}
			}
			if (OutputAccessor != null)
			{
				if (OutputAccessor.IsOpen)
				{
					OutputAccessor.Close();
					mvarTitle = OutputAccessor.GetFileTitle();
				}
			}
		}

		private LastUsedAccessor mvarLastUsedAccessor = LastUsedAccessor.Input;
		private String mvarTitle = String.Empty;
		/// <summary>
		/// The title of this <see cref="Document" />.
		/// </summary>
		public string Title
		{
			get
			{
				if (mvarLastUsedAccessor == LastUsedAccessor.Input && InputAccessor != null)
				{
					return InputAccessor.GetFileTitle();
				}
				else if (mvarLastUsedAccessor == LastUsedAccessor.Output && OutputAccessor != null)
				{
					return OutputAccessor.GetFileTitle();
				}
				return mvarTitle;
			}
		}

		/// <summary>
		/// Determines whether the content of this <see cref="Document" /> has changed.
		/// </summary>
		public bool IsChanged { get; set; }
		/// <summary>
		/// The last-used <see cref="Accessor" /> associated with this <see cref="Document" />.
		/// </summary>
		public Accessor Accessor
		{
			get
			{
				switch (mvarLastUsedAccessor)
				{
					case LastUsedAccessor.Input:
					{
						return InputAccessor;
					}
					case LastUsedAccessor.Output:
					{
						return OutputAccessor;
					}
				}
				return null;
			}
			set
			{
				InputAccessor = value;
				OutputAccessor = value;
			}
		}
		/// <summary>
		/// The <see cref="DataFormat" /> associated with this <see cref="Document" />.
		/// </summary>
		public DataFormat DataFormat
		{
			get
			{
				switch (mvarLastUsedAccessor)
				{
					case LastUsedAccessor.Input:
					{
						return InputDataFormat;
					}
					case LastUsedAccessor.Output:
					{
						return OutputDataFormat;
					}
				}
				return null;
			}
			set
			{
				InputDataFormat = value;
				OutputDataFormat = value;
			}
		}
	}
}
