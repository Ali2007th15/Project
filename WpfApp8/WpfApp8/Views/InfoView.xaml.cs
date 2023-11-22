using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WpfApp8.Views
{
    /// <summary>
    /// Interaction logic for InfoView.xaml
    /// </summary>
    public partial class InfoView : UserControl
    {
        private bool isMenuOpen = false;

        public InfoView()
        {
            InitializeComponent();
        }
        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuOpen)
            {

                ButtonMenu.SetResourceReference(StyleProperty, "OpenBtn");

                Storyboard sb = FindResource("CloseMenu") as Storyboard;
                sb.Begin();

                isMenuOpen = false;
            }
            else
            {
                ButtonMenu.SetResourceReference(StyleProperty, "CloseBtn");

                Storyboard sb = FindResource("OpenMenu") as Storyboard;
                sb.Begin();

                isMenuOpen = true;
            }

        }

        private void Shop_Click(object sender, RoutedEventArgs e)
        {
            Calc calc = new Calc();
            calc.Show();
        }
        private void Pets_Click(object sender, RoutedEventArgs e)
        {
            Calc calc = new Calc();
            calc.Show();
        }
        private void Tooth_Click(object sender, RoutedEventArgs e)
        {
            Calc calc = new Calc();
            calc.Show();
        }
        private void Taxi_Click(object sender, RoutedEventArgs e)
        {
            Calc calc = new Calc();
            calc.Show();
        }
        private void Car_Click(object sender, RoutedEventArgs e)
        {
            Calc calc = new Calc();
            calc.Show();
        }
        private void Drink_Click(object sender, RoutedEventArgs e)
        {
            Calc calc = new Calc();
            calc.Show();
        }
        private void Gift_Click(object sender, RoutedEventArgs e)
        {
            Calc calc = new Calc();
            calc.Show();
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Calc calc = new Calc();
            calc.Show();
        }
        private void Sports_Click(object sender, RoutedEventArgs e)
        {
            Calc calc = new Calc();
            calc.Show();
        }
        private void Phone_Click(object sender, RoutedEventArgs e)
        {
            Calc calc = new Calc();
            calc.Show();
        }
        private void _Click(object sender, RoutedEventArgs e)
        {
            Calc calc = new Calc();
            calc.Show();
        }
        private void Food_Click(object sender, RoutedEventArgs e)
        {
            Calc calc = new Calc();
            calc.Show();
        }
        private void Clothes_Click(object sender, RoutedEventArgs e)
        {
            Calc calc = new Calc();
            calc.Show();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Calc calc = new Calc();
            calc.Show();
        }





    }
}


