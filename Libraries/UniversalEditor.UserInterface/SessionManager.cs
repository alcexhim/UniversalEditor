using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.Accessors;
using UniversalEditor;
using MBS.Framework.UserInterface;

namespace UniversalEditor.UserInterface
{
	public class SessionManager
	{
		public class Window
		{
			public class WindowCollection
				: System.Collections.ObjectModel.Collection<Window>
			{
			}

			private int mvarLeft = 0;
			public int Left { get { return mvarLeft; } set { mvarLeft = value; } }

			private int mvarTop = 0;
			public int Top { get { return mvarTop; } set { mvarTop = value; } }

			private int mvarWidth = 0;
			public int Width { get { return mvarWidth; } set { mvarWidth = value; } }

			private int mvarHeight = 0;
			public int Height { get { return mvarHeight; } set { mvarHeight = value; } }

			private WindowState mvarWindowState = WindowState.Normal;
			public WindowState WindowState { get { return mvarWindowState; } set { mvarWindowState = value; } }

			private List<Document> mvarDocuments = new List<Document>();
			public List<Document> Documents { get { return mvarDocuments; } }

		}
		public class Session
		{
			public class SessionCollection
				: System.Collections.ObjectModel.Collection<Session>
			{
			}

			private string mvarTitle = String.Empty;
			public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

			private Window.WindowCollection mvarWindows = new Window.WindowCollection();
			public Window.WindowCollection Windows { get { return mvarWindows; } }
		}

		private string mvarDataFileName = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
		{
			Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
			"Mike Becker's Software",
			"Universal Editor",
			"Sessions.xml"
		});
		public string DataFileName { get { return mvarDataFileName; } set { mvarDataFileName = value; } }

		private Session.SessionCollection mvarSessions = new Session.SessionCollection();
		public Session.SessionCollection Sessions { get { return mvarSessions; } }

		private Version mvarFormatVersion = new Version(1, 0);

		public void Load()
		{
			MarkupObjectModel mom = new MarkupObjectModel();
			XMLDataFormat xml = new XMLDataFormat();

			if (!System.IO.File.Exists(mvarDataFileName)) return;

			Document.Load(mom, xml, new FileAccessor(mvarDataFileName), true);

			MarkupTagElement tagSessions = (mom.Elements["Sessions"] as MarkupTagElement);
			if (tagSessions == null) return;

			MarkupAttribute attVersion = tagSessions.Attributes["Version"];
			if (attVersion != null)
			{
				mvarFormatVersion = new Version(attVersion.Value);
			}

			foreach (MarkupElement elSession in tagSessions.Elements)
			{
				MarkupTagElement tagSession = (elSession as MarkupTagElement);
				if (tagSession == null) continue;
				if (tagSession.FullName != "Session") continue;

				MarkupAttribute attTitle = tagSession.Attributes["Title"];
				if (attTitle == null) continue;

				Session session = new Session();
				session.Title = attTitle.Value;

				MarkupTagElement tagWindows = (tagSession.Elements["Windows"] as MarkupTagElement);
				foreach (MarkupElement elWindow in tagWindows.Elements)
				{
					MarkupTagElement tagWindow = (elWindow as MarkupTagElement);
					if (tagWindow == null) continue;
					if (tagWindow.FullName != "Window") continue;

					Window window = new Window();

					int left = 0, top = 0, width = 600, height = 400;

					MarkupAttribute attLeft = tagWindow.Attributes["Left"];
					if (attLeft != null) Int32.TryParse(attLeft.Value, out left);
					MarkupAttribute attTop = tagWindow.Attributes["Top"];
					if (attTop != null) Int32.TryParse(attTop.Value, out top);
					MarkupAttribute attWidth = tagWindow.Attributes["Width"];
					if (attWidth != null) Int32.TryParse(attWidth.Value, out width);
					MarkupAttribute attHeight = tagWindow.Attributes["Height"];
					if (attHeight != null) Int32.TryParse(attHeight.Value, out height);

					window.Left = left;
					window.Top = top;
					window.Width = width;
					window.Height = height;

					MarkupTagElement tagDocuments = (tagWindow.Elements["Documents"] as MarkupTagElement);
					if (tagDocuments != null)
					{
						foreach (MarkupElement elDocument in tagDocuments.Elements)
						{
							MarkupTagElement tagDocument = (elDocument as MarkupTagElement);
							if (tagDocument == null) continue;
							if (tagDocument.FullName != "Document") continue;

							// TODO: Implement accessor agnosticism in Session Manager!!!
#if DEBUG
							throw new NotImplementedException();
#endif

							/*
							MarkupAttribute attFileName = tagDocument.Attributes["FileName"];
							if (attFileName == null) continue;

							window.Documents.Add(attFileName.Value);
							*/
						}
					}

					session.Windows.Add(window);
				}

				mvarSessions.Add(session);
			}
		}
		public void Save()
		{
			MarkupObjectModel mom = new MarkupObjectModel();
			UniversalEditor.ObjectModel om = mom;

			XMLDataFormat xml = new XMLDataFormat();

			MarkupPreprocessorElement xmlp = new MarkupPreprocessorElement();
			xmlp.FullName = "xml";
			xmlp.Value = "version=\"1.0\" encoding=\"UTF-8\"";
			mom.Elements.Add(xmlp);

			MarkupTagElement tagSessions = new MarkupTagElement();
			tagSessions.FullName = "Sessions";
			tagSessions.Attributes.Add("Version", mvarFormatVersion.ToString());

			mom.Elements.Add(tagSessions);

			if (mvarSessions.Count > 0)
			{
				foreach (Session session in mvarSessions)
				{
					if (session.Windows.Count < 1) continue;

					MarkupTagElement tagSession = new MarkupTagElement();
					tagSession.FullName = "Session";

					tagSession.Attributes.Add("Title", session.Title);

					MarkupTagElement tagWindows = new MarkupTagElement();
					tagWindows.FullName = "Windows";

					foreach (Window window in session.Windows)
					{
						MarkupTagElement tagWindow = new MarkupTagElement();
						tagWindow.FullName = "Window";

						tagWindow.Attributes.Add("Left", window.Left.ToString());
						tagWindow.Attributes.Add("Top", window.Top.ToString());
						tagWindow.Attributes.Add("Width", window.Width.ToString());
						tagWindow.Attributes.Add("Height", window.Height.ToString());

						if (window.Documents.Count > 0)
						{
							MarkupTagElement tagDocuments = new MarkupTagElement();
							tagDocuments.FullName = "Documents";
							foreach (Document document in window.Documents)
							{
								MarkupTagElement tagDocument = new MarkupTagElement();
								tagDocument.FullName = "Document";

#if DEBUG
								throw new NotImplementedException("Implement accessor agnosticism in Session Manager");
#endif

								// We need to store information about the ObjectModel and DataFormat
								// if the document has not been saved yet.
								if (document.ObjectModel != null)
								{
									MarkupTagElement tagObjectModel = new MarkupTagElement();
									tagObjectModel.FullName = "ObjectModel";

									ObjectModelReference omr = document.ObjectModel.MakeReference();
									if (omr.TypeName != null)
									{
										tagObjectModel.Attributes.Add("TypeName", omr.TypeName);
									}
									if (omr.ID != Guid.Empty)
									{
										tagObjectModel.Attributes.Add("ID", omr.ID.ToString("B"));
									}

									tagDocument.Elements.Add(tagObjectModel);
								}

								// tagDocument.Attributes.Add("FileName", fileName);
								tagDocuments.Elements.Add(tagDocument);
							}
							tagWindow.Elements.Add(tagDocuments);
						}

						tagWindows.Elements.Add(tagWindow);
					}
					tagSession.Elements.Add(tagWindows);

					tagSessions.Elements.Add(tagSession);
				}
			}
			
			string dir = System.IO.Path.GetDirectoryName (mvarDataFileName);
			if (!System.IO.Directory.Exists (dir))
			{
				System.IO.Directory.CreateDirectory (dir);
			}

			Document.Save(om, xml, new FileAccessor(mvarDataFileName, true, true), true);
		}
	}
}