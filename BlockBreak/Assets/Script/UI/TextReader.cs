using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextReader : MonoBehaviour
{
    public TextAsset readedText;//�ı����ݣ�csv��ʽ���ļ�
    public string[] textCut1;//���зָ��Ժ���ı�����
    public TextMeshProUGUI tmpText;//unity���ı���
    public int pageNumber;//��ǰҳ��
    public int id;//����ʶ���Լ�������
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
    /// ������һҳ������
    /// </summary>
    public void NextPage()
    {
        pageNumber++;
        tmpText.text = textCut1[pageNumber];
    }
    /// <summary>
    /// ����Ԥ��ָ��ȡ�ı�
    /// </summary>
    public void ReadText()
    {
        textCut1 = readedText.text.Split('\n');//���зָ��ı�
        NextPage();
    }
}
