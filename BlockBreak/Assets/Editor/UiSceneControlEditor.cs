using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(UiSceneControl))]

public class UiSceneControlEditor : Editor
{
    public override void OnInspectorGUI()
    {
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
    }
}
