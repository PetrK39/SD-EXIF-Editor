using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace SD_EXIF_Editor.Utils
{
    internal class CommandToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not RoutedUICommand command) return null;

            if (command == ApplicationCommands.Delete) return $"{command.Text} ({(command.InputGestures[0] as KeyGesture).Key.ToString()})"; //wierd localisation
            if (command == ApplicationCommands.Close) return $"{command.Text} ({(command.InputGestures[0] as KeyGesture).Key.ToString()})";

            return $"{command.Text} ({(command.InputGestures[0] as KeyGesture).DisplayString})";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
