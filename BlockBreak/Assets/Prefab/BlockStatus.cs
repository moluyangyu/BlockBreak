using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStatus : MonoBehaviour 
{
    public Vector3 originPos;
    public BlockType type;
    public BlockState state;
    public void SwitchState()
    {
        switch(state)
        {
            case BlockState.original:
                state = BlockState.activated;
                break;
            case BlockState.activated:
                state = BlockState.original;
                break;
        }
    }
}
