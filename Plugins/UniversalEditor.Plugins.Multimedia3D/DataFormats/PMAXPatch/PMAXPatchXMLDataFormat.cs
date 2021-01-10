//
//  PMAXPatchXMLDataFormat.cs - provides a DataFormat for manipulating Concertroid PMAX patch files in XML format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2013-2020 Mike Becker's Software
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

using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Markup;

using UniversalEditor.ObjectModels.PMAXPatch;
using UniversalEditor.ObjectModels.PMAXPatch.Chunks;

namespace UniversalEditor.DataFormats.PMAXPatch
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating PMAX patch data.
	/// </summary>
	/// <remarks>
	/// This is a DataFormat I created for Concertroid and really should be refactored into a separate library.
	/// </remarks>
	public class PMAXPatchXMLDataFormat : XMLDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Clear();
				_dfr.Capabilities.Add(typeof(PMAXPatchObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<UniversalEditor.ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);

			objectModels.Push(new MarkupObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			MarkupObjectModel mom = (objectModels.Pop() as MarkupObjectModel);
			PMAXPatchObjectModel pmax = (objectModels.Pop() as PMAXPatchObjectModel);

			MarkupTagElement tagPmax = (mom.Elements["pmax"] as MarkupTagElement);
			if (tagPmax == null) throw new InvalidDataFormatException("PmaXML file does not contain a top-level \"pmax\" tag.");

			foreach (MarkupElement elPatch in tagPmax.Elements)
			{
				MarkupTagElement tagPatch = (elPatch as MarkupTagElement);
				if (tagPatch == null) continue;
				if (tagPatch.Name != "patch") continue;

				MarkupAttribute attPatchInputFileName = tagPatch.Attributes["inputFileName"];
				if (attPatchInputFileName == null) continue;

				UniversalEditor.ObjectModels.PMAXPatch.PMAXPatch patch = new UniversalEditor.ObjectModels.PMAXPatch.PMAXPatch();
				patch.InputFileName = attPatchInputFileName.Value;

				MarkupAttribute attPatchOutputFileName = tagPatch.Attributes["outputFileName"];
				if (attPatchOutputFileName == null)
				{
					patch.OutputFileName = System.IO.Path.ChangeExtension(patch.InputFileName, ".pma");
				}
				else
				{
					patch.OutputFileName = attPatchOutputFileName.Value;
				}

				#region Material Names
				{
					MarkupTagElement tagMaterialNames = (tagPatch.Elements["materialNames"] as MarkupTagElement);
					if (tagMaterialNames != null)
					{
						PMAXMaterialNamesChunk chunk = new PMAXMaterialNamesChunk();
						int lastMaterialIndex = 0;
						foreach (MarkupElement elMaterialName in tagMaterialNames.Elements)
						{
							MarkupTagElement tagMaterialName = (elMaterialName as MarkupTagElement);
							if (tagMaterialName == null) continue;
							if (tagMaterialName.Name != "materialName") continue;

							MarkupAttribute attMaterialIndex = tagMaterialName.Attributes["index"];
							int thisMaterialIndex = lastMaterialIndex;
							if (attMaterialIndex != null)
							{
								thisMaterialIndex = Int32.Parse(attMaterialIndex.Value);
							}

							if (thisMaterialIndex > lastMaterialIndex)
							{
								for (int i = 0; i < (thisMaterialIndex - lastMaterialIndex); i++)
								{
									chunk.MaterialNames.Add(String.Empty);
								}
							}
							chunk.MaterialNames.Add(tagMaterialName.Value);

							lastMaterialIndex++;
						}
						patch.Chunks.Add(chunk);
					}
				}
				#endregion
				#region Animated Texture Blocks
				{
					MarkupTagElement tagadvancedTextureBlocks = (tagPatch.Elements["advancedTextureBlocks"] as MarkupTagElement);
					if (tagadvancedTextureBlocks != null)
					{
						PMAXAdvancedTextureBlockChunk chunk = new PMAXAdvancedTextureBlockChunk();
						foreach (MarkupElement eladvancedTextureBlock in tagadvancedTextureBlocks.Elements)
						{
							MarkupTagElement tagadvancedTextureBlock = (eladvancedTextureBlock as MarkupTagElement);
							if (tagadvancedTextureBlock == null) continue;
							if (tagadvancedTextureBlock.Name != "advancedTextureBlock") continue;

							MarkupAttribute attMaterialIndex = tagadvancedTextureBlock.Attributes["materialIndex"];
							if (attMaterialIndex == null) continue;

							PMAXAdvancedTextureBlock texture = new PMAXAdvancedTextureBlock();
							texture.MaterialID = Int32.Parse(attMaterialIndex.Value);

							MarkupAttribute attAlwaysLight = tagadvancedTextureBlock.Attributes["alwaysLight"];
							if (attAlwaysLight != null)
							{
								switch (attAlwaysLight.Value.ToLower())
								{
									case "on":
									case "yes":
									case "1":
									case "enabled":
									case "true":
									{
										texture.AlwaysLight = true;
										break;
									}
									default:
									{
										texture.AlwaysLight = false;
										break;
									}
								}
							}


							MarkupTagElement tagTextureImages = (tagadvancedTextureBlock.Elements["textureImages"] as MarkupTagElement);
							if (tagTextureImages != null)
							{
								foreach (MarkupElement elTextureImage in tagTextureImages.Elements)
								{
									MarkupTagElement tagTextureImage = (elTextureImage as MarkupTagElement);
									if (tagTextureImage == null) continue;
									if (tagTextureImage.Name != "textureImage") continue;

									MarkupAttribute attTextureImageFileName = tagTextureImage.Attributes["fileName"];
									if (attTextureImageFileName == null) continue;

									MarkupAttribute attTextureImageTimestamp = tagTextureImage.Attributes["timeStamp"];

									PMAXAdvancedTextureBlockImage image = new PMAXAdvancedTextureBlockImage();
									image.FileName = attTextureImageFileName.Value;
									if (attTextureImageTimestamp != null)
									{
										image.Timestamp = Int64.Parse(attTextureImageTimestamp.Value);
									}

									MarkupAttribute attTextureImageFlags = tagTextureImage.Attributes["textureFlags"];
									if (attTextureImageFlags != null)
									{
										switch (attTextureImageFlags.Value.ToLower())
										{
											case "addmap":
											{
												image.TextureFlags = UniversalEditor.ObjectModels.Multimedia3D.Model.ModelTextureFlags.AddMap;
												break;
											}
											case "map":
											{
												image.TextureFlags = UniversalEditor.ObjectModels.Multimedia3D.Model.ModelTextureFlags.Map;
												break;
											}
											case "texture":
											{
												image.TextureFlags = UniversalEditor.ObjectModels.Multimedia3D.Model.ModelTextureFlags.Texture;
												break;
											}
										}
									}

									texture.Images.Add(image);
								}
							}
							chunk.AdvancedTextureBlocks.Add(texture);
						}
						patch.Chunks.Add(chunk);
					}
				}
				#endregion
				#region Effect Scripts
				{
					MarkupTagElement tagEffectScripts = (tagPatch.Elements["effectScripts"] as MarkupTagElement);
					if (tagEffectScripts != null)
					{
						PMAXEffectsScriptChunk chunk = new PMAXEffectsScriptChunk();
						foreach (MarkupElement elEffectScript in tagEffectScripts.Elements)
						{
							MarkupTagElement tagEffectScript = (elEffectScript as MarkupTagElement);
							if (tagEffectScript == null) continue;
							if (tagEffectScript.Name != "effectScript") continue;

							MarkupAttribute attEffectScriptFileName = tagEffectScript.Attributes["fileName"];
							if (attEffectScriptFileName == null) continue;

							chunk.EffectScriptFileNames.Add(attEffectScriptFileName.Value);
						}
						patch.Chunks.Add(chunk);
					}
				}
				#endregion

				pmax.Patches.Add(patch);
			}
		}
	}
}
