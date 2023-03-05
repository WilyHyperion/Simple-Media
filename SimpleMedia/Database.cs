namespace SimpleMedia;
public static class Database{
    public static string path = "Database.db";
    public static List<ISaveable> objects = new List<ISaveable>();
    public static void AddObject(ISaveable obj){
        objects.Add(obj);
        SaveAllObjects();
    }
    public static void SaveAllObjects(){
        String data = "";
        foreach(ISaveable obj in objects){
            data += obj.GetType().AssemblyQualifiedName + "**";
            data += obj.Save().Replace("%","@@@") + "%%%";
        }
        File.WriteAllText(path, data);
    }
    //TODO seprate lists for each ISaveable type,
    public static void LoadAllObjects(){
        if(!File.Exists(path)) return;
        String data = File.ReadAllText(path);

        String[] split = data.Split("%%%");
        foreach(String obj in split){
            if(obj == "") continue;
            String[] split2 = obj.Split("**");

            ISaveable obj2 = Activator.CreateInstance(Type.GetType(split2[0])) as ISaveable;
            obj2.Load(split2[1].Replace("@@@", "%%%"));
            objects.Add(obj2);
        }
    }
}