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

    public GameObject blockPrefab;
    public float moveDuration;

    private int activatedBlocksCount = 0;
    GameObject eliminateArea;
    Vector3[] eliminatePos = new Vector3[3];
    List<GameObject> activatedBlocks = new List<GameObject>();

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
        switch (status.state) {
            case BlockState.original:
                StartCoroutine(Move(block, block.transform.position, eliminatePos[activatedBlocksCount], moveDuration));
                status.state = BlockState.activated;
                activatedBlocks.Add(block);
                activatedBlocksCount++;
                if(activatedBlocksCount == 3)
                {
                    TryEliminate();
                }
                break;
            case BlockState.activated:
                StartCoroutine(Move(block, block.transform.position, status.originPos, moveDuration));
                status.state = BlockState.original;
                activatedBlocks.Remove(block);
                activatedBlocksCount--;
                break;
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
                MoveBlock(block);
            }
            return;
        }
        Eliminate();
    }

    private void Eliminate()
    {
        Debug.Log("Eliminate!");
    }

    private IEnumerator Move(GameObject obj, Vector3 startPos,  Vector3 endPos, float duration)
    {
        float elapsedTime = 0f;
        while (obj.transform.position != endPos)
        {
            float t = elapsedTime / duration;
            obj.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
    }

    public class Block
    {
        public BlockType type;
        public BlockState state;
        public GameObject blockObject;
        public Block(BlockType type)
        {
            this.type = type;
            state = BlockState.original;
        }
    }
}
