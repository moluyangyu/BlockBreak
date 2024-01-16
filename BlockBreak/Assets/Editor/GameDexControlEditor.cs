using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(GameDexControl))]
public class GameDexControlEditor : Editor
{
    // Start is called before the first frame update
    SerializedProperty readedText;
    SerializedProperty sprites;
    private void OnEnable()
    {
        readedText = serializedObject.FindProperty("readedText");
        sprites = serializedObject.FindProperty("sprites");
    }
    public override void OnInspectorGUI()
    {
        // ���� SerializedObject
        serializedObject.Update();
      //  EditorGUILayout.PropertyField(readedText, new GUIContent("ͼ���ı����ñ�"));
        EditorGUILayout.PropertyField(sprites, new GUIContent("ͼ������ͼ"));
        serializedObject.ApplyModifiedProperties();
    }
}
