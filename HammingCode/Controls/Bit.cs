using System;
using System.Drawing;
using System.Windows.Forms;

namespace HammingCode.Controls
{
    public partial class Bit : Button
    {
        private readonly Color _color;

        public Bit() : this(Color.White)
        {
        }

        public Bit(Color color)
        {
            Click += (sender, args) =>
            {
                Text = Text == "0" ? "1" : "0";
            };
            _color = color;
            InitializeComponent();
        }

        public void SetValue(bool value) => Text = value ? "1" : "0";
    }
}