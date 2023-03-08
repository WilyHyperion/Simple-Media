using SimpleMedia;
Console.CancelKeyPress += (sender, eArgs) => {
    
    Console.WriteLine("Enter Y to save database before exiting, N to exit without saving");
    string input = Console.ReadLine();
    if(input == "Y"){
        Database.SaveAllObjects();
    }
    Server.listener.Stop();
    Environment.Exit(0);
};
if (args.Length > 0)
{
    foreach (var arg in args)
    {
        if(arg == "--UserCreate"){
            while(true){
            Console.WriteLine("Username(or QUIT to quit): ");
            string username = Console.ReadLine();
            if(username == "QUIT"){
                Database.SaveAllObjects();
                break;
            }
            Console.WriteLine("Password: ");
            string password = Console.ReadLine();
            if(LoginManager.CreateUser(username, password)){
                Console.WriteLine("User created");
            }else{
                Console.WriteLine("User creation failed");
            }
        }
        }
        if(arg == "--ListUser"){
            
    }
    else{
        Console.WriteLine("Unknown argument: " + arg);
    }
}
}

SimpleMedia.Database.LoadAllObjects();

SimpleMedia.Server s = new SimpleMedia.Server();
Server.Instance = s;
s.Start();