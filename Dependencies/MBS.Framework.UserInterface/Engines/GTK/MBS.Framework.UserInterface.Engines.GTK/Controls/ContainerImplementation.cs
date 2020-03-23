using System;
using System.Collections.Generic;
using MBS.Framework.UserInterface.Layouts;

namespace MBS.Framework.UserInterface.Engines.GTK.Controls
{
	[ControlImplementation(typeof(Container))]
	public class ContainerImplementation : GTKNativeImplementation
	{
		public ContainerImplementation(Engine engine, Container control)
			: base(engine, control)
		{
		}

		private Dictionary<Layout, IntPtr> handlesByLayout = new Dictionary<Layout, IntPtr>();

		private IntPtr mvarContainerHandle = IntPtr.Zero;

		private void ApplyLayout(IntPtr hContainer, Control ctl, Layout layout)
		{
			IntPtr ctlHandle = (Engine.GetHandleForControl(ctl) as GTKNativeControl).Handle;

			if (layout is BoxLayout)
			{
				BoxLayout box = (layout as BoxLayout);
				Internal.GTK.Methods.GtkBox.gtk_box_set_spacing(hContainer, box.Spacing);
				Internal.GTK.Methods.GtkBox.gtk_box_set_homogeneous(hContainer, box.Homogeneous);

				BoxLayout.Constraints c = (box.GetControlConstraints(ctl) as BoxLayout.Constraints);
				if (c == null) c = new BoxLayout.Constraints();

				int padding = c.Padding == 0 ? ctl.Padding.All : c.Padding;

				switch (c.PackType)
				{
					case BoxLayout.PackType.Start:
					{
						Internal.GTK.Methods.GtkBox.gtk_box_pack_start(hContainer, ctlHandle, c.Expand, c.Fill, padding);
						break;
					}
					case BoxLayout.PackType.End:
					{
						Internal.GTK.Methods.GtkBox.gtk_box_pack_end(hContainer, ctlHandle, c.Expand, c.Fill, padding);
						break;
					}
				}
			}
			else if (layout is Layouts.AbsoluteLayout)
			{
				Layouts.AbsoluteLayout.Constraints constraints = (layout.GetControlConstraints(ctl) as Layouts.AbsoluteLayout.Constraints);
				if (constraints == null) constraints = new Layouts.AbsoluteLayout.Constraints(0, 0, 0, 0);
				Internal.GTK.Methods.GtkFixed.gtk_fixed_put(hContainer, ctlHandle, constraints.X, constraints.Y);
			}
			else if (layout is Layouts.GridLayout)
			{
				Layouts.GridLayout.Constraints constraints = (layout.GetControlConstraints(ctl) as Layouts.GridLayout.Constraints);
				if (constraints != null)
				{
					// GtkTable has been deprecated. Use GtkGrid instead. It provides the same capabilities as GtkTable for arranging widgets in a rectangular grid, but does support height-for-width geometry management.
					if (Internal.GTK.Methods.Gtk.LIBRARY_FILENAME == Internal.GTK.Methods.Gtk.LIBRARY_FILENAME_V2)
					{
						Internal.GTK.Methods.GtkTable.gtk_table_attach(hContainer, ctlHandle, (uint)constraints.Column, (uint)(constraints.Column + constraints.ColumnSpan), (uint)constraints.Row, (uint)(constraints.Row + constraints.RowSpan), Internal.GTK.Constants.GtkAttachOptions.Expand, Internal.GTK.Constants.GtkAttachOptions.Fill, 0, 0);
					}
					else
					{
						Internal.GTK.Methods.GtkGrid.gtk_grid_attach(hContainer, ctlHandle, constraints.Column, constraints.Row, constraints.ColumnSpan, constraints.RowSpan);
						// Internal.GTK.Methods.Methods.gtk_table_attach(hContainer, ctlHandle, (uint)constraints.Column, (uint)(constraints.Column + constraints.ColumnSpan), (uint)constraints.Row, (uint)(constraints.Row + constraints.RowSpan), Internal.GTK.Constants.GtkAttachOptions.Expand, Internal.GTK.Constants.GtkAttachOptions.Fill, 0, 0);

						if ((constraints.Expand & ExpandMode.Horizontal) == ExpandMode.Horizontal)
						{
							Internal.GTK.Methods.GtkWidget.gtk_widget_set_hexpand(ctlHandle, true);
						}
						else
						{
							Internal.GTK.Methods.GtkWidget.gtk_widget_set_hexpand(ctlHandle, false);
						}
						if ((constraints.Expand & ExpandMode.Vertical) == ExpandMode.Vertical)
						{
							Internal.GTK.Methods.GtkWidget.gtk_widget_set_vexpand(ctlHandle, true);
						}
						else
						{
							Internal.GTK.Methods.GtkWidget.gtk_widget_set_vexpand(ctlHandle, false);
						}
					}
				}
			}
			else
			{
				Internal.GTK.Methods.GtkContainer.gtk_container_add(hContainer, ctlHandle);
			}
		}


		protected override NativeControl CreateControlInternal(Control control)
		{
			IntPtr hContainer = IntPtr.Zero;
			Container container = (control as Container);

			Layout layout = container.Layout;
			if (container.Layout == null) layout = new Layouts.AbsoluteLayout();

			if (layout is Layouts.BoxLayout)
			{
				Layouts.BoxLayout box = (layout as Layouts.BoxLayout);
				Internal.GTK.Constants.GtkOrientation orientation = Internal.GTK.Constants.GtkOrientation.Vertical;
				switch (box.Orientation)
				{
					case Orientation.Horizontal:
					{
						orientation = Internal.GTK.Constants.GtkOrientation.Horizontal;
						break;
					}
					case Orientation.Vertical:
					{
						orientation = Internal.GTK.Constants.GtkOrientation.Vertical;
						break;
					}
				}
				hContainer = Internal.GTK.Methods.GtkBox.gtk_box_new(orientation, ((Layouts.BoxLayout)layout).Homogeneous, ((Layouts.BoxLayout)layout).Spacing);
			}
			else if (layout is Layouts.AbsoluteLayout)
			{
				Layouts.AbsoluteLayout abs = (layout as Layouts.AbsoluteLayout);
				hContainer = Internal.GTK.Methods.GtkFixed.gtk_fixed_new();
			}
			else if (layout is Layouts.GridLayout)
			{
				if (Internal.GTK.Methods.Gtk.LIBRARY_FILENAME == Internal.GTK.Methods.Gtk.LIBRARY_FILENAME_V2)
				{
					Layouts.GridLayout grid = (layout as Layouts.GridLayout);
					// GtkTable has been deprecated. Use GtkGrid instead. It provides the same capabilities as GtkTable for arranging widgets in a rectangular grid, but does support height-for-width geometry management.
					hContainer = Internal.GTK.Methods.GtkTable.gtk_table_new();
					// hContainer = Internal.GTK.Methods.Methods.gtk_table_new();
					Internal.GTK.Methods.GtkTable.gtk_table_set_row_spacings(hContainer, (uint)grid.RowSpacing);
					Internal.GTK.Methods.GtkTable.gtk_table_set_col_spacings(hContainer, (uint)grid.ColumnSpacing);
				}
				else
				{
					Layouts.GridLayout grid = (layout as Layouts.GridLayout);
					// GtkTable has been deprecated. Use GtkGrid instead. It provides the same capabilities as GtkTable for arranging widgets in a rectangular grid, but does support height-for-width geometry management.
					hContainer = Internal.GTK.Methods.GtkGrid.gtk_grid_new();
					// hContainer = Internal.GTK.Methods.Methods.gtk_table_new();
					Internal.GTK.Methods.GtkGrid.gtk_grid_set_row_spacing(hContainer, (uint)grid.RowSpacing);
					Internal.GTK.Methods.GtkGrid.gtk_grid_set_column_spacing(hContainer, (uint)grid.ColumnSpacing);
				}
			}
			else if (layout is Layouts.FlowLayout)
			{
				hContainer = Internal.GTK.Methods.GtkFlowBox.gtk_flow_box_new();
				Internal.GTK.Methods.GtkFlowBox.gtk_flow_box_set_selection_mode(hContainer, Internal.GTK.Constants.GtkSelectionMode.None);
			}

			if (hContainer != IntPtr.Zero)
			{
				mvarContainerHandle = hContainer;
				handlesByLayout[layout] = hContainer;

				foreach (Control ctl in container.Controls)
				{
					bool ret = ctl.IsCreated;
					if (!ret) ret = Engine.CreateControl(ctl);
					if (!ret) continue;

					ApplyLayout(hContainer, ctl, layout);
				}
			}

			return new GTKNativeControl(hContainer);
		}
	}
}
