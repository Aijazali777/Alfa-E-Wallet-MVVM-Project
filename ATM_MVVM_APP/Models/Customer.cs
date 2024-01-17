using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace ATM_MVVM_APP.Models
{
    public class Customer
    {
        string accountNumber = "";
        string password="";
        string name = "";
        double balance = 0;

        public string AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        //XML File Path
        string path = @"C:\Users\aaijaz\source\repos\ATM_MVVM_APP\account_deatils.xml";

        // SERILIZATION
        public void serilization(List<Customer> customers)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>));
            using (StreamWriter writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, customers);
            }
        }

        // DESERILIZATION
        public List<Customer> deserilization()
        {
            List<Customer> desCust = new List<Customer>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>));
            using (StreamReader reader = new StreamReader(path))
            {
                desCust = (List<Customer>)serializer.Deserialize(reader);
            }
            return desCust;
        }
    }
}
