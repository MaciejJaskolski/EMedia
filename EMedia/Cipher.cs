
namespace EMedia
{
    /*
     * Class used to cipher/decipher data
     */ 
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

        public byte[] getDecipheredData(float[] cipheredData)
        {
            RSA rsa = new RSA();
            return rsa.getDecipheredValue(cipheredData);
        }

    }
}
