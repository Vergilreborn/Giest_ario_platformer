using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;
using MapEditor.Exceptions;
using MapEditor.Forms;
using MapEditor.Objects.MapObjects;

namespace MapEditor.Helpers
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

        public static void SaveFileGameObject(String _fileExt,String _folderName,E _obj)
        {
            using(TextboxDialog fDialog = new TextboxDialog($"Save {_fileExt}:"))
            {
                DialogResult dg =  fDialog.ShowDialog();
                if(dg == DialogResult.OK)
                {
                    String fileName = fDialog.GetField();
                    String currentDirectory = $@"{Directory.GetCurrentDirectory()}\Content\{_folderName}\";
                    if (!Directory.Exists(currentDirectory))
                    {
                        Directory.CreateDirectory(currentDirectory);
                    }
                    String filePath = currentDirectory +  fileName+ _fileExt;
                  
                    File.WriteAllText(filePath, SerializeObject(_obj));
                }
            }
        }

        public static void SaveFile(String _fileType,String _fileTypeName, E _obj)
        {
            using (SaveFileDialog fDialog = new SaveFileDialog())
            {
                fDialog.Filter = $"{_fileTypeName}| *.{_fileType}";
                fDialog.DefaultExt = _fileType;
                DialogResult dg = fDialog.ShowDialog();
                if (dg == DialogResult.OK)
                {
                    String filePath = fDialog.FileName;
                    File.WriteAllText(filePath, SerializeObject(_obj));
                }
            }

        }

        public static E LoadFileGameObject(string _fileExt, string _folderName)
        {
            using (DropDownDialog fDialog = new DropDownDialog(_fileExt, "Map", $"Load ({_fileExt})"))
            {

                DialogResult dg = fDialog.ShowDialog();
                if (dg == DialogResult.OK)
                {
                    String fileName = fDialog.GetField();
                    return DeserializeObject(File.ReadAllText(fileName));
                }
                throw new NoFileSelectedException();
            }

        }

        public static E LoadFile(String _fileType, String _fileTypeName)
        {
            
            using(OpenFileDialog fDialog = new OpenFileDialog())
            {
                fDialog.Filter = $"{_fileTypeName}| *.{_fileType}";
                fDialog.DefaultExt = _fileType;
                DialogResult dg = fDialog.ShowDialog();
                if (dg == DialogResult.OK)
                {
                    String filePath = fDialog.FileName;
                    return DeserializeObject(File.ReadAllText(filePath));
                }
                throw new NoFileSelectedException();    
            }

            
        }
    }
}
