
using System.IO;
using LuaInterface;
public class LuaFileUtil : LuaFileUtils
{
    public static LuaFileUtil Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LuaFileUtil();
            }

            return instance;
        }

        protected set
        {
            instance = value;
        }
    }

    protected static LuaFileUtil instance = null;

    public LuaFileUtil()
    {
        instance = this;
    }


    public override byte[] ReadFile(string fileName)
    {
        byte[] bytes = new byte[] { };
        //UnityEngine.Debug.Log(fileName);
        if (bytes.Length == 0)
        {
            FileInfo file = Util.File.GetChildFile(Path.Combine(PathConst.RESOURCES, "./AB/lua"), fileName + ".lua.txt");
            if (file != null)
            {
                bytes = Util.File.ReadBytes(file.FullName);
            }
        }

        if (bytes.Length == 0)
        {
            bytes = base.ReadFile(fileName);
        }
        return bytes;
    }
}
