using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZB;
using ZB.Architecture;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

namespace ZB
{
    public class ResultWindow : MonoBehaviour
    {
        [Header("�����, ����ȯ")]
        [SerializeField] UnityEvent m_uEvent_Restart;
        [SerializeField] UnityEvent m_uEvent_GoSceneMain;
        [SerializeField] UnityEvent m_uEvent_GoSceneNextStage;

        [Space, Header("Ŭ����, ���� ������ ����")]
        [SerializeField] ResultWindow_Clear m_resultWindow_Clear;
        [SerializeField] ResultWindow_Over m_resultWindow_Over;

        [SerializeField] RectTransform m_rtf_window_Clear;
        [SerializeField] RectTransform m_rtf_window_Over;

        [SerializeField] float m_duration_windowFade;

        Vector2 m_vec2_originalSize_Clear;
        Vector2 m_vec2_originalSize_Over;

        public void OnBtnClicked_Restart()
        {
            m_uEvent_Restart.Invoke();
        }

        public void OnBtnClicked_GoSceneMain()
        {
            m_uEvent_GoSceneMain.Invoke();
        }

        public void OnBtnClicked_GoSceneNextStage()
        {
            m_uEvent_GoSceneNextStage.Invoke();
        }

        public void WindowActive_Clear(bool active)
        {
            WindowActive(active, true);
        }
        public void WindowActive_Over(bool active)
        {
            WindowActive(active, false);
        }

        void Awake()
        {
            m_uEvent_Restart.AddListener(FindObjectOfType<StageManager>().OnEnterStage);

            m_vec2_originalSize_Clear = m_rtf_window_Clear.transform.localScale;
            m_vec2_originalSize_Over = m_rtf_window_Over.transform.localScale;
        }
        void WindowActive(bool active, bool isClear)
        {
            Transform targetWindow = isClear ? m_rtf_window_Clear : m_rtf_window_Over;
            Vector2 originalSize = isClear ? m_vec2_originalSize_Clear : m_vec2_originalSize_Over;
            if (active)
            {
                //Ʈ����
                targetWindow.gameObject.SetActive(true);
                targetWindow.DOKill();
                targetWindow.localScale = Vector2.zero;
                targetWindow.DOScale(originalSize, m_duration_windowFade).SetEase(Ease.OutBack);

                //â ���� ��ġ����
                if (isClear)
                {
                    m_resultWindow_Clear.UpdateLeftTime();
                }
                else
                {

                }

                //�ڷ�ƾ �ʱ�ȭ
                if (UnactiveCycle_C != null) StopCoroutine(UnactiveCycle_C);
            }
            else
            {
                //Ʈ����
                targetWindow.gameObject.SetActive(true);
                targetWindow.DOKill();
                targetWindow.localScale = originalSize;
                targetWindow.DOScale(Vector2.zero, m_duration_windowFade).SetEase(Ease.InBack);

                //�ڷ�ƾ �ʱ�ȭ, ��Ȱ��ȭ �ڷ�ƾ
                if (UnactiveCycle_C != null) StopCoroutine(UnactiveCycle_C);
                UnactiveCycle_C = UnactiveCycle(isClear, m_duration_windowFade);
                StartCoroutine(UnactiveCycle_C);
            }
        }

        WaitForSeconds UnactiveCycle_WFS;
        IEnumerator UnactiveCycle_C;
        IEnumerator UnactiveCycle(bool isClear, float waitT)
        {
            while (waitT > 0) 
            {
                waitT -= Time.deltaTime;
                yield return null;
            }

            if (isClear)
            {
                m_rtf_window_Clear.gameObject.SetActive(false);
            }
            else
            {
                m_rtf_window_Clear.gameObject.SetActive(false);
            }
        }


        //[Space(30)]
        //[SerializeField] bool testActive;
        //[SerializeField] bool testIsClear;
        //[ContextMenu("TestWindowActive")]
        //public void TestWindowActvie()
        //{
        //    WindowActive(testActive, testIsClear);
        //}
    }
}