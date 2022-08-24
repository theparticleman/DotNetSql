namespace DotNetSql.Cli;

public interface IFileSystem
{
   string ReadAllText(string fileName);
}

public class FileSystem : IFileSystem
{
   public string ReadAllText(string fileName)
   {
      return File.ReadAllText(fileName);
   }
}