using QuanLyRung.ViewModel;
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

namespace QuanLyRung.UserControlDat
{
    /// <summary>
    /// Interaction logic for ControlBarUC.xaml
    /// </summary>
    public partial class ControlBarUC : UserControl
    {
        public ControlBarViewModel ViewModel { get; set; }

        public ControlBarUC()
        {
            InitializeComponent();
            this.DataContext = ViewModel = new ControlBarViewModel();
        }

        private void Button_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {

        }
    }
}
