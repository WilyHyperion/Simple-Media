namespace SimpleMedia;
public class User : ISaveable
{

    public User()
    {
    }
    public byte[] Profile;
    private string username;
    public string Username
    {
        get { return username; }
        set
        {
            Token = (value + Password).GetStableHashCode();
            username = value;
        }
    }
    private string password;
    public string Password
    {
        get { return password; }
        set
        {
            Token = (value + username).GetStableHashCode();
            password = value;
        }
    }
    public int Token;
    public User(string username, string password)
    {
        Username = username;
        Password = password;
        Profile = File.ReadAllBytes("Static/Images/Profile/Default.jpeg");
    }

    public void Load(Byte[] data)
    {
        using (BinaryReader r = new BinaryReader(new MemoryStream(data)))
        {
            int length = r.ReadInt32();
            Profile = r.ReadBytes(length);
            Username = r.ReadString();
            Password = r.ReadString();
        }
    }

    public byte[] Save()
    {
        using (var m = new MemoryStream())
        {
            using (BinaryWriter b = new BinaryWriter(m))
            {
                b.Write(Profile.Length);
                b.Write(Profile);
                b.Write(Username);
                b.Write(Password);
                return m.ToArray();
            }
        }
    }
    public override string ToString()
    {
        return $"{Username}:{Password}:{Token}";
    }
}