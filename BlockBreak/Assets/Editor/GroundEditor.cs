using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(ActivityGround))]
public class GroundEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ActivityGround activityGround = (ActivityGround)target;
        EditorGUILayout.LabelField("功能按钮（实验中）：");
        if (GUILayout.Button("添加进演出动画（新的批次）"))
        {
            // 在这里添加新的变量到数组中
            activityGround.AddGround1();
        }
        if (GUILayout.Button("添加进演出动画（同一批次）"))
        {
            // 在这里添加新的变量到数组中
            activityGround.AddGround2();
        }

    }
}
