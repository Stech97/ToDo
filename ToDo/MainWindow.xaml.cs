using System;
using System.Linq;
using System.Windows;
using System.Globalization;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Threading;
using Notifications.Wpf.Core;

namespace ToDo
{
    public partial class MainWindow : Window
    {
        private readonly ResourceDictionary CustomResources = new() { Source = new Uri("pack://application:,,,/Resources.xaml") };
        private readonly DispatcherTimer DispatcherTimer;
        public MainWindow()
        {
            InitializeComponent();
            InitStaticData();
            InitData();
            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            DispatcherTimer.Interval = new TimeSpan(0,0,30);
            DispatcherTimer.Start();
        }

        private async void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            InitData();

            var notificationManager = new NotificationManager();

            await notificationManager.ShowAsync(new NotificationContent
            {
                Title = "Sample notification",
                Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Type = NotificationType.Information
            });
        }

        #region inparentProject
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
        #endregion

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedDate = Calendar.SelectedDate.Value;
            Calendar.DisplayDate = SelectedDate;
            ChangeDate(SelectedDate);
        }

        private void NextDay_Click(object sender, RoutedEventArgs e)
        {
            var CurentDate = Calendar.SelectedDate.GetValueOrDefault(DateTime.Now).AddDays(1);
            Calendar.SelectedDate = CurentDate;
        }

        private void PreviousDay_Click(object sender, RoutedEventArgs e)
        {
            var CurentDate = Calendar.SelectedDate.GetValueOrDefault(DateTime.Now).AddDays(-1);
            Calendar.SelectedDate = CurentDate;
        }

        private void BtnYear_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var CurentDate = Calendar.SelectedDate.GetValueOrDefault(DateTime.Now);
            CurentDate = CurentDate.AddYears((int)button.Content - CurentDate.Year);
            Calendar.SelectedDate = CurentDate;
        }

        private void NextYear_Click(object sender, RoutedEventArgs e)
        {
            var CurentDate = Calendar.SelectedDate.GetValueOrDefault(DateTime.Now).AddYears(1);
            Calendar.SelectedDate = CurentDate;
        }

        private void PreviousYear_Click(object sender, RoutedEventArgs e)
        {
            var CurentDate = Calendar.SelectedDate.GetValueOrDefault(DateTime.Now).AddYears(-1);
            Calendar.SelectedDate = CurentDate;
        }

        private void BtnMonth_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var CurentDate = Calendar.SelectedDate.GetValueOrDefault(DateTime.Now);
            CurentDate = CurentDate.AddMonths((int)button.Content - CurentDate.Month);
            Calendar.SelectedDate = CurentDate;
        }


        private void InitStaticData()
        {
            for (int y = 2020; y <= 2024; y++)
            {
                var button = new Button
                {
                    Content = y,
                    Style = (Style)CustomResources["button"]
                };

                button.Click += BtnYear_Click;

                if (DateTime.Now.Year == y)
                {
                    button.Foreground = new SolidColorBrush(Color.FromRgb(13, 110, 253));
                    button.FontWeight = FontWeights.SemiBold;
                    button.FontSize = 24;
                }
                Years.Children.Add(button);
            }

            for (int m = 1; m <= 12; m++)
            {

                var button = new Button
                {
                    Content = m,
                    Style = (Style)CustomResources["buttonMonth"]
                };

                button.Click += BtnMonth_Click;

                if (DateTime.Now.Month == m)
                {
                    button.Foreground = new SolidColorBrush(Color.FromRgb(13, 110, 253));
                    button.FontWeight = FontWeights.SemiBold;
                }

                Months.Children.Add(button);
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

        private void ChangeDate(DateTime dateTime)
        {
            string Month = dateTime.ToString("MMMM", CultureInfo.CurrentCulture);

            CurrentMonth.Text = string.Concat(Month.First().ToString().ToUpper(), Month.AsSpan(1));
            SelectedDay.Text = dateTime.Day.ToString();
            SelectedDayOfWeek.Text = dateTime.ToString("dddd");
            SelectedMonth.Text = string.Concat(Month.First().ToString().ToUpper(), Month.AsSpan(1));
            SelectedYear.Text = dateTime.Year.ToString() + " год";

            var ButtonMonth = Months.Children.Cast<Button>();

            ButtonMonth.ToList().ForEach(x => x.Foreground = new SolidColorBrush(Color.FromRgb(186, 186, 186)));
            ButtonMonth.FirstOrDefault(x => (int)x.Content == dateTime.Month).Foreground = new SolidColorBrush(Color.FromRgb(13, 110, 253));

            var ButtonYears = Years.Children.Cast<Button>();


            while (dateTime.Year + 2 > ButtonYears.Select(x => (int)x.Content).Last())
            {
                ButtonYears.ToList().ForEach(x => x.Content = (int)x.Content + 1);
            }

            while (dateTime.Year - 2 < ButtonYears.Select(x => (int)x.Content).First())
            {
                ButtonYears.ToList().ForEach(x => x.Content = (int)x.Content - 1);
            }

            ButtonYears.ToList().ForEach(x => x.Foreground = new SolidColorBrush(Color.FromRgb(186, 186, 186)));
            ButtonYears.FirstOrDefault(x => (int)x.Content == dateTime.Year).Foreground = new SolidColorBrush(Color.FromRgb(13, 110, 253));
        }
    }
}