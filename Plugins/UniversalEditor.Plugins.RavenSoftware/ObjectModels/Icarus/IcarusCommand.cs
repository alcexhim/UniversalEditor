//
//  IcarusCommand.cs - the abstract base class from which all ICARUS command implementations derive
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

namespace UniversalEditor.ObjectModels.Icarus
{
	/// <summary>
	/// The abstract base class from which all ICARUS command implementations derive.
	/// </summary>
	public abstract class IcarusCommand : ICloneable
	{
		public class IcarusCommandCollection
			: System.Collections.ObjectModel.Collection<IcarusCommand>
		{
		}

		public IcarusParameter.IcarusParameterCollection Parameters { get; } = new IcarusParameter.IcarusParameterCollection();

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="IcarusCommand" /> is commented by the graphical editor (e.g. UniversalEditor or
		/// BehavEd).
		/// </summary>
		/// <value><c>true</c> if is commented; otherwise, <c>false</c>.</value>
		public bool IsCommented { get; set; } = false;

		public abstract object Clone();

		private static Dictionary<string, Type> _cmdsByName = null;
		public static IcarusCommand CreateFromName(string funcName)
		{
			if (_cmdsByName == null)
			{
				_cmdsByName = new Dictionary<string, Type>();
				Type[] cmdtypes = MBS.Framework.Reflection.GetAvailableTypes(new Type[] { typeof(IcarusCommand) });
				for (int i = 0; i < cmdtypes.Length; i++)
				{
					if (cmdtypes[i].IsAbstract)
						continue;

					IcarusCommand cmd = (cmdtypes[i].Assembly.CreateInstance(cmdtypes[i].FullName) as IcarusCommand);
					if (cmd is IcarusPredefinedCommand)
					{
						string nam = (cmd as IcarusPredefinedCommand).Name;
						if (!String.IsNullOrEmpty(nam))
							_cmdsByName[nam] = cmdtypes[i];
					}
				}
			}
			if (_cmdsByName.ContainsKey(funcName))
				return (_cmdsByName[funcName].Assembly.CreateInstance(_cmdsByName[funcName].FullName) as IcarusCommand);
			return null;
		}
	}
}
