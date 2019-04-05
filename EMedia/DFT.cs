using System;
using AForge.Math;

namespace EMedia
{
    class DFT
    {
        private float[] FreqData {get; set; }
        public float[] DFTData { get; set; }

        public DFT(float[] data)
        {
            this.FreqData = data;
        }


        public Point[] FourierData(int sampleRate)
        {
            int N = this.FreqData.Length;
            Complex[] complex = new Complex[N];
            for(int i=0;i<N / 2;i++)
            {
                complex[i] = new Complex(FreqData[i], 0);
            }
            FourierTransform.FFT(complex, FourierTransform.Direction.Forward);
            Point[] points = new Point[N/2];
            for(int i = 0; i< N/2;i++)
            {
                points[i] = new Point(
                    Math.Abs(sampleRate * i / N),
                    Math.Abs(complex[i].Magnitude));
            }
            
            return points;
      
        }
    }

    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Point()
        {

        }
    }
}
