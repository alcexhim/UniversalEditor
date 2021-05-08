//
//  ConnectionValue.cs - represents the actual connection between one Connection and another Connection
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

namespace UniversalEditor.ObjectModels.Designer
{
	/// <summary>
	/// Represents the actual connection between one <see cref="Connection" /> and another <see cref="Connection" />.
	/// </summary>
	/// <remarks>
	/// It may be more intuitive if we rename <see cref="ConnectionValue" /> to "Connection", and rename <see cref="Connection" /> to "Port".
	/// </remarks>
	public class ConnectionValue
	{
		public class ConnectionValueCollection
			 : System.Collections.ObjectModel.Collection<ConnectionValue>
		{
		}

		/// <summary>
		/// Gets or sets the source <see cref="Connection" /> from which this <see cref="ConnectionValue" /> originates.
		/// </summary>
		/// <value>The source <see cref="Connection" /> from which this <see cref="ConnectionValue" /> originates.</value>
		public Connection SourceConnection { get; set; } = null;
		/// <summary>
		/// Gets or sets the destination <see cref="Connection" /> to which this <see cref="ConnectionValue" /> attaches.
		/// </summary>
		/// <value>The destination <see cref="Connection" /> to which this <see cref="ConnectionValue" /> attaches.</value>
		public Connection DestinationConnection { get; set; } = null;

		public ConnectionValue(Connection source, Connection destination)
		{
			SourceConnection = source;
			DestinationConnection = destination;
		}

	}
}
