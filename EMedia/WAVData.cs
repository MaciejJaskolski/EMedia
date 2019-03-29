using System;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;

namespace EMedia
{
    class WAVData
    {
       public byte[] OriginalData { get; set; }
       public float[] ChannelData { get; set; }

       public WAVData(int numChannels, byte[] data, int size)
       {
            const int maxSize = 1024; //first 1000 samples
            OriginalData = data;
            data = data.Take(maxSize).ToArray();
            float[] frqData = new float[maxSize];
            Int16[] shortFormatCpy = new Int16[maxSize];
            Buffer.BlockCopy(data, 0, shortFormatCpy, 0, maxSize);
            for(int i = 0; i < shortFormatCpy.Length;i++)
            {
                frqData[i] = shortFormatCpy[i] / (float)Int16.MaxValue;
            }
            ChannelData = frqData;
       }

        public byte[] GetBytes(string str)
        {
            BigInteger number;
            return BigInteger.TryParse(str, out number) ? number.ToByteArray() : null;
        }

        public byte[] Normalize(float[] cipheredData)
        {
            List<byte[]> byteList = new List<byte[]>();
            for(int i = 0; i< cipheredData.Length; i++)
            {
                byteList.Add(this.GetBytes(cipheredData[i].ToString()));
            }
            byte[] bytes = byteList.SelectMany(a => a).ToArray();
            return bytes;
        }

        public byte[] Denormalize(float[] deciphered)
        {
            float[] data = new float[OriginalData.Length];
            for (int i = 0; i < OriginalData.Length; i++)
            {
                data[i] = BitConverter.ToSingle(OriginalData, 0);
            }
            return this.Normalize(data);
        }
    }
}
