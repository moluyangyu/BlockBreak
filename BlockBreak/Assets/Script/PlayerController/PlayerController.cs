using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float originSpeed;
    public float fastSpeed;
    private float speed;
    //public float stateDuration;
    public int direction = 1;
    public bool isFast = false;
    public bool stop = false;

    GameObject background;
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
        background = GameObject.Find("BackGround");
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
            background.transform.Translate(Vector3.right * speed * -direction * Time.deltaTime);
    }
    
    public void Turn()
    {
        direction = -direction;
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
