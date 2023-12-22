using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class BlockRefresher : MonoBehaviour
{
    public GameObject blockPrefab;

    private GameObject refreshArea;
    private GameObject[] refreshPoints = new GameObject[99];

    public Sprite turn;
    public Sprite switchSpeed;
    public Sprite switchStop;

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
            refreshPoints[i] = refreshArea.transform.GetChild(i).gameObject;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RefreshAll();
    }

    private void RefreshAll()
    {
        GameObject[] blockArray = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blockArray)
        {
            Destroy(block);
        }
        for (int i = 0;i < refreshArea.transform.childCount; i++)
        {
            CreateBlock(refreshPoints[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateBlock(GameObject point)
    {
        GameObject block = UnityEngine.Object.Instantiate(blockPrefab);
        block.transform.position = point.transform.position;
        BlockStatus status = block.GetComponent<BlockStatus>();
        status.refreshPoint = point;
        status.state = BlockState.original;
        status.moving = false;
        status.type = (BlockType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(BlockType)).Length);
        switch (status.type)
        {
            case BlockType.turn:
                block.GetComponent<SpriteRenderer>().sprite = turn;
                break;
            case BlockType.switchSpeed:
                block.GetComponent<SpriteRenderer>().sprite = switchSpeed;
                break;
            case BlockType.switchStop:
                block.GetComponent<SpriteRenderer>().sprite = switchStop;
                break;
        }
    }

}
