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
        //从脚本中获取变量
        textNames= serializedObject.FindProperty("textNames");
        UiSceneControl uiSceneControl = (UiSceneControl)target;
        uiSceneControl.UpdateTextNames(true);
    }
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
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
        if (GUILayout.Button("应用标识名更改"))
        {
            uiSceneControl.UpdateTextNames(false);
            Debug.Log("更改成功");
        }
        if (GUILayout.Button("读取标识名"))
        {
            uiSceneControl.UpdateTextNames(false);
            Debug.Log("更改成功");
        }
        serializedObject.Update();
        EditorGUILayout.PropertyField(textNames, new GUIContent("对话气泡标识名"));
        serializedObject.ApplyModifiedProperties();
    }
}
