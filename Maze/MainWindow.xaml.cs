using Maze.Algorithm;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Maze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private Button _startPoint, _finishPoint;
        private MazeBuilder _mazeBuilder;
        private PathFinder _pathFinder;
        public MainWindow()
        {
            InitializeComponent();
            _mazeBuilder = new MazeBuilder();
            _pathFinder = new PathFinder();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(10);
            _timer.Tick += FindWay;
        }
        public void FindWay(object o, EventArgs e)
        {
            string[] indexes = _finishPoint.Name.Split(new char[] { 'b', '_' });
            int i = int.Parse(indexes[1]);
            int j = int.Parse(indexes[2]);
            _pathFinder.FindPath(i, j);
            if (_pathFinder.IsFind(i, j))
            {
                PathFinder.Buttons[i, j].Background = Brushes.Green;
                _timer.Stop();
            }
        }
        private void StartBnt_Click(object sender, RoutedEventArgs e)
        {
            if (_startPoint != null && _finishPoint != null)
            {
                string[] indexes = _startPoint.Name.Split(new char[] { 'b', '_' });
                int i = int.Parse(indexes[1]);
                int j = int.Parse(indexes[2]);
                PathFinder.CurrentPosition = new Point(i, j);
                _timer.Start();
            }
        }
        private void SizeFieldSldr_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SizeFieldLbl.Content = $"Size field: {(int)SizeFieldSldr.Value}";
        }
        private void GenerateBnt_Click(object sender, RoutedEventArgs e)
        {
            BuildLabyrinthOnGrid((int)SizeFieldSldr.Value);
            _startPoint = null;
            _finishPoint = null;
        }
        private void SetStartFinishButton(object sender, RoutedEventArgs e)
        {
            string[] indexes = (sender as Button).Name.Split(new char[] { 'b', '_' });
            int i = int.Parse(indexes[1]);
            int j = int.Parse(indexes[2]);
            if (PathFinder.Buttons[i, j].Background != Brushes.Red)
            {
                if (_startPoint == null)
                {
                    _startPoint = PathFinder.Buttons[i, j];
                    PathFinder.Buttons[i, j].Background = Brushes.Green;
                }
                else if (_finishPoint == null)
                {
                    _finishPoint = PathFinder.Buttons[i, j];
                    PathFinder.Buttons[i, j].Background = Brushes.Blue;
                }
            }
        }
        private void BuildLabyrinthOnGrid(int value)
        {
            PathFinder.LabyrinthMap = new int[value, value];
            PathFinder.Buttons = new Button[value, value];
            PathFinder.Way = new bool[value, value];
            MazeGridField.RowDefinitions.Clear();
            MazeGridField.ColumnDefinitions.Clear();
            for (int i = 0; i < value; i++)
                MazeGridField.ColumnDefinitions.Add(new ColumnDefinition());
            for (int i = 0; i < value; i++)
                MazeGridField.RowDefinitions.Add(new RowDefinition());
            int countСrossroads = 0;
            PathFinder.LabyrinthMap = _mazeBuilder.InitializeLabyrinth(value, ref countСrossroads);
            _mazeBuilder.BuildLabyrinth(countСrossroads, ref PathFinder.LabyrinthMap);
            for (int i = 0; i < PathFinder.LabyrinthMap.GetLength(0); ++i)
            {
                for (int j = 0; j < PathFinder.LabyrinthMap.GetLength(1); ++j)
                {
                    PathFinder.Buttons[i, j] = new Button();
                    PathFinder.Buttons[i, j].Name = $"b{i}_{j}";
                    PathFinder.Buttons[i, j].Click += new RoutedEventHandler(SetStartFinishButton);
                    Grid.SetColumn(PathFinder.Buttons[i, j], j);
                    Grid.SetRow(PathFinder.Buttons[i, j], i);
                    if (PathFinder.LabyrinthMap[i, j] == 2)
                    {
                        PathFinder.Buttons[i, j].Background = Brushes.Red;
                    }
                    else
                    {
                        PathFinder.Buttons[i, j].Background = Brushes.White;
                    }
                    MazeGridField.Children.Add(PathFinder.Buttons[i, j]);
                }
            }
        }
    }
}
