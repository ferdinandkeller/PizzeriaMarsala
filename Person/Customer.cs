using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    public class Customer : Person
    {
        public long CustomerID { get; set; }
        public DateTime FirstCommandDate { get; private set; }
        public double CommandsTotalValue { get; set; }

        public Customer(string last_name, string first_name, string address, long phone_number, DateTime first_command_date)
            : this(RandomGenerator.CreateID(), last_name, first_name, address, phone_number, first_command_date, 0)
        {
            // rien
        }
        public Customer(long customer_id, string last_name, string first_name, string address, long phone_number, DateTime first_command_date, double total_value_commands)
            : base(last_name, first_name, address, phone_number)
        {
            CustomerID = customer_id;
            FirstCommandDate = first_command_date;
            CommandsTotalValue = total_value_commands;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nPremière commande : {FirstCommandDate.ToShortDateString()} | Cumul des commandes : {CommandsTotalValue}";
        }

        public override string ToCSV()
        {
            return $"{CustomerID};" + base.ToCSV() + $";{FirstCommandDate.ToShortDateString()};{CommandsTotalValue}";
        }

        public static Customer CSVToClient(String client)
        {
            String[] infos = client.Split(';');
            DateTime first_command_date = infos.Length == 5 ? DateTime.Now : Convert.ToDateTime(infos[5]);
            double commands_total_value = infos.LongLength == 6 ? 0 : double.Parse(infos[6]);
            return new Customer(long.Parse(infos[0]), infos[1], infos[2], infos[3], long.Parse(infos[4]), first_command_date, commands_total_value);
        }

        public static int CompareTotalOrders(Customer c1, Customer c2)
        {
            return c1.CommandsTotalValue.CompareTo(c2.CommandsTotalValue);
        }
        
    }
}
