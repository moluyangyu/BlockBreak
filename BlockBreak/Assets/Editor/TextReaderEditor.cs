using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(TextReader))]

public class TextReaderEditor : Editor
{
    SerializedProperty readedText;
    SerializedProperty c_speed;
    private void OnEnable()
    {
        //从脚本中获取变量
        TextReader textReader = (TextReader)target;
        if(textReader.readedText!=null)
        {
#if UNITY_EDITOR
            string path = AssetDatabase.GetAssetPath(textReader.readedText);
            Debug.Log("Path of TextAsset: " + path);
            int i=UiStatic.LoadId(path);
            textReader.id = i;
#endif
        }
        readedText = serializedObject.FindProperty("readedText");
        c_speed= serializedObject.FindProperty("c_speed");
    }
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        TextReader textReader = (TextReader)target;
        serializedObject.Update();
        EditorGUILayout.PropertyField(readedText, new GUIContent("文本文件"));
        EditorGUILayout.PropertyField(c_speed, new GUIContent("文本显示间隔时间"));
        if (GUILayout.Button("应用标识名更改"))
        {
            SaveData();
        }
        if (UiStatic.TextNamesStatic==null)
        {
            Debug.Log("没有可用的标识名，请先到UiControl组件中更新表示名才会有标识名显示");
        }else
        {
            int i = UiStatic.TextNamesStatic.Length;
            int[] iiis = new int[i];
            for (int j = 0; j < i; j++)
            {
                iiis[j] = j;//生成一个从0到一的大小和textNames一样大的数组
            }
            textReader.id = EditorGUILayout.IntPopup("标识名", textReader.id, UiStatic.TextNamesStatic, iiis);
        }
        serializedObject.ApplyModifiedProperties();
    }
    private void SaveData()
    {
        TextReader textReader = (TextReader)target;
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(textReader.readedText);
        Debug.Log("Path of TextAsset: " + path);
        UiStatic.UpdateCSVAtLine(path, 0, textReader.id.ToString());
#endif

    }
}
