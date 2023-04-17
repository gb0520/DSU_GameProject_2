using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using ZB.Architecture;

namespace ZB
{
    public class ResultWindow_Clear : MonoBehaviour
    {
        [SerializeField] Transform m_tf_ITimerTarget;
        ITimer m_Itimer;

        [SerializeField] TextMeshProUGUI m_tmp_LeftTime_Minuite;
        [SerializeField] TextMeshProUGUI m_tmp_LeftTime_Second;

        public void UpdateLeftTime()
        {
            if (m_Itimer != null)
            {
                m_tmp_LeftTime_Minuite.text = TimeCountUIShower.SecondCount_To_MinuiteInteger(m_Itimer.nowTime()).ToString();
                m_tmp_LeftTime_Second.text = TimeCountUIShower.SecondCount_To_SecondInteger(m_Itimer.nowTime()).ToString();
            }
        }

        void Awake()
        {
            if (!m_tf_ITimerTarget.TryGetComponent(out m_Itimer)) Debug.Log("ZB / ResultWindow_Clear / m_ItimerConnectFailed");
        }
    }
}