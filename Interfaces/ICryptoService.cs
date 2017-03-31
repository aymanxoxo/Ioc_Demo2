namespace Interfaces
{
    public interface ICryptoService
    {
        string Decrypt(string encryptedString);

        string Encrypt(string data);

        string GetPublickKey();
    }
}
