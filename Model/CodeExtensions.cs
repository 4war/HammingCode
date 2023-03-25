using System;
using System.Collections.Generic;
using System.Linq;

//Это Rider ругается, что я преобразую short в byte, вместо ushort в byte
#pragma warning disable CS0675

namespace Model
{
    public static class CodeExtensions
    {
        private static readonly short[] bits = new short[16];
        private static readonly short[] Matrix;
        
        //b1 = b3^b5^b7^b9^b11 = a1^a2^a4^a5^a7
        //b2 = b3^b6^b7^b10^b11 = a1^a3^a4^a6^a7
        //b4 = b5^b6^b7^b12 = a2^a3^a4^a8
        //b8 = b9^b10^b11^b12 = a5^a6^a7^a8
        private static readonly byte[] ControlBits = new byte[4]
        {
            0x5B,
            0x6D,
            0x8E,
            0xF0
        };
        
        static CodeExtensions()
        {
            //Заполняю массив масок битов, которые в числовом представлении равны степеням двойки
            bits[0] = 1;
            for (var i = 1; i < bits.Length; i++)
                bits[i] = (short)(bits[i - 1] << 1);

            //Контрольная матрица M
            Matrix = new[]
            {
                (short)0x0555,
                (short)0x0666,
                (short)0x0878,
                (short)0x0F80,
            };
        }


        /// <summary>
        /// Переводит в код Хэмминга
        /// </summary>
        /// <param name="A">Байт, который вводит пользователь</param>
        /// <returns>Код Хэмминга</returns>
        public static short ToHammingCode(this byte A)
        {
            short B = 0;
            B |= (short)(GetControlBit(0, A) ? 0x01 : 0);
            B |= (short)(GetControlBit(1, A) ? 0x02 : 0);
            B |= (short)((A & 0x01) << 2);
            B |= (short)(GetControlBit(2, A) ? 0x08 : 0);
            B |= (short)((A & 0x02) << 3);
            B |= (short)((A & 0x04) << 3);
            B |= (short)((A & 0x08) << 3);
            B |= (short)(GetControlBit(3, A) ? 0x0080 : 0);
            B |= (short)((A & 0x0010) << 4);
            B |= (short)((A & 0x0020) << 4);
            B |= (short)((A & 0x0040) << 4);
            B |= (short)((A & 0x0080) << 4);
            
            return B;
        }
        
        /// <summary>
        /// Моделирует помехи в канале связи, добавляя матрицу E к B (сложение по модулю 2)
        /// </summary>
        /// <param name="hammingCode">Код Хэмминга 12 бит</param>
        /// <param name="error">Матрица ошибки</param>
        /// <returns>E (+) B - 12 бит</returns>
        public static short AddErrorMatrix(this short hammingCode, short error)
        {
            return (short)(hammingCode ^ error);
        }

        /// <summary>
        /// Возвращает позиицю неправильного бита в сообщении при умножении матриц B x M
        /// </summary>
        /// <param name="message">Переданное сообщение по каналу связи 12 бит</param>
        /// <returns>Позицию неправильного бита</returns>
        public static byte GetWrongBit(this short message)
        {
            byte positionOfWrongBit = 0;
            for (var i = 0; i < Matrix.Length; i++)
                if (((short)(message & Matrix[i])).GetOddity())
                    positionOfWrongBit += (byte)bits[i];

            return positionOfWrongBit;
        }

        /// <summary>
        /// Меняет бит в переданном сообщении
        /// </summary>
        /// <param name="message">переданное сообщение 12 бит</param>
        /// <param name="indexOfWrongBit">номер бита, который нужно поменять</param>
        /// <returns>Исправленное сообщение 12 бит</returns>
        public static short FixMessage(this short message, byte indexOfWrongBit)
        {
            //Если номер неверного бита - 0, значит ошибки нет
            if (indexOfWrongBit == 0)
                return message;

            // Сложение по модулю 2 с 'indexOfWrongBit - 1' битом
            // Вычитаю 1 потому что индексация в массивах с нуля, а биты нумеруются с единицы
            return (short)(message ^ bits[indexOfWrongBit - 1]);
        }

        /// <summary>
        /// Декодирует переданное сообщение 12 -> 8  бит
        /// </summary>
        /// <param name="message">Сообщение, полученное по каналу связи</param>
        /// <returns>Декодированное сообщение 8 бит</returns>
        public static byte Decode(this short message)
        {
            byte result = 0;
            var controlBitsSkipped = 0;
            for (var i = 0; i < 12; i++)
            {
                if (i + 1 == bits[controlBitsSkipped])
                {
                    controlBitsSkipped++;
                    continue;
                }

                if ((message & bits[i]) > 0)
                    result |= (byte)bits[i - controlBitsSkipped];
            }

            return result;
        }

        /// <summary>
        /// Возвращает i-й контрольный бит
        /// 0 - b1
        /// 1 - b2
        /// 2 - b4
        /// 3 - b8
        /// </summary>
        /// <param name="index">i - [0,3] - номер контрольного бита </param>
        /// <param name="inputWord">Сообщение из 1 байта, которое ввел пользователь</param>
        /// <returns></returns>
        private static bool GetControlBit(int index, byte inputWord)
        {
            return ((byte)(ControlBits[index] & inputWord)).GetOddity();
        }

        /// <summary>
        /// Проверяет на нечетность суммы битов a_i 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool GetOddity(this short value)
        {
            //Количество единиц в битовом представлении числа - нечетной (true/false)
            return bits.Count(t => (t & value) > 0) % 2 == 1;
        }

        //Перегрузка для byte
        public static bool GetOddity(this byte value)
        {
            return ((short)value).GetOddity();
        }
    }
}