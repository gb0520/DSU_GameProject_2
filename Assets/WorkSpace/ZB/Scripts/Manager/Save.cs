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

        //�������б�
        public void ReadData()
        {
            //���� ���� �ִ��� Ȯ���Ѵ�.
            if (Parser.Exist(Parser.AccessStyle.PersistentDataPath, path))
            {
                saveData = new SaveData(Parser.ReadSingle(Parser.AccessStyle.PersistentDataPath, path)[0]);
            }

            //���ٸ�, ���λ����ϰ�, �ٽ��д´�.
            else
            {
                saveData = new SaveData();
                Parser.WriteSingle(Parser.AccessStyle.PersistentDataPath, path, saveData.GetDataByList());
                ReadData();
            }
        }

        //���絥���� �����
        public void WriteData()
        {
            Parser.WriteSingle(Parser.AccessStyle.PersistentDataPath, path, saveData.GetDataByList(), true);
        }

        //�����Ϳ� �ִ� ���� �Ϻ� �б�
        public bool Dic_ReadData(string key)
        {
            if (saveData.dicData.ContainsKey(key))
            {
                return saveData.dicData[key] == "true";
            }
            else
            {
                Debug.LogError($"Save / Dic_WriteData / Key : {key} �� ���� ��ųʸ��� ���Ե��� ����");
            }
            return false;
        }

        //�����Ϳ� �ִ� ���� �Ϻ� �����
        public void Dic_WriteData(string key, bool value)
        {
            if (saveData.dicData.ContainsKey(key))
            {
                saveData.dicData[key] = value ? "true" : "false";
            }
            else
            {
                Debug.LogError($"Save / Dic_WriteData / Key : {key} �� ���� ��ųʸ��� ���Ե��� ����");
            }
        }

        //��ųʸ� ���� �б�
        [ContextMenu("CheckData")]
        public void CheckData()
        {
            Debug.LogError("�� CheckDataStart");
            foreach (KeyValuePair<string, string> item in saveData.dicData)
            {
                Debug.LogError($"Key : {item.Key} / Value : {item.Value}");
            }
            Debug.LogError("�� CheckDataEnd");
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