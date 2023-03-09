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

        public void UpdateHammingCode()
        {
            var inputWord = InputWord.ToByte();
            var hammingCode = inputWord.ToHammingCode();
            HammingCode.InsertValue(hammingCode);
        }
    }
}