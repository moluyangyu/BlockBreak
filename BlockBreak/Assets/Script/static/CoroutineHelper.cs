using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    private static CoroutineHelper _instance;
    // 获取或创建实例
    public static CoroutineHelper Instance
    {
        get
        {
            if (_instance == null)
            {
                // 在场景中创建一个新的游戏对象并添加这个脚本作为组件
                GameObject obj = new GameObject("CoroutineHelper");
                _instance = obj.AddComponent<CoroutineHelper>();
                DontDestroyOnLoad(obj); // 保持这个对象在场景加载时不被销毁
            }
            return _instance;
        }
    }
    // 静态方法来启动协程
    public static void WaitForSeconds(MonoBehaviour mono, float seconds, System.Action callback)
    {
        Instance.StartCoroutine(Instance.WaitCoroutine(seconds, callback));
    }

    // 内部协程实现
    private IEnumerator WaitCoroutine(float seconds, System.Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    }
}


