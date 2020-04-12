//
//  CodeForLoopActionElement.cs - represents a CodeElement for a loop that runs continuously until a condition is met and executes a command on each pass
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

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements.CodeLoopActionElements
{
	/// <summary>
	/// Represents a <see cref="CodeElement" /> for a loop that runs continuously until a condition is met and executes a command on each pass.
	/// </summary>
	public class CodeForLoopActionElement : CodeLoopElement
	{
		/// <summary>
		/// Gets or sets the expression to use for the initialization of the loop.
		/// </summary>
		/// <value>The expression to use for the initialization of the loop.</value>
		public CodeElementReference Initialization { get; set; } = null;
		/// <summary>
		/// Gets or sets the expression to use for the condition that must be satisfied in order to continue the loop.
		/// </summary>
		/// <value>The expression to use for the condition that must be satisfied in order to continue the loop.</value>
		public CodeElementReference Condition { get; set; } = null;
		/// <summary>
		/// Gets or sets the expression which is called at the end of each iteration through the loop.
		/// </summary>
		/// <value>The expression which is called at the end of each iteration through the loop.</value>
		public CodeElementReference Increment { get; set; } = null;
	}
}
