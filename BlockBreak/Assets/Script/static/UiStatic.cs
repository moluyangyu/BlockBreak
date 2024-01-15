using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class UiStatic
{
    private static string[] textNamesStatic;//��̬�ı�ʶ����

    public static string[] TextNamesStatic
    {
        get { return textNamesStatic; }
        set { textNamesStatic = value;}
    }
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
                    writer.WriteLine(textNamesStatic[i]);
                }
            }

            Debug.Log("CSV file saved successfully: " + filePath);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error saving CSV file: " + ex.Message);
        }
    }
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

            textNamesStatic=dataArray;
        }
        catch (System.Exception ex)
        {
         //   Debug.LogError("Error loading CSV file: " + ex.Message);
            
        }
    }
    public static void UpdateCSVAtLine(string filePath, int lineIndex, string newData)
    {
        try
        {
            // ��ȡ�ļ���������
            string[] lines = File.ReadAllLines(filePath);

            // �޸�ָ���е�����
            lines[lineIndex] = newData;

            // д���ļ�
            File.WriteAllLines(filePath, lines);

            //Debug.Log("CSV file updated successfully at line " + lineIndex + ": " + filePath);
        }
        catch (System.Exception ex)
        {
           // Debug.LogError("Error updating CSV file: " + ex.Message);
        }
    }
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
}
