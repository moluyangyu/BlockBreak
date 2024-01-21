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
    private Animator anim;

    public string idName;

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

    private void Awake()
    {
        speed = originSpeed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        anim.SetBool("stop", stop);
        anim.SetBool("isFast", isFast);
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
        anim.SetBool("isFast", isFast);
    }

    public void SwitchStop()
    {
        stop = !stop;
        anim.SetBool("stop", stop);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Die")
            Die();
        else if (collision.gameObject.tag == "Jump")
            Jump();
        else if (collision.gameObject.tag == "Talk")
            Talk();
    }

    private void Talk()
    {
        stop = true;
        UiStatic.TalkKickIssue(idName);
    }

    private void Jump()
    {
        rb.AddForce(new Vector2 (0, jumpForce));
    }

    private void Die()
    {
        stop = true;
        anim.SetTrigger("die");
        BlockRefresher.Instance.RefreshAll();
    }
}
