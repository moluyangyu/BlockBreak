using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextReader : MonoBehaviour
{
    public TextAsset readedText;//文本内容，csv格式的文件
    public string[] textCut1;//按行分割以后的文本内容
    public string[][] textCut2;//按行分割基础上按列分割
    public TextMeshProUGUI tmpText;//unity的文本框
    public TextMeshProUGUI nameText;//姓名的文本框
    public int pageNumber;//当前页码
    public int id;//用来识别自己的所属
    public string textName;//用来存储识别代码谨防被编辑器初始化
    public bool isOpen;//对话框的开关
    public RawImage bubbleImage;//气泡的图片
    public Image bubbleImageAni;//动画气泡的图片
    public GameObject profile;//人物头像
    public float c_speed;//显示间隔秒数
    public bool textLock;//只有文字输出完了才能点击下一个对话的锁
    public GameObject DialogueMask;//对话贴上的蒙版
    // Start is called before the first frame update
    private void Awake()
    {
        DialogueMask = GameObject.Find("对话蒙版");
    }
    void Start()
    {
        
        // tmpText = this.gameObject.GetComponent<TextMeshProUGUI>();
        bubbleImage = this.gameObject.GetComponent<RawImage>();

        if (bubbleImage != null)
        {

        }
        else
        {
            bubbleImageAni = this.gameObject.GetComponent<Image>();
        }
        ReadText();
        CloseTalk();
        textLock = false;
        if(DialogueMask!=null)DialogueMask.SetActive(false);

    }
    private void OnEnable()
    {
        // 注册场景加载完成时的事件
        SceneManager.sceneLoaded += OnSceneLoaded;
        UiStatic.TalkKick += TalkKick;
    }

    private void OnDisable()
    {
        // 移除场景加载完成时的事件，以避免重复注册
        SceneManager.sceneLoaded -= OnSceneLoaded;
        UiStatic.TalkKick -= TalkKick;
    }

    // 场景加载完成时调用的方法
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        pageNumber = 0;
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))//测试用，最后删除
        //{
        //    UiStatic.TalkKickIssue("小蓝测试");
        //}
    }
    /// <summary>
    /// 做出和文本名一样的表情
    /// </summary>
    /// <param name="name"></param>
    public void MakePhiZ(string name)
    {
        name = name.Replace("\r", "");//去除多余的回车
        if (profile != null) profile.gameObject.GetComponent<Animator>().SetTrigger(name) ;
    }
    /// <summary>
    /// 实现打字机效果
    /// </summary>
    /// <param name="_text"></param>
    /// <returns></returns>
    public IEnumerator UpdateText(string _text)
    {
        tmpText.text = "";
        UiStatic.textLock = true;
        textLock = true;
        foreach(char letter in _text.ToCharArray())
        {
            tmpText.text += letter;
            yield return new WaitForSeconds(c_speed);
        }
        textLock = false;
        UiStatic.textLock = false;
    }
    /// <summary>
    /// 加载下一页的内容
    /// </summary>
    public bool NextPage()
    {
        pageNumber++;
        if(pageNumber<(textCut1.Length-1))//翻页倒头了停止翻页
        {
            if (textCut2[pageNumber][0] == "0" || textCut2[pageNumber][0] == "#")
            {
                CloseTalk();
            }
            else if (!isOpen)
            {
                OpenTalk();//如果有字了还关着就打开
                // tmpText.text = textCut2[pageNumber][0];//旧版效果淘汰了
                if (nameText != null) nameText.text = textCut2[1][1];//这里是放入对话的玩家的名字
               
                StartCoroutine(UpdateText(textCut2[pageNumber][0]));
            }
            else
            {
               // tmpText.text = textCut2[pageNumber][0];//旧版效果淘汰了
                StartCoroutine(UpdateText(textCut2[pageNumber][0]));
            }
            if(textCut2[pageNumber][1] != "0\r")
            {
                MakePhiZ(textCut2[pageNumber][1]);
            }
            
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
        int i = 0;
        textCut2 = new string[textCut1.Length][];
        foreach (string line in textCut1)
        {
           // Debug.Log("Current Line: " + line);
            textCut2[i] = line.Split(',');
            i++;
        }
        id = int.Parse(textCut1[0]);
        NextPage();
      //  int.TryParse(textCut1[0], out id);
    }
    /// <summary>
    /// 订阅突推进对话的函数
    /// </summary>
    public int TalkKick(int i)
    {
        int a = 0;
        if(i==id )
        {
            
            bool b = NextPage();//如果后期有动画了就把这一步移到动画后触发就可以了，还有逐个字读出的效果倒时候整
            if (b)
            {
                CloseTalk();
                //return false;
                if (DialogueMask != null)
                {
                    DialogueMask.SetActive(false);//关闭对话遮罩
                }
                this.gameObject.SetActive(false);
            }
            else
            {
                a += 1;
            }
        }
        return a;
    }
    /// <summary>
    /// 跳过对话，史诗级功能（）
    /// </summary>
    public void SkipTalk()
    {
        if(pageNumber>=2) pageNumber = (textCut1.Length - 1);
       // UiStatic.TalkKickIssue(id);
    }
    /// <summary>
    /// 关闭对话框用的
    /// </summary>
    public void CloseTalk()
    {
        isOpen = false;
        tmpText.text = "";
        if(nameText!=null)nameText.text = "";
        if(bubbleImage!=null) bubbleImage.color = new Vector4(bubbleImage.color.r, bubbleImage.color.g, bubbleImage.color.b, 0f);
        //profile.color = new Vector4(bubbleImage.color.r, bubbleImage.color.g, bubbleImage.color.b, 0f);
        if(profile!=null)profile.SetActive(false);
        if (bubbleImageAni != null)
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("隐藏");
            // bubbleImageAni.color = new Vector4(bubbleImage.color.r, bubbleImage.color.g, bubbleImage.color.b, 0f);
        }

    }
    /// <summary>
    /// 打开对话框
    /// </summary>
    public void OpenTalk()
    {
        isOpen = true;
        if (bubbleImage != null) bubbleImage.color = new Vector4(bubbleImage.color.r, bubbleImage.color.g, bubbleImage.color.b, 255f);
        if (bubbleImageAni != null)
        {
            //bubbleImageAni.color = new Vector4(bubbleImage.color.r, bubbleImage.color.g, bubbleImage.color.b, 255f);
            this.gameObject.GetComponent<Animator>().SetTrigger("对话框弹出");
        } 
        if(DialogueMask!=null)
        {
            DialogueMask.SetActive(true);//关闭对话遮罩
        }
        // profile.color = new Vector4(bubbleImage.color.r, bubbleImage.color.g, bubbleImage.color.b, 255f);
        if (profile != null) profile.SetActive(true);
    }
}
