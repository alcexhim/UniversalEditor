//
//  SELDataFormat.cs - implements Ark Angles catalog file format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Catalog.ArkAngles;

namespace UniversalEditor.DataFormats.Catalog.ArkAngles
{
	public class SELDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(CatalogObjectModel), DataFormatCapabilities.All);
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

			foreach (Category item in catalog.Categories)
			{
				writer.WriteLine("CATEGORY " + item.Title);
			}
			writer.WriteLine();
			foreach (Platform item in catalog.Platforms)
			{
				writer.WriteLine("PLATFORM " + item.Title);
			}
			writer.WriteLine();
			foreach (Listing item in catalog.Listings)
			{
				writer.WriteLine("LISTING " + item.Title);
			}
			writer.WriteLine();
			foreach (Product item in catalog.Products)
			{
				StringBuilder sbKeywords = new StringBuilder();
				foreach (string item1 in item.Keywords)
				{
					sbKeywords.Append(item1);
					if (item.Keywords.IndexOf(item1) < item.Keywords.Count - 1) sbKeywords.Append(" ");
				}
				StringBuilder sbFileNames = new StringBuilder();
				foreach (string item1 in item.AssociatedFiles)
				{
					sbFileNames.Append(item1);
					if (item.AssociatedFiles.IndexOf(item1) < item.AssociatedFiles.Count - 1) sbFileNames.Append(" ");
				}
				writer.WriteLine("PRODUCT " + item.Title + "," + (item.Category == null ? String.Empty : item.Category.Title) + "," + (item.Platform == null ? String.Empty : item.Platform.Title) + "," + (item.Listing == null ? String.Empty : item.Listing.Title) + "," + sbKeywords.ToString() + "," + sbFileNames.ToString());
			}
		}
	}
}
