namespace UniversalEditor.Plugins.Multimedia3D.UserInterface.WindowsForms.Editors
{
    partial class ModelEditor
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
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Vertices");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Materials");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Bones");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Inverse Kinematics");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Faces");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Groups");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("String Table");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Resources", new System.Windows.Forms.TreeNode[] {
            treeNode18});
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Toons");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Physics");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Joints");
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tv = new System.Windows.Forms.TreeView();
            this.pnlResourcesStringTable = new System.Windows.Forms.Panel();
            this.cboLanguage = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlMaterialsMaterial = new System.Windows.Forms.Panel();
            this.fraColors = new System.Windows.Forms.GroupBox();
            this.fraTextures = new System.Windows.Forms.GroupBox();
            this.cmdColorAmbient = new System.Windows.Forms.Button();
            this.cmdColorDiffuse = new System.Windows.Forms.Button();
            this.pnlColorAmbient = new System.Windows.Forms.Panel();
            this.pnlColorDiffuse = new System.Windows.Forms.Panel();
            this.pnlColorSpecular = new System.Windows.Forms.Panel();
            this.cmdColorSpecular = new System.Windows.Forms.Button();
            this.fraEdge = new System.Windows.Forms.GroupBox();
            this.cmdColorEmissive = new System.Windows.Forms.Button();
            this.pnlColorEmissive = new System.Windows.Forms.Panel();
            this.lblMaterialName = new System.Windows.Forms.Label();
            this.txtMaterialName = new System.Windows.Forms.TextBox();
            this.cmdColorEdge = new System.Windows.Forms.Button();
            this.pnlColorEdge = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.sldEdgeThickness = new System.Windows.Forms.TrackBar();
            this.txtEdgeThickness = new System.Windows.Forms.TextBox();
            this.fraToon = new System.Windows.Forms.GroupBox();
            this.cmdTextureAdd = new System.Windows.Forms.Button();
            this.cmdTextureModify = new System.Windows.Forms.Button();
            this.cmdTextureRemove = new System.Windows.Forms.Button();
            this.cmdTextureClear = new System.Windows.Forms.Button();
            this.lvTextures = new System.Windows.Forms.ListView();
            this.chTextureImageFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTextureMapFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTextureDelay = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlResourcesStringTable.SuspendLayout();
            this.pnlMaterialsMaterial.SuspendLayout();
            this.fraColors.SuspendLayout();
            this.fraTextures.SuspendLayout();
            this.fraEdge.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sldEdgeThickness)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tv);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlMaterialsMaterial);
            this.splitContainer1.Panel2.Controls.Add(this.pnlResourcesStringTable);
            this.splitContainer1.Size = new System.Drawing.Size(660, 381);
            this.splitContainer1.SplitterDistance = 188;
            this.splitContainer1.TabIndex = 1;
            // 
            // tv
            // 
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.Location = new System.Drawing.Point(0, 0);
            this.tv.Name = "tv";
            treeNode12.Name = "nodeVertices";
            treeNode12.Text = "Vertices";
            treeNode13.Name = "nodeMaterials";
            treeNode13.Text = "Materials";
            treeNode14.Name = "nodeBones";
            treeNode14.Text = "Bones";
            treeNode15.Name = "nodeInverseKinematics";
            treeNode15.Text = "Inverse Kinematics";
            treeNode16.Name = "nodeFaces";
            treeNode16.Text = "Faces";
            treeNode17.Name = "nodeGroups";
            treeNode17.Text = "Groups";
            treeNode18.Name = "nodeStringTable";
            treeNode18.Text = "String Table";
            treeNode19.Name = "nodeResources";
            treeNode19.Text = "Resources";
            treeNode20.Name = "nodeToons";
            treeNode20.Text = "Toons";
            treeNode21.Name = "nodePhysics";
            treeNode21.Text = "Physics";
            treeNode22.Name = "nodeJoints";
            treeNode22.Text = "Joints";
            this.tv.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16,
            treeNode17,
            treeNode19,
            treeNode20,
            treeNode21,
            treeNode22});
            this.tv.Size = new System.Drawing.Size(188, 381);
            this.tv.TabIndex = 0;
            this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
            // 
            // pnlResourcesStringTable
            // 
            this.pnlResourcesStringTable.Controls.Add(this.cboLanguage);
            this.pnlResourcesStringTable.Controls.Add(this.label1);
            this.pnlResourcesStringTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResourcesStringTable.Location = new System.Drawing.Point(0, 0);
            this.pnlResourcesStringTable.Name = "pnlResourcesStringTable";
            this.pnlResourcesStringTable.Size = new System.Drawing.Size(468, 381);
            this.pnlResourcesStringTable.TabIndex = 0;
            // 
            // cboLanguage
            // 
            this.cboLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLanguage.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Location = new System.Drawing.Point(67, 3);
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Size = new System.Drawing.Size(398, 21);
            this.cboLanguage.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Language:";
            // 
            // pnlMaterialsMaterial
            // 
            this.pnlMaterialsMaterial.Controls.Add(this.fraToon);
            this.pnlMaterialsMaterial.Controls.Add(this.txtMaterialName);
            this.pnlMaterialsMaterial.Controls.Add(this.lblMaterialName);
            this.pnlMaterialsMaterial.Controls.Add(this.fraEdge);
            this.pnlMaterialsMaterial.Controls.Add(this.fraTextures);
            this.pnlMaterialsMaterial.Controls.Add(this.fraColors);
            this.pnlMaterialsMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMaterialsMaterial.Location = new System.Drawing.Point(0, 0);
            this.pnlMaterialsMaterial.Name = "pnlMaterialsMaterial";
            this.pnlMaterialsMaterial.Size = new System.Drawing.Size(468, 381);
            this.pnlMaterialsMaterial.TabIndex = 0;
            this.pnlMaterialsMaterial.Visible = false;
            // 
            // fraColors
            // 
            this.fraColors.Controls.Add(this.pnlColorEmissive);
            this.fraColors.Controls.Add(this.pnlColorSpecular);
            this.fraColors.Controls.Add(this.pnlColorDiffuse);
            this.fraColors.Controls.Add(this.pnlColorAmbient);
            this.fraColors.Controls.Add(this.cmdColorEmissive);
            this.fraColors.Controls.Add(this.cmdColorSpecular);
            this.fraColors.Controls.Add(this.cmdColorDiffuse);
            this.fraColors.Controls.Add(this.cmdColorAmbient);
            this.fraColors.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.fraColors.Location = new System.Drawing.Point(4, 30);
            this.fraColors.Name = "fraColors";
            this.fraColors.Size = new System.Drawing.Size(270, 103);
            this.fraColors.TabIndex = 2;
            this.fraColors.TabStop = false;
            this.fraColors.Text = "Colors";
            // 
            // fraTextures
            // 
            this.fraTextures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fraTextures.Controls.Add(this.comboBox1);
            this.fraTextures.Controls.Add(this.label3);
            this.fraTextures.Controls.Add(this.lvTextures);
            this.fraTextures.Controls.Add(this.cmdTextureClear);
            this.fraTextures.Controls.Add(this.cmdTextureRemove);
            this.fraTextures.Controls.Add(this.cmdTextureModify);
            this.fraTextures.Controls.Add(this.cmdTextureAdd);
            this.fraTextures.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.fraTextures.Location = new System.Drawing.Point(4, 244);
            this.fraTextures.Name = "fraTextures";
            this.fraTextures.Size = new System.Drawing.Size(459, 134);
            this.fraTextures.TabIndex = 4;
            this.fraTextures.TabStop = false;
            this.fraTextures.Text = "Textures";
            // 
            // cmdColorAmbient
            // 
            this.cmdColorAmbient.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdColorAmbient.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdColorAmbient.Location = new System.Drawing.Point(17, 19);
            this.cmdColorAmbient.Name = "cmdColorAmbient";
            this.cmdColorAmbient.Size = new System.Drawing.Size(55, 23);
            this.cmdColorAmbient.TabIndex = 0;
            this.cmdColorAmbient.Text = "&Ambient";
            this.cmdColorAmbient.UseVisualStyleBackColor = true;
            this.cmdColorAmbient.Click += new System.EventHandler(this.cmdColor_Click);
            // 
            // cmdColorDiffuse
            // 
            this.cmdColorDiffuse.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdColorDiffuse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdColorDiffuse.Location = new System.Drawing.Point(78, 19);
            this.cmdColorDiffuse.Name = "cmdColorDiffuse";
            this.cmdColorDiffuse.Size = new System.Drawing.Size(55, 23);
            this.cmdColorDiffuse.TabIndex = 2;
            this.cmdColorDiffuse.Text = "&Diffuse";
            this.cmdColorDiffuse.UseVisualStyleBackColor = true;
            this.cmdColorDiffuse.Click += new System.EventHandler(this.cmdColor_Click);
            // 
            // pnlColorAmbient
            // 
            this.pnlColorAmbient.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlColorAmbient.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlColorAmbient.Location = new System.Drawing.Point(17, 48);
            this.pnlColorAmbient.Name = "pnlColorAmbient";
            this.pnlColorAmbient.Size = new System.Drawing.Size(55, 41);
            this.pnlColorAmbient.TabIndex = 1;
            this.pnlColorAmbient.Click += new System.EventHandler(this.cmdColor_Click);
            // 
            // pnlColorDiffuse
            // 
            this.pnlColorDiffuse.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlColorDiffuse.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlColorDiffuse.Location = new System.Drawing.Point(78, 48);
            this.pnlColorDiffuse.Name = "pnlColorDiffuse";
            this.pnlColorDiffuse.Size = new System.Drawing.Size(55, 41);
            this.pnlColorDiffuse.TabIndex = 3;
            this.pnlColorDiffuse.Click += new System.EventHandler(this.cmdColor_Click);
            // 
            // pnlColorSpecular
            // 
            this.pnlColorSpecular.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlColorSpecular.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlColorSpecular.Location = new System.Drawing.Point(139, 48);
            this.pnlColorSpecular.Name = "pnlColorSpecular";
            this.pnlColorSpecular.Size = new System.Drawing.Size(55, 41);
            this.pnlColorSpecular.TabIndex = 5;
            this.pnlColorSpecular.Click += new System.EventHandler(this.cmdColor_Click);
            // 
            // cmdColorSpecular
            // 
            this.cmdColorSpecular.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdColorSpecular.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdColorSpecular.Location = new System.Drawing.Point(139, 19);
            this.cmdColorSpecular.Name = "cmdColorSpecular";
            this.cmdColorSpecular.Size = new System.Drawing.Size(55, 23);
            this.cmdColorSpecular.TabIndex = 4;
            this.cmdColorSpecular.Text = "&Specular";
            this.cmdColorSpecular.UseVisualStyleBackColor = true;
            this.cmdColorSpecular.Click += new System.EventHandler(this.cmdColor_Click);
            // 
            // fraEdge
            // 
            this.fraEdge.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.fraEdge.Controls.Add(this.txtEdgeThickness);
            this.fraEdge.Controls.Add(this.sldEdgeThickness);
            this.fraEdge.Controls.Add(this.label2);
            this.fraEdge.Controls.Add(this.pnlColorEdge);
            this.fraEdge.Controls.Add(this.cmdColorEdge);
            this.fraEdge.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.fraEdge.Location = new System.Drawing.Point(5, 139);
            this.fraEdge.Name = "fraEdge";
            this.fraEdge.Size = new System.Drawing.Size(269, 99);
            this.fraEdge.TabIndex = 3;
            this.fraEdge.TabStop = false;
            this.fraEdge.Tag = "";
            this.fraEdge.Text = "Edge";
            // 
            // cmdColorEmissive
            // 
            this.cmdColorEmissive.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdColorEmissive.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdColorEmissive.Location = new System.Drawing.Point(200, 19);
            this.cmdColorEmissive.Name = "cmdColorEmissive";
            this.cmdColorEmissive.Size = new System.Drawing.Size(55, 23);
            this.cmdColorEmissive.TabIndex = 6;
            this.cmdColorEmissive.Text = "&Emissive";
            this.cmdColorEmissive.UseVisualStyleBackColor = true;
            this.cmdColorEmissive.Click += new System.EventHandler(this.cmdColor_Click);
            // 
            // pnlColorEmissive
            // 
            this.pnlColorEmissive.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlColorEmissive.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlColorEmissive.Location = new System.Drawing.Point(200, 48);
            this.pnlColorEmissive.Name = "pnlColorEmissive";
            this.pnlColorEmissive.Size = new System.Drawing.Size(55, 41);
            this.pnlColorEmissive.TabIndex = 7;
            this.pnlColorEmissive.Click += new System.EventHandler(this.cmdColor_Click);
            // 
            // lblMaterialName
            // 
            this.lblMaterialName.AutoSize = true;
            this.lblMaterialName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblMaterialName.Location = new System.Drawing.Point(3, 6);
            this.lblMaterialName.Name = "lblMaterialName";
            this.lblMaterialName.Size = new System.Drawing.Size(38, 13);
            this.lblMaterialName.TabIndex = 0;
            this.lblMaterialName.Text = "&Name:";
            // 
            // txtMaterialName
            // 
            this.txtMaterialName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaterialName.Location = new System.Drawing.Point(47, 3);
            this.txtMaterialName.Name = "txtMaterialName";
            this.txtMaterialName.Size = new System.Drawing.Size(418, 20);
            this.txtMaterialName.TabIndex = 1;
            // 
            // cmdColorEdge
            // 
            this.cmdColorEdge.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdColorEdge.Location = new System.Drawing.Point(15, 19);
            this.cmdColorEdge.Name = "cmdColorEdge";
            this.cmdColorEdge.Size = new System.Drawing.Size(75, 23);
            this.cmdColorEdge.TabIndex = 6;
            this.cmdColorEdge.Text = "E&dge Color";
            this.cmdColorEdge.UseVisualStyleBackColor = true;
            this.cmdColorEdge.Click += new System.EventHandler(this.cmdColor_Click);
            // 
            // pnlColorEdge
            // 
            this.pnlColorEdge.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlColorEdge.Location = new System.Drawing.Point(15, 48);
            this.pnlColorEdge.Name = "pnlColorEdge";
            this.pnlColorEdge.Size = new System.Drawing.Size(75, 41);
            this.pnlColorEdge.TabIndex = 7;
            this.pnlColorEdge.Click += new System.EventHandler(this.cmdColor_Click);
            this.pnlColorEdge.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlColorEdge_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Edge Line &Thickness:";
            // 
            // sldEdgeThickness
            // 
            this.sldEdgeThickness.LargeChange = 10;
            this.sldEdgeThickness.Location = new System.Drawing.Point(96, 45);
            this.sldEdgeThickness.Maximum = 100;
            this.sldEdgeThickness.Name = "sldEdgeThickness";
            this.sldEdgeThickness.Size = new System.Drawing.Size(167, 42);
            this.sldEdgeThickness.TabIndex = 9;
            this.sldEdgeThickness.TickFrequency = 10;
            this.sldEdgeThickness.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // txtEdgeThickness
            // 
            this.txtEdgeThickness.Location = new System.Drawing.Point(212, 19);
            this.txtEdgeThickness.Name = "txtEdgeThickness";
            this.txtEdgeThickness.Size = new System.Drawing.Size(51, 20);
            this.txtEdgeThickness.TabIndex = 10;
            this.txtEdgeThickness.Validating += new System.ComponentModel.CancelEventHandler(this.txtEdgeThickness_Validating);
            this.txtEdgeThickness.Validated += new System.EventHandler(this.txtEdgeThickness_Validated);
            // 
            // fraToon
            // 
            this.fraToon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fraToon.Location = new System.Drawing.Point(280, 30);
            this.fraToon.Name = "fraToon";
            this.fraToon.Size = new System.Drawing.Size(183, 209);
            this.fraToon.TabIndex = 5;
            this.fraToon.TabStop = false;
            this.fraToon.Text = "Mapping";
            // 
            // cmdTextureAdd
            // 
            this.cmdTextureAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdTextureAdd.Location = new System.Drawing.Point(135, 19);
            this.cmdTextureAdd.Name = "cmdTextureAdd";
            this.cmdTextureAdd.Size = new System.Drawing.Size(75, 23);
            this.cmdTextureAdd.TabIndex = 0;
            this.cmdTextureAdd.Text = "&Add...";
            this.cmdTextureAdd.UseVisualStyleBackColor = true;
            // 
            // cmdTextureModify
            // 
            this.cmdTextureModify.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdTextureModify.Location = new System.Drawing.Point(216, 19);
            this.cmdTextureModify.Name = "cmdTextureModify";
            this.cmdTextureModify.Size = new System.Drawing.Size(75, 23);
            this.cmdTextureModify.TabIndex = 0;
            this.cmdTextureModify.Text = "&Modify...";
            this.cmdTextureModify.UseVisualStyleBackColor = true;
            // 
            // cmdTextureRemove
            // 
            this.cmdTextureRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdTextureRemove.Location = new System.Drawing.Point(297, 19);
            this.cmdTextureRemove.Name = "cmdTextureRemove";
            this.cmdTextureRemove.Size = new System.Drawing.Size(75, 23);
            this.cmdTextureRemove.TabIndex = 0;
            this.cmdTextureRemove.Text = "&Remove";
            this.cmdTextureRemove.UseVisualStyleBackColor = true;
            // 
            // cmdTextureClear
            // 
            this.cmdTextureClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTextureClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdTextureClear.Location = new System.Drawing.Point(378, 19);
            this.cmdTextureClear.Name = "cmdTextureClear";
            this.cmdTextureClear.Size = new System.Drawing.Size(75, 23);
            this.cmdTextureClear.TabIndex = 0;
            this.cmdTextureClear.Text = "&Clear";
            this.cmdTextureClear.UseVisualStyleBackColor = true;
            // 
            // lvTextures
            // 
            this.lvTextures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTextures.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTextureImageFileName,
            this.chTextureMapFileName,
            this.chTextureDelay});
            this.lvTextures.FullRowSelect = true;
            this.lvTextures.GridLines = true;
            this.lvTextures.HideSelection = false;
            this.lvTextures.Location = new System.Drawing.Point(6, 48);
            this.lvTextures.Name = "lvTextures";
            this.lvTextures.Size = new System.Drawing.Size(447, 80);
            this.lvTextures.TabIndex = 1;
            this.lvTextures.UseCompatibleStateImageBehavior = false;
            this.lvTextures.View = System.Windows.Forms.View.Details;
            // 
            // chTextureImageFileName
            // 
            this.chTextureImageFileName.Text = "Image filename";
            this.chTextureImageFileName.Width = 192;
            // 
            // chTextureMapFileName
            // 
            this.chTextureMapFileName.Text = "Map filename";
            this.chTextureMapFileName.Width = 170;
            // 
            // chTextureDelay
            // 
            this.chTextureDelay.Text = "Delay (ms)";
            this.chTextureDelay.Width = 74;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "&Toon:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(63, 21);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(66, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // ModelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(660, 381);
            this.Name = "ModelEditor";
            this.Size = new System.Drawing.Size(660, 381);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.pnlResourcesStringTable.ResumeLayout(false);
            this.pnlResourcesStringTable.PerformLayout();
            this.pnlMaterialsMaterial.ResumeLayout(false);
            this.pnlMaterialsMaterial.PerformLayout();
            this.fraColors.ResumeLayout(false);
            this.fraTextures.ResumeLayout(false);
            this.fraTextures.PerformLayout();
            this.fraEdge.ResumeLayout(false);
            this.fraEdge.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sldEdgeThickness)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.Panel pnlResourcesStringTable;
        private System.Windows.Forms.ComboBox cboLanguage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlMaterialsMaterial;
        private System.Windows.Forms.GroupBox fraTextures;
        private System.Windows.Forms.GroupBox fraColors;
        private System.Windows.Forms.Panel pnlColorSpecular;
        private System.Windows.Forms.Panel pnlColorDiffuse;
        private System.Windows.Forms.Panel pnlColorAmbient;
        private System.Windows.Forms.Button cmdColorSpecular;
        private System.Windows.Forms.Button cmdColorDiffuse;
        private System.Windows.Forms.Button cmdColorAmbient;
        private System.Windows.Forms.GroupBox fraEdge;
        private System.Windows.Forms.Panel pnlColorEmissive;
        private System.Windows.Forms.Button cmdColorEmissive;
        private System.Windows.Forms.TextBox txtMaterialName;
        private System.Windows.Forms.Label lblMaterialName;
        private System.Windows.Forms.Panel pnlColorEdge;
        private System.Windows.Forms.Button cmdColorEdge;
        private System.Windows.Forms.TrackBar sldEdgeThickness;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEdgeThickness;
        private System.Windows.Forms.GroupBox fraToon;
        private System.Windows.Forms.ListView lvTextures;
        private System.Windows.Forms.Button cmdTextureClear;
        private System.Windows.Forms.Button cmdTextureRemove;
        private System.Windows.Forms.Button cmdTextureModify;
        private System.Windows.Forms.Button cmdTextureAdd;
        private System.Windows.Forms.ColumnHeader chTextureImageFileName;
        private System.Windows.Forms.ColumnHeader chTextureMapFileName;
        private System.Windows.Forms.ColumnHeader chTextureDelay;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
    }
}
