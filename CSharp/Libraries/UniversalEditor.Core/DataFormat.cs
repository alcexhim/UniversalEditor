//
//  DataFormat.cs - translates ObjectModel to serialized data in a particular format
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
using System.Diagnostics;
using System.Text;

using UniversalEditor.IO;

namespace UniversalEditor
{
	public abstract class DataFormat : References<DataFormatReference>
	{
		public class DataFormatCollection 
			: System.Collections.ObjectModel.Collection<DataFormat>
		{

		}

		internal DataFormatReference mvarReference = null;
		/// <summary>
		/// The DataFormatReference used to create this DataFormat.
		/// </summary>
		public DataFormatReference Reference
		{
			get { return mvarReference; }
		}

		public DataFormatReference MakeReference()
		{
			DataFormatReference dfr = MakeReferenceInternal();
			DataFormatReference.Register(dfr);
			return dfr;
		}
		protected virtual DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = new DataFormatReference(GetType());
			return dfr;
		}
		
		private Accessor mvarAccessor = null;
		protected internal Accessor Accessor { get { return mvarAccessor; } set { mvarAccessor = value; } }

		/// <summary>
		/// Continues loading the file into the specified <see cref="ObjectModel" /> with a different
		/// <see cref="DataFormat" />.
		/// </summary>
		/// <param name="objectModel">The <see cref="ObjectModel" /> in which to continue loading the document.</param>
		/// <param name="otherDataFormat">The <see cref="DataFormat" /> used to parse the document.</param>
		protected void ContinueLoading(ref ObjectModel objectModel, DataFormat otherDataFormat)
		{
			otherDataFormat.Accessor = mvarAccessor;
			otherDataFormat.Load(ref objectModel);
		}

		protected virtual bool IsObjectModelSupported(ObjectModel objectModel)
		{
			DataFormatReference dfr = MakeReferenceInternal();
			ObjectModelReference omr = objectModel.MakeReference();
			return dfr.Capabilities.Contains(omr.Type) || dfr.Capabilities.Contains(omr.ID);
		}

		[DebuggerNonUserCode()]
		public void Load(ref ObjectModel objectModel)
		{
			if (objectModel == null) throw new ArgumentNullException("objectModel", "objectModel cannot be null");

			Stack<ObjectModel> stack = new Stack<ObjectModel>();
			stack.Push(objectModel);
			BeforeLoadInternal(stack);

			ObjectModel omb = stack.Pop();
			LoadInternal(ref omb);
			stack.Push(omb);
			/*
			if (!IsObjectModelSupported(omb))
			{
				throw new NotSupportedException("Object model not supported");
			}
			*/
			AfterLoadInternal(stack);
		}
		public void Save(ObjectModel objectModel)
		{
			if (objectModel == null) throw new ArgumentNullException("objectModel", "objectModel cannot be null");

			Stack<ObjectModel> stack = new Stack<ObjectModel>();
			stack.Push(objectModel);
			BeforeSaveInternal(stack);

			ObjectModel omb = stack.Pop();
			SaveInternal(omb);
			stack.Push(omb);

			AfterSaveInternal(stack);
		}

		protected abstract void LoadInternal(ref ObjectModel objectModel);
		protected abstract void SaveInternal(ObjectModel objectModel);

		protected virtual void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
		}
		protected virtual void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
		}
		protected virtual void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
		}
		protected virtual void AfterSaveInternal(Stack<ObjectModel> objectModels)
		{
		}
	}
}
