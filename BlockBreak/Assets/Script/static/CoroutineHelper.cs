using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    private static CoroutineHelper _instance;
    // ��ȡ�򴴽�ʵ��
    public static CoroutineHelper Instance
    {
        get
        {
            if (_instance == null)
            {
                // �ڳ����д���һ���µ���Ϸ�����������ű���Ϊ���
                GameObject obj = new GameObject("CoroutineHelper");
                _instance = obj.AddComponent<CoroutineHelper>();
                DontDestroyOnLoad(obj); // ������������ڳ�������ʱ��������
            }
            return _instance;
        }
    }
    // ��̬����������Э��
    public static void WaitForSeconds(MonoBehaviour mono, float seconds, System.Action callback)
    {
        Instance.StartCoroutine(Instance.WaitCoroutine(seconds, callback));
    }

    // �ڲ�Э��ʵ��
    private IEnumerator WaitCoroutine(float seconds, System.Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    }
}


