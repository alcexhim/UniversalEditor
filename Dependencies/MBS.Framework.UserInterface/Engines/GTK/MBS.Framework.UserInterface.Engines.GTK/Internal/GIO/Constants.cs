using System;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.GIO
{
	internal class Constants
	{
		[Flags()]
		public enum GApplicationFlags
		{
			None = 0,
			/// <summary>
			/// Run as a service. In this mode, registration fails if the service is already running,
			/// and the application will initially wait up to 10 seconds for an initial activation
			/// message to arrive.
			/// </summary>
			IsService = (1 << 0),
			/// <summary>
			/// Don't try to become the primary instance.
			/// </summary>
			IsLauncher = (1 << 1),
			/// <summary>
			/// This application handles opening files (in the primary instance). Note that this flag
			/// only affects the default implementation of local_command_line(), and has no effect if
			/// <see cref="HandlesCommandLine"/> is given. See g_application_run() for details.
			/// </summary>
			HandlesOpen = (1 << 2),
			/// <summary>
			/// This application handles command line arguments (in the primary instance). Note that
			/// this flag only affect the default implementation of local_command_line(). See
			/// g_application_run() for details.
			/// </summary>
			HandlesCommandLine = (1 << 3),
			/// <summary>
			/// Send the environment of the launching process to the primary instance. Set this flag
			/// if your application is expected to behave differently depending on certain environment
			/// variables. For instance, an editor might be expected to use the <envar>GIT_COMMITTER_NAME</envar>
			/// environment variable when editing a git commit message. The environment is available to the
			/// #GApplication::command-line signal handler, via g_application_command_line_getenv().
			/// </summary>
			SendEnvironment = (1 << 4),
			/// <summary>
			/// Make no attempts to do any of the typical single-instance application negotiation, even if the
			/// application ID is given.  The application neither attempts to become the owner of the application
			/// ID nor does it check if an existing owner already exists.  Everything occurs in the local process.
			/// Since: 2.30.
			/// </summary>
			NonUnique = (1 << 5)
		}
	}
}

