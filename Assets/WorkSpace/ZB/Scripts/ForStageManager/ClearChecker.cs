using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ZB.Architecture;

namespace ZB
{
    public class ClearChecker : MonoBehaviour, IOverCheck, ITimer
    {
        [SerializeField] private UnityEvent m_OnTimeCountStart;
        [SerializeField] private UnityEvent m_OnTimeCountPauseTrue;
        [SerializeField] private UnityEvent m_OnTimeCountPauseFalse;
        [SerializeField] private UnityEvent m_OnTimeCountStop;
        [SerializeField] private UnityEvent m_OnTimeCountUpdate;

        [Header("수정요소")]
        [SerializeField] private float m_timeLimit;

        [Header("확인요소")]
        [SerializeField] private float m_nowTime;
        [SerializeField] private bool m_pause;
        [SerializeField] private bool m_over;
        [SerializeField] private bool m_timeCounting;

        public void OnEnterStage()
        {

        }
        public void OnExitStage()
        {

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
            if (!m_timeCounting)
            {
                m_OnTimeCountStart.Invoke();

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
            m_pause = active;

            if (active)     m_OnTimeCountPauseTrue.Invoke();
            else            m_OnTimeCountPauseFalse.Invoke();
        }
        [ContextMenu("TimeCountStop")]
        public void TimeCountStop()
        {
            if (m_timeCounting)
            {
                if (timeCountUpdate_C != null)
                {
                    StopCoroutine(timeCountUpdate_C);
                }
                m_OnTimeCountStop.Invoke();

                m_nowTime = m_timeLimit;
                m_pause = false;
                m_over = false;
                m_timeCounting = false;
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

            m_OnTimeCountStop.Invoke();

            m_timeCounting = false;
            m_nowTime = 0;
            m_over = true;
        }
    }
}