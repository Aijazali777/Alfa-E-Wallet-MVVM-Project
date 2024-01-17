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

using ATM_MVVM_APP.ViewModels;
namespace ATM_MVVM_APP.Views
{
    public partial class CustomerView : UserControl
    {
        bool isBtnDepositClicked = false;
        bool isBtnWithDrawClicked = false;
        bool isBtnShowBalanceClicked = false;
        bool isBtnExitClicked = false;

        CustomerViewModel ViewModel;
        public CustomerView()
        {
            InitializeComponent();
            ViewModel = new CustomerViewModel();
            this.DataContext = ViewModel;
            Welcome();
        }

        private void Welcome()
        {
            TxtWelcome.Text = "Welcome to Alfa E-Wallet";
            TxtMainOption.Text = "Please Select any option below to continue";

            TxtWelcome.Visibility = Visibility.Visible;
            TxtMainOption.Visibility = Visibility.Visible;
            SPMainChoice.Visibility = Visibility.Visible;
            SPLogin.Visibility = Visibility.Collapsed;
            SPSignUp.Visibility = Visibility.Collapsed;
            SPMenu.Visibility = Visibility.Collapsed;
            SPDepositAndWithdraw.Visibility = Visibility.Collapsed;
            TxtBalance.Visibility = Visibility.Collapsed;
            BtnBack.Visibility = Visibility.Collapsed;
            TxtSignupResponse.Visibility = Visibility.Collapsed;
        }

        private void ExistingUser(object sender, RoutedEventArgs e)
        {
            SPMainChoice.Visibility = Visibility.Collapsed;
            SPSignUp.Visibility = Visibility.Collapsed;
            SPLogin.Visibility = Visibility.Visible;

            TxtMainOption.Text = "Login";
            TxtResponse.Text = "";
        }

        private void NewUser(object sender, RoutedEventArgs e)
        {
            SPMainChoice.Visibility = Visibility.Collapsed;
            SPSignUp.Visibility = Visibility.Visible;

            TxtMainOption.Text = "SignUp";
            TxtSignupResponse.Text = "";
            TxtSignupResponse.Visibility = Visibility.Visible;
        }

        private void ClickSignUp(object sender, RoutedEventArgs e)
        {
            if (ValidateRequiredFileds())
            {
                bool success = ViewModel.SignUp();
                if(success)
                {
                    TxtSignupResponse.Text = "Account created successfully!";
                    ExistingUser(sender, e);
                }
                else
                {
                    TxtSignupResponse.Text = "This account number already exists. Try another one.";
                }
            }
        }

        bool ValidateRequiredFileds()
        {
            bool valid = true;
            if (ViewModel.CurrentCustomer.AccountNumber == "" || ViewModel.CurrentCustomer.AccountNumber == null)
            {
                TxtSignupResponse.Text = "Please enter an account number to register.";
                valid = false;
            }
            else if (ViewModel.CurrentCustomer.Password == "" || ViewModel.CurrentCustomer.Password == null)
            {
                TxtSignupResponse.Text = "Please enter a password to register.";
                valid = false;
            }
            else if (ViewModel.CurrentCustomer.Name == "" || ViewModel.CurrentCustomer.Name == null)
            {
                TxtSignupResponse.Text = "Please enter your name to register.";
                valid = false;
            }
            else if (ViewModel.CurrentCustomer.Balance < 500)
            {
                TxtSignupResponse.Text = "Please enter an amount of 500 or above to register.";
                valid = false;
            }
            return valid;
        }

        private void Clicklogin(object sender, RoutedEventArgs e)
        {
            TxtSignupResponse.Visibility = Visibility.Collapsed;
            bool isAccount = false;

            isAccount = ViewModel.Login();
       
            if (!isAccount)
            {
                TxtResponse.Text = "Incorrect account number or password";
            }
            else
            {
                TxtResponse.Text = "Login Successed";
                Menu();
            }
        }

        void Menu()
        {
            SPLogin.Visibility = Visibility.Collapsed;
            SPMenu.Visibility = Visibility.Visible;
            TxtMainOption.Text = "Welcome " + ViewModel.currentCustomerName + " :) ";
        }

        private void ClickDeposit(object sender, RoutedEventArgs e)
        {
            SPMenu.Visibility = Visibility.Collapsed;
            SPDepositAndWithdraw.Visibility = Visibility.Visible;
            TxtBalance.Visibility = Visibility.Collapsed;
            BtnBack.Visibility = Visibility.Visible;
            TxtBalance.Text = "";
            BtnDone.Content = "Deposit";
            TxtMainOption.Text = "Please enter an amount to deposit!";
            isBtnDepositClicked = true;
        }


        private void ClickWithdraw(object sender, RoutedEventArgs e)
        {
            SPMenu.Visibility = Visibility.Collapsed;
            SPDepositAndWithdraw.Visibility = Visibility.Visible;
            TxtBalance.Visibility = Visibility.Collapsed;
            BtnBack.Visibility = Visibility.Visible;
            TxtBalance.Text = "";
            BtnDone.Content = "Withdraw";
            TxtMainOption.Text = "Please enter an amount to Withdraw!";
            isBtnWithDrawClicked = true;
        }

        private void ClickShowBalance(object sender, RoutedEventArgs e)
        {
            SPMenu.Visibility = Visibility.Collapsed;
            SPDepositAndWithdraw.Visibility = Visibility.Collapsed;
            TxtBalance.Visibility = Visibility.Visible;
            BtnBack.Visibility = Visibility.Visible;
            isBtnShowBalanceClicked = true;
            isBtnDepositClicked = false;
            isBtnWithDrawClicked = false;
            ClickDone(sender, e);
        }

        private void ClickExit(object sender, RoutedEventArgs e)
        {
            SPMenu.Visibility = Visibility.Collapsed;
            isBtnExitClicked = true;
            ClickDone(sender, e);
        }

        private void ClickDone(object sender, RoutedEventArgs e)
        {
            SPDepositAndWithdraw.Visibility = Visibility.Collapsed;
            TxtBalance.Visibility = Visibility.Visible;
            BtnBack.Visibility = Visibility.Visible;
            bool success = false;

            if(isBtnDepositClicked)
            {
                success = ViewModel.Deposit();
                if (success)
                {
                    TxtMainOption.Text = "Amount Added Successfully!";
                    TxtBalance.Text = "Your new Balance is " + ViewModel.currentCustomerBalance;
                    isBtnDepositClicked = false;
                }
                else
                {
                    TxtMainOption.Text = "Please enter an amount of 500 or above to deposit";
                    TxtBalance.Visibility = Visibility.Collapsed;
                    SPDepositAndWithdraw.Visibility = Visibility.Visible;
                }

            }
            else if (isBtnWithDrawClicked)
            {
                success = ViewModel.WithDraw();
                if (success)
                {
                    TxtMainOption.Text = "Amount withdrawn successfully!";
                    TxtBalance.Text = "Your new Balance is " + ViewModel.currentCustomerBalance;
                    isBtnWithDrawClicked = false;
                }
                else
                {
                    if(ViewModel.CurrentCustomer.Balance > ViewModel.currentCustomerBalance)
                    {
                        TxtMainOption.Text = "You have insufficient balance. Please try another amount";
                    }
                    else
                    {
                        TxtMainOption.Text = "Please enter an amount of 500 or above to withdraw";
                    }
                    
                    TxtBalance.Visibility = Visibility.Collapsed;
                    SPDepositAndWithdraw.Visibility = Visibility.Visible;
                }
            }
            else if (isBtnShowBalanceClicked)
            {
                TxtMainOption.Text = "";
                TxtBalance.Text = "Your available Balance is " + ViewModel.currentCustomerBalance;
                isBtnShowBalanceClicked = false;
            }
            else if (isBtnExitClicked)
            {
                SPDepositAndWithdraw.Visibility = Visibility.Collapsed;
                TxtBalance.Visibility = Visibility.Collapsed;
                BtnBack.Visibility = Visibility.Collapsed;

                isBtnExitClicked = false;
                Welcome();
            }
            ViewModel.CurrentCustomer.serilization(ViewModel.desCustomer);
        }

        private void ClickBack(object sender, RoutedEventArgs e)
        {
            SPSignUp.Visibility = Visibility.Collapsed;
            TxtMainOption.Text = "";
            SPDepositAndWithdraw.Visibility = Visibility.Collapsed;
            TxtBalance.Visibility = Visibility.Collapsed;
            BtnBack.Visibility = Visibility.Collapsed;
            Menu();
        }
    }
}
