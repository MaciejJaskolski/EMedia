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

        private float CipherLoop(long exponent, long originalValue)
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
            return (float)cipheredValue;
        }

        public float[] GetCipheredValue(byte[] normalSample)
        {
            float[] rsaData = new float[this.n];
            try
            {
                for (long i = 0; i < normalSample.Length; i++)
                {
                    rsaData[i] = this.CipherLoop(this.e, i);
                }
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show("Problem przy szyfrowaniu.", e.ToString());
            }
            return rsaData;
        }
        
        public float[] getDecipheredValue(byte[] cipheredSample)
        {
            float[] rsaData = new float[this.n];
            try
            {
                for (long i = 0; i < this.n; i++)
                {
                    rsaData[i] = this.CipherLoop(this.e, i);
                }
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show("Problem przy odszyfrowaniu.", e.ToString());
            }
            return rsaData;
        }
    }
}
