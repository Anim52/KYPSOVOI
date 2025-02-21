using System.Configuration;
using System.Data;
using System.Windows;
using WpfApp1.Context;
using WpfApp1.Migrations;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            using(var context = new SqlServerContext())
            {
                context.Database.EnsureCreated();
            }

        }

        public static string CurrentUserLogin { get; set; }
        public static string CurrentUserRole { get; set; }

    }

}
