//
//  EditorPage.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
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
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Layouts;
using MBS.Framework.UserInterface;
using UniversalEditor.ObjectModels.Text.Plain;
using UniversalEditor.ObjectModels.Binary;

namespace UniversalEditor.UserInterface.Pages
{
	public partial class EditorPage : FilePage // , AwesomeControls.MultipleDocumentContainer.IMultipleDocumentContainerProgressBroadcaster // wtf is this???
	{
		public event EventHandler DocumentEdited;
		protected virtual void OnDocumentEdited(EventArgs e)
		{
			if (DocumentEdited != null) DocumentEdited(this, e);
		}

		public EditorPage()
		{
			this.Layout = new BoxLayout (Orientation.Vertical);
		}

		/*
		public event AwesomeControls.MultipleDocumentContainer.MultipleDocumentContainerProgressEventHandler Progress;
		protected virtual void OnProgress(AwesomeControls.MultipleDocumentContainer.MultipleDocumentContainerProgressEventArgs e)
		{
			if (Progress != null) Progress(this, e);
		}
		*/

		/*
		private ObjectModel mvarObjectModel = null;
		public ObjectModel ObjectModel
		{
			get { return mvarObjectModel; }
			set
			{
				if (mvarObjectModel != value)
				{
					ObjectModelChangingEventArgs ce = new ObjectModelChangingEventArgs(mvarObjectModel, value);
					OnObjectModelChanging(ce);
					if (ce.Cancel) return;
				}
				mvarObjectModel = value;

				RefreshEditor();
				OnObjectModelChanged(EventArgs.Empty);
			}
		}
		*/

		/// <summary>
		/// try to determine within a reasonable doubt whether or not <see cref="filename" /> is a "plain text" file (e.g. ASCII, UTF-8, UTF-16lE, UTF-16BE, UTF-32, etc.)
		/// </summary>
		/// <returns><c>true</c>, if the specified file appears to be a text file, <c>false</c> otherwise.</returns>
		/// <param name="filename">Filename.</param>
		private bool isText(Accessor acc)
		{
			int len = 2048;
			len = (int)Math.Min(len, acc.Length);

			acc.Seek(0, IO.SeekOrigin.Begin);
			byte[] b = acc.Reader.ReadBytes(len);
			acc.Seek(-len, IO.SeekOrigin.Current);

			string utf8 = System.Text.Encoding.UTF8.GetString(b);

			// yes I know this isn't the best way to do this
			bool isUTF8 = (b.Length >= 3 && b[0] == 0xEF && b[1] == 0xBB && b[2] == 0xBF);
			int start = isUTF8 ? 3 : 0;
			for (int i = start; i < utf8.Length; i++)
			{
				if (Char.IsControl(utf8[i]) && !Char.IsWhiteSpace(utf8[i]))
				{
					// control character, so bail out
					return false;
				}
			}
			return true;
		}

		private EditorReference DefaultBinaryEditor = new EditorReference(typeof(Editors.Binary.BinaryEditor));
		private EditorReference DefaultTextEditor = new EditorReference(typeof(Editors.Text.Plain.PlainTextEditor));

		private Document mvarDocument = null;
		public Document Document
		{
			get { return mvarDocument; }
			set
			{
				if (mvarDocument == value) return;
				mvarDocument = value;

				RefreshEditor();
			}
		}

		private void RefreshEditor()
		{
			if (mvarDocument == null) return;

			// pnlLoading.Enabled = true;
			// pnlLoading.Visible = true;

			string title = String.IsNullOrEmpty(Title) ? Document.Title : Title;

			ObjectModel om = null;
			EditorReference[] reditors = new EditorReference[0];
			if (mvarDocument.ObjectModel != null)
			{
				om = mvarDocument.ObjectModel;
				reditors = UniversalEditor.UserInterface.Common.Reflection.GetAvailableEditors(om.MakeReference());
			}
			if (reditors.Length == 0)
			{
				// errorMessage1.Enabled = true;
				// errorMessage1.Visible = true;

				// errorMessage1.Details = "Detected object model: " + om.GetType().FullName;

				bool requiresOpen = false;
				if (mvarDocument.Accessor == null)
					return;

				if (!mvarDocument.Accessor.IsOpen)
				{
					mvarDocument.Accessor.Open();
					requiresOpen = true;
				}

				Editor ed = null;
				if (isText(mvarDocument.Accessor))
				{
					ed = DefaultTextEditor.Create();

					PlainTextObjectModel om1 = new PlainTextObjectModel();
					if (mvarDocument.Accessor.Length < Math.Pow(1024, 2))
					{
						string value = mvarDocument.Accessor.Reader.ReadStringToEnd();
						om1.Text = value;
					}
					ed.ObjectModel = om1;
				}
				else
				{
					ed = DefaultBinaryEditor.Create();
					BinaryObjectModel om1 = new BinaryObjectModel();
					if (mvarDocument.Accessor.Length < Math.Pow(1024, 4))
					{
						byte[] content = mvarDocument.Accessor.Reader.ReadToEnd();
						om1.Data = content;
					}
					ed.ObjectModel = om1;
				}
				ed.Document = Document;

				if (requiresOpen)
					mvarDocument.Accessor.Close();

				if (ed == null) return;
				ed.Title = title;
				ed.DocumentEdited += editor_DocumentEdited;
				mvarDocument.ObjectModel = ed.ObjectModel;

				Controls.Add(ed, new BoxLayout.Constraints(true, true));
			}
			else
			{
				// this MIGHT or MIGHT NOT be a performance improvement, but it comes about as a rather ugly hack
				// to deal with deficiencies currently present in TreeModels/GtkTreeView and control collection management
				bool changed = false;
				if (Controls.Count - 1 == reditors.Length)
				{
					for (int i = 0; i < Controls.Count - 1; i++)
					{
						if (reditors[i].EditorType != Controls[i].GetType())
						{
							// if one of our editors doesn't match what we currently have, we've obviously changed editors
							changed = true;
							break;
						}
					}
				}
				else
				{
					// if we have more or less editors than currently present in the control, we've obviously changed editors
					changed = true;
				}

				if (changed)
				{
					// only re-create the control collection if we've actually had a change
					// otherwise, this causes an exact replica of the original editor to be loaded, and sometimes breaks things
					// (e.g. FileSystemEditor does not work properly)

					// this is definitely an underlying bug in the UWT implementation tracking GtkTreeView and GtkTreeModel handles, but we can sweep it under the rug
					// for the moment by only refreshing the control collection if we REALLY need a different Editor (in which case, since it's a different Editor, the
					// problem no longer manifests itself)
					Controls.Clear();

					Container tbEditorsAndViews = new Container();
					tbEditorsAndViews.Layout = new GridLayout();
					for (int i = 0; i < reditors.Length; i++)
					{
						EditorReference reditor = reditors[i];
						Editor editor = reditor.Create();

						// editor.Dock = DockStyle.Fill;
						editor.ObjectModel = om;
						editor.DocumentEdited += editor_DocumentEdited;
						editor.Title = title;
						for (int j = 0; j < reditor.Views.Count; j++)
						{
							EditorView view = reditor.Views[j];
							Button btn = new Button();
							btn.BorderStyle = ButtonBorderStyle.None;
							btn.Text = view.Title;
							btn.Click += tibEditorView_Click;
							btn.SetExtraData<Editor>("editor", editor);
							btn.SetExtraData<EditorView>("view", view);
							btn.HorizontalAlignment = HorizontalAlignment.Left;
							// btn.DisplayStyle = ToolbarItemDisplayStyle.ImageAndText;
							tbEditorsAndViews.Controls.Add(btn, new GridLayout.Constraints(0, i + j));
						}
						Controls.Add(editor, new BoxLayout.Constraints(true, true));
					}
					Controls.Add(tbEditorsAndViews, new BoxLayout.Constraints(false, false));
				}
				else
				{
					// the Editors are all the same, so just update them with the new ObjectModel
					for (int i = 0; i < Controls.Count - 1; i++)
					{
						(Controls[i] as Editor).ObjectModel = om;
						(Controls[i] as Editor).Title = title;
					}
				}
			}
		}

		private void tibEditorView_Click(object sender, EventArgs e)
		{
			Button tib = (sender as Button);
			Editor editor = tib.GetExtraData<Editor>("editor");
			EditorView view = tib.GetExtraData<EditorView>("view");
			editor.CurrentView = view;

			Console.WriteLine("Switching to view '" + view.Title + "'");
		}


		private void editor_DocumentEdited(object sender, EventArgs e)
		{
			OnDocumentEdited(e);
		}

		/*
		public event ObjectModelChangingEventHandler ObjectModelChanging;c
		protected virtual void OnObjectModelChanging(ObjectModelChangingEventArgs e)
		{
			if (ObjectModelChanging != null) ObjectModelChanging(this, e);
		}
		public event EventHandler ObjectModelChanged;
		protected virtual void OnObjectModelChanged(EventArgs e)
		{
			if (ObjectModelChanged != null) ObjectModelChanged(this, e);
		}
		*/

		/// <summary>
		/// The event that is raised when a file has loaded successfully in the editor.
		/// </summary>
		public event EventHandler FileOpened;
		protected virtual void OnFileOpened(object sender, EventArgs e)
		{
			if (FileOpened != null) FileOpened(this, e);
		}

		private void NotifyOnFileOpened(object sender, EventArgs e)
		{
			Action<object, EventArgs> _NotifyOnFileOpened = new Action<object, EventArgs>(OnFileOpened);
			// if (IsHandleCreated) Invoke(_NotifyOnFileOpened, sender, e);
		}

		public void OpenFile(Document document)
		{
			this.Document = document;

			if (System.IO.Directory.Exists(document.Accessor.GetFileName()))
			{
				// we're looking at a directory, so create a FileSystemObjectModel for it
				FileSystemObjectModel fsom = FileSystemObjectModel.FromDirectory(document.Accessor.GetFileName());

				Document = new Document(fsom, null, null);
				NotifyOnFileOpened(this, EventArgs.Empty);
				RefreshEditor();
				return;
			}

			System.Threading.Thread tOpenFile = new System.Threading.Thread(tOpenFile_ThreadStart);
			tOpenFile.Start();
		}

		private void _ErrorMessageConfig(bool visible, string title)
		{
			// errorMessage1.Visible = visible;
			// errorMessage1.Enabled = visible;
			// if (title != null) errorMessage1.Title = title;
		}
		private void _ErrorMessageConfig(bool visible, string title, string message)
		{
			// errorMessage1.Visible = visible;
			// errorMessage1.Enabled = visible;
			// if (title != null) errorMessage1.Title = title;
			// if (message != null) errorMessage1.Details = message;
		}

		private void tOpenFile_ThreadStart()
		{
			if (mvarDocument.DataFormat == null)
			{
				// TODO: DataFormat-guessing should be implemented in the platform-
				// independent UserInterface library, or possibly in core

				DataFormatReference[] fmts = UniversalEditor.Common.Reflection.GetAvailableDataFormats(mvarDocument.Accessor);
				#region When there is no available DataFormat
				if (fmts.Length == 0)
				{
					// Invoke(new Action<bool, string>(_ErrorMessageConfig), true, "There are no data formats available for this file");
				}
				#endregion
				#region When there is more than one DataFormat
				else if (fmts.Length > 1)
				{
					// attempt to guess the data format for the object model
					ObjectModelReference[] objms = UniversalEditor.Common.Reflection.GetAvailableObjectModels(mvarDocument.Accessor);
					ObjectModel om1 = null;
					DataFormat df1 = null;
					bool found1 = false;
					bool notimplemented = false;
					foreach (ObjectModelReference omr in objms)
					{
						EditorReference[] reditors = UniversalEditor.UserInterface.Common.Reflection.GetAvailableEditors(omr);
						if (reditors.Length < 1) continue;

						ObjectModel om = omr.Create();

						bool found = false;
						DataFormat df = null;
						foreach (DataFormatReference dfr in fmts)
						{
							df = dfr.Create();

							Document d = new UniversalEditor.Document(om, df, mvarDocument.Accessor);
							d.InputAccessor.Open();
							try
							{
								d.Load();
								found = true;
								break;
							}
							catch (InvalidDataFormatException ex)
							{
								continue;
							}
							catch (NotImplementedException ex)
							{
								notimplemented = true;
								continue;
							}
							catch (NotSupportedException ex)
							{
								continue;
							}
							catch (DataCorruptedException ex)
							{
								// MessageBox.Show("The data is corrupted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
								found = true;
								break;
							}
							catch (ArgumentException ex)
							{
								found = true;
								break;
							}
							finally
							{
								// do not close input accessor since we may need to read from it later (i.e. if
								// it's a FileSystemObjectModel with deferred file data loading)

								// d.InputAccessor.Close();
							}
						}

						if (found)
						{
							notimplemented = false;
							om1 = om;
							df1 = df;
							found1 = true;
							break;
						}
					}

					if (!found1)
					{
						// Invoke(new Action<bool, string>(_ErrorMessageConfig), true, "There are no data formats available for this file");
					}
					else if (notimplemented)
					{
						// Invoke(new Action<bool, string>(_ErrorMessageConfig), true, "The data format for this file has not been implemented");
					}
					else
					{
						Document = new Document(om1, df1, mvarDocument.Accessor);

						// if (IsHandleCreated) Invoke(new Action<bool, string>(_ErrorMessageConfig), false, null);

						NotifyOnFileOpened(this, EventArgs.Empty);
					}
				}
				#endregion
				#region When there is exactly one DataFormat
				else
				{
					ObjectModelReference[] objms = UniversalEditor.Common.Reflection.GetAvailableObjectModels(fmts[0]);
					if (objms.Length >= 1)
					{
						/*
						if (objms.Length > 1)
						{
							// TODO: Attempt to sort available object models by relevance
							Pages.MultipleObjectModelPage page = new Pages.MultipleObjectModelPage();
							// page.DataFormat = fmts[0].Create();
							// page.FileName = FileName;
							// page.SelectionChanged += MultipleObjectModelPage_SelectionChanged;
							mdcc.Documents.Add(System.IO.Path.GetFileName(FileName), page);
						}
						else if (objms.Length == 1)
						{
						*/
						try
						{
							ObjectModel objm = objms[0].Create();
							DataFormat fmt = fmts[0].Create();

							if (!Engine.CurrentEngine.ShowCustomOptionDialog(ref fmt, CustomOptionDialogType.Import)) return;

							Document document = new UniversalEditor.Document(objm, fmt, mvarDocument.Accessor);
							document.InputAccessor.Open();
							document.Load();

							Document = document;
							NotifyOnFileOpened(this, EventArgs.Empty);
						}
						catch (InvalidDataFormatException ex)
						{
							// Invoke(new Action<bool, string, string>(_ErrorMessageConfig), true, ex.GetType().FullName, ex.Message);
						}
					}
					else
					{

					}
				}
				#endregion
			}
			else
			{
				ObjectModel objm = mvarDocument.ObjectModel;
				DataFormat fmt = mvarDocument.DataFormat;

				if (!Engine.CurrentEngine.ShowCustomOptionDialog(ref fmt, CustomOptionDialogType.Import)) return;

				Document document = new UniversalEditor.Document(objm, fmt, mvarDocument.Accessor);
				document.InputAccessor.Open();
				document.Load();

				Document = document;
				NotifyOnFileOpened(this, EventArgs.Empty);
			}

			RefreshEditor();
		}
	}
}