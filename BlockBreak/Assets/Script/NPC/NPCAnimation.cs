using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private DragonBones.UnityArmatureComponent animDB;
    void Start()
    {
        animDB = GetComponent<DragonBones.UnityArmatureComponent>();
        animDB.animation.Play("start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
