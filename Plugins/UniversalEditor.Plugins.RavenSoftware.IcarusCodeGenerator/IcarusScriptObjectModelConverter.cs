//
//  MyClass.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2023 ${CopyrightHolder}
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
using UniversalEditor.ObjectModels.Icarus;
using UniversalEditor.ObjectModels.SourceCode;

namespace UniversalEditor.Plugins.RavenSoftware.IcarusCodeGenerator
{
	public class IcarusScriptObjectModelConverter : ObjectModelConverter
	{
		protected override void ConvertInternal(ObjectModel source, ObjectModel destination)
		{
			if (destination is CodeObjectModel)
			{

			}
		}
		protected override ObjectModelConversion[] GetSupportedConversionsInternal()
		{
			ObjectModelConversion[] supported = new ObjectModelConversion[]
			{
				new ObjectModelConversion
				(
					new ObjectModelReference(typeof(IcarusScriptObjectModel)),
					new ObjectModelReference(typeof(CodeObjectModel))
				)
			};
			return supported;
		}
	}
}
