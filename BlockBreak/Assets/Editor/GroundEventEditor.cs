using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(GroundEvent))]
public class GroundEventEditor : Editor
{
    SerializedProperty activityGroundGroup;
    SerializedProperty nowStepNumber;
    SerializedProperty nowBollardNumber;
    // new SerializedObject serializedObject;
    // Start is called before the first frame update
    private void OnEnable()
    {
        GroundEvent groundEvent = (GroundEvent)target;
       // serializedObject = new SerializedObject(groundEvent);
        activityGroundGroup = serializedObject.FindProperty("activityGroundGroup");
        nowStepNumber = serializedObject.FindProperty("nowStepNumber");
        nowBollardNumber = serializedObject.FindProperty("nowBollardNumber");
    }
    public override void OnInspectorGUI()
    {
        GroundEvent groundEvent = (GroundEvent)target;
        // 更新 SerializedObject
        serializedObject.Update();
        EditorGUILayout.PropertyField(nowStepNumber, new GUIContent("当前台阶触发数"));
        EditorGUILayout.PropertyField(nowBollardNumber, new GUIContent("当前升降柱触发数"));
        EditorGUILayout.LabelField("功能按钮：");
        if (GUILayout.Button("初始化列表（暂时用不了）"))
        {
            // 在这里添加新的变量到数组中
            groundEvent.StartGroup();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
