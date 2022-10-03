using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace ATMLib
{
    public class AutomatedTellerMachine
    {
        DataBaseConnection database = new DataBaseConnection();
        public delegate void MessageHendler(object sender, Event e);
        public event MessageHendler BalaceEvent;
        public event MessageHendler AddMoneyEvent;
        public event MessageHendler AuthentificatinEvent;
        public event MessageHendler WithdrawMoneyEvent;
        public int IdATM { get; set; }
        public string AmountOfMoneyInATM;
        public string Adress { get; set; }

      

        public double GetBalance(string cardNum)
        {
            double result = 0;
            var balanceQuery = $"select bank_card_balance from bank_card where bank_card_number like {cardNum}";
            SqlCommand command = new SqlCommand(balanceQuery, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result += Convert.ToDouble(reader.GetDecimal(0));
            }
            reader.Close();
            return result;
        }
        public string AddMoney(int countOfMoney, long cardNum)
        {
            string notice = $"Картку успішно повнено на суму {countOfMoney}";
            string error = $"Помилка поповнення картки :((";
            var addMoneyBalance = $"update bank_card set bank_card_balance +={countOfMoney} where bank_card_number = {cardNum}";
            try
            {
                SqlCommand commandAddMoney = new SqlCommand(addMoneyBalance, database.getConnection());
                database.openConnection();
                commandAddMoney.ExecuteNonQuery();
                database.closeConnection();

                return notice;
            }
            catch
            {
                return error;
            }
            
        }
        public void EmailNotice(string recipientMail, string themeOfEmail, string textOfEmail)
        {
            string senderEmail = "biletskijzena@gmail.com";
            string senderPassword = "fanr spki cdqi wyha";
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            smtpClient.Send(senderEmail, recipientMail, themeOfEmail, textOfEmail);
        }

        public bool Login(string cardNum, string cardPin)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            bool login = false;

            if (!string.IsNullOrEmpty(cardNum) && !string.IsNullOrEmpty(cardPin))
            {
                var checkClientDataQuery = $"select * from bank_card where bank_card_number = {cardNum} and bank_card_pin = {cardPin}";

                
                SqlCommand commandCheckData = new SqlCommand(checkClientDataQuery, database.getConnection());
                database.openConnection();
                adapter.SelectCommand = commandCheckData;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                   login = true;
                }
                else
                {
                    login = false;
                }
            }
            return login;
        }
        public string SendMoneyTo(string cardNum, string sum, string destinationCard)
        {
            bool errorCheker = false;
            string errorNoCard = "Такої картки немає";
            string errorThisSameCard = "Ви не можете переказати кошти на свою картку";
            string errorSumLessThenOne = "Мінімальна сума переказу 1.00 UAH";
            string errorSumMoreThenBalance = "Недостатньо коштів для здійснення операції.";
            string result = "Гроші переведені";
            var checkCardNumberQuery = $"select bank_card_number from bank_card where bank_card_number = {destinationCard}";
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            SqlCommand commandCheckDestinationCard = new SqlCommand(checkCardNumberQuery, database.getConnection());
            database.openConnection();
            adapter.SelectCommand = commandCheckDestinationCard;
            adapter.Fill(table);

            if(table.Rows.Count > 0)
            {
                if(Convert.ToDouble(sum) < 1.00)
                {
                    return errorSumLessThenOne;
                    errorCheker = true;
                }
                if(cardNum == destinationCard)
                {
                    return errorThisSameCard;
                    errorCheker = true;
                }
                if (Convert.ToDouble(sum) > Convert.ToDouble(GetBalance(cardNum)))
                {
                    return errorSumMoreThenBalance;
                    errorCheker = true;
                }
                if(errorCheker == false)
                {
                    var transactionQueryOne = $"update bank_card set bank_card_balance -={sum} where bank_card_number = {cardNum}";
                    var transactionQuerySecond = $"update bank_card set bank_card_balance +={sum} where bank_card_number = {destinationCard}";
                    SqlCommand commandForTransactionOne = new SqlCommand(transactionQueryOne, database.getConnection());
                    SqlCommand commandForTransactionSecond = new SqlCommand(transactionQuerySecond, database.getConnection());
                    database.openConnection();
                    commandForTransactionOne.ExecuteNonQuery();
                    commandForTransactionSecond.ExecuteNonQuery();
                    database.closeConnection();
                }
                return result;
            }
            else
            {
                return errorNoCard;
            }

        }
        public string WithdrawMoney(string cardNum, double sumForWithdraw)
        {
            string error = "Withdara can't because not enough money";
            string successe = $"All is good take your {sumForWithdraw} y.o";
            if (GetBalance(cardNum) < sumForWithdraw)
            {
                return error;
            }
            else
            {
                var widhdrawMoneyQurery = $"update bank_card set bank_card_balance -={sumForWithdraw} where bank_card_number = {cardNum}";
                SqlCommand commandAddMoney = new SqlCommand(widhdrawMoneyQurery, database.getConnection());
                database.openConnection();
                commandAddMoney.ExecuteNonQuery();
                database.closeConnection();

                return successe;
            }
        }
    }
}
