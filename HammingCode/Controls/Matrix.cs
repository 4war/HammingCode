using System.Windows.Forms;

namespace HammingCode.Controls
{
    public partial class Matrix : UserControl
    {
        private readonly int _length;
        private readonly string _label;
        private readonly Controller _controller;
        public Bit[] Bits { get; } 
        public Matrix(int length, string label, Controller controller)
        {
            _length = length;
            _label = label;
            _controller = controller;
            Bits = new Bit[length];
            InitializeComponent();
        }
    }
}