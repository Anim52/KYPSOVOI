using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Logger
{
    public static class Logger
    {
        private static readonly string logFilePath = "Logs/log.txt"; // Файл логов

        static Logger()
        {
            // Создаем папку "Logs", если ее нет
            string logDir = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }
        }

        public static void Log(string message, string level = "INFO")
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";

            try
            {
                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка записи в лог: " + ex.Message);
            }
        }

        public static void Info(string message) => Log(message, "INFO");
        public static void Warning(string message) => Log(message, "WARNING");
        public static void Error(string message) => Log(message, "ERROR");
    }
}

