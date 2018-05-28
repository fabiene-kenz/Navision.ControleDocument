using Navision.WebApi.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Navision.WebApi.Helpers
{
    public  class CryptDecrypt
    {
        public static string Decrypt(string word)
        {
            byte[] bytesToBeDecrypted = Convert.FromBase64String(word);
            var wordDecrypted = Encoding.UTF8.GetString(GenerationToken.AES_Decrypt(bytesToBeDecrypted));
            return wordDecrypted;
        }
    }
}