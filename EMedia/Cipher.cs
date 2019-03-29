
namespace EMedia
{
    class Cipher
    {
        public byte[] Data { get; set; }

        public Cipher(byte[] data)
        {
            this.Data = data;
        }

        public float[] getCipheredData()
        {
            RSA rsa = new RSA();
            return rsa.GetCipheredValue(Data);         
        }

        public float[] getDecipheredData()
        {
            RSA rsa = new RSA();
            return rsa.getDecipheredValue(Data);
        }

    }
}
