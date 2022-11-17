using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ToDo.UserControls
{
    public partial class MenuButton : UserControl
    {
        public MenuButton()
        {
            InitializeComponent();
        }


        public string Caption
        {
            get
            {
                return (string)GetValue(CaptionProperty);
            }

            set
            {
                SetValue(CaptionProperty, value);
            }
        }

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(string), typeof(MenuButton));


        public FontAwesome.WPF.FontAwesomeIcon Icon
        {
            get
            {
                return (FontAwesome.WPF.FontAwesomeIcon)GetValue(IconProperty);
            }

            set
            {
                SetValue(IconProperty, value);
            }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(FontAwesome.WPF.FontAwesomeIcon), typeof(MenuButton));


        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(MenuButton));

        public object CommandParameter
        {
            get
            {
                return GetValue(CommandParameterProperty);
            }
            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(MenuButton));


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Command.Execute(CommandParameter);
        }
    }
}