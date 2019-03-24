using System;
using System.IO;

namespace EMedia
{

    class WAVReader
    {
        public string Filename { get; set; }
      
        public WAVReader()
        {

        }

        public WAVReader(string filename)
        {
            try
            {
                this.Filename = this.ValidateFileName(filename) ? filename : throw new Exception();
            }
            catch(Exception e)
            {
                
            }
        }

        private bool ValidateFileName(string filename)
        {

            if(filename.Contains(".wav"))
            {
                return true;
            }
            return true;
        }

        private string FormatValue(int value)
        {
            string hexField = value.ToString("X");
            string formattedValue = String.Empty;
            try
            {
                if (hexField.Length % 2 != 0)
                {
                    hexField = "0" + hexField;
                }

                for (int i = 0; i < hexField.Length; i += 2)
                {
                    String ch = String.Empty;

                    ch = hexField.Substring(i, 2);
                    uint dec = Convert.ToUInt32(ch, 16);
                    char c = Convert.ToChar(dec);
                    formattedValue += c;
                }

                char[] arr = formattedValue.ToCharArray();
                Array.Reverse(arr);
                return new string(arr);
            }
            catch (Exception e)
            {
                return "Error while formatting value: " + e;
            }
        }

        public WAVHeader ReadWAVFile()
        {
            WAVHeader wavHeader = new WAVHeader();
            using (FileStream fs = File.Open(this.Filename, FileMode.Open))
            {
                BinaryReader binaryReader = new BinaryReader(fs);
                wavHeader.ChunkId = this.FormatValue(binaryReader.ReadInt32());
                wavHeader.ChunkSize = binaryReader.ReadUInt32();
                wavHeader.Format = this.FormatValue(binaryReader.ReadInt32());
                wavHeader.Subchunk1Id = this.FormatValue(binaryReader.ReadInt32());
                wavHeader.Subchunk1Size = binaryReader.ReadInt32();
                wavHeader.AudioFormat = binaryReader.ReadInt16();
                wavHeader.NumChannels = binaryReader.ReadInt16();
                wavHeader.SampleRate = binaryReader.ReadInt32();
                wavHeader.ByteRate = binaryReader.ReadInt32() * 8;
                wavHeader.BlockAlign = binaryReader.ReadInt16();
                wavHeader.BitPerSample = binaryReader.ReadInt16();
                wavHeader.Subchunk2Id = this.FormatValue(binaryReader.ReadInt32()); 
                wavHeader.Subchunk2Size = binaryReader.ReadInt32();
                byte[] readDataBytes = binaryReader.ReadBytes(wavHeader.Subchunk2Size);
                WAVData wavData = new WAVData(wavHeader.NumChannels, readDataBytes, wavHeader.Subchunk2Size);
                wavHeader.WavData = wavData;
            }
            return wavHeader;
        }

    }
}
