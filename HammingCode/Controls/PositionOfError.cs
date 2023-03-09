using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HammingCode.Controls
{
    public partial class PositionOfErrorMatrix : Matrix
    {
        public PositionOfErrorMatrix(int lenght, string label, Controller controller) : base(lenght, label, controller)
        {
            Location = new Point(
                x: Bit.Static.MarginX + 8 * (Bit.Static.Width + Bit.Static.MarginX),
                y: 5 * (Bit.Static.MarginY + Bit.Static.Label.Height + Bit.Static.Height) + Bit.Static.MarginY
                );
            
            InitializeComponent();
            foreach (var bit in Bits)
            {
                bit.Enabled = false;
            }
        }
    }
}