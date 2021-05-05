namespace UniversalEditor.UserInterface
{
	public interface IHostApplication
	{
		/// <summary>
		/// Gets or sets the current window of the host application.
		/// </summary>
		// IHostApplicationWindow CurrentWindow { get; set; }
		/// <summary>
		/// Gets or sets the output window of the host application, where other plugins can read from and write to.
		/// </summary>
		HostApplicationOutputWindow OutputWindow { get; set; }
		/// <summary>
		/// A collection of messages to display in the Error List panel.
		/// </summary>
		HostApplicationMessage.HostApplicationMessageCollection Messages { get; }
	}
}
