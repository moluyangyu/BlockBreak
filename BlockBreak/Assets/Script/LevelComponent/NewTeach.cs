using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTeach : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject teachSprite;//���ֽ�ѧ��ͼ
    public GameObject cameraObject;//�õ�����������ƺ���
    void Start()
    {
        if(teachSprite!=null)
        {
            teachSprite.SetActive(false);
        }
        cameraObject = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenTeach()
    {
        if (teachSprite != null && cameraObject != null)
        {
            teachSprite.SetActive(true);
            cameraObject.GetComponent<CameraController>().SceneBlack(true);
        }
    }
    public void CloseTeach()
    {
        if (teachSprite != null && cameraObject != null)
        {
            teachSprite.SetActive(false);
            cameraObject.GetComponent<CameraController>().SceneBlack(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
