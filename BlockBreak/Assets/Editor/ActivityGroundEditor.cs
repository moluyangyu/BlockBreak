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
        //�ӽű��л�ȡ����
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
        EditorGUILayout.LabelField("���ܰ�ť��ʵ���л��ò��ˣ���");
        if (GUILayout.Button("��ӽ��ݳ��������µ����Σ�"))
        {
            // ����������µı�����������
            activityGround.AddGround1();
        }
        if (GUILayout.Button("��ӽ��ݳ�������ͬһ���Σ�"))
        {
            // ����������µı�����������
            activityGround.AddGround2();
        }
        serializedObject.Update();
        EditorGUILayout.PropertyField(spriteProp, new GUIContent("����ͼƬ"));
        EditorGUILayout.PropertyField(floorClassProp, new GUIContent("�������"));
        EditorGUILayout.Slider(direction, 0f, 360f, new GUIContent("�ƶ��Ƕ�"));
        EditorGUILayout.PropertyField(moveSpeedProp, new GUIContent("�ƶ��ٶ�"));
        EditorGUILayout.PropertyField(distanceProp, new GUIContent("�ƶ�����"));
        EditorGUILayout.PropertyField(delayTimeProp, new GUIContent("������ʱ"));
        EditorGUILayout.PropertyField(serialNumberProp, new GUIContent("��������"));
        EditorGUILayout.LabelField("�Ƿ����ͨ��", canCross.intValue.ToString());
        serializedObject.ApplyModifiedProperties();
    }
}
