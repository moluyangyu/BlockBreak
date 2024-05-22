using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    GameObject player;
    public float offset_min;
    public float offset_max;
    public float x_min;
    public float x_max;
    static GameObject SceneBlacking;
    private void Awake()
    {
        player = GameObject.Find("Player");
        SceneBlacking=GameObject.Find("变暗特效");
        if(SceneBlacking != null)SceneBlacking.SetActive(false);
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
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, x_min, x_max), player.transform.position.y+7.0f, transform.position.z);
    }
    /// <summary>
    /// 让场景变暗与否
    /// </summary>
    /// <param name="a"></param>决定开关状态的变量
    public  void SceneBlack(bool a)
    {
        if (a)
        {
            SceneBlacking.SetActive(true);
        }
        else
        {
            SceneBlacking.SetActive(false);
        }
    }

    

}
