using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(GroundEvent))]
public class GroundEventEditor : Editor
{
    SerializedProperty activityGroundGroup;
   // new SerializedObject serializedObject;
    // Start is called before the first frame update
    private void OnEnable()
    {
        GroundEvent groundEvent = (GroundEvent)target;
       // serializedObject = new SerializedObject(groundEvent);
        activityGroundGroup = serializedObject.FindProperty("activityGroundGroup");
        
    }
    public override void OnInspectorGUI()
    {
        GroundEvent groundEvent = (GroundEvent)target;
        // 更新 SerializedObject
        serializedObject.Update();
        // 显示默认变量
        DrawDefaultInspector();
        EditorGUILayout.LabelField("功能按钮：");
        if (GUILayout.Button("初始化列表"))
        {
            // 在这里添加新的变量到数组中
            groundEvent.StartGroup();
        }
        // 显示二维 List 数组
        Show2DListArray();
        // 应用所有的属性修改
        serializedObject.ApplyModifiedProperties();
    }
    void Show2DListArray()
    {
        EditorGUILayout.LabelField("Your 2D List Array:");

        // 获取外部 List 的大小
        int rowCount = activityGroundGroup.arraySize;

        for (int i = 0; i < rowCount; i++)
        {
            // 获取内部 List 的 SerializedProperty
            SerializedProperty innerListProp = activityGroundGroup.GetArrayElementAtIndex(i).FindPropertyRelative("Array");

            // 显示每一行的 Label
            EditorGUILayout.LabelField($"Row {i}:");

            // 嵌套显示每一行的内部 List
            EditorGUI.indentLevel++;
            int columnCount = innerListProp.arraySize;
            for (int j = 0; j < columnCount; j++)
            {
                EditorGUILayout.PropertyField(innerListProp.GetArrayElementAtIndex(j));
            }
            EditorGUI.indentLevel--;
        }
    }
}
