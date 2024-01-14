using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float originSpeed;
    public float origin_acc;
    public float fastSpeed;
    public float fast_acc;
    private float speed;
    //public float stateDuration;
    public int direction = 1;
    public bool isFast = false;
    public bool stop = false;
    Rigidbody2D rb;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
    }
    
    public void Turn()
    {
        direction = -direction;
        rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
    }

    public void SwitchSpeed()
    {
        isFast = !isFast;
        if (isFast) speed = fastSpeed;
        else speed = originSpeed;
    }

    public void SwitchStop()
    {
        stop = !stop;
    }

}
