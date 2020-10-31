using System;
using System.Windows;
using System.Windows.Controls;

namespace Bayesian
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BayesianNetwork _bayesian;
        public MainWindow()
        {
            _bayesian = new BayesianNetwork();
            _bayesian.Solve();
            InitializeComponent();
            PrintInfo();
        }
        private void PrintInfo()
        {
            PrintPanel.Children.Clear();
            for (int i = 0; i < _bayesian.Nodes.Length; ++i)
            {
                Label label = new Label();
                label.Content = $"{_bayesian.Nodes[i].Name}\n" +
                $"True = {Math.Round(_bayesian.Nodes[i].CurrentValues[1] * 100, 2)}" + 
                $"% | False = {Math.Round(_bayesian.Nodes[i].CurrentValues[0] * 100, 2)}%";
                PrintPanel.Children.Add(label);
            }
        }

        private void ParamChange_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _bayesian.ChangeZeroParam(ParamChange.Value/100.0);
            _bayesian.Solve();
            PrintInfo();
        }
    }
}
