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

                //열 분리시킬 텍스트 떼내기
                while (text[index + 1] != '\n')
                {
                    temp += text[index++];
                }
                string[] textKeys = temp.Split(",");
                index += 2;

                //리스트 추가 시작
                bool endLoop = false;
                while (!endLoop)
                {
                    result.Add(new Dictionary<string, string>());
                    for (int i = 0; i < textKeys.Length; i++)
                    {
                        temp = "";

                        //줄바꿈 있는 텍스트일 경우
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

                        //줄바꿈 없는 텍스트일 경우
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
            Debug.LogError("Path에 해당하는 파일없음 : " + path);
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

                    //열 분리시킬 텍스트 떼내기
                    while (text[index + 1] != '\n')
                    {
                        temp += text[index++];
                    }
                    string[] textKeys = temp.Split(",");
                    index += 2;

                    //리스트 추가 시작
                    bool endLoop = false;
                    while (!endLoop)
                    {
                        tempList.Add(new Dictionary<string, string>());
                        for (int j = 0; j < textKeys.Length; j++)
                        {
                            temp = "";

                            //줄바꿈 있는 텍스트일 경우
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

                            //줄바꿈 없는 텍스트일 경우
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
            //아스키코드로 읽기
            for (int i = 0; i < csvData.Length; i++)
            {
                Debug.LogError(i + " / " + (int)csvData[i] + " / " + csvData[i]);
            }
            Debug.LogError("---------------------");

            //경로입력
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

            //덮어쓰기
            File.WriteAllText(fullPath, csvData);
        }
    }
}