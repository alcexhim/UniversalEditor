using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.UserInterface.WindowsForms.Pages
{
    public partial class EditorPage : FilePage, AwesomeControls.MultipleDocumentContainer.IMultipleDocumentContainerProgressBroadcaster
    {
        public event EventHandler DocumentEdited;
        protected virtual void OnDocumentEdited(EventArgs e)
        {
            if (DocumentEdited != null) DocumentEdited(this, e);
        }

        public EditorPage()
        {
            InitializeComponent();
        }

        public event AwesomeControls.MultipleDocumentContainer.MultipleDocumentContainerProgressEventHandler Progress;
        protected virtual void OnProgress(AwesomeControls.MultipleDocumentContainer.MultipleDocumentContainerProgressEventArgs e)
        {
            if (Progress != null) Progress(this, e);
        }

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
            if (InvokeRequired)
            {
                while (!IsHandleCreated)
                {
                    System.Threading.Thread.Sleep(500);
                }

                Action _RefreshEditor = new Action(RefreshEditorInternal);
                Invoke(_RefreshEditor);
            }
            else
            {
                RefreshEditorInternal();
            }
        }

        private void RefreshEditorInternal()
        {
            if (mvarDocument == null) return;
            if (mvarDocument.ObjectModel == null) return;

            pnlLoading.Enabled = true;
            pnlLoading.Visible = true;

            ObjectModel om = mvarDocument.ObjectModel;
            IEditorImplementation[] ieditors = UniversalEditor.UserInterface.Common.Reflection.GetAvailableEditors(om.MakeReference());
            if (ieditors.Length == 0)
            {
                errorMessage1.Enabled = true;
                errorMessage1.Visible = true;

                errorMessage1.Details = "Detected object model: " + om.GetType().FullName;
            }
            else
            {
                errorMessage1.Enabled = false;
                errorMessage1.Visible = false;

                Controls.Clear();
                Controls.Add(pnlLoading);

                List<Editor> realEditors = new List<Editor>();
                foreach (IEditorImplementation ieditor in ieditors)
                {
                    if (ieditor is Editor) realEditors.Add((ieditor as Editor));
                }

                if (realEditors.Count == 1)
                {
                    IEditorImplementation ieditor = ieditors[0];
                    if (ieditor is Editor)
                    {
                        Editor editor = (ieditor.GetType().Assembly.CreateInstance(ieditor.GetType().FullName) as Editor);
                        editor.Dock = DockStyle.Fill;
                        editor.ObjectModel = om;
                        editor.DocumentEdited += editor_DocumentEdited;

                        pnlLoading.Visible = false;
                        pnlLoading.Enabled = false;
                        Controls.Add(editor);
                    }
                }
                else
                {
                    TabControl tbs = new TabControl();
                    tbs.Dock = DockStyle.Fill;

                    foreach (IEditorImplementation ieditor in realEditors)
                    {
                        if (ieditor is Editor)
                        {
                            System.Reflection.Assembly asm = ieditor.GetType().Assembly;
                            Editor editor = (asm.CreateInstance(ieditor.GetType().FullName) as Editor);
                            editor.Dock = DockStyle.Fill;
                            editor.ObjectModel = om;
                            editor.DocumentEdited += editor_DocumentEdited;

                            TabPage tab = new TabPage();
                            tab.Text = editor.Title;

                            tab.Controls.Add(editor);
                            tbs.TabPages.Add(tab);
                        }
                    }

                    pnlLoading.Visible = false;
                    pnlLoading.Enabled = false;
                    Controls.Add(tbs);
                }
            }
        }

        private void editor_DocumentEdited(object sender, EventArgs e)
        {
            OnDocumentEdited(e);
        }

        /*
        public event ObjectModelChangingEventHandler ObjectModelChanging;
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
            if (IsHandleCreated) Invoke(_NotifyOnFileOpened, sender, e);
        }

        public void OpenFile(string FileName)
        {
            this.FileName = FileName;

            System.Threading.Thread tOpenFile = new System.Threading.Thread(tOpenFile_ParameterizedThreadStart);
            tOpenFile.Start(FileName);
        }

        private void _ErrorMessageConfig(bool visible, string title)
        {
            errorMessage1.Visible = visible;
            errorMessage1.Enabled = visible;
            if (title != null) errorMessage1.Title = title;
        }
        private void _ErrorMessageConfig(bool visible, string title, string message)
        {
            errorMessage1.Visible = visible;
            errorMessage1.Enabled = visible;
            if (title != null) errorMessage1.Title = title;
            if (message != null) errorMessage1.Details = message;
        }

        private void tOpenFile_ParameterizedThreadStart(object param)
        {
            string Path = param.ToString();

            string[] FileNameParts = Path.Split(new string[] { "::/" }, 2, StringSplitOptions.None);
            string FileName = FileNameParts[0];
            string SecondaryFileName = String.Empty;
            if (FileNameParts.Length > 1) SecondaryFileName = FileNameParts[1];

            DataFormatReference[] fmts = UniversalEditor.Common.Reflection.GetAvailableDataFormats(FileName);
            #region When there is no available DataFormat
            if (fmts.Length == 0)
            {
                Invoke(new Action<bool, string>(_ErrorMessageConfig), true, "There are no data formats available for this file");
            }
            #endregion
            #region When there is more than one DataFormat
            else if (fmts.Length > 1)
            {
                // attempt to guess the data format for the object model
                ObjectModelReference[] objms = UniversalEditor.Common.Reflection.GetAvailableObjectModels(FileName);
                ObjectModel om1 = null;
                DataFormat df1 = null;
                bool found1 = false;
                bool notimplemented = false;
                foreach (ObjectModelReference omr in objms)
                {
                    IEditorImplementation[] editors = UniversalEditor.UserInterface.Common.Reflection.GetAvailableEditors(omr);
                    if (editors.Length < 1) continue;

                    ObjectModel om = omr.Create();

                    bool found = false;
                    DataFormat df = null;
                    foreach (DataFormatReference dfr in fmts)
                    {
                        df = dfr.Create();

						Document d = new UniversalEditor.Document(om, df, new FileAccessor(FileName));
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
                    Invoke(new Action<bool, string>(_ErrorMessageConfig), true, "There are no data formats available for this file");
                }
                else if (notimplemented)
                {
                    Invoke(new Action<bool, string>(_ErrorMessageConfig), true, "The data format for this file has not been implemented");
                }
                else
                {
                    Document = new Document(om1, df1, new FileAccessor(FileName));

                    if (IsHandleCreated) Invoke(new Action<bool, string>(_ErrorMessageConfig), false, null);

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
                        // TODO: Attempt to sort available object models by
                        // relevance.
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

                        if (!String.IsNullOrEmpty(SecondaryFileName))
                        {
                            FileSystemObjectModel fsom = (objm as FileSystemObjectModel);
							Document document = new UniversalEditor.Document(fsom, fmt, new FileAccessor(FileName));

                            object fso = fsom.FindObject(SecondaryFileName);
                            if (fso is UniversalEditor.ObjectModels.FileSystem.File)
                            {
                                UniversalEditor.ObjectModels.FileSystem.File file = (fso as UniversalEditor.ObjectModels.FileSystem.File);
                                // OpenFileInternal(file, FileName);
                            }
                            else if (fso is UniversalEditor.ObjectModels.FileSystem.Folder)
                            {
                            }
                            else
                            {
                            }
                            return;
                        }
                        else
                        {
                            if (!Engine.CurrentEngine.ShowDataFormatOptionsDialog(ref fmt, DataFormatOptionsDialogType.Import)) return;

							Document document = new UniversalEditor.Document(objm, fmt, new FileAccessor(FileName));
							document.InputAccessor.Open();
							document.Load();

                            Document = document;
                            NotifyOnFileOpened(this, EventArgs.Empty);
                        }
                    }
                    catch (InvalidDataFormatException ex)
                    {
                        Invoke(new Action<bool, string, string>(_ErrorMessageConfig), true, ex.GetType().FullName, ex.Message);
                    }
                }
                else
                {

                }
            }
            #endregion

            RefreshEditor();
        }
    }
}