using System.Drawing;
using System.Windows.Forms;

namespace HammingCode.Controls
{
    public partial class DecodedWordMatrix : Matrix
    {
        public DecodedWordMatrix(int lenght, string label, Controller controller) : base(lenght, label, controller)
        {
            Location = new Point(
                x: Bit.Static.MarginX + 4 * (Bit.Static.Width + Bit.Static.MarginX),
                7 * (Bit.Static.MarginY + Bit.Static.Label.Height + Bit.Static.Height) + Bit.Static.MarginY
                );

            foreach (var bit in Bits)
            {
                bit.Enabled = false;
            }

            InitializeComponent();
        }
    }
}