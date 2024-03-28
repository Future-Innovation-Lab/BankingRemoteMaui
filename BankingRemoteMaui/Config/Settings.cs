using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingRemoteMaui.Config
{
    public static class Settings
    {
        //https://localhost:7053/api/Auth

        public const string ServerAddress = "https://10.0.2.2:7053";
        public const string AuthControllerRoute = "/api/Auth/";
        public const string TransactionControllerRoute = "/api/Transaction";
        public const string BankAccountControllerRoute = "/api/BankAccount";
        public const string ClientControllerRoute = "/api/Client";
    }
}
