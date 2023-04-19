using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMoveArea : MonoBehaviour
{
    [SerializeField] string m_sceneName;
    [SerializeField] float m_areaEnteringTime;

    public void OnEnter()
    {
        if (SceneMoveCycle_C != null)
        {
            StopCoroutine(SceneMoveCycle_C);
        }
        SceneMoveCycle_C = SceneMoveCycle();
        StartCoroutine(SceneMoveCycle_C);
    }

    public void OnExit()
    {
        if (SceneMoveCycle_C != null)
        {
            StopCoroutine(SceneMoveCycle_C);
        }
    }

    WaitForSeconds SceneMoveCycle_WFS;
    IEnumerator SceneMoveCycle_C;
    IEnumerator SceneMoveCycle()
    {
        yield return SceneMoveCycle_WFS;
        ZB.Managers.instance.SceneMove.SceneMoveStart(m_sceneName);
    }

    private void Awake()
    {
        SceneMoveCycle_WFS = new WaitForSeconds(m_areaEnteringTime);
    }
}
