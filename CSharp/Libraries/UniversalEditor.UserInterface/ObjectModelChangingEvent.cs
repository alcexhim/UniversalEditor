using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace UniversalEditor.UserInterface
{
	/// <summary>
	/// The event handler that is raised when an <see cref="ObjectModel" /> is changed, e.g. on an <see cref="Editor" />.
	/// </summary>
	public delegate void ObjectModelChangingEventHandler(object sender, ObjectModelChangingEventArgs e);
	/// <summary>
	/// The <see cref="EventArgs" /> used with the <see cref="ObjectModelChangingEventHandler" />.
	/// </summary>
	public class ObjectModelChangingEventArgs : CancelEventArgs
	{
		/// <summary>
		/// The original <see cref="ObjectModel" /> before the change occurs.
		/// </summary>
		/// <value>The original <see cref="ObjectModel" /> before the change occurs.</value>
		public ObjectModel OldObjectModel { get; private set; }
		/// <summary>
		/// The current <see cref="ObjectModel" /> after the change occurs.
		/// </summary>
		/// <value>The current <see cref="ObjectModel" /> after the change occurs.</value>
		public ObjectModel NewObjectModel { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectModelChangingEventArgs"/> class with the given old <see cref="ObjectModel" /> and new <see cref="ObjectModel" />.
		/// </summary>
		/// <param name="oldObjectModel">The original <see cref="ObjectModel" /> before the change occurs.</param>
		/// <param name="newObjectModel">The current <see cref="ObjectModel" /> after the change occurs.</param>
		public ObjectModelChangingEventArgs(ObjectModel oldObjectModel, ObjectModel newObjectModel)
		{
			OldObjectModel = oldObjectModel;
			NewObjectModel = newObjectModel;
		}
	}
}
