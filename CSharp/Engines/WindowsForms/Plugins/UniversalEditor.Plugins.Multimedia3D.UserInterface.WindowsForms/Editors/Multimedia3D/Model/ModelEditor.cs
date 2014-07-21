using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using UniversalEditor;
using UniversalEditor.UserInterface.WindowsForms;

using UniversalEditor.ObjectModels.Multimedia3D.Model;
using UniversalEditor.Plugins.Multimedia3D.UserInterface.WindowsForms.Dialogs;

using AwesomeControls.PropertyGrid;

namespace UniversalEditor.Plugins.Multimedia3D.UserInterface.WindowsForms.Editors
{
	public partial class ModelEditor : Editor
	{
		public ModelEditor()
		{
			InitializeComponent();

			base.SupportedObjectModels.Add(typeof(ModelObjectModel));
			
			// cboMaterialToon.SelectedIndex = 0;

			PropertyDataType dtTorus = new PropertyDataType("Torus");

			PropertyGroup pgTorus_AU_IB = new PropertyGroup("Torus_AU_IB", dtTorus);

			PropertyDataType dtVector3 = new PropertyDataType("Vector3");
			dtVector3.Properties.Add(new Property("X"));
			dtVector3.Properties.Add(new Property("Y"));
			dtVector3.Properties.Add(new Property("Z"));
			dtVector3.PropertyValueRendering += delegate(object sender, PropertyValueRenderingEventArgs e)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append("{ ");
				sb.Append(e.Property.Properties["X"].Value.ToString());
				sb.Append(", ");
				sb.Append(e.Property.Properties["Y"].Value.ToString());
				sb.Append(", ");
				sb.Append(e.Property.Properties["Z"].Value.ToString());
				sb.Append(" }");
				e.DisplayString = sb.ToString();
			};
			dtVector3.PropertyValueParsing += delegate(object sender, PropertyValueParsingEventArgs e)
			{
				string w = e.DisplayString;
				if (!w.StartsWith("{") && !w.EndsWith("}"))
				{
					e.Cancel = true;
					return;
				}
				w = w.Substring(1, w.Length - 2).Trim();
				string[] x = w.Split(",");
				if (x.Length != 3)
				{
				}
			};

			pgTorus_AU_IB.Properties.Add(new Property("Translate"));
			pgTorus_AU_IB.Properties[pgTorus_AU_IB.Properties.Count - 1].DataType = dtVector3;
			pgTorus_AU_IB.Properties[pgTorus_AU_IB.Properties.Count - 1].Properties["X"].Value = -3.713;
			pgTorus_AU_IB.Properties[pgTorus_AU_IB.Properties.Count - 1].Properties["Y"].Value = 0.000;
			pgTorus_AU_IB.Properties[pgTorus_AU_IB.Properties.Count - 1].Properties["Z"].Value = 3.984;

			base.PropertyGroups.Add(pgTorus_AU_IB);
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);
			Refresh();
		}
		public override void Refresh()
		{
			base.Refresh();

			TreeNode tnModel = tv.Nodes["nodeModel"];
			tnModel.Nodes["nodeBones"].Nodes.Clear();
			tnModel.Nodes["nodeMaterials"].Nodes.Clear();

			ModelObjectModel model = (ObjectModel as ModelObjectModel);
			if (model == null)
			{
				return;
			}

			foreach (ModelBoneGroup group in model.BoneGroups)
			{
				TreeNode tnGroup = new TreeNode();
				tnGroup.Text = group.Name;
				tnGroup.Tag = group;
				foreach (ModelBone bone in group.Bones)
				{
					TreeNode tnBon = new TreeNode();
					tnBon.Text = bone.Name;
					tnBon.Tag = bone;
					tnGroup.Nodes.Add(tnBon);
				}
				tnModel.Nodes["nodeBones"].Nodes.Add(tnGroup);
			}

			foreach (ModelMaterial mat in model.Materials)
			{
				TreeNode tnMat = new TreeNode();
				tnMat.Text = mat.Name;
				tnMat.Tag = mat;
				tnModel.Nodes["nodeMaterials"].Nodes.Add(tnMat);
			}
		}

		private void cmdColor_Click(object sender, EventArgs e)
		{
			if (tv.SelectedNode == null) return;
			ModelMaterial mat = (tv.SelectedNode.Tag as ModelMaterial);
			if (mat == null) return;

			ColorDialog dlg = new ColorDialog();
			if (sender == cmdColorAmbient || sender == pnlColorAmbient)
			{
				dlg.Color = mat.AmbientColor.ToGdiColor();
			}
			else if (sender == cmdColorDiffuse || sender == pnlColorDiffuse)
			{
				dlg.Color = mat.DiffuseColor.ToGdiColor();
			}
			else if (sender == cmdColorSpecular || sender == pnlColorSpecular)
			{
				dlg.Color = mat.SpecularColor.ToGdiColor();
			}
			else if (sender == cmdColorEmissive || sender == pnlColorEmissive)
			{
				dlg.Color = mat.EmissiveColor.ToGdiColor();
			}
			else if (sender == cmdColorEdge || sender == pnlColorEdge)
			{
				dlg.Color = mat.EdgeColor.ToGdiColor();
			}
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				if (sender == cmdColorAmbient || sender == pnlColorAmbient)
				{
					pnlColorAmbient.BackColor = dlg.Color;
					mat.AmbientColor = dlg.Color.ToUniversalColor();
				}
				else if (sender == cmdColorDiffuse || sender == pnlColorDiffuse)
				{
					pnlColorDiffuse.BackColor = dlg.Color;
					mat.DiffuseColor = dlg.Color.ToUniversalColor();
				}
				else if (sender == cmdColorSpecular || sender == pnlColorSpecular)
				{
					pnlColorSpecular.BackColor = dlg.Color;
					mat.SpecularColor = dlg.Color.ToUniversalColor();
				}
				else if (sender == cmdColorEmissive || sender == pnlColorEmissive)
				{
					pnlColorEmissive.BackColor = dlg.Color;
					mat.EmissiveColor = dlg.Color.ToUniversalColor();
				}
				else if (sender == cmdColorEdge || sender == pnlColorEdge)
				{
					pnlColorEdge.BackColor = dlg.Color;
					mat.EdgeColor = dlg.Color.ToUniversalColor();
				}
			}
		}

		private void SwitchPanel(Panel panel)
		{
			foreach (Control ctl in splitContainer1.Panel2.Controls)
			{
				if (ctl == panel)
				{
					ctl.Enabled = true;
					ctl.Visible = true;
				}
				else
				{
					ctl.Visible = false;
					ctl.Enabled = false;
				}
			}
		}

		private void tv_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (tv.SelectedNode != null)
			{
				if (tv.SelectedNode.Tag is ModelMaterial)
				{
					ModelMaterial mat = (tv.SelectedNode.Tag as ModelMaterial);
					txtMaterialName.Text = mat.Name;

					pnlColorAmbient.BackColor = mat.AmbientColor.ToGdiColor();
					pnlColorDiffuse.BackColor = mat.DiffuseColor.ToGdiColor();
					pnlColorSpecular.BackColor = mat.SpecularColor.ToGdiColor();
					pnlColorEmissive.BackColor = mat.EmissiveColor.ToGdiColor();
					pnlColorEdge.BackColor = mat.EdgeColor.ToGdiColor();

					chkMaterialEnableAnimation.Checked = mat.EnableAnimation;
					chkMaterialEnableLightSource.Checked = mat.AlwaysLight;
					chkMaterialEnableGlow.Checked = mat.EnableGlow;

					lvTextures.Items.Clear();
					foreach (ModelTexture tex in mat.Textures)
					{
						ListViewItem lvi = new ListViewItem();
						lvi.Tag = tex;
						lvi.Text = tex.TextureFileName;
						lvi.SubItems.Add(tex.MapFileName);
						lvi.SubItems.Add(tex.Duration.ToString() + " ms");
						lvTextures.Items.Add(lvi);
					}
					cmdTextureClear.Enabled = (lvTextures.Items.Count > 0);

					sldEdgeThickness.Value = (int)(mat.EdgeSize * 100);
					txtEdgeThickness.Text = mat.EdgeSize.ToString();
					SwitchPanel(pnlMaterialsMaterial);
				}
				else if (tv.SelectedNode.Tag is ModelBone)
				{
					ModelBone bon = (tv.SelectedNode.Tag as ModelBone);
					txtBoneName.Text = bon.Name;
					cboBoneType.SelectedIndex = (int)(bon.BoneType + 1);

					SwitchPanel(pnlBonesBone);
				}
				else if (tv.SelectedNode.Tag is ModelBoneGroup)
				{
					ModelBoneGroup grp = (tv.SelectedNode.Tag as ModelBoneGroup);
					txtGroupName.Text = grp.Name;

					SwitchPanel(pnlBonesGroup);
				}
				else if (tv.SelectedNode.Name == "nodeModel")
				{
					SwitchPanel(pnlModelEditor);
				}
				else
				{
					SwitchPanel(null);
				}
			}
			else
			{
				SwitchPanel(null);
			}
		}

		private void pnlColorEdge_Paint(object sender, PaintEventArgs e)
		{

		}

		private void txtEdgeThickness_Validating(object sender, CancelEventArgs e)
		{
			float dummy = 0.0f;
			if (!Single.TryParse(txtEdgeThickness.Text, out dummy))
			{
				MessageBox.Show("Please enter a single-precision floating-point value between 0.0 and 1.0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				e.Cancel = true;
				return;
			}
			else if (dummy > 1.0f)
			{
				MessageBox.Show("Please enter a single-precision floating-point value between 0.0 and 1.0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				e.Cancel = true;
				return;
			}
		}

		private void txtEdgeThickness_Validated(object sender, EventArgs e)
		{
			sldEdgeThickness.Value = (int)(Single.Parse(txtEdgeThickness.Text) * 100);
			txtEdgeThickness.Text = (sldEdgeThickness.Value / sldEdgeThickness.Maximum).ToString();
		}

		private void chkMaterialFlags_CheckedChanged(object sender, EventArgs e)
		{
			if (tv.SelectedNode == null) return;

			ModelMaterial mat = (tv.SelectedNode.Tag as ModelMaterial);
			if (mat == null) return;

			mat.EnableAnimation = chkMaterialEnableAnimation.Checked;
			mat.EnableGlow = chkMaterialEnableGlow.Checked;
			mat.AlwaysLight = chkMaterialEnableLightSource.Checked;
		}

		private void txtGroupName_Validated(object sender, EventArgs e)
		{
			if (tv.SelectedNode == null) return;
			
			ModelBoneGroup grp = (tv.SelectedNode.Tag as ModelBoneGroup);
			if (grp == null) return;

			BeginEdit();
			grp.Name = txtGroupName.Text;
			tv.SelectedNode.Text = grp.Name;
			EndEdit();
		}

		private void tv_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			if (e.Node != null)
			{
				if (!(e.Node.Tag is ModelBone || e.Node.Tag is ModelBoneGroup || e.Node.Tag is ModelMaterial))
				{
					e.CancelEdit = true;
					return;
				}
			}
		}
		private void tv_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			if (e.Node != null)
			{
				if (e.Node.Tag == null) return;
				if (e.Label == null) return;

				BeginEdit();
				if (e.Node.Tag is ModelBone)
				{
					ModelBone item = (e.Node.Tag as ModelBone);
					item.Name = e.Label;
					txtBoneName.Text = item.Name;
				}
				else if (e.Node.Tag is ModelBoneGroup)
				{
					ModelBoneGroup item = (e.Node.Tag as ModelBoneGroup);
					item.Name = e.Label;
					txtGroupName.Text = item.Name;
				}
				else if (e.Node.Tag is ModelMaterial)
				{
					ModelMaterial item = (e.Node.Tag as ModelMaterial);
					item.Name = e.Label;
					txtMaterialName.Text = item.Name;
				}
				EndEdit();
			}
		}

		private void cmdTextureAdd_Click(object sender, EventArgs e)
		{
			ModelObjectModel model = (base.ObjectModel as ModelObjectModel);
			if (model == null) return;

			if (tv.SelectedNode == null) return;
			ModelMaterial mat = (tv.SelectedNode.Tag as ModelMaterial);
			if (mat == null) return;

			TexturePropertiesDialog dlg = new TexturePropertiesDialog();
			dlg.ParentModel = model;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				ModelTexture tex = new ModelTexture();
				tex.Duration = (int)dlg.txtDuration.Value;
				tex.TextureFileName = dlg.txtTextureFileName.Text;
				tex.MapFileName = dlg.txtMapFileName.Text;
				tex.Flags = ModelTextureFlags.None;

				if (dlg.chkFlagAddMap.Checked)
				{
					tex.Flags |= ModelTextureFlags.AddMap;
				}
				if (dlg.chkFlagMap.Checked)
				{
					tex.Flags |= ModelTextureFlags.Map;
				}
				if (dlg.chkFlagTexture.Checked)
				{
					tex.Flags |= ModelTextureFlags.Texture;
				}

				mat.Textures.Add(tex);
				
				ListViewItem lvi = new ListViewItem();
				lvi.Tag = tex;
				lvi.Text = tex.TextureFileName;
				lvi.SubItems.Add(tex.MapFileName);
				lvi.SubItems.Add(tex.Duration.ToString() + " ms");
				lvTextures.Items.Add(lvi);
			}
		}

		private void cmdTextureModify_Click(object sender, EventArgs e)
		{
			ModelObjectModel model = (base.ObjectModel as ModelObjectModel);
			if (model == null) return;

			if (lvTextures.SelectedItems.Count != 1) return;
			ListViewItem lvi = lvTextures.SelectedItems[0];
			ModelTexture tex = (lvi.Tag as ModelTexture);
			if (tex == null) return;

			TexturePropertiesDialog dlg = new TexturePropertiesDialog();
			dlg.ParentModel = model;


			dlg.txtDuration.Value = tex.Duration;
			dlg.txtMapFileName.Text = tex.MapFileName;
			dlg.txtTextureFileName.Text = tex.TextureFileName;

			dlg.chkFlagAddMap.Checked = ((tex.Flags & ModelTextureFlags.AddMap) == ModelTextureFlags.AddMap);
			dlg.chkFlagMap.Checked = ((tex.Flags & ModelTextureFlags.Map) == ModelTextureFlags.Map);
			dlg.chkFlagTexture.Checked = ((tex.Flags & ModelTextureFlags.Texture) == ModelTextureFlags.Texture);

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				tex.Duration = (int)dlg.txtDuration.Value;
				tex.MapFileName = dlg.txtMapFileName.Text;
				tex.TextureFileName = dlg.txtTextureFileName.Text;

				tex.Flags = ModelTextureFlags.None;
				if (dlg.chkFlagAddMap.Checked) tex.Flags |= ModelTextureFlags.AddMap;
				if (dlg.chkFlagMap.Checked) tex.Flags |= ModelTextureFlags.Map;
				if (dlg.chkFlagTexture.Checked) tex.Flags |= ModelTextureFlags.Texture;

				lvi.Text = tex.TextureFileName;
				lvi.SubItems[1].Text = tex.MapFileName;
				lvi.SubItems[2].Text = (tex.Duration.ToString() + " ms");
			}
		}

		private void cmdTextureClear_Click(object sender, EventArgs e)
		{
			if (tv.SelectedNode == null) return;
			ModelMaterial mat = (tv.SelectedNode.Tag as ModelMaterial);
			if (mat == null) return;

			if (MessageBox.Show("Are you sure you want to remove all textures?", "Remove All Textures", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
			mat.Textures.Clear();
			lvTextures.Items.Clear();
		}

		private void lvTextures_SelectedIndexChanged(object sender, EventArgs e)
		{
			cmdTextureModify.Enabled = (lvTextures.SelectedItems.Count == 1);
			cmdTextureRemove.Enabled = (lvTextures.SelectedItems.Count > 0);
		}

		private void cmdTextureRemove_Click(object sender, EventArgs e)
		{
			if (tv.SelectedNode == null) return;
			ModelMaterial mat = (tv.SelectedNode.Tag as ModelMaterial);
			if (mat == null) return;

			string sz = "texture", sz1 = "Texture";
			if (lvTextures.SelectedItems.Count > 1)
			{
				sz = "textures";
				sz1 = "Textures";
			}
			if (MessageBox.Show("Are you sure you want to remove the selected " + sz + "?", "Remove " + sz1, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

			while (lvTextures.SelectedItems.Count > 0)
			{
				ModelTexture tex = (lvTextures.SelectedItems[0].Tag as ModelTexture);
				mat.Textures.Remove(tex);

				lvTextures.SelectedItems[0].Remove();
			}
		}

		private void lvTextures_ItemActivate(object sender, EventArgs e)
		{
			if (lvTextures.SelectedItems.Count == 1)
			{
				cmdTextureModify_Click(sender, e);
			}
		}
	}
}
