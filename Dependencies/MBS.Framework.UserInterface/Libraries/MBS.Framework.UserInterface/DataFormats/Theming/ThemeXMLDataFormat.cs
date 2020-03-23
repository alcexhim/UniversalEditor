using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Markup;

using MBS.Framework.UserInterface.ObjectModels.Theming;
using MBS.Framework.UserInterface.ObjectModels.Theming.RenderingActions;
using MBS.Framework.UserInterface.ObjectModels.Theming.Metrics;

namespace MBS.Framework.UserInterface.DataFormats.Theming
{
	public class ThemeXMLDataFormat : XMLDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
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
			ThemeObjectModel themes = (objectModels.Pop() as ThemeObjectModel);

			MarkupTagElement tagThemes = (mom.FindElement("AwesomeControls", "Theming", "Themes") as MarkupTagElement);
			if (tagThemes != null)
			{
				foreach (MarkupElement elTheme in tagThemes.Elements)
				{
					MarkupTagElement tagTheme = (elTheme as MarkupTagElement);
					if (tagTheme == null) continue;

					MarkupAttribute attThemeID = tagTheme.Attributes["ID"];
					if (attThemeID == null) continue;

					Theme theme = new Theme();

					UniversalEditor.Accessors.FileAccessor fa = (this.Accessor as UniversalEditor.Accessors.FileAccessor);
					if (fa != null) theme.BasePath = System.IO.Path.GetDirectoryName(fa.FileName);

					theme.ID = new Guid(attThemeID.Value);

					MarkupAttribute attInheritsThemeID = tagTheme.Attributes["InheritsThemeID"];
					if (attInheritsThemeID != null) theme.InheritsThemeID = new Guid(attInheritsThemeID.Value);

					MarkupTagElement tagInformation = (tagTheme.Elements["Information"] as MarkupTagElement);
					if (tagInformation != null)
					{
						MarkupTagElement tagInformationTitle = (tagInformation.Elements["Title"] as MarkupTagElement);
						if (tagInformationTitle != null) theme.Title = tagInformationTitle.Value;
					}

					MarkupTagElement tagMetrics = (tagTheme.Elements["Metrics"] as MarkupTagElement);
					if (tagMetrics != null)
					{
						foreach (MarkupElement elMetric in tagMetrics.Elements)
						{
							MarkupTagElement tagMetric = (elMetric as MarkupTagElement);
							if (tagMetric == null) continue;

							MarkupAttribute attMetricName = tagMetric.Attributes["Name"];
							if (attMetricName == null) continue;

							switch (tagMetric.FullName.ToLower())
							{
								case "paddingmetric":
								{
									PaddingMetric metric = new PaddingMetric();
									metric.Name = attMetricName.Value;

									MarkupAttribute attMetricLeft = tagMetric.Attributes["Left"];
									if (attMetricLeft != null) metric.Left = Single.Parse(attMetricLeft.Value);
									MarkupAttribute attMetricTop = tagMetric.Attributes["Top"];
									if (attMetricTop != null) metric.Top = Single.Parse(attMetricTop.Value);
									MarkupAttribute attMetricBottom = tagMetric.Attributes["Bottom"];
									if (attMetricBottom != null) metric.Bottom = Single.Parse(attMetricBottom.Value);
									MarkupAttribute attMetricRight = tagMetric.Attributes["Right"];
									if (attMetricRight != null) metric.Right = Single.Parse(attMetricRight.Value);

									theme.Metrics.Add(metric);
									break;
								}
							}
						}
					}

					MarkupTagElement tagColors = (tagTheme.Elements["Colors"] as MarkupTagElement);
					if (tagColors != null)
					{
						foreach (MarkupElement elColor in tagColors.Elements)
						{
							MarkupTagElement tagColor = (elColor as MarkupTagElement);
							if (tagColor == null) continue;
							if (tagColor.FullName != "Color") continue;

							MarkupAttribute attColorID = tagColor.Attributes["ID"];
							MarkupAttribute attColorName = tagColor.Attributes["Name"];

							if (attColorID == null && attColorName == null) continue;

							MarkupAttribute attColorValue = tagColor.Attributes["Value"];
							if (attColorValue == null) continue;

							ThemeColor color = new ThemeColor();
							if (attColorID != null) color.ID = new Guid(attColorID.Value);
							if (attColorName != null) color.Name = attColorName.Value;
							if (attColorValue != null) color.Value = attColorValue.Value;

							theme.Colors.Add(color);
						}
					}

					MarkupTagElement tagFonts = (tagTheme.Elements["Fonts"] as MarkupTagElement);
					if (tagFonts != null)
					{
						foreach (MarkupElement elFont in tagFonts.Elements)
						{
							MarkupTagElement tagFont = (elFont as MarkupTagElement);
							if (tagFont == null) continue;
							if (tagFont.FullName != "Font") continue;

							MarkupAttribute attFontName = tagFont.Attributes["Name"];
							if (attFontName == null) continue;

							MarkupAttribute attFontValue = tagFont.Attributes["Value"];
							if (attFontValue == null) continue;

							ThemeFont font = new ThemeFont();
							font.Name = attFontName.Value;
							font.Value = attFontValue.Value;

							theme.Fonts.Add(font);
						}
					}

					MarkupTagElement tagStockImages = (tagTheme.Elements["StockImages"] as MarkupTagElement);
					if (tagStockImages != null)
					{
						foreach (MarkupElement elStockImage in tagStockImages.Elements)
						{
							MarkupTagElement tagStockImage = (elStockImage as MarkupTagElement);
							if (tagStockImage == null) continue;
							if (tagStockImage.FullName != "StockImage") continue;

							MarkupAttribute attStockImageName = tagStockImage.Attributes["Name"];
							if (attStockImageName == null) continue;

							MarkupAttribute attStockImageFileName = tagStockImage.Attributes["FileName"];
							if (attStockImageFileName == null) continue;

							ThemeStockImage stockImage = new ThemeStockImage();
							stockImage.Name = attStockImageName.Value;
							stockImage.ImageFileName = attStockImageFileName.Value;

							theme.StockImages.Add(stockImage);
						}
					}

					MarkupTagElement tagProperties = (tagTheme.Elements["Properties"] as MarkupTagElement);
					if (tagProperties != null)
					{
						foreach (MarkupElement elProperty in tagProperties.Elements)
						{
							MarkupTagElement tagProperty = (elProperty as MarkupTagElement);
							if (tagProperty == null) continue;
							if (tagProperty.FullName != "Property") continue;

							MarkupAttribute attName = tagProperty.Attributes["Name"];
							if (attName == null) continue;

							ThemeProperty property = new ThemeProperty();
							property.Name = attName.Value;

							MarkupAttribute attValue = tagProperty.Attributes["Value"];
							if (attValue != null) property.Value = attValue.Value;

							theme.Properties.Add(property);
						}
					}

					MarkupTagElement tagComponents = (tagTheme.Elements["Components"] as MarkupTagElement);
					if (tagComponents != null)
					{
						foreach (MarkupElement elComponent in tagComponents.Elements)
						{
							MarkupTagElement tagComponent = (elComponent as MarkupTagElement);
							if (tagComponent == null) continue;
							if (tagComponent.FullName != "Component") continue;

							MarkupAttribute attComponentID = tagComponent.Attributes["ID"];
							if (attComponentID == null) continue;

							ThemeComponent component = new ThemeComponent();
							component.ID = new Guid(attComponentID.Value);

							MarkupAttribute attInheritsComponentID = tagComponent.Attributes["InheritsComponentID"];
							if (attInheritsComponentID != null) component.InheritsComponentID = new Guid(attInheritsComponentID.Value);

							MarkupTagElement tagComponentStates = (tagComponent.Elements["States"] as MarkupTagElement);
							if (tagComponentStates != null)
							{
								// if States is specified, only apply to specific states
								foreach (MarkupElement elState in tagComponentStates.Elements)
								{
									MarkupTagElement tagState = (elState as MarkupTagElement);
									if (tagState == null) continue;
									if (tagState.FullName != "State") continue;

									MarkupAttribute attStateID = tagState.Attributes["ID"];
									if (attStateID == null) continue;

									ThemeComponentState state = new ThemeComponentState();
									state.ID = new Guid(attStateID.Value);

									MarkupAttribute attStateName = tagState.Attributes["Name"];
									if (attStateName != null) state.Name = attStateName.Value;

									component.States.Add(state);
								}
							}

							MarkupTagElement tagRenderings = (tagComponent.Elements["Renderings"] as MarkupTagElement);
							if (tagRenderings != null)
							{
								foreach (MarkupElement elRendering in tagRenderings.Elements)
								{
									MarkupTagElement tagRendering = (elRendering as MarkupTagElement);
									if (tagRendering == null) continue;
									if (tagRendering.FullName != "Rendering") continue;

									MarkupTagElement tagRenderingActions = (tagRendering.Elements["Actions"] as MarkupTagElement);
									if (tagRenderingActions == null) continue;

									ThemeRendering rendering = new ThemeRendering();
									foreach (MarkupElement elRenderingAction in tagRenderingActions.Elements)
									{
										MarkupTagElement tagRenderingAction = (elRenderingAction as MarkupTagElement);
										if (tagRenderingAction == null) continue;

										switch (tagRenderingAction.FullName)
										{
											case "Rectangle":
											{
												MarkupAttribute attX = tagRenderingAction.Attributes["X"];
												MarkupAttribute attY = tagRenderingAction.Attributes["Y"];
												MarkupAttribute attWidth = tagRenderingAction.Attributes["Width"];
												MarkupAttribute attHeight = tagRenderingAction.Attributes["Height"];

												RectangleRenderingAction item = new RectangleRenderingAction();
												item.X = RenderingExpression.Parse(attX.Value);
												item.Y = RenderingExpression.Parse(attY.Value);
												item.Width = RenderingExpression.Parse(attWidth.Value);
												item.Height = RenderingExpression.Parse(attHeight.Value);

												item.Outline = OutlineFromTag(tagRenderingAction.Elements["Outline"] as MarkupTagElement);
												item.Fill = FillFromTag(tagRenderingAction.Elements["Fill"] as MarkupTagElement);

												rendering.Actions.Add(item);
												break;
											}
											case "Line":
											{
												LineRenderingAction item = new LineRenderingAction();

												MarkupAttribute attX1 = tagRenderingAction.Attributes["X1"];
												if (attX1 != null)
												{
													item.X1 = RenderingExpression.Parse(attX1.Value);
												}
												MarkupAttribute attX2 = tagRenderingAction.Attributes["X2"];
												if (attX2 != null)
												{
													item.X2 = RenderingExpression.Parse(attX2.Value);
												}
												MarkupAttribute attY1 = tagRenderingAction.Attributes["Y1"];
												if (attY1 != null)
												{
													item.Y1 = RenderingExpression.Parse(attY1.Value);
												}
												MarkupAttribute attY2 = tagRenderingAction.Attributes["Y2"];
												if (attY2 != null)
												{
													item.Y2 = RenderingExpression.Parse(attY2.Value);
												}

												item.Outline = OutlineFromTag(tagRenderingAction.Elements["Outline"] as MarkupTagElement);

												rendering.Actions.Add(item);
												break;
											}
											case "Text":
											{
												MarkupAttribute attX = tagRenderingAction.Attributes["X"];
												MarkupAttribute attY = tagRenderingAction.Attributes["Y"];
												MarkupAttribute attWidth = tagRenderingAction.Attributes["Width"];
												MarkupAttribute attHeight = tagRenderingAction.Attributes["Height"];

												if (attX == null || attY == null) continue;

												MarkupAttribute attHorizontalAlignment = tagRenderingAction.Attributes["HorizontalAlignment"];
												MarkupAttribute attVerticalAlignment = tagRenderingAction.Attributes["VerticalAlignment"];

												TextRenderingAction item = new TextRenderingAction();
												item.X = RenderingExpression.Parse(attX.Value);
												item.Y = RenderingExpression.Parse(attY.Value);
												
												if (attWidth != null) item.Width = RenderingExpression.Parse(attWidth.Value);
												if (attWidth != null) item.Height = RenderingExpression.Parse(attHeight.Value);

												if (attHorizontalAlignment != null)
												{
													switch (attHorizontalAlignment.Value.ToLower())
													{
														case "center":
														{
															item.HorizontalAlignment = HorizontalAlignment.Center;
															break;
														}
														case "justify":
														{
															item.HorizontalAlignment = HorizontalAlignment.Justify;
															break;
														}
														case "left":
														{
															item.HorizontalAlignment = HorizontalAlignment.Left;
															break;
														}
														case "right":
														{
															item.HorizontalAlignment = HorizontalAlignment.Right;
															break;
														}
													}
												}

												if (attVerticalAlignment != null)
												{
													switch (attVerticalAlignment.Value.ToLower())
													{
														case "bottom":
														{
															item.VerticalAlignment = VerticalAlignment.Bottom;
															break;
														}
														case "middle":
														{
															item.VerticalAlignment = VerticalAlignment.Middle;
															break;
														}
														case "top":
														{
															item.VerticalAlignment = VerticalAlignment.Top;
															break;
														}
													}
												}

												MarkupAttribute attColor = tagRenderingAction.Attributes["Color"];
												if (attColor != null) item.Color = attColor.Value;

												MarkupAttribute attFont = tagRenderingAction.Attributes["Font"];
												if (attFont != null) item.Font = attFont.Value;

												MarkupAttribute attValue = tagRenderingAction.Attributes["Value"];
												if (attValue != null) item.Value = attValue.Value;

												rendering.Actions.Add(item);
												break;
											}
										}
									}

									MarkupTagElement tagRenderingStates = (tagRendering.Elements["States"] as MarkupTagElement);
									if (tagRenderingStates != null)
									{
										// if States is specified, only apply to specific states
										foreach (MarkupElement elState in tagRenderingStates.Elements)
										{
											MarkupTagElement tagState = (elState as MarkupTagElement);
											if (tagState == null) continue;
											if (tagState.FullName != "State") continue;

											MarkupAttribute attStateID = tagState.Attributes["ID"];
											if (attStateID == null) continue;

											ThemeComponentStateReference state = new ThemeComponentStateReference();
											state.StateID = new Guid(attStateID.Value);
											rendering.States.Add(state);
										}
									}

									component.Renderings.Add(rendering);
								}
							}

							theme.Components.Add(component);
						}
					}
					themes.Themes.Add(theme);
				}
			}
		}

		private Outline OutlineFromTag(MarkupTagElement tag)
		{
			if (tag == null) return null;

			Outline outline = null;

			MarkupAttribute attOutlineType = tag.Attributes["Type"];
			if (attOutlineType != null)
			{
				switch (attOutlineType.Value.ToLower())
				{
					case "none":
					{
						break;
					}
					case "solid":
					{
						MarkupAttribute attColor = tag.Attributes["Color"];
						if (attColor != null)
						{
							SolidOutline realOutline = new SolidOutline();
							realOutline.Color = attColor.Value;
							outline = realOutline;
						}
						break;
					}
					case "inset":
					case "outset":
					{
						MarkupAttribute attLightColor = tag.Attributes["LightColor"];
						MarkupAttribute attDarkColor = tag.Attributes["DarkColor"];
						MarkupAttribute attColor = tag.Attributes["Color"];

						if ((attLightColor != null && attDarkColor != null) || (attColor != null))
						{
							ThreeDOutline realOutline = new ThreeDOutline();
							switch (attOutlineType.Value.ToLower())
							{
								case "inset":
								{
									realOutline.Type = ThreeDOutlineType.Inset;
									break;
								}
								case "outset":
								{
									realOutline.Type = ThreeDOutlineType.Outset;
									break;
								}
							}
							if (attLightColor != null && attDarkColor != null)
							{
								realOutline.LightColor = attLightColor.Value;
								realOutline.DarkColor = attDarkColor.Value;
							}
							else if (attColor != null)
							{
								realOutline.LightColor = attColor.Value;
								realOutline.DarkColor = attColor.Value;
							}
							outline = realOutline;
						}
						break;
					}
				}

				MarkupAttribute attOutlineWidth = tag.Attributes["Width"];
				if (attOutlineWidth != null && outline != null)
				{
					outline.Width = Single.Parse(attOutlineWidth.Value);
				}
			}
			return outline;
		}

		private Fill FillFromTag(MarkupTagElement tag)
		{
			if (tag == null) return null;
			
			MarkupAttribute attFillType = tag.Attributes["Type"];
			if (attFillType != null)
			{
				switch (attFillType.Value.ToLower())
				{
					case "none":
					{
						break;
					}
					case "solid":
					{
						MarkupAttribute attFillColor = tag.Attributes["Color"];
						if (attFillColor != null)
						{
							return new SolidFill(attFillColor.Value);
						}
						break;
					}
					case "lineargradient":
					{
						MarkupAttribute attOrientation = tag.Attributes["Orientation"];
						if (attOrientation == null) return null;

						MarkupTagElement tagColorStops = (tag.Elements["ColorStops"] as MarkupTagElement);
						if (tagColorStops != null)
						{
							LinearGradientFill fill = new LinearGradientFill();

							switch (attOrientation.Value.ToLower())
							{
								case "horizontal": fill.Orientation = LinearGradientFillOrientation.Horizontal; break;
								case "vertical": fill.Orientation = LinearGradientFillOrientation.Vertical; break;
							}

							foreach (MarkupElement elColorStop in tagColorStops.Elements)
							{
								MarkupTagElement tagColorStop = (elColorStop as MarkupTagElement);
								if (tagColorStop == null) continue;
								if (tagColorStop.FullName != "ColorStop") continue;

								MarkupAttribute attPosition = tagColorStop.Attributes["Position"];
								if (attPosition == null) continue;

								MarkupAttribute attColor = tagColorStop.Attributes["Color"];
								if (attColor == null) continue;

								fill.ColorStops.Add(new LinearGradientFillColorStop(attPosition.Value, attColor.Value));
							}

							return fill;
						}
						break;
					}
				}
			}
			return null;
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			ThemeObjectModel theme = (objectModels.Pop() as ThemeObjectModel);
			MarkupObjectModel mom = new MarkupObjectModel();



			objectModels.Push(mom);
		}
	}
}
