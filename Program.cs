namespace GTA5OnlineLua;

internal class Program
{
    private const string host = "https://ghproxy.com/https://raw.githubusercontent.com/CrazyZhang666/GTA5OnlineLua/main";

    static void Main(string[] args)
    {
        var kiddionLua = new List<LuaInfo>();
        var yimMenuLua = new List<LuaInfo>();

        GetLuaInfo(kiddionLua, "Kiddion");
        GetLuaInfo(yimMenuLua, "YimMenu");

        JsonHelper.WriteFile("./Kiddion.json", kiddionLua);
        JsonHelper.WriteFile("./YimMenu.json", yimMenuLua);

        Console.WriteLine("操作结束!");
        Console.ReadLine();
    }

    private static void GetLuaInfo(List<LuaInfo> onlineLua, string dirName)
    {
        foreach (var file in Directory.GetFiles($"./{dirName}"))
        {
            var fileInfo = new FileInfo(file);

            if (fileInfo.Extension == ".ini")
            {
                var luaFile = fileInfo.FullName.Replace(".ini", "") + ".zip";
                var luaFileInfo = new FileInfo(luaFile);

                var name = IniHelper.ReadValue("LuaInfo", "Name", file);
                var author = IniHelper.ReadValue("LuaInfo", "Author", file);
                var description = IniHelper.ReadValue("LuaInfo", "Description", file);
                var version = IniHelper.ReadValue("LuaInfo", "Version", file);
                var update = IniHelper.ReadValue("LuaInfo", "Update", file);

                var size = GetFileSize(luaFileInfo.Length);
                var download = $"{host}/{dirName}/{luaFileInfo.Name}";

                onlineLua.Add(new()
                {
                    Name = name,
                    Author = author,
                    Description = description,
                    Version = version,
                    Update = update,
                    Size = size,
                    Download = download
                });
            }
        }
    }

    private static string GetFileSize(long size)
    {
        var kb = size / 1024.0f;

        if (kb > 1024)
            return $"{kb / 1024.0f:0.00} MB";
        else
            return $"{kb:0.00} KB";
    }
}
