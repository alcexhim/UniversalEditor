using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus
{
    public abstract class IcarusCommand : ICloneable
    {
        public class IcarusCommandCollection
            : System.Collections.ObjectModel.Collection<IcarusCommand>
        {
        }

		public IcarusParameter.IcarusParameterCollection Parameters { get; } = new IcarusParameter.IcarusParameterCollection();

        public abstract object Clone();

		private static Dictionary<string, Type> _cmdsByName = null;
		public static IcarusCommand CreateFromName(string funcName)
		{
			if (_cmdsByName == null)
			{
				_cmdsByName = new Dictionary<string, Type>();
				Type[] cmdtypes = MBS.Framework.Reflection.GetAvailableTypes(new Type[] { typeof(IcarusCommand) });
				for (int i = 0; i < cmdtypes.Length; i++)
				{
					if (cmdtypes[i].IsAbstract)
						continue;

					IcarusCommand cmd = (cmdtypes[i].Assembly.CreateInstance(cmdtypes[i].FullName) as IcarusCommand);
					if (cmd is IcarusPredefinedCommand)
					{
						string nam = (cmd as IcarusPredefinedCommand).Name;
						if (!String.IsNullOrEmpty(nam))
							_cmdsByName[nam] = cmdtypes[i];
					}
				}
			}
			if (_cmdsByName.ContainsKey(funcName))
				return (_cmdsByName[funcName].Assembly.CreateInstance(_cmdsByName[funcName].FullName) as IcarusCommand);
			return null;
		}
	}
}
