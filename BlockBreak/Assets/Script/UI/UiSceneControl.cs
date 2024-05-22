#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
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
    private bool a;//切换打包和编辑器环境的
    private void Awake()
    {
        //ceneManager.LoadScene(1, LoadSceneMode.Additive);//额外加载UI场景
       // UpdateTextNames(false);
        UpdateTextNames(true);
    }
    void Start()
    {
        
    }
    private void OnEnable()
    {
        // 注册场景加载完成时的事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 移除场景加载完成时的事件，以避免重复注册
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 场景加载完成时调用的方法
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        a = true;
#if UNITY_EDITOR
        a = false;
#endif
        if (a)
        {
            SceneManager.LoadScene("Assets/Scenes/UiInLevel.unity", LoadSceneMode.Additive);
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
#if UNITY_EDITOR
    public void OpenUI()
    {
        ui = EditorSceneManager.OpenScene("Assets/Scenes/UiInLevel.unity", OpenSceneMode.Additive);//额外加载UI场景
    }

    public void CloseUI()
    {
        EditorSceneManager.CloseScene(ui, true);//卸载UI场景
    }
#endif
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
