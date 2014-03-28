using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	public static class BoneNameConverter
	{
		private static List<List<string>> _cnvs = new List<List<string>>();
		static BoneNameConverter()
		{
			InitializeMMD();
			InitializeMoosta();
		}
		private static void InitializeMMD()
		{
			List<string> _cnv = new List<string>();
			_cnv.Add("センター");
			_cnv.Add("上半身");
			_cnv.Add("首");
			_cnv.Add("頭");
			_cnv.Add("左目");
			_cnv.Add("右目");
			_cnv.Add("両目");
			_cnv.Add("左肩");
			_cnv.Add("左腕");
			_cnv.Add("左ひじ");
			_cnv.Add("左手首");
			_cnv.Add("左親指１");
			_cnv.Add("左親指２");
			_cnv.Add("左人指１");
			_cnv.Add("左人指２");
			_cnv.Add("左人指３");
			_cnv.Add("左中指１");
			_cnv.Add("左中指２");
			_cnv.Add("左中指３");
			_cnv.Add("左薬指１");
			_cnv.Add("左薬指２");
			_cnv.Add("左薬指３");
			_cnv.Add("左小指１");
			_cnv.Add("左小指２");
			_cnv.Add("左小指３");
			_cnv.Add("右肩");
			_cnv.Add("右腕");
			_cnv.Add("右ひじ");
			_cnv.Add("右手首");
			_cnv.Add("右親指１");
			_cnv.Add("右親指２");
			_cnv.Add("右人指１");
			_cnv.Add("右人指２");
			_cnv.Add("右人指３");
			_cnv.Add("右中指１");
			_cnv.Add("右中指２");
			_cnv.Add("右中指３");
			_cnv.Add("右薬指１");
			_cnv.Add("右薬指２");
			_cnv.Add("右薬指３");
			_cnv.Add("右小指１");
			_cnv.Add("右小指２");
			_cnv.Add("右小指３");
			_cnv.Add("下半身");
			_cnv.Add("左足");
			_cnv.Add("左ひざ");
			_cnv.Add("左足首");
			_cnv.Add("右足");
			_cnv.Add("右ひざ");
			_cnv.Add("右足首");
			_cnvs.Add(_cnv);
		}
		private static void InitializeMoosta()
		{
			List<string> _cnv = new List<string>();
			_cnv.Add("OMP.ROOT");
			_cnv.Add("OMP.UPPERBODY");
			_cnv.Add("OMP.NECK");
			_cnv.Add("OMP.HEAD");
			_cnv.Add("OMP.L.EYE");
			_cnv.Add("OMP.R.EYE");
			_cnv.Add("OMP.EYES");
			_cnv.Add("OMP.L.COLLAR");
			_cnv.Add("OMP.L.SHOULDER");
			_cnv.Add("OMP.L.ELBOW");
			_cnv.Add("OMP.L.WRIST");
			_cnv.Add("OMP.L.THUMB.1");
			_cnv.Add("OMP.L.THUMB.2");
			_cnv.Add("OMP.L.FINGER1.1");
			_cnv.Add("OMP.L.FINGER1.2");
			_cnv.Add("OMP.L.FINGER1.3");
			_cnv.Add("OMP.L.FINGER2.1");
			_cnv.Add("OMP.L.FINGER2.2");
			_cnv.Add("OMP.L.FINGER2.3");
			_cnv.Add("OMP.L.FINGER3.1");
			_cnv.Add("OMP.L.FINGER3.2");
			_cnv.Add("OMP.L.FINGER3.3");
			_cnv.Add("OMP.L.FINGER4.1");
			_cnv.Add("OMP.L.FINGER4.2");
			_cnv.Add("OMP.L.FINGER4.3");
			_cnv.Add("OMP.R.COLLAR");
			_cnv.Add("OMP.R.SHOULDER");
			_cnv.Add("OMP.R.ELBOW");
			_cnv.Add("OMP.R.WRIST");
			_cnv.Add("OMP.R.THUMB.1");
			_cnv.Add("OMP.R.THUMB.2");
			_cnv.Add("OMP.R.FINGER1.1");
			_cnv.Add("OMP.R.FINGER1.2");
			_cnv.Add("OMP.R.FINGER1.3");
			_cnv.Add("OMP.R.FINGER2.1");
			_cnv.Add("OMP.R.FINGER2.2");
			_cnv.Add("OMP.R.FINGER2.3");
			_cnv.Add("OMP.R.FINGER3.1");
			_cnv.Add("OMP.R.FINGER3.2");
			_cnv.Add("OMP.R.FINGER3.3");
			_cnv.Add("OMP.R.FINGER4.1");
			_cnv.Add("OMP.R.FINGER4.2");
			_cnv.Add("OMP.R.FINGER4.3");
			_cnv.Add("OMP.LOWER_BODY");
			_cnv.Add("OMP.L.HIP");
			_cnv.Add("OMP.L.KNEE");
			_cnv.Add("OMP.L.ANKLE");
			_cnv.Add("OMP.R.HIP");
			_cnv.Add("OMP.R.KNEE");
			_cnv.Add("OMP.R.ANKLE");
			_cnvs.Add(_cnv);
		}

	}
}
