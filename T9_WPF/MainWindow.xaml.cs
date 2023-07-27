using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace T9_WPF
{
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

        private List<string> recommendations = new List<string>();
        public List<string> vocabulary = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
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