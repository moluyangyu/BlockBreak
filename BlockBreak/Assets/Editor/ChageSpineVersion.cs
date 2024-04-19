using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Linq;
public class ChageSpineVersion
{
    [MenuItem("���㹤��~~~/ChageSpineVersion", false, 2)]
    private static void ChageSpineVersion_()
    {
        // �����·�������spine��·��
        string[] files = Directory.GetDirectories(Application.dataPath + "/Resources/Spine");

        foreach (string item in files)
        {
            Change(Directory.GetFiles(item));
        }
    }

    /// <summary>
    /// �ı�.json�ļ�����spine�汾
    /// </summary>
    /// <param name="p_files"></param>
    public static void Change(string[] p_files)
    {
        foreach (string item in p_files)
        {
            if (item.EndsWith(".json"))
            {
                // �������õ���Newtonsoft������json�ļ��������������
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
        Debug.Log("Spine�汾�ı����!!!");
    }
}
