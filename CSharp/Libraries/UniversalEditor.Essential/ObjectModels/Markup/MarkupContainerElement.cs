using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.Markup
{
	public abstract class MarkupContainerElement : MarkupElement
	{
		private MarkupElement.MarkupElementCollection mvarElements = null;
		public MarkupElement.MarkupElementCollection Elements { get { return mvarElements; } }

		public MarkupContainerElement()
		{
			this.mvarElements = new MarkupElement.MarkupElementCollection(this, this.ParentObjectModel);
		}
		public MarkupElement FindElement(params string[] fullNames)
		{
			MarkupElement result;
			if (fullNames.Length == 0)
			{
				result = null;
			}
			else
			{
				MarkupElement e = this.mvarElements[fullNames[0]];
				for (int i = 1; i < fullNames.Length; i++)
				{
					if (e != null && e is MarkupContainerElement)
					{
						e = (e as MarkupContainerElement).Elements[fullNames[i]];
					}
				}
				result = e;
			}
			return result;
		}
		public MarkupTagElement CreateElement(params string[] elementNames)
		{
			MarkupTagElement basetag = null;
			MarkupTagElement tag = null;
			for (int i = elementNames.Length - 1; i > -1; i--)
			{
				if (tag == null)
				{
					tag = new MarkupTagElement();
					tag.FullName = elementNames[i];
					basetag = tag;
					this.Elements.Add(tag);
				}
				else
				{
					MarkupTagElement newtag = new MarkupTagElement();
					newtag.FullName = elementNames[i];
					tag.Elements.Add(newtag);
					tag = newtag;
				}
			}
			return basetag;
		}

		public string XMLSchema
		{
			get
			{
				if (this.Namespace == null)
					return null;

				for (int i = 0; i < this.ParentObjectModel.Elements.Count; i++) {
					MarkupTagElement tagTopLevel = (this.ParentObjectModel.Elements [i] as MarkupTagElement);
					if (tagTopLevel == null)
						continue;

					MarkupAttribute att = tagTopLevel.Attributes ["xmlns:" + this.Namespace];
					if (att != null)
						return att.Value;
				}

				return null;
			}
		}

		protected override void UpdateParentObjectModel ()
		{
			base.UpdateParentObjectModel ();
			this.Elements.ParentObjectModel = this.ParentObjectModel;
			for (int i = 0; i < this.Elements.Count; i++) {
				this.Elements [i].ParentObjectModel = this.ParentObjectModel;
			}
		}

		public MarkupElement FindElementUsingSchema(string schema, string name)
		{
			string tagPrefix = null;
			for (int i = 0; i < this.ParentObjectModel.Elements.Count; i++)
			{
				MarkupTagElement tagTopLevel = (this.ParentObjectModel.Elements [i] as MarkupTagElement);
				if (tagTopLevel != null) {
					for (int j = 0; j < tagTopLevel.Attributes.Count; j++) {
						if (tagTopLevel.Attributes [j].Namespace.Equals ("xmlns")) {

							if (tagTopLevel.Attributes [j].Value.Equals (schema)) {
								tagPrefix = tagTopLevel.Attributes [j].Name;
								break;
							}

						}
					}
				}
			}

			if (tagPrefix == null) {
				Console.WriteLine ("ue: MarkupObjectModel: tag prefix for schema '" + schema + "' not found");
				return null;
			}

			string fullName = tagPrefix + ":" + name;
			return FindElement (fullName);
		}

        public override void Combine(MarkupElement el)
        {
            MarkupTagElement tag = (el as MarkupTagElement);
            if (tag == null) throw new InvalidOperationException("Cannot combine MarkupTagElement with " + el.GetType().Name);

            foreach (MarkupElement el1 in tag.Elements)
            {
				string id = null;
				MarkupTagElement tag1 = (el1 as MarkupTagElement);
				if (tag1 != null)
				{
					MarkupAttribute attID = tag1.Attributes["ID"];
					if (attID != null) id = attID.Value;
				}

                if (Elements.Contains(el1.FullName, id))
                {
                    Elements[el1.FullName].Combine(el1);
                }
                else
                {
                    Elements.Add(el1);
                }
            }
        }
	}
}
