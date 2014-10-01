﻿using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.IO;

namespace UniversalEditor
{
	public abstract class DataFormat
	{
		internal DataFormatReference mvarReference = null;
		/// <summary>
		/// The DataFormatReference used to create this DataFormat.
		/// </summary>
		public DataFormatReference Reference
		{
			get { return mvarReference; }
		}

		public virtual DataFormatReference MakeReference()
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
			DataFormatReference dfr = MakeReference();
			ObjectModelReference omr = objectModel.MakeReference();
			return dfr.Capabilities.Contains(omr.ObjectModelType) || dfr.Capabilities.Contains(omr.ObjectModelID);
		}

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
