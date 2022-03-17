//
//  Template.cs - abstract base class providing common functionality for Universal Editor project and file templates
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
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.ObjectModels.Project;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor
{
	public class TemplateVariable
	{
		public class TemplateVariableCollection
			: System.Collections.ObjectModel.Collection<TemplateVariable>
		{
			public TemplateVariable this[string name]
			{
				get
				{
					foreach (TemplateVariable varr in this)
					{
						if (varr.Name == name) return varr;
					}
					return null;
				}
			}
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarDataType = String.Empty;
		public string DataType { get { return mvarDataType; } set { mvarDataType = value; } }

		private string mvarLabel = String.Empty;
		public string Label { get { return mvarLabel; } set { mvarLabel = value; } }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		private Dictionary<string, string> mvarChoices = new Dictionary<string, string>();
		public Dictionary<string, string> Choices { get { return mvarChoices; } }

	}
	/// <summary>
	/// Abstract base class providing common functionality for Universal Editor project and file templates.
	/// </summary>
	public abstract class Template
	{
		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string[] mvarPath = null;
		/// <summary>
		///
		/// </summary>
		public string[] Path { get { return mvarPath; } set { mvarPath = value; } }

		/// <summary>
		/// Gets or sets the template file name prefix.
		/// </summary>
		/// <value>The template file name prefix.</value>
		public string Prefix { get; set; } = String.Empty;

		private string mvarTitle = String.Empty;
		/// <summary>
		/// The title of the document template.
		/// </summary>
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string mvarDescription = String.Empty;
		/// <summary>
		/// A short summary of the purpose and content of the document template.
		/// </summary>
		public string Description { get { return mvarDescription; } set { mvarDescription = value; } }

		public string Prefix { get; set; } = null;

		private TemplateVariable.TemplateVariableCollection mvarVariables = new TemplateVariable.TemplateVariableCollection();
		/// <summary>
		/// Variables that affect the content of this template.
		/// </summary>
		public TemplateVariable.TemplateVariableCollection Variables { get { return mvarVariables; } }

		private string mvarLargeIconImageFileName = null;
		public string LargeIconImageFileName { get { return mvarLargeIconImageFileName; } set { mvarLargeIconImageFileName = value; } }

		private string mvarSmallIconImageFileName = null;
		public string SmallIconImageFileName { get { return mvarSmallIconImageFileName; } set { mvarSmallIconImageFileName = value; } }

		private string mvarPreviewImageFileName = null;
		public string PreviewImageFileName { get { return mvarPreviewImageFileName; } set { mvarPreviewImageFileName = value; } }
	}
	public class DocumentTemplate : Template
	{
		public class DocumentTemplateCollection
			: System.Collections.ObjectModel.Collection<DocumentTemplate>
		{

		}

		private ObjectModel mvarObjectModel = null;
		/// <summary>
		/// The complete object model that is used as the basis for the content provided by the template.
		/// </summary>
		public ObjectModel ObjectModel
		{
			get
			{
				if (mvarObjectModel == null) Create();
				return mvarObjectModel;
			}
			set { mvarObjectModel = value; }
		}

		private ObjectModelReference mvarObjectModelReference = null;
		/// <summary>
		/// A reference to the object model that is used to create the completed object model upon a call to <see cref="Create" />.
		/// </summary>
		public ObjectModelReference ObjectModelReference { get { return mvarObjectModelReference; } set { mvarObjectModelReference = value; } }

		private MarkupObjectModel mvarTemplateContent = new MarkupObjectModel();
		/// <summary>
		/// A <see cref="UniversalEditor.ObjectModels.Markup.MarkupObjectModel" /> that provides the content for this template.
		/// </summary>
		public MarkupObjectModel TemplateContent { get { return mvarTemplateContent; } }

		/// <summary>
		/// Initializes the template's ObjectModel with the content specified in <see cref="TemplateContent" />.
		/// </summary>
		public void Create()
		{
			mvarObjectModel = mvarObjectModelReference.Create();
			object om = mvarObjectModel;

			MarkupTagElement tagContent = (mvarTemplateContent.Elements["Content"] as MarkupTagElement);
			if (tagContent == null) return;

			Type type = mvarObjectModel.GetType();

			foreach (MarkupElement el in tagContent.Elements)
			{
				MarkupTagElement tag = (el as MarkupTagElement);
				RecursiveLoadTag(tag, ref om);
			}
		}

		private void RecursiveLoadTag(MarkupTagElement tag, ref object applyTo)
		{
			Type type = applyTo.GetType();

			System.Reflection.PropertyInfo pi = type.GetProperty(tag.Name);
			object piVal = pi.GetValue(applyTo, null);

			if (pi.PropertyType.GetInterface("System.Collections.IList") != null)
			{
				// This is a list. Iterate through the child elements in the XML
				foreach (MarkupElement el1 in tag.Elements)
				{
					MarkupTagElement tag1 = (el1 as MarkupTagElement);
					if (tag1 == null) continue;

					// create the instance of the object to add
					Type[] args = pi.PropertyType.BaseType.GetGenericArguments();
					object whatToAdd = RecursiveCreateObjectFromTag(tag1, args[0]);

					System.Reflection.MethodInfo[] mis = pi.PropertyType.GetMethods();

					System.Reflection.MethodInfo miAdd = pi.PropertyType.GetMethod("Add", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public, null, new Type[] { whatToAdd.GetType() }, null);
					miAdd.Invoke(piVal, new object[] { whatToAdd });
				}
			}
			else
			{
				// could this be text?
				if (tag.Elements.Count > 0 && tag.Elements[0] is MarkupStringElement)
				{
					MarkupStringElement strText = (tag.Elements[0] as MarkupStringElement);
					pi.SetValue(applyTo, strText.Value, null);
				}
			}
		}
		private object RecursiveCreateObjectFromTag(MarkupTagElement tag, Type objectType)
		{
			object obj = objectType.Assembly.CreateInstance(objectType.FullName);

			// create an instance of the object defined in type
			foreach (MarkupAttribute att in tag.Attributes)
			{
				System.Reflection.PropertyInfo pi = objectType.GetProperty(att.Name);
				if (pi == null) continue;

				pi.SetValue(obj, att.Value, null);
			}
			foreach (MarkupElement el1 in tag.Elements)
			{
				MarkupTagElement tag1 = (el1 as MarkupTagElement);
				if (tag1 == null) continue;

				RecursiveLoadTag(tag1, ref obj);
			}

			return obj;
		}
	}
	public class ProjectTemplate : Template
	{
		public class ProjectTemplateCollection
			 : System.Collections.ObjectModel.Collection<ProjectTemplate>
		{

		}

		public ProjectType.ProjectTypeCollection ProjectTypes { get; } = new ProjectType.ProjectTypeCollection();

		private ProjectFileSystem mvarFileSystem = new ProjectFileSystem();
		public ProjectFileSystem FileSystem { get { return mvarFileSystem; } }

		private PropertyListObjectModel mvarConfiguration = new PropertyListObjectModel();
		public PropertyListObjectModel Configuration { get { return mvarConfiguration; } }

		/// <summary>
		/// Creates a project from this project template.
		/// </summary>
		/// <returns></returns>
		public ProjectObjectModel Create()
		{
			ProjectObjectModel p = new ProjectObjectModel();
			for (int i = 0; i < ProjectTypes.Count; i++)
			{
				p.ProjectTypes.Add(ProjectTypes[i]);
			}
			mvarFileSystem.CopyTo(p.FileSystem);
			mvarConfiguration.CopyTo(p.Configuration);
			return p;
		}

	}
}
