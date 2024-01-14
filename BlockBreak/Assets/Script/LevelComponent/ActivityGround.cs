using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    UpLeft,
    UpRight,
    DownLeft,
    DownRight
}
public enum FloorClass
{
    step,
    bollard
}

public class ActivityGround : MonoBehaviour
{
    [Tooltip("地形图片")]
    public Sprite sprite;
    public FloorClass floorClass;
    // public Direction direction;
    [Tooltip("必须大于0，不大于1的会被修改为3")]
    public float moveSpeed;
    public float distance;
    [Tooltip("从0开始计数")]
    public int serialNumber;
    [Tooltip("单位为秒")]
    public float delayTime;
    public int[] a;
    [Header("-----分割线-----")]
    public bool isPlay;
    public Vector3 startPoint;//计算移动用到的两个位置
    public Vector3 endPoint;
    public bool canCross;//是否可以通过
    public GameObject groundEvent;//地板事件
    public float t;//插值因子
    public float direction;
    // Start is called before the first frame update
    void Start()
    {
        groundEvent = GameObject.Find("GroundEvent");
        groundEvent.GetComponent<GroundEvent>().MoveStart += MoveStart;
        moveSpeed = (moveSpeed <= 0) ? 3 : moveSpeed;//如果小于1就等于3
        moveSpeed /= (distance*100);//将速度归一化用于计算
    }
    // Update is called once per frame
    void Update()
    {
 
    }
    /// <summary>
    /// 订阅移动事件，根据预设值计算出目的地然后启动移动的函数，移动一次后更改可通过状态然后取消订阅
    /// </summary>
    /// <param name="i"></param>传入移动编号，验证是否应该移动
    public void MoveStart(int i, FloorClass a)
    {
        if(i==serialNumber&&a==floorClass)
        {
            Debug.Log("移动开始第" + serialNumber + "号"+"类型："+floorClass);
            startPoint = this.gameObject.transform.position;
            endPoint = this.gameObject.transform.position;//计算单次移动需要的两个位置
            float radiaAngle = Mathf.Deg2Rad * direction;
            endPoint += new Vector3(distance * Mathf.Cos(radiaAngle), distance*Mathf.Sin(radiaAngle), 0f);
            //switch (direction)
            //{
            //    case Direction.Up: endPoint += new Vector3(0f * distance, 1.0f * distance, 0f * distance); break;
            //    case Direction.Down: endPoint += new Vector3(0f * distance, -1.0f * distance, 0f * distance); break;
            //    case Direction.Left: endPoint += new Vector3(-1.0f * distance, 0f * distance, 0f * distance); break;
            //    case Direction.Right: endPoint += new Vector3(1.0f * distance, 0f * distance, 0f * distance); break;
            //    case Direction.DownLeft: endPoint += new Vector3(-1.0f * distance, -1.0f * distance, 0f * distance); break;
            //    case Direction.DownRight: endPoint += new Vector3(1.0f * distance, -1.0f * distance, 0f * distance); break;
            //    case Direction.UpLeft: endPoint += new Vector3(-1.0f * distance, 1.0f * distance, 0f * distance); break;
            //    case Direction.UpRight: endPoint += new Vector3(1.0f * distance, 1.0f * distance, 0f * distance); break;
            //}
            InvokeRepeating(nameof(Move), delayTime, 0.01f);
            groundEvent.GetComponent<GroundEvent>().MoveStart -= MoveStart; //这里加入取消订阅
            canCross = true;//更改移动状态
        }
    }
    /// <summary>
    /// 实际移动的函数，通过插值平滑移动，抵达目标位置以后结束调用
    /// </summary>
    public void Move()
    {
        if (this.gameObject.transform.position == endPoint)
        {
            CancelInvoke(nameof(Move));//取消调用
        }
        t = (t > 1) ? 1 : t + moveSpeed;
        this.gameObject.transform.position = Vector3.Lerp(startPoint, endPoint, t);
    }
    /// <summary>
    /// 在数组中添加组件,新开批次
    /// </summary>
    /// <param name="a"></param>
    public void AddGround1()
    {
        if(!isPlay)
        {
            isPlay = true;
            groundEvent = GameObject.Find("GroundEvent");
            serialNumber = groundEvent.GetComponent<GroundEvent>().AddGround1(this.gameObject);
        }
    }
    public void AddGround2()
    {
        if(!isPlay)
        {
            isPlay = true;
            groundEvent = GameObject.Find("GroundEvent");
            serialNumber = groundEvent.GetComponent<GroundEvent>().AddGround2(this.gameObject);
        }
    }
}
