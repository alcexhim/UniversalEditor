using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework.UserInterface.Controls;

namespace MBS.Framework.UserInterface.ObjectModels.Theming
{
	public static class ThemeComponentGuid
	{
		private static Dictionary<Type, Guid> guidsForType = new Dictionary<Type, Guid>();
		static ThemeComponentGuid()
		{
			guidsForType.Add(typeof(Button), ThemeComponentGuids.Button);
		}
		public static Guid FromControlType(Type type)
		{
			if (guidsForType.ContainsKey(type)) return guidsForType[type];
			if (type.BaseType != null) return FromControlType(type.BaseType);
			return Guid.Empty;
		}
	}
	public static class ThemeComponentGuids
	{
		public static readonly Guid CommandBarMenu = new Guid("{92ED06B1-7E08-46FF-B5FF-A44431D32C67}");
		public static readonly Guid CommandBar = new Guid("{8BFF8467-F940-47C9-AE44-CEBB38AE8747}");
		public static readonly Guid CommandBarItem = new Guid("{D83F03AF-EEBE-4EFB-ACDB-7D2952B5D566}");
		public static readonly Guid CommandBarSplitItem = new Guid("{4F01984B-BA87-4EDB-A759-0698910AE56E}");
		public static readonly Guid CommandBarTopLevelItem = new Guid("{96C8D995-0628-4F0B-98AD-B55B053C481B}");
		public static readonly Guid CommandBarMenuItem = new Guid("{F064AB35-B3EE-4645-95B9-3DAFE1BD94B7}");
		public static readonly Guid CommandBarPopup = new Guid("{68969627-9C9D-487A-B89A-36AFC4810459}");
		public static readonly Guid CommandBarTopLevelPopup = new Guid("{5D5BE8EE-973B-4BC6-A973-3A8AA955A1CC}");
        public static readonly Guid CommandBarRaftingContainer = new Guid("{1FB02962-E290-46CC-B615-8E852EDE9B84}");

		public static readonly Guid ContentArea = new Guid("{2CA5F623-7ECF-4A1A-BBD5-1B57D4EB46C8}");

		public static readonly Guid DocumentTab = new Guid("{88B2D8F5-B1B5-44B2-BC89-04E107329945}");

		public static readonly Guid ListView = new Guid("{3176A0F5-A678-4606-8E64-3E64E679BAA7}");
		public static readonly Guid ListViewItem = new Guid("{98810A61-0F05-45AD-BFF4-AEEE5565B2C1}");
		public static readonly Guid ListViewSelectionRectangle = new Guid("{E7F9B7FE-E761-4086-9815-5DE2B674893A}");

		public static readonly Guid StatusBar = new Guid("{AAC316E5-9D9B-40D4-A7E0-C73BD831B80F}");

		// Common Controls
		public static readonly Guid Button = new Guid("{10E0B007-0B55-4E1C-9AE6-32FE1A797ADB}");
		public static readonly Guid TextBox = new Guid("{6517D097-8649-4905-97AA-A6FE1B39B376}");

		public static readonly Guid Window = new Guid("{69DFC07B-3A05-4BDA-95EF-FD3B487AEF69}");
	}
}
