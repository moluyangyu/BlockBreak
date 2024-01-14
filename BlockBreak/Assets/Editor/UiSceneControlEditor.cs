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
        EditorGUILayout.LabelField("功能按钮");
        if (GUILayout.Button("在编辑器里打开UI"))
        {
            uiSceneControl.OpenUI();
        }
        if (GUILayout.Button("在编辑器里隐藏UI"))
        {
            uiSceneControl.CloseUI();
        }
    }
}
