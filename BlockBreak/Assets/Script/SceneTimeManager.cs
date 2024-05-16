using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneTimeManager : MonoBehaviour
{
    private Dictionary<string, float> sceneTimeScales = new Dictionary<string, float>();

    void Start()
    {
        // 初始化每个场景的时间缩放
        foreach (Scene scene in SceneManager.GetAllScenes())
        {
            if (scene.isLoaded)
            {
                sceneTimeScales[scene.name] = 1f;
            }
        }
    }

    // 暂停特定场景的时间
    public void PauseScene(string sceneName)
    {
        if (sceneTimeScales.ContainsKey(sceneName))
        {
            sceneTimeScales[sceneName] = 0f;
            SetSceneTimeScale(sceneName, 0f);
        }
    }

    // 恢复特定场景的时间
    public void ResumeScene(string sceneName)
    {
        if (sceneTimeScales.ContainsKey(sceneName))
        {
            sceneTimeScales[sceneName] = 1f;
            SetSceneTimeScale(sceneName, 1f);
        }
    }

    // 设置场景时间缩放
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
        // 在Update中调整场景内所有对象的时间
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
