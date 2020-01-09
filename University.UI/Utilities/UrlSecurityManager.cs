using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace University.UI.Utilities
{
    public static class UrlSecurityManager
    {
        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string Encrypt(string plainText, string passPhrase)
        {
            string encriptedString = "";
            try
            {
                string EncrptKey = passPhrase;
                byte[] byKey = { };
                byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
                byKey = System.Text.Encoding.UTF8.GetBytes(EncrptKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return encriptedString = Convert.ToBase64String(ms.ToArray());
            }
            catch(Exception ex)
            {
               // ErrorLogger.Logger(ex, null);
            }
            return encriptedString;
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            try
            {
                cipherText = cipherText.Replace(" ", "+");
                string DecryptKey = passPhrase;
                byte[] byKey = { };
                byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
                byte[] inputByteArray = new byte[cipherText.Length];

                byKey = System.Text.Encoding.UTF8.GetBytes(DecryptKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(cipherText.Replace(" ", "+"));
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch
            {
                return "Invalid";
            }
        }
        
    }
}