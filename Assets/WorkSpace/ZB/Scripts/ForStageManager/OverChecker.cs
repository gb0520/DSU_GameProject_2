using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ZB.Architecture;

namespace ZB
{
    public class OverChecker : MonoBehaviour, IOverCheck, ITimer
    {
        [Header("시간체크 시작 바닥충돌 전"), SerializeField]
        private UnityEvent m_OnTimeCountStart_Focus;         //시간체크 시작
        [Header("시간체크 시작 바닥충돌 후"), SerializeField]
        private UnityEvent m_OnTimeCountStart_Col;         //시간체크 시작
        [Header("일시정지 입장"), SerializeField]
        private UnityEvent m_OnTimeCountPauseTrue;     //일시정지 Enter
        [Header("일시정지 퇴장"), SerializeField]
        private UnityEvent m_OnTimeCountPauseFalse;    //일시정지 Exit
        [Header("시간체크 끝"), SerializeField]
        private UnityEvent m_OnTimeCountStop;          //시간체크 끝 (게임오버)
        [SerializeField] private UnityEvent m_OnTimeCountUpdate;        //시간체크 Stay

        [Header("수정요소")]
        [SerializeField] private float m_timeLimit;

        [Header("확인요소")]
        [SerializeField] private float m_nowTime;
        [SerializeField] private bool m_pause;
        [SerializeField] private bool m_over;
        [SerializeField] private bool m_timeCounting;

        public void OnEnterStage()
        {
            m_OnTimeCountStart_Focus.Invoke();
        }
        public void OnExitStage()
        {
            TimeCountPauseActive(true);
        }
        public bool OverCheck()
        {
            if (m_over)
            {
                m_over = false;
                return true;
            }
            return false;
        }

        public int nowTime()
        {
            return (int)m_nowTime;
        }
        public int timeLimit()
        {
            return (int)m_timeLimit;
        }
        public bool isPaused()
        {
            return m_pause;
        }

        [ContextMenu("TimeCountStart")]
        public void TimeCountStart()
        {
            //if (!m_timeCounting)
            {
                m_OnTimeCountStart_Col.Invoke();

                if (timeCountUpdate_C != null)
                    StopCoroutine(timeCountUpdate_C);
                timeCountUpdate_C = timeCountUpdate();
                StartCoroutine(timeCountUpdate_C);
            }
        }
        [ContextMenu("PauseTrue")]
        public void TempFunc_PauseTrue()
        {
            TimeCountPauseActive(true);
        }
        [ContextMenu("PauseFalse")]
        public void TempFunc_PauseFalse()
        {
            TimeCountPauseActive(false);
        }
        public void TimeCountPauseActive(bool active)
        {
            if (m_timeCounting)
            {
                m_pause = active;

                if (active) m_OnTimeCountPauseTrue.Invoke();
                else m_OnTimeCountPauseFalse.Invoke();
            }
        }

        IEnumerator timeCountUpdate_C;
        IEnumerator timeCountUpdate()
        {
            m_timeCounting = true;
            m_nowTime = m_timeLimit;
            m_pause = false;

            while (m_nowTime > 0) 
            {
                if (!m_pause)
                {
                    m_OnTimeCountUpdate.Invoke();
                    m_nowTime -= Time.deltaTime;
                }
                yield return null;
            }

            m_timeCounting = false;
            m_nowTime = 0;
            m_over = true;

            m_OnTimeCountStop.Invoke();
        }
    }
}