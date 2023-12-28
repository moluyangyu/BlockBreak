using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public int nowSerialNumber;//��ǰ�����ĺ���
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //�ƶ���ʼ���¼�
    public delegate void MoveStartHandler(int i);
    public event MoveStartHandler MoveStart;
    /// <summary>
    /// �ƶ���ʼ
    /// </summary>
    public void MoveStartIssue()
    {
        
        MoveStart?.Invoke(nowSerialNumber);
        nowSerialNumber++;
    }
    //�����ʵ����¼�
    public delegate void TriggerEggHandler(string finishName);
    public event TriggerEggHandler TriggerEgg;
    /// <summary>
    /// �ʵ�����
    /// </summary>
    /// <param name="finishName"></param>�����Ĳʵ�����
    public void TriggerEggIssue(string finishName)
    {
        TriggerEgg?.Invoke(finishName);
    }
}
