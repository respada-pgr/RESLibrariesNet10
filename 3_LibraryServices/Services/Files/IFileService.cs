namespace GdcWinApp.Services.Files;

public interface IGdcFileService
{
    string GeneratePrefix(int length = 8);
    Task<string> ReadFileAsync(string filePath);
    Task WriteFileAsync(string filePath, string content);
    bool FileExists(string filePath);
}