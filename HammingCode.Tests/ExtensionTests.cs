using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using NUnit.Framework;

namespace HammingCode.Tests
{
    [TestFixture]
    public class Tests
    {
        private readonly int[] _powersOfTwo =
            { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 4096 * 2, 4096 * 4, 4096 * 8 };


        [TestCase("0000 0000", "0000 0000 0000")]
        [TestCase("0000 0001", "0000 0000 0111")]
        [TestCase("1000 0000", "1000 1000 1000")]
        [TestCase("0000 1000", "0000 0100 1011")]
        [TestCase("1111 1111", "1111 0111 0111")]
        [TestCase("1001 1011", "1001 0101 1100")]
        [TestCase("1010 1110", "1010 0111 0010")]
        public void Should_GetCorrectHammingCodeFromInputByte(string inputString, string expectedString)
        {
            var input = Convert.ToByte(inputString.Replace(" ", ""), 2);
            var expected = Convert.ToInt16(expectedString.Replace(" ", ""), 2);
            var actual = input.ToHammingCode();
            var errorMessage = BuildErrorMessage(expected, actual, 12);

            Assert.AreEqual(expected, actual, errorMessage);
        }

        [TestCase("1010 0111 0010", "0000 0000 0000", "1010 0111 0010")]
        [TestCase("1010 0111 0010", "1000 0000 0000", "0010 0111 0010")]
        [TestCase("1010 0111 0010", "0000 0000 0001", "1010 0111 0011")]
        [TestCase("1111 1111 1111", "0000 1000 0000", "1111 0111 1111")]
        [TestCase("1010 0111 0010", "0010 0000 0100", "1000 0111 0110")]
        public void Should_AddErrors(string inputHammingCode, string errorString, string expectedString)
        {
            var input = Convert.ToInt16(inputHammingCode.Replace(" ", ""), 2);
            var error = Convert.ToInt16(errorString.Replace(" ", ""), 2);
            var expected = Convert.ToInt16(expectedString.Replace(" ", ""), 2);
            Assert.AreEqual(expected, input.AddErrorMatrix(error));
        }

        [TestCase("1010 0111 0010", "0000 0000 0100", "0011")]
        [TestCase("1010 0111 0010", "0001 0000 0000", "1001")]
        [TestCase("1010 0111 0010", "0000 0000 0000", "0000")]
        public void Should_FindError(string inputString, string errorString, string expectedString)
        {
            var input = Convert.ToInt16(inputString.Replace(" ", ""), 2);
            var error = Convert.ToInt16(errorString.Replace(" ", ""), 2);
            var message = input.AddErrorMatrix(error);
            var expected = Convert.ToByte(expectedString.Replace(" ", ""), 2);
            var actual = message.GetWrongBit();
            var errorMessage = BuildErrorMessage(expected, actual, 4);
            Assert.AreEqual(expected, actual, errorMessage);
        }


        [TestCase("0000 0000 0000")]
        [TestCase("0000 0000 0111")]
        [TestCase("1000 1000 1000")]
        [TestCase("0000 0100 1011")]
        [TestCase("1111 0111 0111")]
        [TestCase("1001 0101 1100")]
        [TestCase("1010 0111 0010")]
        public void Should_FixSingleError_OrNoErrors(string inputString)
        {
            const int max = 12;
            var input = Convert.ToInt16(inputString.Replace(" ", ""), 2);

            var list = new List<short>()
                {
                    Convert.ToInt16(new string('0', max), 2)
                }
                .Concat(GenerateAllPossibleSingleErrors()).ToList();

            for (var index = 0; index < list.Count; index++)
            {
                var error = list[index];
                var message = input.AddErrorMatrix(error);
                var wrongBitIndex = message.GetWrongBit();
                var actual = message.FixMessage(wrongBitIndex);
                Assert.AreEqual(index, wrongBitIndex);
                Assert.AreEqual(input, actual);
            }
        }


        [TestCase("0000 0000", "0000 0000 0000")]
        [TestCase("0000 0001", "0000 0000 0111")]
        [TestCase("1000 0000", "1000 1000 1000")]
        [TestCase("0000 1000", "0000 0100 1011")]
        [TestCase("1111 1111", "1111 0111 0111")]
        [TestCase("1001 1011", "1001 0101 1100")]
        [TestCase("1010 1110", "1010 0111 0010")]
        public void Should_DecodeFixedMessage(string expectedString, string messageString)
        {
            var expected = Convert.ToInt16(expectedString.Replace(" ", ""), 2);
            var message = Convert.ToInt16(messageString.Replace(" ", ""), 2);
            var actual = message.Decode();
            var errorMessage = BuildErrorMessage(expected, actual, 12);
            Assert.AreEqual(expected, actual, errorMessage);
        }

        [Test]
        public void Should_FixSingleError_AllPossibleCases()
        {
            for (byte value = 0; value < _powersOfTwo[7]; value++)
                foreach (var singleError in GenerateAllPossibleSingleErrors())
                    PerformRandomTest(value, singleError);
        }

        [Test]
        public void Should_NotChangeInputIfNoErrors()
        {
            var noErrorMatrix = Convert.ToInt16(new string('0', 12), 2);
            for (byte value = 0; value < _powersOfTwo[7]; value++)
                PerformRandomTest(value, noErrorMatrix);
        }

        [Test]
        public void Should_FailIfMoreThanOneError()
        {
            for (byte value = 0; value < _powersOfTwo[7]; value++)
            {
                for (var i = 0; i < 100; i++)
                {
                    var error = Generate_PossibleCase_WhenTwoErrorsInMatrix();
                    var hammingCode = value.ToHammingCode();
                    var message = hammingCode.AddErrorMatrix(error);
                    var wrongBit = message.GetWrongBit();
                    var fixedMessage = message.FixMessage(wrongBit);
                    var decodedInitialMessage = fixedMessage.Decode();
                    var errorMessage =
                        $"value: {Convert.ToString(value,2)} \n" +
                        $"error: {Convert.ToString(error,2)} \n" +
                        $"hammingCode: {Convert.ToString(hammingCode,2)} \n" +
                        $"message: {Convert.ToString(message,2)} \n" +
                        $"wrongBit: {Convert.ToString(wrongBit,2)} \n" +
                        $"fixedMessage: {Convert.ToString(fixedMessage,2)} \n" +
                        $"decodedInitialMessage: {Convert.ToString(decodedInitialMessage,2)} \n";
                    Assert.AreNotEqual(value, decodedInitialMessage, errorMessage);
                }
            }
        }


        private void PerformRandomTest(byte value, short error)
        {
            var hammingCode = value.ToHammingCode();
            var message = hammingCode.AddErrorMatrix(error);
            var wrongBit = message.GetWrongBit();
            var fixedMessage = message.FixMessage(wrongBit);
            var decodedInitialMessage = fixedMessage.Decode();
            var errorMessage = BuildErrorMessage(value, decodedInitialMessage, 8);
            Assert.AreEqual(value, decodedInitialMessage, errorMessage);
        }

        private List<short> GenerateAllPossibleSingleErrors()
        {
            var maxLength = 12;
            var result = new List<short>();
            for (var i = 0; i < maxLength; i++)
                result.Add(Convert.ToInt16($"{new string('0', maxLength - i - 1)}1{new string('0', i)}", 2));
            return result;
        }

        private short Generate_PossibleCase_WhenTwoErrorsInMatrix()
        {
            var random = new Random();
            var maxLength = 12;
            var randomIndex1 = random.Next(maxLength - 2);
            var randomIndex2 = random.Next(maxLength - randomIndex1 - 1) + randomIndex1 + 1;

            var sb = new StringBuilder(new string('0', 12));
            sb[randomIndex1] = '1';
            sb[randomIndex2] = '1';
            return Convert.ToInt16(sb.ToString(), 2);
        }

        private string BuildErrorMessage(short expected, short actual, int max)
        {
            var errorList = new List<string>();
            for (var i = 0; i < max; i++)
            {
                if ((_powersOfTwo[i] & expected) != (_powersOfTwo[i] & actual))
                {
                    errorList.Add(
                        $"b{i + 1}: expected: {_powersOfTwo[i] & expected} actual: {_powersOfTwo[i] & actual}");
                }
            }

            if (errorList.Any())
            {
                return $"expected: {Convert.ToString(expected, 2)}, but actual: {Convert.ToString(actual, 2)}\n" +
                       string.Join("\n", errorList);
            }

            return string.Empty;
        }
    }
}