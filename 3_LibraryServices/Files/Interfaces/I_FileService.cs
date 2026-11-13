namespace _3_LibraryServices.Files;

/// <summary>
/// Contract for basic file operations used by applications.
/// <para><i>[ES] Contrato de operaciones básicas de fichero para aplicaciones.</i></para>
/// </summary>
public interface I_FileService
{
    /// <summary>
    /// Reads the entire UTF-8 file asynchronously. Throws if the file does not exist.
    /// <para><i>[ES] Lee todo el fichero UTF-8 de forma asíncrona. Lanza si no existe.</i></para>
    /// </summary>
    Task<string> ReadFileAsync(string filePath);

    /// <summary>
    /// Writes or overwrites a UTF-8 text file asynchronously.
    /// <para><i>[ES] Escribe o sobrescribe un fichero de texto UTF-8 de forma asíncrona.</i></para>
    /// </summary>
    Task WriteFileAsync(string filePath, string content);

    /// <summary>
    /// Returns whether the file exists.
    /// <para><i>[ES] Indica si el fichero existe.</i></para>
    /// </summary>
    bool FileExists(string filePath);
}
