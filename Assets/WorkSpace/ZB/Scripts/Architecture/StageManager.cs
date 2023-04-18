using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ZB.Architecture;

namespace ZB.Architecture
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] UnityEvent OnClear;
        [SerializeField] UnityEvent OnOver;
        [SerializeField] Transform clearCheck_Target;
        [SerializeField] Transform overCheck_Target;
        [SerializeField] Transform[] handlers_Target;

        IClearCheck clearCheck;
        IOverCheck overCheck;
        List<IHandlers> handlers;

        [SerializeField] bool stageDoing;

        //씬 처음에 로드할 때, ResultWindow에서 재실행 클릭할 경우 실행됩니다.
        [ContextMenu("OnEnter")]
        public void OnEnterStage()
        {
            stageDoing = true;
            clearCheck.OnEnterStage();
            overCheck.OnEnterStage();
            for (int i = 0; i < handlers.Count; i++)
                handlers[i].OnEnterStage();
        }

        //ClearCheck 에서 완성도가 기준치가 됐을때, 호출합니다.
        //OverCheck 에서 타임카운트가 끝났을때, 호출합니다.
        [ContextMenu("OnExit")]
        public void OnExitStage()
        {
            stageDoing = false;
            clearCheck.OnExitStage();
            overCheck.OnExitStage();
            for (int i = 0; i < handlers.Count; i++)
                handlers[i].OnExitStage();
        }

        private void Awake()
        {
            if (clearCheck_Target.TryGetComponent(out clearCheck))
                Debug.Log("ClearCheck Connected");
            if (overCheck_Target.TryGetComponent(out overCheck))
                Debug.Log("OverCheck Connected");
            handlers = new List<IHandlers>();
            IHandlers extractedHandler;
            for (int i = 0; i < handlers_Target.Length; i++)
            {
                if (handlers_Target[i].TryGetComponent(out extractedHandler))
                {
                    Debug.Log("Handler Connected / name : " + handlers_Target[i].gameObject.name);
                    handlers.Add(extractedHandler);
                }
            }
        }

        private void Start()
        {
            OnEnterStage();
        }

        private void Update()
        {
            if (stageDoing)
            {
                //스테이지 클리어
                if (clearCheck.ClearCheck())
                {
                    OnExitStage();
                    OnClear.Invoke();
                }

                //스테이지 오버
                if (overCheck.OverCheck())
                {
                    OnExitStage();
                    OnOver.Invoke();
                }
            }
        }
    }
}