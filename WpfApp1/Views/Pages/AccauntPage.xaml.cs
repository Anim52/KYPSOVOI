using System;
using System.Collections.Generic;
using System.Globalization;
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
using WpfApp1.PageModelViews;
using WpfApp1.Service;

namespace WpfApp1.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для AccauntPage.xaml
    /// </summary>
    public partial class AccauntPage : UserControl
    {
        public AccauntPage(Guid userId)
        {
            InitializeComponent();
            AccountViewModel accountViewModel = new AccountViewModel(userId);
            this.DataContext = accountViewModel;
        }
        
       
    }
}
