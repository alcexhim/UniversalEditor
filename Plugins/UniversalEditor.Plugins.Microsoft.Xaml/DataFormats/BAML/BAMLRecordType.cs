//
//  BAMLRecord.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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

namespace UniversalEditor.Plugins.Microsoft.Xaml.DataFormats.BAML
{
	public enum BAMLRecordType : byte
	{
		Unknown = 0,
		DocumentStart = 1,
		DocumentEnd = 2,

		/// <summary>
		/// Start Element Node, which may be a CLR object or a DependencyObject
		/// </summary>
		ElementStart = 3,
		ElementEnd = 4,

		/// <summary>
		/// Property Node, which may be a CLR property or a DependencyProperty
		/// </summary>
		Property = 5,

		/// <summary>
		/// Binary serialization of a property
		/// </summary>
		PropertyCustom = 6,

		/// <summary>
		/// Complex Property Node
		/// </summary>
		PropertyComplexStart = 7,

		/// <summary>
		/// End Complex Property Node
		/// </summary>
		PropertyComplexEnd = 8,

		/// <summary>
		/// Start Array Property Node
		/// </summary>
		PropertyArrayStart = 9,

		/// <summary>
		/// End Array Property Node
		/// </summary>
		PropertyArrayEnd = 10,

		/// <summary>
		/// Star IList Property Node
		/// </summary>
		PropertyIListStart = 11,

		/// <summary>
		/// End PropertyIListStart Node
		/// </summary>
		PropertyIListEnd = 12,

		/// <summary>
		/// Start IDictionary Property Node
		/// </summary>
		PropertyIDictionaryStart = 13,

		/// <summary>
		/// End IDictionary Property Node
		/// </summary>
		PropertyIDictionaryEnd = 14,

		/// <summary>
		/// LiteralContent Node
		/// </summary>
		LiteralContent = 15,

		/// <summary>
		/// Text Node
		/// </summary>
		Text = 16,

		/// <summary>
		/// Text that has an associated custom typeconverter
		/// </summary>
		TextWithConverter = 17,

		/// <summary>
		/// RoutedEventNode
		/// </summary>
		RoutedEvent = 18,

		/// <summary>
		/// ClrEvent Node
		/// </summary>
		ClrEvent = 19,

		/// <summary>
		/// XmlnsProperty Node
		/// </summary>
		XmlnsProperty = 20,

		/// <summary>
		/// XmlAttribute Node
		/// </summary>
		XmlAttribute = 21,

		/// <summary>
		/// Processing Intstruction Node
		/// </summary>
		ProcessingInstruction = 22,

		/// <summary>
		/// Comment Node
		/// </summary>
		Comment = 23,

		/// <summary>
		/// DefTag Node
		/// </summary>
		DefTag = 24,

		/// <summary>
		/// x:name="value" attribute.  One typical use of this
		/// attribute is to define a key to use when inserting an item into an IDictionary
		/// </summary>
		DefAttribute = 25,

		/// <summary>
		/// EndAttributes Node
		/// </summary>
		EndAttributes = 26,

		/// <summary>
		/// PI xml - clr namespace mapping
		/// </summary>
		PIMapping = 27,

		/// <summary>
		/// Assembly information
		/// </summary>
		AssemblyInfo = 28,

		/// <summary>
		/// Type information
		/// </summary>
		TypeInfo = 29,

		/// <summary>
		/// Type information for a Type that has an associated custom serializer
		/// </summary>
		TypeSerializerInfo = 30,

		/// <summary>
		/// Attribute (eg - properties and events) information
		/// </summary>
		AttributeInfo = 31,

		/// <summary>
		/// Resource information
		/// </summary>
		StringInfo = 32,

		/// <summary>
		/// Property Resource Reference
		/// </summary>
		PropertyStringReference = 33,

		/// <summary>
		/// Record for setting a property to a Type reference.  This is used for
		/// properties that are of type "Type"
		/// </summary>
		PropertyTypeReference = 34,

		/// <summary>
		/// Property that has a simple MarkupExtension value.
		/// </summary>
		PropertyWithExtension = 35,      // 35

		/// <summary>
		/// Property that has an associated custom typeconverter
		/// </summary>
		PropertyWithConverter = 36,

		/// <summary>
		/// Start a deferable content block
		/// </summary>
		DeferableContentStart = 37,

		/// <summary>
		/// x:name="value" attribute when used within a defer load
		/// dictionary.  These keys are hoisted to the front of the dictionary when
		/// written to baml.
		/// </summary>
		DefAttributeKeyString = 38,

		/// <summary>
		/// Implied key that is a Type attribute when used within a defer load
		/// dictionary.  These keys are hoisted to the front of the dictionary when
		/// written to baml.
		/// </summary>
		DefAttributeKeyType = 39,

		/// <summary>
		/// This marks the start of an element tree that is used as the key in
		/// an IDictionary.
		/// </summary>
		KeyElementStart = 40,


		/// <summary>
		/// This marks the end of an element tree that is used as the key in
		/// an IDictionary.
		/// </summary>
		KeyElementEnd = 41,

		/// <summary>
		/// Record marks the start of a section containing constructor parameters
		/// </summary>
		ConstructorParametersStart = 42,

		/// <summary>
		/// Record marks the end of a section containing constructor parameters
		/// </summary>
		ConstructorParametersEnd = 43,

		/// <summary>
		/// Constructor parameter that has been resolved to a Type.
		/// </summary>
		ConstructorParameterType = 44,

		/// <summary>
		/// Record that has info about which event or id to connect to in an object tree.
		/// </summary>
		ConnectionId = 45,

		/// <summary>
		/// Record that set the conntent property context for the element
		/// </summary>
		ContentProperty = 46,

		/// <summary>
		/// ElementStartRecord that also carries an element name.
		/// </summary>
		NamedElementStart = 47,

		/// <summary>
		/// Start of StaticResourceExtension within the header of a deferred section.
		/// </summary>
		StaticResourceStart = 48,

		/// <summary>
		/// End of a StaticResourceExtension within the header of a deferred section.
		/// </summary>
		StaticResourceEnd = 49,

		/// <summary>
		/// BamlRecord that carries an identifier for a StaticResourceExtension
		/// within the header of a deferred section.
		/// </summary>
		StaticResourceId = 50,

		/// <summary>
		/// This is a TextRecord that holds an Id for the String value it represents.
		/// </summary>
		TextWithId = 51,

		/// <summary>
		/// PresentationOptions:Freeze="value" attribute. Used for ignorable
		/// WPF-specific parsing options
		/// </summary>
		PresentationOptionsAttribute = 52,

		/// <summary>
		/// Debugging information record that holds the source XAML linenumber.
		/// </summary>
		LineNumberAndPosition = 53,

		/// <summary>
		/// Debugging information record that holds the source XAML line position.
		/// </summary>
		LinePosition = 54,

		/// <summary>
		/// OptimizedStaticResourceExtension within the header of a deferred section.
		/// </summary>
		OptimizedStaticResource = 55,

		/// <summary>
		/// BamlPropertyRecord that carries an identifier for a StaticResourceExtension
		/// within the header of a deferred section.
		/// </summary>
		PropertyWithStaticResourceId = 56
	}
}
