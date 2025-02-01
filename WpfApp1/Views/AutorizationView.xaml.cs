using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.ViewModel;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для AutorizationView.xaml
    /// </summary>
    public partial class AutorizationView : Window
    {
        public AutorizationView()
        {
            InitializeComponent();
            AuthViewModel authViewModel = new AuthViewModel();
            this.DataContext = authViewModel;
        }

        private void Registration_Btn(object sender, RoutedEventArgs e)
        {
            RegistrationView registrationView = new RegistrationView();
            registrationView.Show();
            this.Close();
        }
    }
}
