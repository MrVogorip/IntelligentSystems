using Genetic.Algorithm;
using System.Windows;
using System.Windows.Controls;

namespace Genetic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly int _numInit = 50;
        readonly int _numGen = 20;

        public MainWindow()
        {
            InitializeComponent();
            Calculate();
        }

        private void Calculate()
        {
            Evolution evolution = new Evolution(_numInit);
            Genotype[] genotype = evolution.StartEvolution(_numGen);
            for (int i = 0; i < genotype.Length; i++)
            {
                Label label = new Label();
                label.Content = $"{i + 1}) {genotype[i]}\nF = {genotype[i].FitFunction}\n";
                PopulationPanel.Children.Add(label);
            }
            FinalResult.Content += $"\n{genotype[genotype.Length - 1]}\nF = {genotype[genotype.Length - 1].FitFunction}\n";
        }
    }
}
