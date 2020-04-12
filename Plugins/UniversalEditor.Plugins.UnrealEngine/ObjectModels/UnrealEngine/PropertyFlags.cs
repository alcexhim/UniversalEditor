//
//  PropertyFlags.cs - indicates the attributes for properties in an Unreal Engine package file
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

namespace UniversalEditor.ObjectModels.UnrealEngine
{
	/// <summary>
	/// Indicates the attributes for properties in an Unreal Engine package file.
	/// </summary>
	[Flags()]
	public enum PropertyFlags
	{
		None = 0x00000000,
		/// <summary>
		/// Property is user-settable in the editor.
		/// </summary>
		Editable = 0x00000001,
		/// <summary>
		/// Actor's property always matches class's default actor property.
		/// </summary>
		Constant = 0x00000002,
		/// <summary>
		/// Variable is writable by the input system.
		/// </summary>
		InputWritable = 0x00000004,
		/// <summary>
		/// Object can be exported with actor.
		/// </summary>
		Exportable = 0x00000008,
		/// <summary>
		/// Optional parameter (if CPF_Param is set).
		/// </summary>
		OptionalParameter = 0x00000010,
		/// <summary>
		/// Property is relevant to network replication (not specified in source code)
		/// </summary>
		NetworkReplication = 0x00000020,
		/// <summary>
		/// Reference to a constant object.
		/// </summary>
		ConstantReference = 0x00000040,
		/// <summary>
		/// Function/When call parameter
		/// </summary>
		Parameter = 0x00000080,
		/// <summary>
		/// Value is copied out after function call.
		/// </summary>
		OutParameter = 0x00000100,
		/// <summary>
		/// Property is a short-circuitable evaluation function parm.
		/// </summary>
		SkipParameter = 0x00000200,
		/// <summary>
		/// Return value.
		/// </summary>
		ReturnParameter = 0x00000400,
		/// <summary>
		/// Coerce args into this function parameter
		/// </summary>
		CoerceParameter = 0x00000800,
		/// <summary>
		/// Property is native: C++ code is responsible for serializing it.
		/// </summary>
		Native = 0x00001000,
		/// <summary>
		/// Property is transient: shouldn't be saved, zero-filled at load time.
		/// </summary>
		Transient = 0x00002000,
		/// <summary>
		/// Property should be loaded/saved as permanent profile.
		/// </summary>
		Configuration = 0x00004000,
		/// <summary>
		/// Property should be loaded as localizable text
		/// </summary>
		Localized = 0x00008000,
		/// <summary>
		/// Property travels across levels/servers.
		/// </summary>
		Travel = 0x00010000,
		/// <summary>
		/// Property is uneditable in the editor
		/// </summary>
		PreventEdit = 0x00020000,
		/// <summary>
		/// Load config from base class, not subclass.
		/// </summary>
		GlobalConfiguration = 0x00040000,
		/// <summary>
		/// Object or dynamic array loaded on demand only.
		/// </summary>
		Demand = 0x00100000,
		/// <summary>
		/// Automatically create inner object
		/// </summary>
		AutoCreate = 0x00200000,
		/// <summary>
		/// Fields need construction/destruction (not specified in source code)
		/// </summary>
		ConstructionRequired = 0x00400000
	}
}
