using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.UserInterface
{
    public interface IOptionPanelImplementation
    {
        void LoadSettings();
        void SaveSettings();
        void ResetSettings();

        bool IsAvailable { get; }

        string[] OptionGroups { get; }
    }
}
