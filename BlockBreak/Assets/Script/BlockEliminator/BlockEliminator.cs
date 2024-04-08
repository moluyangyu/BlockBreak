using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    turn,
    switchSpeed,
    switchStop,
    landform,
    ui,
    easterEgg
}

public enum BlockState
{
    original=0,
    activated=1
}

public class BlockEliminator : MonoBehaviour
{

    private const int ELIMINATE_COUNT = 3;
    private const int BLOCK_LAYER = 1 << 8;

    public float moveDuration;

    public float nextScenePos;
    private bool nextScene = false;

    private int activatedBlocksCount = 0;
    private int actingBlocksCount = 0;
    private GameObject eliminateArea;
    //private Vector3[] eliminatePos = new Vector3[3]; 
    private List<GameObject> activatedBlocks = new List<GameObject>();

    private List<GameObject> blockOriginPoints = new List<GameObject>();
    private GameObject[] eliminatePoints = new GameObject[ELIMINATE_COUNT];
    private bool[] eliminatejudge = new bool[ELIMINATE_COUNT];
    private GameObject player;
    private GameObject nextArea;
    private void Awake()
    {
        Initialize();
        GetEliminateArea();
        player = PlayerController.Player;
    }

    private void GetEliminateArea()
    {
        eliminateArea = GameObject.Find("BlockEliminateArea");
        for (int i = 0; i < ELIMINATE_COUNT; i++)
        {
            //eliminatePos[i] = eliminateArea.transform.GetChild(i).position;
            eliminatePoints[i] = eliminateArea.transform.GetChild(i).gameObject;
        }
        
    }

    private void Initialize()
    {
        for (int i = 0; i < ELIMINATE_COUNT; i++)
        {
            eliminatejudge[i] = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckClick();
        if(player.transform.position.x >= nextScenePos && !nextScene)
        {
            nextArea = GameObject.Find("NextArea");
            for (int i = 0; i < ELIMINATE_COUNT; i++)
            {
                //eliminatePos[i] = eliminateArea.transform.GetChild(i).position;
              //  eliminatePoints[i].transform.position = nextArea.transform.GetChild(i).position;
            }
        }
        
    }

    private void CheckClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray check = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(check.origin.x, check.origin.y), Vector2.down, 0.01f, BLOCK_LAYER);
            if (hit.collider)
            {
                GameObject nowBlock = hit.collider.gameObject;
                MoveBlock(nowBlock);
            }
        }
    }

    private void MoveBlock(GameObject block)
    {
        BlockStatus status = block.GetComponent<BlockStatus>();
        if(!block.GetComponent<BlockStatus>().moving)
        {
            if (status.state == BlockState.original && actingBlocksCount < ELIMINATE_COUNT)
            {
                status.state = BlockState.activated;
                activatedBlocks.Add(block);
                actingBlocksCount++;
                StartCoroutine(Move(block, block.transform.position, moveDuration, 1));
            }
            else if(status.state == BlockState.activated)
            {
                status.state = BlockState.original;
                activatedBlocks.Remove(block);
                actingBlocksCount--;
                StartCoroutine(Move(block, block.transform.position, moveDuration, -1));
            }
        }
    }

    private void TryEliminate()
    {
        
        BlockType type = activatedBlocks[0].GetComponent<BlockStatus>().type;
        for(int i = 0; i < activatedBlocksCount; i++)
        {
            
            BlockStatus status = activatedBlocks[i].GetComponent<BlockStatus>();
            if (status.order == status.ePointIndex + 1 || status.order == 0)
            {
                
                if (type == status.type)
                {
                    continue;
                }
                    
            }
            
            //foreach(GameObject block in activatedBlocks)
            //{
            //    BlockStatus status = block.GetComponent<BlockStatus>();
            //    status.state = BlockState.original;

            //    StartCoroutine(Move(block, block.transform.position, moveDuration, -1));
            //}
            
            return;
        }
        
        //activatedBlocksCount=0;
        //actingBlocksCount = 0;
        Eliminate();
    }

    private void Eliminate()
    {
        BlockType type = activatedBlocks[0].GetComponent<BlockStatus>().type;
        foreach (GameObject block in activatedBlocks)
        {
            //blockOriginPoints.Add(block.GetComponent<BlockStatus>().refreshPoint);
            //Destroy(block);
            //eliminatejudge[block.GetComponent<BlockStatus>().ePointIndex] = true;
            //block.SetActive(false);
        }
        //foreach (GameObject point in blockOriginPoints)
        //{
        //    BlockRefresher.Instance.CreateBlock(point);
        //}
        //activatedBlocks.Clear();
        //activatedBlocksCount=0;
        //actingBlocksCount = 0;
        //event
        switch (type)
        {
            case BlockType.turn:
                PlayerController.Instance.Turn();
                break;
            case BlockType.switchSpeed:
                PlayerController.Instance.SwitchSpeed();
                break;
            case BlockType.switchStop:
                PlayerController.Instance.SwitchStop();
                break;
            case BlockType.landform:
                //landform();
                break;
            case BlockType.easterEgg:
                //easterEgg();
                break;
        }
    }

    private IEnumerator Move(GameObject obj, Vector3 startPos, float duration, int dir)
    {
        BlockStatus status = obj.GetComponent<BlockStatus>();
        status.moving = true;
        float elapsedTime = 0f;
        if (dir == 1)
        {
            status.ePointIndex = FindEliminatePoint();
            status.eliminatePoint = eliminatePoints[status.ePointIndex];
            eliminatejudge[status.ePointIndex] = false;
            while ((obj.transform.position - status.eliminatePoint.transform.position).magnitude > 0.01f)
            {
                float t = elapsedTime / duration;
                obj.transform.position = Vector3.Lerp(startPos, status.eliminatePoint.transform.position, t);
                yield return null;
                elapsedTime += Time.deltaTime;
            }
        }
        else if (dir == -1)
        {
            eliminatejudge[status.ePointIndex] = true;
            while ((obj.transform.position - status.refreshPoint.transform.position).magnitude > 0.01f)
            {
                float t = elapsedTime / duration;
                obj.transform.position = Vector3.Lerp(startPos, status.refreshPoint.transform.position, t);
                yield return null;
                elapsedTime += Time.deltaTime;
            }
        }
        
        status.moving = false;
        activatedBlocksCount += dir;
        if (activatedBlocksCount == ELIMINATE_COUNT)
        {
            TryEliminate();
        }
    }

    private int FindEliminatePoint()
    {
        for(int i = 0; i< ELIMINATE_COUNT; i++)
        {
            if (eliminatejudge[i] == true)
            {
                return i;
            }
        }
        return -1;
    }

}
