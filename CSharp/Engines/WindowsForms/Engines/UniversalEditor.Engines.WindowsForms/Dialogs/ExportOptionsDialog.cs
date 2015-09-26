using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.Engines.WindowsForms.Dialogs
{
    public partial class ExportOptionsDialog : Form
    {
        public ExportOptionsDialog()
        {
            InitializeComponent();
        }

        private Dictionary<ExportOption, Control> exportOptionControls = new Dictionary<ExportOption, Control>();

        private DataFormat mvarDataFormat = null;
        public DataFormat DataFormat
        {
            get { return mvarDataFormat; }
            set
            {
                mvarDataFormat = value;
                cmdAbout.Enabled = (mvarDataFormat != null);
                if (mvarDataFormat == null) return;

                DataFormatReference dfr = mvarDataFormat.MakeReference();
                Type type = mvarDataFormat.GetType();

                foreach (ExportOption eo in dfr.ExportOptions)
                {
                    System.Reflection.PropertyInfo pi = type.GetProperty(eo.PropertyName);

                    Label lbl = new Label();
                    lbl.FlatStyle = FlatStyle.System;
                    lbl.Anchor = AnchorStyles.Left;
                    lbl.Text = eo.Title;
                    lbl.AutoSize = true;
                    tblGeneral.Controls.Add(lbl);

                    if (eo is ExportOptionChoice)
                    {
                        ExportOptionChoice option = (eo as ExportOptionChoice);
                        
                        ComboBox cbo = new ComboBox();
                        cbo.Dock = DockStyle.Fill;
                        if (option.RequireChoice) cbo.DropDownStyle = ComboBoxStyle.DropDownList;
                        foreach (ExportOptionFieldChoice choice in option.Choices)
                        {
                            cbo.Items.Add(choice);
                            if (pi != null)
                            {
                                object v = pi.GetValue(mvarDataFormat, null);
                                if (choice.Value == v) cbo.SelectedIndex = cbo.Items.Count - 1;
                            }
                            else if (choice.IsDefault)
                            {
                                cbo.SelectedIndex = cbo.Items.Count - 1;
                            }
                        }
                        tblGeneral.Controls.Add(cbo);

                        exportOptionControls.Add(option, cbo);
                    }
                    else if (eo is ExportOptionNumber)
                    {
                        ExportOptionNumber option = (eo as ExportOptionNumber);

                        NumericUpDown txt = new NumericUpDown();
                        txt.Dock = DockStyle.Fill;
                        if (option.MaximumValue.HasValue)
                        {
                            txt.Maximum = option.MaximumValue.Value;
                        }
                        else
                        {
                            txt.Maximum = Decimal.MaxValue;
                        }
                        if (option.MinimumValue.HasValue)
                        {
                            txt.Minimum = option.MinimumValue.Value;
                        }
                        else
                        {
                            txt.Minimum = Decimal.MinValue;
                        }
                        if (pi != null)
                        {
                            txt.Value = (decimal)Convert.ChangeType(pi.GetValue(mvarDataFormat, null), typeof(Decimal));
                        }
                        else
                        {
                            txt.Value = option.DefaultValue;
                        }
                        
                        tblGeneral.Controls.Add(txt);

                        exportOptionControls.Add(option, txt);
                    }
                    else if (eo is ExportOptionText)
                    {
                        ExportOptionText option = (eo as ExportOptionText);

                        TextBox txt = new TextBox();
                        if (pi != null)
                        {
                            txt.Text = pi.GetValue(mvarDataFormat, null).ToString();
                        }
                        else
                        {
                            txt.Text = option.DefaultValue;
                        }

                        txt.Dock = DockStyle.Fill;
                        tblGeneral.Controls.Add(txt);

                        exportOptionControls.Add(option, txt);
                    }

                }

                for (int i = 0; i < tblGeneral.RowStyles.Count; i++)
                {
                    tblGeneral.RowStyles[i].Height = 24;
                    tblGeneral.RowStyles[i].SizeType = SizeType.Absolute;
                }

                Label lblSpacer = new Label();
                lblSpacer.Dock = DockStyle.Fill;
                tblGeneral.Controls.Add(lblSpacer);
                tblGeneral.SetColumnSpan(lblSpacer, 2);

                Font = SystemFonts.MenuFont;
            }
        }

        private void cmdAbout_Click(object sender, EventArgs e)
        {
            DataFormatAboutDialog dlg = new DataFormatAboutDialog();
            dlg.DataFormatReference = mvarDataFormat.MakeReference();
            dlg.ShowDialog();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            DataFormatReference dfr = mvarDataFormat.MakeReference();
            Type type = mvarDataFormat.GetType();

            foreach (ExportOption eo in dfr.ExportOptions)
            {
                System.Reflection.PropertyInfo pi = type.GetProperty(eo.PropertyName);
                if (pi == null) continue;

                Control ctl = exportOptionControls[eo];
                if (ctl is NumericUpDown)
                {
                    NumericUpDown txt = (ctl as NumericUpDown);
                    pi.SetValue(mvarDataFormat, Convert.ChangeType(txt.Value, pi.PropertyType), null);
                }
                else if (ctl is ComboBox)
                {
                    ExportOptionFieldChoice choice = ((ctl as ComboBox).SelectedItem as ExportOptionFieldChoice);
                    if (choice != null)
                    {
                        pi.SetValue(mvarDataFormat, Convert.ChangeType(choice.Value, pi.PropertyType), null);
                    }
                }
                else if (ctl is TextBox)
                {
                    TextBox txt = (ctl as TextBox);
                    pi.SetValue(mvarDataFormat, Convert.ChangeType(txt.Text, pi.PropertyType), null);
                }
            }


            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
