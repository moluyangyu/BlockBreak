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
    private GameObject levelLock;
    //[Header("当前关卡数")]
    //public int checkNumber;
    public GameObject dex;//图鉴游戏体
    public GameObject dexButton;//打开图鉴的按钮
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
    /// <summary>
    /// 退出游戏
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
    /// <summary>
    /// 打开图鉴
    /// </summary>
    public void OpenDex(int i)
    {
        dex.SetActive(true);
        dexButton.SetActive(true);
        UiStatic.GameDexTrigger -= OpenDex;
        //UiStatic.UiOpenIssue(true);
        UiStatic.GameDexTriggerIssue(i);//在图鉴游戏体打开以后再传一次消息才能接收到
        UiStatic.GameDexTrigger += OpenDex;
        
    }
    public void OpenDex()//UI界面按钮用的重载
    {
        dex.SetActive(true);
        dex.GetComponent<GameDexControl>().UpdateDex(dex.GetComponent<GameDexControl>().pageNumber);//打开上一次打开的页码
        dexButton.SetActive(false);
        UiStatic.UiOpenIssue(false);
    }
    /// <summary>
    /// 关闭图鉴
    /// </summary>
    public void CloseDex()
    {
        dex.SetActive(false);
        dexButton.SetActive(true);
        UiStatic.UiOpenIssue(true);
    }
}
