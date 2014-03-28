using Neo;
/*
 * Created by SharpDevelop.
 * User: Mike Becker
 * Date: 5/19/2013
 * Time: 3:44 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace UniversalEditor
{
	/// <summary>
	/// Description of ExtensionMethods.
	/// </summary>
	public static class ExtensionMethods
    {
        /// <summary>
        /// Writes a series of 2 32-bit Single values as a <see cref="TextureVector2" />.
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        public static void WriteTextureVector2x32(this IO.Writer bw, TextureVector2 vec)
        {
            bw.WriteSingle((float)vec.U);
            bw.WriteSingle((float)vec.V);
        }

        /// <summary>
        /// Writes a series of 3 32-bit Single values as a <see cref="PositionVector3" />.
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        public static void WritePositionVector3x32(this IO.Writer bw, PositionVector3 vec)
        {
            bw.WriteSingle((float)vec.X);
            bw.WriteSingle((float)vec.Y);
            bw.WriteSingle((float)vec.Z);
        }
        /// <summary>
        /// Writes a series of 4 32-bit Single values as a <see cref="PositionVector3" />.
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        public static void WritePositionVector4x32(this IO.Writer bw, PositionVector4 vec)
        {
            bw.WriteSingle((float)vec.X);
            bw.WriteSingle((float)vec.Y);
            bw.WriteSingle((float)vec.Z);
            bw.WriteSingle((float)vec.W);
		}

		/// <summary>
		/// Reads a series of 2 32-bit Single values as a <see cref="TextureVector2" />.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static TextureVector2 ReadTextureVector2x32(this IO.Reader br)
		{
			float textureU = br.ReadSingle();
			float textureV = br.ReadSingle();
			return new TextureVector2(textureU, textureV);
		}
		/// <summary>
		/// Reads a series of 2 64-bit Double values as a <see cref="TextureVector2" />.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static TextureVector2 ReadTextureVector2x64(this IO.Reader br)
		{
			double textureU = br.ReadDouble();
			double textureV = br.ReadDouble();
			return new TextureVector2(textureU, textureV);
		}

		/// <summary>
		/// Reads a series of 3 32-bit Single values as a <see cref="PositionVector3" />.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static PositionVector3 ReadPositionVector3x32(this IO.Reader br)
		{
            float positionX = br.ReadSingle();
            float positionY = br.ReadSingle();
            float positionZ = br.ReadSingle();
            return new PositionVector3(positionX, positionY, positionZ);
		}
		/// <summary>
		/// Reads a series of 3 64-bit Double values as a <see cref="PositionVector3" />.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static PositionVector3 ReadPositionVector3x64(this IO.Reader br)
		{
            double positionX = br.ReadDouble();
            double positionY = br.ReadDouble();
            double positionZ = br.ReadDouble();
            return new PositionVector3(positionX, positionY, positionZ);
        }
        /// <summary>
        /// Reads a series of 3 32-bit Single values as a <see cref="PositionVector3" />.
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        public static PositionVector4 ReadPositionVector4x32(this IO.Reader br)
        {
            float positionX = br.ReadSingle();
            float positionY = br.ReadSingle();
            float positionZ = br.ReadSingle();
            float positionW = br.ReadSingle();
            return new PositionVector4(positionX, positionY, positionZ, positionW);
        }
        /// <summary>
        /// Reads a series of 3 64-bit Double values as a <see cref="PositionVector3" />.
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        public static PositionVector4 ReadPositionVector4x64(this IO.Reader br)
        {
            double positionX = br.ReadDouble();
            double positionY = br.ReadDouble();
            double positionZ = br.ReadDouble();
            double positionW = br.ReadDouble();
            return new PositionVector4(positionX, positionY, positionZ, positionW);
        }

        public static PositionVector2 Transform(this PositionVector2 input, Matrix matrix)
        {
            double newX = input.X * matrix[0, 0] + input.Y * matrix[1, 0] + matrix[3, 0];
            double newY = input.X * matrix[0, 1] + input.Y * matrix[1, 1] + matrix[3, 1];
            return new PositionVector2(newX, newY);
        }
        public static PositionVector2 Rotate(this PositionVector2 input, Matrix matrix)
        {
            double newX = input.X * matrix[0, 0] + input.Y * matrix[1, 0];
            double newY = input.X * matrix[0, 1] + input.Y * matrix[1, 1];
            return new PositionVector2(newX, newY);
        }
	}
}
