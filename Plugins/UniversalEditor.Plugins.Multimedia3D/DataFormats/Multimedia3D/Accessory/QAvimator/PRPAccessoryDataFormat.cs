//
//  PRPAccessoryDataFormat.cs - provides a DataFormat for manipulating accessory models in QAvimator PRP format
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

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia3D.Accessory;

namespace UniversalEditor.DataFormats.Multimedia3D.Accessory.QAvimator
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating accessory models in QAvimator PRP format.
	/// </summary>
	public class PRPAccessoryDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(AccessoryObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			AccessoryObjectModel accs = (objectModel as AccessoryObjectModel);
			if (accs == null) throw new ObjectModelNotSupportedException();

			Reader tr = base.Accessor.Reader;
			while (!tr.EndOfStream)
			{
				string propLine = tr.ReadLine();
				string[] propVals = propLine.Split(new char[] { ' ' }, "\"");
				if (propVals.Length != 11) continue;

				int propType = Int32.Parse(propVals[0]);
				double propPosX = Double.Parse(propVals[1]);
				double propPosY = Double.Parse(propVals[2]);
				double propPosZ = Double.Parse(propVals[3]);
				double propSclX = Double.Parse(propVals[4]);
				double propSclY = Double.Parse(propVals[5]);
				double propSclZ = Double.Parse(propVals[6]);
				double propRotX = Double.Parse(propVals[7]);
				double propRotY = Double.Parse(propVals[8]);
				double propRotZ = Double.Parse(propVals[9]);
				int propBoneIndex = Int32.Parse(propVals[10]);

				AccessoryItem acc = new AccessoryItem();
				acc.Title = propType.ToString();
				acc.BoneName = propBoneIndex.ToString();
				acc.Position = new PositionVector3(propPosX, propPosY, propPosZ);
				acc.Rotation = new PositionVector4(propRotX, propRotY, propRotZ);
				acc.Scale = new PositionVector3(propSclX, propSclY, propSclZ);
				accs.Accessories.Add(acc);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			AccessoryObjectModel accs = new AccessoryObjectModel();
			Writer tw = base.Accessor.Writer;
			foreach (AccessoryItem acc in accs.Accessories)
			{
				string accTitle = acc.Title;
				if (accTitle.Contains(" "))
				{
					accTitle = "\"" + accTitle + "\"";
				}
				string accBoneName = acc.BoneName;
				if (accBoneName.Contains(" "))
				{
					accBoneName = "\"" + accBoneName + "\"";
				}
				tw.WriteLine(accTitle + " " + acc.Position.ToString(" ", null, null) + " " + acc.Scale.ToString(" ", null, null) + " " + acc.Rotation.ToString(" ", null, null, 3) + accBoneName);
			}
			tw.Flush();
		}
	}
}
