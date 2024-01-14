using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(UiSceneControl))]

public class UiSceneControlEditor : Editor
{
    SerializedProperty textNames;
    private void OnEnable()
    {
        //�ӽű��л�ȡ����
        textNames= serializedObject.FindProperty("textNames");
        UiSceneControl uiSceneControl = (UiSceneControl)target;
        uiSceneControl.UpdateTextNames();
    }
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        UiSceneControl uiSceneControl = (UiSceneControl)target;
        EditorGUILayout.LabelField("���ܰ�ť");
        if (GUILayout.Button("�ڱ༭�����UI"))
        {
            uiSceneControl.OpenUI();
        }
        if (GUILayout.Button("�ڱ༭��������UI"))
        {
            uiSceneControl.CloseUI();
        }
        if (GUILayout.Button("Ӧ�ñ�ʶ������"))
        {
            uiSceneControl.UpdateTextNames();
            Debug.Log("���ĳɹ�");
        }
        serializedObject.Update();
        EditorGUILayout.PropertyField(textNames, new GUIContent("�Ի����ݱ�ʶ��"));
        serializedObject.ApplyModifiedProperties();
    }
}
