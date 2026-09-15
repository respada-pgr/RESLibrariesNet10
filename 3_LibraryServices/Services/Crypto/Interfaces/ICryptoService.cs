namespace _3_LibraryServicesNet10.Services.Crypto
{
    public interface ICryptoService
    {
        string DecryptFileContent(string base64Content, string password);

        string EncryptFileContent(string plainText, string password);
    }
}
