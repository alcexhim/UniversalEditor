using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Pages
{
    public partial class EmbeddedEditorPage : Page
    {
        public event EventHandler DocumentEdited;
        protected virtual void OnDocumentEdited(EventArgs e)
        {
            if (DocumentEdited != null) DocumentEdited(this, e);
        }

        public EmbeddedEditorPage()
        {
            InitializeComponent();
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
            Controls.Clear();
            if (mvarDocument == null) return;
            if (mvarDocument.ObjectModel == null) return;

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

                if (ieditors.Length == 1)
                {
                    IEditorImplementation ieditor = ieditors[0];
                    if (ieditor is Editor)
                    {
                        Editor editor = (ieditor as Editor);
                        editor.Dock = DockStyle.Fill;
                        editor.ObjectModel = om;
                        editor.DocumentEdited += editor_DocumentEdited;

                        Controls.Add(editor);
                    }
                }
                else
                {
                    TabControl tbs = new TabControl();
                    tbs.Dock = DockStyle.Fill;

                    foreach (IEditorImplementation ieditor in ieditors)
                    {
                        if (ieditor is Editor)
                        {
                            Editor editor = (ieditor.GetType().Assembly.CreateInstance(ieditor.GetType().FullName) as Editor);
                            editor.Dock = DockStyle.Fill;
                            editor.ObjectModel = om;
                            editor.DocumentEdited += editor_DocumentEdited;

                            TabPage tab = new TabPage();
                            tab.Text = editor.Title;

                            tab.Controls.Add(editor);
                            tbs.TabPages.Add(tab);
                        }
                    }

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
    }
}