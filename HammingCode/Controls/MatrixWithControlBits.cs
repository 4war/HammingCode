using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HammingCode.Controls
{
    public partial class MatrixWithControlBits : Matrix
    {
        public List<Bit> ControlBits { get; }
        public static class Static
        {
            public static Color ControlBitColor = Color.FromArgb(178, 216, 255);
        }
        
        public MatrixWithControlBits(int lenght, string label, Controller controller) : base(lenght, label, controller)
        {
            InitializeComponent();
            foreach (var bit in Bits)
            {
                bit.Enabled = false;
            }
            
            ControlBits = new List<int> { 0, 1, 3, 7 }.Select(x => Bits[x]).ToList();
            foreach (var controlBit in ControlBits)
            {
                controlBit.BackColor = Static.ControlBitColor;
            }
        }
    }
}