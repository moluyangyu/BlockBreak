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
    public bool aniPlay;//是否播放动画
    private void Awake()
    {

       // tmpText = this.gameObject.GetComponent<TextMeshProUGUI>();
     //   ReadText();
    }
    void Start()
    {
        lockNumber = 0;
        Animator animator = GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        // 注册场景加载完成时的事件
        UiStatic.GameDexTrigger += UpdateDex;
    }

    private void OnDisable()
    {
        // 移除场景加载完成时的事件，以避免重复注册
        UiStatic.GameDexTrigger -= UpdateDex;
    }
    /// <summary>
    /// 将图鉴翻到指定的内容
    /// </summary>
    /// <param name="i"></param>
    public void UpdateDex(int i)
    {
        pageNumber = i;

        //  tmpText.text = textCut1[pageNumber];
       // pageImage.GetComponent<Image>().sprite = sprites[i];
        if (lockNumber<i)//新激活的图鉴编号
        {
            nextPage.SetActive(false);
            lastPage.SetActive(true);
            if(lockNumber==0)
            {
                lastPage.SetActive(false);
            }
            aniPlay = false;
            this.gameObject.GetComponent<Animator>().SetTrigger(pageNumber.ToString()+"n");//播放动画
            this.gameObject.GetComponent<MusicController>().PlayMusic();
            lockNumber = i;
        }else if(i<=1)
        {
            nextPage.SetActive(true);
            lastPage.SetActive(false);
            if(lockNumber==1)
            {
                nextPage.SetActive(false);
            }
        }else if(lockNumber==i)
        {
            nextPage.SetActive(false);
            lastPage.SetActive(true);
        }
        else
        {
            nextPage.SetActive(true);
            lastPage.SetActive(true);
        }
        if (aniPlay)
        {
            this.gameObject.GetComponent<Animator>().SetTrigger(pageNumber.ToString());//播放动画
            GameObject.Find("OpenGameDex").GetComponent<MusicController>().PlayMusic();//播放普通打开音效
        }
      //  aniPlay = true;

    }
    /// <summary>
    /// 加载下一页的内容
    /// </summary>
    public void NextPage()
    {      
        pageNumber++;
        nextPage.GetComponent<Animator>().SetTrigger("点击");
        if(aniPlay)
        {
            aniPlay = false;
            this.gameObject.GetComponent<Animator>().SetTrigger(pageNumber.ToString());
            UpdateDex(pageNumber);
          //  aniPlay = true;
        }
    }
    /// <summary>
    /// 加载上一页的内容
    /// </summary>
    public void LastPage()
    {
        pageNumber--;
        lastPage.GetComponent<Animator>().SetTrigger("点击");
        if (aniPlay)
        {
            aniPlay = false;
            this.gameObject.GetComponent<Animator>().SetTrigger(pageNumber.ToString() + "l");
            UpdateDex(pageNumber);
          //  aniPlay = true;
        }
    }
    /// <summary>
    /// 动画结束调用这个函数
    /// </summary>
    public void AniEnd()
    {
        aniPlay = true;
    }
    /// <summary>
    /// 依据预设分割获取文本
    /// </summary>
    public void ReadText()
    {
        //textCut1 = readedText.text.Split('\n');//按行分割文本
        //  int.TryParse(textCut1[0], out id);
    }
}
