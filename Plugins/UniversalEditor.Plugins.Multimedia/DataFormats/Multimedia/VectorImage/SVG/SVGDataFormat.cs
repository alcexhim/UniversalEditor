//
//  SVGDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using System.Collections.Generic;
using System.Text;
using MBS.Framework.Drawing;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.ObjectModels.Multimedia.VectorImage;
using UniversalEditor.ObjectModels.Multimedia.VectorImage.VectorItems;

namespace UniversalEditor.DataFormats.Multimedia.VectorImage.SVG
{
	public class SVGDataFormat : XMLDataFormat
	{
		private const string SCHEMA_SVG = "http://www.w3.org/2000/svg";

		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(VectorImageObjectModel), DataFormatCapabilities.All);
				_dfr.Title = "Scalable Vector Graphics";
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new MarkupObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			MarkupObjectModel mom = (objectModels.Pop() as MarkupObjectModel);
			VectorImageObjectModel vec = (objectModels.Pop() as VectorImageObjectModel);

			MarkupTagElement tagSVG = mom.FindElementUsingSchema(SCHEMA_SVG, "svg") as MarkupTagElement;
			if (tagSVG == null) throw new InvalidDataFormatException("top-level SVG element not found");

			MarkupAttribute attViewBox = tagSVG.Attributes["viewBox"];
			if (attViewBox != null)
			{
				string[] viewbox = attViewBox.Value.Split(new char[] { ' ' });
				if (viewbox.Length == 4)
				{
					vec.ViewBox = new Rectangle(Double.Parse(viewbox[0]), Double.Parse(viewbox[1]), Double.Parse(viewbox[2]), Double.Parse(viewbox[3]));
				}
			}

			for (int i = 0; i < tagSVG.Elements.Count; i++)
			{
				MarkupTagElement tagItem = tagSVG.Elements[i] as MarkupTagElement;
				if (tagItem == null) continue;

				if (tagItem.Name == "path")
				{
					MarkupAttribute attD = tagItem.Attributes["d"];
					if (attD == null) continue;

					Dictionary<string, string> dictStyles = new Dictionary<string, string>();
					MarkupAttribute attStyle = tagItem.Attributes["style"];
					if (attStyle != null)
					{
						string[] styles = attStyle.Value.Split(new char[] { ';' });
						for (int j = 0; j < styles.Length; j++)
						{
							string[] st = styles[j].Split(new char[] { ':' });
							if (st.Length == 2)
							{
								dictStyles[st[0]] = st[1];
							}
						}
					}

					string[] ds = attD.Value?.Split(new char[] { ' ' });

					PolygonVectorItem poly = new PolygonVectorItem();

					if (dictStyles.ContainsKey("fill"))
					{
						poly.Style.FillColor = Color.FromString(dictStyles["fill"]);
					}
					if (dictStyles.ContainsKey("stroke"))
					{
						poly.Style.BorderColor = Color.FromString(dictStyles["stroke"]);
					}
					if (ds[0] == "m" && ds[ds.Length - 1] == "z")
					{
						for (int j = 1; j < ds.Length - 1; j++)
						{
							string[] vs = ds[j].Split(new char[] { ',' });
							if (vs.Length == 2)
							{
								poly.Points.Add(new PositionVector2(Double.Parse(vs[0]), Double.Parse(vs[1])));
							}
						}
					}

					vec.Items.Add(poly);
				}
			}
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			VectorImageObjectModel vec = (objectModels.Pop() as VectorImageObjectModel);
			if (vec == null) throw new ObjectModelNotSupportedException();

			MarkupObjectModel mom = new MarkupObjectModel();
			MarkupTagElement tagSVG = new MarkupTagElement();
			tagSVG.Name = "svg";

			tagSVG.Attributes.Add("xmlns", "http://www.w3.org/2000/svg");
			tagSVG.Attributes.Add("height", vec.Height.ToString());
			tagSVG.Attributes.Add("width", vec.Width.ToString());

			if (vec.ViewBox != Rectangle.Empty)
			{
				tagSVG.Attributes.Add("viewBox", String.Format("{0} {1} {2} {3}", vec.ViewBox.X, vec.ViewBox.Y, vec.ViewBox.Width, vec.ViewBox.Height));
			}

			for (int i = 0; i < vec.Items.Count; i++)
			{
				if (vec.Items[i] is PolygonVectorItem)
				{
					PolygonVectorItem poly = (vec.Items[i] as PolygonVectorItem);

					MarkupTagElement tagPath = new MarkupTagElement();
					tagPath.FullName = "path";

					StringBuilder sb = new StringBuilder();
					sb.Append("m ");
					for (int j = 0; j < poly.Points.Count; j++)
					{
						sb.Append(poly.Points[j].X.ToString());
						sb.Append(',');
						sb.Append(poly.Points[j].Y.ToString());
						sb.Append(' ');
					}
					sb.Append('z');

					tagPath.Attributes.Add("d", sb.ToString());

					if (poly.Style.FillColor != Colors.Transparent)
						tagPath.Attributes.Add("fill", poly.Style.FillColor.ToHexadecimalHTML());

					tagSVG.Elements.Add(tagPath);
				}
			}

			mom.Elements.Add(tagSVG);
			objectModels.Push(mom);
		}
	}
}
