using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;

namespace Cleared.Droid.Engine
{
    public class ResourceUtil : IResourceUtil
    {
        Context context;

        public ResourceUtil(Context ctx)
        {
            context = ctx;
        }

        public string Read(string folderName, string resourceName)
        {
            int id = context.Resources.GetIdentifier(
                resourceName.Replace(".json","").ToLower(), 
                "raw", context.PackageName);

            using (var stream = context.Resources.OpenRawResource(id))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        public T Read<T>(string foldername, string resourceName)
        {
            var json = Read(foldername, resourceName);

            var value = json.FromJson<T>();

            return value;
        }
    }
}