//
//  CodeObjectModel.cs - provides an ObjectModel for manipulating source code
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

using System.Collections.Generic;

using UniversalEditor.ObjectModels.SourceCode.CodeElements;
using UniversalEditor.ObjectModels.SourceCode.SearchExpressions;

namespace UniversalEditor.ObjectModels.SourceCode
{
	/// <summary>
	/// Provides an <see cref="CodeObjectModel" /> for manipulating source code.
	/// </summary>
	public class CodeObjectModel : ObjectModel
	{
		private ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Programming", "Code File" };
				_omr.Title = "Source code";
			}
			return _omr;
		}

		public override void Clear()
		{
			mvarElements.Clear();
		}

		public override void CopyTo(ObjectModel destination)
		{
			CodeObjectModel clone = (destination as CodeObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();

			foreach (CodeElement element in mvarElements)
			{
				clone.Elements.Add(element.Clone() as CodeElement);
			}
		}

		private CodeElement.CodeElementCollection mvarElements = new CodeElement.CodeElementCollection();
		public CodeElement.CodeElementCollection Elements
		{
			get { return mvarElements; }
		}

		public T FindElement<T>(string Name) where T : CodeElement, INamedCodeElement
		{
			foreach (CodeElement e in mvarElements)
			{
				if (!(e is INamedCodeElement)) continue;

				if (e is T && (e as INamedCodeElement).Name == Name)
				{
					return (e as T);
				}
				else if (e is CodeElementContainerElement)
				{
					T ce = (e as CodeElementContainerElement).FindElement<T>(Name);
					if (ce != null) return ce;
				}
			}
			return null;
		}

		public void Replace(CodeMethodCallActionSearch find, CodeMethodCallActionSearch replace)
		{
			Replace(find, replace, null);
		}
		private void Replace(CodeMethodCallActionSearch find, CodeMethodCallActionSearch replace, CodeElementContainerElement parent)
		{
			CodeElement.CodeElementCollection elements = null;
			if (parent == null)
			{
				elements = mvarElements;
			}
			else
			{
				elements = parent.Elements;
			}

			foreach (CodeElement e in elements)
			{
				if (e is CodeMethodElement)
				{
					CodeMethodElement meth = (e as CodeMethodElement);
					foreach (CodeElement action in meth.Elements)
					{
						CodeMethodCallElement metha = (action as CodeMethodCallElement);
						if (metha != null)
						{
							if (metha.ObjectName.Match(find.ObjectName) && metha.MethodName == find.MethodName)
							{
								metha.ObjectName = (replace.ObjectName.Clone() as string[]);
								metha.MethodName = replace.MethodName;

								Dictionary<string, CodeElementReference> reffs = new Dictionary<string, CodeElementReference>();
								for (int i = 0; i < find.Parameters.Count; i++)
								{
									if (i >= replace.Parameters.Count) break;

									if (find.Parameters[i] is CodeMethodSearchParameterValue)
									{
									}
									else if (find.Parameters[i] is CodeMethodSearchParameterReference)
									{
										CodeMethodSearchParameterReference reff = (find.Parameters[i] as CodeMethodSearchParameterReference);

										CodeElementReference cer = null;
										if (i >= 0 && i < metha.Parameters.Count)
										{
											cer = metha.Parameters[i].Value;
										}
										else
										{
											cer = new CodeElementReference(reff.DefaultValue);
										}
										reffs.Add(reff.Name, cer);
									}
								}

								for (int i = 0; i < replace.Parameters.Count; i++)
								{
									if (replace.Parameters[i] is CodeMethodSearchParameterValue)
									{
										CodeMethodSearchParameterValue val = (replace.Parameters[i] as CodeMethodSearchParameterValue);
										if (i > metha.Parameters.Count)
										{
											metha.Parameters.Add("Parameter" + (i + 1).ToString(), CodeDataType.Object, new CodeElementReference(val.Value));
										}
										else
										{
											metha.Parameters[i].Value = new CodeElementReference(val.Value);
										}
									}
									else if (replace.Parameters[i] is CodeMethodSearchParameterReference)
									{
										CodeMethodSearchParameterReference val = (replace.Parameters[i] as CodeMethodSearchParameterReference);
										if (reffs.ContainsKey(val.Name))
										{
											if (i >= metha.Parameters.Count)
											{
												metha.Parameters.Add("Parameter" + (i + 1).ToString(), CodeDataType.Object, reffs[val.Name]);
											}
											else
											{
												metha.Parameters[i].Value = reffs[val.Name];
											}
										}
										else
										{
											if (i > metha.Parameters.Count)
											{
												metha.Parameters.Add("Parameter" + (i + 1).ToString(), CodeDataType.Object, new CodeElementReference(val.DefaultValue));
											}
											else
											{
												metha.Parameters[i].Value = new CodeElementReference(val.DefaultValue);
											}
										}
									}
								}
							}
						}
					}
				}
				else if (e is CodeElementContainerElement)
				{
					Replace(find, replace, (e as CodeElementContainerElement));
				}
			}
		}
	}
}
