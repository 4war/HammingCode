using System;
using System.Collections.Generic;
using System.Linq;

//Это Rider ругается, что я преобразую short в byte, вместо ushort в byte
#pragma warning disable CS0675

namespace Model
{
    public static class CodeExtensions
    {
        private static readonly short[] PowersOfTwo = new short[16];

        //b1 = b3^b5^b7^b9^b11 = a1^a2^a4^a5^a7
        //b2 = b3^b6^b7^b10^b11 = a1^a3^a4^a6^a7
        //b4 = b5^b6^b7^b12 = a2^a3^a4^a8
        //b8 = b9^b10^b11^b12 = a5^a6^a7^a8
        private static readonly byte[][] ControlBitsArray = new byte[4][]
        {
            new byte[] { 1, 2, 4, 5, 7 }, //b1
            new byte[] { 1, 3, 4, 6, 7 }, //b2
            new byte[] { 2, 3, 4, 8 }, //b4
            new byte[] { 5, 6, 7, 8 }, //b8
        };

        private static readonly short[] Matrix;
        private static readonly byte[] ControlBits = new byte[4];


        static CodeExtensions()
        {
            PowersOfTwo[0] = 1;
            for (var i = 1; i < PowersOfTwo.Length; i++)
            {
                PowersOfTwo[i] = (short)(PowersOfTwo[i - 1] * 2);
            }

            for (var i = 0; i < 4; i++)
            {
                ControlBits[i] = ControlBitsArray[i]
                    .Select(x => (byte)PowersOfTwo[x - 1])
                    .Aggregate((x, y) => (byte)(x | y));
            }

            Matrix = new[]
            {
                (short)(PowersOfTwo[7] | PowersOfTwo[8] | PowersOfTwo[9] | PowersOfTwo[10] | PowersOfTwo[11]),
                (short)(PowersOfTwo[3] | PowersOfTwo[4] | PowersOfTwo[5] | PowersOfTwo[6] | PowersOfTwo[11]),
                (short)(PowersOfTwo[1] | PowersOfTwo[2] | PowersOfTwo[5] | PowersOfTwo[6] | PowersOfTwo[9] |
                        PowersOfTwo[10]),
                (short)(PowersOfTwo[0] | PowersOfTwo[2] | PowersOfTwo[4] | PowersOfTwo[6] | PowersOfTwo[8] |
                        PowersOfTwo[10]),
            }.Reverse().ToArray();
        }

        public static short ToHammingCode(this byte inputWord)
        {
            var positionsOfControlBits = new List<short>(PowersOfTwo.Take(4));
            short resultHammingCode = 0;
            var numberOfControlBitsAdded = 0;
            for (var i = 0; i < 8; i++)
            {
                for (;
                     numberOfControlBitsAdded < positionsOfControlBits.Count &&
                     positionsOfControlBits[numberOfControlBitsAdded] - 1 - numberOfControlBitsAdded == i;
                     numberOfControlBitsAdded += 1)
                {
                    if (GetControlBit(index: numberOfControlBitsAdded, inputWord))
                    {
                        resultHammingCode |= PowersOfTwo[positionsOfControlBits[numberOfControlBitsAdded] - 1];
                    }
                }

                var inputWordBit = (inputWord & (byte)PowersOfTwo[i]) > 0;
                if (inputWordBit)
                {
                    resultHammingCode |= PowersOfTwo[numberOfControlBitsAdded + i];
                }
            }

            return resultHammingCode;
        }

        public static short AddErrorMatrix(this short hammingCode, short error)
        {
            return (short)(hammingCode ^ error);
        }

        public static byte GetWrongBit(this short message)
        {
            byte positionOfWrongBit = 0;
            for (var i = 0; i < Matrix.Length; i++)
                if (((short)(message & Matrix[i])).GetOddity())
                    positionOfWrongBit += (byte)PowersOfTwo[i];

            return positionOfWrongBit;
        }

        public static short FixMessage(this short message, byte indexOfWrongBit)
        {
            if (indexOfWrongBit == 0)
                return message;

            return (short)(message ^ PowersOfTwo[indexOfWrongBit - 1]);
        }
        public static short FixMessage(this short message)
        {
            var indexOfWrongBit = GetWrongBit(message);
            if (indexOfWrongBit == 0)
                return message;

            return (short)(message ^ PowersOfTwo[indexOfWrongBit - 1]);
        }

        public static byte Decode(this short message)
        {
            byte result = 0;
            var controlBitsSkipped = 0;
            for (var i = 0; i < 12; i++)
            {
                if (i + 1 == PowersOfTwo[controlBitsSkipped])
                {
                    controlBitsSkipped++;
                    continue;
                }

                if ((message & PowersOfTwo[i]) > 0)
                    result |= (byte)PowersOfTwo[i - controlBitsSkipped];
            }

            return result;
        }

        private static bool GetControlBit(int index, byte inputWord)
        {
            return ((byte)(ControlBits[index] & inputWord)).GetOddity();
        }

        //Сумма a_i mod 2 
        public static bool GetOddity(this short value)
        {
            return PowersOfTwo.Count(t => (t & value) > 0) % 2 == 1;
        }

        public static bool GetOddity(this byte value)
        {
            return ((short)value).GetOddity();
        }
    }
}