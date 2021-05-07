using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Pose;

namespace UniversalEditor.Plugins.Multimedia3D.DataFormats.PolygonMovieMaker
{
	public class PoseDataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("Polygon Movie Maker pose data", new byte?[][] { new byte?[] { (byte)'V', (byte)'o', (byte)'c', (byte)'a', (byte)'l', (byte)'o', (byte)'i', (byte)'d', (byte)' ', (byte)'P', (byte)'o', (byte)'s', (byte)'e', (byte)' ', (byte)'D', (byte)'a', (byte)'t', (byte)'a', (byte)' ', (byte)'f', (byte)'i', (byte)'l', (byte)'e' } }, new string[] { "*.vpd" });
			dfr.Capabilities.Add(typeof(PoseObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PoseObjectModel pose = (objectModel as PoseObjectModel);
			if (pose == null) return;

			IO.TextReader tr = new IO.TextReader(base.Stream.BaseStream, Encoding.GetEncoding("shift-jis"));

			bool parentFileNameRead = false;
			bool totalNumberOfBonesRead = false;

			bool boneIDAndNameRead = false;
			bool boneTransRead = false;
			bool boneQuaternionRead = false;

			string parentFileName = String.Empty;
			int totalNumberOfBones = 0;
			int totalNumberOfBonesDefined = 0;

			tr.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);

			string signature = tr.ReadLine();

			PoseBone bone = null;

			while (!tr.EndOfStream)
			{
				string line = tr.ReadLine();
				if (String.IsNullOrEmpty(line)) continue;

				if (!parentFileNameRead)
				{
					parentFileName = line.Substring(0, line.IndexOf(';'));
					parentFileNameRead = true;


					if (parentFileName.EndsWith(".osm"))
					{
						parentFileName = parentFileName.Substring(0, parentFileName.Length - 4);
						pose.ModelName = parentFileName;
					}

					continue;
				}
				else if (!totalNumberOfBonesRead)
				{
					totalNumberOfBones = Int32.Parse(line.Substring(0, line.IndexOf(';')));
					totalNumberOfBonesRead = true;
					continue;
				}
				else if (totalNumberOfBonesRead)
				{
					if (!boneIDAndNameRead)
					{
						string BoneID = line.Substring(0, line.IndexOf('{'));
						string BoneName = line.Substring(line.IndexOf('{') + 1);

						if (bone == null) bone = new PoseBone();
						bone.BoneID = BoneID;
						bone.BoneName = BoneName;

						boneIDAndNameRead = true;
					}
					else if (!boneTransRead)
					{
						string values = line.Substring(0, line.IndexOf(';')).Trim();
						string[] valueValues = values.Split(',');

						double x = 0;
						double y = 0;
						double z = 0;

						if (valueValues.Length > 0)
						{
							x = Double.Parse(valueValues[0]);
							if (valueValues.Length > 1)
							{
								y = Double.Parse(valueValues[1]);
								if (valueValues.Length > 2)
								{
									z = Double.Parse(valueValues[2]);
								}
							}
						}

						if (bone == null) bone = new PoseBone();
						bone.Position = new PositionVector3(x, y, z);

						boneTransRead = true;
					}
					else if (!boneQuaternionRead)
					{
						string values = line.Substring(0, line.IndexOf(';')).Trim();
						string[] valueValues = values.Split(',');

						double x = 0;
						double y = 0;
						double z = 0;
						double w = 0;

						if (valueValues.Length > 0)
						{
							x = Double.Parse(valueValues[0]);
							if (valueValues.Length > 1)
							{
								y = Double.Parse(valueValues[1]);
								if (valueValues.Length > 2)
								{
									z = Double.Parse(valueValues[2]);
									if (valueValues.Length > 3)
									{
										w = Double.Parse(valueValues[3]);
									}
								}
							}
						}

						if (bone == null) bone = new PoseBone();
						bone.Quaternion = new PositionVector4(x, y, z, w);

						boneQuaternionRead = true;
					}
					else
					{
						if (line == "}")
						{
							boneIDAndNameRead = false;
							boneTransRead = false;
							boneQuaternionRead = false;
							totalNumberOfBonesDefined++;

							pose.Bones.Add(bone);
							bone = null;
						}
					}
				}
			}

			if (totalNumberOfBones != totalNumberOfBonesDefined)
			{
				// throw an error?
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
		}
	}
}
