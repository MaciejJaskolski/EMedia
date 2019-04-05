using System;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;

namespace EMedia
{
    /*
     * Class containing information about WAV data and functions on it
     */
    class WAVData
    {
       public byte[] OriginalData { get; set; }
       public float[] ChannelData { get; set; }

        /**
         * Contructor, saves first 1024 samples converted into voltage, saves original bytes in OriginalData 
         */
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
                frqData[i] = shortFormatCpy[i];// / (float)Int16.MaxValue;
            }
            ChannelData = frqData;
       }

        /*
         * Get bytes out of stringified ciphered value 
         */
        public byte[] GetBytes(string str)
        {
            BigInteger number;
            return BigInteger.TryParse(str, out number) ? number.ToByteArray() : null;
        }

        /*
         * Returs an array of ciphered bytes for further use 
         */
        public byte[] Normalize(float[] cipheredData)
        {
            List<byte[]> byteList = new List<byte[]>();
            for(int i = 0; i< cipheredData.Length; i++)
            {
                byte[] result = new byte[4];
                byte[] r = this.GetBytes(cipheredData[i].ToString());
                for (int j = 0; j< 4; j++ )
                {
                    result[j] = 0;
                }
                for (int j = 0; j < r.Length; j++)
                {
                    result[j] = r[j];
                }
                byteList.Add(result);
            }
            byte[] bytes = byteList.SelectMany(a => a).ToArray();
            return bytes;
        }

        /*
         * Returns an array of deciphered bytes for further use
         */
        public float[] Denormalize()
        {
            List<byte[]> listCipher = new List<byte[]>();
            for (int i = 0; i + 3 < OriginalData.Length; i += 4)
            {
                byte[] b = new byte[]
                {
                    OriginalData[i],
                    OriginalData[i+1],
                    OriginalData[i+2],
                    OriginalData[i+3]
                };
                listCipher.Add(b);
            }

            List<float> floats = new List<float>();
            foreach(byte[] b in listCipher)
            {
                floats.Add(BitConverter.ToSingle(b, 0));
            }
            return floats.ToArray();
        }
    }
}
