using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HammingCode
{
    partial class Bit
    {
        public static class Static
        {
            public static readonly int Width = 35; 
            public static readonly int Height = 35; 
            public static readonly int PaddingX = 0; 
            public static readonly int PaddingY = 0; 
            public static readonly int MarginX = 5; 
            public static readonly int MarginY = 10;

            public static class Label
            {
                public static readonly int Height = 10;
                public static readonly float FontSize = 6.0f;
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
            FlatStyle = FlatStyle.Flat;
            BackColor = _color;
            Size = new Size(Static.Width, Static.Height);
            Text = "0";
            Font = new Font(Font.FontFamily, 10f);
        }

        #endregion
    }
}