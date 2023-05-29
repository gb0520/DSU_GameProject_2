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
        [Header("재시작, 씬변환")]
        [SerializeField] UnityEvent m_uEvent_Restart;
        [SerializeField] UnityEvent m_uEvent_GoSceneMain;
        [SerializeField] UnityEvent m_uEvent_GoSceneNextStage;

        [Space, Header("클리어, 오버 윈도우 관리")]
        [SerializeField] ResultWindow_Clear m_resultWindow_Clear;
        [SerializeField] ResultWindow_Over m_resultWindow_Over;

        [SerializeField] RectTransform m_rtf_window_Clear;
        [SerializeField] RectTransform m_rtf_window_Over;
        [SerializeField] RectTransform m_rtf_window_Pause;

        [SerializeField] float m_duration_windowFade;

        Vector2 m_vec2_originalSize_Clear;
        Vector2 m_vec2_originalSize_Over;

        [SerializeField] string m_sceneName_Main;
        [SerializeField] string m_sceneName_Restart;
        [SerializeField] string m_sceneName_Next;

        private void Update() //테스트
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                WindowActive_Pause();
            }
        }

        public void OnBtnClicked_Restart()
        {
            m_uEvent_Restart.Invoke();
            Managers.instance.SceneMove.SceneMoveStart(m_sceneName_Restart);
        }

        public void OnBtnClicked_GoSceneMain()
        {
            m_uEvent_GoSceneMain.Invoke();
            Managers.instance.SceneMove.SceneMoveStart(m_sceneName_Main);
        }

        public void OnBtnClicked_GoSceneNextStage()
        {
            m_uEvent_GoSceneNextStage.Invoke();
            Managers.instance.SceneMove.SceneMoveStart(m_sceneName_Next);
        }

        public void OnBtnClicked_Resume()
        {
            Time.timeScale = 1;
            m_rtf_window_Pause.GetComponent<DOTweenAnimation>().DORestartById("Resume");
            transform.DOLocalMove(Vector3.zero, 0.3f).OnComplete(() =>
            {
                m_rtf_window_Pause.gameObject.SetActive(false);
            });

        }

        public void WindowActive_Pause()
        {
            if (m_rtf_window_Pause.gameObject.activeSelf) return;
            m_rtf_window_Pause.gameObject.SetActive(true);
            m_rtf_window_Pause.GetComponent<DOTweenAnimation>().DORestartById("Pause");
            transform.DOLocalMove(Vector3.zero, 0.3f).OnComplete(() =>
            {
                Time.timeScale = 0; 
            });
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
            m_vec2_originalSize_Clear = m_rtf_window_Clear.transform.localScale;
            m_vec2_originalSize_Over = m_rtf_window_Over.transform.localScale;
        }
        void WindowActive(bool active, bool isClear)
        {
            Transform targetWindow = isClear ? m_rtf_window_Clear : m_rtf_window_Over;
            Vector2 originalSize = isClear ? m_vec2_originalSize_Clear : m_vec2_originalSize_Over;
            if (active)
            {
                Debug.LogError("열려라");
                //트위닝
                targetWindow.gameObject.SetActive(true);
                targetWindow.DOKill();
                targetWindow.localScale = Vector2.zero;
                targetWindow.DOScale(originalSize, m_duration_windowFade).SetEase(Ease.OutBack);

                //창 개별 수치조정
                if (isClear)
                {
                    m_resultWindow_Clear.UpdateLeftTime();
                }
                else
                {

                }

                //코루틴 초기화
                if (UnactiveCycle_C != null) StopCoroutine(UnactiveCycle_C);
            }
            else
            {
                Debug.LogError("닫혀라");
                //트위닝
                targetWindow.gameObject.SetActive(true);
                targetWindow.DOKill();
                targetWindow.localScale = originalSize;
                targetWindow.DOScale(Vector2.zero, m_duration_windowFade).SetEase(Ease.InBack);

                //코루틴 초기화, 비활성화 코루틴
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