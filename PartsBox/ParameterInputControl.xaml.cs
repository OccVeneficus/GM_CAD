using System.Windows.Controls;

namespace PartsBox
{
    /// <summary>
    /// Interaction logic for ParameterInputControl.xaml
    /// </summary>
    public partial class ParameterInputControl : UserControl
    {
        /// <summary>
        /// Свойство для названия параметра.
        /// </summary>
        public string ParameterLabelName { get; set; }

        public ParameterInputControl()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
