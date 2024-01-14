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
        // ���� SerializedObject
        serializedObject.Update();
        EditorGUILayout.PropertyField(nowStepNumber, new GUIContent("��ǰ̨�״�����"));
        EditorGUILayout.PropertyField(nowBollardNumber, new GUIContent("��ǰ������������"));
        EditorGUILayout.LabelField("���ܰ�ť��");
        if (GUILayout.Button("��ʼ���б���ʱ�ò��ˣ�"))
        {
            // ����������µı�����������
            groundEvent.StartGroup();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
