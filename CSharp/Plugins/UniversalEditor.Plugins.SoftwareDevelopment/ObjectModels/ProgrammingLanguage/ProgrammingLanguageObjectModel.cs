using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.Accessors;

namespace UniversalEditor
{
	public class ScriptLanguage
	{
		private string mvarName = "";
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private Version mvarFlameVersion = new Version(1, 0, 0, 0);
		public Version FlameVersion { get { return mvarFlameVersion; } set { mvarFlameVersion = value; } }

		public void LoadXMLFile(string FileName)
		{
			MarkupObjectModel xd = new MarkupObjectModel();
			Document.Load(xd, new XMLDataFormat(), new FileAccessor(FileName));
			LoadXMLDocument(xd);
		}
		public void LoadXMLString(string xml)
		{
			MarkupObjectModel xd = new MarkupObjectModel();
			ObjectModel om = xd;

			XMLDataFormat xdf = new XMLDataFormat();
			Document.Load(xd, new XMLDataFormat(), new StringAccessor(xml));

			LoadXMLDocument(xd);
		}
		public void LoadXMLDocument(MarkupObjectModel xd)
		{
			MarkupTagElement tagFlame = (xd.Elements["flame"] as MarkupTagElement);
			if (tagFlame == null) throw new InvalidOperationException("Document does not contain a 'flame' top-level element");

			MarkupAttribute attVersion = tagFlame.Attributes["version"];
			if (attVersion != null) mvarFlameVersion = new Version(attVersion.Value);

			MarkupTagElement tagLanguage = (tagFlame.Elements["language"] as MarkupTagElement);
			if (tagLanguage == null) throw new InvalidOperationException("Flame language file does not contain a 'language' element inside the 'flame' element");

			MarkupTagElement tagName = (tagLanguage.Elements["name"] as MarkupTagElement);
			if (tagName != null)
			{
				mvarName = tagName.Value;
			}

			MarkupTagElement tagSyntax = (tagLanguage.Elements["syntax"] as MarkupTagElement);
			if (tagSyntax == null) throw new InvalidOperationException("Flame language file does not contain a 'syntax' element inside the 'language' element");

			// Load the syntax
			foreach (MarkupElement el in tagSyntax.Elements)
			{
				MarkupTagElement xn = (el as MarkupTagElement);
				if (xn == null) continue;

				_Xml_LoadSyntaxBlock(xn);
			}
		}

		private void _Xml_LoadSyntaxBlock(MarkupTagElement xn)
		{
			switch (xn.Name)
			{
				case "block":
					break;
				case "inline":
					break;
			}
		}
		public override string ToString()
		{
			return mvarName;
		}

		public class ScriptLanguageCollection
			: System.Collections.ObjectModel.Collection<ScriptLanguage>
		{

		}
	}
}
