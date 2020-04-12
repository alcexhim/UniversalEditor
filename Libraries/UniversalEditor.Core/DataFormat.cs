//
//  DataFormat.cs - translates ObjectModel to serialized data in a particular format
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
using System.Collections.Generic;
using System.Diagnostics;

namespace UniversalEditor
{
	/// <summary>
	/// The on-disk representation of data stored in an <see cref="ObjectModel" /> and accessed by an <see cref="Accessor" />.
	/// </summary>
	public abstract class DataFormat : References<DataFormatReference>
	{
		/// <summary>
		/// Represents a collection of <see cref="DataFormat" /> objects.
		/// </summary>
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

		/// <summary>
		/// Creates a <see cref="DataFormatReference" /> for this <see cref="DataFormat" /> and registers it for future use.
		/// </summary>
		/// <returns>The <see cref="DataFormatReference" /> that provides metadata and other information about this <see cref="DataFormat" />.</returns>
		public DataFormatReference MakeReference()
		{
			DataFormatReference dfr = MakeReferenceInternal();
			DataFormatReference.Register(dfr);
			return dfr;
		}
		/// <summary>
		/// Creates a new <see cref="DataFormatReference" />. The returned <see cref="DataFormatReference" /> is not cached. It is recommended that subclasses
		/// override this method and cache their own personal instances of <see cref="DataFormatReference" /> containing the appropriate metadata for their
		/// subclassed implementations.
		/// </summary>
		/// <returns>The <see cref="DataFormatReference" /> that provides metadata and other information about this <see cref="DataFormat" />.</returns>
		protected virtual DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = new DataFormatReference(GetType());
			return dfr;
		}

		/// <summary>
		/// The <see cref="Accessor" /> used to read and write data.
		/// </summary>
		/// <value>The <see cref="Accessor" /> used to read and write data.</value>
		[Obsolete("In next version, Accessor will be a parameter passed into LoadInternal / SaveInternal methods")]
		protected internal Accessor Accessor { get; set; } = null;

		/// <summary>
		/// Continues loading the file into the specified <see cref="ObjectModel" /> with a different
		/// <see cref="DataFormat" />.
		/// </summary>
		/// <param name="objectModel">The <see cref="ObjectModel" /> in which to continue loading the document.</param>
		/// <param name="otherDataFormat">The <see cref="DataFormat" /> used to parse the document.</param>
		protected void ContinueLoading(ref ObjectModel objectModel, DataFormat otherDataFormat)
		{
			otherDataFormat.Accessor = Accessor;
			otherDataFormat.Load(ref objectModel);
		}

		/// <summary>
		/// Determines if the given <see cref="ObjectModel" /> is supported by this <see cref="DataFormat" />.
		/// </summary>
		/// <returns><c>true</c>, if the specified <see cref="ObjectModel" /> is supported by this <see cref="DataFormat" />, <c>false</c> otherwise.</returns>
		/// <param name="objectModel">The <see cref="ObjectModel" /> whose support should be checked.</param>
		protected virtual bool IsObjectModelSupported(ObjectModel objectModel)
		{
			DataFormatReference dfr = MakeReferenceInternal();
			ObjectModelReference omr = objectModel.MakeReference();
			return dfr.Capabilities.Contains(omr.Type) || dfr.Capabilities.Contains(omr.ID);
		}

		/// <summary>
		/// Reads the contents of the specified <see cref="ObjectModel" /> from the <see cref="Accessor" /> using this <see cref="DataFormat" />.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Before the actual content is read using this <see cref="DataFormat" />'s <see cref="LoadInternal" /> method, a <see cref="Stack{ObjectModel}" /> is
		/// created and the specified <see cref="ObjectModel" /> is pushed onto the stack. The <see cref="BeforeLoadInternal" /> method is then called, with the
		/// stack as its only parameter. A single <see cref="ObjectModel" /> is then popped off the stack, and the <see cref="LoadInternal" /> method is called
		/// with this <see cref="ObjectModel"/> as its only parameter. After the actual content is read, the <see cref="AfterLoadInternal" /> method is called,
		/// again passing in the stack as its only parameter.
		/// </para>
		/// <para>
		/// Note that the <see cref="ObjectModel" /> passed into the <see cref="Load" /> method may not necessarily be the same as the <see cref="ObjectModel" />
		/// that is eventually passed into the <see cref="LoadInternal" /> method. It is possible to subclass <see cref="DataFormat" />s to create new formats
		/// based on existing ones. XML, ZIP, and RIFF are a few good examples of <see cref="DataFormat" />s that serve as the base for others. For more
		/// information, refer to the documentation for the <see cref="BeforeLoadInternal" />,  <see cref="AfterLoadInternal" />, and
		/// <see cref="BeforeSaveInternal" /> methods.
		/// </para>
		/// </remarks>
		/// <param name="objectModel">The <see cref="ObjectModel" /> whose content should be written using this <see cref="DataFormat" />.</param>
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
		/// <summary>
		/// Writes the contents of the specified <see cref="ObjectModel" /> to the <see cref="Accessor" /> using this <see cref="DataFormat" />.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Before the actual content is written using this <see cref="DataFormat" />'s <see cref="SaveInternal" /> method, a <see cref="Stack{ObjectModel}" /> is
		/// created and the specified <see cref="ObjectModel" /> is pushed onto the stack. The <see cref="BeforeSaveInternal" /> method is then called, with the
		/// stack as its only parameter. A single <see cref="ObjectModel" /> is then popped off the stack, and the <see cref="SaveInternal" /> method is called
		/// with this <see cref="ObjectModel"/> as its only parameter. After the actual content is written, the <see cref="AfterSaveInternal" /> method is called,
		/// again passing in the stack as its only parameter.
		/// </para>
		/// <para>
		/// Note that the <see cref="ObjectModel" /> passed into the <see cref="Save" /> method may not necessarily be the same as the <see cref="ObjectModel" />
		/// that is eventually passed into the <see cref="SaveInternal" /> method. It is possible to subclass <see cref="DataFormat" />s to create new formats
		/// based on existing ones. XML, ZIP, and RIFF are a few good examples of <see cref="DataFormat" />s that serve as the base for others. For more
		/// information, refer to the documentation for the <see cref="BeforeLoadInternal" />,  <see cref="AfterLoadInternal" />, and
		/// <see cref="BeforeSaveInternal" /> methods.
		/// </para>
		/// </remarks>
		/// <param name="objectModel">The <see cref="ObjectModel" /> whose content should be written using this <see cref="DataFormat" />.</param>
		public void Save(ObjectModel objectModel)
		{
			if (objectModel == null) throw new ArgumentNullException(nameof(objectModel), "objectModel cannot be null");

			Stack<ObjectModel> stack = new Stack<ObjectModel>();
			stack.Push(objectModel);
			BeforeSaveInternal(stack);

			ObjectModel omb = stack.Pop();
			SaveInternal(omb);
			stack.Push(omb);

			AfterSaveInternal(stack);
		}

		/// <summary>
		/// Reads the data from the <see cref="Accessor" /> into the specified <see cref="ObjectModel" />.
		/// </summary>
		/// <param name="objectModel">The <see cref="ObjectModel" /> into which to load data.</param>
		protected abstract void LoadInternal(ref ObjectModel objectModel);
		/// <summary>
		/// Writes the contents of the specified <see cref="ObjectModel" /> to the <see cref="Accessor" />.
		/// </summary>
		/// <param name="objectModel">The <see cref="ObjectModel" /> from which to save data.</param>
		protected abstract void SaveInternal(ObjectModel objectModel);

		/// <summary>
		/// Method called BEFORE the <see cref="LoadInternal" /> method is called on the original <see cref="DataFormat" />'s subclass.
		/// </summary>
		/// <remarks>
		/// When inheriting from a
		/// <see cref="DataFormat" /> subclass (e.g. XMLDataFormat), you need to create a new instance of the appropriate <see cref="ObjectModel" /> that the
		/// subclass expects, and push that onto the <paramref name="objectModels"/> stack, i.e. <code>objectModels.Push(new MarkupObjectModel());</code> This is
		/// usually the only line of code in the overridden <see cref="BeforeLoadInternal" /> method's body.
		/// </remarks>
		/// <example>
		/// objectModels.Push(new BaseObjectModel()); // this is all we need to do
		/// </example>
		/// <param name="objectModels">The stack of <see cref="ObjectModel"/>s used by this <see cref="DataFormat" />.</param>
		protected virtual void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
		}
		/// <summary>
		/// Method called AFTER the <see cref="LoadInternal"/> method is called on the original <see cref="DataFormat" />'s subclass.
		/// </summary>
		/// <remarks>
		/// When inheriting from a <see cref="DataFormat" /> subclass (e.g. XMLDataFormat), you need to first pop the <see cref="ObjectModel" /> that you pushed
		/// onto the <paramref name="objectModels"/> stack in your <see cref="BeforeLoadInternal" /> implementation, then pop the <see cref="ObjectModel" /> that
		/// your class expects to get passed. Now you can read data from the original <see cref="ObjectModel" /> and modify the second <see cref="ObjectModel" />.
		/// Because these objects are passed by reference, you do not need to push them back onto the stack for them to get properly loaded.
		/// </remarks>
		/// <example>
		/// BaseObjectModel bom = (objectModels.Pop() as BaseObjectModel); // base object model comes first
		/// MyVerySpecificObjectModel myOM = (objectModels.Pop() as MyVerySpecificObjectModel);
		/// 
		/// // populate MyVerySpecificObjectModel... and we're done. nothing else needs to be pushed back onto the stack.
		/// </example>
		/// <param name="objectModels">The stack of <see cref="ObjectModel"/>s used by this <see cref="DataFormat" />.</param>
		protected virtual void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
		}
		/// <summary>
		/// Method called BEFORE the <see cref="SaveInternal"/> method is called on the original <see cref="DataFormat" />'s subclass.
		/// </summary>
		/// <remarks>
		/// When inheriting from a <see cref="DataFormat" /> subclass (e.g. XMLDataFormat), you need to first pop the <see cref="ObjectModel" /> that your class
		/// expects to get passed, then create a new instance of the proper type of <see cref="ObjectModel" /> the base class is expecting. Now you can retrieve
		/// data from the <see cref="ObjectModel" /> that your class expects and properly format it for the <see cref="ObjectModel" /> the base class expects.
		/// When you're done, you need to push the newly-created <see cref="ObjectModel" /> onto the stack so that the underlying <see cref="SaveInternal" />
		/// method will be able to see it.
		/// </remarks>
		/// <example>
		/// MyVerySpecificObjectModel myOM = (objectModels.Pop() as MyVerySpecificObjectModel);
		/// BaseObjectModel bom = new BaseObjectModel();
		/// 
		/// // populate BaseObjectModel...
		/// 
		/// objectModels.Push(bom); // aaand we're done
		/// </example>
		/// <param name="objectModels">The stack of <see cref="ObjectModel"/>s used by this <see cref="DataFormat" />.</param>
		protected virtual void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
		}
		/// <summary>
		/// Method called AFTER the <see cref="SaveInternal"/> method is called on the original <see cref="DataFormat" />'s subclass.
		/// </summary>
		/// <remarks>
		/// Although this method does get called after the <see cref="SaveInternal" /> method is called, there does not seem to be any reason to actually use it.
		/// The standard practice of inheriting <see cref="DataFormat" />s utilizes the <see cref="BeforeLoadInternal" />, <see cref="AfterLoadInternal" />, and
		/// <see cref="BeforeSaveInternal" /> methods.
		/// </remarks>
		/// <param name="objectModels">The stack of <see cref="ObjectModel"/>s used by this <see cref="DataFormat" />.</param>
		protected virtual void AfterSaveInternal(Stack<ObjectModel> objectModels)
		{
		}
	}
}
