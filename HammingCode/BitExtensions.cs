using System.Collections.Generic;

namespace HammingCode
{
    public static class BitExtensions
    {
        private static readonly short[] PowersOfTwo = new short[16];
        
        static BitExtensions()
        {
            PowersOfTwo[0] = 1;
            for (var i = 1; i < PowersOfTwo.Length; i++)
            {
                PowersOfTwo[i] = (short)(PowersOfTwo[i - 1] * 2);
            }
        }

        public static Bit[] InsertValue(this Bit[] array, short value)
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i].SetValue((value & PowersOfTwo[i]) > 0);
            }

            return array;
        }

        public static Bit[] InsertValue(this Bit[] array, byte value)
        {
            return array.InsertValue((short)value);
        }

        public static short ToShort(this Bit[] array)
        {
            short result = 0;
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i].Text == "1")
                {
                    result |= PowersOfTwo[i];
                }
            }

            return result;
        }

        public static byte ToByte(this Bit[] array)
        {
            return (byte)array.ToShort();
        }
    }
}