using System.Net;

namespace SimpleMedia
{
    public class LoginManager
    {
        //TODO: Implement
        public static bool LoggedIn(HttpListenerRequest request)
        {
            Console.WriteLine("Checking login:" + request.Cookies["LoginToken"]);
            if(request.Cookies["LoginToken"] != null && request.Cookies["LoginToken"].Value != ""){
                Console.WriteLine("Token: " + request.Cookies["LoginToken"].Value);
                int token = 0;
                try
                {
                    token = Int32.Parse(request.Cookies["LoginToken"].Value);
                }
                catch (System.FormatException)
                {
                    return false;    
                }
                foreach (User u in Database.objects)
                {
                    Console.WriteLine("User token: " + u.Token);
                    if (u.Token == token)
                    {
                        return true;
                    }
                }
            }
            Console.WriteLine("Not logged in");
            return false;
        }
        public static bool CreateUser(String username, String password)
        {
            if(vaildUsername(username) && vaildPassword(password)){
                Database.AddObject(new User(username, password));
                return true;
            }
            return false;
        }

        private static bool vaildPassword(string password)
        {
            //TODO checks
            return true;
        }

        private static bool vaildUsername(string username)
        {
            foreach (User u in Database.objects)
            {
                if (u.Username == username)
                {
                    return false;
                }
            }
            return true;
        }
        public static User Login(String username, String password, HttpListenerRequest request)
        {
            foreach (User u in Database.objects)
            {
                if (u.Username == username && u.Password == password)
                {
                    return u;
                }
            }
            return null;
        }

    }
}