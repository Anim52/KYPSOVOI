using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Command;
using WpfApp1.Handlers;
using WpfApp1.Service;

namespace WpfApp1.ViewModel
{
    public class AutorizationViewModel : ViewModelBase
    {
        public AutorizationViewModel()
        {
            RegisterCommand = new RelayCommand(o =>
            {
                var user = User.Create(lastname, firstname, middlename, login, password);
                if(InputValidator.IsValid(user))
                {

                }
            });
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }
        private string firstname = string.Empty;
        private string lastname = string.Empty;
        private string middlename = string.Empty;
        private string login = string.Empty;
        private string password = string.Empty;

        public string Firstname { get => firstname; set => Set(ref firstname, value, nameof(Firstname)); }
        public string Lastname { get => lastname; set => Set(ref lastname, value, nameof(Lastname)); }
        public string Middlename { get => middlename; set => Set(ref middlename, value, nameof(Middlename)); }
        public string Login { get => login; set => Set(ref login, value, nameof(Login)); }
        public string Password { get => password; set => Set(ref password, value, nameof(Password)); }

        public RelayCommand LoginCommand { get; }
        public RelayCommand RegisterCommand { get; }
        public RelayCommand CloseCommand { get; }

    }
}

