
namespace EMedia
{
    class Cipher
    {
        public float[] Data { get; set; }

        public Cipher(float[] data)
        {
            this.Data = data;
        }

        public float[] getCipheredData()
        {
            RSA rsa = new RSA();
            float result = rsa.GetCipheredValue(983415);
            float[] r = new float[1];
            return r;
        }

    }
}
