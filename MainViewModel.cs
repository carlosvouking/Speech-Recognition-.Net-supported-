using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.Speech.Recognition;
using System.Windows.Input;


namespace SpeechToText.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase, IDisposable
    {
        private SpeechRecognitionEngine _recognizer;      //@"C:\Users\cvouking\Downloads\fadg0\fadg0\audio\sa1.wav" 



        public ICommand BrowseCallCommand { get; private set; }
        private string _filepath = @"C:\Users\cvouking\Downloads\fadg0\fadg0\audio\sa1.wav";


        public string Filepath
        {
            get { return _filepath; }
            set { Set(ref _filepath, value); }
        }



        public ICommand ActivateRecognitionCommand { get; private set; }
        private string _recognizedText;

        public string RecognizedText
        {
            get { return _recognizedText; }
            set { Set(ref _recognizedText, value); }
        }

        public MainViewModel()
        {
            BrowseCallCommand = new RelayCommand(BrowseCall);

            //initialize the command ppty and assign it a relay command instance
            ActivateRecognitionCommand = new RelayCommand(ActivateRecognition);

            InitializeRecognitionEngine();
        }


        private void BrowseCall()
        {
            //Clear the text for each session.
            Filepath = string.Empty;
            RecognizedText = string.Empty;

            //Create OpenFileDialog
            OpenFileDialog dlg = new OpenFileDialog();


            //filter on .wav or .mp3
            dlg.DefaultExt = ".wav";
            dlg.Filter = "Audio files (.wav)|*.wav";


            //Display OpenFileDialog by calling ShowDialog()
            Nullable<bool> result = dlg.ShowDialog();


            //Get the selected file name and display in a TextBox 
            if (result == true)
            {
                string fileName = dlg.FileName;
                Filepath = fileName;
            }
        }


        private void InitializeRecognitionEngine()
        {
            _recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            _recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-GB"));

            //_recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("fr-FR"));
            //_recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("fr-FR"));

            //_recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("nl-NL"));
            //_recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("nl-BE"));


            // sound input
            _recognizer.SetInputToWaveFile(Filepath);
            _recognizer.LoadGrammar(new DictationGrammar());

            //LoadWeatherGrammar(); 
            //LoadCalculatorGrammar();  

            // Event to notify if the speech has been recognized ??
            _recognizer.RecognizeCompleted += RecognizedCompleted;


        }

        private void LoadCalculatorGrammar()
        {
            GrammarBuilder builder = new GrammarBuilder();
            Choices numbers = new Choices();

            numbers.Add(new SemanticResultValue("zero", .0));
            numbers.Add(new SemanticResultValue("one", 1.0));
            numbers.Add(new SemanticResultValue("two", 2.0));
            numbers.Add(new SemanticResultValue("three", 3.0));
            numbers.Add(new SemanticResultValue("four", 4.0));
            numbers.Add(new SemanticResultValue("five", 5.0));
            numbers.Add(new SemanticResultValue("six", 6.0));
            numbers.Add(new SemanticResultValue("seven", 7.0));
            numbers.Add(new SemanticResultValue("eight", 8.0));
            numbers.Add(new SemanticResultValue("nine", 9.0));

            builder.Append(numbers);

            _recognizer.LoadGrammar(new Grammar(builder));
        }
        private void LoadWeatherGrammar()
        {
            GrammarBuilder builder = new GrammarBuilder();
            Choices dayChoices = new Choices("Monday", "Tuesday", "Wednesday", "Thursday",
                "Friday", "Saturday", "Sunday", "Tomorrow");

            builder.AppendWildcard();
            builder.Append("forecast for");
            builder.Append(dayChoices, 1, 3);

            _recognizer.LoadGrammar(new Grammar(builder));
        }


        private void RecognizedCompleted(object sender, RecognizeCompletedEventArgs e)
        {

            if (e.Result != null)
            {
                RecognizedText = e.Result.Text;
            }
            else
                RecognizedText = "geen sound input te detecteren";
        }



        private void ActivateRecognition()
        {
            Filepath = _filepath;

            //Clear the text for each session.
            RecognizedText = string.Empty;

            //tell the recognition engine to start listening to te input 
            _recognizer.RecognizeAsync();
        }

        public void Dispose()
        {
            if (_recognizer != null)
            {
                _recognizer.Dispose();
            }
        }
    }
}