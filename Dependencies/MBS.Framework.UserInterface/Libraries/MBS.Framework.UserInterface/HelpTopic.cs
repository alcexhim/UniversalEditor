using System;
namespace MBS.Framework.UserInterface
{
	public class HelpTopic
	{
		public string Name { get; set; } = null;

		public HelpTopic()
		{
		}
		public HelpTopic(string name)
		{
			Name = name;
		}
	}
}
