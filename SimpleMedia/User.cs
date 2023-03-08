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
    //TODO check for symbols in name that cause issues like :
    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public void Load(Byte[] data)
    {
        var str = System.Text.Encoding.UTF8.GetString(data);
        var split = str.Split(':');
        Username = split[0];
        Password = split[1];
        Token = Int32.Parse(split[2]);
        }

    public byte[] Save()
    {
        return System.Text.Encoding.UTF8.GetBytes(ToString());
    }
    public override string ToString()
    {
        return $"{Username}:{Password}:{Token}";
    }
}