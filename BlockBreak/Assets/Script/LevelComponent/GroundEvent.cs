using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public int nowSerialNumber;//当前触发的号码
    public List<List<GameObject>> activityGroundGroup;//演出方块的分组
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
    /// <summary>
    /// 在数组中添加组件，新开批次
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public int AddGround1(GameObject a)
    {
        activityGroundGroup.Add(new List<GameObject>());//新开批次就新增行
        int i = activityGroundGroup.Count;
        activityGroundGroup[i].Add(a);
        EditorUtility.SetDirty(this);
        return activityGroundGroup.Count;
    }
    /// <summary>
    /// 在数组中添加组件，同一批次
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public int AddGround2(GameObject a)
    {
        int i = activityGroundGroup.Count;
        activityGroundGroup[i].Add(a);
        EditorUtility.SetDirty(this);
        return activityGroundGroup.Count;
    }
    /// <summary>
    /// 依据目前的数组配置刷新组件触发顺序
    /// </summary>
    public void UpdateGround()
    {
        int i = 0;
        foreach(List<GameObject> a in activityGroundGroup)
        {
            i++;
            foreach(GameObject b in a)//这里嵌套一下,b是活动组件
            {
                b.GetComponent<ActivityGround>().serialNumber = i;
            }
        }
    }
}
