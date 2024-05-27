using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextReader : MonoBehaviour
{
    public TextAsset readedText;//�ı����ݣ�csv��ʽ���ļ�
    public string[] textCut1;//���зָ��Ժ���ı�����
    public string[][] textCut2;//���зָ�����ϰ��зָ�
    public TextMeshProUGUI tmpText;//unity���ı���
    public TextMeshProUGUI nameText;//�������ı���
    public int pageNumber;//��ǰҳ��
    public int id;//����ʶ���Լ�������
    public string textName;//�����洢ʶ�����������༭����ʼ��
    public bool isOpen;//�Ի���Ŀ���
    public RawImage bubbleImage;//���ݵ�ͼƬ
    public Image bubbleImageAni;//�������ݵ�ͼƬ
    public GameObject profile;//����ͷ��
    public float c_speed;//��ʾ�������
    public bool textLock;//ֻ������������˲��ܵ����һ���Ի�����
    public GameObject DialogueMask;//�Ի����ϵ��ɰ�
    // Start is called before the first frame update
    private void Awake()
    {
        DialogueMask = GameObject.Find("�Ի��ɰ�");
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
        // ע�᳡���������ʱ���¼�
        SceneManager.sceneLoaded += OnSceneLoaded;
        UiStatic.TalkKick += TalkKick;
    }

    private void OnDisable()
    {
        // �Ƴ������������ʱ���¼����Ա����ظ�ע��
        SceneManager.sceneLoaded -= OnSceneLoaded;
        UiStatic.TalkKick -= TalkKick;
    }

    // �����������ʱ���õķ���
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        pageNumber = 0;
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))//�����ã����ɾ��
        //{
        //    UiStatic.TalkKickIssue("С������");
        //}
    }
    /// <summary>
    /// �������ı���һ���ı���
    /// </summary>
    /// <param name="name"></param>
    public void MakePhiZ(string name)
    {
        name = name.Replace("\r", "");//ȥ������Ļس�
        if (profile != null) profile.gameObject.GetComponent<Animator>().SetTrigger(name) ;
    }
    /// <summary>
    /// ʵ�ִ��ֻ�Ч��
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
    /// ������һҳ������
    /// </summary>
    public bool NextPage()
    {
        pageNumber++;
        if(pageNumber<(textCut1.Length-1))//��ҳ��ͷ��ֹͣ��ҳ
        {
            if (textCut2[pageNumber][0] == "0" || textCut2[pageNumber][0] == "#")
            {
                CloseTalk();
            }
            else if (!isOpen)
            {
                OpenTalk();//��������˻����žʹ�
                // tmpText.text = textCut2[pageNumber][0];//�ɰ�Ч����̭��
                if (nameText != null) nameText.text = textCut2[1][1];//�����Ƿ���Ի�����ҵ�����
               
                StartCoroutine(UpdateText(textCut2[pageNumber][0]));
            }
            else
            {
               // tmpText.text = textCut2[pageNumber][0];//�ɰ�Ч����̭��
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
    /// ����Ԥ��ָ��ȡ�ı�
    /// </summary>
    public void ReadText()
    {
        textCut1 = readedText.text.Split('\n');//���зָ��ı�
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
    /// ����ͻ�ƽ��Ի��ĺ���
    /// </summary>
    public int TalkKick(int i)
    {
        int a = 0;
        if(i==id )
        {
            
            bool b = NextPage();//��������ж����˾Ͱ���һ���Ƶ������󴥷��Ϳ����ˣ���������ֶ�����Ч����ʱ����
            if (b)
            {
                CloseTalk();
                //return false;
                if (DialogueMask != null)
                {
                    DialogueMask.SetActive(false);//�رնԻ�����
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
    /// �����Ի���ʷʫ�����ܣ���
    /// </summary>
    public void SkipTalk()
    {
        if(pageNumber>=2) pageNumber = (textCut1.Length - 1);
       // UiStatic.TalkKickIssue(id);
    }
    /// <summary>
    /// �رնԻ����õ�
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
            this.gameObject.GetComponent<Animator>().SetTrigger("����");
            // bubbleImageAni.color = new Vector4(bubbleImage.color.r, bubbleImage.color.g, bubbleImage.color.b, 0f);
        }

    }
    /// <summary>
    /// �򿪶Ի���
    /// </summary>
    public void OpenTalk()
    {
        isOpen = true;
        if (bubbleImage != null) bubbleImage.color = new Vector4(bubbleImage.color.r, bubbleImage.color.g, bubbleImage.color.b, 255f);
        if (bubbleImageAni != null)
        {
            //bubbleImageAni.color = new Vector4(bubbleImage.color.r, bubbleImage.color.g, bubbleImage.color.b, 255f);
            this.gameObject.GetComponent<Animator>().SetTrigger("�Ի��򵯳�");
        } 
        if(DialogueMask!=null)
        {
            DialogueMask.SetActive(true);//�رնԻ�����
        }
        // profile.color = new Vector4(bubbleImage.color.r, bubbleImage.color.g, bubbleImage.color.b, 255f);
        if (profile != null) profile.SetActive(true);
    }
}
