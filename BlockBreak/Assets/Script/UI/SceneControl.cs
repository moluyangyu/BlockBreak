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
    public GameObject levelLock;
    [Header("��ǰ�ؿ���")]
    public int checkNumber;
    void Start()
    {
      //  win = GameObject.Find("Win");
     //   win?.SetActive(false);
      //  lose = GameObject.Find("Lose");
     //   lose?.SetActive(false);
        //levelLock = GameObject.Find("LevelLock");
    }

    // Update is called once per frame
    void Update()
    {

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
    public void QuitGame()
    {
        Application.Quit();
    }
}
