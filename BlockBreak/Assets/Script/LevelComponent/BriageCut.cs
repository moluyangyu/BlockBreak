using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriageCut : MonoBehaviour
{
    // Start is called before the first frame update
    private HingeJoint2D hingeJoint2d;
    public GameObject ropeController;//�������Ƶ���
    void Start()
    {
        hingeJoint2d = this.gameObject.GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// �ж�����������
    /// </summary>
    public void Cut()
    {
        ropeController.GetComponent<RopeController>().Cut();
        hingeJoint2d.enabled = false;//�ر��������
    }
}
