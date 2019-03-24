using System;
using System.Linq;

namespace EMedia
{
    class WAVData
    {
       public float[] ChannelData { get; set; }

       public WAVData(int numChannels, byte[] data, int size)
       {
            const int maxSize = 1024; //first 1000 samples
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
   
    }
}
