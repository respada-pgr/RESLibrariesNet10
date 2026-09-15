using _2_LibraryUtils.Converters;
using _3_LibraryServicesNet10.Services.Crypto.Models;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace _3_LibraryServicesNet10.Services.Crypto; 

public class Pbkdf2CryptoService : ICryptoService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new C_JsonConverter() }
    };

    /// <summary>
    /// Descifra el contenido de un archivo .gdc (soporta formato PBKDF2 actual y legado AES-GCM)
    /// </summary>
    public string DecryptFileContent(string encryptedBase64Content, string password, Pbkdf2CryptoConfig config = new())
    {
        if (string.IsNullOrWhiteSpace(encryptedBase64Content))
            return string.Empty;

        byte[] jsonBytes = Convert.FromBase64String(encryptedBase64Content.Trim());

        try
        {
            var pbkdf2Dto = JsonSerializer.Deserialize<Pbkdf2Payload>(jsonBytes, JsonOptions);
            if (pbkdf2Dto != null && pbkdf2Dto.Salt.Length > 0)
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                byte[] key = Rfc2898DeriveBytes.Pbkdf2(
                    passwordBytes,
                    pbkdf2Dto.Salt,
                    config.Pbkdf2Iterations,
                    HashAlgorithmName.SHA256,
                    config.KeySizeBytes
                );

                return DecryptAesGcm(pbkdf2Dto.Data, key, pbkdf2Dto.Iv);
            }            
        }
        catch (Exception ex) 
        {
            throw new CryptographicException("AesGcmCryptoService: Error to decrypt file content.");
        }        

        throw new CryptographicException("AesGcmCryptoService: Error to decrypt file content, format invalid or data corrupt.");
    }

    /// <summary>
    /// Cifra texto plano usando el estándar actual PBKDF2 + AES-GCM de la web
    /// </summary>
    public string EncryptFileContent(string plainText, string password, Pbkdf2CryptoConfig config = new())
    {
        if (string.IsNullOrEmpty(plainText))
            return string.Empty;

        byte[] salt = RandomNumberGenerator.GetBytes(config.SaltSizeBytes); // 16 bytes
        byte[] iv = RandomNumberGenerator.GetBytes(config.IvSizeBytes);     // 12 bytes
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        // Derivar clave de 32 bytes (256 bits) usando el método estático de .NET 10
        byte[] key = Rfc2898DeriveBytes.Pbkdf2(
            passwordBytes,
            salt,
            config.Pbkdf2Iterations,
            HashAlgorithmName.SHA256,
            config.KeySizeBytes
        );

        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
        byte[] cipherText = new byte[plainBytes.Length];
        byte[] tag = new byte[config.TagSizeBytes]; // 16 bytes

        using (var aesGcm = new AesGcm(key, config.TagSizeBytes))
        {
            aesGcm.Encrypt(iv, plainBytes, cipherText, tag);
        }

        // En JS, 'data' contiene ciphertext + tag concatenados
        byte[] cipherTextWithTag = new byte[cipherText.Length + tag.Length];
        Buffer.BlockCopy(cipherText, 0, cipherTextWithTag, 0, cipherText.Length);
        Buffer.BlockCopy(tag, 0, cipherTextWithTag, cipherText.Length, tag.Length);

        // Crear JSON idéntico al de JS: { salt: [...], iv: [...], data: [...] }
        var payload = new Pbkdf2Payload
        {
            Salt = salt,
            Iv = iv,
            Data = cipherTextWithTag
        };

        string jsonPayload = JsonSerializer.Serialize(payload);
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonPayload));
    }


    private string DecryptAesGcmLegacy(byte[] cipherTextWithTag, byte[] iv, string password)
    {
        // En el formato legacy AES-GCM, la clave es directamente SHA256(password)
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] key = SHA256.HashData(passwordBytes);

        return DecryptAesGcm(cipherTextWithTag, key, iv);
    }



    private string DecryptAesGcm(byte[] cipherTextWithTag, byte[] key, byte[] iv)
    {
        int tagSize = AppConstants.Crypto.TagSizeBytes; // 16 bytes
        int cipherTextSize = cipherTextWithTag.Length - tagSize;

        if (cipherTextSize < 0)
            throw new CryptographicException("El payload cifrado es demasiado corto.");

        byte[] cipherText = new byte[cipherTextSize];
        byte[] tag = new byte[tagSize];

        // Separar el Ciphertext del Tag de autenticación
        Buffer.BlockCopy(cipherTextWithTag, 0, cipherText, 0, cipherTextSize);
        Buffer.BlockCopy(cipherTextWithTag, cipherTextSize, tag, 0, tagSize);

        byte[] decryptedBytes = new byte[cipherTextSize];

        using (var aesGcm = new AesGcm(key, tagSize))
        {
            aesGcm.Decrypt(iv, cipherText, tag, decryptedBytes);
        }

        return Encoding.UTF8.GetString(decryptedBytes);
    }
}