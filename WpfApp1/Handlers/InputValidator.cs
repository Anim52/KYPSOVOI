using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Service;

namespace WpfApp1.Handlers
{
    internal static class InputValidator
    {
        internal static bool IsValid<T>(T entity)
        {
            var result = false;
            switch (entity)
            {
                case User:
                    result = IsUserValid((entity as User)!);
                    break;
            }
            return result;
        }
        // Проверка User
        private static bool IsUserValid(User user)
        {
            if (InputHandler.Handle(user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
