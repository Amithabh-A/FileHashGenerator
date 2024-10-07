using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class Program
{
  static void Main(string[] args)
  {
    if (args.Length != 1)
    {
      Console.WriteLine("Usage: dotnet run <directory_path>");
      return;
    }

    string directoryPath = args[0];

    if (!Directory.Exists(directoryPath))
    {
      Console.WriteLine($"Directory does not exist: {directoryPath}");
      return;
    }

    foreach (var filePath in Directory.GetFiles(directoryPath))
    {
      string hash = GetFileHash(filePath);
      Console.WriteLine($"{Path.GetFileName(filePath)}: {hash}");
    }
  }

  static string GetFileHash(string filePath)
  {
    using (var sha256 = SHA256.Create())
    using (var stream = File.OpenRead(filePath))
    {
      byte[] hashBytes = sha256.ComputeHash(stream);
      return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }
  }
}

