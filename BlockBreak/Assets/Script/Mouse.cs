using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mouse : MonoBehaviour
{
    // Start is called before the first frame update
    public Image hammer;  //����һ��ͼƬ

    void Start()
    {
       // DontDestroyOnLoad(this);
        Cursor.visible = false;
        hammer = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0;
        
        hammer.rectTransform.position = mousePosition; //ͼƬ��λ�ø�������λ��
    }
}
