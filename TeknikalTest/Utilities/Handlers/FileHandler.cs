namespace TeknikalTest.Utilities.Handlers
{
    public class FileHandler
    {
        public static void DeleteFileIfExist(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
