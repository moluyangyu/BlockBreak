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
        // ���� SerializedObject
        serializedObject.Update();
        // ��ʾĬ�ϱ���
        DrawDefaultInspector();
        EditorGUILayout.LabelField("���ܰ�ť��");
        if (GUILayout.Button("��ʼ���б�"))
        {
            // ����������µı�����������
            groundEvent.StartGroup();
        }
        // ��ʾ��ά List ����
        Show2DListArray();
        // Ӧ�����е������޸�
        serializedObject.ApplyModifiedProperties();
    }
    void Show2DListArray()
    {
        EditorGUILayout.LabelField("Your 2D List Array:");

        // ��ȡ�ⲿ List �Ĵ�С
        int rowCount = activityGroundGroup.arraySize;

        for (int i = 0; i < rowCount; i++)
        {
            // ��ȡ�ڲ� List �� SerializedProperty
            SerializedProperty innerListProp = activityGroundGroup.GetArrayElementAtIndex(i).FindPropertyRelative("Array");

            // ��ʾÿһ�е� Label
            EditorGUILayout.LabelField($"Row {i}:");

            // Ƕ����ʾÿһ�е��ڲ� List
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
