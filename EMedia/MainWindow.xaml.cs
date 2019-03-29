﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;
using System.Media;
using System.Windows.Forms;

namespace EMedia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WAVHeader wavHeader;
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Adds wav header items to first tab for MainWindow.xaml
        /// </summary>
        private void AddToList(WAVHeader header)
        {
            List<ListFormat> list = new List<ListFormat>();
            list.Add(new ListFormat("ChunkID", header.ChunkId));
            list.Add(new ListFormat("ChunkSize", header.ChunkSize));
            list.Add(new ListFormat("Format", header.Format));
            list.Add(new ListFormat("Subchunk1ID", header.Subchunk1Id));
            list.Add(new ListFormat("Subchunk1Size", header.Subchunk1Size));
            list.Add(new ListFormat("AudioFormat", header.AudioFormat));
            list.Add(new ListFormat("Number of channels", header.NumChannels));
            list.Add(new ListFormat("Sample Rate", header.SampleRate));
            list.Add(new ListFormat("Byte Rate", header.ByteRate));
            list.Add(new ListFormat("Block Align", header.BlockAlign));
            list.Add(new ListFormat("Bits per Sample", header.BitPerSample));
            list.Add(new ListFormat("Subchunk2ID", header.Subchunk2Id));
            list.Add(new ListFormat("Subchunk2Size", header.Subchunk2Size));

            listTemplate.ItemsSource = list;
        }

        /// <summary>
        /// Loads data for chart for DFT tab for MainWindow.xaml
        ///     <paramref name="data">
        ///         Points for chart
        ///     </paramref>
        /// </summary>
        private void LoadChartData(Point[] data)
        {
            List<KeyValuePair<double, double>> points = new List<KeyValuePair<double, double>>();
            foreach(Point p in data)
            {
                points.Add(new KeyValuePair<double, double>(p.X, p.Y));
            }
            ((LineSeries)chartFFT.Series[0]).ItemsSource = points;
            
        }

        private void BtnPlayCipher_Click(object sender, RoutedEventArgs e)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = System.IO.Path.Combine(basePath, @"./cipher.wav");
            player.Load();
            player.Play();
        }

        private void BtnPlayDecipher_Click(object sender, RoutedEventArgs e)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = System.IO.Path.Combine(basePath, @"./decipher.wav");
            player.Load();
            player.Play();
        }

        private void BtnDecipher_Click(object sender, RoutedEventArgs e)
        {
            WAVHeader cipheredFile = new WAVReader("./cipher.wav").ReadWAVFile();
            Cipher cipher = new Cipher(cipheredFile.WavData.OriginalData);
            float[] deciphered = cipher.getDecipheredData();
            wavHeader.WavData.OriginalData = wavHeader.WavData.Normalize(deciphered);  
            WAVWriter wavWriter = new WAVWriter("decipher.wav");
            wavWriter.WriteWAVFile(wavHeader);
        }

        private void BtnCipher_Click(object sender, RoutedEventArgs e)
        {
            Cipher cipher = new Cipher(wavHeader.WavData.OriginalData);
            float[] encoded = cipher.getCipheredData();
            wavHeader.WavData.OriginalData = wavHeader.WavData.Normalize(encoded);
            WAVWriter wavWriter = new WAVWriter();
            wavWriter.WriteWAVFile(wavHeader);
        }

        private void BtnLoadWAV_Click(object sender, RoutedEventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "wav files (*.wav)|*.wav";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    wavHeader = new WAVReader(filePath).ReadWAVFile();
                    AddToList(wavHeader);
                    DFT dft = new DFT(wavHeader.WavData.ChannelData);
                    Point[] points = dft.FourierData(wavHeader.SampleRate);
                    dft.FourierData(wavHeader.SampleRate);
                    List<double> dataFourier = new List<double>();
                    this.LoadChartData(points);
                }
            }
        }
    }

    /// <summary>
    /// List format to display wav header items
    /// </summary>
    public class ListFormat
    {
        public string LblName { get; set; }
        public string ValName { get; set; }

        public ListFormat(string label, dynamic value)
        {
            this.LblName = label;
            this.ValName = value.ToString();
        }
    }
}
