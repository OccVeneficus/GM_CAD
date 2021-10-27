using System.Windows;
using System.Windows.Controls;

namespace PartsBox.Views
{
    /// <summary>
    /// Interaction logic for ParameterInputControl.xaml
    /// </summary>
    public partial class ParameterInputControl : UserControl
    {
        private static readonly DependencyProperty TextBoxValueProperty =
            DependencyProperty.Register(
                nameof(TextBoxValue),
                typeof(string),
                typeof(ParameterInputControl));

        private static readonly DependencyProperty ParameterLabelNameProperty =
            DependencyProperty.Register(
                nameof(ParameterLabelName),
                typeof(string),
                typeof(ParameterInputControl));

        /// <summary>
        /// Свойство для названия параметра.
        /// </summary>
        public string ParameterLabelName 
        {
            get
            {
                return (string)GetValue(ParameterLabelNameProperty);
            }
            set
            {
                SetValue(ParameterLabelNameProperty, value);
            }
        }

        /// <summary>
        /// Свойство для значения введенного в TextBox.
        /// </summary>
        public string TextBoxValue 
        {
            get
            {
                return (string)GetValue(TextBoxValueProperty);
            }
            set
            {
                SetValue(TextBoxValueProperty, value);
            }
        }


        public ParameterInputControl()
        {
            InitializeComponent();
        }
    }
}
