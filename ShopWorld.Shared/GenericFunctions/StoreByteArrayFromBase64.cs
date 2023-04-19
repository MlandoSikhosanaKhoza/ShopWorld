using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWorld.Shared
{
    public static class StoreByteArrayFromBase64
    {
        public static string Execute(string Base64,string DirectoryPath,string Extension)
        {
            byte[] byteArray = Convert.FromBase64String(Base64);
            string imagestore = Guid.NewGuid().ToString();
            string imageName = $"{imagestore}{Extension}";
            string path = Path.Combine(DirectoryPath,imageName);
            System.IO.File.WriteAllBytes(path, byteArray);
            return path;
        }
    }
}
