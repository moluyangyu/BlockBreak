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
    public bool aniPlay;//�Ƿ񲥷Ŷ���
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
        // ע�᳡���������ʱ���¼�
        UiStatic.GameDexTrigger += UpdateDex;
    }

    private void OnDisable()
    {
        // �Ƴ������������ʱ���¼����Ա����ظ�ע��
        UiStatic.GameDexTrigger -= UpdateDex;
    }
    /// <summary>
    /// ��ͼ������ָ��������
    /// </summary>
    /// <param name="i"></param>
    public void UpdateDex(int i)
    {
        pageNumber = i;

        //  tmpText.text = textCut1[pageNumber];
       // pageImage.GetComponent<Image>().sprite = sprites[i];
        if (lockNumber<i)//�¼����ͼ�����
        {
            nextPage.SetActive(false);
            lastPage.SetActive(true);
            if(lockNumber==0)
            {
                lastPage.SetActive(false);
            }
            aniPlay = false;
            this.gameObject.GetComponent<Animator>().SetTrigger(pageNumber.ToString()+"n");//���Ŷ���
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
            this.gameObject.GetComponent<Animator>().SetTrigger(pageNumber.ToString());//���Ŷ���
            GameObject.Find("OpenGameDex").GetComponent<MusicController>().PlayMusic();//������ͨ����Ч
        }
      //  aniPlay = true;

    }
    /// <summary>
    /// ������һҳ������
    /// </summary>
    public void NextPage()
    {      
        pageNumber++;
        nextPage.GetComponent<Animator>().SetTrigger("���");
        if(aniPlay)
        {
            aniPlay = false;
            this.gameObject.GetComponent<Animator>().SetTrigger(pageNumber.ToString());
            UpdateDex(pageNumber);
          //  aniPlay = true;
        }
    }
    /// <summary>
    /// ������һҳ������
    /// </summary>
    public void LastPage()
    {
        pageNumber--;
        lastPage.GetComponent<Animator>().SetTrigger("���");
        if (aniPlay)
        {
            aniPlay = false;
            this.gameObject.GetComponent<Animator>().SetTrigger(pageNumber.ToString() + "l");
            UpdateDex(pageNumber);
          //  aniPlay = true;
        }
    }
    /// <summary>
    /// �������������������
    /// </summary>
    public void AniEnd()
    {
        aniPlay = true;
    }
    /// <summary>
    /// ����Ԥ��ָ��ȡ�ı�
    /// </summary>
    public void ReadText()
    {
        //textCut1 = readedText.text.Split('\n');//���зָ��ı�
        //  int.TryParse(textCut1[0], out id);
    }
}
