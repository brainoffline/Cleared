using System;
using System.Threading.Tasks;
using UIKit;

namespace Cleared.iOS.Engine
{
    public class ResourceUtil : IResourceUtil
    {
        public string Read(string folderName, string resourceName)
        {
            return System.IO.File.ReadAllText("Data/" + resourceName.ToLower());
        }

        public T Read<T>(string foldername, string resourceName)
        {
            var json = Read(foldername, resourceName);

            var value = json.FromJson<T>();

            return value;
        }
    }
}
