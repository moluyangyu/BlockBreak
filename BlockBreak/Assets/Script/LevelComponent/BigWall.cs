using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWall : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] animals;//
    public bool[] password;//√‹¬Î
    public GameObject groundEvent;
    void Start()
    {
        groundEvent = GameObject.Find("GroundEvent");
        groundEvent.GetComponent<GroundEvent>().MoveStart += MoveStart;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveStart(int i, FloorClass a)
    {
        int k = 0;
        for(int j=0;j<7;j++)
        {
            if(animals[j].GetComponent<ActivityGround>().height==password[j])
            {
                k++;
            }
        }
        //√‹¬Î’˝»∑
        this.GetComponent<ActivityGround>().MoveStart(this.GetComponent<ActivityGround>().serialNumber, FloorClass.bollard);
        PlayerController.Instance.SwitchStop(1);
    }
}
