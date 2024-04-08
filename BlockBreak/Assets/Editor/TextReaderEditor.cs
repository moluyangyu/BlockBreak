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
        //�ӽű��л�ȡ����
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
        EditorGUILayout.PropertyField(readedText, new GUIContent("�ı��ļ�"));
        EditorGUILayout.PropertyField(c_speed, new GUIContent("�ı���ʾ���ʱ��"));
        if (GUILayout.Button("Ӧ�ñ�ʶ������"))
        {
            SaveData();
        }
        if (UiStatic.TextNamesStatic==null)
        {
            Debug.Log("û�п��õı�ʶ�������ȵ�UiControl����и��±�ʾ���Ż��б�ʶ����ʾ");
        }else
        {
            int i = UiStatic.TextNamesStatic.Length;
            int[] iiis = new int[i];
            for (int j = 0; j < i; j++)
            {
                iiis[j] = j;//����һ����0��һ�Ĵ�С��textNamesһ���������
            }
            textReader.id = EditorGUILayout.IntPopup("��ʶ��", textReader.id, UiStatic.TextNamesStatic, iiis);
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
