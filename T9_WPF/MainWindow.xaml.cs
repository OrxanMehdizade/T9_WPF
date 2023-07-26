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

        private List<string> oneriler = new List<string>();
        public List<string> vocabulary = new List<string>();
 

        public MainWindow()
        {
            InitializeComponent();

            // Düğme tıklama olaylarını bağlıyoruz
            // ...

            // TextBox için TextChanged olayını bağlıyoruz
            txtInput.TextChanged += TxtInput_TextChanged;
            LoadVocabulary("Vocabulary.txt");
            SynvoOfMascaras();
        }

        private void LoadVocabulary(string filePath)
        {
            try
            {
                // Vocabulary.txt dosyasından kelimeleri okuyup vocabulary listesine ekliyoruz
                string[] lines = File.ReadAllLines(filePath);
                vocabulary = lines.Select(line => line.Trim().ToLower()).ToList();
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vocabulary dosyası yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        private async void TxtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            await Task.Delay(100); // Asenkron davranışı simüle etmek için küçük bir gecikme ekliyoruz (TPL)

            // Kullanıcının girdisiyle uygun kelime önerilerini buluyoruz
            string girdiMetni = txtInput.Text.ToLower();
            oneriler = vocabulary.Where(kelime => kelime.StartsWith(girdiMetni)).ToList();

            // Önerileri kullanıcıya gösteriyoruz
            GosterOneriler();
        }

        private void GosterOneriler()
        {

            suggestionsPanel.Children.Clear();
            foreach (var oneri in oneriler)
            {
                var oneriDugme = new Button
                {
                    Content = oneri,
                    Margin = new Thickness(5)
                };

                oneriDugme.Click += OneriDugme_Click;
                suggestionsPanel.Children.Add(oneriDugme);
            }
        }

        private void OneriDugme_Click(object sender, RoutedEventArgs e)
        {
            var dugme = (Button)sender;
            string secilenKelime = dugme.Content.ToString();
            txtInput.Text = secilenKelime;
        }







        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // "Add" adındaki butona tıklandığında buradaki kod çalışır
            // Bu örnekte "Add" butonuna tıklandığında TextBox'a girilen metni önerilere ekliyoruz.

            if (!string.IsNullOrEmpty(txtInput.Text))
            {
                string yeniOneri = txtInput.Text.Trim();

                // Yeni öneriyi zaten listede yoksa ve boş değilse ekleyelim
                if (!string.IsNullOrEmpty(yeniOneri) && !oneriler.Contains(yeniOneri) && !vocabulary.Contains(yeniOneri.Trim().ToLower()))
                {
                    oneriler.Add(yeniOneri);

                    // Yeni öneriyi düğme olarak ekleyelim
                    var oneriDugme = new Button
                    {
                        Content = yeniOneri,
                        Margin = new Thickness(5)
                    };
                    oneriDugme.Click += OneriDugme_Click;
                    suggestionsPanel.Children.Add(oneriDugme);

                    vocabulary.Add(yeniOneri);

                    // Güncellenmiş vocabulary listesini dosyaya geri yazıyoruz
                    try
                    {
                        File.AppendAllText("Vocabulary.txt", yeniOneri + Environment.NewLine);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Vocabulary dosyasına yazılırken bir hata oluştu: " + ex.Message);
                    }
                }
            }

            // TextBox'a girilen metni temizleyelim
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