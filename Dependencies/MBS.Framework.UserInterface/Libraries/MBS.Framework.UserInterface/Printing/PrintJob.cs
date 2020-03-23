//
//  PrintJob.cs
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

namespace MBS.Framework.UserInterface.Printing
{
	public class PrintJob : ISupportsExtraData
	{
		public string Title { get; set; } = String.Empty;
		public Printer Printer { get; set; } = null;

		public PrintSettings Settings { get; set; } = null;

		public event PrintEventHandler BeginPrint;
		public event PrintEventHandler DrawPage;

		protected virtual void OnDrawPage(PrintEventArgs e)
		{
			DrawPage?.Invoke(this, e);
		}

		public PrintJob(string title, Printer printer, PrintSettings settings = null)
		{
			Title = title;
			Printer = printer;
			Settings = settings;
		}

		private Dictionary<string, object> _ExtraData = new Dictionary<string, object>();
		public T GetExtraData<T>(string key, T defaultValue = default(T))
		{
			if (_ExtraData.ContainsKey(key)) return (T)_ExtraData[key];
			return defaultValue;
		}
		public object GetExtraData(string key, object defaultValue = null)
		{
			return GetExtraData<object>(key, defaultValue);
		}
		public void SetExtraData<T>(string key, T value)
		{
			_ExtraData[key] = value;
		}
		public void SetExtraData(string key, object value)
		{
			SetExtraData<object>(key, value);
		}

		/// <summary>
		/// Send this print job to the printer.
		/// </summary>
		public void Send()
		{
			Application.Engine.Print(this);
		}
	}
}
