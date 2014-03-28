using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia3D.Accessory
{
    public class AccessoryItem
    {
        /// <summary>
        /// Stores a <see cref="System.Collections.ObjectModel.Collection" /> of <see cref="AccessoryItem" /> objects.
        /// </summary>
        public class AccessoryItemCollection
            : System.Collections.ObjectModel.Collection<AccessoryItem>
        {
        }

        private string mvarTitle = String.Empty;
        /// <summary>
        /// The title of this <see cref="AccessoryItem" />.
        /// </summary>
        /// <remarks>This is the string displayed in the accessory menu upon loading in MikuMikuDance, or in the Accessory Browser upon loading in Concertroid! Editor.</remarks>
        public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

        private string mvarModelFileName = String.Empty;
        /// <summary>
        /// The file name of the model file to associate with this <see cref="AccessoryItem" />.
        /// </summary>
        /// <remarks>For use in MikuMikuDance, the model file must be a DirectX model (*.x file). For use in Concertroid!, may be any model file that is readable by the Universal Editor.</remarks>
        public string ModelFileName { get { return mvarModelFileName; } set { mvarModelFileName = value; } }

        private PositionVector3 mvarScale = new PositionVector3(1.0, 1.0, 1.0);
        /// <summary>
        /// Amount of magnification to apply to this <see cref="AccessoryItem" />.
        /// </summary>
        public PositionVector3 Scale { get { return mvarScale; } set { mvarScale = value; } }

        private PositionVector3 mvarPosition = new PositionVector3(0.0, 0.0, 0.0);
        /// <summary>
        /// The position of this <see cref="AccessoryItem" /> relative to the parent bone.
        /// </summary>
        public PositionVector3 Position { get { return mvarPosition; } set { mvarPosition = value; } }

        private PositionVector4 mvarRotation = new PositionVector4(0.0, 0.0, 0.0, 1.0);
        /// <summary>
        /// The angle of rotation of this <see cref="AccessoryItem" /> relative to the parent bone.
        /// </summary>
        public PositionVector4 Rotation { get { return mvarRotation; } set { mvarRotation = value; } }

        private string mvarBoneName = String.Empty;
        /// <summary>
        /// The name of the bone to which this <see cref="AccessoryItem" /> should be attached.
        /// </summary>
        /// <remarks>The <see cref="AccessoryObjectModel" /> stores the bone name in DataFormat-independent UTF-8 encoding. Certain DataFormats (like MikuMikuDance <see cref="VACDataFormat" />) will convert the string to and from Japanese Shift-JIS encoding upon saving and loading.</remarks>
        public string BoneName { get { return mvarBoneName; } set { mvarBoneName = value; } }
    }
}
