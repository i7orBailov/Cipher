using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Cipher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetAllDefault();
        }

        string DefaultKey()
        {
            string defaultKey = "Enter key";
            Key.Text = defaultKey;
            Key.Foreground = Brushes.Gray;
            return defaultKey;
        }

        string DefaultMessage()
        {
            string defaultMessage = "Enter message";
            Message.Text = defaultMessage;
            Message.Foreground = Brushes.Gray;
            return defaultMessage;
        }

        string DefaultResult(bool encrypt)
        {
            string defaultResult = encrypt ? "Encrypted message will be shown here" : "Decrypted message will be shown here";
            Result.Text = defaultResult;
            Result.Foreground = Brushes.Gray;
            return defaultResult;
        }

        void GetAllDefault(bool message = true, bool key = true)
        {
            if (key)
                Key.Text = DefaultKey();
            if (message)
                Message.Text = DefaultMessage();
            Result.Text = DefaultResult(encrypt: (string)Cipher.Content == "Encrypt" ? true : false);
        }

        void MessageBoxError(string messageBoxText) => MessageBox.Show(messageBoxText, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        void Cipher_Click(object sender, RoutedEventArgs e)
        {
            if (Message.Foreground == Brushes.Gray || string.IsNullOrWhiteSpace(Message.Text))
            {
                MessageBoxError("Message can`t be empty!");
                GetAllDefault(key: false);
            }
            else if (Key.Foreground == Brushes.Gray || string.IsNullOrWhiteSpace(Key.Text))
            {
                MessageBoxError("Key can`t be empty!");
                GetAllDefault(message: false);
            }
            else
            {
                if (ComboBox.SelectedIndex == 0)
                {
                    bool isNumber = int.TryParse(Key.Text, out int KeyNumber);
                    if (isNumber)
                    {
                        Result.Foreground = Brushes.Black;
                        if (KeyNumber >= 0 && KeyNumber <= 26)
                            Result.Text = (bool)CheckBoxDecrypt.IsChecked ? CaesarCipher.Decrypt(Message.Text, KeyNumber) : CaesarCipher.Encrypt(Message.Text, KeyNumber);
                        else
                        {
                            MessageBoxError("Rotation can`t be longer than 26 or shorter than 0!");
                            GetAllDefault(message: false);
                        }
                    }
                    else
                    {
                        MessageBoxError("Key must be number!");
                        GetAllDefault(message: false);
                    }
                }
                else
                {
                    if (Key.Text.Any(x => char.IsWhiteSpace(x) || char.IsNumber(x)))
                    {
                        MessageBoxError("Key should not contain spaces or numbers!");
                        GetAllDefault(message: false);
                    }
                    else
                    {
                        Result.Foreground = Brushes.Black;
                        Result.Text = (bool)CheckBoxDecrypt.IsChecked ? VigenereCipher.Decrypt(Message.Text, Key.Text) : VigenereCipher.Encrypt(Message.Text, Key.Text);
                    }
                }
            }
        }

        void CheckBoxDecrypt_Checked(object sender, RoutedEventArgs e)
        {
            Cipher.Content = "Decrypt";
            Result.Text = DefaultResult(encrypt: false);
        }

        void CheckBoxDecrypt_Unchecked(object sender, RoutedEventArgs e)
        {
            Cipher.Content = "Encrypt";
            Result.Text = DefaultResult(encrypt: true);
        }

        void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox.SelectedIndex == 0)
            {
                Key.MaxLength = 2;
                GetAllDefault(message: false);
            }
            else
            {
                Key.MaxLength = 30;
                GetAllDefault(message: false);
            }
        }

        void Message_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Message.Foreground == Brushes.Gray)
            {
                Message.Text = string.Empty;
                Message.Foreground = Brushes.Black;
            }
        }

        void Message_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Message.Text == string.Empty)
                Message.Text = DefaultMessage();
        }

        void Key_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Key.Foreground == Brushes.Gray)
            {
                Key.Text = string.Empty;
                Key.Foreground = Brushes.Black;
            }
        }

        void Key_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Key.Text))
                Key.Text = DefaultKey();
        }

        void Info_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => MessageBox.Show("\tWelcome to 'Cipher' program!" +
                            "\nIt helps to encrypt/decrypt message. 2 encryption methods are available. " +
                            "Make sure checkbox is checked to decrypt message.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}