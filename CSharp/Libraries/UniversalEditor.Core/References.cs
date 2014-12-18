using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
    /// <summary>
    /// Defines an object that references a <see cref="T:ReferencedBy`1" /> object.
    /// </summary>
    /// <typeparam name="TRef">The <see cref="T:ReferencedBy`1" /> object referenced by this <see cref="T:References`1" /> object.</typeparam>
	public interface References<TRef>
	{
        /// <summary>
		/// Creates or returns an existing <see cref="T:ReferencedBy`1" /> object referring to this <see cref="T:References`1" /> object.
        /// </summary>
		/// <returns>A <see cref="T:ReferencedBy`1" /> object that can be used to create additional instances of this <see cref="T:References`1" /> object.</returns>
		TRef MakeReference();
	}
    /// <summary>
	/// Defines an object that is referenced by the given <see cref="T:References`1" /> object.
    /// </summary>
	/// <typeparam name="TObj">The <see cref="T:References`1" /> object which references this <see cref="T:ReferencedBy`1" /> object.</typeparam>
	public interface ReferencedBy<TObj>
    {
        /// <summary>
		/// Creates an instance of this <see cref="T:ReferencedBy`1" /> object from the <see cref="Type" /> described in the associated <see cref="T:References`1" /> object.
        /// </summary>
		/// <returns>An instance of this <see cref="T:ReferencedBy`1" /> object created from the <see cref="Type" /> described in the associated <see cref="T:References`1" /> object.</returns>
		TObj Create();
        
        /// <summary>
		/// Gets the detail fields that are shown in lists of this <see cref="T:ReferencedBy`1" /> object in details view.
        /// </summary>
		/// <returns>An array of <see cref="String" />s that are shown in detail columns of lists of this <see cref="T:ReferencedBy`1" /> object.</returns>
        string[] GetDetails();
	}
}
