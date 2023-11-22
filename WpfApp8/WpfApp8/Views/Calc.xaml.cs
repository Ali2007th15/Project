using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Data;

namespace WpfApp8.Views
{
    /// <summary>
    /// Interaction logic for Calc.xaml
    /// </summary>
    public partial class Calc : Window
    {
        public Calc()
        {
            InitializeComponent();


            foreach (UIElement num in Calculator.Children)
            {
                if (num is Button)
                {
                    ((Button)num).Click += Button_Click;
                }


            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string text = (string)((Button)e.OriginalSource).Content;

            if (text == "AC")
            {
                textBox1.Text = "";
            }
            else if (text == "=")
            {
                string value = new DataTable().Compute(textBox1.Text, null).ToString();
                textBox1.Text = value;
            }
            else
                textBox1.Text += text;

        }
    }
}