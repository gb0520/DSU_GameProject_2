using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ZB.Architecture;
using DG.Tweening;

namespace ZB
{
    public class TimeCountUIShower : MonoBehaviour
    {
        [SerializeField] private Transform m_ITimerTarget;
        private ITimer m_ITimer;

        [SerializeField] private Image m_img_body;
        [SerializeField] private Image m_rtf_body;
        [SerializeField] private TextMeshProUGUI m_tmp_Minuite;
        [SerializeField] private TextMeshProUGUI m_tmp_Colon;
        [SerializeField] private TextMeshProUGUI m_tmp_Second;

        void Awake()
        {
            if (m_ITimerTarget.TryGetComponent(out m_ITimer))
            {
                Debug.Log($"ZB / TimeCountUIShower / m_ITimer Connect Success");
            }
        }

        public void OnTimeCountStart()
        {
            m_rtf_body.transform.DOShakeScale(0.5f, 1, 8);
            m_tmp_Minuite.color = Color.white;
            m_tmp_Colon.color = Color.white;
            m_tmp_Second.color = Color.white;
            m_tmp_Minuite.DOColor(Color.black, 0.7f).SetEase(Ease.OutQuart);
            m_tmp_Colon.DOColor(Color.black, 0.7f).SetEase(Ease.OutQuart);
            m_tmp_Second.DOColor(Color.black, 0.7f).SetEase(Ease.OutQuart);
        }
        public void OnTimeCountPause(bool active)
        {
            if (m_ITimer.isPaused())
            {
                //일시정지 비활성화
                m_rtf_body.transform.DOShakeScale(0.5f, 1, 8);
                m_img_body.DOColor(Color.gray, 1).SetEase(Ease.OutQuart);
            }
            else
            {
                //일시정지 활성화
                m_rtf_body.transform.DOShakeScale(0.5f, 1, 8);
                m_img_body.DOColor(Color.white, 0.5f).SetEase(Ease.OutQuart);
            }
        }
        public void OnTimeCountStop()
        {
            m_rtf_body.transform.DOShakeScale(0.8f, 1.5f, 15);
            m_img_body.color = Color.red;

        }
        public void OnTimeCountUpdate()
        {
            m_tmp_Minuite.text = SecondCount_To_MinuiteInteger(m_ITimer.nowTime()).ToString("00");
            m_tmp_Second.text = SecondCount_To_SecondInteger(m_ITimer.nowTime()).ToString("00");
        }

        int SecondCount_To_MinuiteInteger(float nowTime)
        {
            return (int)nowTime / 60;
        }
        int SecondCount_To_SecondInteger(float nowTime)
        {
            return (int)nowTime % 60;
        }
    }
}