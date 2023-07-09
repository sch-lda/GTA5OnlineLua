using System.Runtime.InteropServices;
using System.Text;

namespace GTA5OnlineLua;

public static class IniHelper
{
    [DllImport("kernel32.dll")]
    private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

    [DllImport("kernel32.dll")]
    private static extern long WritePrivateProfileString(string section, byte[] key, byte[] val, string filePath);

    /// <summary>
    /// 读取节点值
    /// </summary>
    /// <param name="section"></param>
    /// <param name="key"></param>
    /// <param name="intPath"></param>
    /// <returns></returns>
    public static string ReadValue(string section, string key, string intPath)
    {
        var buffer = new byte[1024];
        var bufferLength = GetPrivateProfileString(section, key, string.Empty, buffer, buffer.GetUpperBound(0), intPath);
        return Encoding.UTF8.GetString(buffer, 0, bufferLength);
    }

    /// <summary>
    /// 写入节点值
    /// </summary>
    /// <param name="section"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="intPath"></param>
    public static void WriteValue(string section, string key, string value, string intPath)
    {
        WritePrivateProfileString(section, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(value), intPath);
    }
}
