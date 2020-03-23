//
//  KnownContexts.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
namespace MBS.Framework.UserInterface
{
	public static class KnownContexts
	{
		public static Context Default { get; } = new Context(Guid.Empty, "Default");
		public static Context System { get; } = new Context(new Guid("{0A3EFF85-F8D5-49AC-AA11-A69BB3752DA2}"), "System");
		public static Context Application { get; } = new Context(new Guid("{017EED75-B294-4DA3-808C-EE90770F7A46}"), "Application");
		public static Context User { get; } = new Context(new Guid("{1C3E3C35-8205-48D2-B822-37834906C908}"), "User");
	}
}
