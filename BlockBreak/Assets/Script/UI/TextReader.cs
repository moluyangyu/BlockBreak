using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextReader : MonoBehaviour
{
    public TextAsset readedText;//文本内容，csv格式的文件
    public string[] textCut1;//按行分割以后的文本内容
    public TextMeshProUGUI tmpText;//unity的文本框
    public int pageNumber;//当前页码
    public int id;//用来识别自己的所属
    // Start is called before the first frame update
    void Start()
    {
        pageNumber = 0;
        tmpText = this.gameObject.GetComponent<TextMeshProUGUI>();
        ReadText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 加载下一页的内容
    /// </summary>
    public void NextPage()
    {
        pageNumber++;
        tmpText.text = textCut1[pageNumber];
    }
    /// <summary>
    /// 依据预设分割获取文本
    /// </summary>
    public void ReadText()
    {
        textCut1 = readedText.text.Split('\n');//按行分割文本
        NextPage();
    }
}
