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
    private bool a;//�л�����ͱ༭��������
    private void Awake()
    {
        //ceneManager.LoadScene(1, LoadSceneMode.Additive);//�������UI����
       // UpdateTextNames(false);
        UpdateTextNames(true);
    }
    void Start()
    {
        
    }
    private void OnEnable()
    {
        // ע�᳡���������ʱ���¼�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // �Ƴ������������ʱ���¼����Ա����ظ�ע��
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // �����������ʱ���õķ���
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
        ui = EditorSceneManager.OpenScene("Assets/Scenes/UiInLevel.unity", OpenSceneMode.Additive);//�������UI����
    }

    public void CloseUI()
    {
        EditorSceneManager.CloseScene(ui, true);//ж��UI����
    }
#endif
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
