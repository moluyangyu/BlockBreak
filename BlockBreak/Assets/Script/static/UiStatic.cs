using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class UiStatic
{
    private static string[] textNamesStatic;//��̬�ı�ʶ����
    public static bool textLock;//ֻ������������˲��ܵ����һ���Ի�����
    public static string[] TextNamesStatic
    {
        get { return textNamesStatic; }
        set { if (value != null) { textNamesStatic = value; } }
    }
    /// <summary>
    /// �Ѿ�̬�ı�ʶ����д��CSV�ļ�
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
                // д������У�����еĻ���
                // writer.WriteLine("ColumnName1,ColumnName2,ColumnName3");

                // д��������
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
    /// ��CSV�����ó�����̬�ı�ʾ����
    /// </summary>
    /// <param name="filePath"></param>
    public static void LoadFromCSV(string filePath)
    {
        try
        {
            // ��ȡ�ļ���������
            string[] lines = File.ReadAllLines(filePath);

            // ����һ���ַ����������洢CSV�ļ�������
            List<string> data = new List<string>();

            // ����ÿһ�в���ӵ�������
            for (int i = 0; i < lines.Length; i++)
            {
                data.Add(lines[i]);
            }

            // �� List תΪ����
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
    /// �Ի�����д���ʶ��Ҫ�õ��ĺ���������Ҳ���������ɱ��
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="lineIndex"></param>д��ָ���ĵڼ���
    /// <param name="newData"></param>д�������
    public static void UpdateCSVAtLine(string filePath, int lineIndex, string newData)
    {
        try
        {
            // ��ȡ�ļ���������
            string[] lines = File.ReadAllLines(filePath);

            // �޸�ָ���е�����
            lines[lineIndex] = newData;

            // д���ļ�
            File.WriteAllLines(filePath, lines, Encoding.UTF8);

            //Debug.Log("CSV file updated successfully at line " + lineIndex + ": " + filePath);
        }
        catch (System.Exception ex)
        {
            // Debug.LogError("Error updating CSV file: " + ex.Message);
        }
    }
    /// <summary>
    /// ����ID�������ر�ʶ��
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static int LoadId(string filePath)
    {
        try
        {
            // ��ȡ�ļ���������
            string[] lines = File.ReadAllLines(filePath);
            // ����һ���ַ����������洢CSV�ļ�������
            List<string> data = new List<string>();

            // ����ÿһ�в���ӵ�������
            for (int i = 0; i < lines.Length; i++)
            {
                data.Add(lines[i]);
            }

            // �� List תΪ����
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
    //����UI���¼�
    public delegate void UiOpenHandler(int a);//��������״̬����������0������Ϊtrue��1Ϊfalse��2����ֱ�ӷ�ת(�µ�ʹ�ÿ���ʱ����ʵ��)
    public static event UiOpenHandler UiOpen;
    /// <summary>
    /// false����ͣ������true������
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
    //����ͼ�����¼�
    public delegate void GameDexTriggerHandler(int i);
    public static event GameDexTriggerHandler GameDexTrigger;
    /// <summary>
    /// ����ͼ��
    /// </summary>
    /// <param name="i"></param>���Ϊ�����˵ڼ�����ǣ��ǵ�һ��Ϸ������ʹ�õ�
    public static void GameDexTriggerIssue(int i)
    {
        GameDexTrigger?.Invoke(i);
        Time.timeScale = 0;
    }
    //������ƽ��Ի����¼�
    public delegate int TalkKickHandler(int id);
    public static event TalkKickHandler TalkKick;
    /// <summary>
    /// ������Ժ���ƽ�������ͬ��ʶ���ĶԻ������½���һ��
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
                    TalkResult += handler(j);//����ʶ��ת��Ϊint��idȻ����ȥ�Ա�
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
    /// �������ͨ�ص��ź�
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
