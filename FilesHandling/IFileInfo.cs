namespace FilesHandling
{
    public interface IFileInfo
    {
        string FindLongestFileName(string path);
        string FindBiggestFiles(string path, int amount);
        string FindBiggestGroupsFiles(string path, int interval);
        string AllocationFilesExtension(string path);
    }
}