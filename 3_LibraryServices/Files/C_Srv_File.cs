using System.Text;
using _1_LibraryClasses.File;

namespace _3_LibraryServices.Files;

/// <summary>
/// File service that delegates I/O to <see cref="C_Hlp_File"/> / <see cref="C_Hlp_FileTextAsync"/>.
/// <para><i>[ES] Servicio de ficheros que delega el I/O en <see cref="C_Hlp_File"/> / <see cref="C_Hlp_FileTextAsync"/>.</i></para>
/// </summary>
public class C_Srv_File : I_FileService
{
    /// <inheritdoc />
    public async Task<string> ReadFileAsync(string filePath)
    {
        string? text = await C_Hlp_FileTextAsync.ReadAsync(filePath, Encoding.UTF8).ConfigureAwait(false);
        if (text is null)
            throw new FileNotFoundException($"El archivo no existe en la ruta: {filePath}", filePath);
        return text;
    }

    /// <inheritdoc />
    public Task WriteFileAsync(string filePath, string content)
        => C_Hlp_FileTextAsync.WriteAsync(content, filePath, Encoding.UTF8);

    /// <inheritdoc />
    public bool FileExists(string filePath)
        => C_Hlp_File.Exists(filePath);
}
