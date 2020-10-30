using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Solver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Questions _questions;
        private readonly Results _results;
        private readonly Table _table;
        private readonly User _user;
        public MainWindow()
        {
            InitializeComponent();
            _questions = new Questions();
            _results = new Results();
            _table = new Table();
            _user = new User();
            FormUpdate();
        }
        private void FormUpdate()
        {
            AnswerPanel.Children.Clear();
            Tuple<int, string> question = _questions.GetQuestion(_user.Status);
            if (question == null)
            {
                MessageBox.Show(_results.GetResult(_user.Status));
                _user.Status = 0;
                FormUpdate();
            }
            else
            {
                int tempStatus = question.Item1;
                QuestionsLbl.Content = question.Item2;
                List<(int, Tuple<int, string>)> answers = _table.GetAnswers(tempStatus);
                for (int i = 0; i < answers.Count; i++)
                {
                    Button button = new Button();
                    button.MaxWidth = 250;
                    button.Click += ChangeStatus;
                    button.Content = answers[i].Item2.Item2;
                    button.Tag = answers[i].Item1.ToString();
                    AnswerPanel.Children.Add(button);
                }
            }
        }
        private void ChangeStatus(object sender, RoutedEventArgs e)
        {
            _user.Status = int.Parse((sender as Button).Tag.ToString());
            FormUpdate();
        }
    }
}
