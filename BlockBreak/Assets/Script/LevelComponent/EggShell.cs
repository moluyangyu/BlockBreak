using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggShell : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("����ǰ")]
    public Sprite spriteBefore;
    [Header("������")]
    public Sprite spriteAfter;
    [Header("�ʵ�ʶ�����֣���һ��Ҫ��")]
    public string finishName;
    [Header("-----�ָ���-----")]
    public GameObject groundEvent;//�ذ��¼�
    public bool canTrigger;//�Ƿ���Դ���
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
    /// �����ʵ��¼��Ժ�ִ�еĺ���
    /// </summary>
    /// <param name="i"></param>�ʵ����֣�����ȷ���Ƿ񴥷��ʵ�
    public void TriggerEgg(string i)
    {
        if(IsFuzzyMatch(finishName, i, 1) == true)
        {
            canTrigger = true;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteAfter;
        }
    }
    /// <summary>
    /// �ж������ַ����Ƿ�ƥ�䣬�仯��ָ���������ڣ������λ1�͵�λ2��һ���ַ��仯
    /// </summary>
    /// <param name="str1"></param>
    /// <param name="str2"></param>
    /// <param name="tolerance"></param>ָ���ַ����仯��Χ������
    /// <returns></returns>
    public static bool IsFuzzyMatch(string str1, string str2, int tolerance)
    {
        int distance = ComputeLevenshteinDistance(str1, str2);
        return distance <= tolerance;
    }
    /// <summary>
    /// �ַ���ƥ����㷨ʵ�ֺ���
    /// </summary>
    /// <param name="str1"></param>
    /// <param name="str2"></param>
    /// <returns></returns>
    public static int ComputeLevenshteinDistance(string str1, string str2)
    {
        int len1 = str1.Length;
        int len2 = str2.Length;
        int[,] dp = new int[len1 + 1, len2 + 1];// ����һ����ά�������洢�����м���
        for (int i = 0; i <= len1; i++)// ��ʼ������߽�
        {
            dp[i, 0] = i;
        }
        for (int j = 0; j <= len2; j++)
        {
            dp[0, j] = j;
        }
        for (int i = 1; i <= len1; i++)// ����Levenshtein����
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
