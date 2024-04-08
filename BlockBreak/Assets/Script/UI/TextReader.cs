using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    public GameObject profile;//����ͷ��
    public float c_speed;//��ʾ�������
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
        if (Input.GetMouseButtonDown(0))//�����ã����ɾ��
        {
            UiStatic.TalkKickIssue("С������");
        }
    }
    /// <summary>
    /// �������ı���һ���ı���
    /// </summary>
    /// <param name="name"></param>
    public void MakePhiZ(string name)
    {
        name = name.Replace("\r", "");//ȥ������Ļس�
        profile.gameObject.GetComponent<Animator>().SetTrigger(name) ;
    }
    /// <summary>
    /// ʵ�ִ��ֻ�Ч��
    /// </summary>
    /// <param name="_text"></param>
    /// <returns></returns>
    public IEnumerator UpdateText(string _text)
    {
        tmpText.text = "";
        foreach(char letter in _text.ToCharArray())
        {
            tmpText.text += letter;
            yield return new WaitForSeconds(c_speed);
        }
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
                // tmpText.text = textCut2[pageNumber][0];//�ɰ�Ч����̭��
                nameText.text = textCut2[1][1];//�����Ƿ���Ի�����ҵ�����
                OpenTalk();//��������˻����žʹ�
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
        NextPage();
      //  int.TryParse(textCut1[0], out id);
    }
    /// <summary>
    /// ����ͻ�ƽ��Ի��ĺ���
    /// </summary>
    public bool TalkKick(int i)
    {
        if(i==id)
        {
            
            bool b = NextPage();//��������ж����˾Ͱ���һ���Ƶ������󴥷��Ϳ����ˣ���������ֶ�����Ч����ʱ����
            if (b)
            {
                CloseTalk();
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// �رնԻ����õ�
    /// </summary>
    public void CloseTalk()
    {
        isOpen = false;
        tmpText.text = "";
        nameText.text = "";
        bubbleImage.color = new Vector4(bubbleImage.color.r, bubbleImage.color.g, bubbleImage.color.b, 0f);
        //profile.color = new Vector4(bubbleImage.color.r, bubbleImage.color.g, bubbleImage.color.b, 0f);
        profile.SetActive(false);
    }
    /// <summary>
    /// �򿪶Ի���
    /// </summary>
    public void OpenTalk()
    {
        isOpen = true;
        bubbleImage.color = new Vector4(bubbleImage.color.r, bubbleImage.color.g, bubbleImage.color.b, 255f);
        // profile.color = new Vector4(bubbleImage.color.r, bubbleImage.color.g, bubbleImage.color.b, 255f);
        profile.SetActive(true);
    }
}
