using SpeechToText.ViewModel;
using System.ComponentModel;
using System.Windows;

namespace SpeechToText
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            //doing a save cast..
            MainViewModel viewModel = DataContext as MainViewModel;

            if (viewModel != null)
                viewModel.Dispose();
        }

        private void BtnLoadcall_Click(object sender, RoutedEventArgs e)
        {
            ////Create OpenFileDialog
            //OpenFileDialog dlg = new OpenFileDialog();


            ////filter on .wav or .mp3
            //dlg.DefaultExt = ".wav";
            //dlg.Filter = "Audio files (.wav)|*.wav";


            ////Display OpenFileDialog by calling ShowDialog()
            //Nullable<bool> result = dlg.ShowDialog();


            ////Get the selected file name and display in a TextBox 
            //if (result == true)
            //{
            //    string fileName = dlg.FileName;
            //    TxtFileName.Text = fileName;

            //}
        }
    }
}
