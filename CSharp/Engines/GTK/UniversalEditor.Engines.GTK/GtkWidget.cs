using System;

namespace UniversalEditor.Engines.GTK
{
	public abstract class GtkWidget
	{
		public class GtkWidgetCollection
			: System.Collections.ObjectModel.Collection<GtkWidget>
		{
			private GtkContainer _parent = null;
			public GtkWidgetCollection(GtkContainer parent)
			{
				_parent = parent;
			}

			protected override void InsertItem (int index, GtkWidget item)
			{
				base.InsertItem (index, item);
				Internal.GTK.Methods.gtk_container_add (_parent.Handle, item.Handle);
			}
		}

		private IntPtr mvarHandle = IntPtr.Zero;
		public IntPtr Handle { get { return mvarHandle; } }

		public bool IsCreated {
			get { return mvarHandle != IntPtr.Zero; }
		}

		protected abstract IntPtr CreateInternal ();

		protected virtual void BeforeCreateInternal()
		{
		}
		protected virtual void AfterCreateInternal()
		{
		}

		public GtkWidget()
		{
			Create();
		}

		public bool Create()
		{
			if (IsCreated)
				return false;

			BeforeCreateInternal ();

			IntPtr handle = CreateInternal ();
			if (handle == IntPtr.Zero)
				return false;

			mvarHandle = handle;

			AfterCreateInternal ();
			return true;
		}

		private GtkWidget mvarParent = null;
		public GtkWidget Parent {
			get {
				if (mvarParent != null)
					return mvarParent;

				IntPtr handle = Internal.GTK.Methods.gtk_widget_get_parent (mvarHandle);
				if (handle != IntPtr.Zero)
					return null;
				return null;
			}
			set {
				Internal.GTK.Methods.gtk_widget_set_parent (mvarHandle, value.Handle);
				IntPtr handleCheck = Internal.GTK.Methods.gtk_widget_get_parent (mvarHandle);
				if (handleCheck == value.Handle)
					mvarParent = value;
			}
		}

		public bool Visible
		{
			get
			{
				return Internal.GTK.Methods.gtk_widget_get_visible (mvarHandle);
			}
			set
			{
				Internal.GTK.Methods.gtk_widget_set_visible (mvarHandle, value);
			}
		}
	}
}

