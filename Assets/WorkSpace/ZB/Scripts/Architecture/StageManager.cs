using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZB.Architecture;

namespace ZB.Architecture
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] Transform clearCheck_Target;
        [SerializeField] Transform overCheck_Target;
        [SerializeField] Transform[] handlers_Target;

        IClearCheck clearCheck;
        IOverCheck overCheck;
        IHandlers[] handlers;

        public void OnEnterStage()
        {
            clearCheck.OnEnterStage();
            overCheck.OnEnterStage();
            for (int i = 0; i < handlers.Length; i++)
                handlers[i].OnEnterStage();
        }

        public void OnExitStage()
        {
            clearCheck.OnExitStage();
            overCheck.OnExitStage();
            for (int i = 0; i < handlers.Length; i++)
                handlers[i].OnExitStage();
        }

        private void Awake()
        {
            clearCheck_Target.TryGetComponent(out clearCheck);
            overCheck_Target.TryGetComponent(out overCheck);
            handlers = new IHandlers[handlers_Target.Length];
            for (int i = 0; i < handlers.Length; i++)
            {
                handlers_Target[i].TryGetComponent(out handlers[i]);
            }
        }
    }
}