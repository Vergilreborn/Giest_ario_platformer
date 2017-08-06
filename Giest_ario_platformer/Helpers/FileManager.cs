using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Giest_ario_platformer.Helpers
{
    class FileManager<E>
    {

        private static String SerializeObject(E _obj)
        {
            return JsonConvert.SerializeObject(_obj);
        }

        private static E DeserializeObject(String _objStr)
        {
            return JsonConvert.DeserializeObject<E>(_objStr);
        }

        public static void SaveFile(String _fileName,E _obj)
        {
            File.WriteAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/{_fileName}.lvl", SerializeObject(_obj));
        }

        public static E LoadFile(String _filePath)
        {
            return DeserializeObject(File.ReadAllText(_filePath));
        }

    }
}
