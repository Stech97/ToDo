using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;

namespace ToDo
{
    public partial class MainWindow : Window
    {
        private readonly ResourceDictionary CustomResources = new() { Source = new Uri("pack://application:,,,/Resources.xaml") };

        public MainWindow()
        {
            InitializeComponent();
            InitMonths();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void LblNote_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtNote.Focus();
        }

        private void LblTime_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TxtTime.Focus();
        }

        private void TxtNote_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNote.Text) && txtNote.Text.Length > 0)
                lblNote.Visibility = Visibility.Collapsed;
            else
                lblNote.Visibility = Visibility.Visible;
        }

        private void TxtTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtTime.Text) && TxtTime.Text.Length > 0)
                LblTime.Visibility = Visibility.Collapsed;
            else
                LblTime.Visibility = Visibility.Visible;
        }


        private void InitMonths()
        {
            for (int m = 1; m <= 12; m++)
            {

                var but = new Button
                {
                    Content = m,
                    Style = (Style)CustomResources["buttonMonth"]
                };

                if (DateTime.Now.Month == m)
                {
                    but.Foreground = new SolidColorBrush(Color.FromRgb(13, 110, 253));
                    but.FontWeight = FontWeights.SemiBold;
                }

                Months.Children.Add(but);
            }
        }

    }
}