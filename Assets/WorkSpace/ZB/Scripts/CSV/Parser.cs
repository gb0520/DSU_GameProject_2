using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace ZB.CSV
{
    public class Parser
    {
        public static List<Dictionary<string, string>> ReadSingle(string path, out string fileName)
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();

            TextAsset sourceFile = Resources.Load<TextAsset>(path);
            StringReader sr = new StringReader(sourceFile.text);
            fileName = sourceFile.name;

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
                    result.Add(new Dictionary<string, string>());
                    for (int i = 0; i < textKeys.Length; i++)
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

                            if (i + 1 >= textKeys.Length)
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
                        result[result.Count - 1].Add(textKeys[i], temp);

                        if (index + 1 >= text.Length)
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
        public static List<List<Dictionary<string, string>>> ReadMultiple(string path, out string[] fileNames)
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

        public static void WriteSingle(string path, List<Dictionary<string,string>> data)
        {
            string csvData = "";

            //Keys
            foreach (KeyValuePair<string, string> entry in data[0])
            {
                csvData += entry.Key + ",";
            }
                csvData = csvData.Remove(csvData.Length - 1);
            csvData += "\n";

            //Values
            foreach (Dictionary<string, string> row in data)
            {
                foreach (KeyValuePair<string, string> entry in row)
                {
                    csvData += entry.Value + ",";
                }
                csvData = csvData.Remove(csvData.Length - 1);
                csvData += "\n";
            }

            Debug.LogError("---------------------");
            //�ƽ�Ű�ڵ�� �б�
            for (int i = 0; i < csvData.Length; i++)
            {
                Debug.LogError(i + " / " + (int)csvData[i] + " / " + csvData[i]);
            }
            Debug.LogError("---------------------");

            //����Է�
            string fullPath="";

            bool isAndroid=false;

#if UNITY_ANDROID
            fullPath = Path.Combine(Application.streamingAssetsPath, "Resources", path, "Save.csv");
            isAndroid = true;
#endif

#if UNITY_EDITOR
            fullPath = Path.Combine(Application.dataPath, "Resources", path + "/Save.csv");
            isAndroid = false;
#endif

            Debug.LogError(isAndroid);
            Debug.LogError(fullPath);

            //�����
            File.WriteAllText(fullPath, csvData);
        }
    }
}