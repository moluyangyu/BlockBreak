using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextReader : MonoBehaviour
{
    public TextAsset readedText;//�ı����ݣ�csv��ʽ���ļ�
    public string[] textCut1;//���зָ��Ժ���ı�����
    public TextMeshProUGUI tmpText;//unity���ı���
    public int pageNumber;//��ǰҳ��
    public int id;//����ʶ���Լ�������
    public string textName;//�����洢ʶ�����������༭����ʼ��
    public bool isOpen;//�Ի���Ŀ���
    public RawImage bubbleImage;//���ݵ�ͼƬ
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
    /// ������һҳ������
    /// </summary>
    public bool NextPage()
    {
        pageNumber++;
        if(pageNumber<textCut1.Length)//��ҳ��ͷ��ֹͣ��ҳ
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
    /// ����Ԥ��ָ��ȡ�ı�
    /// </summary>
    public void ReadText()
    {
        textCut1 = readedText.text.Split('\n');//���зָ��ı�
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
            if(tmpText.text.Equals("0")|| tmpText.text.Equals("�رնԻ���"))
            {
                CloseTalk();
            }
            else if(!isOpen)
            {
                OpenTalk();//��������˻����žʹ�
            }
            bool b=NextPage();//��������ж����˾Ͱ���һ���Ƶ������󴥷��Ϳ����ˣ���������ֶ�����Ч����ʱ����
            if(b)
            {
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
        bubbleImage.color = new Vector4(bubbleImage.color.r, bubbleImage.color.g, bubbleImage.color.b, 0f);
    }
    /// <summary>
    /// �򿪶Ի���
    /// </summary>
    public void OpenTalk()
    {
        isOpen = true;
        bubbleImage.color = new Vector4(bubbleImage.color.r, bubbleImage.color.g, bubbleImage.color.b, 255f);
    }
}
