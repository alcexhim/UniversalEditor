using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.ObjectModels.Theming
{
	public static class ThemeComponentStateGuids
	{
		public static readonly Guid Normal = new Guid("{8DB00426-DE86-4F13-B451-D83602D5585E}");
		public static readonly Guid NormalFocused = new Guid("{DEBD7515-DC08-40C0-8BDE-4C988626B0B1}");
		public static readonly Guid NormalFocusedSelected = new Guid("{0EA7A231-25F9-4B7C-A905-BDF7ED47D56C}");
		public static readonly Guid NormalSelected = new Guid("{F0AF167B-A85D-4ACC-8D22-3C63986C120D}");

		public static readonly Guid Hover = new Guid("{753EF929-00E9-4C9A-BE0E-F897DCA649B8}");
		public static readonly Guid HoverFocused = new Guid("{F443022D-6CF8-43CF-B456-09C1EA3EAD48}");
		public static readonly Guid HoverFocusedSelected = new Guid("{A37E2383-C075-4497-9CA7-8D7BC41E8C1C}");
		public static readonly Guid HoverSelected = new Guid("{D6EF73B1-9491-4D98-8039-BCFF6E3A8A51}");

        public static readonly Guid Pressed = new Guid("{962963D7-D209-42D0-94BA-AB022109AB53}");
		public static readonly Guid PressedFocused = new Guid("{F7A8E2FB-775B-4FB1-8261-6375276D6B15}");
		public static readonly Guid PressedFocusedSelected = new Guid("{7CB48046-AA52-4458-A0DC-36479FA395E8}");
		public static readonly Guid PressedSelected = new Guid("{7C2EE830-E568-449E-AD55-F826F414DA07}");

        public static readonly Guid Disabled = new Guid("{E6C4A9F6-D702-456B-B425-9D044BFCE154}");
        public static readonly Guid None = new Guid("{00000000-0000-0000-0000-000000000000}");
	}
}
