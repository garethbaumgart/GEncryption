namespace GEncryption
{
    public interface IEncryptionService
    {
        string Encrypt(string toEncrypt, string encryptionKey, string salt);
        string Decrypt(string toDecrypt, string encryptionKey, string salt);
    }
}