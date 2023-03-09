using System;
using System.Drawing;
using System.Windows.Forms;

namespace HammingCode
{
    public partial class Bit : Button
    {
        private readonly Color _color;
        public Bit() : this(Color.White)
        {
            
        }

        public Bit(Color color)
        {
            _color = color;
            InitializeComponent();
        }

        protected override void OnClick(EventArgs e)
        {
            Text = Text == "0" ? "1" : "0";
            //BackColor = Text == "1" ? HammingMatrix.Static.ControlBitColor : Color.White;
        }

        public void SetValue(bool value)
        {
            Text = value ? "1" : "0";
        }
    }
}