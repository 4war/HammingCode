using System.ComponentModel;
using System.Drawing;

namespace HammingCode.Controls
{
    partial class Matrix
    {
        public static class Static
        {
            public static class Label
            {
                public static readonly int Width = 300;
            }
        }

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Size = new Size(
                Static.Label.Width + _length * (Bit.Static.Width + Bit.Static.MarginX) + Bit.Static.MarginX, 
                Bit.Static.Height + Bit.Static.MarginY + Bit.Static.Label.Height
                );

            var label = new System.Windows.Forms.Label()
            {
                Text = _label + ": ",
                Font = new Font(Font.FontFamily, 12f),
                Height = Bit.Static.Height,
                Width = Static.Label.Width,
                TextAlign = ContentAlignment.MiddleRight,
            };
            Controls.Add(label);
            
            var x = label.Width + Bit.Static.MarginX + (Bit.Static.Width + Bit.Static.MarginX) * (_length - 1);
            for (var i = 0; i < _length; i++)
            {
                var bit = new Bit()
                {
                    Location = new Point(x: x, y: Location.Y),
                };
                Bits[i] = bit;
                Controls.Add(bit);
                
                var microLabel = new System.Windows.Forms.Label()
                {
                    Text = (i+1).ToString(),
                    Location = new Point(x, Location.Y + Bit.Static.Height),
                    Font = new Font(Font.FontFamily, Bit.Static.Label.FontSize),
                    Size = new Size(Bit.Static.Width, Bit.Static.Label.Height),
                    TextAlign = ContentAlignment.TopCenter
                };
                Controls.Add(microLabel);
                
                x -= bit.Width + Bit.Static.MarginX;
            }
        }

        #endregion
    }
}