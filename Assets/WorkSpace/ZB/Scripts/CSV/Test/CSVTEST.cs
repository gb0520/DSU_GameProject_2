using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ZB.CSV.Test
{
    public class CSVTEST : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI tmp_read;
        [SerializeField] DataPool dataPool;

        public void OnBtnClicked_Read()
        {
            Debug.LogError("Btn_Read");

            dataPool.Load();

            string showStr = "";
            showStr += dataPool.FileNames[0] + '\n';

            for (int i = 0; i < dataPool.Files.Count; i++)
            {
                for (int j = 0; j < dataPool.Files[i].Count; j++)
                {
                    foreach (KeyValuePair<string, string> item in dataPool.Files[i][j])
                    {
                        showStr += $"{j}За / Key : {item.Key}   Value : {item.Value}\n";
                    }
                }
            }

            tmp_read.text = showStr;
        }
        public void OnBtnClicked_Write()
        {
            Debug.LogError("Btn_Write");

            List<Dictionary<string, string>> newData = new List<Dictionary<string, string>>();

            newData.Add(new Dictionary<string, string>());
            for (int i = 0; i < 3; i++)
                newData[0].Add(dataPool.RowDatas[i], "a");

            newData.Add(new Dictionary<string, string>());
            for (int i = 0; i < 3; i++)
                newData[1].Add(dataPool.RowDatas[i], "b");

            Parser.WriteSingle(dataPool.Path, newData);
        }
    }
}
