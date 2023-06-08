using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZB.CSV
{
    public class DataPool : MonoBehaviour
    {
        public enum LoadStyle { single, multiple }

        public string Path { get => path; }
        public string[] FileNames { get => fileNames; }
        public string[] RowDatas { get => rowDatas; }
        public List<List<Dictionary<string,string>>> Files;

        [SerializeField] LoadStyle loadStyle;
        [SerializeField] string path;
        [SerializeField] string[] fileNames;
        [SerializeField] string[] rowDatas;

        [ContextMenu("Load")]
        public void Load()
        {
            switch(loadStyle)
            {
                case LoadStyle.single:
                    fileNames = new string[1];
                    Files = new List<List<Dictionary<string, string>>>();
                    Files.Add(Parser.ReadSingle(path, out fileNames[0]));
                    break;

                case LoadStyle.multiple:
                    Files = Parser.ReadMultiple(path, out fileNames);
                    break;
            }

            List<string> list = new List<string>();
            foreach (KeyValuePair<string, string> item in Files[0][0])
            {
                list.Add(item.Key);
            }
            rowDatas = new string[list.Count];
            for (int i = 0; i < rowDatas.Length; i++)
            {
                rowDatas[i] = list[i];
            }
        }

        [ContextMenu("CheckData")]
        public void CheckData()
        {
            for (int i = 0; i < Files.Count; i++)
            {
                for (int j = 0; j < Files[i].Count; j++)
                {
                    foreach (KeyValuePair<string, string> item in Files[i][j])
                    {
                        Debug.LogError($"���� : {fileNames[i]} / {j}�� / �� Key : {item.Key}   Value : {item.Value} ��");
                    }
                }
            }
        }
    }
}