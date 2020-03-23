//
//  Endpoint.cs - UE 5 proposed feature to represent a DF and Accessor pair
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	/// <summary>
	/// Represents an endpoint (<see cref="DataFormat"/>/<see cref="Accessor"/> pair) that
	/// defines how and where data is transferred.
	/// </summary>
	public class Endpoint
	{
		public class EndpointCollection
			: System.Collections.ObjectModel.Collection<Endpoint>
		{
		}

		private Accessor mvarAccessor = null;
		public Accessor Accessor { get { return mvarAccessor; } set { mvarAccessor = value; } }

		private DataFormat mvarDataFormat = null;
		public DataFormat DataFormat { get { return mvarDataFormat; } set { mvarDataFormat = value; } }
	}
}
