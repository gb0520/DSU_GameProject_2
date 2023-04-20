using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ZB.Architecture;

namespace ZB
{
    public class OverChecker : MonoBehaviour, IOverCheck, ITimer
    {
        [Header("�ð�üũ ���� �ٴ��浹 ��"), SerializeField]
        private UnityEvent m_OnTimeCountStart_Focus;         //�ð�üũ ����
        [Header("�ð�üũ ���� �ٴ��浹 ��"), SerializeField]
        private UnityEvent m_OnTimeCountStart_Col;         //�ð�üũ ����
        [Header("�Ͻ����� ����"), SerializeField]
        private UnityEvent m_OnTimeCountPauseTrue;     //�Ͻ����� Enter
        [Header("�Ͻ����� ����"), SerializeField]
        private UnityEvent m_OnTimeCountPauseFalse;    //�Ͻ����� Exit
        [Header("�ð�üũ ��"), SerializeField]
        private UnityEvent m_OnTimeCountStop;          //�ð�üũ �� (���ӿ���)
        [SerializeField] private UnityEvent m_OnTimeCountUpdate;        //�ð�üũ Stay

        [Header("�������")]
        [SerializeField] private float m_timeLimit;

        [Header("Ȯ�ο��")]
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