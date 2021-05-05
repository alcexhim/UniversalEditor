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
		public static string FormatSize(long size, int powBy = 1024)
		{
			double dsize = (double)size;
			double rsize = dsize;
			string rstr = String.Empty;

			powBy = 1000; // 1024
			if (dsize > powBy)
			{
				// KB - kilobytes (kibibytes)
				if (dsize > Math.Pow(powBy, 2))
				{
					// MB - megabytes (mebibytes)
					if (dsize > Math.Pow(powBy, 3))
					{
						// GB - gigabytes (gibibytes)
						if (dsize > Math.Pow(powBy, 4))
						{
							// TB - terabytes (tebibytes)
							if (dsize > Math.Pow(powBy, 5))
							{
								// PB - petabytes (pebibytes)
								if (dsize > Math.Pow(powBy, 6))
								{
									// EB - exabytes (exbibytes)
									if (dsize > Math.Pow(powBy, 7))
									{
										// ZB - zettabytes (zebibytes)
										if (dsize > Math.Pow(powBy, 8))
										{
											// YB - yottabytes (yobibytes)
											rsize = (dsize / Math.Pow(powBy, 8));
											rstr = powBy == 1000 ? "YB" : "YiB";
										}
										else
										{
											rsize = (dsize / Math.Pow(powBy, 7));
											rstr = powBy == 1000 ? "ZB" : "ZiB";
										}
									}
									else
									{
										rsize = (dsize / Math.Pow(powBy, 6));
										rstr = powBy == 1000 ? "EB" : "EiB";
									}
								}
								else
								{
									rsize = (dsize / Math.Pow(powBy, 5));
									rstr = powBy == 1000 ? "PB" : "PiB";
								}
							}
							else
							{
								rsize = (dsize / Math.Pow(powBy, 4));
								rstr = powBy == 1000 ? "TB" : "TiB";
							}
						}
						else
						{
							rsize = (dsize / Math.Pow(powBy, 3));
							rstr = powBy == 1000 ? "GB" : "GiB";
						}
					}
					else
					{
						rsize = (dsize / Math.Pow(powBy, 2));
						rstr = powBy == 1000 ? "MB" : "MiB";
					}
				}
				else
				{
					rsize = (dsize / ((double)powBy));
					rstr = powBy == 1000 ? "KB" : "KiB";
				}
			}
			else
			{
				rsize = dsize;
				rstr = "bytes";
			}
			rsize = Math.Round(rsize, 1);
			return String.Format("{0} {1}", rsize.ToString(), rstr);
		}
	}
}
