using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggShell : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("触发前")]
    public Sprite spriteBefore;
    [Header("触发后")]
    public Sprite spriteAfter;
    [Header("彩蛋识别名字，不一定要用")]
    public string finishName;
    [Header("-----分割线-----")]
    public GameObject groundEvent;//地板事件
    public bool canTrigger;//是否可以触发
    void Start()
    {
        groundEvent = GameObject.Find("GroundEvent");
        groundEvent.GetComponent<GroundEvent>().TriggerEgg += TriggerEgg;
        canTrigger = false;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteBefore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 触发彩蛋事件以后执行的函数
    /// </summary>
    /// <param name="i"></param>彩蛋名字，用来确定是否触发彩蛋
    public void TriggerEgg(string i)
    {
        if(IsFuzzyMatch(finishName, i, 1) == true)
        {
            canTrigger = true;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteAfter;
        }
    }
    /// <summary>
    /// 判断两个字符串是否匹配，变化在指定数量字内，例如点位1和点位2是一个字符变化
    /// </summary>
    /// <param name="str1"></param>
    /// <param name="str2"></param>
    /// <param name="tolerance"></param>指定字符串变化范围的数量
    /// <returns></returns>
    public static bool IsFuzzyMatch(string str1, string str2, int tolerance)
    {
        int distance = ComputeLevenshteinDistance(str1, str2);
        return distance <= tolerance;
    }
    /// <summary>
    /// 字符串匹配的算法实现函数
    /// </summary>
    /// <param name="str1"></param>
    /// <param name="str2"></param>
    /// <returns></returns>
    public static int ComputeLevenshteinDistance(string str1, string str2)
    {
        int len1 = str1.Length;
        int len2 = str2.Length;
        int[,] dp = new int[len1 + 1, len2 + 1];// 创建一个二维数组来存储计算中间结果
        for (int i = 0; i <= len1; i++)// 初始化数组边界
        {
            dp[i, 0] = i;
        }
        for (int j = 0; j <= len2; j++)
        {
            dp[0, j] = j;
        }
        for (int i = 1; i <= len1; i++)// 计算Levenshtein距离
        {
            for (int j = 1; j <= len2; j++)
            {
                int cost = (str1[i - 1] == str2[j - 1]) ? 0 : 1;
                dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), dp[i - 1, j - 1] + cost);
            }
        }
        return dp[len1, len2];
    }
}
