using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(TextReader))]

public class TextReaderEditor : Editor
{
    SerializedProperty readedText;
    private void OnEnable()
    {
        //从脚本中获取变量
        readedText = serializedObject.FindProperty("readedText");
    }
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        TextReader textReader = (TextReader)target;
        serializedObject.Update();
        EditorGUILayout.PropertyField(readedText, new GUIContent("文本文件"));
        if(UiStatic.TextNamesStatic==null)
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
}
