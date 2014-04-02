/*
 * Created by SharpDevelop.
 * User: Mike Becker
 * Date: 5/12/2013
 * Time: 1:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace UniversalEditor.DataFormats.FileSystem.Nero.Internal
{
	/// <summary>
	/// Description of DirectoryEntry.
	/// </summary>
	internal struct DirectoryEntry
	{
		public string name;
		public System.Collections.Generic.List<DirectoryEntry> entries;
	}
}
