using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("触发距离")]
    public int distance;//触发距离
    [Header("标识名")]
    public string idname;//关卡内的编号
    private BoxCollider2D boxCollider;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        Vector2 a = new Vector2(distance, distance);
        SetColliderSize(a);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //检测玩家进入就发开始对话的信号
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().idName = idname;
          //  PlayerController.Instance.stop = true;
        }
    }
    /// <summary>
    /// 设置碰撞体大小
    /// </summary>
    /// <param name="newSize"></param>
    void SetColliderSize(Vector2 newSize)
    {
        if (boxCollider != null)
        {
            // 设置 BoxCollider2D 的大小
            boxCollider.size = newSize;
            //   Debug.Log("BoxCollider2D size set to: " + newSize);
        }
        else
        {
            //    Debug.LogError("BoxCollider2D component not found.");
        }
    }
}
