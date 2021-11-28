using System;
using System.Text;

namespace LedItBe.Core.IO.Extensions
{
    internal static class BufferExtensions
    {
        public static string ReadAsciiString(this byte[] data, int index, int count, bool trim = true)
        {
            if (trim)
            {
                return Encoding.ASCII.GetString(data, index, count).TrimEnd();
            }
            else
            {
                return Encoding.ASCII.GetString(data, index, count);
            }
        }

        public static string ReadAsciiString(this byte[] data, int index)
        {
            int count = 0;
            for (int i = index; i < data.Length; i++)
            {
                if (data[i] == 0x00)
                {
                    break;
                }

                ++count;
            }

            return Encoding.ASCII.GetString(data, index, count);
        }

        public static byte[] Create(this byte[] data, int index = 0, int count = -1)
        {
            if (count == -1)
            {
                count = data.Length;
            }

            byte[] buffer = new byte[count];

            for (int i = 0; i < count; i++)
            {
                buffer[i] = data[index + i];
            }

            return buffer;
        }

        public static byte[] Copy(this byte[] data, byte[] buffer, int indexData = 0, int bufferIndex = 0, int count = -1)
        {
            if (count == -1)
            {
                count = data.Length;
            }

            for (int i = 0; i < count; i++)
            {
                buffer[bufferIndex + i] = data[indexData + i];
            }

            return buffer;
        }

        public static bool IsEqual(this byte[] buffer1, byte[] buffer2)
        {
            if (buffer1.Length != buffer2.Length)
            {
                return false;
            }

            for (int i = 0, max = buffer1.Length; i < max; i++)
            {
                if (buffer1[i] != buffer2[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsRangeEqual(this byte[] buffer1, byte[] buffer2, int buffer1Index = -1, int buffer1Length = -1, int buffer2Index = -1, int buffer2Length = -1)
        {
            if (buffer1Index == -1)
            {
                buffer1Index = 0;
            }

            if (buffer1Length == -1)
            {
                buffer1Length = buffer1.Length;
            }

            if (buffer2Index == -1)
            {
                buffer2Index = buffer1Index;
            }

            if (buffer2Length == -1)
            {
                buffer2Length = buffer1Length;
            }

            if (buffer1.Length < buffer1Index + buffer1Length)
            {
                throw new IndexOutOfRangeException();
            }

            if (buffer2.Length < buffer2Index + buffer2Length)
            {
                throw new IndexOutOfRangeException();
            }

            if (buffer1Length != buffer2Length)
            {
                return false;
            }

            for (int i = buffer1Index, u = buffer2Index, max = buffer1Index + buffer1Length; i < max; i++, u++)
            {
                if (buffer1[i] != buffer2[u])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
