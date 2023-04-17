using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWorld.Shared
{
    public static class DeleteExistingFile
    {
        public static bool Execute(string Path)
        {
            if (File.Exists(Path))
            {
                File.Delete(Path);
                return true;
            }
            return false;
        }
    }
}
