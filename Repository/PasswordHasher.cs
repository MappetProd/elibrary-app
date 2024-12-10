using System.Security.Cryptography;

namespace EL.Repository.Security
{
	public class PasswordHasher
	{
		/// <summary>
		/// Size of salt.
		/// </summary>
		private const int SaltSize = 16;

		/// <summary>
		/// Size of hash.
		/// </summary>
		private const int HashSize = 20;

		/// <summary>
		/// Iterations.
		/// </summary>
		private const int Iterations = 100000;


		/// <summary>
		/// Creates a hash from a password with 100000 iterations
		/// </summary>
		/// <param name="password">The password.</param>
		/// <returns>The hash.</returns>

		public static string Hash(string password)
		{
			byte[] salt;
			byte[] buffer2;
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, SaltSize, Iterations, HashAlgorithmName.SHA256))
			{
				salt = bytes.Salt;
				buffer2 = bytes.GetBytes(HashSize);
			}
			byte[] dst = new byte[SaltSize + HashSize];
			Buffer.BlockCopy(salt, 0, dst, 0, SaltSize);
			Buffer.BlockCopy(buffer2, 0, dst, SaltSize, HashSize);

			return Convert.ToBase64String(dst);
		}

		/// <summary>
		/// Verifies a password against a hash.
		/// </summary>
		/// <param name="password">The password.</param>
		/// <param name="hashedPassword">The hash.</param>
		/// <returns>Could be verified?</returns>
		public static bool VerifyHashedPassword(string hashedPassword, string password)
		{
			byte[] buffer4;
			if (hashedPassword == null)
			{
				return false;
			}
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			byte[] src = Convert.FromBase64String(hashedPassword);
			if ((src.Length != SaltSize + HashSize))
			{
				return false;
			}

			byte[] salt = new byte[SaltSize];
			Buffer.BlockCopy(src, 0, salt, 0, SaltSize);
			byte[] hash = new byte[HashSize];
			Buffer.BlockCopy(src, SaltSize, hash, 0, HashSize);

			using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
			{
				buffer4 = bytes.GetBytes(HashSize);
			}

			return ByteArraysEqual(hash, buffer4);
		}

		private static bool ByteArraysEqual(byte[] b1, byte[] b2)
		{
			if (b1 == b2) return true;
			if (b1 == null || b2 == null) return false;
			if (b1.Length != b2.Length) return false;
			for (int i = 0; i < b1.Length; i++)
			{
				if (b1[i] != b2[i]) return false;
			}
			return true;
		}
	}


}
