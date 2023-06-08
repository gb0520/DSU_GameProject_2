using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZB;

public class SceneBGMListener : MonoBehaviour
{
    private void Start()
    {
        BgmChange();
    }

    private void BgmChange()
    {
        if(Managers.instance.SoundMaster.audioDictionary.ContainsKey(SceneManager.GetActiveScene().name))
        {
            Managers.instance.SoundMaster.Play(Managers.instance.SoundMaster.audioDictionary[SceneManager.GetActiveScene().name], true);
        }
    }
}
