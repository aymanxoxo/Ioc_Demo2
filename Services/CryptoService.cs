using Interfaces;
using System;
using System.Text;
using System.Security.Cryptography;

namespace Services
{
    public class CryptoService : ICryptoService
    {
        public string Decrypt(string encryptedString)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedString);
            var rsa = GetRsaProvider();
            var decryptedBytes = rsa.Decrypt(encryptedBytes, true);
            return Encoding.UTF8.GetString(decryptedBytes);
        }

        public string Encrypt(string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            //var rsa = new RSACryptoServiceProvider();
            //rsa.FromXmlString(GetPublickKey());
            var encryptedBytes = GetRsaProvider().Encrypt(bytes, true);
            return Convert.ToBase64String(encryptedBytes);
        }

        public string GetPublickKey()
        {
            //DeleteCert();
            return GetRsaProvider().ToXmlString(false);
        }

        private RSACryptoServiceProvider GetRsaProvider()
        {
            var rsaParam = new CspParameters();
            rsaParam.KeyContainerName = "crossRsa";
            rsaParam.Flags = CspProviderFlags.UseMachineKeyStore;
            return new RSACryptoServiceProvider(2048, rsaParam);
        }

        private void DeleteCert()
        {
            var rsa = GetRsaProvider();
            rsa.PersistKeyInCsp = false;
            rsa.Clear();
        }
    }
}
