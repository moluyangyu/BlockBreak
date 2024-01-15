using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class UiStatic
{
    private static string[] textNamesStatic;//静态的标识数组

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
                // 写入标题行（如果有的话）
                // writer.WriteLine("ColumnName1,ColumnName2,ColumnName3");

                // 写入数据行
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
            // 读取文件的所有行
            string[] lines = File.ReadAllLines(filePath);

            // 修改指定行的数据
            lines[lineIndex] = newData;

            // 写回文件
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
}
