using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Dynamic;
using System.Globalization;
using System.Net.WebSockets;

namespace ATMLib
{
    
    public class Account
    {
        DataBaseConnection database = new DataBaseConnection();
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }
        public int CardNum { get; set; }
        public int CardBalance { get; set; }
        public string CardPaymentSystem { get; set; }
        public int CardPin { get; set; }

        /*  public Account(string name, string middleName, string lastName, string email, string phoneNum, int cardNum, int cardBalance, string cardPaymentSystem, int cardPin)
          {
              Name = name;
              MiddleName = middleName;
              LastName = lastName;
              Email = email;
              PhoneNum = phoneNum;
              CardNum = cardNum;
              CardBalance = cardBalance;
              CardPaymentSystem = cardPaymentSystem;
              CardPin = cardPin;
          }*/
        

       public string GetFullName(string cardNum)
        {
            string result = "";
            var fullNameQuery = $"select client_last_name,client_first_name,client_middle_name from client join bank_card on client.id_client = bank_card.id_bank_card where bank_card_number = '{cardNum}'";
               
            SqlCommand command = new SqlCommand(fullNameQuery, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result += $"{Convert.ToString(reader[0])} {Convert.ToString(reader[1])} {Convert.ToString(reader[2])}";
                
            }
            reader.Close();


            return result;
        }
        public string GetEmail(string cardNum)
        {
            string result = "";
            var emailQuery = $"select client_email from client join bank_card on client.id_client = bank_card.id_bank_card where bank_card_number = '{cardNum}'";
            SqlCommand command = new SqlCommand(emailQuery, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result += Convert.ToString(reader[0]);
            }
            reader.Close();
            return result;
        }
        public string GetPhoneNumber(string cardNum)
        {
            string result = "";
            string phoneQuery = $"select client_phone_number from client join bank_card on client.id_client = bank_card.id_bank_card where bank_card_number = '{cardNum}'";
            SqlCommand command = new SqlCommand(phoneQuery, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result += Convert.ToString(reader[0]);
            }
            reader.Close();
            return result;
        }
    }
}