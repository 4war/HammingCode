using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HammingCode.Controls
{
    public partial class FixedMessageMatrix : MatrixWithControlBits
    {
        public FixedMessageMatrix(int lenght, string label, Controller controller) : base(lenght, label, controller)
        {
            Location = new Point(x: Bit.Static.MarginX,
                y: 6 * (Bit.Static.MarginY + Bit.Static.Label.Height + Bit.Static.Height) + Bit.Static.MarginY);

            InitializeComponent();
        }
    }
}