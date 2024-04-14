using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    // Start is called before the first frame update
    //��ʤ����
    private GameObject win;
    //ʧ�ܽ���
    private GameObject lose;
    //�ؿ�����
    private GameObject levelLock;
    //[Header("��ǰ�ؿ���")]
    //public int checkNumber;
    public GameObject dex;//ͼ����Ϸ��
    public GameObject dexButton;//��ͼ���İ�ť
    void Start()
    {
        //  win = GameObject.Find("Win");
        //   win?.SetActive(false);
        //  lose = GameObject.Find("Lose");
        //   lose?.SetActive(false);
        //levelLock = GameObject.Find("LevelLock");
        dex = GameObject.Find("GameDex");
        dexButton = GameObject.Find("OpenGameDex");
        UiStatic.GameDexTrigger += OpenDex;
        dex.SetActive(false);
        dexButton.SetActive(false);
    }
    
    /// <summary>
    /// ����ʤ������
    /// </summary>
    /// <param name="a"></param>���������Ӯ�ı���
    public void End(bool a)
    {
        if (a)
        {
            win.SetActive(true);
          //  levelLock.GetComponent<LevelLock>().AddNumber(checkNumber);//ͨ���˾ͽ����µĹؿ�
        }
        else
        {
            lose.SetActive(true);
        }
    }
    /// <summary>
    /// ��UI�õ���ת������
    /// </summary>
    /// <param name="i"></param>�������
    public void LoadScence(int i)
    {
        if (levelLock != null)
        {
           /* if (i <= levelLock.GetComponent<LevelLock>().levelNumber)
            {
                SceneManager.LoadScene(i);
            }*/
        }
        else
        {
            SceneManager.LoadScene(i);
        }
    }
    /// <summary>
    /// �˳���Ϸ
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
    /// <summary>
    /// ��ͼ��
    /// </summary>
    public void OpenDex(int i)
    {
        dex.SetActive(true);
        dexButton.SetActive(true);
        UiStatic.GameDexTrigger -= OpenDex;
        //UiStatic.UiOpenIssue(true);
        UiStatic.GameDexTriggerIssue(i);//��ͼ����Ϸ����Ժ��ٴ�һ����Ϣ���ܽ��յ�
        UiStatic.GameDexTrigger += OpenDex;
        
    }
    public void OpenDex()//UI���水ť�õ�����
    {
        dex.SetActive(true);
        dex.GetComponent<GameDexControl>().UpdateDex(dex.GetComponent<GameDexControl>().pageNumber);//����һ�δ򿪵�ҳ��
        dexButton.SetActive(false);
        UiStatic.UiOpenIssue(false);
    }
    /// <summary>
    /// �ر�ͼ��
    /// </summary>
    public void CloseDex()
    {
        dex.SetActive(false);
        dexButton.SetActive(true);
        UiStatic.UiOpenIssue(true);
    }
}
