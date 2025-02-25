﻿using System;
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
using WpfApp1.ViewModel;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        public RegistrationView()
        {
            InitializeComponent();
           
            RegisterViewModel registerViewModel = new RegisterViewModel();
            this.DataContext = registerViewModel;
        }

        private void Login_Btn(object sender, RoutedEventArgs e)
        {
            AutorizationView autorizationView = new AutorizationView();
            autorizationView.Show();
            this.Close();
        }
    }
}
