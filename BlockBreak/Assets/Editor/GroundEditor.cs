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
        EditorGUILayout.LabelField("���ܰ�ť��ʵ���У���");
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

    }
}
