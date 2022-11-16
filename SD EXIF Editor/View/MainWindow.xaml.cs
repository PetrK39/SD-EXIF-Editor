using SD_EXIF_Editor.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SD_EXIF_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mvm)
        {
            InitializeComponent();
            DataContext = mvm;

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, CloseCommandExecuted));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, mvm.SaveCommandExecuted));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, mvm.CopyCommandExecuted));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, mvm.DeleteCommandExecuted));

            TextBox.TextChanged += TextBox_GotFocus;
            TextBox.Focus();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox.GotFocus -= TextBox_GotFocus;
            TextBox.CaretIndex = TextBox.Text.Length;
        }

        private void CloseCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}
