namespace SimpleMedia;
public class User : ISaveable
{

    public User(){

    }
    private string username;
    public string Username{
        get{ return username;}
        set{
            Token = (value + Password).GetStableHashCode();
            username = value;
        }
    }
    private string password;
    public string Password{
        get{ return password;}
        set{
            Token = (value + username).GetStableHashCode();
            password = value;
        }
    }
    public int Token;

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public void Load(string data)
    {
        string[] split = data.Split(':');
        Username = split[0];
        Password = split[1];
    }

    public string Save()
    {
        return $"{Username}:{Password}";
    }
    public override string ToString()
    {
        return $"Username: {Username}:Password:{Password}:Token:{Token}";
    }
}