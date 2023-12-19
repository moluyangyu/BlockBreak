using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    moveLeft,
    moveRight,
    speedUp,
    stop,
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

    public float moveDuration;

    private int activatedBlocksCount = 0;
    private int actingBlocksCount = 0;
    private GameObject eliminateArea;
    private Vector3[] eliminatePos = new Vector3[3];
    private List<GameObject> activatedBlocks = new List<GameObject>();

    private List<Vector3> blockOriginPos = new List<Vector3>();

    private void Awake()
    {
        GetEliminateArea();
    }

    private void GetEliminateArea()
    {
        eliminateArea = GameObject.Find("BlockEliminateArea");
        for (int i = 0; i < 3; i++)
        {
            eliminatePos[i] = eliminateArea.transform.GetChild(i).position;
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
    }

    private void CheckClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray check = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(check.origin.x, check.origin.y), Vector2.down, 0.01f, 1<<8);
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
        if(actingBlocksCount < 3 && !block.GetComponent<BlockStatus>().moving)
        {
            if (status.state == BlockState.original)
            {
                status.state = BlockState.activated;
                activatedBlocks.Add(block);
                actingBlocksCount++;
                StartCoroutine(Move(block, block.transform.position, eliminatePos[actingBlocksCount - 1], moveDuration, 1));
            }
        }
    }

    private void TryEliminate()
    {
        BlockType type = activatedBlocks[0].GetComponent<BlockStatus>().type;
        for(int i = 1; i < activatedBlocksCount; i++)
        {
            if (type == activatedBlocks[i].GetComponent<BlockStatus>().type)
                continue;
            foreach(GameObject block in activatedBlocks)
            {
                BlockStatus status = block.GetComponent<BlockStatus>();
                status.state = BlockState.original;

                StartCoroutine(Move(block, block.transform.position, status.originPos, moveDuration, -1));
            }
            activatedBlocks.Clear();
            //activatedBlocksCount=0;
            actingBlocksCount=0;
            return;
        }
        Eliminate();
    }

    private void Eliminate()
    {
        Debug.Log("Eliminate!");
        BlockType type = activatedBlocks[0].GetComponent<BlockStatus>().type;
        foreach (GameObject block in activatedBlocks)
        {
            blockOriginPos.Add(block.GetComponent<BlockStatus>().originPos);
            Destroy(block);
        }
        foreach (Vector3 pos in blockOriginPos)
        {
            BlockRefresher.Instance.CreateBlock(pos);
        }
        activatedBlocks.Clear();
        activatedBlocksCount=0;
        actingBlocksCount = 0;
        //event
    }

    private IEnumerator Move(GameObject obj, Vector3 startPos,  Vector3 endPos, float duration, int dir)
    {
        BlockStatus status = obj.GetComponent<BlockStatus>();
        status.moving = true;
        float elapsedTime = 0f;
        while (obj.transform.position != endPos)
        {
            float t = elapsedTime / duration;
            obj.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        status.moving = false;
        activatedBlocksCount += dir;
        if (activatedBlocksCount == 3)
        {
            TryEliminate();
        }
    }
}
