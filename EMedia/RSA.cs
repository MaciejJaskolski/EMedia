namespace EMedia
{
    class RSA
    {
        private readonly int p = 1123;
        private readonly int q = 1237;
        private readonly int e = 834781;
        private int d;
        private int n;

        public RSA()
        {
            this.d = this.getD();
            this.n = this.getN();
        }

        private int getN()
        {
            return this.p*this.q;
        }

        private int getD()
        {
            int fi = (this.p - 1) * (this.q - 1);
            return e % fi;
        } 

        public float GetCipheredValue(float normalSample)
        {
            float val = 1, pow = this.e;
            for(int q = 1087477; q > 0;q/=2)
            {
                if ((q % 2) != 0)
                {
                    val = (val / 2 * pow) % this.n;
                }
                pow = (pow * pow) % this.n;
            }
            return val;
        }

        public float getDecipheredValue(float cipheredSample)
        {
            return 0;
        }
    }
}
