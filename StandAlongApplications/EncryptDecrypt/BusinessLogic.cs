using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EncryptDecrypt
{
    class BusinessLogic
    {
        public static string Encrypt(string plainText, string key, string initVector, int keyBitSize = 256)
        {
            byte[] bytes, rgbKey;

            try
            {
                bytes = Convert.FromBase64String(initVector);
            }
            catch (Exception e)
            {
                throw new Exception("Init Vectory - " + e.Message, e);
            }
            try
            {
                rgbKey = Convert.FromBase64String(key);
            }
            catch (Exception e)
            {
                throw new Exception("Key - " + e.Message, e);
            }
            byte[] buffer4 = Encoding.UTF8.GetBytes(plainText);

            using (RijndaelManaged transform = new RijndaelManaged())
            {
                transform.Mode = CipherMode.CBC;
                using (MemoryStream stream2 = new MemoryStream())
                using (CryptoStream stream = new CryptoStream(stream2, transform.CreateEncryptor(rgbKey, bytes),
                    CryptoStreamMode.Write))
                {
                    stream.Write(buffer4, 0, buffer4.Length);
                    stream.FlushFinalBlock();
                    byte[] inArray = stream2.ToArray();
                    return Convert.ToBase64String(inArray);
                }
            }
        }
        public static string Decrypt(string cipherText, string key, string initVector, int keySize = 256)
        {
            byte[] bytes, rgbKey;

            try
            {
                bytes = Convert.FromBase64String(initVector);
            }
            catch (Exception e)
            {
                throw new Exception("Init Vectory - " + e.Message, e);
            }
            try
            {
                rgbKey = Convert.FromBase64String(key);
            }
            catch (Exception e)
            {
                throw new Exception("Key - " + e.Message, e);
            }
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (RijndaelManaged transform = new RijndaelManaged())
            {
                transform.Mode = CipherMode.CBC;
                using (MemoryStream stream2 = new MemoryStream(buffer))
                using (CryptoStream stream = new CryptoStream(stream2, transform.CreateDecryptor(rgbKey, bytes),
                    CryptoStreamMode.Read))
                {
                    byte[] buffer4 = new byte[buffer.Length + 1];
                    int count = stream.Read(buffer4, 0, buffer4.Length);
                    return Encoding.UTF8.GetString(buffer4, 0, count);
                }
            }
        }
    }
}
