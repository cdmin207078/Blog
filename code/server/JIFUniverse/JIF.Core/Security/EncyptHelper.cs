using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core.Security
{
    /// <summary>
    /// 加密算法类
    /// 参考文献: http://www.cnblogs.com/rush/archive/2011/07/24/2115613.html
    /// </summary>
    public static class EncyptHelper
    {

        /// <summary>
        /// Encrypts the specified hash algorithm.
        /// 1. Generates a cryptographic Hash Key for the provided text data.
        /// </summary>
        /// <param name="hashAlgorithm">The hash algorithm.</param>
        /// <param name="dataToHash">The data to hash.</param>
        /// <returns></returns>
        public static string Encrypt(HashAlgorithm hashAlgorithm, string dataToHash)
        {

            string[] tabStringHex = new string[16];
            UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            byte[] data = UTF8.GetBytes(dataToHash);
            byte[] result = hashAlgorithm.ComputeHash(data);
            StringBuilder hexResult = new StringBuilder(result.Length);

            for (int i = 0; i < result.Length; i++)
            {
                //// Convert to hexadecimal
                hexResult.Append(result[i].ToString("x2"));
            }
            return hexResult.ToString();
        }

        /// <summary>
        /// Determines whether [is hash match] [the specified hash algorithm].
        /// </summary>
        /// <param name="hashAlgorithm">hash 算法</param>
        /// <param name="hashedText">加密后的密文</param>
        /// <param name="unhashedText">原文</param>
        /// <returns>
        ///   <c>true</c> if [is hash match] [the specified hash algorithm]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsHashMatch(HashAlgorithm hashAlgorithm, string hashedText, string unhashedText)
        {
            string hashedTextToCompare = Encrypt(hashAlgorithm, unhashedText);
            return (string.Compare(hashedText, hashedTextToCompare, false) == 0);
        }


        /// <summary>
        /// 使用 MD5算法 加密字符串
        /// </summary>
        /// <param name="dataToHash"></param>
        /// <returns></returns>
        public static string Encrypt_MD5(string dataToHash)
        {
            return Encrypt(MD5.Create(), dataToHash);
        }
    }
}
