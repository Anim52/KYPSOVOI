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
using WpfApp1.PageModelViews;

namespace WpfApp1.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminServicePage.xaml
    /// </summary>
    public partial class AdminServicePage : UserControl
    {
        public AdminServicePage(Guid userId)
        {
            InitializeComponent();
            ServiceModelPage serviceModelPage = new ServiceModelPage(userId,true);
            this.DataContext = serviceModelPage;
        }

    }
}
