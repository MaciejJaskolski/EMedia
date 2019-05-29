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
        public byte[] DataToSave { get; set; }
        public float[] ChannelData { get; set; }

        /**
         * Contructor, saves first 1024 samples converted into voltage, saves original bytes in OriginalData 
         */
       public WAVData(byte[] data, int size)
       {
            const int maxSize = 1024; //first 1024 samples
            OriginalData = data;
            data = data.Take(maxSize).ToArray();
            float[] frqData = new float[maxSize];
            Int16[] shortFormatCpy = new Int16[maxSize];
            Buffer.BlockCopy(data, 0, shortFormatCpy, 0, maxSize);
            for(int i = 0; i < shortFormatCpy.Length;i++)
            {
                frqData[i] = shortFormatCpy[i];
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
            List<int> byteList = new List<int>();
            try
            {
                for (int i = 0; i < OriginalData.Length; i++)
                {
                    int x = (int)cipheredData[i] ^ OriginalData[i];
                    byteList.Add(BitConverter.GetBytes(x)[0]);
                }
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
            }
            int[] result = byteList.ToArray();
            byte[] b = new byte[OriginalData.Length];
            for (int i = 0; i < OriginalData.Length; i++)
            {
                b[i] = BitConverter.GetBytes(result[i])[0];
            }
            return b;
        }

        /*
         * Returns an array of deciphered bytes for further use
         */
        public byte[] Denormalize(float[] ciphered)
        {
            List<int> byteList = new List<int>();
            try
            {
                for (int i = 0; i < OriginalData.Length; i++)
                {
                    int x = (int)ciphered[i] ^ OriginalData[i];
                    byte[] y = BitConverter.GetBytes(x);
                    byteList.Add(BitConverter.GetBytes(x)[0]);
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
            }
            int[] result = byteList.ToArray();
            byte[] b = new byte[OriginalData.Length];
            for (int i = 0; i < OriginalData.Length; i++)
            {
                b[i] = BitConverter.GetBytes(result[i])[0];
            }
            return b;
        }
    }
}
