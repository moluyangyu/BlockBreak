using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    public float speed;
    private int dir = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Turn")
        {
            dir = -dir;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 0f);
        }
    }
}
