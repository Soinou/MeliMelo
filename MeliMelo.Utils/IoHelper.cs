using System.IO;

namespace MeliMelo.Utils
{
    public static class IoHelper
    {
        public static void Append(string path, string content)
        {
            EnsureDirectory(path);

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.Write(content);
            }
        }

        public static void EnsureDirectory(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(Path.GetDirectoryName(path));

            if (!directory.Exists)
            {
                directory.Create();
            }
        }

        public static string Read(string path)
        {
            EnsureDirectory(path);

            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static void Write(string path, string content)
        {
            EnsureDirectory(path);

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.Write(content);
            }
        }
    }
}
