//
//  FileInfo.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
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
using System;
namespace UniversalEditor.UserInterface.Common
{
	public static class FileInfo
	{
		public static string FormatSize(long size)
		{
			double dsize = (double)size;
			double rsize = dsize;
			string rstr = String.Empty;

			if (dsize > 1024)
			{
				// KB - kilobytes (kibibytes)
				if (dsize > Math.Pow(1024, 2))
				{
					// MB - megabytes (mebibytes)
					if (dsize > Math.Pow(1024, 3))
					{
						// GB - gigabytes (gibibytes)
						if (dsize > Math.Pow(1024, 4))
						{
							// TB - terabytes (tebibytes)
							if (dsize > Math.Pow(1024, 5))
							{
								// PB - petabytes (pebibytes)
								if (dsize > Math.Pow(1024, 6))
								{
									// EB - exabytes (exbibytes)
									if (dsize > Math.Pow(1024, 7))
									{
										// ZB - zettabytes (zebibytes)
										if (dsize > Math.Pow(1024, 8))
										{
											// YB - yottabytes (yobibytes)
											rsize = (dsize / Math.Pow(1024, 8));
											rstr = "YiB";
										}
										else
										{
											rsize = (dsize / Math.Pow(1024, 7));
											rstr = "ZiB";
										}
									}
									else
									{
										rsize = (dsize / Math.Pow(1024, 6));
										rstr = "EiB";
									}
								}
								else
								{
									rsize = (dsize / Math.Pow(1024, 5));
									rstr = "PiB";
								}
							}
							else
							{
								rsize = (dsize / Math.Pow(1024, 4));
								rstr = "TiB";
							}
						}
						else
						{
							rsize = (dsize / Math.Pow(1024, 3));
							rstr = "GiB";
						}
					}
					else
					{
						rsize = (dsize / Math.Pow(1024, 2));
						rstr = "MiB";
					}
				}
				else
				{
					rsize = (dsize / ((double)1024));
					rstr = "KiB";
				}
			}
			else
			{
				rsize = dsize;
				rstr = "bytes";
			}
			rsize = Math.Round(rsize, 1);
			return rsize.ToString() + " " + rstr;
		}
	}
}
