using SimpleMedia.Models;
Console.CancelKeyPress += new ConsoleCancelEventHandler((sender, eArgs) =>
{
    eArgs.Cancel = false;
    Server.Running = false;
    Console.BackgroundColor = ConsoleColor.DarkRed;
    Console.WriteLine("Enter Y to save database before exiting, N to exit without saving");
    string input = Console.ReadLine();
    if (input == "Y")
    {
        Console.WriteLine("Saving database...");
        Database.SaveAllObjects();
    }
    Environment.Exit(0);
});

SimpleMedia.Models.Database.LoadAllObjects();
if (args.Length > 0)
{
    foreach (var arg in args)
    {
        if(arg == "--ClearPosts"){
            Console.WriteLine("Hit Enter to confirm");
            Console.ReadLine();

            foreach(var post in Database.GetObjects<Post>()){
                Database.RemoveObject(post);
                Database.SaveAllObjects();
            }
        }
        else if(arg == "--RemoveMessages"){
            Console.WriteLine("Hit Enter to confirm");
            Console.ReadLine();

            foreach(var message in Database.GetObjects<GlobalMessage>()){
                Database.RemoveObject(message);
                Database.SaveAllObjects();
            }

        }
        else if(arg == "--RemoveAllUsers"){
            Console.WriteLine("Hit Enter to confirm");
            Console.ReadLine();

            foreach(var user in Database.GetObjects<User>()){
                Database.RemoveObject(user);
                Database.SaveAllObjects();
            }

        }
        else if (arg == "--UserCreate")
        {
            while (true)
            {
                Console.WriteLine("Username(or QUIT to quit): ");
                string username = Console.ReadLine();
                if (username.ToLower() == "quit")
                {
                    Database.SaveAllObjects();
                    break;
                }
                Console.WriteLine("Password: ");
                string password = Console.ReadLine();
                bool t = LoginManager.CreateUser(username, password);
                if (t)
                {
                    Console.WriteLine("User created");
                }
                else
                {
                    Console.WriteLine("User creation failed");
                }
                User u = LoginManager.GetUser(username);
                if (u != null)
                {
                    Console.WriteLine("Bio: ");
                    u.Bio = Console.ReadLine();
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

SimpleMedia.Models.Database.LoadAllObjects();

SimpleMedia.Models.Server s = new SimpleMedia.Models.Server();
Server.Instance = s;
s.Start();