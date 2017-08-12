using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEditor.Forms
{
    public partial class TextboxDialog : Form
    {
        public TextboxDialog()
        {
            InitializeComponent();
        }

        public TextboxDialog(String _displayText)
        {
            this.questionLabel.Text = _displayText;
        }

        public String GetField()
        {
            return this.textBox1.Text;
        }
    }
}
