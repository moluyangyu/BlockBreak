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
    /// <summary>
    /// 移动开始
    /// </summary>
    public void MoveStartIssue()
    {
        
        MoveStart?.Invoke(nowSerialNumber);
        nowSerialNumber++;
    }
    //触发彩蛋的事件
    public delegate void TriggerEggHandler(string finishName);
    public event TriggerEggHandler TriggerEgg;
    /// <summary>
    /// 彩蛋触发
    /// </summary>
    /// <param name="finishName"></param>触发的彩蛋名字
    public void TriggerEggIssue(string finishName)
    {
        TriggerEgg?.Invoke(finishName);
    }
}
