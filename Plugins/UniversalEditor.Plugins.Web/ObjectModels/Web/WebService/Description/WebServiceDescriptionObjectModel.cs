//
//  WebServiceDescriptionObjectModel.cs - provides an ObjectModel for storing information about a Web Service
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
	/// Provides an <see cref="ObjectModel" /> for storing information about a Web Service.
	/// </summary>
	public class WebServiceDescriptionObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Web", "Services", "Web Service Description" };
			}
			return _omr;
		}

		public override void Clear()
		{
		}

		public override void CopyTo(ObjectModel where)
		{
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarDescription = String.Empty;
		public string Description { get { return mvarDescription; } set { mvarDescription = value; } }

		private Message.MessageCollection mvarMessages = new Message.MessageCollection();
		public Message.MessageCollection Messages { get { return mvarMessages; } }

		private Port.PortCollection mvarPorts = new Port.PortCollection();
		public Port.PortCollection Ports { get { return mvarPorts; } }
	}
}
