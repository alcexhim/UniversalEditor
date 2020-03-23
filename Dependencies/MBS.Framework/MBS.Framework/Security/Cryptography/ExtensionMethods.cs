using System.Security.Cryptography;

namespace MBS.Framework.Security.Cryptography
{
	public static class ExtensionMethods
	{
		/// <summary>
		/// Computes the hash of the specified value in the given encoding.
		/// </summary>
		/// <returns>The hash.</returns>
		/// <param name="value">Value.</param>
		public static string ComputeHash(this HashAlgorithm ha, string value, System.Text.Encoding encoding = null)
		{
			if (encoding == null) encoding = System.Text.Encoding.UTF8;

			byte[] buffer = encoding.GetBytes(value);

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			byte[] output = ha.ComputeHash(buffer);
			for (int i = 0; i < output.Length; i++)
			{
				sb.Append(output[i].ToString("x").PadLeft(2, '0'));
			}
			return sb.ToString();
		}
	}
}
