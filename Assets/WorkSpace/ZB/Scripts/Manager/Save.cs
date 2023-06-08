using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZB.CSV;

namespace ZB
{
    public class Save : MonoBehaviour
    {
        [SerializeField] string path;

        private SaveData saveData;

        //데이터읽기
        public void ReadData()
        {
            //읽을 파일 있는지 확인한다.
            if (Parser.Exist(Parser.AccessStyle.PersistentDataPath, path))
            {
                saveData = new SaveData(Parser.ReadSingle(Parser.AccessStyle.PersistentDataPath, path)[0]);
            }

            //없다면, 새로생성하고, 다시읽는다.
            else
            {
                saveData = new SaveData();
                Parser.WriteSingle(Parser.AccessStyle.PersistentDataPath, path, saveData.GetDataByList());
                ReadData();
            }
        }

        //현재데이터 덮어쓰기
        public void WriteData()
        {
            Parser.WriteSingle(Parser.AccessStyle.PersistentDataPath, path, saveData.GetDataByList(), true);
        }

        //데이터에 있는 정보 일부 읽기
        public bool Dic_ReadData(string key)
        {
            if (saveData.dicData.ContainsKey(key))
            {
                return saveData.dicData[key] == "true";
            }
            else
            {
                Debug.LogError($"Save / Dic_WriteData / Key : {key} 는 저장 딕셔너리에 포함되지 않음");
            }
            return false;
        }

        //데이터에 있는 정보 일부 덮어쓰기
        public void Dic_WriteData(string key, bool value)
        {
            if (saveData.dicData.ContainsKey(key))
            {
                saveData.dicData[key] = value ? "true" : "false";
            }
            else
            {
                Debug.LogError($"Save / Dic_WriteData / Key : {key} 는 저장 딕셔너리에 포함되지 않음");
            }
        }

        //딕셔너리 정보 읽기
        [ContextMenu("CheckData")]
        public void CheckData()
        {
            Debug.LogError("★ CheckDataStart");
            foreach (KeyValuePair<string, string> item in saveData.dicData)
            {
                Debug.LogError($"Key : {item.Key} / Value : {item.Value}");
            }
            Debug.LogError("★ CheckDataEnd");
        }

        private void Awake()
        {
            ReadData();
        }

        [System.Serializable]
        public class SaveData
        {
            public Dictionary<string, string> dicData;

            public List<Dictionary<string,string>> GetDataByList()
            {
                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                list.Add(dicData);
                return list;
            }

            public SaveData()
            {
                dicData = new Dictionary<string, string>();
                dicData.Add("StartAtTutorial", "false");
                dicData.Add("Tutorial_1_stage", "false");
                dicData.Add("Tutorial_2_stage", "false");
                dicData.Add("Tutorial_3_stage", "false");
                dicData.Add("1_Stage", "false");
            }
            public SaveData(Dictionary<string, string> dicData)
            {
                Debug.LogError("Inited!");
                this.dicData = new Dictionary<string, string>();
                foreach (KeyValuePair<string, string> item in dicData)
                {
                    this.dicData.Add(item.Key, item.Value);
                    Debug.LogError($"{item.Key} / {item.Value}");
                }
            }
        }
    }
}