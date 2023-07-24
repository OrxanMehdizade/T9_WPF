using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace T9_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<char, string> t9Mappings = new Dictionary<char, string>
        {
            {'2', "abc"},
            {'3', "def"},
            {'4', "ghi"},
            {'5', "jkl"},
            {'6', "mno"},
            {'7', "pqrs"},
            {'8', "tuv"},
            {'9', "wxyz"}
        };

        private StringBuilder inputBuffer = new();
        private List<string> Vocabulary = new();

        public MainWindow()
        {
            InitializeComponent();
            StartFillVocabulary();
            SynvoOfMascaras();
        }

        private void StartFillVocabulary()
        {
            string text = File.ReadAllText(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Vocabulary.txt"));
            Vocabulary = text.Split("\r\n").ToList();
        }

        Button lastClickedButton = null!;
        int clickCount = 1;

        #region Button Check

        public void ZeroOrSpace(TextBlock textBlock)
        {
            if (clickCount > 2) clickCount = 1;

            switch (clickCount)
            {
                case 1:
                    inputBuffer.Append(textBlock.Text);
                    break;
                case 2:
                    inputBuffer.Remove(inputBuffer.Length - 1, 1);
                    inputBuffer.Append(" ");
                    break;
            }
        }

        public string SevenOrNine(TextBlock textBlock)
        {
            if (clickCount > 5) clickCount = 1;
            switch (clickCount)
            {
                case 1:
                    return textBlock.Text;
                case 2:
                    inputBuffer.Remove(inputBuffer.Length - 1, 1);
                    return t9Mappings[textBlock.Text[0]][0].ToString();
                case 3:
                    inputBuffer.Remove(inputBuffer.Length - 1, 1);
                    return t9Mappings[textBlock.Text[0]][1].ToString();
                case 4:
                    inputBuffer.Remove(inputBuffer.Length - 1, 1);
                    return t9Mappings[textBlock.Text[0]][2].ToString();
                case 5:
                    inputBuffer.Remove(inputBuffer.Length - 1, 1);
                    return t9Mappings[textBlock.Text[0]][3].ToString();
            }
            return null!;
        }

        public string TwoToEight(TextBlock textBlock)
        {
            if (clickCount > 4) clickCount = 1;
            switch (clickCount)
            {
                case 1:
                    return textBlock.Text;
                case 2:
                    inputBuffer.Remove(inputBuffer.Length - 1, 1);
                    return t9Mappings[textBlock.Text[0]][0].ToString();
                case 3:
                    inputBuffer.Remove(inputBuffer.Length - 1, 1);
                    return t9Mappings[textBlock.Text[0]][1].ToString();
                case 4:
                    inputBuffer.Remove(inputBuffer.Length - 1, 1);
                    return t9Mappings[textBlock.Text[0]][2].ToString();
            }
            return null!;
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            bool zeroClicked = false;
            if (lastClickedButton == clickedButton) clickCount++;
            else
            {
                lastClickedButton = clickedButton;
                clickCount = 1;
            }

            string buttonText = clickedButton.Content.ToString()!;

            if (clickedButton.Content is Grid grid)
            {
                if (grid.Children.Count > 0 && grid.Children[0] is TextBlock textBlock) ZeroOrSpace(textBlock);
                zeroClicked = true;
            }
            else
            {

                if (clickedButton.Content is StackPanel stackPanel)
                {
                    if (stackPanel.Children.Count > 0 && stackPanel.Children[0] is TextBlock textBlock)
                    {
                        if (textBlock.Text != "9" && textBlock.Text != "7") buttonText = TwoToEight(textBlock);
                        else buttonText = SevenOrNine(textBlock);
                        inputBuffer.Append(buttonText);
                    }
                }
                else inputBuffer.Append(buttonText);
            }

            UpdateInputDisplay();
            if (!zeroClicked)
                Task.Run(() => { RecommendWord(); });


        }

        private void UpdateInputDisplay() => txtInput.Text = inputBuffer.ToString();

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (inputBuffer.Length != 0)
            {
                lastClickedButton = null!;
                clickCount = 1;
                inputBuffer.Remove(inputBuffer.Length - 1, 1);
                UpdateInputDisplay();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            inputBuffer.Clear();
            UpdateInputDisplay();
        }

        private void RecommendWord()
        {
            Dispatcher.Invoke(() =>
            {

                List<string> wordsWrited = txtInput.Text.Split(' ', '*', '#').ToList();
                string recommended = Vocabulary.FirstOrDefault(v => v.StartsWith(wordsWrited.Last()))!;
                if (recommended != null)
                {

                    int lastSpaceIndex = txtInput.Text.LastIndexOf(wordsWrited.Last());

                    int len = txtInput.Text.Length;

                    txtInput.Text = txtInput.Text[..lastSpaceIndex] + recommended;

                    txtInput.Select(len, txtInput.Text.Length);

                    txtInput.Focus();
                }
            });
        }

        private void txtInput_PreviewKeyDown(object sender, KeyEventArgs e) => e.Handled = true;


        private void C_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Clear();
        }

        public void SynvoOfMascaras()
        {
            toparrow.Content = $"^";
            leftarrow.Content = $"<";
            rightarrow.Content = $">";
            bottomarrow.Content = $"\\/";
            one.Content = $"1";
            two.Content = $"   2\nABC";
            three.Content = $"   3\nDEF";
            four.Content = $"   4\nGHI";
            five.Content = $"  5\nJKL";
            six.Content = $"   6\nMNO";
            seven.Content = $"   7\nPQRS";
            eight.Content = $"   8\nTUV";
            nine.Content = $"   9\nWXYZ";
            zero.Content = $"0\n+";
        }
    }
}