using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    public float offset_min;
    public float offset_max;
    public float x_min;
    public float x_max;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        //offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, player.transform.position.x + offset_min, player.transform.position.x + offset_max), transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, x_min, x_max), transform.position.y, transform.position.z);
    }
}
