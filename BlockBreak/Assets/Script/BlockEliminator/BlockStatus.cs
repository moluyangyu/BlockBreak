using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStatus : MonoBehaviour 
{
    public GameObject refreshPoint;
    public GameObject eliminatePoint;
    public BlockType type;
    public BlockState state;
    public bool moving;
    public int ePointIndex;




    private void Update()
    {
        if(!moving && state == BlockState.activated)
        {
            transform.position = eliminatePoint.transform.position;
        }
    }

}
