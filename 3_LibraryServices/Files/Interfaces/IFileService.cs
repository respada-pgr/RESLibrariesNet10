namespace _3_LibraryServicesNet10.Files;

public interface IGdcFileService
{
    string GeneratePrefix(int length = 8);
    Task<string> ReadFileAsync(string filePath);
    Task WriteFileAsync(string filePath, string content);
    bool FileExists(string filePath);
}