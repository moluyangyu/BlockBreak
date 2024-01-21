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

    private GameObject[] blocks = new GameObject[99];

    //public Sprite turn;
    //public Sprite switchSpeed;
    //public Sprite switchStop;
    //public Sprite ui;
    //public Sprite landform;
    //public Sprite easterEgg;

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
        GetBlocks();
    }

    private void GetBlocks()
    {
        blocks = GameObject.FindGameObjectsWithTag("block");
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
        //RefreshAll();
    }

    public void RefreshAll()
    {
        foreach (GameObject block in blocks) {
            block.SetActive(true);
            BlockStatus status = block.GetComponent<BlockStatus>();
            if (!status.moving)
            {
                status.state = BlockState.original;
            }
        }

        //GameObject[] blockArray = GameObject.FindGameObjectsWithTag("Block");
        //foreach (GameObject block in blockArray)
        //{
        //    Destroy(block);
        //}
        //for (int i = 0;i < refreshArea.transform.childCount; i++)
        //{
        //    CreateBlock(refreshPoints[i]);
        //}
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
        //switch (status.type)
        //{
        //    case BlockType.turn:
        //        status.GetComponent<SpriteRenderer>().sprite = turn;
        //        break;
        //    case BlockType.switchSpeed:
        //        status.GetComponent<SpriteRenderer>().sprite = switchSpeed;
        //        break;
        //    case BlockType.switchStop:
        //        status.GetComponent<SpriteRenderer>().sprite = switchStop;
        //        break;
        //    case BlockType.ui:
        //        status.GetComponent<SpriteRenderer>().sprite = ui;
        //        break;
        //    case BlockType.landform:
        //        status.GetComponent<SpriteRenderer>().sprite = landform;
        //        break;
        //    case BlockType.easterEgg:
        //        status.GetComponent<SpriteRenderer>().sprite = easterEgg;
        //        break;
        //}

    }

}
