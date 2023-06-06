using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ZB
{
    public class SceneMoveArea : MonoBehaviour
    {
        [SerializeField] string m_sceneName;
        [SerializeField] float m_areaEnteringTime;

        public void OnEnter()
        {
            Managers.instance.ScreenSwap.Fade(
                Managers.instance.SceneMove.LoadSceneEvent(m_sceneName),
                2, 1, Ease.InQuart, Ease.OutQuart);
        }

        public void OnExit()
        {
            Managers.instance.ScreenSwap.FadeCancel();
        }
    }
}