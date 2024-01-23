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
        //ceneManager.LoadScene(1, LoadSceneMode.Additive);//�������UI����
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
        ui = EditorSceneManager.OpenScene("Assets/Scenes/UiInLevel.unity", OpenSceneMode.Additive);//�������UI����
    }
    public void CloseUI()
    {
        EditorSceneManager.CloseScene(ui, true);//ж��UI����
    }
    /// <summary>
    /// �ڶೡ����ͬ���������
    /// </summary>
    /// <param name="a"></param>trueΪ��Ҫ�Ӿ�̬�����£�falseΪ����Ҫ�Ӿ�̬������
    public void UpdateTextNames(bool a)
    {
        if (a)
        {
            UiStatic.LoadFromCSV("Assets/Text/��ʶ��.csv");
            textNames = UiStatic.TextNamesStatic;
        }
        else
        {
            UiStatic.TextNamesStatic = textNames;//����
            UiStatic.SaveToCSV("Assets/Text/��ʶ��.csv");
        }
    }
}
