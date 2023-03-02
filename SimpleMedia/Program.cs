using SimpleMedia;

SimpleMedia.Database.LoadAllObjects();
if (args.Length > 0)
{
    foreach (var arg in args)
    {
        if(arg == "--User"){
            Console.WriteLine("Username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Password: ");
            string password = Console.ReadLine();
            if(LoginManager.CreateUser(username, password)){
                Console.WriteLine("User created");
            }else{
                Console.WriteLine("User creation failed");
            }
        }
        if(arg == "--ListUser"){
            foreach (User u in Database.objects)
            {
                Console.WriteLine(u.Username);
                Console.WriteLine(u.Password);
                Console.WriteLine(u.Token);
            }
        }
    }
}

SimpleMedia.Server s = new SimpleMedia.Server();
Server.Instance = s;
s.Start();