using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Speech.Recognition;
using System.Globalization;

namespace Speech
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string[] _words;
        private Label _label;
        public MainWindow()
        {
            InitializeComponent();
            _words = new string[] { "Примите", "Лабу", "По", "Распознаванию" };
            Loaded += Shown;
        }
        void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence > 0.7)
                _label.Content += e.Result.Text + " ";
        }
        void Shown(object sender, EventArgs e)
        {
            _label = TextResult;
            CultureInfo region = new CultureInfo("ru-ru");
            SpeechRecognitionEngine speechRecognition = new SpeechRecognitionEngine(region);
            speechRecognition.SetInputToDefaultAudioDevice();
            speechRecognition.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(SpeechRecognized);
            Choices choices = new Choices();
            choices.Add(_words);
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Culture = region;
            grammarBuilder.Append(choices);
            Grammar grammar = new Grammar(grammarBuilder);
            speechRecognition.LoadGrammar(grammar);
            speechRecognition.RecognizeAsync(RecognizeMode.Multiple);
            base.OnContentRendered(e);
        }
    }
}
