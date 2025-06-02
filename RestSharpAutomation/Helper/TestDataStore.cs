using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RestSharpAutomation.Helper
{
    public static class TestDataStore
    {
        private static readonly string projectRoot;
        private static readonly string directoryPath;
        private static readonly string filePath;

        static TestDataStore()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            projectRoot = Directory.GetParent(baseDir)!.Parent!.Parent!.FullName;
            directoryPath = Path.Combine(projectRoot, "TestData");
            filePath = Path.Combine(directoryPath, "userId.txt");
        }

        public static void SaveUserId(int userId)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            File.WriteAllText(filePath, userId.ToString());
        }

        public static int LoadUserId()
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"User ID file not found at {filePath}");
            return int.Parse(File.ReadAllText(filePath));
        }
    }

}
