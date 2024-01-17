using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATM_MVVM_APP.Models;
namespace ATM_MVVM_APP.ViewModels
{
    public class CustomerViewModel
    {
        public bool isSuccess = false;
        public double currentCustomerBalance;
        public string currentCustomerName;

        public List<Customer> desCustomer = new List<Customer>();
        public CustomerViewModel()
        {
            CurrentCustomer = new Customer();
            CurrentCustomer.AccountNumber = null;
            CurrentCustomer.Password = "";
            desCustomer = CurrentCustomer.deserilization();
        }

        private Customer currentCustomer;
        public Customer CurrentCustomer { get { return currentCustomer; } set { currentCustomer = value; } }


        public bool SignUp()
        {
            bool notExistingAccount = true;
            foreach (Customer arg in desCustomer)
            {
                if (CurrentCustomer.AccountNumber == arg.AccountNumber)
                {
                    notExistingAccount = false;
                    return notExistingAccount;
                }
            }

            if(notExistingAccount)
            {
                Customer newCustomer = new Customer();
                newCustomer.AccountNumber = CurrentCustomer.AccountNumber;
                newCustomer.Password = CurrentCustomer.Password;
                newCustomer.Name = CurrentCustomer.Name;
                newCustomer.Balance = CurrentCustomer.Balance;

                desCustomer.Add(newCustomer);
                CurrentCustomer.serilization(desCustomer);
            }

            return notExistingAccount;
        }

        public bool Login()
        {
            bool isAccount = false;
            foreach (Customer arg in desCustomer)
            {
                if (CurrentCustomer.AccountNumber == arg.AccountNumber)
                {
                 //   isAccount = true;
                    if (CurrentCustomer.Password == arg.Password)
                    {
                        currentCustomerName= arg.Name;
                        currentCustomerBalance = arg.Balance;
                        isAccount = true;
                        return isAccount;
                    }
                }
            }
            return isAccount;
        }

        public bool Deposit()
        {
            isSuccess = false;
            foreach (Customer arg in desCustomer)
            {
                if (CurrentCustomer.AccountNumber == arg.AccountNumber)
                {
                    if (CurrentCustomer.Password == arg.Password)
                    {
                        if (CurrentCustomer.Balance >= 500)
                        {
                            arg.Balance = arg.Balance + CurrentCustomer.Balance;
                            currentCustomerBalance = arg.Balance;
                            isSuccess = true;
                            return isSuccess;
                        }
                    }
                }
            }
            return isSuccess;
        }

        public bool WithDraw()
        {
            isSuccess = false;
            foreach (Customer arg in desCustomer)
            {
                if (CurrentCustomer.AccountNumber == arg.AccountNumber)
                {
                    if (CurrentCustomer.Password == arg.Password)
                    {
                        if(CurrentCustomer.Balance < arg.Balance)
                        {
                            if (CurrentCustomer.Balance >= 500)
                            {
                                arg.Balance = arg.Balance - CurrentCustomer.Balance;
                                currentCustomerBalance = arg.Balance;
                                isSuccess = true;
                                return isSuccess;
                            }
                        }
                       
                    }
                }
            }
            return isSuccess;
        }
    }
}
