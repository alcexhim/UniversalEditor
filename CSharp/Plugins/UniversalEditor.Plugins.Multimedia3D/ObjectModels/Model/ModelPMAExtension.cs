using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	public class ModelPMAExtension
	{
		public class TextureFlippingBlock
		{
			public class TextureFlippingBlockCollection : Collection<ModelPMAExtension.TextureFlippingBlock>
			{
			}
			private ModelMaterial mvarMaterial = null;
			private ModelPMAExtension.TextureFlippingFrame.TextureFlippingFrameCollection mvarFrames = new ModelPMAExtension.TextureFlippingFrame.TextureFlippingFrameCollection();
			public ModelMaterial Material
			{
				get
				{
					return this.mvarMaterial;
				}
				set
				{
					this.mvarMaterial = value;
				}
			}
			public ModelPMAExtension.TextureFlippingFrame.TextureFlippingFrameCollection Frames
			{
				get
				{
					return this.mvarFrames;
				}
			}
		}
		public class TextureFlippingFrame
		{
			public class TextureFlippingFrameCollection : Collection<ModelPMAExtension.TextureFlippingFrame>
			{
			}
			private ulong mvarTimestamp = 0uL;
			private string mvarFileName = string.Empty;
			public ulong Timestamp
			{
				get
				{
					return this.mvarTimestamp;
				}
				set
				{
					this.mvarTimestamp = value;
				}
			}
			public string FileName
			{
				get
				{
					return this.mvarFileName;
				}
				set
				{
					this.mvarFileName = value;
				}
			}
		}
		public class TextureFlippingInformation
		{
			private bool mvarEnabled = false;
			private ModelPMAExtension.TextureFlippingBlock.TextureFlippingBlockCollection mvarBlocks = new ModelPMAExtension.TextureFlippingBlock.TextureFlippingBlockCollection();
			public bool Enabled
			{
				get
				{
					return this.mvarEnabled;
				}
				set
				{
					this.mvarEnabled = value;
				}
			}
			public ModelPMAExtension.TextureFlippingBlock.TextureFlippingBlockCollection Blocks
			{
				get
				{
					return this.mvarBlocks;
				}
			}
		}
		private bool mvarEnabled = false;
		private Version mvarVersion = new Version(1, 0, 0, 0);
		private ModelPMAExtension.TextureFlippingInformation mvarTextureFlipping = new ModelPMAExtension.TextureFlippingInformation();
		public bool Enabled
		{
			get
			{
				return this.mvarEnabled;
			}
			set
			{
				this.mvarEnabled = value;
			}
		}
		public Version Version
		{
			get
			{
				return this.mvarVersion;
			}
			set
			{
				this.mvarVersion = value;
			}
		}
		public ModelPMAExtension.TextureFlippingInformation TextureFlipping
		{
			get
			{
				return this.mvarTextureFlipping;
			}
		}
	}
}
