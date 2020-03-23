//
//  Reflection.cs
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
using System.Collections.Generic;
using System.Reflection;

namespace UniversalEditor.Printing
{
	public static class Reflection
	{
		private static PrintHandlerReference[] _phrs = null;
		public static PrintHandlerReference[] GetAvailablePrintHandlers()
		{
			if (_phrs == null)
			{
				List<PrintHandlerReference> list = new List<PrintHandlerReference>();
				Type[] types = MBS.Framework.Reflection.GetAvailableTypes(new Type[] { typeof(PrintHandler) });
				foreach (Type type in types)
				{
					if (type == null) continue;

					if (type.IsSubclassOf(typeof(PrintHandler)))
					{
						PrintHandler ph = (type.Assembly.CreateInstance(type.FullName) as PrintHandler);
						PrintHandlerReference phr = ph.MakeReference();

						list.Add(phr);
					}
				}
				_phrs = list.ToArray();
			}
			return _phrs;
		}

		public static PrintHandlerReference[] GetAvailablePrintHandlers(ObjectModel om)
		{
			List<PrintHandlerReference> list = new List<PrintHandlerReference>();
			PrintHandlerReference[] phrs = GetAvailablePrintHandlers();
			foreach (PrintHandlerReference phr in phrs)
			{
				if (phr.SupportedObjectModels.Contains(om.GetType()))
					list.Add(phr);
			}
			return list.ToArray();
		}
	}
}
