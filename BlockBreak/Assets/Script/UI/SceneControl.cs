using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    // Start is called before the first frame update
    //获胜界面
    private GameObject win;
    //失败界面
    private GameObject lose;
    //关卡解锁
    public GameObject levelLock;
    [Header("当前关卡数")]
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
    /// 开关胜负界面
    /// </summary>
    /// <param name="a"></param>传达输或者赢的变量
    public void End(bool a)
    {
        if (a)
        {
            win.SetActive(true);
          //  levelLock.GetComponent<LevelLock>().AddNumber(checkNumber);//通关了就解锁新的关卡
        }
        else
        {
            lose.SetActive(true);
        }
    }
    /// <summary>
    /// 给UI用的跳转场景的
    /// </summary>
    /// <param name="i"></param>场景编号
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
