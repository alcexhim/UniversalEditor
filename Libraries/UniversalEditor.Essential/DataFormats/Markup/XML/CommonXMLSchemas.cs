//
//  XMLSchemas.cs - provides common XML schema definitions used in a wide variety of applications
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
namespace UniversalEditor.DataFormats.Markup.XML
{
	/// <summary>
	/// Provides common XML schema definitions used in a wide variety of applications.
	/// </summary>
	public static class CommonXMLSchemas
	{
		/// <summary>
		/// The /elements/1.1/ namespace was created in 2000 for the RDF representation of the fifteen-element
		/// Dublin Core and has been widely used in data for more than twenty years. This namespace corresponds
		/// to the original scope of ISO 15836, which was published first in 2003 and last revised in 2017 as
		/// ISO 15836-1:2017.
		/// </summary>
		/// <value>The XML schema for the original Dublin Core namespace.</value>
		public static string DublinCore { get; } = "http://purl.org/dc/elements/1.1/";
		/// <summary>
		/// The /terms/ namespace was originally created in 2001 for identifying new terms coined outside of the
		/// original fifteen-element Dublin Core. In 2008, in the context of defining formal semantic constraints
		/// for DCMI metadata terms in support of RDF applications, the original fifteen elements themselves were
		/// mirrored in the /terms/ namespace. As a result, there exists both a dc:date (http://purl.org/dc/elements/1.1/date)
		/// with no formal range and a corresponding dcterms:date (http://purl.org/dc/terms/date) with a formal
		/// range of "literal". While these distinctions are significant for creators of RDF applications, most users
		/// can safely treat the fifteen parallel properties as equivalent. The most useful properties and classes of
		/// DCMI Metadata Terms have now been published as ISO 15836-2:2019 [ISO 15836-2:2019]. While the /elements/1.1/
		/// namespace will be supported indefinitely, DCMI gently encourages use of the /terms/ namespace.
		/// </summary>
		/// <value>The XML schema for the Dublin Core Terms namespace.</value>
		public static string DublinCoreTerms { get; } = "http://purl.org/dc/terms/";
		/// <summary>
		/// The /dcmitype/ namespace was created in 2001 for the DCMI Type Vocabulary, which defines classes for
		/// basic types of thing that can be described using DCMI metadata terms.
		/// </summary>
		/// <value>The XML schema for the Dublin Core DCMI Type namespace.</value>
		public static string DublinCoreDCMIType { get; } = "http://purl.org/dc/dcmitype/";
		/// <summary>
		/// The /dcam/ namespace was created in 2008 for terms used in the description of DCMI metadata terms.
		/// </summary>
		/// <value>The XML schema for the Dublin Core DCAM namespace.</value>
		public static string DublinCoreDCAM { get; } = "http://purl.org/dc/dcam/";
	}
}
