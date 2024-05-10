using System;
using System.ComponentModel.Design;
using System.Security.Principal;
using System.Text.Json;
using System.Xml.Linq;

class Program
{
    //variables declarations
    public double amount;
    public string[] Cart;
    public bool[] inCartS;
    public bool[] inCartR;
    public bool[] inCartF;
    public string genre;
    public int cartN;

    //defining class User
    public class User
    {
        public string Username { get; set; }

        public virtual void Login()
        {
            Console.WriteLine("Logging in..");
        }
        public virtual void Logout()
        {
            Console.WriteLine("Logging out..");
        }

    }

    //Defining derived class
    public class Admin : User //inheritance example
    {
        public string Username { get; set; }

        public override void Login() //Polymorphism
        {
            base.Login(); // Call Login on base class
            Console.WriteLine($"User {Username} has been logged in.");

        }
        public override void Logout()
        {
            base.Logout(); // Call Logout on base class
            Console.WriteLine($"User {Username} has been logged out. ");
        }
    }

    //Defining derived class
    public class Guest : User //inheritance example
    {
        public string Username { get; set; }

        public override void Login()
        {
            base.Login(); // Call Login on base class
            Console.WriteLine($"User {Username} has been logged in.");
        }
        public override void Logout()
        {
            base.Logout(); // Call Logout on base class
            Console.WriteLine($"User {Username} has been logged out. ");
        }

    }

    //defining an arrays
    readonly string[] Strategy = { "Europa Universalis IV | 39,99€", "XCOM 2 | 49,99€", "Frostpunk | 29,99€" };
    readonly float[] StrategyP = { 39.99f, 49.99f, 29.99f };
    readonly string[] RPG = { "The Witcher 3: Wild Hunt | 29,99€ ", "God of War | 49,99€", "Baldur's Gate 3 | 59,99€" };
    readonly float[] RPGP = { 29.99f, 49.99f, 59.99f };
    readonly string[] FPS = { "Battlefield V | 49,99€", "Call of Duty: Modern Warfare III | 69,99€", "Hunt: Showdown | 39,99€" };
    readonly float[] FPSP = { 49.99f, 69.99f, 39.99f };




    //Overloading methods
    public class Overload
    {
        public void LoginOverloading(string OverName)
        {
            Console.WriteLine($"Hi {OverName}!");
        }
        public void LoginOverloading(string OverName, int OverAge)
        {
            Console.WriteLine($"Hi {OverName}, {OverAge}!");
        }
    }


    //Main
    static public void Main(String[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        LoggingIn();


        //variables declarations
        var program = new Program();
        program.cartN = 0;
        program.amount = 0;
        program.Cart = new string[9];
        program.inCartS= new bool[3];
        program.inCartR = new bool[3];
        program.inCartF = new bool[3];

        //self-repeating menu
        bool menu = true;
        do
        {
            Console.WriteLine();
            Console.WriteLine("1. Display games");
            Console.WriteLine("2. Add game to cart");
            Console.WriteLine("3. Go to checkout");
            Console.WriteLine("4. Check receipt");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string opcja = Console.ReadLine();

            switch (opcja)
            {
                case "1":
                    program.DisplaybyGenre();
                    break;

                case "2":
                    program.DisplaybyGenre();
                    program.AddToCart();
                    break;

                case "3":
                    program.Checkout();
                    break;

                case "4":
                    program.Receipt();
                    break;

                case "5":
                    menu = false;
                    break;
            }

        } while (menu == true);




    }

    //Methods

    public static void LoggingIn()
    {
        List<User> users = new List<User>();
        Console.Write("Choose account (Admin/Guest): ");
        string name = Console.ReadLine();
        string nameLower = name.ToLower(); //systematization

        if (nameLower == "admin")
        {
            Console.Write("Password: ");
            string password = Console.ReadLine();
            if (password == "zaq1@WSX")
            {
                Admin admin1 = new Admin();
                admin1.Username = "GamingRhapsody Employee";

                users.Add(admin1);
                LoginAdmin(users); //Calling a method
            }
            else
            {
                Console.WriteLine("Wrong password, try again. ");
                LoggingIn();
            }
        }
        else
        {
            Console.Write("Hi, enter your nickname: ");
            string OverName = Console.ReadLine();
            Console.Write("And your age (press Enter to skip it): ");
            string OverAge = Console.ReadLine();
            Overload ov = new Overload();
            if (OverAge == "")
            {
                ov.LoginOverloading(OverName);
            }
            else
            {
                int OverAgeInt = Convert.ToInt16(OverAge);
                ov.LoginOverloading(OverName, OverAgeInt);
            }
            Guest guest1 = new Guest();
            guest1.Username = OverName;
            users.Add(guest1);

            LoginGuest(users);  //Calling a method
        }
    }

    public void DisplaybyGenre()
    {
        Console.Write("Type genre to see (Strategy/RPG/FPS): ");
        genre = Console.ReadLine();
        Console.WriteLine();
        genre = genre.ToLower(); //systematization

        switch (genre)
        {
            case "strategy":
                foreach (string game in Strategy)
                {
                    Console.WriteLine(game);
                }
                break;
            case "rpg":
                foreach (string game in RPG)
                {
                    Console.WriteLine(game);
                }
                break;
            case "fps":
                foreach (string game in FPS)
                {
                    Console.WriteLine(game);
                }
                break;
            default:
                Console.WriteLine("Invalid category");
                DisplaybyGenre();
                break;
        }
    }

    public void AddToCart()
    {
        int position;
        Console.WriteLine();
        Console.Write("Which position you want to buy? (1-3): ");
        string line = Console.ReadLine();
        try
        {
            position = Convert.ToInt32(line); //tries to convet string to int
            if (position >= 1 && position <= 3) { 

                switch (genre)
                {
                case "strategy":
                        if (inCartS[position - 1] != true)
                        {
                            Cart[cartN] = Strategy[position - 1];
                            Console.WriteLine(Cart[cartN]);
                            cartN++;
                            amount += StrategyP[position - 1];
                            inCartS[position - 1] = true;
                        }
                        else
                        {
                            Console.WriteLine("You already have this game in your cart");
                        }
                    break;
                case "rpg":
                        if (inCartR[position - 1] != true)
                        {
                            Cart[cartN] = RPG[position - 1];
                            Console.WriteLine(Cart[cartN]);
                            cartN++;
                            amount += RPGP[position - 1];
                            inCartR[position - 1]=true;
                        }
                        else
                        {
                            Console.WriteLine("You already have this game in your cart");
                        }
                    break;
                case "fps":
                        if (inCartF[position - 1] != true)
                        {
                            Cart[cartN] = FPS[position - 1];
                            Console.WriteLine(Cart[cartN]);
                            cartN++;
                            amount += FPSP[position - 1];
                            inCartF[position-1]=true;
                        }
                        else
                        {
                            Console.WriteLine("You already have this game in your cart");
                        }
                    break;
                default:
                    Console.WriteLine("Invalid Category");
                    break;
                }
            }
            else
            {
                Console.WriteLine("Invalid position");
                AddToCart();
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid position");
            AddToCart();
        }
    }

    async public void Checkout()
    {
        Console.WriteLine();
        Console.WriteLine("This is your cart: ");
        foreach (string game in Cart)
        {
            if (game != null)
                Console.WriteLine(game);
        }
        amount = Math.Round(amount, 2);
        Console.WriteLine($"The full amount is: {amount}€");
        Console.Write("Do you want to procced with the purchase? (Y/N): ");
        string purchase = Console.ReadLine();
        purchase=purchase.ToLower();
        Console.WriteLine();
        if (purchase == "y")
        {
            await using FileStream createStream = File.Create("receipt.json");
            string jsonString = JsonSerializer.Serialize(Cart);
            await JsonSerializer.SerializeAsync(createStream, Cart);
            Console.WriteLine("Purchase successful");
            purchaseConfirmed();
        }
        else
        {
            Console.WriteLine("Purchase aborted");
        }
    }

    public void purchaseConfirmed()
    {
        Cart = new string[9];
        cartN = 0;
        amount = 0;
        inCartS = new bool[3];
        inCartR = new bool[3];
        inCartF = new bool[3];
    }
    public void Receipt()
    {
        try
        {
            Console.WriteLine(File.ReadAllText("receipt.json"));
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found");
        }
    }

    static void LoginAdmin(List<User> users)
    {
        foreach (User Admin in users)
        {
            Admin.Login(); //Calls a new method
        }
    }
    static void LoginGuest(List<User> users)
    {
        foreach (User Guest in users)
        {
            Guest.Login(); //Calls a new method
        }
    }
}