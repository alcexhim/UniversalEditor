//
//  ExecutableObjectModel.cs - provides an ObjectModel for handling executable files
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

namespace UniversalEditor.ObjectModels.Executable
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for handling executable files.
	/// </summary>
	public class ExecutableObjectModel : ObjectModel
	{
		private ExecutableSection.ExecutableSectionCollection mvarSections = new ExecutableSection.ExecutableSectionCollection();
		public ExecutableSection.ExecutableSectionCollection Sections { get { return mvarSections; } }

		private ExecutableLibraryReference.ExecutableLibraryReferenceCollection mvarLibraryReferences = new ExecutableLibraryReference.ExecutableLibraryReferenceCollection();
		public ExecutableLibraryReference.ExecutableLibraryReferenceCollection LibraryReferences { get { return mvarLibraryReferences; } }

		private ExecutableMachine mvarTargetMachineType = ExecutableMachine.Unknown;
		public ExecutableMachine TargetMachineType { get { return mvarTargetMachineType; } set { mvarTargetMachineType = value; } }

		private ExecutableFunctionCall.ExecutableFunctionCallCollection mvarFunctionCalls = new ExecutableFunctionCall.ExecutableFunctionCallCollection();
		public ExecutableFunctionCall.ExecutableFunctionCallCollection FunctionCalls { get { return mvarFunctionCalls; } }
		
		#region Object Model Methods
		public override void Clear()
		{
			mvarSections.Clear();
			mvarLibraryReferences.Clear();
			mvarTargetMachineType = ExecutableMachine.Unknown;
			mvarCharacteristics = ExecutableCharacteristics.None;
			mvarLibraryCharacteristics = ExecutableLibraryCharacteristics.None;
			mvarRelativeVirtualAddresses.Clear();
			mvarInstructions.Clear();
			mvarFunctionCalls.Clear();
			mvarSubsystem = ExecutableSubsystem.Unknown;
			mvarTimestamp = DateTime.Now;
		}
		public override void CopyTo(ObjectModel where)
		{
			ExecutableObjectModel clone = (where as ExecutableObjectModel);
			foreach (ExecutableSection sect in mvarSections)
			{
				clone.Sections.Add(sect.Clone() as ExecutableSection);
			}
			foreach (ExecutableLibraryReference lref in mvarLibraryReferences)
			{
				clone.LibraryReferences.Add(lref.Clone() as ExecutableLibraryReference);
			}
			clone.TargetMachineType = mvarTargetMachineType;
			clone.Characteristics = mvarCharacteristics;
			clone.LibraryCharacteristics = mvarLibraryCharacteristics;
			foreach (ExecutableRelativeVirtualAddress rva in mvarRelativeVirtualAddresses)
			{
				clone.RelativeVirtualAddresses.Add(rva.Clone() as ExecutableRelativeVirtualAddress);
			}
			foreach (ExecutableFunctionCall func in mvarFunctionCalls)
			{
				clone.FunctionCalls.Add(func.Clone() as ExecutableFunctionCall);
			}
			clone.Subsystem = mvarSubsystem;
			clone.Timestamp = mvarTimestamp;
		}
		protected override ObjectModelReference MakeReferenceInternal()
		{
			ObjectModelReference omr = base.MakeReferenceInternal();
			omr.Title = "Executable";
			omr.Path = new string[] { "Software Development", "Executable" };
			return omr;
		}
		#endregion

		private ExecutableCharacteristics mvarCharacteristics = ExecutableCharacteristics.None;
		public ExecutableCharacteristics Characteristics { get { return mvarCharacteristics; } set { mvarCharacteristics = value; } }

		private ExecutableLibraryCharacteristics mvarLibraryCharacteristics = ExecutableLibraryCharacteristics.None;
		public ExecutableLibraryCharacteristics LibraryCharacteristics { get { return mvarLibraryCharacteristics; } set { mvarLibraryCharacteristics = value; } }

		private ExecutableLoaderFlags mvarLoaderFlags = ExecutableLoaderFlags.None;
		public ExecutableLoaderFlags LoaderFlags { get { return mvarLoaderFlags; } set { mvarLoaderFlags = value; } }

		private ExecutableSubsystem mvarSubsystem = ExecutableSubsystem.Unknown;
		public ExecutableSubsystem Subsystem { get { return mvarSubsystem; } set { mvarSubsystem = value; } }

		private DateTime mvarTimestamp = DateTime.Now;
		public DateTime Timestamp { get { return mvarTimestamp; } set { mvarTimestamp = value; } }

		private ExecutableRelativeVirtualAddress.ExecutableRelativeVirtualAddressCollection mvarRelativeVirtualAddresses = new ExecutableRelativeVirtualAddress.ExecutableRelativeVirtualAddressCollection();
		public ExecutableRelativeVirtualAddress.ExecutableRelativeVirtualAddressCollection RelativeVirtualAddresses { get { return mvarRelativeVirtualAddresses; } }

		private ExecutableInstruction.ExecutableInstructionCollection mvarInstructions = new ExecutableInstruction.ExecutableInstructionCollection();
		public ExecutableInstruction.ExecutableInstructionCollection Instructions { get { return mvarInstructions; } }

		/// <summary>
		/// The <see cref="System.Reflection.Assembly" /> for this <see cref="ExecutableObjectModel" />.
		/// </summary>
		/// <value>The managed assembly.</value>
		public System.Reflection.Assembly ManagedAssembly { get; set; }
	}
}
