using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZB
{
    public class InitSceneLoad : MonoBehaviour
    {
        [SerializeField] string scene_Tutorial;
        [SerializeField] string scene_Lobby;

        void Start()
        {
            SceneLoad();
        }

        [ContextMenu("SceneLoad")]
        public void SceneLoad()
        {
            bool playTutorial = true;

            Managers.instance.SceneMove.SceneMoveDirectly(
                playTutorial ? scene_Tutorial : scene_Lobby);
        }
    }
}