//
//  CertificateObjectModel.cs - provides an ObjectModel for manipulating X.509 security certificates
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

namespace UniversalEditor.ObjectModels.Security.Certificate
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating X.509 security certificates.
	/// </summary>
	public class CertificateObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Security certificate";
				_omr.Path = new string[] { "Security", "Certificate" };
			}
			return _omr;
		}
		public override void Clear()
		{
		}
		public override void CopyTo(ObjectModel where)
		{
		}
	}
}
