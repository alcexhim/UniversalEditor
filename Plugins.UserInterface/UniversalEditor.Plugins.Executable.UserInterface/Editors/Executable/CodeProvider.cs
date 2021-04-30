//
//  CodeProvider.cs - the abstract base class from which all MSIL code translators inherit
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
using System.Reflection;

namespace UniversalEditor.Plugins.Executable.UserInterface.Editors.Executable
{
	/// <summary>
	/// The abstract base class from which all MSIL code translators inherit.
	/// </summary>
	public abstract class CodeProvider
	{
		/// <summary>
		/// Gets the <see cref="CodeProviders.CSharpCodeProvider" />
		/// </summary>
		/// <value>The C# code provider.</value>
		public static CodeProviders.CSharpCodeProvider CSharp { get; } = new CodeProviders.CSharpCodeProvider();

		public abstract string Title { get; }
		public abstract string CodeFileExtension { get; }

		protected abstract string GetAccessModifiersInternal(bool isPublic, bool isFamily, bool isAssembly, bool isPrivate, bool isAbstract, bool isSealed);

		public string GetAccessModifiers(EventInfo ei)
		{
			MethodInfo miAdd = ei.GetAddMethod();
			MethodInfo miRemove = ei.GetRemoveMethod();
			MethodInfo miRaise = ei.GetRaiseMethod();

			bool IsPublic = false;
			bool IsFamily = false;
			bool IsAssembly = false;
			bool IsPrivate = false;

			if (miAdd != null)
			{
				IsPublic = miAdd.IsPublic;
				IsFamily = miAdd.IsFamily;
				IsAssembly = miAdd.IsAssembly;
				IsPrivate = miAdd.IsPrivate;
			}
			else if (miRemove != null)
			{
				IsPublic = miRemove.IsPublic;
				IsFamily = miRemove.IsFamily;
				IsAssembly = miRemove.IsAssembly;
				IsPrivate = miRemove.IsPrivate;
			}
			else if (miRaise != null)
			{
				IsPublic = miRaise.IsPublic;
				IsFamily = miRaise.IsFamily;
				IsAssembly = miRaise.IsAssembly;
				IsPrivate = miRaise.IsPrivate;
			}
			else
			{
			}

			return GetAccessModifiersInternal(IsPublic, IsFamily, IsAssembly, IsPrivate, false, false);
		}
		public string GetAccessModifiers(MethodInfo mi)
		{
			bool IsPublic = mi.IsPublic;
			bool IsFamily = mi.IsFamily;
			bool IsAssembly = mi.IsAssembly;
			bool IsPrivate = mi.IsPrivate;
			return GetAccessModifiersInternal(IsPublic, IsFamily, IsAssembly, IsPrivate, mi.IsAbstract, false);
		}
		public string GetAccessModifiers(FieldInfo mi)
		{
			bool IsPublic = mi.IsPublic;
			bool IsFamily = mi.IsFamily;
			bool IsAssembly = mi.IsAssembly;
			bool IsPrivate = mi.IsPrivate;
			return GetAccessModifiersInternal(IsPublic, IsFamily, IsAssembly, IsPrivate, false, false);
		}
		public string GetAccessModifiers(PropertyInfo mi)
		{
			bool IsPublic = (mi.GetGetMethod()?.IsPublic).GetValueOrDefault() || (mi.GetSetMethod()?.IsPublic).GetValueOrDefault();
			bool IsFamily = (mi.GetGetMethod()?.IsFamily).GetValueOrDefault() && (mi.GetSetMethod()?.IsFamily).GetValueOrDefault();
			bool IsAssembly = (mi.GetGetMethod()?.IsAssembly).GetValueOrDefault() && (mi.GetSetMethod()?.IsAssembly).GetValueOrDefault();
			bool IsPrivate = (mi.GetGetMethod()?.IsPrivate).GetValueOrDefault() && (mi.GetSetMethod()?.IsPrivate).GetValueOrDefault();
			return GetAccessModifiersInternal(IsPublic, IsFamily, IsAssembly, IsPrivate, false, false);
		}
		public string GetAccessModifiers(Type mi)
		{
			bool IsPublic = mi.IsPublic;
			bool IsAbstract = mi.IsAbstract;
			bool IsSealed = mi.IsSealed;
			return GetAccessModifiersInternal(IsPublic, false, false, false, IsAbstract, IsSealed);
		}

		protected abstract string GetBeginBlockInternal(int indentLevel);
		public string GetBeginBlock(int indentLevel)
		{
			return GetBeginBlockInternal(indentLevel);
		}
		protected abstract string GetBeginBlockInternal(Type type, int indentLevel);
		public string GetBeginBlock(Type type, int indentLevel)
		{
			return GetBeginBlockInternal(type, indentLevel);
		}
		protected abstract string GetEndBlockInternal(string elementName, int indentLevel);
		public string GetEndBlock(Type type, int indentLevel)
		{
			return GetEndBlockInternal(GetElementName(type), indentLevel);
		}

		protected abstract string GetBeginBlockInternal(System.Reflection.MethodInfo mi, int indentLevel);
		public string GetBeginBlock(System.Reflection.MethodInfo mi, int indentLevel)
		{
			return GetBeginBlockInternal(mi, indentLevel);
		}
		protected abstract string GetEndBlockInternal(System.Reflection.MethodInfo mi, int indentLevel);
		public string GetEndBlock(System.Reflection.MethodInfo mi, int indentLevel)
		{
			return GetEndBlockInternal(GetElementName(mi), indentLevel);
		}

		protected abstract string GetElementNameInternal(System.Reflection.MethodInfo mi);
		public string GetElementName(System.Reflection.MethodInfo mi)
		{
			return GetElementNameInternal(mi);
		}
		protected abstract string GetElementNameInternal(Type type);
		public string GetElementName(Type type)
		{
			return GetElementNameInternal(type);
		}

		protected abstract string GetSourceCodeInternal(Type mi, int indentLevel);
		public string GetSourceCode(Type mi, int indentLevel)
		{
			return GetSourceCodeInternal(mi, indentLevel);
		}
		protected abstract string GetSourceCodeInternal(System.Reflection.EventInfo item, int indentLevel);
		public string GetSourceCode(System.Reflection.EventInfo item, int indentLevel)
		{
			return GetSourceCodeInternal(item, indentLevel);
		}
		protected abstract string GetSourceCodeInternal(System.Reflection.FieldInfo mi, int indentLevel);
		public string GetSourceCode(System.Reflection.FieldInfo mi, int indentLevel)
		{
			return GetSourceCodeInternal(mi, indentLevel);
		}
		protected abstract string GetSourceCodeInternal(System.Reflection.MethodInfo mi, int indentLevel);
		public string GetSourceCode(System.Reflection.MethodInfo mi, int indentLevel)
		{
			return GetSourceCodeInternal(mi, indentLevel);
		}
		protected abstract string GetSourceCodeInternal(System.Reflection.PropertyInfo item, bool autoProperty, int indentLevel);
		public string GetSourceCode(System.Reflection.PropertyInfo item, bool autoProperty, int indentLevel)
		{
			return GetSourceCodeInternal(item, autoProperty, indentLevel);
		}

		protected abstract string GetTypeNameInternal(Type type);
		public string GetTypeName(Type type)
		{
			return GetTypeNameInternal(type);
		}

		protected abstract string GetMethodSignatureInternal(System.Reflection.MethodInfo mi);
		public string GetMethodSignature(System.Reflection.MethodInfo mi)
		{
			return GetMethodSignatureInternal(mi);
		}
	}
}
