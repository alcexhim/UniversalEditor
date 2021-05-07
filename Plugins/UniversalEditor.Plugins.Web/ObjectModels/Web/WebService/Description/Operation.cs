//
//  Operation.cs - WSDL Operation
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

namespace UniversalEditor.ObjectModels.Web.WebService.Description
{
	/// <summary>
	/// WSDL Operation
	/// </summary>
	public class Operation
	{
		public class OperationCollection
			: System.Collections.ObjectModel.Collection<Operation>
		{
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarDescription = String.Empty;
		public string Description { get { return mvarDescription; } set { mvarDescription = value; } }

		private Input.InputCollection mvarInputs = new Input.InputCollection();
		public Input.InputCollection Inputs { get { return mvarInputs; } }

		private Output.OutputCollection mvarOutputs = new Output.OutputCollection();
		public Output.OutputCollection Outputs { get { return mvarOutputs; } }

		private Fault.FaultCollection mvarFaults = new Fault.FaultCollection();
		public Fault.FaultCollection Faults { get { return mvarFaults; } }
	}
}
