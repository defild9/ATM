using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using ATMLib;

/*Console.WriteLine("Name = ");
string name = Console.ReadLine();
Console.WriteLine("LastName = ");
string lastName = Console.ReadLine();

Account account = new Account(name, lastName);

Console.WriteLine($"{account.Name} {account.LastName}");*/

/*Account account = new Account();*/

AutomatedTellerMachine atm = new AutomatedTellerMachine();

/*Console.WriteLine("Enter CardNum");
string cardNum = Console.ReadLine();
Console.WriteLine("Enter CardOin");
string cardPin = Console.ReadLine();
string email = "ipz213_byev@student.ztu.edu.ua";
string theme = "Login";
string text = $"Are you login in {DateTime.Now}";

if (atm.Login(cardNum, cardPin) == true)
{
    Console.WriteLine("You are genius!! And you are login in ATM");
    Console.WriteLine($"Your balance: {atm.GetBalance(cardNum)}");
    atm.EmailNotice(email, theme, text);
    Console.WriteLine("Enter dist card");
    string desCard = Console.ReadLine();
    Console.WriteLine("Enter summ");
    string sum = Console.ReadLine();
    Console.WriteLine($"Transfer to {desCard} from {cardNum}: {sum}", atm.SendMoneyTo(cardNum, sum, desCard));
    Console.WriteLine($"Your balance: {atm.GetBalance(cardNum)}");

    atm.EmailNotice(email, theme, text);

}
else if (atm.Login(cardNum, cardPin) == false)
{
    Console.WriteLine("Fatal Error!");
}*/
Account ac = new Account();
Bank bank = new Bank();
string fullName = ac.GetFullName("4149499386487521");
string email = ac.GetEmail("4149499386487521");
string phone = ac.GetPhoneNumber("4149499386487521");
string adress = bank.GetAdress("1");
string nameBank = bank.GetBankName("1");
Console.WriteLine(fullName);
Console.WriteLine(email);
Console.WriteLine(phone);
Console.WriteLine($"BankName {nameBank}\nAndress: {adress}");


/*Console.WriteLine($"Balance: {account.GetBalance(i):D2}");*/

//Отправка сообщений 
/*for (int i = 0; i < 10; i++)
{
    var smtpClient = new SmtpClient("smtp.gmail.com")
    {
        Port = 587,
        UseDefaultCredentials = false,
        Credentials = new NetworkCredential("biletskijzena@gmail.com", "fanr spki cdqi wyha"),
        EnableSsl = true,
        DeliveryMethod = SmtpDeliveryMethod.Network
    };
    smtpClient.Send("biletskijzena@gmail.com", "ipz213_byev@student.ztu.edu.ua", $"Spam {i}", $"spam {i}");
}*/

