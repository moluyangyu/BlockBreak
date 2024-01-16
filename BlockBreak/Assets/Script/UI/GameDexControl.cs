using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameDexControl : MonoBehaviour
{
    // Start is called before the first frame update
    public TextAsset readedText;//�ı����ݣ�csv��ʽ���ļ�
    public string[] textCut1;//���зָ��Ժ���ı�����
    public Sprite[] sprites;//ͼ�������ݺϼ�
    public TextMeshProUGUI tmpText;//unity���ı���
    public int pageNumber;//��ǰҳ��
    public int lockNumber;//���½�����ͼ�����
    public GameObject nextPage;//��һҳ��ť
    public GameObject lastPage;//��һҳ��ť
    public GameObject pageImage;//ͼ������ͼ
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
    /// ��ͼ������ָ��������
    /// </summary>
    /// <param name="i"></param>
    public void UpdateDex(int i)
    {
        if(lockNumber<=i)//�¼����ͼ�����
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
    /// ������һҳ������
    /// </summary>
    public void NextPage()
    {
        pageNumber++;
        UpdateDex(pageNumber);
    }
    /// <summary>
    /// ������һҳ������
    /// </summary>
    public void LastPage()
    {
        pageNumber--;
        UpdateDex(pageNumber);
    }
    /// <summary>
    /// ����Ԥ��ָ��ȡ�ı�
    /// </summary>
    public void ReadText()
    {
        textCut1 = readedText.text.Split('\n');//���зָ��ı�
        //  int.TryParse(textCut1[0], out id);
    }
}
