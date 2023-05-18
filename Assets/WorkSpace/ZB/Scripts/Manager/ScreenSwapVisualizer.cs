using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace ZB
{
    public class ScreenSwapVisualizer : MonoBehaviour
    {
        public bool Fading { get { return m_fading; } }

        [SerializeField] Transform m_tf_Mask;
        [SerializeField] Transform m_tf_Masked;

        [Header("확인용")]
        [SerializeField] bool m_fading;

        [Header("변경가능")]
        [SerializeField] float m_duration_FadeIn;
        [SerializeField] float m_duration_LoadWait;
        [SerializeField] float m_duration_FadeOut;
        [SerializeField] float m_circleSize;

        UnityEvent OnFadeEnded;

        public void Fade(UnityAction fadeEndAction = null)
        {
            if (fadeEndAction != null)
                OnFadeEnded.AddListener(fadeEndAction);

            if (MoveCycle_C != null)
                StopCoroutine(MoveCycle_C);
            MoveCycle_C = MoveCycle();
            StartCoroutine(MoveCycle_C);
        }

        [ContextMenu("FadeIn")]
        public void TestFadeIn()
        {
            Fade();
        }

        WaitForSecondsRealtime MoveCycle_WFS;
        IEnumerator MoveCycle_C;
        IEnumerator MoveCycle()
        {
            m_fading = true;

            m_tf_Mask.gameObject.SetActive(true);
            m_tf_Masked.gameObject.SetActive(true);

            //Fade In
            m_tf_Mask.DOKill();
            m_tf_Mask.localScale = new Vector2(m_circleSize, m_circleSize);
            m_tf_Mask.DOScale(Vector2.zero, m_duration_FadeIn).SetUpdate(true).SetEase(Ease.InQuart);

            yield return MoveCycle_WFS;

            OnFadeEnded.Invoke();

            //FadeOut
            m_tf_Mask.DOKill();
            m_tf_Mask.localScale = Vector2.zero;
            m_tf_Mask.DOScale(new Vector2(m_circleSize, m_circleSize), m_duration_FadeOut).SetUpdate(true).SetEase(Ease.InQuart).OnComplete(()=>
            {
                m_fading = false;
                OnFadeEnded.RemoveAllListeners();

                m_tf_Mask.gameObject.SetActive(false);
                m_tf_Masked.gameObject.SetActive(false);
            });
        }

        private void Awake()
        {
            OnFadeEnded = new UnityEvent();
            MoveCycle_WFS = new WaitForSecondsRealtime(m_duration_LoadWait);

            m_tf_Mask.gameObject.SetActive(false);
            m_tf_Masked.gameObject.SetActive(false);
        }
    }
}