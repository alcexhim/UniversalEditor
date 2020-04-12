//
//  SynthesizedAudioStylePlugin.cs
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
namespace UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized
{
	public class SynthesizedAudioStylePlugin
	{
		private Guid mvarID = Guid.Empty;
		private string mvarName = string.Empty;
		private Version mvarVersion = new Version(1, 0);
		public Guid ID
		{
			get
			{
				return this.mvarID;
			}
			set
			{
				this.mvarID = value;
			}
		}
		public string Name
		{
			get
			{
				return this.mvarName;
			}
			set
			{
				this.mvarName = value;
			}
		}
		public Version Version
		{
			get
			{
				return this.mvarVersion;
			}
			set
			{
				this.mvarVersion = value;
			}
		}
	}
}
