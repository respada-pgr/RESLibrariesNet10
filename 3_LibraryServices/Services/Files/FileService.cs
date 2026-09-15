using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GdcWinApp.Services.Files;

public class FileService : IGdcFileService
{
    /// <summary>
    /// Genera un prefijo aleatorio de N caracteres de longitud.
    /// Ej: "aB7kXpQz_"
    /// </summary>
    public string GeneratePrefix(int length = 8)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        char[] stringChars = new char[length];

        for (int i = 0; i < length; i++)
        {
            stringChars[i] = chars[Random.Shared.Next(chars.Length)];
        }

        return new string(stringChars) + "_";
    }

    /// <summary>
    /// Lee el contenido completo de un archivo codificado en UTF-8 de forma asíncrona.
    /// </summary>
    public async Task<string> ReadFileAsync(string filePath)
    {
        if (!FileExists(filePath))
        {
            throw new FileNotFoundException($"El archivo no existe en la ruta: {filePath}");
        }

        return await File.ReadAllTextAsync(filePath, Encoding.UTF8);
    }

    /// <summary>
    /// Escribe o sobrescribe un archivo con texto codificado en UTF-8 de forma asíncrona.
    /// </summary>
    public async Task WriteFileAsync(string filePath, string content)
    {
        string? directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await File.WriteAllTextAsync(filePath, content, Encoding.UTF8);
    }

    /// <summary>
    /// Comprueba si un archivo existe en el sistema.
    /// </summary>
    public bool FileExists(string filePath)
    {
        return File.Exists(filePath);
    }
}