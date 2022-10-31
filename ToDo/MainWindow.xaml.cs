using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Globalization;
using System.Linq;

namespace ToDo
{
    public partial class MainWindow : Window
    {
        private readonly ResourceDictionary CustomResources = new() { Source = new Uri("pack://application:,,,/Resources.xaml") };

        public MainWindow()
        {
            InitializeComponent();
            InitStaticData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) => InitData();

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void LblNote_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TxtNote.Focus();
        }

        private void LblTime_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TxtTime.Focus();
        }

        private void TxtNote_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtNote.Text) && TxtNote.Text.Length > 0)
                LblNote.Visibility = Visibility.Collapsed;
            else
                LblNote.Visibility = Visibility.Visible;
        }

        private void TxtTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtTime.Text) && TxtTime.Text.Length > 0)
                LblTime.Visibility = Visibility.Collapsed;
            else
                LblTime.Visibility = Visibility.Visible;
        }


        private void InitStaticData()
        {
            for (int y = 2020; y <= 2024; y++)
            {

                var but = new Button
                {
                    Content = y,
                    Style = (Style)CustomResources["button"]
                };

                if (DateTime.Now.Year == y)
                {
                    but.Foreground = new SolidColorBrush(Color.FromRgb(13, 110, 253));
                    but.FontWeight = FontWeights.SemiBold;
                    but.FontSize = 24;
                }
                Years.Children.Add(but);
            }

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

        private void InitData()
        {
            string Month = DateTime.Now.ToString("MMMM", CultureInfo.CurrentCulture);

            CurrentMonth.Text = string.Concat(Month.First().ToString().ToUpper(), Month.AsSpan(1));
            SelectedDay.Text = DateTime.Now.Day.ToString();
            SelectedDayOfWeek.Text = DateTime.Now.ToString("dddd", CultureInfo.CurrentCulture);
            SelectedMonth.Text = string.Concat(Month.First().ToString().ToUpper(), Month.AsSpan(1));
            SelectedYear.Text = DateTime.Now.Year.ToString() + " год";
        }
    }
}