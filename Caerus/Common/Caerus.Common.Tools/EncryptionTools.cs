using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Tools
{
    public static class EncryptionTools
    {
        public static string GenerateSha256Hash(string password)
        {
            var crypt = new SHA256Managed();
            var hash = String.Empty;
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            return crypto.Aggregate(hash, (current, bit) => current + bit.ToString("x2"));
        }

        public static string GenerateSha512Hash(string password)
        {
            var crypt = new SHA512Managed();
            var hash = String.Empty;
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            return crypto.Aggregate(hash, (current, bit) => current + bit.ToString("x2"));
        }

        public static string GenerateMd5Hash(string password)
        {
            var crypt = new MD5CryptoServiceProvider();
            var hash = String.Empty;
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            return crypto.Aggregate(hash, (current, bit) => current + bit.ToString("x2"));
        }

        /// <summary>
        /// Uses SHA1 to compute a hash value from 2 <see cref="Byte"/> arrays
        /// </summary>
        /// <param name="data">The data value</param>
        /// <param name="salt">The salt value</param>
        /// <returns>A hashed value as a <see cref="Byte"/> array</returns>
        private static byte[] ComputeHash(byte[] data, byte[] salt)
        {
            var combinedArray = new byte[data.Length + salt.Length];
            Array.Copy(data, combinedArray, data.Length);
            Array.Copy(salt, 0, combinedArray, data.Length, salt.Length);


            return SHA1.Create().ComputeHash(combinedArray);

        }

        /// <summary>
        /// Compares a computed hash value of the supplied data and salt keys
        /// with a supplied hash value, for equality
        /// </summary>
        /// <param name="data">The data value</param>
        /// <param name="hash">The hashed value</param>
        /// <param name="salt">The salt value</param>
        /// <returns>True if the hash value matches, else False</returns>
        private static bool VerifyHash(byte[] data, byte[] hash, byte[] salt)
        {
            var newHash = ComputeHash(data, salt);

            //if the newly hashed value+salt is not the same length as supplied hash, no match
            if (newHash.Length != hash.Length)
                return false;

            for (int i = 0; i < hash.Length; i++)
            {
                if (!hash[i].Equals(newHash[i]))
                    return false;
            }

            //all array items match, so hash matches
            return true;
        }

        /// <summary>
        /// Compares a computed hash value of the supplied data and salt keys
        /// with a supplied hash value, for equality
        /// </summary>
        /// <param name="hashObject"> </param>
        /// <param name="hash">The hashed value</param>
        /// <param name="salt">The salt value</param>
        /// <returns>True if the hash value matches, else False</returns>
        public static bool VerifyHash(object hashObject, string hash, string salt)
        {
            var saltByteValue = GetByteArray(salt);
            var dataByteValue = GetByteArray(GetValueToHash(hashObject));
            var hashedByteValue = Convert.FromBase64String(hash);

            return VerifyHash(dataByteValue, hashedByteValue, saltByteValue);
        }

        /// <summary>
        /// Extracts the values from an object as a concatenated string,
        /// using only the public properties with non-null values
        /// </summary>
        /// <param name="hashObject">The object from which the values are to be extracted</param>
        /// <returns>A string value containing each property value concatenated, 
        /// sorted alphabetically in ascending order of the property names</returns>
        private static string GetValueToHash(object hashObject)
        {
            var valueToHash = string.Empty;
            var propertiesToHash = hashObject
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => !p.GetIndexParameters().Any())
                .Where(p => p.CanRead && p.CanWrite)
                .OrderBy(p => p.Name)
                .ToList();

            foreach (var propertyInfo in propertiesToHash)
            {
                var value = propertyInfo.GetValue(hashObject);
                if (value != null)
                {
                    valueToHash += value;
                }
            }

            return valueToHash;
        }

        /// <summary>
        /// Converts a string value to a binary value
        /// </summary>
        /// <param name="value">The <see cref="string"/> value to be converted</param>
        /// <returns>A <see cref="Byte"/> array containing the data</returns>
        private static byte[] GetByteArray(string value)
        {
            return Encoding.ASCII.GetBytes(value);
        }

        /// <summary>
        /// Hashes the supplied object's public & non-null properties in ascending order,
        /// using the supplied salt key
        /// </summary>
        /// <param name="objectToHash">The object with the values to hash</param>
        /// <param name="saltCode">The salt key</param>
        /// <returns>A Base 64 encoded string of the hash value</returns>
        public static string ComputeHashValue(object objectToHash, string saltCode)
        {
            var saltByteValue = GetByteArray(saltCode);
            var dataByteValue = GetByteArray(GetValueToHash(objectToHash));

            var hashedValue = ComputeHash(dataByteValue, saltByteValue);

            return Convert.ToBase64String(hashedValue);
        }
    }
}
