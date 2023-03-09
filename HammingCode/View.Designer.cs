using System.Drawing;
using System.Windows.Forms;
using HammingCode.Controls;

namespace HammingCode
{
    partial class View
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Код Хэмминга";
            
          
            var word = new InputWordMatrix(8, "Входное слово", _controller);
            var hammingCode = new HammingMatrix(12, "Код Хэмминга", _controller);
            var error = new ErrorMatrix(12, "Ошибка", _controller);
            var message = new MessageMatrix(12, "Переданное слово", _controller);
            var positionOfError = new PositionOfErrorMatrix(4, "Номер неверного разряда", _controller);
            var fixedMessage = new FixedMessageMatrix(12, "Исправленное слово", _controller);
            var decodedMessage = new DecodedWordMatrix(8, "Полученное слово", _controller);

            _controller.InputWord = word.Bits;
            _controller.HammingCode = hammingCode.Bits;
            _controller.Error = error.Bits;
            _controller.Message = message.Bits;
            _controller.PositionOfError = positionOfError.Bits;
            _controller.FixedMessage = fixedMessage.Bits;
            _controller.DecodedMessage = decodedMessage.Bits;
            
            Controls.Add(word);
            Controls.Add(hammingCode);
            Controls.Add(error);
            Controls.Add(message);
            Controls.Add(positionOfError);
            Controls.Add(fixedMessage);
            Controls.Add(decodedMessage);
            
        }
    

        #endregion
    }
}