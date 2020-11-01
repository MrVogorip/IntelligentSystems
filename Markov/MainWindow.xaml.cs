using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace Markov
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MarkovAlgorithm _markov;
        private string _text;
        public MainWindow()
        {
            InitializeComponent();
            _markov = new MarkovAlgorithm();
        }
        private void OpenBnt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.DefaultExt = ".txt";
                openFileDialog.Filter = "txt (*.txt)|*.txt";
                openFileDialog.ShowDialog();
                string path = openFileDialog.FileName;
                _text = Regex.Replace(File.ReadAllText(path), @"\s+", " ").TrimEnd(' ');
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void GenerateBnt_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, Dictionary<string, uint>> frequency = _markov.BuildFrequency(_text);
            PrintInfo.Text = _markov.BuildString(frequency).TrimEnd(' ');
        }
    }
}
