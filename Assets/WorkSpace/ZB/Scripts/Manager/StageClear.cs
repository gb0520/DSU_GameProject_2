using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZB
{
    public class StageClear : MonoBehaviour
    {
        public void SignalToManager_ClearDataSave()
        {
            Managers.instance.Save.Dic_WriteData(SceneManager.GetActiveScene().name, true);
            Managers.instance.Save.WriteData();
        }
    }
}