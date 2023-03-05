using SimpleMedia;

SimpleMedia.Database.LoadAllObjects();
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
            foreach (User u in Database.objects)
            {
                Console.WriteLine(u.Username);
                Console.WriteLine(u.Password);
                Console.WriteLine(u.Token);
                Console.WriteLine();
            }
        }
    }
}

SimpleMedia.Server s = new SimpleMedia.Server();
Server.Instance = s;
s.Start();