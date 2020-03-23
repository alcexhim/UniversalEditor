using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework.Drawing;

namespace MBS.Framework.UserInterface.Controls
{
	public enum ButtonBorderStyle
	{
		None,
		Half,
		Normal
	}
	namespace Native
	{
		public interface IButtonControlImplementation
		{
			void SetImagePosition(RelativePosition value);
			RelativePosition GetImagePosition();
		}
	}
	public class Button : SystemControl
	{
		public class ButtonCollection
			: System.Collections.ObjectModel.Collection<Button>
		{
			private Container _parent = null;
			public ButtonCollection(Container parent)
			{
				_parent = parent;
			}

			protected override void ClearItems()
			{
				for (int i = 0; i < Count; i++)
					Items[i].Parent = null;
				base.ClearItems();
			}
			protected override void InsertItem(int index, Button item)
			{
				base.InsertItem(index, item);
				item.Parent = _parent;
			}
			protected override void RemoveItem(int index)
			{
				this[index].Parent = null;
				base.RemoveItem(index);
			}
		}

		public Button()
		{
		}
		public Button(string text)
			: this(text, DialogResult.None)
		{
		}
		public Button(string text, EventHandler onClick)
			: this(text, DialogResult.None)
		{
			if (onClick != null)
				Click += onClick;
		}
		public Button(string text, DialogResult responseValue = DialogResult.None)
			: this(text, (int)responseValue)
		{
		}
		public Button(string text, int responseValue = (int)DialogResult.None)
		{
			this.Text = text;
			mvarResponseValue = responseValue;
		}
		public Button(StockType type, DialogResult responseValue)
			: this(type, (int)responseValue)
		{
		}
		public Button(StockType type, int responseValue = (int)DialogResult.None)
		{
			StockType = type;
			mvarResponseValue = responseValue;
		}

		public MBS.Framework.UserInterface.Drawing.Image Image { get; set; } = null;

		private bool mvarAlwaysShowImage = false;
		public bool AlwaysShowImage {  get { return mvarAlwaysShowImage;  } set { mvarAlwaysShowImage = value; } }

		public Dimension2D ImageSize { get; set; } = Dimension2D.Empty;

		private RelativePosition mvarImagePosition = RelativePosition.Default;
		public RelativePosition ImagePosition {
			get {
				if ((ControlImplementation as Native.IButtonControlImplementation) != null) {
					mvarImagePosition = (ControlImplementation as Native.IButtonControlImplementation).GetImagePosition ();
				}
				return mvarImagePosition; }
			set {
				if ((ControlImplementation as Native.IButtonControlImplementation) != null) {
					(ControlImplementation as Native.IButtonControlImplementation).SetImagePosition (value);
				}
				mvarImagePosition = value;
			}
		}

		protected internal override void OnClick(EventArgs e)
		{
			if (ResponseValue != (int)DialogResult.None && this.ParentWindow is Dialog)
			{
				(this.ParentWindow as Dialog).DialogResult = (DialogResult)ResponseValue;
				(this.ParentWindow as Dialog).Close();
				return;
			}
			base.OnClick(e);
		}

		private ButtonBorderStyle mvarBorderStyle = ButtonBorderStyle.Normal;
		public ButtonBorderStyle BorderStyle { get { return mvarBorderStyle; } set { mvarBorderStyle = value; Application.Engine.UpdateControlProperties (this); } }

		public StockType StockType { get; set; } = StockType.None;

		private int mvarResponseValue = 0;
		/// <summary>
		/// The response value used when this <see cref="Button" /> is added to a <see cref="Dialog" />.
		/// </summary>
		/// <value>The response value.</value>
		public int ResponseValue { get { return mvarResponseValue; } set { mvarResponseValue = value; } }

		private HorizontalAlignment mvarHorizontalAlignment = HorizontalAlignment.Default;
		public HorizontalAlignment HorizontalAlignment { get { return mvarHorizontalAlignment; } set { mvarHorizontalAlignment = value; } }
	}
}
