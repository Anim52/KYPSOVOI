using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Service;
using WpfApp1.ViewModel;

namespace WpfApp1.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для NumberPage.xaml
    /// </summary>
    public partial class NumberPage : UserControl
    {
        public NumberPage()
        {
            InitializeComponent();
            AddNomerViewModel addNomerViewModel = new AddNomerViewModel();
            this.DataContext = addNomerViewModel;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Effect = new BlurEffect { Radius = 10 };

            // Показываем затемнитель
            Overlay.Visibility = Visibility.Visible;

            // Открываем окно добавления номера
            var addNomerWindow = new NumberAdd();
            addNomerWindow.Owner = Window.GetWindow(this);
            addNomerWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addNomerWindow.ShowDialog();

            // Убираем размытие и затемнитель после закрытия окна
            MainGrid.Effect = null;
            Overlay.Visibility = Visibility.Collapsed;
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Effect = new BlurEffect { Radius = 10 };

            // Показываем затемнитель
            Overlay.Visibility = Visibility.Visible;

            // Открываем окно добавления номера
            var addNomerWindow = new NumberAdd();
            addNomerWindow.Owner = Window.GetWindow(this);
            addNomerWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addNomerWindow.ShowDialog();

            // Убираем размытие и затемнитель после закрытия окна
            MainGrid.Effect = null;
            Overlay.Visibility = Visibility.Collapsed;
        }

        private void EditNomer_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
