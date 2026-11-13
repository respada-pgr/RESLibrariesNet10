namespace _3_LibraryServices.Crypto
{
    public interface ICryptoService
    {
        string DecryptFileContent(string base64Content, string password);

        string EncryptFileContent(string plainText, string password);
    }
}
