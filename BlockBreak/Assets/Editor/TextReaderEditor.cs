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
        //�ӽű��л�ȡ����
        readedText = serializedObject.FindProperty("readedText");
    }
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        TextReader textReader = (TextReader)target;
        serializedObject.Update();
        EditorGUILayout.PropertyField(readedText, new GUIContent("�ı��ļ�"));
        if(UiStatic.TextNamesStatic==null)
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
}
