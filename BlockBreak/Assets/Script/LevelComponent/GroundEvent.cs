using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public int nowSerialNumber;//当前触发的号码
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //移动开始的事件
    public delegate void MoveStartHandler(int i);
    public event MoveStartHandler MoveStart;
    public void MoveStartIssue()
    {
        Debug.Log("移动开始");
        MoveStart?.Invoke(nowSerialNumber);
        nowSerialNumber++;
    }
}
