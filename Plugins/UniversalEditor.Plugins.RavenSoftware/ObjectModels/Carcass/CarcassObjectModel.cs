//
//  CarcassObjectModel.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
namespace UniversalEditor.Plugins.RavenSoftware.ObjectModels.Carcass
{
	public class CarcassObjectModel : ObjectModel
	{
		public CarcassObjectModel()
		{
		}

		public ModelReference.ModelReferenceCollection ModelReferences { get; } = new ModelReference.ModelReferenceCollection();
		public string BasePath { get; set; }

		public bool MakeSkin { get; set; } = false;

		/// <summary>
		/// Gets or sets the scale at which the skeleton should be made when <see cref="MakeSkeleton"/> is <c>true</c>.
		/// </summary>
		/// <value>The scale.</value>
		public double Scale { get; set; } = 1.0;
		/// <summary>
		/// Gets or sets the name of the Ghoul2 animation (GLA) file from which to clone bone indices.
		/// </summary>
		/// <value>The name of the Ghoul2 animation (GLA) file from which to clone bone indices.</value>
		public string GLAFileName { get; set; } = null;
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="CarcassObjectModel"/> keep motion bone.
		/// </summary>
		/// <value><c>true</c> if keep motion bone; otherwise, <c>false</c>.</value>
		public bool KeepMotionBone { get; set; } = false;
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="CarcassObjectModel"/> smooth all surfaces.
		/// </summary>
		/// <value><c>true</c> if smooth all surfaces; otherwise, <c>false</c>.</value>
		public bool SmoothAllSurfaces { get; set; } = false;
		/// <summary>
		/// Gets or sets a value indicating whether duplicate vertices should be removed when compiling this <see cref="CarcassObjectModel"/>.
		/// </summary>
		/// <value><c>true</c> if duplicate vertices should be removed; otherwise, <c>false</c>.</value>
		public bool RemoveDuplicateVertices { get; set; } = false;
		public PositionVector3 Origin { get; set; } = PositionVector3.Empty;

		/// <summary>
		/// Gets the pcj.
		/// </summary>
		/// <value>The pcj.</value>
		public System.Collections.Specialized.StringCollection PCJ { get; } = new System.Collections.Specialized.StringCollection();
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="CarcassObjectModel"/> uses (older) pre-quaternian animation-compression.
		/// </summary>
		/// <value><c>true</c> if (older) pre-quaternian animation-compression should be used; otherwise, <c>false</c>.</value>
		public bool UseLegacyCompression { get; set; } = false;
		public string SkeletonFileName { get; set; } = null;
		public int Framestep { get; set; } = 1;

		public override void Clear()
		{
			ModelReferences.Clear();
			BasePath = null;
			MakeSkin = false;
			Scale = 1.0;
			GLAFileName = null;
			KeepMotionBone = false;
			SmoothAllSurfaces = false;
			RemoveDuplicateVertices = false;
			Origin = PositionVector3.Empty;
			UseLegacyCompression = false;
			SkeletonFileName = null;
			Framestep = 1;
		}

		public override void CopyTo(ObjectModel where)
		{
			CarcassObjectModel clone = (where as CarcassObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();

			for (int i = 0; i < ModelReferences.Count; i++)
			{
				clone.ModelReferences.Add(ModelReferences[i].Clone() as ModelReference);
			}
			clone.BasePath = (BasePath?.Clone() as string);
			clone.MakeSkin = MakeSkin;
			clone.Scale = Scale;
			clone.GLAFileName = (GLAFileName?.Clone() as string);
			clone.KeepMotionBone = KeepMotionBone;
			clone.SmoothAllSurfaces = SmoothAllSurfaces;
			clone.RemoveDuplicateVertices = RemoveDuplicateVertices;
			clone.Origin = (PositionVector3)Origin.Clone();
			for (int i = 0; i < PCJ.Count; i++)
			{
				clone.PCJ.Add(PCJ[i].Clone() as string);
			}
			clone.UseLegacyCompression = UseLegacyCompression;
			clone.SkeletonFileName = (SkeletonFileName?.Clone() as string);
			clone.Framestep = Framestep;
		}
	}
}
