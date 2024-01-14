using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(ActivityGround))]
public class ActivityGroundEditor : Editor
{
    SerializedProperty direction;
    SerializedProperty spriteProp;
    SerializedProperty floorClassProp;
    SerializedProperty moveSpeedProp;
    SerializedProperty distanceProp;
    SerializedProperty serialNumberProp;
    SerializedProperty delayTimeProp;
    SerializedProperty canCross;
    private void OnEnable()
    {
        //从脚本中获取变量
        direction = serializedObject.FindProperty("direction");
        spriteProp = serializedObject.FindProperty("sprite");
        floorClassProp = serializedObject.FindProperty("floorClass");
        moveSpeedProp = serializedObject.FindProperty("moveSpeed");
        distanceProp = serializedObject.FindProperty("distance");
        serialNumberProp = serializedObject.FindProperty("serialNumber");
        delayTimeProp = serializedObject.FindProperty("delayTime");
        canCross = serializedObject.FindProperty("canCross");
    }
    public override void OnInspectorGUI()
    {
        ActivityGround activityGround = (ActivityGround)target;
        EditorGUILayout.LabelField("功能按钮（实验中还用不了）：");
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
        serializedObject.Update();
        EditorGUILayout.PropertyField(spriteProp, new GUIContent("地形图片"));
        EditorGUILayout.PropertyField(floorClassProp, new GUIContent("组件类型"));
        EditorGUILayout.Slider(direction, 0f, 360f, new GUIContent("移动角度"));
        EditorGUILayout.PropertyField(moveSpeedProp, new GUIContent("移动速度"));
        EditorGUILayout.PropertyField(distanceProp, new GUIContent("移动距离"));
        EditorGUILayout.PropertyField(delayTimeProp, new GUIContent("触发延时"));
        EditorGUILayout.PropertyField(serialNumberProp, new GUIContent("触发批次"));
        EditorGUILayout.LabelField("是否可以通过", canCross.intValue.ToString());
        serializedObject.ApplyModifiedProperties();
    }
}
