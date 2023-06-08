using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace ZB.CSV
{
    public class Parser
    {
        public enum AccessStyle
        {
            Resources,
            PersistentDataPath
        }

        public static List<Dictionary<string, string>> ReadSingle(AccessStyle accessStyle, string path, out string fileName)
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();

            string csvData = "";
            fileName = "";

            switch (accessStyle)
            {
                case AccessStyle.Resources:
                    TextAsset sourceFile = Resources.Load<TextAsset>(path);

                    if (sourceFile == null)
                    {
                        Debug.LogError($"Parser.ReadSingle / ReadByResources / Failed / Path : {path}");
                        return null;
                    }

                    StringReader sr = new StringReader(sourceFile.text);
                    fileName = sourceFile.name;
                    csvData = sr.ReadToEnd();
                    Debug.LogError($"Parser.ReadSingle / ReadByResources / Success");
                    break;

                case AccessStyle.PersistentDataPath:
                    string fullPath = Path.Combine(Application.persistentDataPath, path);
                    fileName = Path.GetFileName(fullPath);
                    csvData = File.ReadAllText(fullPath);
                    Debug.LogError($"Parser.ReadSingle / ReadByPersistentDataPath / FullPath : {fullPath}");
                    break;
            }

            if (csvData != "")
            {
                string temp = "";
                int index = 0;

                //�� �и���ų �ؽ�Ʈ ������
                while (csvData[index + 1] != '\n')
                {
                    temp += csvData[index++];
                }
                string[] textKeys = temp.Split(",");
                index += 2;

                //����Ʈ �߰� ����
                bool endLoop = false;
                while (!endLoop)
                {
                    result.Add(new Dictionary<string, string>());
                    for (int i = 0; i < textKeys.Length; i++)
                    {
                        temp = "";

                        //�ٹٲ� �ִ� �ؽ�Ʈ�� ���
                        if (csvData[index] == '\"')
                        {
                            index++;
                            while (csvData[index] != '\"')
                            {
                                temp += csvData[index++];
                            }

                            if (i + 1 >= textKeys.Length)
                                index += 3;
                            else
                                index += 2;
                        }

                        //�ٹٲ� ���� �ؽ�Ʈ�� ���
                        else
                        {
                            while (csvData[index] != ',' && csvData[index] != '\n')
                            {
                                temp += csvData[index++];
                            }
                            index++;
                        }
                        result[result.Count - 1].Add(textKeys[i], temp);

                        if (index + 1 >= csvData.Length)
                        {
                            endLoop = true;
                            break;
                        }
                    }
                }

                return result;
            }
            Debug.LogError("Path�� �ش��ϴ� ���Ͼ��� : " + path);
            return null;
        }
        public static List<List<Dictionary<string, string>>> ReadMultipleByResources(string path, out string[] fileNames)
        {
            List<List<Dictionary<string, string>>> result = new List<List<Dictionary<string, string>>>();
            List<Dictionary<string, string>> tempList;

            TextAsset[] sourceFiles = Resources.LoadAll<TextAsset>(path);
            StringReader sr;

            fileNames = new string[sourceFiles.Length];
            for (int i = 0; i < sourceFiles.Length; i++)
            {
                sr = new StringReader(sourceFiles[i].text);
                fileNames[i] = sourceFiles[i].name;

                tempList = new List<Dictionary<string, string>>();

                if (sr != null)
                {
                    string temp = "";
                    int index = 0;
                    string text = sr.ReadToEnd();

                    //�� �и���ų �ؽ�Ʈ ������
                    while (text[index + 1] != '\n')
                    {
                        temp += text[index++];
                    }
                    string[] textKeys = temp.Split(",");
                    index += 2;

                    //����Ʈ �߰� ����
                    bool endLoop = false;
                    while (!endLoop)
                    {
                        tempList.Add(new Dictionary<string, string>());
                        for (int j = 0; j < textKeys.Length; j++)
                        {
                            temp = "";

                            //�ٹٲ� �ִ� �ؽ�Ʈ�� ���
                            if (text[index] == '\"')
                            {
                                index++;
                                while (text[index] != '\"')
                                {
                                    temp += text[index++];
                                }

                                if (j + 1 >= textKeys.Length)
                                    index += 3;
                                else
                                    index += 2;
                            }

                            //�ٹٲ� ���� �ؽ�Ʈ�� ���
                            else
                            {
                                while (text[index] != ',' && text[index] != '\n')
                                {
                                    temp += text[index++];
                                }
                                index++;
                            }
                            tempList[tempList.Count - 1].Add(textKeys[j], temp);

                            if (index + 1 >= text.Length)
                            {
                                endLoop = true;
                                break;
                            }
                        }
                    }
                }

                result.Add(tempList);
            }

            return result;
        }

        public static void WriteSingle(AccessStyle accesStyle, string path, List<Dictionary<string,string>> data, bool canOverwrite = false)
        {
            string csvData = "";

            //Keys
            foreach (KeyValuePair<string, string> entry in data[0])
            {
                csvData += entry.Key + ",";
            }
                csvData = csvData.Remove(csvData.Length - 1);
            csvData += "\r\n"; // CRLF�� ����

            //Values
            foreach (Dictionary<string, string> row in data)
            {
                foreach (KeyValuePair<string, string> entry in row)
                {
                    csvData += entry.Value + ",";
                }
                csvData = csvData.Remove(csvData.Length - 1);
                csvData += "\r\n"; // CRLF�� ����
            }

            //����Է�
            string fullPath="";

            switch(accesStyle)
            {
                case AccessStyle.PersistentDataPath:
                fullPath = Path.Combine(Application.persistentDataPath, path);
                    break;
            }

            Debug.LogError($"Parser.WriteSingle / FullPath : {fullPath}");

            if (!File.Exists(fullPath) ||                   //���λ�������
                (File.Exists(fullPath) && canOverwrite))    //���������
            {
                File.WriteAllText(fullPath, csvData);
            }
        }        
    }
}