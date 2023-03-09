using System.Drawing;
using System.Windows.Forms;

namespace HammingCode.Controls
{
    public partial class ErrorMatrix : Matrix
    {
        public ErrorMatrix(int lenght, string label, Controller controller) : base(lenght, label, controller)
        {
            Location = new Point(x: Bit.Static.MarginX,
                y: 2 * (Bit.Static.MarginY + Bit.Static.Label.Height + Bit.Static.Height) + Bit.Static.MarginY);

            foreach (var bit in Bits)
                bit.Click += (_, _) => controller.Update();
            
            InitializeComponent();
        }
    }
}