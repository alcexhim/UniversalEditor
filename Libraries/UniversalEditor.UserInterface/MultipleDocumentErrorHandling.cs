//
//  MultipleDocumentErrorHandling.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
namespace UniversalEditor.UserInterface
{
	/// <summary>
	/// Describes the way in which to handle an <see cref="Exception" /> when
	/// there are additional <see cref="Document" />s to be processed afterward.
	/// </summary>
	public enum MultipleDocumentErrorHandling
	{
		/// <summary>
		/// Ignore the error for the current <see cref="Document" /> and proceed
		/// to processing the next <see cref="Document" />.
		/// </summary>
		Ignore,
		/// <summary>
		/// Cancels the operation for a single <see cref="Document" />. Other
		/// <see cref="Document" />s will continue to be processed.
		/// </summary>
		CancelOne,
		/// <summary>
		/// Cancels the entire operation, including any <see cref="Document" />s
		/// that still have yet to be processed.
		/// </summary>
		CancelAll
	}
}
