using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExecutePY
{
    public class CustomTextBox : TextBox
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public CustomTextBox(string name, string text, int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
        }
    }
}
