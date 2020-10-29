using System.Windows;
using System.Windows.Controls;
using Wumpus.Logic;

namespace Wumpus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Game _game;
        private bool _isShootActionCommand;
        public MainWindow()
        {
            InitializeComponent();
            _game = new Game();
            _game.Move(_game.CurrentRoom);
            FormUpdate();
        }
        private void ChoiceRoomBtm_Click(object sender, RoutedEventArgs e)
        {
            int numberRoom = int.Parse((string)(sender as Button).Content);
            if (_isShootActionCommand)
            {
                _game.Shoot(numberRoom);
            }
            else
            {
                _game.Move(numberRoom);
            }
            FormUpdate();
            if (_game.IsLose || _game.IsWin)
            {
                ControlGrid.Children.Clear();
            }
        }
        private void FormUpdate()
        {
            StatusLbl.Content = _game.GetStatus();
            int[] neighbors = _game.GetNeighbors();
            ChoiceRoomBtm1.Content = neighbors[0].ToString();
            ChoiceRoomBtm2.Content = neighbors[1].ToString();
            ChoiceRoomBtm3.Content = neighbors[2].ToString();
        }
        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            _isShootActionCommand = false;
        }
        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            _isShootActionCommand = true;
        }
    }
}
