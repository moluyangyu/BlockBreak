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
    public void MoveStartIssue()
    {
        Debug.Log("�ƶ���ʼ");
        MoveStart?.Invoke(nowSerialNumber);
        nowSerialNumber++;
    }
}
