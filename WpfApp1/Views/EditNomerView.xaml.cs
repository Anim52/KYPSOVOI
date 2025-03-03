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
using System.Windows.Shapes;
using WpfApp1.PageModelViews;
using WpfApp1.Service;
using WpfApp1.ViewModel;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для EditNomerVieц.xaml
    /// </summary>
    public partial class EditNomerVieц : Window
    {
        public EditNomerVieц(Nomer SelectedNomer)
        {
            InitializeComponent();
            if (SelectedNomer != null)
            {
               EditNomerViewModel editNomerViewModel = new EditNomerViewModel(SelectedNomer);
                this.DataContext = editNomerViewModel;
                
                
            }
            else
            {
                MessageBox.Show("Выберите номер для редактирования.");
            }
        }
    }
}
