using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace ZB
{
    public class SceneMove : MonoBehaviour
    {
        [SerializeField] string currentInputSceneName;

        public void SceneMoveStart(string sceneName)
        {
            Managers.instance.ScreenSwap.Fade(LoadSceneEvent(sceneName), 1, 1, Ease.InQuad, Ease.OutQuart);
        }

        public UnityEvent LoadSceneEvent(string sceneName)
        {
            currentInputSceneName = sceneName;

            UnityEvent unityEvent = new UnityEvent();
            unityEvent.AddListener(()=>SceneManager.LoadScene(sceneName));
            return unityEvent;
        }

        public UnityAction LoadSceneAction(string sceneName)
        {
            currentInputSceneName = sceneName;

            return new UnityAction(()=>SceneManager.LoadScene(sceneName));
        }
    }
}