using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	public enum ModelBoneType
	{
		Unknown = -1,
		Rotate = 0,
		RotateMove = 1,
		InverseKinematics = 2,
		Blank = 3,
		IKInfluencedRotation = 4,
		InfluencedRotation = 5,
		IKConnect = 6,
		Hidden = 7,
		Twist = 8,
		Revolution = 9
	}
}
