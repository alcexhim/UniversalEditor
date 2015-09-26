using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;

namespace UniversalEditor.ObjectModels.Web
{
	public static class MeasurementExtensionMethods
	{
		public static void WriteMeasurement(this Writer writer, Measurement measurement)
		{
			writer.WriteByte((byte)measurement.Unit);
			writer.WriteDouble(measurement.Value);
		}
		public static Measurement ReadMeasurement(this Reader reader)
		{
			MeasurementUnit unit = (MeasurementUnit)reader.ReadByte();
			double value = reader.ReadDouble();
			return new Measurement(value, unit);
		}
	}
}
