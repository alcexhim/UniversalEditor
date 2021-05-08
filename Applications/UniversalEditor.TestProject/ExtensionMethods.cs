using System;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.TestProject
{
	public static class ExtensionMethods
	{
		public static MarkupTagElement ToXML(this ModelVertex vertex, string name)
		{
			MarkupTagElement tagVertex = new MarkupTagElement();
			tagVertex.FullName = name;

			tagVertex.Elements.Add(vertex.Position.ToXML("cr:position"));
			tagVertex.Elements.Add(vertex.Normal.ToXML("cr:normal"));
			tagVertex.Elements.Add(vertex.Texture.ToXML("cr:texture"));

			return tagVertex;
		}
		public static MarkupTagElement ToXML(this TextureVector2 texture, string name)
		{
			MarkupTagElement tag = new MarkupTagElement();
			tag.FullName = name;
			tag.Attributes.Add("u", texture.U.ToString());
			tag.Attributes.Add("v", texture.V.ToString());
			return tag;
		}
		public static MarkupTagElement ToXML(this PositionVector4 position, string name)
		{
			MarkupTagElement tag = new MarkupTagElement();
			tag.FullName = name;
			tag.Attributes.Add("x", position.X.ToString());
			tag.Attributes.Add("y", position.Y.ToString());
			tag.Attributes.Add("z", position.Z.ToString());
			tag.Attributes.Add("w", position.W.ToString());
			return tag;
		}
		public static MarkupTagElement ToXML(this PositionVector3 position, string name)
		{
			MarkupTagElement tag = new MarkupTagElement();
			tag.FullName = name;
			tag.Attributes.Add("x", position.X.ToString());
			tag.Attributes.Add("y", position.Y.ToString());
			tag.Attributes.Add("z", position.Z.ToString());
			return tag;
		}
		public static MarkupTagElement ToXML(this PositionVector2 position, string name)
		{
			MarkupTagElement tag = new MarkupTagElement();
			tag.FullName = name;
			tag.Attributes.Add("x", position.X.ToString());
			tag.Attributes.Add("y", position.Y.ToString());
			return tag;
		}
	}
}
