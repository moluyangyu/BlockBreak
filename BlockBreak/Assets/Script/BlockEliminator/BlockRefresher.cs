using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class BlockRefresher : MonoBehaviour
{
    public GameObject blockPrefab;

    private GameObject refreshArea;
    private Vector3[] refreshPos = new Vector3[99];

    private static BlockRefresher instance;
    public static BlockRefresher Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.Find("BlockRefreshArea").GetComponent<BlockRefresher>();
            return instance;
        }
    }
    
    private void Awake()
    {
        GetRefreshArea();
    }

    private void GetRefreshArea()
    {
        refreshArea = GameObject.Find("BlockRefreshArea");
        for (int i = 0; i < refreshArea.transform.childCount; i++)
        {
            refreshPos[i] = refreshArea.transform.GetChild(i).position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateBlock(Vector3 pos)
    {
        GameObject block = UnityEngine.Object.Instantiate(blockPrefab);
        block.transform.position = pos;
        BlockStatus status = block.GetComponent<BlockStatus>();
        status.originPos = pos;
        status.state = BlockState.original;
        status.moving = false;
        status.type = (BlockType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(BlockType)).Length);
    }

}
