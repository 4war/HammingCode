using System;
using System.Drawing;
using System.Windows.Forms;

namespace HammingCode.Controls
{
    public partial class InputWordMatrix : Matrix
    {
        private event EventHandler Listener;

        public InputWordMatrix(int lenght, string label, Controller controller) : base(lenght, label, controller)
        {
            Location = new Point(
                x: Bit.Static.MarginX + 4 * (Bit.Static.Width + Bit.Static.MarginX),
                y: Bit.Static.MarginY
                );
            
            Listener = (sender, args) =>
            {
                controller.UpdateHammingCode();
            };
            
            foreach (var bit in Bits)
            {
                bit.Click += Listener;
            }
            
            InitializeComponent();
        }
    }
}