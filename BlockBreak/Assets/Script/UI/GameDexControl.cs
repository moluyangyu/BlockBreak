using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameDexControl : MonoBehaviour
{
    // Start is called before the first frame update
    public TextAsset readedText;//文本内容，csv格式的文件
    public string[] textCut1;//按行分割以后的文本内容
    public Sprite[] sprites;//图鉴的内容合集
    public TextMeshProUGUI tmpText;//unity的文本框
    public int pageNumber;//当前页码
    public int lockNumber;//最新解锁的图鉴编号
    public GameObject nextPage;//下一页按钮
    public GameObject lastPage;//上一页按钮
    public GameObject pageImage;//图鉴的配图
    private void Awake()
    {
        UiStatic.GameDexTrigger += UpdateDex;
        tmpText = this.gameObject.GetComponent<TextMeshProUGUI>();
        ReadText();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 将图鉴翻到指定的内容
    /// </summary>
    /// <param name="i"></param>
    public void UpdateDex(int i)
    {
        if(lockNumber<=i)//新激活的图鉴编号
        {
            nextPage.SetActive(false);
            lastPage.SetActive(true);
            lockNumber = i;
        }else if(i==1)
        {
            nextPage.SetActive(true);
            lastPage.SetActive(false);
        }else
        {
            nextPage.SetActive(true);
            lastPage.SetActive(true);
        }
        pageNumber = i;

        tmpText.text = textCut1[pageNumber];
        pageImage.GetComponent<Image>().sprite = sprites[i];
    }
    /// <summary>
    /// 加载下一页的内容
    /// </summary>
    public void NextPage()
    {
        pageNumber++;
        UpdateDex(pageNumber);
    }
    /// <summary>
    /// 加载上一页的内容
    /// </summary>
    public void LastPage()
    {
        pageNumber--;
        UpdateDex(pageNumber);
    }
    /// <summary>
    /// 依据预设分割获取文本
    /// </summary>
    public void ReadText()
    {
        textCut1 = readedText.text.Split('\n');//按行分割文本
        //  int.TryParse(textCut1[0], out id);
    }
}
