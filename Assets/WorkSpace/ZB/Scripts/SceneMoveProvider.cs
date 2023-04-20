using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMoveProvider : MonoBehaviour
{
    public void SceneMoveStart(string sceneName)
    {
        ZB.Managers.instance.SceneMove.SceneMoveStart(sceneName);
    }
}
