using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace MapEditor.Forms
{
    public partial class DropDownDialog : Form
    {
        public DropDownDialog()
        {
            InitializeComponent();
        }

        public DropDownDialog(String _displayText, String[] mapFiles)
        {
            InitializeComponent();
            this.questionLabel.Text = _displayText;
            this.comboBox1.Items.AddRange(mapFiles);
        }

        public DropDownDialog(bool _isMap,String _displayText)
        {
            InitializeComponent();
            if (_isMap)
            {
                String mapDirectory = Directory.GetCurrentDirectory() + @"\Content\Maps\";
                String[] mapFiles = Directory.GetFiles(mapDirectory, "*.gmap");
                foreach(String item in mapFiles)
                {
                    this.comboBox1.Items.Add(item.Replace(mapDirectory,""));
                }
                
                this.questionLabel.Text = _displayText;
            }
        }

        public DropDownDialog(String _displayText)
        {
            InitializeComponent();
            this.questionLabel.Text = _displayText;
        }

        public String GetField()
        {
            if(this.comboBox1.SelectedItem == null)
                return "";
            return this.comboBox1.SelectedItem.ToString();
        }
    }
}
