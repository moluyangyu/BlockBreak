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
        UpdateTextNames(false);
        UpdateTextNames(true);
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
    /// <summary>
    /// 在多场景间同步这个数组
    /// </summary>
    /// <param name="a"></param>true为需要从静态处更新，false为不需要从静态处更新
    public void UpdateTextNames(bool a)
    {
        if (a)
        {
            UiStatic.LoadFromCSV("Assets/Text/标识名.csv");
            textNames = UiStatic.TextNamesStatic;
        }
        else
        {
            UiStatic.TextNamesStatic = textNames;//更新
            UiStatic.SaveToCSV("Assets/Text/标识名.csv");
        }
    }
}
