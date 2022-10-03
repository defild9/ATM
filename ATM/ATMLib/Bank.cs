using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMLib
{
    public class Bank
    {
        DataBaseConnection database = new DataBaseConnection();
        public int idATM { get; set; }
        public string bankName { get; set; }

        public string GetBankName(string idAtm)
        {
            string result = "";
            var bankNameQuery = $"select bank_name from AutomatedTellerMachine join bank on bank.id_bank = AutomatedTellerMachine.id_atm where id_atm = {idAtm}";
            SqlCommand command = new SqlCommand(bankNameQuery, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result += Convert.ToString(reader[0]);
            }
            reader.Close();
            return result;
        }
        public string GetAdress(string idAtm)
        {
            string result = "";
            var bankNameQuery = $"select atm_adress_country,atm_adress_city, atm_adress_street from AutomatedTellerMachine join bank on bank.id_bank = AutomatedTellerMachine.id_atm where id_atm = {idAtm}";
            SqlCommand command = new SqlCommand(bankNameQuery, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result += $"{Convert.ToString(reader[0])} {Convert.ToString(reader[1])} {Convert.ToString(reader[2])}";

            }
            reader.Close();


            return result;
        }
    }
}
