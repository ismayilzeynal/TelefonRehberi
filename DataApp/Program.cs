using DataApp.Models;
using System.Text.RegularExpressions;
NorthwindContext db = new NorthwindContext();
MainMenu();



static void MainMenu()
{
    Console.WriteLine("-------------------- TELEFON REHBERİ --------------------\n");
    Console.WriteLine("Kullanıcı Ekleme İşlemi   : A");
    Console.WriteLine("Kullanıcı Listeleme       : L");
    Console.WriteLine("Kullanıcı Arama           : S");
    Console.WriteLine("Çıkmak İçin               : E");

    string Secim = Console.ReadLine();

    switch (Secim)
    {
        case "A":
            ChoiseA();
            break;
        case "B":
            Console.WriteLine("B seçildi");
            break;
        case "C":
            Console.WriteLine("C seçildi");
            break;
        case "D":
            Console.WriteLine("D seçildi");
            break;
        default:
            Console.WriteLine("Yanlış girdiniz");
            break;
    }
}



static void ChoiseA()
{
    Console.Clear();
    string Ad, Soyad, TelefonNumarasi, Email;
    Console.Write("Kullanıcının adı: ");
    Ad = Console.ReadLine();
    Console.Write("Kullanıcının  Soyadı: ");
    Soyad = Console.ReadLine();

    Console.Write("Telefon: ");
    TelefonNumarasi = Console.ReadLine();
    if (!CheckValidPhoneNumber(TelefonNumarasi))
        do
        {
            WrongInput();
            TelefonNumarasi = Console.ReadLine();
        } while (!CheckValidPhoneNumber(TelefonNumarasi));

    Console.Write("E-mail: ");

    Email = Console.ReadLine();
    if (!CheckValidMail(Email))
        do
        {
            WrongInput();
            Email = Console.ReadLine();
        } while (!CheckValidMail(Email));

    NextA(Ad, Soyad, TelefonNumarasi, Email);
}

static void NextA(string Ad, string Soyad, string TelefonNumarasi, string Email)
{
    NorthwindContext db = new NorthwindContext();


    db.Users.Add(new()
    {
        FirstName = Ad,
        LastName = Soyad,
        Phone = TelefonNumarasi,
        EmailAddress = Email
    });
    db.SaveChanges();
}



















static bool CheckValidPhoneNumber(string str)
{
    string regex = @"^\+994[0-9]{9}$|^\+994[\s-][0-9]{2}[\s-][0-9]{3}[\s-][0-9]{2}[\s-][0-9]{2}$";
    return Regex.IsMatch(str, regex, RegexOptions.IgnoreCase);
}

static bool CheckValidMail(string str)
{
    string regex = @"^[a-zA-Z0-9_]+@[a-zA-Z0-9]+\.[a-zA-Z]{1,3}$";
    return Regex.IsMatch(str, regex, RegexOptions.IgnoreCase);
}


static void WrongInput()
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Wrong Input! Please retry\n");
    Console.ForegroundColor = ConsoleColor.White;
}
























/*










 */














