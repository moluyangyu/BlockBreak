using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class UiStatic
{
    private static string[] textNamesStatic;//静态的标识数组
    public static bool textLock;//只有文字输出完了才能点击下一个对话的锁
    public static string[] TextNamesStatic
    {
        get { return textNamesStatic; }
        set { if (value != null) { textNamesStatic = value; } }
    }
    /// <summary>
    /// 把静态的标识数组写入CSV文件
    /// </summary>
    /// <param name="filePath"></param>
    public static void SaveToCSV(string filePath)
    {
        if (textNamesStatic == null || textNamesStatic.Length == 0)
        {
            Debug.LogWarning("Array is empty. Nothing to save.");
            return;
        }
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // 写入标题行（如果有的话）
                // writer.WriteLine("ColumnName1,ColumnName2,ColumnName3");

                // 写入数据行
                for (int i = 0; i < textNamesStatic.Length; i++)
                {
                    writer.WriteLine(textNamesStatic[i], Encoding.UTF8);
                }
            }

            Debug.Log("CSV file saved successfully: " + filePath);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error saving CSV file: " + ex.Message);
        }
    }
    /// <summary>
    /// 从CSV里面拿出来静态的表示数组
    /// </summary>
    /// <param name="filePath"></param>
    public static void LoadFromCSV(string filePath)
    {
        try
        {
            // 读取文件的所有行
            string[] lines = File.ReadAllLines(filePath);

            // 创建一个字符串数组来存储CSV文件的内容
            List<string> data = new List<string>();

            // 遍历每一行并添加到数组中
            for (int i = 0; i < lines.Length; i++)
            {
                data.Add(lines[i]);
            }

            // 将 List 转为数组
            string[] dataArray = data.ToArray();

            // Debug.Log("CSV file loaded successfully: " + filePath);

            textNamesStatic = dataArray;
        }
        catch (System.Exception ex)
        {
            //   Debug.LogError("Error loading CSV file: " + ex.Message);

        }
    }
    /// <summary>
    /// 对话气泡写入标识符要用到的函数，后面也可以拿来干别的
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="lineIndex"></param>写入指定的第几行
    /// <param name="newData"></param>写入的内容
    public static void UpdateCSVAtLine(string filePath, int lineIndex, string newData)
    {
        try
        {
            // 读取文件的所有行
            string[] lines = File.ReadAllLines(filePath);

            // 修改指定行的数据
            lines[lineIndex] = newData;

            // 写回文件
            File.WriteAllLines(filePath, lines, Encoding.UTF8);

            //Debug.Log("CSV file updated successfully at line " + lineIndex + ": " + filePath);
        }
        catch (System.Exception ex)
        {
            // Debug.LogError("Error updating CSV file: " + ex.Message);
        }
    }
    /// <summary>
    /// 加载ID用来加载标识符
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static int LoadId(string filePath)
    {
        try
        {
            // 读取文件的所有行
            string[] lines = File.ReadAllLines(filePath);
            // 创建一个字符串数组来存储CSV文件的内容
            List<string> data = new List<string>();

            // 遍历每一行并添加到数组中
            for (int i = 0; i < lines.Length; i++)
            {
                data.Add(lines[i]);
            }

            // 将 List 转为数组
            string[] dataArray = data.ToArray();
            int result;
            if (int.TryParse(dataArray[0], out result))
            {
                return result;
            }
            return 0;
        }
        catch (System.Exception ex)
        {
            //   Debug.LogError("Error loading CSV file: " + ex.Message);
            return 0;
        }
    }
    //激活UI的事件
    public delegate void UiOpenHandler(int a);//用于主角状态机器，传入0就设置为true，1为false，2就是直接反转(新的使用控制时间来实现)
    public static event UiOpenHandler UiOpen;
    /// <summary>
    /// false就是停下来，true就是走
    /// </summary>
    /// <param name="a"></param>
    public static void UiOpenIssue(bool a)
    {
        if (a)
        {
            //UiOpen?.Invoke(1);
            Time.timeScale = 1;
        }
        else
        {
            //UiOpen?.Invoke(0);
            Time.timeScale = 0;
        }

    }
    public static void UiOpenIssue()
    {
       // UiOpen?.Invoke(2);
    }
    //激活图鉴的事件
    public delegate void GameDexTriggerHandler(int i);
    public static event GameDexTriggerHandler GameDexTrigger;
    /// <summary>
    /// 激活图鉴
    /// </summary>
    /// <param name="i"></param>这个为激活了第几个标记，非单一游戏场景内使用的
    public static void GameDexTriggerIssue(int i)
    {
        GameDexTrigger?.Invoke(i);
        Time.timeScale = 0;
    }
    //鼠标点击推进对话的事件
    public delegate int TalkKickHandler(int id);
    public static event TalkKickHandler TalkKick;
    /// <summary>
    /// 鼠标点击以后就推进所有相同标识名的对话框往下进行一步
    /// </summary>
    /// <param name="idName"></param>
    public static bool TalkKickIssue(string idName)
    {
        if (textLock==true) return false;
        int TalkResult = 0;
        foreach (TalkKickHandler handler in TalkKick.GetInvocationList())
        {

            for (int j = 0; j < textNamesStatic.Length; j++)
            {
                if (textNamesStatic[j].Equals(idName))
                {
                    TalkResult += handler(j);//将标识名转化为int的id然后送去对比
                }
            }

        }
        if (TalkResult == 0)
        {
            return true;
        }
        return false;
    }
    public delegate void NextLevelHandler();
    public static event NextLevelHandler NextLevel;
    /// <summary>
    /// 发出这关通关的信号
    /// </summary>
    public static void NextLevelIssue()
    {
        NextLevel?.Invoke();
    }
    public delegate void PlayerDieHandler(bool a);
    public static event PlayerDieHandler PlayerDie;
    public static void PlayerDieIssue(bool a)
    {
        PlayerDie?.Invoke(a);
    }
}
