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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Vertices");
			System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Materials");
			System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Bones");
			System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Inverse Kinematics");
			System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Faces");
			System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("String Table");
			System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Resources", new System.Windows.Forms.TreeNode[] {
            treeNode16});
			System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Toons");
			System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Physics");
			System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Joints");
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tv = new System.Windows.Forms.TreeView();
			this.mnuContextTreeView = new AwesomeControls.CommandBars.CBContextMenu(this.components);
			this.mnuContextTreeViewAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.vertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.materialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.boneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.faceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stringTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.rigidBodyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.jointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuContextTreeViewCut = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuContextTreeViewDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.pnlBonesGroup = new System.Windows.Forms.Panel();
			this.txtGroupName = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.pnlBonesBone = new System.Windows.Forms.Panel();
			this.comboBox4 = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.cboBoneType = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtBoneName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.pnlResourcesStringTable = new System.Windows.Forms.Panel();
			this.cboLanguage = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.pnlMaterialsMaterial = new System.Windows.Forms.Panel();
			this.fraToon = new System.Windows.Forms.GroupBox();
			this.lvMapping = new System.Windows.Forms.ListView();
			this.txtMaterialName = new System.Windows.Forms.TextBox();
			this.lblMaterialName = new System.Windows.Forms.Label();
			this.fraEdge = new System.Windows.Forms.GroupBox();
			this.txtEdgeThickness = new System.Windows.Forms.TextBox();
			this.sldEdgeThickness = new System.Windows.Forms.TrackBar();
			this.label2 = new System.Windows.Forms.Label();
			this.pnlColorEdge = new System.Windows.Forms.Panel();
			this.cmdColorEdge = new System.Windows.Forms.Button();
			this.fraTextures = new System.Windows.Forms.GroupBox();
			this.chkMaterialEnableAnimation = new System.Windows.Forms.CheckBox();
			this.cboMaterialToon = new System.Windows.Forms.ComboBox();
			this.chkMaterialEnableGlow = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.chkMaterialEnableLightSource = new System.Windows.Forms.CheckBox();
			this.lvTextures = new System.Windows.Forms.ListView();
			this.chTextureImageFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chTextureMapFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chTextureDelay = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cmdTextureClear = new System.Windows.Forms.Button();
			this.cmdTextureRemove = new System.Windows.Forms.Button();
			this.cmdTextureModify = new System.Windows.Forms.Button();
			this.cmdTextureAdd = new System.Windows.Forms.Button();
			this.fraColors = new System.Windows.Forms.GroupBox();
			this.pnlColorEmissive = new System.Windows.Forms.Panel();
			this.pnlColorSpecular = new System.Windows.Forms.Panel();
			this.pnlColorDiffuse = new System.Windows.Forms.Panel();
			this.pnlColorAmbient = new System.Windows.Forms.Panel();
			this.cmdColorEmissive = new System.Windows.Forms.Button();
			this.cmdColorSpecular = new System.Windows.Forms.Button();
			this.cmdColorDiffuse = new System.Windows.Forms.Button();
			this.cmdColorAmbient = new System.Windows.Forms.Button();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.mnuContextTreeView.SuspendLayout();
			this.pnlBonesGroup.SuspendLayout();
			this.pnlBonesBone.SuspendLayout();
			this.pnlResourcesStringTable.SuspendLayout();
			this.pnlMaterialsMaterial.SuspendLayout();
			this.fraToon.SuspendLayout();
			this.fraEdge.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sldEdgeThickness)).BeginInit();
			this.fraTextures.SuspendLayout();
			this.fraColors.SuspendLayout();
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
			this.splitContainer1.Panel2.Controls.Add(this.pnlBonesGroup);
			this.splitContainer1.Panel2.Controls.Add(this.pnlBonesBone);
			this.splitContainer1.Panel2.Controls.Add(this.pnlResourcesStringTable);
			this.splitContainer1.Size = new System.Drawing.Size(660, 393);
			this.splitContainer1.SplitterDistance = 188;
			this.splitContainer1.TabIndex = 1;
			// 
			// tv
			// 
			this.tv.ContextMenuStrip = this.mnuContextTreeView;
			this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tv.LabelEdit = true;
			this.tv.Location = new System.Drawing.Point(0, 0);
			this.tv.Name = "tv";
			treeNode11.Name = "nodeVertices";
			treeNode11.Text = "Vertices";
			treeNode12.Name = "nodeMaterials";
			treeNode12.Text = "Materials";
			treeNode13.Name = "nodeBones";
			treeNode13.Text = "Bones";
			treeNode14.Name = "nodeInverseKinematics";
			treeNode14.Text = "Inverse Kinematics";
			treeNode15.Name = "nodeFaces";
			treeNode15.Text = "Faces";
			treeNode16.Name = "nodeStringTable";
			treeNode16.Text = "String Table";
			treeNode17.Name = "nodeResources";
			treeNode17.Text = "Resources";
			treeNode18.Name = "nodeToons";
			treeNode18.Text = "Toons";
			treeNode19.Name = "nodePhysics";
			treeNode19.Text = "Physics";
			treeNode20.Name = "nodeJoints";
			treeNode20.Text = "Joints";
			this.tv.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode17,
            treeNode18,
            treeNode19,
            treeNode20});
			this.tv.Size = new System.Drawing.Size(188, 393);
			this.tv.TabIndex = 0;
			this.tv.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tv_BeforeLabelEdit);
			this.tv.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tv_AfterLabelEdit);
			this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
			// 
			// mnuContextTreeView
			// 
			this.mnuContextTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextTreeViewAdd,
            this.toolStripMenuItem2,
            this.mnuContextTreeViewCut,
            this.mnuContextTreeViewCopy,
            this.mnuContextTreeViewPaste,
            this.mnuContextTreeViewDelete});
			this.mnuContextTreeView.Name = "mnuContextTreeView";
			this.mnuContextTreeView.Size = new System.Drawing.Size(145, 120);
			// 
			// mnuContextTreeViewAdd
			// 
			this.mnuContextTreeViewAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vertexToolStripMenuItem,
            this.materialToolStripMenuItem,
            this.boneToolStripMenuItem,
            this.faceToolStripMenuItem,
            this.groupToolStripMenuItem,
            this.stringTableToolStripMenuItem,
            this.toonToolStripMenuItem,
            this.toolStripMenuItem1,
            this.rigidBodyToolStripMenuItem,
            this.jointToolStripMenuItem});
			this.mnuContextTreeViewAdd.Name = "mnuContextTreeViewAdd";
			this.mnuContextTreeViewAdd.Size = new System.Drawing.Size(144, 22);
			this.mnuContextTreeViewAdd.Text = "A&dd";
			// 
			// vertexToolStripMenuItem
			// 
			this.vertexToolStripMenuItem.Name = "vertexToolStripMenuItem";
			this.vertexToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.vertexToolStripMenuItem.Text = "&Vertex";
			// 
			// materialToolStripMenuItem
			// 
			this.materialToolStripMenuItem.Name = "materialToolStripMenuItem";
			this.materialToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.materialToolStripMenuItem.Text = "&Material";
			// 
			// boneToolStripMenuItem
			// 
			this.boneToolStripMenuItem.Name = "boneToolStripMenuItem";
			this.boneToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.boneToolStripMenuItem.Text = "&Bone";
			// 
			// faceToolStripMenuItem
			// 
			this.faceToolStripMenuItem.Name = "faceToolStripMenuItem";
			this.faceToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.faceToolStripMenuItem.Text = "&Face";
			// 
			// groupToolStripMenuItem
			// 
			this.groupToolStripMenuItem.Name = "groupToolStripMenuItem";
			this.groupToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.groupToolStripMenuItem.Text = "&Group";
			// 
			// stringTableToolStripMenuItem
			// 
			this.stringTableToolStripMenuItem.Name = "stringTableToolStripMenuItem";
			this.stringTableToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.stringTableToolStripMenuItem.Text = "&String Table";
			// 
			// toonToolStripMenuItem
			// 
			this.toonToolStripMenuItem.Name = "toonToolStripMenuItem";
			this.toonToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.toonToolStripMenuItem.Text = "&Toon";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(128, 6);
			// 
			// rigidBodyToolStripMenuItem
			// 
			this.rigidBodyToolStripMenuItem.Name = "rigidBodyToolStripMenuItem";
			this.rigidBodyToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.rigidBodyToolStripMenuItem.Text = "&Rigid Body";
			// 
			// jointToolStripMenuItem
			// 
			this.jointToolStripMenuItem.Name = "jointToolStripMenuItem";
			this.jointToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.jointToolStripMenuItem.Text = "&Joint";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(141, 6);
			// 
			// mnuContextTreeViewCut
			// 
			this.mnuContextTreeViewCut.Name = "mnuContextTreeViewCut";
			this.mnuContextTreeViewCut.ShortcutKeyDisplayString = "Ctrl+X";
			this.mnuContextTreeViewCut.Size = new System.Drawing.Size(144, 22);
			this.mnuContextTreeViewCut.Text = "Cu&t";
			// 
			// mnuContextTreeViewCopy
			// 
			this.mnuContextTreeViewCopy.Name = "mnuContextTreeViewCopy";
			this.mnuContextTreeViewCopy.ShortcutKeyDisplayString = "Ctrl+C";
			this.mnuContextTreeViewCopy.Size = new System.Drawing.Size(144, 22);
			this.mnuContextTreeViewCopy.Text = "&Copy";
			// 
			// mnuContextTreeViewPaste
			// 
			this.mnuContextTreeViewPaste.Name = "mnuContextTreeViewPaste";
			this.mnuContextTreeViewPaste.ShortcutKeyDisplayString = "Ctrl+V";
			this.mnuContextTreeViewPaste.Size = new System.Drawing.Size(144, 22);
			this.mnuContextTreeViewPaste.Text = "&Paste";
			// 
			// mnuContextTreeViewDelete
			// 
			this.mnuContextTreeViewDelete.Name = "mnuContextTreeViewDelete";
			this.mnuContextTreeViewDelete.ShortcutKeyDisplayString = "Del";
			this.mnuContextTreeViewDelete.Size = new System.Drawing.Size(144, 22);
			this.mnuContextTreeViewDelete.Text = "&Delete";
			// 
			// pnlBonesGroup
			// 
			this.pnlBonesGroup.Controls.Add(this.txtGroupName);
			this.pnlBonesGroup.Controls.Add(this.label13);
			this.pnlBonesGroup.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlBonesGroup.Location = new System.Drawing.Point(0, 0);
			this.pnlBonesGroup.Name = "pnlBonesGroup";
			this.pnlBonesGroup.Size = new System.Drawing.Size(468, 393);
			this.pnlBonesGroup.TabIndex = 2;
			this.pnlBonesGroup.Visible = false;
			// 
			// txtGroupName
			// 
			this.txtGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtGroupName.Location = new System.Drawing.Point(47, 3);
			this.txtGroupName.Name = "txtGroupName";
			this.txtGroupName.Size = new System.Drawing.Size(418, 20);
			this.txtGroupName.TabIndex = 3;
			this.txtGroupName.Validated += new System.EventHandler(this.txtGroupName_Validated);
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label13.Location = new System.Drawing.Point(3, 6);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(38, 13);
			this.label13.TabIndex = 2;
			this.label13.Text = "&Name:";
			// 
			// pnlBonesBone
			// 
			this.pnlBonesBone.Controls.Add(this.comboBox4);
			this.pnlBonesBone.Controls.Add(this.label8);
			this.pnlBonesBone.Controls.Add(this.comboBox3);
			this.pnlBonesBone.Controls.Add(this.label7);
			this.pnlBonesBone.Controls.Add(this.comboBox2);
			this.pnlBonesBone.Controls.Add(this.label6);
			this.pnlBonesBone.Controls.Add(this.cboBoneType);
			this.pnlBonesBone.Controls.Add(this.label5);
			this.pnlBonesBone.Controls.Add(this.txtBoneName);
			this.pnlBonesBone.Controls.Add(this.label4);
			this.pnlBonesBone.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlBonesBone.Location = new System.Drawing.Point(0, 0);
			this.pnlBonesBone.Name = "pnlBonesBone";
			this.pnlBonesBone.Size = new System.Drawing.Size(468, 393);
			this.pnlBonesBone.TabIndex = 1;
			this.pnlBonesBone.Visible = false;
			// 
			// comboBox4
			// 
			this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.comboBox4.FormattingEnabled = true;
			this.comboBox4.Location = new System.Drawing.Point(221, 84);
			this.comboBox4.Name = "comboBox4";
			this.comboBox4.Size = new System.Drawing.Size(121, 21);
			this.comboBox4.TabIndex = 5;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label8.Location = new System.Drawing.Point(174, 87);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(20, 13);
			this.label8.TabIndex = 4;
			this.label8.Text = "&IK:";
			// 
			// comboBox3
			// 
			this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.comboBox3.FormattingEnabled = true;
			this.comboBox3.Location = new System.Drawing.Point(221, 57);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(121, 21);
			this.comboBox3.TabIndex = 5;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label7.Location = new System.Drawing.Point(174, 60);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(33, 13);
			this.label7.TabIndex = 4;
			this.label7.Text = "&Child:";
			// 
			// comboBox2
			// 
			this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Location = new System.Drawing.Point(221, 30);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(121, 21);
			this.comboBox2.TabIndex = 5;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label6.Location = new System.Drawing.Point(174, 33);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(41, 13);
			this.label6.TabIndex = 4;
			this.label6.Text = "&Parent:";
			// 
			// cboBoneType
			// 
			this.cboBoneType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboBoneType.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cboBoneType.FormattingEnabled = true;
			this.cboBoneType.Items.AddRange(new object[] {
            "Unknown",
            "Rotate",
            "Rotate/move",
            "Inverse kinematics",
            "Blank",
            "IK-influenced rotation",
            "Influenced rotation",
            "IKConnect",
            "Hidden",
            "Twist",
            "Revolution"});
			this.cboBoneType.Location = new System.Drawing.Point(47, 29);
			this.cboBoneType.Name = "cboBoneType";
			this.cboBoneType.Size = new System.Drawing.Size(121, 21);
			this.cboBoneType.TabIndex = 5;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label5.Location = new System.Drawing.Point(3, 32);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(34, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "&Type:";
			// 
			// txtBoneName
			// 
			this.txtBoneName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoneName.Location = new System.Drawing.Point(47, 3);
			this.txtBoneName.Name = "txtBoneName";
			this.txtBoneName.Size = new System.Drawing.Size(418, 20);
			this.txtBoneName.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label4.Location = new System.Drawing.Point(3, 6);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(38, 13);
			this.label4.TabIndex = 2;
			this.label4.Text = "&Name:";
			// 
			// pnlResourcesStringTable
			// 
			this.pnlResourcesStringTable.Controls.Add(this.cboLanguage);
			this.pnlResourcesStringTable.Controls.Add(this.label1);
			this.pnlResourcesStringTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlResourcesStringTable.Location = new System.Drawing.Point(0, 0);
			this.pnlResourcesStringTable.Name = "pnlResourcesStringTable";
			this.pnlResourcesStringTable.Size = new System.Drawing.Size(468, 393);
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
			this.pnlMaterialsMaterial.Size = new System.Drawing.Size(468, 393);
			this.pnlMaterialsMaterial.TabIndex = 0;
			this.pnlMaterialsMaterial.Visible = false;
			// 
			// fraToon
			// 
			this.fraToon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.fraToon.Controls.Add(this.lvMapping);
			this.fraToon.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraToon.Location = new System.Drawing.Point(280, 30);
			this.fraToon.Name = "fraToon";
			this.fraToon.Size = new System.Drawing.Size(185, 208);
			this.fraToon.TabIndex = 5;
			this.fraToon.TabStop = false;
			this.fraToon.Text = "Mapping";
			// 
			// lvMapping
			// 
			this.lvMapping.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvMapping.Location = new System.Drawing.Point(6, 19);
			this.lvMapping.Name = "lvMapping";
			this.lvMapping.Size = new System.Drawing.Size(173, 184);
			this.lvMapping.TabIndex = 0;
			this.lvMapping.UseCompatibleStateImageBehavior = false;
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
			// fraEdge
			// 
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
			// txtEdgeThickness
			// 
			this.txtEdgeThickness.Location = new System.Drawing.Point(212, 19);
			this.txtEdgeThickness.Name = "txtEdgeThickness";
			this.txtEdgeThickness.Size = new System.Drawing.Size(51, 20);
			this.txtEdgeThickness.TabIndex = 10;
			this.txtEdgeThickness.Validating += new System.ComponentModel.CancelEventHandler(this.txtEdgeThickness_Validating);
			this.txtEdgeThickness.Validated += new System.EventHandler(this.txtEdgeThickness_Validated);
			// 
			// sldEdgeThickness
			// 
			this.sldEdgeThickness.LargeChange = 10;
			this.sldEdgeThickness.Location = new System.Drawing.Point(96, 45);
			this.sldEdgeThickness.Maximum = 100;
			this.sldEdgeThickness.Name = "sldEdgeThickness";
			this.sldEdgeThickness.Size = new System.Drawing.Size(167, 45);
			this.sldEdgeThickness.TabIndex = 9;
			this.sldEdgeThickness.TickFrequency = 10;
			this.sldEdgeThickness.TickStyle = System.Windows.Forms.TickStyle.Both;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(96, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(110, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Edge Line Thic&kness:";
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
			// fraTextures
			// 
			this.fraTextures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.fraTextures.Controls.Add(this.chkMaterialEnableAnimation);
			this.fraTextures.Controls.Add(this.cboMaterialToon);
			this.fraTextures.Controls.Add(this.chkMaterialEnableGlow);
			this.fraTextures.Controls.Add(this.label3);
			this.fraTextures.Controls.Add(this.chkMaterialEnableLightSource);
			this.fraTextures.Controls.Add(this.lvTextures);
			this.fraTextures.Controls.Add(this.cmdTextureClear);
			this.fraTextures.Controls.Add(this.cmdTextureRemove);
			this.fraTextures.Controls.Add(this.cmdTextureModify);
			this.fraTextures.Controls.Add(this.cmdTextureAdd);
			this.fraTextures.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.fraTextures.Location = new System.Drawing.Point(4, 244);
			this.fraTextures.Name = "fraTextures";
			this.fraTextures.Size = new System.Drawing.Size(459, 146);
			this.fraTextures.TabIndex = 4;
			this.fraTextures.TabStop = false;
			this.fraTextures.Text = "Textures";
			// 
			// chkMaterialEnableAnimation
			// 
			this.chkMaterialEnableAnimation.AutoSize = true;
			this.chkMaterialEnableAnimation.Location = new System.Drawing.Point(186, 21);
			this.chkMaterialEnableAnimation.Name = "chkMaterialEnableAnimation";
			this.chkMaterialEnableAnimation.Size = new System.Drawing.Size(107, 17);
			this.chkMaterialEnableAnimation.TabIndex = 0;
			this.chkMaterialEnableAnimation.Text = "Enable &animation";
			this.chkMaterialEnableAnimation.UseVisualStyleBackColor = true;
			this.chkMaterialEnableAnimation.CheckedChanged += new System.EventHandler(this.chkMaterialFlags_CheckedChanged);
			// 
			// cboMaterialToon
			// 
			this.cboMaterialToon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboMaterialToon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMaterialToon.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cboMaterialToon.FormattingEnabled = true;
			this.cboMaterialToon.Items.AddRange(new object[] {
            "(none)",
            "Toon01.bmp",
            "Toon02.bmp",
            "Toon03.bmp",
            "Toon04.bmp",
            "Toon05.bmp",
            "Toon06.bmp",
            "Toon07.bmp",
            "Toon08.bmp",
            "Toon09.bmp",
            "Toon10.bmp"});
			this.cboMaterialToon.Location = new System.Drawing.Point(348, 19);
			this.cboMaterialToon.Name = "cboMaterialToon";
			this.cboMaterialToon.Size = new System.Drawing.Size(105, 21);
			this.cboMaterialToon.TabIndex = 3;
			// 
			// chkMaterialEnableGlow
			// 
			this.chkMaterialEnableGlow.AutoSize = true;
			this.chkMaterialEnableGlow.Location = new System.Drawing.Point(96, 21);
			this.chkMaterialEnableGlow.Name = "chkMaterialEnableGlow";
			this.chkMaterialEnableGlow.Size = new System.Drawing.Size(84, 17);
			this.chkMaterialEnableGlow.TabIndex = 0;
			this.chkMaterialEnableGlow.Text = "Enable &glow";
			this.chkMaterialEnableGlow.UseVisualStyleBackColor = true;
			this.chkMaterialEnableGlow.CheckedChanged += new System.EventHandler(this.chkMaterialFlags_CheckedChanged);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(307, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Too&n:";
			// 
			// chkMaterialEnableLightSource
			// 
			this.chkMaterialEnableLightSource.AutoSize = true;
			this.chkMaterialEnableLightSource.Location = new System.Drawing.Point(6, 21);
			this.chkMaterialEnableLightSource.Name = "chkMaterialEnableLightSource";
			this.chkMaterialEnableLightSource.Size = new System.Drawing.Size(84, 17);
			this.chkMaterialEnableLightSource.TabIndex = 0;
			this.chkMaterialEnableLightSource.Text = "&Light source";
			this.chkMaterialEnableLightSource.UseVisualStyleBackColor = true;
			this.chkMaterialEnableLightSource.CheckedChanged += new System.EventHandler(this.chkMaterialFlags_CheckedChanged);
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
			this.lvTextures.Location = new System.Drawing.Point(6, 75);
			this.lvTextures.Name = "lvTextures";
			this.lvTextures.Size = new System.Drawing.Size(447, 65);
			this.lvTextures.TabIndex = 1;
			this.lvTextures.UseCompatibleStateImageBehavior = false;
			this.lvTextures.View = System.Windows.Forms.View.Details;
			this.lvTextures.ItemActivate += new System.EventHandler(this.lvTextures_ItemActivate);
			this.lvTextures.SelectedIndexChanged += new System.EventHandler(this.lvTextures_SelectedIndexChanged);
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
			// cmdTextureClear
			// 
			this.cmdTextureClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdTextureClear.Enabled = false;
			this.cmdTextureClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdTextureClear.Location = new System.Drawing.Point(378, 46);
			this.cmdTextureClear.Name = "cmdTextureClear";
			this.cmdTextureClear.Size = new System.Drawing.Size(75, 23);
			this.cmdTextureClear.TabIndex = 0;
			this.cmdTextureClear.Text = "&Clear";
			this.cmdTextureClear.UseVisualStyleBackColor = true;
			this.cmdTextureClear.Click += new System.EventHandler(this.cmdTextureClear_Click);
			// 
			// cmdTextureRemove
			// 
			this.cmdTextureRemove.Enabled = false;
			this.cmdTextureRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdTextureRemove.Location = new System.Drawing.Point(168, 46);
			this.cmdTextureRemove.Name = "cmdTextureRemove";
			this.cmdTextureRemove.Size = new System.Drawing.Size(75, 23);
			this.cmdTextureRemove.TabIndex = 0;
			this.cmdTextureRemove.Text = "&Remove";
			this.cmdTextureRemove.UseVisualStyleBackColor = true;
			this.cmdTextureRemove.Click += new System.EventHandler(this.cmdTextureRemove_Click);
			// 
			// cmdTextureModify
			// 
			this.cmdTextureModify.Enabled = false;
			this.cmdTextureModify.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdTextureModify.Location = new System.Drawing.Point(87, 46);
			this.cmdTextureModify.Name = "cmdTextureModify";
			this.cmdTextureModify.Size = new System.Drawing.Size(75, 23);
			this.cmdTextureModify.TabIndex = 0;
			this.cmdTextureModify.Text = "&Modify...";
			this.cmdTextureModify.UseVisualStyleBackColor = true;
			this.cmdTextureModify.Click += new System.EventHandler(this.cmdTextureModify_Click);
			// 
			// cmdTextureAdd
			// 
			this.cmdTextureAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdTextureAdd.Location = new System.Drawing.Point(6, 46);
			this.cmdTextureAdd.Name = "cmdTextureAdd";
			this.cmdTextureAdd.Size = new System.Drawing.Size(75, 23);
			this.cmdTextureAdd.TabIndex = 0;
			this.cmdTextureAdd.Text = "&Add...";
			this.cmdTextureAdd.UseVisualStyleBackColor = true;
			this.cmdTextureAdd.Click += new System.EventHandler(this.cmdTextureAdd_Click);
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
			// cmdColorEmissive
			// 
			this.cmdColorEmissive.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.cmdColorEmissive.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdColorEmissive.Location = new System.Drawing.Point(200, 19);
			this.cmdColorEmissive.Name = "cmdColorEmissive";
			this.cmdColorEmissive.Size = new System.Drawing.Size(55, 23);
			this.cmdColorEmissive.TabIndex = 6;
			this.cmdColorEmissive.Text = "E&missive";
			this.cmdColorEmissive.UseVisualStyleBackColor = true;
			this.cmdColorEmissive.Click += new System.EventHandler(this.cmdColor_Click);
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
			// ModelEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.MinimumSize = new System.Drawing.Size(660, 393);
			this.Name = "ModelEditor";
			this.Size = new System.Drawing.Size(660, 393);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.mnuContextTreeView.ResumeLayout(false);
			this.pnlBonesGroup.ResumeLayout(false);
			this.pnlBonesGroup.PerformLayout();
			this.pnlBonesBone.ResumeLayout(false);
			this.pnlBonesBone.PerformLayout();
			this.pnlResourcesStringTable.ResumeLayout(false);
			this.pnlResourcesStringTable.PerformLayout();
			this.pnlMaterialsMaterial.ResumeLayout(false);
			this.pnlMaterialsMaterial.PerformLayout();
			this.fraToon.ResumeLayout(false);
			this.fraEdge.ResumeLayout(false);
			this.fraEdge.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.sldEdgeThickness)).EndInit();
			this.fraTextures.ResumeLayout(false);
			this.fraTextures.PerformLayout();
			this.fraColors.ResumeLayout(false);
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
        private System.Windows.Forms.ComboBox cboMaterialToon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvMapping;
        private AwesomeControls.CommandBars.CBContextMenu mnuContextTreeView;
        private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewAdd;
        private System.Windows.Forms.ToolStripMenuItem vertexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem materialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem faceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stringTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toonToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rigidBodyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jointToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewCut;
        private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewPaste;
        private System.Windows.Forms.ToolStripMenuItem mnuContextTreeViewDelete;
        private System.Windows.Forms.Panel pnlBonesBone;
        private System.Windows.Forms.TextBox txtBoneName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboBoneType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox4;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.CheckBox chkMaterialEnableAnimation;
		private System.Windows.Forms.CheckBox chkMaterialEnableGlow;
		private System.Windows.Forms.CheckBox chkMaterialEnableLightSource;
		private System.Windows.Forms.Panel pnlBonesGroup;
		private System.Windows.Forms.TextBox txtGroupName;
		private System.Windows.Forms.Label label13;
    }
}
