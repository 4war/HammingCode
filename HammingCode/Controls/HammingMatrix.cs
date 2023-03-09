using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace HammingCode.Controls
{
    public partial class HammingMatrix : MatrixWithControlBits
    {
        public HammingMatrix(int lenght, string label, Controller controller) : base(lenght, label, controller)
        {
            Location = new Point(x: Bit.Static.MarginX,
                y: 2 * Bit.Static.MarginY + Bit.Static.Label.Height + Bit.Static.Height);
            InitializeComponent();
        }
    }
}