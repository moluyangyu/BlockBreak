using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextReader : MonoBehaviour
{
    public TextAsset readedText;//文本内容，csv格式的文件
    public string[] textCut1;//按行分割以后的文本内容
    public TextMeshProUGUI tmpText;//unity的文本框
    public int pageNumber;//当前页码
    public int id;//用来识别自己的所属
    public string textName;//用来存储识别代码谨防被编辑器初始化
    public bool isOpen;//对话框的开关
    public RawImage bubbleImage;//气泡的图片
    // Start is called before the first frame update
    void Start()
    {
        pageNumber = 0;
        // tmpText = this.gameObject.GetComponent<TextMeshProUGUI>();
        bubbleImage = this.gameObject.GetComponent<RawImage>();
        ReadText();
        CloseTalk();
        UiStatic.TalkKick += TalkKick;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 加载下一页的内容
    /// </summary>
    public bool NextPage()
    {
        pageNumber++;
        if(pageNumber<textCut1.Length)//翻页倒头了停止翻页
        {
            tmpText.text = textCut1[pageNumber];
        }
        else
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 依据预设分割获取文本
    /// </summary>
    public void ReadText()
    {
        textCut1 = readedText.text.Split('\n');//按行分割文本
        NextPage();
      //  int.TryParse(textCut1[0], out id);
    }
    /// <summary>
    /// 订阅突推进对话的函数
    /// </summary>
    public bool TalkKick(int i)
    {
        if(i==id)
        {
            if(tmpText.text.Equals("0")|| tmpText.text.Equals("关闭对话框"))
            {
                CloseTalk();
            }
            else if(!isOpen)
            {
                OpenTalk();//如果有字了还关着就打开
            }
            bool b=NextPage();//如果后期有动画了就把这一步移到动画后触发就可以了，还有逐个字读出的效果倒时候整
            if(b)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 关闭对话框用的
    /// </summary>
    public void CloseTalk()
    {
        isOpen = false;
        tmpText.text = "";
        bubbleImage.color = new Vector4(bubbleImage.color.r, bubbleImage.color.g, bubbleImage.color.b, 0f);
    }
    /// <summary>
    /// 打开对话框
    /// </summary>
    public void OpenTalk()
    {
        isOpen = true;
        bubbleImage.color = new Vector4(bubbleImage.color.r, bubbleImage.color.g, bubbleImage.color.b, 255f);
    }
}
