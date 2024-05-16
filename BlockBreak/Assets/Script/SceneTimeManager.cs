using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneTimeManager : MonoBehaviour
{
    private Dictionary<string, float> sceneTimeScales = new Dictionary<string, float>();

    void Start()
    {
        // ��ʼ��ÿ��������ʱ������
        foreach (Scene scene in SceneManager.GetAllScenes())
        {
            if (scene.isLoaded)
            {
                sceneTimeScales[scene.name] = 1f;
            }
        }
    }

    // ��ͣ�ض�������ʱ��
    public void PauseScene(string sceneName)
    {
        if (sceneTimeScales.ContainsKey(sceneName))
        {
            sceneTimeScales[sceneName] = 0f;
            SetSceneTimeScale(sceneName, 0f);
        }
    }

    // �ָ��ض�������ʱ��
    public void ResumeScene(string sceneName)
    {
        if (sceneTimeScales.ContainsKey(sceneName))
        {
            sceneTimeScales[sceneName] = 1f;
            SetSceneTimeScale(sceneName, 1f);
        }
    }

    // ���ó���ʱ������
    private void SetSceneTimeScale(string sceneName, float timeScale)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene.isLoaded)
        {
            GameObject[] rootObjects = scene.GetRootGameObjects();
            foreach (GameObject obj in rootObjects)
            {
                var scripts = obj.GetComponentsInChildren<MonoBehaviour>();
                foreach (var script in scripts)
                {
                    script.enabled = (timeScale != 0f);
                }
            }
        }
    }

    void Update()
    {
        // ��Update�е������������ж����ʱ��
        foreach (var entry in sceneTimeScales)
        {
            if (entry.Value == 0f)
            {
                Scene scene = SceneManager.GetSceneByName(entry.Key);
                if (scene.isLoaded)
                {
                    GameObject[] rootObjects = scene.GetRootGameObjects();
                    foreach (GameObject obj in rootObjects)
                    {
                        var scripts = obj.GetComponentsInChildren<MonoBehaviour>();
                        foreach (var script in scripts)
                        {
                            if (script is ITimeControllable)
                            {
                                ((ITimeControllable)script).SetTimeScale(0f);
                            }
                        }
                    }
                }
            }
        }
    }
}

public interface ITimeControllable
{
    void SetTimeScale(float timeScale);
}
