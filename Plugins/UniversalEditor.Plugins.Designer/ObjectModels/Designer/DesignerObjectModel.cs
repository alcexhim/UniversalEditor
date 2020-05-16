//
//  DesignerObjectModel.cs - provides an ObjectModel for manipulating component designer layouts
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

using MBS.Framework.Drawing;

namespace UniversalEditor.ObjectModels.Designer
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating component designer layouts.
	/// </summary>
	public class DesignerObjectModel : ObjectModel
	{

		public override void Clear()
		{
			Designs.Clear();
			Libraries.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			throw new NotImplementedException();
		}

		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Designer";
			}
			return _omr;
		}

		/// <summary>
		/// Gets a collection of <see cref="Design" /> instances representing the component designer layouts in this <see cref="DesignerObjectModel" />.
		/// </summary>
		/// <value>The component designer layouts in this <see cref="DesignerObjectModel" />.</value>
		public Design.DesignCollection Designs { get; } = new Design.DesignCollection();
		/// <summary>
		/// Gets a collection of <see cref="Library" /> instances representing the component designer layout libraries referenced by this <see cref="DesignerObjectModel" />.
		/// </summary>
		/// <value>The component designer layout libraries referenced by this <see cref="DesignerObjectModel" />.</value>
		public Library.LibraryCollection Libraries { get; } = new Library.LibraryCollection();
	}
}
