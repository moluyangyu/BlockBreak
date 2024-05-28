using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float originSpeed;
    public float origin_acc;
    public float fastSpeed;
    public float fast_acc;
    private float speed;
    //public float stateDuration;
    public float jumpForce;
    public int direction = 1;
    public static bool isFast = false;
    public static bool stop = false;
    private Rigidbody2D rb;
    //private Animator anim;
    public bool isTalk = false;
    public bool isStair = false;
    public string idName;

    private GameObject stairPoint1;
    private GameObject stairPoint2;
    private float x1,x2,y1,y2;
    private GameObject Cat;//猫雕像
    public GameObject elevatorTrigger;//用于记录当前踩上的电梯组件
    private int catmiss;//计数用于第二次触发消除猫

    private static DragonBones.UnityArmatureComponent animDB;
 //   private DragonBones.UnityArmatureComponent animDB2;
    public enum ActionType
    {
        turn,
        switchSpeed,
        switchStop,
    }


    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.Find("Player").GetComponent<PlayerController>();
            return instance;
        }
    }

    private static GameObject player;
    public static GameObject Player
    {
        get {
            if (player == null)
                player = GameObject.Find("Player");
            return player;
        }
    }

    private void Awake()
    {
        speed = originSpeed;
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        


        stairPoint1 = GameObject.Find("StairPoint1");
        stairPoint2 = GameObject.Find("StairPoint2");
        x1 = stairPoint1.transform.position.x;
        x2 = stairPoint2.transform.position.x;
        y1 = stairPoint1.transform.position.y;
        y2 = stairPoint2.transform.position.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        UiStatic.UiOpen += SwitchStop;
        animDB = GetComponent<DragonBones.UnityArmatureComponent>();
        Cat = GameObject.Find("馆长雕像");
        SwitchStop(1);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!stop)
        {
            if(isFast)
            {
                if (Mathf.Abs(rb.velocity.x) < fastSpeed)
                {
                    rb.AddForce(new Vector2(fast_acc * direction, 0));
                }
            }
            else
            {
                if (Mathf.Abs(rb.velocity.x) < originSpeed)
                {
                    rb.AddForce(new Vector2(origin_acc * direction, 0));
                }
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if (transform.position.x >= x1 && transform.position.x <= x2)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(y1, y2, Mathf.InverseLerp(x1, x2, transform.position.x)), 0);
            isStair = true;
        }
        else
        {
            isStair = false;
        }

        if(isTalk&&Input.GetMouseButtonDown(0))
        {
            bool i=UiStatic.TalkKickIssue(idName);
            if(i)
            {
                isTalk = false;
                SwitchStop(1);
                //第一关的通关条件写在这里
                if (idName== "第3章-2")
                {
                    UiStatic.NextLevelIssue();
                    SwitchStop(0);
                }
                if (idName == "序章-2")
                {
                    SwitchStop(0);
                    PlayAnim("problem");
                    //调用通用延时函数
                    CoroutineHelper.WaitForSeconds(this, 1.5f, () =>
                    {
                        SwitchStop(1);
                    });
                }
                if (idName=="序章")
                {
                    SwitchStop(0);
                    idName = "序章-2";
                    PlayAnim("surprise");
                    CoroutineHelper.WaitForSeconds(this, 1.5f, () =>
                    {
                        Talk();
                    });
                }
            }
        }
    }

    public void Turn()
    {
        direction = -direction;
        rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void SwitchSpeed()
    {
        isFast = !isFast;
        if (isFast) speed = fastSpeed;
        else speed = originSpeed;
        //anim.SetBool("isFast", isFast);
        PlayAnim();
    }

    public void SwitchStop()
    {
        stop = !stop;
        //anim.SetBool("stop", stop);
        PlayAnim();
    }
    /// <summary>
    /// 0是停下来，1是走起来，2是切换状态
    /// </summary>
    /// <param name="a"></param>
    public void SwitchStop(int a)
    {
        switch (a)
        {
            case 0:
                stop = true;

                break;
            case 1:
                stop = false;
                
                break;
            case 2:
                SwitchStop(); break;
        }
        PlayAnim();
    }

    public static void PlayAnim(string name = null)
    {
       if(animDB!=null)
        {
            // animDB = GetComponent<DragonBones.UnityArmatureComponent>();
            if (name == null)
            {
                if (stop)
                {
                    animDB.animation.Play("idle");
                }
                else if (isFast)
                {
                    animDB.animation.Play("run");
                }
                else
                {
                    animDB.animation.Play("walk");
                }
            }
            else
            {
                animDB.animation.Play(name, 1);

            }
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Die")
            Die();
        else if (collision.gameObject.tag == "Jump")
            Jump();
        else if (collision.gameObject.tag == "Talk")
        {
            idName = collision.gameObject.GetComponent<TalkTrigger>().idname;
            collision.gameObject.SetActive(false);
            Talk();
        }

        else if (collision.gameObject.tag == "Stop")
        {
            SwitchStop(0);
            BlockEliminator.Instance.NextScene();
            collision.gameObject.SetActive(false);
        }else if(collision.gameObject.tag=="Dex")
        {
           // SwitchStop(0);
            collision.gameObject.GetComponent<GameDexTrigger>().DexON();
        }
        else if (collision.gameObject.tag == "Teach")
        {
            SwitchStop(0);
            BlockEliminator.Instance.NextScene();
            collision.gameObject.GetComponent<NewTeach>().OpenTeach();

            collision.gameObject.SetActive(false);
        }else if(collision.gameObject.tag=="DropTrigger")
        {
            if (catmiss == 0)
            { 
                Cat.GetComponent<Animator>().SetTrigger("掉落");
                catmiss++;
            }else
            {
                Cat.SetActive(false);
                collision.gameObject.SetActive(false);
            }
 
        }else if (collision.gameObject.tag == "Elevator")
        {
            SwitchStop(0);
            BlockEliminator.Instance.NextScene();
            elevatorTrigger = collision.gameObject;
        }


    }
    /// <summary>
    /// 三消组件电梯触发这个函数,InvokeRepeating(nameof(MoveFollow()), 0f, 0.01f);调用语句
    /// </summary>
    public void MoveFollow()
    {
        if (this.gameObject.transform.position == elevatorTrigger.transform.position)
        {
            CancelInvoke(nameof(MoveFollow));//取消调用
        }

        this.gameObject.transform.position = elevatorTrigger.transform.position;
    }

    public void Dianti()
    {
        InvokeRepeating(nameof(MoveFollow), 0f, 0.01f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Teach")
        {
            //SwitchStop(0);
            collision.gameObject.GetComponent<NewTeach>().CloseTeach();
            //collision.gameObject.SetActive(false);
        }else if (collision.gameObject.CompareTag("Elevator"))
        {
            elevatorTrigger = null;
            collision.gameObject.SetActive(false);
        }

    }

    private void Talk()
    {
        SwitchStop(0);
        isTalk = true;
        UiStatic.TalkKickIssue(idName);
    }

    private void Jump()
    {
        rb.AddForce(new Vector2 (0, jumpForce));
    }

    private void Die()
    {
        SwitchStop(0);
        //anim.SetTrigger("die");
        PlayAnim("die");
        // 调用通用暂停方法
        CoroutineHelper.WaitForSeconds(this, 1.5f, () =>
        {
            BlockRefresher.Instance.RefreshAll();
            transform.position = new Vector3(72, transform.position.y, transform.position.z);//在第一关归位
            BlockEliminator.Instance.LastScene();
            PlayAnim("idie");
            // UiStatic.PlayerDieIssue(false);
        });
        
    }
 

}
