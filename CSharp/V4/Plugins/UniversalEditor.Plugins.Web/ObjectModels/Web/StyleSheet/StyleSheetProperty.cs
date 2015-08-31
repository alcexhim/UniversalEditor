using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Web.StyleSheet
{
	public class StyleSheetProperty : ICloneable
	{
		public class StyleSheetPropertyCollection
			: System.Collections.ObjectModel.Collection<StyleSheetProperty>
		{
			public StyleSheetProperty Add(StyleSheetKnownProperty knownProperty, string value)
			{
				StyleSheetProperty prop = new StyleSheetProperty(knownProperty, value);
				base.Add(prop);
				return prop;
			}
			public StyleSheetProperty Add(string name, string value)
			{
				StyleSheetProperty prop = new StyleSheetProperty(name, value);
				base.Add(prop);
				return prop;
			}
		}

		public StyleSheetProperty()
		{
		}
		public StyleSheetProperty(string name, string value)
		{
			Name = name;
			Value = value;
		}
		public StyleSheetProperty(StyleSheetKnownProperty knownProperty, string value)
		{
			KnownProperty = knownProperty;
			Value = value;
		}

		public StyleSheetKnownProperty KnownProperty
		{
			get
			{
				switch (mvarName.ToLower())
				{
					case "azimuth": return StyleSheetKnownProperty.Azimuth;
					case "background": return StyleSheetKnownProperty.Background;
					case "background-attachment": return StyleSheetKnownProperty.BackgroundAttachment;
					case "background-color": return StyleSheetKnownProperty.BackgroundColor;
					case "background-image": return StyleSheetKnownProperty.BackgroundImage;
					case "background-position": return StyleSheetKnownProperty.BackgroundPosition;
					case "background-repeat": return StyleSheetKnownProperty.BackgroundRepeat;
					case "border": return StyleSheetKnownProperty.Border;
					case "border-bottom": return StyleSheetKnownProperty.BorderBottom;
					case "border-bottom-color": return StyleSheetKnownProperty.BorderBottomColor;
					case "border-bottom-style": return StyleSheetKnownProperty.BorderBottomStyle;
					case "border-bottom-width": return StyleSheetKnownProperty.BorderBottomWidth;
					case "border-collapse": return StyleSheetKnownProperty.BorderCollapse;
					case "border-color": return StyleSheetKnownProperty.BorderColor;
					case "border-left": return StyleSheetKnownProperty.BorderLeft;
					case "border-left-color": return StyleSheetKnownProperty.BorderLeftColor;
					case "border-left-style": return StyleSheetKnownProperty.BorderLeftStyle;
					case "border-left-width": return StyleSheetKnownProperty.BorderLeftWidth;
					case "border-right": return StyleSheetKnownProperty.BorderRight;
					case "border-right-color": return StyleSheetKnownProperty.BorderRightColor;
					case "border-right-style": return StyleSheetKnownProperty.BorderRightStyle;
					case "border-right-width": return StyleSheetKnownProperty.BorderRightWidth;
					case "border-spacing": return StyleSheetKnownProperty.BorderSpacing;
					case "border-style": return StyleSheetKnownProperty.BorderStyle;
					case "border-top": return StyleSheetKnownProperty.BorderTop;
					case "border-top-color": return StyleSheetKnownProperty.BorderTopColor;
					case "border-top-style": return StyleSheetKnownProperty.BorderTopStyle;
					case "border-top-width": return StyleSheetKnownProperty.BorderTopWidth;
					case "border-width": return StyleSheetKnownProperty.BorderWidth;
					case "bottom": return StyleSheetKnownProperty.Bottom;
					case "caption-side": return StyleSheetKnownProperty.CaptionSide;
					case "clear": return StyleSheetKnownProperty.Clear;
					case "clip": return StyleSheetKnownProperty.Clip;
					case "color": return StyleSheetKnownProperty.Color;
				}
				return StyleSheetKnownProperty.None;
			}
			set
			{
				switch (value)
				{
					case StyleSheetKnownProperty.Azimuth: mvarName = "azimuth"; break;
					case StyleSheetKnownProperty.Background: mvarName = "background"; break;
					case StyleSheetKnownProperty.BackgroundAttachment: mvarName = "background-attachment"; break;
					case StyleSheetKnownProperty.BackgroundColor: mvarName = "background-color"; break;
					case StyleSheetKnownProperty.BackgroundImage: mvarName = "background-image"; break;
					case StyleSheetKnownProperty.BackgroundPosition: mvarName = "background-position"; break;
					case StyleSheetKnownProperty.BackgroundRepeat: mvarName = "background-repeat"; break;
					case StyleSheetKnownProperty.Border: mvarName = "border"; break;
					case StyleSheetKnownProperty.BorderBottom: mvarName = "border-bottom"; break;
					case StyleSheetKnownProperty.BorderBottomColor: mvarName = "border-bottom-color"; break;
					case StyleSheetKnownProperty.BorderBottomStyle: mvarName = "border-bottom-style"; break;
					case StyleSheetKnownProperty.BorderBottomWidth: mvarName = "border-bottom-width"; break;
					case StyleSheetKnownProperty.BorderCollapse: mvarName = "border-collapse"; break;
					case StyleSheetKnownProperty.BorderColor: mvarName = "border-color"; break;
					case StyleSheetKnownProperty.BorderLeft: mvarName = "border-left"; break;
					case StyleSheetKnownProperty.BorderLeftColor: mvarName = "border-left-color"; break;
					case StyleSheetKnownProperty.BorderLeftStyle: mvarName = "border-left-style"; break;
					case StyleSheetKnownProperty.BorderLeftWidth: mvarName = "border-left-width"; break;
					case StyleSheetKnownProperty.BorderRight: mvarName = "border-right"; break;
					case StyleSheetKnownProperty.BorderRightColor: mvarName = "border-right-color"; break;
					case StyleSheetKnownProperty.BorderRightStyle: mvarName = "border-right-style"; break;
					case StyleSheetKnownProperty.BorderRightWidth: mvarName = "border-right-width"; break;
					case StyleSheetKnownProperty.BorderSpacing: mvarName = "border-spacing"; break;
					case StyleSheetKnownProperty.BorderStyle: mvarName = "border-style"; break;
					case StyleSheetKnownProperty.BorderTop: mvarName = "border-top"; break;
					case StyleSheetKnownProperty.BorderTopColor: mvarName = "border-top-color"; break;
					case StyleSheetKnownProperty.BorderTopStyle: mvarName = "border-top-style"; break;
					case StyleSheetKnownProperty.BorderTopWidth: mvarName = "border-top-width"; break;
					case StyleSheetKnownProperty.BorderWidth: mvarName = "border-width"; break;
					case StyleSheetKnownProperty.Bottom: mvarName = "bottom"; break;
					case StyleSheetKnownProperty.CaptionSide: mvarName = "caption-side"; break;
					case StyleSheetKnownProperty.Clear: mvarName = "clear"; break;
					case StyleSheetKnownProperty.Clip: mvarName = "clip"; break;
					case StyleSheetKnownProperty.Color: mvarName = "color"; break;
				}
			}
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		public object Clone()
		{
			StyleSheetProperty clone = new StyleSheetProperty();
			clone.Name = mvarName;
			clone.Value = mvarValue;
			return clone;
		}
	}
}
