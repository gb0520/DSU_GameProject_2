using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZB
{
    public class InitSceneLoad : MonoBehaviour
    {
        [SerializeField] string scene_Tutorial_1;
        [SerializeField] string scene_Lobby;

        void Start()
        {
            SceneLoad();
        }

        [ContextMenu("SceneLoad")]
        public void SceneLoad()
        {
            Save save = Managers.instance.Save;
            //Ʃ�丮����� �����ϴ°�?
            if(!save.Dic_ReadData("Tutorial_3_stage"))
            {
                Managers.instance.SceneMove.SceneMoveDirectly(scene_Tutorial_1);
            }
            //�ƴϸ� �κ�� �� �̵�
            else
            {
                Managers.instance.SceneMove.SceneMoveDirectly(scene_Lobby);
            }
        }
    }
}