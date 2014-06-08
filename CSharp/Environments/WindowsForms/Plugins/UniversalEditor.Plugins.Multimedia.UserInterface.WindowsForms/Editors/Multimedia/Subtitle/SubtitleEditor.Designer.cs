namespace UniversalEditor.Editors.Multimedia.Subtitle
{
    partial class SubtitleEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.lvEvents = new System.Windows.Forms.ListView();
			this.chEventActor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chEventStart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chEventEnd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chEventDuration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chEventText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.fraEvents = new System.Windows.Forms.GroupBox();
			this.cmdEventsClear = new System.Windows.Forms.Button();
			this.cmdEventsRemove = new System.Windows.Forms.Button();
			this.cmdEventsModify = new System.Windows.Forms.Button();
			this.cmdEventsAdd = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.fraStyles = new System.Windows.Forms.GroupBox();
			this.cmdStyleClear = new System.Windows.Forms.Button();
			this.cmdStyleAdd = new System.Windows.Forms.Button();
			this.cmdStyleModify = new System.Windows.Forms.Button();
			this.cmdStyleRemove = new System.Windows.Forms.Button();
			this.lvStyles = new System.Windows.Forms.ListView();
			this.chStyleName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.fraActors = new System.Windows.Forms.GroupBox();
			this.cmdActorClear = new System.Windows.Forms.Button();
			this.cmdActorAdd = new System.Windows.Forms.Button();
			this.cmdActorRemove = new System.Windows.Forms.Button();
			this.cmdActorModify = new System.Windows.Forms.Button();
			this.lvActors = new System.Windows.Forms.ListView();
			this.chActorName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.fraEvents.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.fraStyles.SuspendLayout();
			this.fraActors.SuspendLayout();
			this.SuspendLayout();
			// 
			// lvEvents
			// 
			this.lvEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chEventActor,
            this.chEventStart,
            this.chEventEnd,
            this.chEventDuration,
            this.chEventText});
			this.lvEvents.FullRowSelect = true;
			this.lvEvents.GridLines = true;
			this.lvEvents.HideSelection = false;
			this.lvEvents.Location = new System.Drawing.Point(6, 48);
			this.lvEvents.Name = "lvEvents";
			this.lvEvents.Size = new System.Drawing.Size(654, 118);
			this.lvEvents.TabIndex = 0;
			this.lvEvents.UseCompatibleStateImageBehavior = false;
			this.lvEvents.View = System.Windows.Forms.View.Details;
			// 
			// chEventActor
			// 
			this.chEventActor.Text = "Actor";
			// 
			// chEventStart
			// 
			this.chEventStart.Text = "Start";
			// 
			// chEventEnd
			// 
			this.chEventEnd.Text = "End";
			// 
			// chEventDuration
			// 
			this.chEventDuration.Text = "Duration";
			// 
			// chEventText
			// 
			this.chEventText.Text = "Text";
			this.chEventText.Width = 403;
			// 
			// fraEvents
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.fraEvents, 2);
			this.fraEvents.Controls.Add(this.cmdEventsClear);
			this.fraEvents.Controls.Add(this.cmdEventsRemove);
			this.fraEvents.Controls.Add(this.cmdEventsModify);
			this.fraEvents.Controls.Add(this.cmdEventsAdd);
			this.fraEvents.Controls.Add(this.lvEvents);
			this.fraEvents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fraEvents.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraEvents.Location = new System.Drawing.Point(3, 180);
			this.fraEvents.Name = "fraEvents";
			this.fraEvents.Size = new System.Drawing.Size(666, 172);
			this.fraEvents.TabIndex = 1;
			this.fraEvents.TabStop = false;
			this.fraEvents.Text = "Events";
			// 
			// cmdEventsClear
			// 
			this.cmdEventsClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdEventsClear.Location = new System.Drawing.Point(585, 19);
			this.cmdEventsClear.Name = "cmdEventsClear";
			this.cmdEventsClear.Size = new System.Drawing.Size(75, 23);
			this.cmdEventsClear.TabIndex = 1;
			this.cmdEventsClear.Text = "Cl&ear";
			this.cmdEventsClear.UseVisualStyleBackColor = true;
			// 
			// cmdEventsRemove
			// 
			this.cmdEventsRemove.Location = new System.Drawing.Point(168, 19);
			this.cmdEventsRemove.Name = "cmdEventsRemove";
			this.cmdEventsRemove.Size = new System.Drawing.Size(75, 23);
			this.cmdEventsRemove.TabIndex = 1;
			this.cmdEventsRemove.Text = "&Remove";
			this.cmdEventsRemove.UseVisualStyleBackColor = true;
			// 
			// cmdEventsModify
			// 
			this.cmdEventsModify.Location = new System.Drawing.Point(87, 19);
			this.cmdEventsModify.Name = "cmdEventsModify";
			this.cmdEventsModify.Size = new System.Drawing.Size(75, 23);
			this.cmdEventsModify.TabIndex = 1;
			this.cmdEventsModify.Text = "&Modify...";
			this.cmdEventsModify.UseVisualStyleBackColor = true;
			// 
			// cmdEventsAdd
			// 
			this.cmdEventsAdd.Location = new System.Drawing.Point(6, 19);
			this.cmdEventsAdd.Name = "cmdEventsAdd";
			this.cmdEventsAdd.Size = new System.Drawing.Size(75, 23);
			this.cmdEventsAdd.TabIndex = 1;
			this.cmdEventsAdd.Text = "&Add...";
			this.cmdEventsAdd.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.fraEvents, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.fraStyles, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.fraActors, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(672, 355);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// fraStyles
			// 
			this.fraStyles.Controls.Add(this.cmdStyleClear);
			this.fraStyles.Controls.Add(this.cmdStyleAdd);
			this.fraStyles.Controls.Add(this.cmdStyleModify);
			this.fraStyles.Controls.Add(this.cmdStyleRemove);
			this.fraStyles.Controls.Add(this.lvStyles);
			this.fraStyles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fraStyles.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraStyles.Location = new System.Drawing.Point(3, 3);
			this.fraStyles.Name = "fraStyles";
			this.fraStyles.Size = new System.Drawing.Size(330, 171);
			this.fraStyles.TabIndex = 2;
			this.fraStyles.TabStop = false;
			this.fraStyles.Text = "Styles";
			// 
			// cmdStyleClear
			// 
			this.cmdStyleClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdStyleClear.Location = new System.Drawing.Point(249, 19);
			this.cmdStyleClear.Name = "cmdStyleClear";
			this.cmdStyleClear.Size = new System.Drawing.Size(75, 23);
			this.cmdStyleClear.TabIndex = 1;
			this.cmdStyleClear.Text = "Cl&ear";
			this.cmdStyleClear.UseVisualStyleBackColor = true;
			// 
			// cmdStyleAdd
			// 
			this.cmdStyleAdd.Location = new System.Drawing.Point(6, 19);
			this.cmdStyleAdd.Name = "cmdStyleAdd";
			this.cmdStyleAdd.Size = new System.Drawing.Size(75, 23);
			this.cmdStyleAdd.TabIndex = 1;
			this.cmdStyleAdd.Text = "&Add...";
			this.cmdStyleAdd.UseVisualStyleBackColor = true;
			// 
			// cmdStyleModify
			// 
			this.cmdStyleModify.Location = new System.Drawing.Point(87, 19);
			this.cmdStyleModify.Name = "cmdStyleModify";
			this.cmdStyleModify.Size = new System.Drawing.Size(75, 23);
			this.cmdStyleModify.TabIndex = 1;
			this.cmdStyleModify.Text = "&Modify...";
			this.cmdStyleModify.UseVisualStyleBackColor = true;
			// 
			// cmdStyleRemove
			// 
			this.cmdStyleRemove.Location = new System.Drawing.Point(168, 19);
			this.cmdStyleRemove.Name = "cmdStyleRemove";
			this.cmdStyleRemove.Size = new System.Drawing.Size(75, 23);
			this.cmdStyleRemove.TabIndex = 1;
			this.cmdStyleRemove.Text = "&Remove";
			this.cmdStyleRemove.UseVisualStyleBackColor = true;
			// 
			// lvStyles
			// 
			this.lvStyles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvStyles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chStyleName});
			this.lvStyles.FullRowSelect = true;
			this.lvStyles.GridLines = true;
			this.lvStyles.HideSelection = false;
			this.lvStyles.Location = new System.Drawing.Point(6, 48);
			this.lvStyles.Name = "lvStyles";
			this.lvStyles.Size = new System.Drawing.Size(318, 117);
			this.lvStyles.TabIndex = 0;
			this.lvStyles.UseCompatibleStateImageBehavior = false;
			this.lvStyles.View = System.Windows.Forms.View.Details;
			// 
			// chStyleName
			// 
			this.chStyleName.Text = "Name";
			this.chStyleName.Width = 304;
			// 
			// fraActors
			// 
			this.fraActors.Controls.Add(this.cmdActorClear);
			this.fraActors.Controls.Add(this.cmdActorAdd);
			this.fraActors.Controls.Add(this.cmdActorRemove);
			this.fraActors.Controls.Add(this.cmdActorModify);
			this.fraActors.Controls.Add(this.lvActors);
			this.fraActors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fraActors.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraActors.Location = new System.Drawing.Point(339, 3);
			this.fraActors.Name = "fraActors";
			this.fraActors.Size = new System.Drawing.Size(330, 171);
			this.fraActors.TabIndex = 2;
			this.fraActors.TabStop = false;
			this.fraActors.Text = "Actors";
			// 
			// cmdActorClear
			// 
			this.cmdActorClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdActorClear.Location = new System.Drawing.Point(249, 19);
			this.cmdActorClear.Name = "cmdActorClear";
			this.cmdActorClear.Size = new System.Drawing.Size(75, 23);
			this.cmdActorClear.TabIndex = 1;
			this.cmdActorClear.Text = "Cl&ear";
			this.cmdActorClear.UseVisualStyleBackColor = true;
			// 
			// cmdActorAdd
			// 
			this.cmdActorAdd.Location = new System.Drawing.Point(6, 19);
			this.cmdActorAdd.Name = "cmdActorAdd";
			this.cmdActorAdd.Size = new System.Drawing.Size(75, 23);
			this.cmdActorAdd.TabIndex = 1;
			this.cmdActorAdd.Text = "&Add...";
			this.cmdActorAdd.UseVisualStyleBackColor = true;
			// 
			// cmdActorRemove
			// 
			this.cmdActorRemove.Location = new System.Drawing.Point(168, 19);
			this.cmdActorRemove.Name = "cmdActorRemove";
			this.cmdActorRemove.Size = new System.Drawing.Size(75, 23);
			this.cmdActorRemove.TabIndex = 1;
			this.cmdActorRemove.Text = "&Remove";
			this.cmdActorRemove.UseVisualStyleBackColor = true;
			// 
			// cmdActorModify
			// 
			this.cmdActorModify.Location = new System.Drawing.Point(87, 19);
			this.cmdActorModify.Name = "cmdActorModify";
			this.cmdActorModify.Size = new System.Drawing.Size(75, 23);
			this.cmdActorModify.TabIndex = 1;
			this.cmdActorModify.Text = "&Modify...";
			this.cmdActorModify.UseVisualStyleBackColor = true;
			// 
			// lvActors
			// 
			this.lvActors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvActors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chActorName});
			this.lvActors.FullRowSelect = true;
			this.lvActors.GridLines = true;
			this.lvActors.HideSelection = false;
			this.lvActors.Location = new System.Drawing.Point(6, 48);
			this.lvActors.Name = "lvActors";
			this.lvActors.Size = new System.Drawing.Size(318, 117);
			this.lvActors.TabIndex = 0;
			this.lvActors.UseCompatibleStateImageBehavior = false;
			this.lvActors.View = System.Windows.Forms.View.Details;
			// 
			// chActorName
			// 
			this.chActorName.Text = "Name";
			this.chActorName.Width = 304;
			// 
			// SubtitleEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.MinimumSize = new System.Drawing.Size(672, 355);
			this.Name = "SubtitleEditor";
			this.Size = new System.Drawing.Size(672, 355);
			this.fraEvents.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.fraStyles.ResumeLayout(false);
			this.fraActors.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvEvents;
        private System.Windows.Forms.ColumnHeader chEventActor;
        private System.Windows.Forms.ColumnHeader chEventStart;
        private System.Windows.Forms.ColumnHeader chEventEnd;
        private System.Windows.Forms.ColumnHeader chEventDuration;
        private System.Windows.Forms.ColumnHeader chEventText;
        private System.Windows.Forms.GroupBox fraEvents;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox fraStyles;
        private System.Windows.Forms.GroupBox fraActors;
        private System.Windows.Forms.Button cmdEventsClear;
        private System.Windows.Forms.Button cmdEventsRemove;
        private System.Windows.Forms.Button cmdEventsModify;
        private System.Windows.Forms.Button cmdEventsAdd;
        private System.Windows.Forms.Button cmdStyleClear;
        private System.Windows.Forms.Button cmdStyleAdd;
        private System.Windows.Forms.Button cmdStyleModify;
        private System.Windows.Forms.Button cmdStyleRemove;
        private System.Windows.Forms.Button cmdActorClear;
        private System.Windows.Forms.Button cmdActorAdd;
        private System.Windows.Forms.Button cmdActorRemove;
        private System.Windows.Forms.Button cmdActorModify;
        private System.Windows.Forms.ListView lvStyles;
        private System.Windows.Forms.ColumnHeader chStyleName;
        private System.Windows.Forms.ListView lvActors;
        private System.Windows.Forms.ColumnHeader chActorName;
    }
}
