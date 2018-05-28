using Navision.ControleDocuments.Controllers.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Navision.ControleDocuments.Controllers.Helpers
{
    public static class Utils
    {
       

        public static string SerializeToJson(object obj)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj);
                return json;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static T DeserializeFromJson<T>(string jsonObj)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<T>(jsonObj);
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

       public static byte[] EncryptStringToBytes_Aes(string plainText)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(ConstantsValues.Password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            
            byte[] encrypted;
            // Create an AesManaged object
            // with the specified key and IV.
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.KeySize = 256;
                aesAlg.BlockSize = 128;

                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);
                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(ConstantsValues.Password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

    }
}
