using System;
using System.Threading.Tasks;

namespace Cleared
{
    public interface IResourceUtil
    {
        string Read(string folderName, string resourceName);
        T Read<T>(string foldername, string resourceName);
    }

    public static class ResourceUtil
    {
        public static IResourceUtil Impl { get; set; }
    }
}
