using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

using _2_LibraryUtils.Converters;

namespace _3_LibraryServicesNet10.Services.Crypto; 

public class AesGcmCryptoService : ICryptoService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new C_JsonConverter() }
    };

    /// <summary>
    /// Descifra el contenido de un archivo .gdc (soporta formato PBKDF2 actual y legado AES-GCM)
    /// </summary>
    public string DecryptFileContent(string encryptedBase64Content, string password)
    {
        if (string.IsNullOrWhiteSpace(encryptedBase64Content))
            return string.Empty;

        // 1. Decodificar el Base64 exterior a bytes
        byte[] jsonBytes = Convert.FromBase64String(encryptedBase64Content.Trim());

       
            // 3. Fallback: Intentar descifrado con el estándar antiguo (AES-GCM directo con SHA-256)
            try
            {
                var aesGcmDto = JsonSerializer.Deserialize<AesGcmPayload>(jsonBytes, JsonOptions);
                if (aesGcmDto != null && aesGcmDto.Iv.Length > 0)
                {
                    return DecryptAesGcmLegacy(aesGcmDto.Data, aesGcmDto.Iv, password);
                }
            }
            catch (Exception ex2)
            {
                throw new CryptographicException("AesGcmCryptoService: Error to decrypt file content.");
            }               

    }

    /// <summary>
    /// Cifra texto plano usando el estándar actual PBKDF2 + AES-GCM de la web
    /// </summary>
    public string EncryptFileContent(string plainText, string password)
    {
        if (string.IsNullOrEmpty(plainText))
            return string.Empty;

        //byte[] salt = RandomNumberGenerator.GetBytes(AppConstants.Crypto.SaltSizeBytes); // 16 bytes
        byte[] iv = RandomNumberGenerator.GetBytes(AppConstants.Crypto.IvSizeBytes);     // 12 bytes
        //byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        // Derivar clave de 32 bytes (256 bits) usando el método estático de .NET 10
        //byte[] key = Rfc2898DeriveBytes.Pbkdf2(
        //    passwordBytes,
        //    salt,
        //    AppConstants.Crypto.Pbkdf2Iterations,
        //    HashAlgorithmName.SHA256,
        //    AppConstants.Crypto.KeySizeBytes
        //);

        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
        byte[] cipherText = new byte[plainBytes.Length];
        byte[] tag = new byte[AppConstants.Crypto.TagSizeBytes]; // 16 bytes

        using (var aesGcm = new AesGcm(key, AppConstants.Crypto.TagSizeBytes))
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

    private string DecryptPbkdf2(byte[] cipherTextWithTag, byte[] iv, byte[] salt, string password)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        // Usando el método estático optimizado de .NET 10
        byte[] key = Rfc2898DeriveBytes.Pbkdf2(
            passwordBytes,
            salt,
            AppConstants.Crypto.Pbkdf2Iterations,
            HashAlgorithmName.SHA256,
            AppConstants.Crypto.KeySizeBytes
        );

        return DecryptAesGcm(cipherTextWithTag, key, iv);
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