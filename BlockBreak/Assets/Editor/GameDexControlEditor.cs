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
        // 更新 SerializedObject
        serializedObject.Update();
      //  EditorGUILayout.PropertyField(readedText, new GUIContent("图鉴文本放置表"));
        EditorGUILayout.PropertyField(sprites, new GUIContent("图鉴的配图"));
        serializedObject.ApplyModifiedProperties();
    }
}
