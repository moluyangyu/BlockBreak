using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
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
    public bool isFast = false;
    public bool stop = false;
    private Rigidbody2D rb;
    //private Animator anim;
    public bool isTalk = false;
    public bool isStair = false;
    public string idName;

    private GameObject stairPoint1;
    private GameObject stairPoint2;
    private float x1,x2,y1,y2;


    private DragonBones.UnityArmatureComponent animDB;
    private DragonBones.UnityArmatureComponent animDB2;
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
        PlayAnim();
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
                stop = false;
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
    public void SwitchStop(int a)
    {
        switch (a)
        {
            case 0:
                stop = true;
                PlayAnim();
                break;
            case 1:
                stop = false;
                PlayAnim();
                break;
            case 2:
                SwitchStop(); break;
        }
    }

    public void PlayAnim(string name = null)
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
            Talk();
        }

        else if (collision.gameObject.tag == "Stop")
        {
            SwitchStop(0);
            BlockEliminator.Instance.NextScene();
            collision.gameObject.SetActive(false);
        }else if(collision.gameObject.tag=="Dex")
        {
            SwitchStop(0);
            collision.gameObject.GetComponent<GameDexTrigger>().DexON();
        }
            
    }

    private void Talk()
    {
        stop = true;
        isTalk = true;
        UiStatic.TalkKickIssue(idName);
    }

    private void Jump()
    {
        rb.AddForce(new Vector2 (0, jumpForce));
    }

    private void Die()
    {
        stop = true;
        //anim.SetTrigger("die");
        BlockRefresher.Instance.RefreshAll();
    }

}
