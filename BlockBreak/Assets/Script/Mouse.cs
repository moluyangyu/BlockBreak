using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mouse : MonoBehaviour
{
    // Start is called before the first frame update
    public Image hammer;  //定义一个图片

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
        
        hammer.rectTransform.position = mousePosition; //图片的位置跟随鼠标的位置
    }
}
