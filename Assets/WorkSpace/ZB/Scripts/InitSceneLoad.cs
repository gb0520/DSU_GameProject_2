using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZB
{
    public class InitSceneLoad : MonoBehaviour
    {
        [SerializeField] string scene_Tutorial_1;
        [SerializeField] string scene_Tutorial_2;
        [SerializeField] string scene_Tutorial_3;
        [SerializeField] string scene_Lobby;

        void Start()
        {
            SceneLoad();
        }

        [ContextMenu("SceneLoad")]
        public void SceneLoad()
        {
            Save save = Managers.instance.Save;
            //튜토리얼부터 시작하는가?
            if(save.Dic_ReadData("StartAtTutorial"))
            {
                string sceneName = scene_Lobby;
                if (!save.Dic_ReadData("Tutorial_1")) sceneName = scene_Tutorial_1;
                else if (!save.Dic_ReadData("Tutorial_2")) sceneName = scene_Tutorial_2;
                else if (!save.Dic_ReadData("Tutorial_3")) sceneName = scene_Tutorial_3;
                else
                {
                    //모두 클리어 한 상태라면 해당검사 다음부터 안하도록 덮어쓰기
                    save.Dic_WriteData("StartAtTutorial", true);
                    save.WriteData();
                }

                Managers.instance.SceneMove.SceneMoveDirectly(sceneName);
            }
            //아니면 로비로 씬 이동
            else
            {
                Managers.instance.SceneMove.SceneMoveDirectly(scene_Lobby);
            }
        }
    }
}