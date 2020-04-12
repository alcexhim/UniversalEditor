//
//  IcarusCommandCamera.cs - represents the ICARUS "camera" command
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

using UniversalEditor.ObjectModels.Icarus.Expressions;
using UniversalEditor.ObjectModels.Icarus.Parameters;

namespace UniversalEditor.ObjectModels.Icarus.Commands
{
	/// <summary>
	/// Represents the ICARUS "camera" command.
	/// </summary>
	public class IcarusCommandCamera : IcarusPredefinedCommand
    {
        public override string Name
        {
            get { return "camera"; }
        }

        public IcarusCameraOperation Operation { get { return (IcarusCameraOperation)((IcarusConstantExpression)Parameters["Operation"].Value)?.Value; } set { ((IcarusConstantExpression)Parameters["Operation"].Value).Value = value; } }

		public IcarusCommandCamera()
		{
			Parameters.Add(new IcarusChoiceParameter("Operation", new IcarusConstantExpression(IcarusCameraOperation.None)));
		}

        public override object Clone()
        {
            IcarusCommandCamera clone = new IcarusCommandCamera();
            clone.Operation = Operation;
            return clone;
        }
    }
}
