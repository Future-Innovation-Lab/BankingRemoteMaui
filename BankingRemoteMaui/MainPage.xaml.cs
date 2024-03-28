using BankingRemoteApi.Models;
using BankingRemoteCore.Models;
using BankingRemoteMaui.Interfaces;
using BankingRemoteMaui.Services;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace BankingRemoteMaui
{
    public partial class MainPage : ContentPage
    {
        private IBankingService _bankingService;

        private Client _currentClient;

        public Client CurrentClient
        {
            get { return _currentClient; }
            set
            {
                _currentClient = value;

                OnPropertyChanged();

            }
        }

        private BankAccount _bankAccount;

        public BankAccount FirstBankAccont
        {
            get { return _bankAccount; }
            set
            {
                _bankAccount = value;

                OnPropertyChanged();
            }
        }


        private ObservableCollection<Transaction> _transactions;

        public ObservableCollection<Transaction> Transactions
        {
            get { return _transactions; }
            set
            {
                _transactions = value;

                OnPropertyChanged();

            }
        }

        public MainPage(IBankingService bankingService)
        {
            InitializeComponent();

            BindingContext = this;

            _bankingService = bankingService;

        }

        public MainPage(BankingService bankingService)
        {
            InitializeComponent();

            _bankingService = bankingService;

            BindingContext = this;

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await LoadData();
        }


        private async void ReloadButton_Clicked(object sender, EventArgs e)
        {
            await LoadData();

        }

        private async Task LoadData()
        {
            try
            {

                Client client = await _bankingService.VerifyLogin(new Auth() { Username = "Bob", Password = "12345" });


                CurrentClient = client;

                if (CurrentClient != null)
                {

                    FirstBankAccont = CurrentClient.BankAccounts.FirstOrDefault();

                    if (FirstBankAccont != null)
                    {
                        Transactions = new ObservableCollection<Transaction>(await _bankingService.GetTransactionsByBankAccountId(FirstBankAccont.BankAccountId));
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await _bankingService.UpdateClient(CurrentClient);

                await DisplayAlert("Success","Client Saved", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Cancel");
            }

        }
    }

}
