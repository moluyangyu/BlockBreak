using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class UiSceneControl : MonoBehaviour
{
    // Start is called before the first frame update
    Scene ui;
    public string[] textNames;
    public int textInt;
    public int[] testInts;
    private void Awake()
    {
        //ceneManager.LoadScene(1, LoadSceneMode.Additive);//额外加载UI场景
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OpenUI()
    {
        ui = EditorSceneManager.OpenScene("Assets/Scenes/UiInLevel.unity", OpenSceneMode.Additive);//额外加载UI场景
    }
    public void CloseUI()
    {
        EditorSceneManager.CloseScene(ui, true);//卸载UI场景
    }
    public void UpdateTextNames()
    {
        UiStatic.TextNamesStatic = textNames;//更新
    }
}
