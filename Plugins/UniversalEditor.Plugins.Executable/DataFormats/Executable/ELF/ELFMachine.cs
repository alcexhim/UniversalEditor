//
//  ELFMachine.cs - specifies the required architecture for the Executable and Linkable Format (ELF) executable
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace UniversalEditor.DataFormats.Executable.ELF
{
    /// <summary>
    /// Specifies the required architecture for the Executable and Linkable Format (ELF) executable.
    /// </summary>
    public enum ELFMachine : ushort
    {
        None = 0,
        /// <summary>
        /// AT&T WE 32100
        /// </summary>
        M32 = 1,
        /// <summary>
        /// SPARC
        /// </summary>
        SPARC = 2,
        /// <summary>
        /// Intel 80386
        /// </summary>
        Intel80386 = 3,
        /// <summary>
        /// Motorola 68K
        /// </summary>
        Motorola68000 = 4,
        /// <summary>
        /// Motorola 88K
        /// </summary>
        Motorola88000 = 5,
        /// <summary>
        /// Intel 80860
        /// </summary>
        Intel80860 = 7,
        /// <summary>
        /// MIPS RS3000
        /// </summary>
        MIPS = 8
    }
}
