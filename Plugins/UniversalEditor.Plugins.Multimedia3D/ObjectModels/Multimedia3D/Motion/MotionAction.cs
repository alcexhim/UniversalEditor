//
//  MotionAction.cs - classes representing actions in a 3D animation file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2013-2020 Mike Becker's Software
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
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia3D.Motion
{
	/// <summary>
	/// The abstract base class from which all 3D animation actions derive.
	/// </summary>
	public abstract class MotionAction : ICloneable
	{
		public class MotionActionCollection
			: System.Collections.ObjectModel.Collection<MotionAction>
		{
		}

		public abstract object Clone();
	}
	/// <summary>
	/// Repositions the specified face on the current model.
	/// </summary>
	public class MotionFaceRepositionAction : MotionAction
	{
		/// <summary>
		/// The name of the face to which this action is applied.
		/// </summary>
		public string FaceName { get; set; } = String.Empty;
		/// <summary>
		/// The weight of the face.
		/// </summary>
		public float Weight { get; set; } = 0.0f;

		public override object Clone()
		{
			MotionFaceRepositionAction clone = new MotionFaceRepositionAction();
			clone.FaceName = (FaceName.Clone() as string);
			clone.Weight = Weight;
			return clone;
		}
	}
	/// <summary>
	/// Repositions the specified bone on the current model.
	/// </summary>
	public class MotionBoneRepositionAction : MotionAction
	{
		/// <summary>
		/// The name of the bone to which this action is applied.
		/// </summary>
		public string BoneName { get; set; } = String.Empty;

		public PositionVector3 Position { get; set; } = new PositionVector3();
		public PositionVector4 Rotation { get; set; } = new PositionVector4();

		public MotionInterpolationData Interpolation { get; } = new MotionInterpolationData();
		public MotionInterpolationData Interpolation2 { get; } = new MotionInterpolationData();
		public MotionInterpolationData Interpolation3 { get; } = new MotionInterpolationData();
		public MotionInterpolationData Interpolation4 { get; } = new MotionInterpolationData();

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Reposition: ");
			sb.Append(" bone \"");
			sb.Append(BoneName);
			sb.Append("\" @ pos ");
			sb.Append(Position.ToString());
			sb.Append("; rot ");
			sb.Append(Rotation.ToString());
			return sb.ToString();
		}

		public override object Clone()
		{
			MotionBoneRepositionAction clone = new MotionBoneRepositionAction();
			clone.BoneName = (BoneName.Clone() as string);

			clone.Interpolation.Rotation.X1 = Interpolation.Rotation.X1;
			clone.Interpolation.Rotation.X2 = Interpolation.Rotation.X2;
			clone.Interpolation.Rotation.Y1 = Interpolation.Rotation.Y1;
			clone.Interpolation.Rotation.Y2 = Interpolation.Rotation.Y2;
			clone.Interpolation.XAxis.X1 = Interpolation.XAxis.X1;
			clone.Interpolation.XAxis.X2 = Interpolation.XAxis.X2;
			clone.Interpolation.XAxis.Y1 = Interpolation.XAxis.Y1;
			clone.Interpolation.XAxis.Y2 = Interpolation.XAxis.Y2;
			clone.Interpolation.YAxis.X1 = Interpolation.YAxis.X1;
			clone.Interpolation.YAxis.X2 = Interpolation.YAxis.X2;
			clone.Interpolation.YAxis.Y1 = Interpolation.YAxis.Y1;
			clone.Interpolation.YAxis.Y2 = Interpolation.YAxis.Y2;
			clone.Interpolation.ZAxis.X1 = Interpolation.ZAxis.X1;
			clone.Interpolation.ZAxis.X2 = Interpolation.ZAxis.X2;
			clone.Interpolation.ZAxis.Y1 = Interpolation.ZAxis.Y1;
			clone.Interpolation.ZAxis.Y2 = Interpolation.ZAxis.Y2;

			clone.Interpolation2.Rotation.X1 = Interpolation2.Rotation.X1;
			clone.Interpolation2.Rotation.X2 = Interpolation2.Rotation.X2;
			clone.Interpolation2.Rotation.Y1 = Interpolation2.Rotation.Y1;
			clone.Interpolation2.Rotation.Y2 = Interpolation2.Rotation.Y2;
			clone.Interpolation2.XAxis.X1 = Interpolation2.XAxis.X1;
			clone.Interpolation2.XAxis.X2 = Interpolation2.XAxis.X2;
			clone.Interpolation2.XAxis.Y1 = Interpolation2.XAxis.Y1;
			clone.Interpolation2.XAxis.Y2 = Interpolation2.XAxis.Y2;
			clone.Interpolation2.YAxis.X1 = Interpolation2.YAxis.X1;
			clone.Interpolation2.YAxis.X2 = Interpolation2.YAxis.X2;
			clone.Interpolation2.YAxis.Y1 = Interpolation2.YAxis.Y1;
			clone.Interpolation2.YAxis.Y2 = Interpolation2.YAxis.Y2;
			clone.Interpolation2.ZAxis.X1 = Interpolation2.ZAxis.X1;
			clone.Interpolation2.ZAxis.X2 = Interpolation2.ZAxis.X2;
			clone.Interpolation2.ZAxis.Y1 = Interpolation2.ZAxis.Y1;
			clone.Interpolation2.ZAxis.Y2 = Interpolation2.ZAxis.Y2;

			clone.Interpolation3.Rotation.X1 = Interpolation3.Rotation.X1;
			clone.Interpolation3.Rotation.X2 = Interpolation3.Rotation.X2;
			clone.Interpolation3.Rotation.Y1 = Interpolation3.Rotation.Y1;
			clone.Interpolation3.Rotation.Y2 = Interpolation3.Rotation.Y2;
			clone.Interpolation3.XAxis.X1 = Interpolation3.XAxis.X1;
			clone.Interpolation3.XAxis.X2 = Interpolation3.XAxis.X2;
			clone.Interpolation3.XAxis.Y1 = Interpolation3.XAxis.Y1;
			clone.Interpolation3.XAxis.Y2 = Interpolation3.XAxis.Y2;
			clone.Interpolation3.YAxis.X1 = Interpolation3.YAxis.X1;
			clone.Interpolation3.YAxis.X2 = Interpolation3.YAxis.X2;
			clone.Interpolation3.YAxis.Y1 = Interpolation3.YAxis.Y1;
			clone.Interpolation3.YAxis.Y2 = Interpolation3.YAxis.Y2;
			clone.Interpolation3.ZAxis.X1 = Interpolation3.ZAxis.X1;
			clone.Interpolation3.ZAxis.X2 = Interpolation3.ZAxis.X2;
			clone.Interpolation3.ZAxis.Y1 = Interpolation3.ZAxis.Y1;
			clone.Interpolation3.ZAxis.Y2 = Interpolation3.ZAxis.Y2;

			clone.Interpolation4.Rotation.X1 = Interpolation4.Rotation.X1;
			clone.Interpolation4.Rotation.X2 = Interpolation4.Rotation.X2;
			clone.Interpolation4.Rotation.Y1 = Interpolation4.Rotation.Y1;
			clone.Interpolation4.Rotation.Y2 = Interpolation4.Rotation.Y2;
			clone.Interpolation4.XAxis.X1 = Interpolation4.XAxis.X1;
			clone.Interpolation4.XAxis.X2 = Interpolation4.XAxis.X2;
			clone.Interpolation4.XAxis.Y1 = Interpolation4.XAxis.Y1;
			clone.Interpolation4.XAxis.Y2 = Interpolation4.XAxis.Y2;
			clone.Interpolation4.YAxis.X1 = Interpolation4.YAxis.X1;
			clone.Interpolation4.YAxis.X2 = Interpolation4.YAxis.X2;
			clone.Interpolation4.YAxis.Y1 = Interpolation4.YAxis.Y1;
			clone.Interpolation4.YAxis.Y2 = Interpolation4.YAxis.Y2;
			clone.Interpolation4.ZAxis.X1 = Interpolation4.ZAxis.X1;
			clone.Interpolation4.ZAxis.X2 = Interpolation4.ZAxis.X2;
			clone.Interpolation4.ZAxis.Y1 = Interpolation4.ZAxis.Y1;
			clone.Interpolation4.ZAxis.Y2 = Interpolation4.ZAxis.Y2;

			clone.Position = ((PositionVector3)Position.Clone());
			clone.Rotation = ((PositionVector4)Rotation.Clone());
			return clone;
		}
	}
	/// <summary>
	/// Updates the texture on the specified material.
	/// </summary>
	public class MotionTextureUpdateAction : MotionAction
	{
		public override object Clone()
		{
			return this.MemberwiseClone();
		}
	}
}
