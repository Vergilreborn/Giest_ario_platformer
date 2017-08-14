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
using MapEditor.Forms.Ext;

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

        public DropDownDialog(String _fileExt, String _objectType, String _displayText)
        {
            InitializeComponent();
            String mapDirectory = Directory.GetCurrentDirectory() + $@"\Content\{_objectType}\";
            String[] mapFiles = Directory.GetFiles(mapDirectory, $"*{_fileExt}");
            foreach(String item in mapFiles)
            {
                //item.Replace(mapDirectory, "")
                this.comboBox1.Items.Add(new ComboBoxItem() { Text = item.Replace(mapDirectory, ""),
                                                              Value = item});
            }
                
            this.questionLabel.Text = _displayText;
           
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
