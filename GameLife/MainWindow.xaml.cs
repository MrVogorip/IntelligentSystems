using GameLife.Algorithm;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace GameLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private LifeSimulation _life;
        private Button[,] buttons;
        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Step;
        }
        private void Reload()
        {
            _life.NextGeneration();
            if (_life.LivingСells <= 0)
            {
                _timer.Stop();
                MessageBox.Show("No one survived");
            }
            GameGridField.RowDefinitions.Clear();
            GameGridField.ColumnDefinitions.Clear();
            for (int i = 0; i < _life.NumberCells; i++)
                GameGridField.ColumnDefinitions.Add(new ColumnDefinition());
            for (int i = 0; i < _life.NumberCells; i++)
                GameGridField.RowDefinitions.Add(new RowDefinition());
            for (int i = 0; i < _life.NumberCells; i++)
            {
                for (int j = 0; j < _life.NumberCells; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Name = $"b{i}_{j}";
                    buttons[i, j].Click += new RoutedEventHandler(SetActiveCell);
                    Grid.SetColumn(buttons[i, j], j);
                    Grid.SetRow(buttons[i, j], i);
                    if (_life.Cells[i, j].Actual)
                    {
                        buttons[i, j].Background = Brushes.Red;
                    }
                    else
                    {
                        buttons[i, j].Background = Brushes.White;
                    }
                    GameGridField.Children.Add(buttons[i, j]);
                }
            }
        }
        private void SetActiveCell(object sender, RoutedEventArgs e)
        {
            string[] indexes = (sender as Button).Name.Split(new char[] { 'b', '_' });
            int i = int.Parse(indexes[1]);
            int j = int.Parse(indexes[2]);
            buttons[i, j].Background = Brushes.Red;
            _life.SetCell(i, j);
        }
        public void Step(object o, EventArgs e)
        {
            Reload();
        }
        private void StartBnt_Click(object sender, RoutedEventArgs e)
        {
            _timer.Start();
        }
        private void NumberCellsSldr_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            NumberCellsLbl.Content = $"Number of cells: {(int)NumberCellsSldr.Value}";
        }
        private void RandomBnt_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            _life = new LifeSimulation((int)NumberCellsSldr.Value);
            buttons = new Button[_life.NumberCells, _life.NumberCells];
            Reload();
        }
    }
}
