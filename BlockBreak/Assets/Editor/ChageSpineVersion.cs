using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Linq;
public class ChageSpineVersion
{
    [MenuItem("杂鱼工具~~~/ChageSpineVersion", false, 2)]
    private static void ChageSpineVersion_()
    {
        // 这里的路径是你放spine的路径
        string[] files = Directory.GetDirectories(Application.dataPath + "/Resources/Spine");

        foreach (string item in files)
        {
            Change(Directory.GetFiles(item));
        }
    }

    /// <summary>
    /// 改变.json文件里面spine版本
    /// </summary>
    /// <param name="p_files"></param>
    public static void Change(string[] p_files)
    {
        foreach (string item in p_files)
        {
            if (item.EndsWith(".json"))
            {
                // 我这里用的是Newtonsoft来操作json文件你可以用其他的
                JObject jo = JObject.Parse(File.ReadAllText(item, Encoding.UTF8));
                JToken jt = jo["skeleton"]["spine"];

                if (jt != null && jt.Type == JTokenType.String && jt.ToString() != "3.8")
                {
                    jo["skeleton"]["spine"] = "3.8";
                    File.WriteAllText(item, JsonConvert.SerializeObject(jo, Formatting.Indented));
                }
            }
        }

        AssetDatabase.Refresh();
        Debug.Log("Spine版本改变完成!!!");
    }
}
