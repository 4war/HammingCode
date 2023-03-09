using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HammingCode.Controls
{
    public partial class MessageMatrix : MatrixWithControlBits
    {
        public MessageMatrix(int lenght, string label, Controller controller) : base(lenght, label, controller)
        {
            Location = new Point(x: Bit.Static.MarginX,
                y: 4 * (Bit.Static.MarginY + Bit.Static.Label.Height + Bit.Static.Height) + Bit.Static.MarginY);
            
            InitializeComponent();
        }
    }
}