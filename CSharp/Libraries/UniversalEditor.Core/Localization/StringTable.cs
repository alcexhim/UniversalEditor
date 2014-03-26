using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.Localization
{
    public static class StringTable
    {
        private static string mvarApplicationName = "Universal Editor";
        public static string ApplicationName { get { return mvarApplicationName; } }

        private static string mvarErrorDataFormatNotOpen = "The data format is not open.";
        public static string ErrorDataFormatNotOpen { get { return mvarErrorDataFormatNotOpen; } }

        private static string mvarErrorDataCorrupted = "The file is corrupted.";
        public static string ErrorDataCorrupted { get { return mvarErrorDataCorrupted; } }

        private static string mvarErrorFileNotFound = "The file could not be found.";
        public static string ErrorFileNotFound { get { return mvarErrorFileNotFound; } }
        private static string mvarErrorNotAnObjectModel = "The specified type is not an object model.";
        public static string ErrorNotAnObjectModel { get { return mvarErrorNotAnObjectModel; } }

        private static string mvarErrorObjectModelNull = "The object model must not be null.";
        public static string ErrorObjectModelNull { get { return mvarErrorObjectModelNull; } set { mvarErrorObjectModelNull = value; } }

		private static string mvarErrorDataFormatInvalid = "The data format is invalid.";
		public static string ErrorDataFormatInvalid { get { return mvarErrorDataFormatInvalid; } set { mvarErrorDataFormatInvalid = value; } }
	}
}
