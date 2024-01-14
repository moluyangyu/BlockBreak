using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiSceneControl : MonoBehaviour
{
    // Start is called before the first frame update
    Scene ui;
    private void Awake()
    {
        //ceneManager.LoadScene(1, LoadSceneMode.Additive);//�������UI����
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
        ui=EditorSceneManager.OpenScene("Assets/Scenes/UiInLevel.unity", OpenSceneMode.Additive);//�������UI����
    }
    public void CloseUI()
    {
        EditorSceneManager.CloseScene(ui,true);//ж��UI����
    }
}
