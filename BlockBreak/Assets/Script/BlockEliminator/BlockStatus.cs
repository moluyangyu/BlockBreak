using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStatus : MonoBehaviour 
{
    public GameObject refreshPoint;
    public BlockType type;
    public BlockState state;
    public bool moving;





    private void Update()
    {
        if(!moving && state == BlockState.original)
        {
            transform.position = refreshPoint.transform.position;
        }
    }

}
