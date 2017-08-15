using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Forms.Ext
{
    class ComboBoxItem
    {
        public String Text { get; set; }
        public Object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
