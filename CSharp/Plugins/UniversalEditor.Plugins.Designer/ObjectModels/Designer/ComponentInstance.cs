using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Designer
{
    public class ComponentInstance
    {
        public class ComponentInstanceCollection
            : System.Collections.ObjectModel.Collection<ComponentInstance>
        {

        }

        private Guid mvarID = Guid.Empty;
        public Guid ID { get { return mvarID; } set { mvarID = value; } }

        private Component mvarComponent = null;
        public Component Component { get { return mvarComponent; } set { mvarComponent = value; } }

        private ConnectionValue.ConnectionValueCollection mvarConnectionValues = new ConnectionValue.ConnectionValueCollection();
        public ConnectionValue.ConnectionValueCollection ConnectionValues { get { return mvarConnectionValues; } }

        private PropertyValue.PropertyValueCollection mvarPropertyValues = new PropertyValue.PropertyValueCollection();
        public PropertyValue.PropertyValueCollection PropertyValues { get { return mvarPropertyValues; } }

        private Measurement mvarX = new Measurement(0, MeasurementUnit.Pixel);
        /// <summary>
        /// The distance between the left edge of the design and the left edge of the component.
        /// </summary>
        public Measurement X { get { return mvarX; } set { mvarX = value; } }

        private Measurement mvarY = new Measurement(0, MeasurementUnit.Pixel);
        /// <summary>
        /// The distance between the top edge of the design and the top edge of the component.
        /// </summary>
        public Measurement Y { get { return mvarY; } set { mvarY = value; } }

        private Measurement mvarZ = new Measurement(0, MeasurementUnit.Pixel);
        /// <summary>
        /// The distance between the left edge of the design and the left edge of the component.
        /// </summary>
        public Measurement Z { get { return mvarZ; } set { mvarZ = value; } }

        private Measurement mvarWidth = new Measurement(0, MeasurementUnit.Pixel);
        /// <summary>
        /// The width of the component.
        /// </summary>
        public Measurement Width { get { return mvarWidth; } set { mvarWidth = value; } }

        private Measurement mvarHeight = new Measurement(0, MeasurementUnit.Pixel);
        /// <summary>
        /// The height of the component.
        /// </summary>
        public Measurement Height { get { return mvarHeight; } set { mvarHeight = value; } }

        private Measurement mvarDepth = new Measurement(0, MeasurementUnit.Pixel);
        /// <summary>
        /// The depth of the component.
        /// </summary>
        public Measurement Depth { get { return mvarDepth; } set { mvarDepth = value; } }

    }
}
