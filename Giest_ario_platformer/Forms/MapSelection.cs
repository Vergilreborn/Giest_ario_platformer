using Giest_ario_platformer.Forms.Ext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Giest_ario_platformer.Forms
{
    public partial class MapSelection : Form
    {
        public MapSelection()
        {
            InitializeComponent();
            String mapDirectory = Directory.GetCurrentDirectory() + @"\Content\Map\";
            String[] mapFiles = Directory.GetFiles(mapDirectory, "*.gmap");
            foreach (String item in mapFiles)
            {
                //item.Replace(mapDirectory, "")
                this.comboBox1.Items.Add(new ComboBoxItem()
                {
                    Text = item.Replace(mapDirectory, ""),
                    Value = item
                });
            }

            this.questionLabel.Text = "Select Map:";

        }

        public String GetField()
        {
            if (this.comboBox1.SelectedItem == null)
                return "";
            return ((ComboBoxItem)this.comboBox1.SelectedItem).Value.ToString();
        }
        public String GetCleanText()
        {
            if (this.comboBox1.SelectedItem == null)
                return "";
            return ((ComboBoxItem)this.comboBox1.SelectedItem).Text.ToString();
        }
    }
}
