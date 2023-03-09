using SimpleMedia;
Console.CancelKeyPress += new ConsoleCancelEventHandler((sender, eArgs) =>
{
    eArgs.Cancel = false;
    Server.Running = false;
    Console.BackgroundColor = ConsoleColor.DarkRed;
    Console.WriteLine("Enter Y to save database before exiting, N to exit without saving");
    string input = Console.ReadLine();
    if (input == "Y")
    {
        Database.SaveAllObjects();
    }
    Environment.Exit(0);
});

SimpleMedia.Database.LoadAllObjects();
if (args.Length > 0)
{
    foreach (var arg in args)
    {
        if (arg == "--UserCreate")
        {
            while (true)
            {
                Console.WriteLine("Username(or QUIT to quit): ");
                string username = Console.ReadLine();
                if (username == "QUIT")
                {
                    Database.SaveAllObjects();
                    break;
                }
                Console.WriteLine("Password: ");
                string password = Console.ReadLine();
                if (LoginManager.CreateUser(username, password))
                {
                    Console.WriteLine("User created");
                }
                else
                {
                    Console.WriteLine("User creation failed");
                }
            }
        }
        else if (arg == "--UserList")
        {
            foreach (var user in Database.GetObjects<User>())
            {
                Console.WriteLine(user.Username);
            }
        }
        else
        {
            Console.WriteLine("Unknown argument: " + arg);
        }
    }
}

SimpleMedia.Database.LoadAllObjects();

SimpleMedia.Server s = new SimpleMedia.Server();
Server.Instance = s;
s.Start();