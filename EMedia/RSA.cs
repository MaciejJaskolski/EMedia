using System;

namespace EMedia
{
    class RSA
    {
        private readonly int p = 1123;
        private readonly int q = 1237;
        private long e = 834781;
        private long d = 1087477;
        private int n;

        public RSA()
        {
            this.n = this.getN();
        }

        private int getN()
        {
            return this.p*this.q;
        }

        private long CipherLoop(long exponent, long originalValue)
        {
            long cipheredValue = 1;
            for(long i = exponent; i > 0; i/=2)
            {
                if(i % 2 == 1)
                {
                    cipheredValue = (originalValue * cipheredValue) % this.n;
                }
                originalValue = (originalValue * originalValue) % this.n;
            }
            return cipheredValue;
        }

        public float[] GetCipheredValue(byte[] normalSample)
        {
            float[] rsaData = new float[normalSample.Length];
            for(int i =0;i<normalSample.Length;i++)
            {
                long value = normalSample[i] % this.n;
                rsaData[i] = this.CipherLoop(this.e, value);
            }
            return rsaData;
        }

        public byte[] getDecipheredValue(float[] cipheredSample)
        {
            byte[] rsaData = new byte[cipheredSample.Length];
            for (int i = 0; i < cipheredSample.Length; i++)
            {
                long value = (long)cipheredSample[i] % this.n;
                rsaData[i] = (byte)this.CipherLoop(this.d, value);
            }
            return rsaData;
        }
    }
}
