namespace EMedia
{
    class WAVHeader
    {
        public string ChunkId { get; set; }
        public uint ChunkSize { get; set; }
        public string Format { get; set; }
        public string Subchunk1Id { get; set; }
        public int Subchunk1Size { get; set; }
        public int AudioFormat { get; set; }
        public int NumChannels { get; set; }
        public int SampleRate { get; set; }
        public int ByteRate { get; set; }
        public int BlockAlign { get; set; }
        public int BitPerSample { get; set; }
        public string Subchunk2Id { get; set; }
        public int Subchunk2Size { get; set; }
        public WAVData WavData { get; set; }

        public WAVHeader(string filename)
        {
            WAVReader wavReader = new WAVReader(filename);
        }

        public WAVHeader()
        {

        }

        public override string ToString()
        {
            return ChunkId + " " + ChunkSize.ToString() + " " + Format + " " + Subchunk1Id + " " + Subchunk1Size.ToString() + " " +
                AudioFormat.ToString() + " " + NumChannels.ToString() + " " + SampleRate.ToString() + " " + ByteRate.ToString() +
                " " + BlockAlign.ToString() + " " + BitPerSample.ToString() + " " + Subchunk2Id + " " + Subchunk2Size;
        }

    }
}
