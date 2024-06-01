using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStatus : MonoBehaviour 
{
    public GameObject refreshPoint;
    [HideInInspector] public GameObject eliminatePoint;
    [HideInInspector] public BlockState state;
    [HideInInspector] public bool moving;
    [HideInInspector] public int ePointIndex;
    public BlockType type;
    public int order;
    public bool auto;//自动匹配回归点

    void Start()
    {
        if(auto)
        {
            refreshPoint = GameObject.Find("BlockRefreshArea/" + this.gameObject.name);
        }
    }

    private void Update()
    {
        if(!moving)
        {
            if (state == BlockState.activated)
            {
                transform.position = eliminatePoint.transform.position;
            }
            else
            {
                if(refreshPoint!=null)transform.position = refreshPoint.transform.position;
            }
        }
        
    }

}
