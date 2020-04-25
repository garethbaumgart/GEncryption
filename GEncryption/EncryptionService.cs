using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GEncryption
{
    public class EncryptionService : IEncryptionService
    {
        public string Encrypt(string toEncrypt, string encryptionKey, string salt)
        {
            byte[] toEncryptBytes = Encoding.Unicode.GetBytes(toEncrypt);
            byte[] saltBytes = Encoding.Unicode.GetBytes(salt);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, saltBytes);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(toEncryptBytes, 0, toEncryptBytes.Length);
                        cs.Close();
                    }
                    toEncrypt = Convert.ToBase64String(ms.ToArray());
                }
            }
            return toEncrypt;
        }
        public string Decrypt(string toDecrypt, string encryptionKey, string salt)
        {
            toDecrypt = toDecrypt.Replace(" ", "+");
            byte[] saltBytes = Encoding.Unicode.GetBytes(salt);
            byte[] toDecryptBytes = Convert.FromBase64String(toDecrypt);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, saltBytes);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(toDecryptBytes, 0, toDecryptBytes.Length);
                        cs.Close();
                    }
                    toDecrypt = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return toDecrypt;
        }
    }
}
