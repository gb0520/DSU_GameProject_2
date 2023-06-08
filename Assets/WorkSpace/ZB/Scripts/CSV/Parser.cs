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

                //열 분리시킬 텍스트 떼내기
                while (csvData[index + 1] != '\n')
                {
                    temp += csvData[index++];
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

                        //줄바꿈 없는 텍스트일 경우
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
            Debug.LogError("Path에 해당하는 파일없음 : " + path);
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

        public static void WriteSingle(AccessStyle accesStyle, string path, List<Dictionary<string,string>> data, bool canOverwrite = false)
        {
            string csvData = "";

            //Keys
            foreach (KeyValuePair<string, string> entry in data[0])
            {
                csvData += entry.Key + ",";
            }
                csvData = csvData.Remove(csvData.Length - 1);
            csvData += "\r\n"; // CRLF로 변경

            //Values
            foreach (Dictionary<string, string> row in data)
            {
                foreach (KeyValuePair<string, string> entry in row)
                {
                    csvData += entry.Value + ",";
                }
                csvData = csvData.Remove(csvData.Length - 1);
                csvData += "\r\n"; // CRLF로 변경
            }

            //경로입력
            string fullPath="";

            switch(accesStyle)
            {
                case AccessStyle.PersistentDataPath:
                fullPath = Path.Combine(Application.persistentDataPath, path);
                    break;
            }

            Debug.LogError($"Parser.WriteSingle / FullPath : {fullPath}");

            if (!File.Exists(fullPath) ||                   //새로생성조건
                (File.Exists(fullPath) && canOverwrite))    //덮어쓰기조건
            {
                File.WriteAllText(fullPath, csvData);
            }
        }        
    }
}