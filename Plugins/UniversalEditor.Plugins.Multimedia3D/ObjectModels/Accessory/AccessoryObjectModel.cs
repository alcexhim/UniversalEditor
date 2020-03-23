using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia3D.Accessory
{
    /// <summary>
    /// Represents an accessory attached to a 3D model.
    /// </summary>
    public class AccessoryObjectModel : ObjectModel
    {
        public override void Clear()
        {
        }
        public override void CopyTo(ObjectModel where)
        {
        }

        private static ObjectModelReference _omr = null;
        public override ObjectModelReference MakeReference()
        {
            if (_omr == null)
            {
                _omr = base.MakeReference();
                _omr.Title = "Accessory";
                _omr.Path = new string[] { "Multimedia", "3D Multimedia", "Accessory" };
                _omr.Description = "Defines a 3D model that can be attached to another 3D model that supports the attachment of accessories";
            }
            return _omr;
        }

        private AccessoryItem.AccessoryItemCollection mvarAccessories = new AccessoryItem.AccessoryItemCollection();
        /// <summary>
        /// The accessories contained in this file.
        /// </summary>
        public AccessoryItem.AccessoryItemCollection Accessories { get { return mvarAccessories; } }
    }
}
