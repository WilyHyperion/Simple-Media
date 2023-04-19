using System.Net;

namespace SimpleMedia.Models
{
    public static class LoginManager
    {
        public static User GetUser(string username)
        {
            foreach (User u in Database.GetObjects<User>())
            {
                if (u.Username.ToLower() == username.ToLower())
                {
                    return u;
                }
            }
            return null;
        }
        public static User GetUser(HttpListenerRequest r)
        {
            if (r.Cookies["LoginToken"] != null && r.Cookies["LoginToken"].Value != "")
            {
                String token = "";
                try
                {
                    token = (r.Cookies["LoginToken"].Value);
                }
                catch (System.FormatException)
                {
                    return null;
                }
                foreach (User u in Database.GetObjects<User>())
                {
                    if (u.Token == token)
                    {
                        return u;
                    }
                }
            }
                Console.WriteLine("Test");
            return null;
        }
        public static bool LoggedIn(HttpListenerRequest request)
        {

            if (request.Cookies["LoginToken"] != null && request.Cookies["LoginToken"].Value != "")
            {
                 String token = "";
                try
                {
                    token = request.Cookies["LoginToken"].Value;
                }
                catch (System.FormatException)
                {
                    return false;
                }
                foreach (User u in Database.GetObjects<User>())
                {
                    if (u.Token == token)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool CreateUser(String username, String password)
        {
            if (vaildUsername(username) && vaildPassword(password))
            {
                Database.AddObject(new User(username, password));
                return true;
            }
            return false;
        }
        //TODO more Vaildation
        private static bool vaildPassword(string password)
        {
            return true;
        }
        private static bool vaildUsername(string username)
        {
            foreach (User u in Database.GetObjects<User>())
            {
                if (u.Username.ToLower() == username.ToLower())
                {
                    return false;
                }
            }
            for(int i = 0; i < Util.invalidChars.Length; i++)
            {
                if (username.Contains(Util.invalidChars[i]))
                {
                    return false;
                }
            }
            return true;
        }
        public static User Login(String username, String password, HttpListenerRequest request, HttpListenerResponse r)
        {
            foreach (User u in Database.GetObjects<User>())
            {
                if (u.Username == username && u.Password == password)
                {
                    r.SetCookie(new Cookie("LoginToken", u.Token.ToString()));
                    return u;

                }
            }
            return null;
        }

        internal static void Logout(HttpListenerResponse r)
        {
            r.SetCookie(new Cookie("LoginToken", "out"));
        }

    }
}