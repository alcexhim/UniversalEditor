//
//  SceneObjectModel.cs - provides an ObjectModel for manipulating 3D scene graphs
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

using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.ObjectModels.Multimedia3D.Scene
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating 3D scene graphs.
	/// </summary>
	public class SceneObjectModel : ObjectModel
	{
		protected override ObjectModelReference MakeReferenceInternal()
		{
			ObjectModelReference omr = base.MakeReferenceInternal();
			omr.Title = "Animation scene";
			omr.Path = new string[] { "Multimedia", "3D Multimedia", "3D Scene" };
			omr.Description = "Stores model settings and camera settings for an animated or static scene in 3D space.";
			return omr;
		}

		public uint ImageWidth { get; set; } = 512;
		public uint ImageHeight { get; set; } = 384;

		public bool FPSVisible { get; set; } = false;
		public bool CoordinateAxisVisible { get; set; } = true;
		public bool GroundShadowVisible { get; set; } = true;
		public bool GroundShadowTransparent { get; set; } = false;
		public SceneScreenCaptureMode ScreenCaptureMode { get; set; } = SceneScreenCaptureMode.None;
		public float GroundShadowBrightness { get; set; } = 1.0f;

		public SceneModelReference.SceneModelReferenceCollection Models { get; } = new SceneModelReference.SceneModelReferenceCollection();
		public SceneBrush.SceneBrushCollection Brushes { get; } = new SceneBrush.SceneBrushCollection();
		public ModelVertex.ModelVertexCollection Vertices { get; } = new ModelVertex.ModelVertexCollection();

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="T:UniversalEditor.ObjectModels.Multimedia3D.Scene.SceneObjectModel"/> is visible.
		/// </summary>
		/// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
		public bool Visible { get; set; } = true;

		public override void CopyTo(ObjectModel destination)
		{
			SceneObjectModel clone = destination as SceneObjectModel;
			foreach (SceneModelReference smr in this.Models)
			{
				clone.Models.Add(smr.Clone() as SceneModelReference);
			}

			clone.ImageWidth = ImageWidth;
			clone.ImageHeight = ImageHeight;
		}
		public override void Clear()
		{
			Models.Clear();

			ImageWidth = 512;
			ImageHeight = 384;
		}

		/// <summary>
		/// Gets or sets the frame rate limit.
		/// </summary>
		/// <value>The frame rate limit.</value>
		public float FPSLimit { get; set; } = 60.0f;
	}
}
