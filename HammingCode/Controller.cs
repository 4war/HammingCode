using HammingCode.Controls;
using Model;

namespace HammingCode
{
    public class Controller
    {
        public Bit[] InputWord { get; set; }
        public Bit[] HammingCode { get; set; }
        public Bit[] Error { get; set; }
        public Bit[] Message { get; set; }
        public Bit[] PositionOfError { get; set; }
        public Bit[] FixedMessage { get; set; }
        public Bit[] DecodedMessage { get; set; }

        public Controller()
        {
            
        }

        public void Update()
        {
            var inputWord = InputWord.ToByte();
            var hammingCode = inputWord.ToHammingCode();
            HammingCode.InsertValue(hammingCode);

            var error = Error.ToShort();
            var message = hammingCode.AddErrorMatrix(error);
            Message.InsertValue(message);

            var wrongBit = message.GetWrongBit();
            PositionOfError.InsertValue(wrongBit);

            var fixedMessage = message.FixMessage(wrongBit);
            FixedMessage.InsertValue(fixedMessage);

            var decodedMessage = fixedMessage.Decode();
            DecodedMessage.InsertValue(decodedMessage);
        }
    }
}