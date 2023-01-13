using DataApp.Models;
using System.ComponentModel;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

NorthwindContext db = new NorthwindContext();
MainMenu();
//DeleteUser();

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
        case "L":
            ChoiseL();
            Console.WriteLine("L seçildi");
            break;
        case "S":
            ChoiseS();
            Console.WriteLine("S seçildi");
            break;
        case "E":
            ChoiseE();
            Console.WriteLine("E seçildi");
            break;
        default:
            Console.Clear();
            WrongInput();
            MainMenu();
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
    while (!CheckValidPhoneNumber(TelefonNumarasi))
    {
        WrongInput();
        TelefonNumarasi = Console.ReadLine();
    }

    Console.Write("E-mail: ");
    Email = Console.ReadLine();
    while (!CheckValidMail(Email))
    {
        WrongInput();
        Email = Console.ReadLine();
    }

    AddUserToDb(Ad, Soyad, TelefonNumarasi, Email);
    NextChoiseA();
}

static void ChoiseL()
{
    Console.Clear();
    PrintUsers();

    Console.WriteLine("Istifadəçini düzənlə:     U");
    Console.WriteLine("Istifadəçini sil:         D");
    Console.WriteLine("Ana Menyu:                M");
    string choise= Console.ReadLine();

    switch (choise)
    {
        case "U":
            UpdateUser();
            break;
        case "D":
            DeleteUser();
            break;
        case "M":
            MainMenu();
            break;
        default:
            Console.Clear();
            WrongInput();
            ChoiseL();
            break;
    }

}

static void ChoiseS()
{
    NorthwindContext db = new NorthwindContext();
    Console.Clear();
    Console.WriteLine("Axtarış üçün simvol daxil edin: ");
    string SearchTip=Console.ReadLine();
    bool IsFound = false;
    var Users = db.Users.ToList();
    foreach (var user in Users)
    {
        if(user.FirstName.Contains(SearchTip) || user.LastName.Contains(SearchTip) || user.EmailAddress.Contains(SearchTip) || user.Phone.Contains(SearchTip) || user.EmployeeId.ToString().Contains(SearchTip))
        {
            Console.WriteLine($"{user.EmployeeId}\t {user.FirstName}\t\t {user.LastName}\t\t{user.EmailAddress}\t\t {user.Phone}");
            IsFound = true;
        }
    }
    if (!IsFound) Console.WriteLine("Heç bir nəticə tapılmadı");

    ChoiseL();
}

static void ChoiseE()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Proqramı istifadə etdiyiniz üçün təşəkkürlər");
}























static void UpdateUser()
{
    NorthwindContext db = new NorthwindContext();
    Console.Clear();
    PrintUsers();
    Console.WriteLine("Düzənlənəcək olan istifadəçinin ID-i daxil edin:  ");

    string InputId = Console.ReadLine();
    int IntegerId;
    while (!int.TryParse(InputId, out IntegerId))
    {
        WrongInput();
        InputId = Console.ReadLine();
    }

    string Ad, Soyad, TelefonNumarasi, Email;
    Console.Write("Kullanıcının adı: ");
    Ad = Console.ReadLine();
    Console.Write("Kullanıcının  Soyadı: ");
    Soyad = Console.ReadLine();

    Console.Write("Telefon: ");
    TelefonNumarasi = Console.ReadLine();
    while (!CheckValidPhoneNumber(TelefonNumarasi))
    {
        WrongInput();
        TelefonNumarasi = Console.ReadLine();
    }

    Console.Write("E-mail: ");
    Email = Console.ReadLine();
    while (!CheckValidMail(Email))
    {
        WrongInput();
        Email = Console.ReadLine();
    }

Update:
    Console.WriteLine(db.Users.Find(IntegerId).FirstName + " Dəyişikliyi təsdiqləyirsiz?  (Y/N)");
    string Choise = Console.ReadLine();
    if (Choise == "Y")
    {
        var updatedUser = db.Users.Find(InputId);
        updatedUser.FirstName = Ad;
        updatedUser.LastName = Soyad;
        updatedUser.Phone= TelefonNumarasi;
        updatedUser.EmailAddress= Email;
        bool updatedResult = db.SaveChanges() > 0;
        Console.Clear();
        Console.WriteLine(updatedResult ? "Kayıt Güncellendi" : "Kayıt Güncelleme İşlemi Hatalı");
        MainMenu();
    }
    else if (Choise == "N")
        MainMenu();
    else
    {
        WrongInput();
        goto Update;
    }

}

static void DeleteUser()
{
    NorthwindContext db = new NorthwindContext();
    Console.WriteLine("\n\nZəhmət olmasa silinəcək istifadəçinin ID-i daxil edin: ");
    string InputId=Console.ReadLine();
    int IntegerId;
    while(!int.TryParse(InputId, out IntegerId))
    {
        WrongInput();
        InputId = Console.ReadLine();
    }
    
    var deletedUser = db.Users.Find(IntegerId);
    Silmek:
    Console.WriteLine(db.Users.Find(IntegerId).FirstName + " Silməyi təsdiqləyirsiz?  (Y/N)");
    string Choise= Console.ReadLine();
    if (Choise == "Y")
    {
        db.Users.Remove(deletedUser);
        bool deleteResult = db.SaveChanges() > 0;
        Console.Clear() ;
        Console.WriteLine(deleteResult ? "Istifadəçi silindi" : "Silmə prosesində problem yaşandı\n");
        MainMenu();
    }
    else if (Choise == "N")
        ChoiseL();
    else
    {
        WrongInput();
        goto Silmek;
    }
}

static void PrintUsers()
{
    NorthwindContext db = new NorthwindContext();
    Console.WriteLine($"{"ID",-10} {"FirstName",-20} {"LastName",-25} {"Email",-30} {"Phone",-30}");

    var Users = db.Users.ToList();
    foreach (var user in Users)
    {
        Console.WriteLine($"{user.EmployeeId,-10} {user.FirstName,-20} {user.LastName,-25} {user.EmailAddress,-30} {user.Phone,-30}");
    }
}

static void NextChoiseA()
{
    Console.WriteLine("Yeni qeyd əlavə etmək      : A");
    Console.WriteLine("Ana Menyu                  : M");
    string choise = Console.ReadLine();
    switch (choise)
    {
        case "A":
            ChoiseA();
            break;
        case "M":
            MainMenu();
            break;
        default:
            Console.Clear();
            WrongInput();
            NextChoiseA();
            break;
    }
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

static void AddUserToDb(string Ad, string Soyad, string TelefonNumarasi, string Email)
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

