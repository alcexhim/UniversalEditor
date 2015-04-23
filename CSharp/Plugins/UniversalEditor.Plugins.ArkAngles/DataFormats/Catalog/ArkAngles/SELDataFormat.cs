using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Catalog.ArkAngles;

namespace UniversalEditor.DataFormats.Catalog.ArkAngles
{
	public class SELDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			CatalogObjectModel catalog = (objectModel as CatalogObjectModel);
			if (catalog == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine();

				string command = line;
				string paramzLine = String.Empty;
				if (line.Contains(" "))
				{
					int i = line.IndexOf(' ');
					command = line.Substring(0, i);
					paramzLine = line.Substring(i + 1);
				}

				string[] paramz = paramzLine.Split(new char[] { ',' });
				List<string> realParamz = new List<string>();
				foreach (string paramm in paramz)
				{
					realParamz.Add(paramm.Trim());
				}
				paramz = realParamz.ToArray();

				switch (command.ToLower())
				{
					case "title":
					{
						catalog.HeaderText = paramzLine;
						break;
					}
					case "help":
					{
						catalog.HelpFileName = paramzLine;
						break;
					}
					case "msg":
					{
						catalog.FooterText = paramzLine;
						break;
					}
					case "call":
					{
						// TODO: implement imported files
						break;
					}
					case "category":
					{
						catalog.Categories.Add(paramzLine);
						break;
					}
					case "platform":
					{
						catalog.Platforms.Add(paramzLine);
						break;
					}
					case "listing":
					{
						catalog.Listings.Add(paramzLine);
						break;
					}
					case "product":
					{
						Product product = new Product();
						if (paramz.Length > 0)
						{
							product.Title = paramz[0];
							if (paramz.Length > 1)
							{
								product.Category = catalog.Categories[paramz[1]];
								if (paramz.Length > 2)
								{
									product.Platform = catalog.Platforms[paramz[2]];
									if (paramz.Length > 3)
									{
										product.Listing = catalog.Listings[paramz[3]];
										if (paramz.Length > 4)
										{
											string[] keywords = paramz[4].Split(new char[] { ' ' });
											foreach (string keyword in keywords)
											{
												product.Keywords.Add(keyword);
											}
											if (paramz.Length > 5)
											{
												string[] filenames = paramz[5].Split(new char[] { ' ' });
												foreach (string fileName in filenames)
												{
													product.AssociatedFiles.Add(fileName);
												}
											}
										}
									}
								}
							}
						}
						catalog.Products.Add(product);
						break;
					}
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			CatalogObjectModel catalog = (objectModel as CatalogObjectModel);
			if (catalog == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;

			foreach (Category category in catalog.Categories)
			{
				writer.WriteLine("CATEGORY " + category.Title);
			}
		}
	}
}
