using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using System.Text;
using JIF.Core.Security;

namespace JIF.Core.Test
{
    [TestClass]
    public class EncyptHelper_Test
    {
        [TestMethod]
        public void MD5_Test()
        {
            Console.WriteLine(EncyptHelper.Encrypt(MD5.Create(), "1"));

            Console.WriteLine(EncyptHelper.Encrypt_MD5("1"));

            Console.WriteLine(EncyptHelper.IsHashMatch(MD5.Create(), "c4ca4238a0b923820dcc509a6f75849b", "1"));
        }
    }


}
