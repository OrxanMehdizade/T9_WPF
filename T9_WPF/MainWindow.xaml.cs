using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public MainWindow()
        {
            InitializeComponent();
            SynvoOfMascaras();
            

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

        private void one_Click(object sender, RoutedEventArgs e)
        {

                MessageBox.Show("salam");
 
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D1)
            {
                
                MessageBox.Show("NumPad1 key pressed!");
            }
        }
    }
}
