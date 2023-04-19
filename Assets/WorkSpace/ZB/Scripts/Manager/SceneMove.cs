using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZB
{
    public class SceneMove : MonoBehaviour
    {
        [SerializeField] string currentInputSceneName;

        public void SceneMoveStart(string sceneName)
        {
            currentInputSceneName = sceneName;
            Managers.instance.ScreenSwap.Fade(LoadScene);
        }

        void LoadScene()
        {
            SceneManager.LoadScene(currentInputSceneName);
        }

        [ContextMenu("TestLoadScene")]
        public void Test()
        {
            SceneMoveStart("StageTest_Recent");
        }
    }
}