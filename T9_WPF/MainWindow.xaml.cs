using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace T9_WPF
{
    public partial class MainWindow : Window
    {
        private int count = 0;

        private List<string> recommendations = new List<string>();
        public List<string> vocabulary = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            MessageBox.Show("Bəzi buttonların ilk hərifləri işlmiyə bilər ama axtarış klavyenen yazsaq textbox yazilmış herşeye görə axtarır");
            txtInput.TextChanged += TxtInput_TextChanged;
            LoadVocabulary("Vocabulary.txt");
            SynvoOfMascaras();

        }


        private void LoadVocabulary(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                vocabulary = lines.Select(line => line.Trim().ToLower()).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vocabulary faylını yükləyərkən səhv oldu: " + ex.Message);
            }
        }

        private async void TxtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            await Task.Delay(100); // Asenkron davranışdı gözlədirik kiçik müddətli

            // daxil etmə ilə eyni olanları göstərən LINQ
            string includedtxt = txtInput.Text.ToLower();
            recommendations = vocabulary.Where(kelime => kelime.StartsWith(includedtxt)).ToList();

            // göstərmək üçün olan metod 
            ShowingSuggestions();
        }

        private void ShowingSuggestions()
        {
            suggestionsPanel.Children.Clear();
            foreach (var item in recommendations)
            {
                var recommendationbtn = new Button
                {
                    Content = item,
                    Margin = new Thickness(5)
                };

                recommendationbtn.Click += suggestionbtn_Click;
                suggestionsPanel.Children.Add(recommendationbtn);
            }
        }

        private void suggestionbtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            string selectabletxt = btn.Content.ToString();
            txtInput.Text = selectabletxt;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInput.Text))
            {
                string newtxt = txtInput.Text.Trim();

                if (!string.IsNullOrEmpty(newtxt) && !recommendations.Contains(newtxt) && !vocabulary.Contains(newtxt.Trim().ToLower()))
                {
                    recommendations.Add(newtxt);

                    var suggestionbtn = new Button
                    {
                        Content = newtxt,
                        Margin = new Thickness(5)
                    };
                    suggestionbtn.Click += suggestionbtn_Click;
                    suggestionsPanel.Children.Add(suggestionbtn);

                    vocabulary.Add(newtxt);

                    try
                    {
                        File.AppendAllText("Vocabulary.txt", newtxt + Environment.NewLine);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Vocabulary faylına yazarkən səhv baş verdi:" + ex.Message);
                    }
                }
            }

            txtInput.Clear();
        }



        private void C_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Clear();
        }



    


        private void two_Click(object sender, RoutedEventArgs e)
        {
            switch (count)
            {
                case 0:
                    txtInput.Text += "a";
                    count++;
                    break;
                case 1:
                    txtInput.Text += "b";
                    count++;
                    break;
                case 2:
                    txtInput.Text += "c";
                    count++;
                    break;
                default:
                    count = 0;
                    break;
            }


        }//2
         
        private void three_Click(object sender, RoutedEventArgs e)
        {

            switch (count)
            {
                case 0:
                    txtInput.Text += "d";
                    count++;
                    break;
                case 1:
                    txtInput.Text += "e";
                    count++;
                    break;
                case 2:
                    txtInput.Text += "f";
                    break;

                default:
                    count = 0;
                    break;
            }

            
        }//3


        private void four_Click(object sender, RoutedEventArgs e)
        {
            switch (count)
            {
                case 0:
                    txtInput.Text += "g";
                    count++;
                    break;
                case 1:
                    txtInput.Text += "h";
                    count++;
                    break;
                case 2:
                    txtInput.Text +="i";
                    break;

                default:
                    count = 0;
                    break;
            }

        }//4

        private void five_Click(object sender, RoutedEventArgs e)
        {

            switch (count)
            {
                case 0:
                    txtInput.Text += "j";
                    count++;
                    break;
                case 1:
                    txtInput.Text += "k";
                    count++;
                    break;
                case 2:
                    txtInput.Text += "l";
                    break;

                default:
                    count = 0;
                    break;
            }


        }//5


        private void six_Click(object sender, RoutedEventArgs e)
        {
            switch(count){
                case 0:
                    txtInput.Text += "m";
                    count++;
                    break;
                case 1:
                    txtInput.Text +="n" ;
                    count++;
                    break;
                case 2:
                    txtInput.Text += "o";
                    break;

                default:
                    count = 0;
                    break;
            }

        }//6

        private void seven_Click(object sender, RoutedEventArgs e)
        {
            switch (count)
            {
                case 0:
                    txtInput.Text += "p";
                    count++;
                    break;
                case 1:
                    txtInput.Text +="q";
                    count++;
                    break;
                case 2:
                    txtInput.Text +="r";
                    break;
                case 3:
                    txtInput.Text +="s";
                    break;
                default:
                    count = 0;
                    break;
            }
        }

        private void eight_Click(object sender, RoutedEventArgs e)
        {
            switch (count)
            {
                case 0:
                    txtInput.Text +="t";
                    count++;
                    break;
                case 1:
                    txtInput.Text += "u";
                    count++;
                    break;
                case 2:
                    txtInput.Text +="v";
                    break;

                default:
                    count = 0;
                    break;
            }
        }

        private void nine_Click(object sender, RoutedEventArgs e)
        {

            switch (count)
            {
                case 0:
                    txtInput.Text +="w";
                    count++;
                    break;
                case 1:
                    txtInput.Text += "x";
                    count++;
                    break;
                case 2:
                    txtInput.Text +="y";
                    break;
                case 3:
                    txtInput.Text += "z";
                    break;

                default:
                    count = 0;
                    break;
            }
        }

        private void zero_Click(object sender, RoutedEventArgs e)
        {
            switch (count)
            {
                case 0:
                    txtInput.Text += "0";
                    count++;
                    break;
                case 1:
                    txtInput.Text +="+";
                    count++;
                    break;
                default:
                    count = 0;
                    break;
            }
        }

        private void star_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Text += "*";

        }

        private void hash_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Text += "#";
        }

        private void leftarrow_Click(object sender, RoutedEventArgs e)
        {
            TextBox textBox = txtInput;

            if (textBox.SelectionStart > 0)
            {
                textBox.SelectionStart--;
                textBox.SelectionLength = 0;
            }
            textBox.Focus();
        }

        private void rightarrow_Click(object sender, RoutedEventArgs e)
        {
            TextBox textBox = txtInput;
            if (textBox.SelectionStart > 0)
            {
                textBox.SelectionStart++;
                textBox.SelectionLength = 0;
            }
            textBox.Focus();
        }

        private void bottomarrow_Click(object sender, RoutedEventArgs e)
        {
            TextBox textBox = txtInput;
            int currentLine = textBox.GetLineIndexFromCharacterIndex(textBox.SelectionStart);
            if (currentLine < textBox.LineCount - 1)
            {
                int nextLineFirstCharIndex = textBox.GetCharacterIndexFromLineIndex(currentLine + 1);
                textBox.SelectionStart = nextLineFirstCharIndex;
                textBox.SelectionLength = 0;
            }
            textBox.Focus();
        }

        private void toparrow_Click(object sender, RoutedEventArgs e)
        {
            TextBox textBox = txtInput;
            int currentLine = textBox.GetLineIndexFromCharacterIndex(textBox.SelectionStart);
            if (currentLine > 0)
            {
                int prevLineFirstCharIndex = textBox.GetCharacterIndexFromLineIndex(currentLine - 1);
                textBox.SelectionStart = prevLineFirstCharIndex;
                textBox.SelectionLength = 0;
            }
            textBox.Focus();


        }
        public void SynvoOfMascaras()
        {
            toparrow.Content = $"^";
            leftarrow.Content = $"<";
            rightarrow.Content = $">";
            bottomarrow.Content = $"\\/";
            one.Content = $"1";
            two.Content = $"   2\nabc";
            three.Content = $"   3\ndef";
            four.Content = $"   4\nghi";
            five.Content = $"  5\njkl";
            six.Content = $"   6\nmno";
            seven.Content = $"   7\npqrs";
            eight.Content = $"   8\ntuv";
            nine.Content = $"   9\nwxyz";
            zero.Content = $"0\n+";
        }


    }
}