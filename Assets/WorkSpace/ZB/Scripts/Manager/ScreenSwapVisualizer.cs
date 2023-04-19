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

        [Header("확인용")]
        [SerializeField] bool m_fading;

        [Header("변경가능")]
        [SerializeField] float m_moveDuration;
        [SerializeField] float m_circleSize;

        UnityEvent OnFadeEnded;

        /// <summary>
        /// true : Fade In / false : Fade Out
        /// </summary>
        /// <param name="active"></param>
        /// <param name="fadeEndAction"></param>
        public void Fade(bool active, UnityAction fadeEndAction = null)
        {
            //Fade In
            if (active)
            {
                m_tf_Mask.DOKill();
                m_tf_Mask.localScale = new Vector2(m_circleSize, m_circleSize);
                m_tf_Mask.DOScale(Vector2.zero, m_moveDuration).SetEase(Ease.InQuart);
            }

            //Fade Out
            else
            {
                m_tf_Mask.DOKill();
                m_tf_Mask.localScale = Vector2.zero;
                m_tf_Mask.DOScale(new Vector2(m_circleSize, m_circleSize), m_moveDuration).SetEase(Ease.InQuart);
            }

            if (MoveCycle_C != null)
                StopCoroutine(MoveCycle_C);
            MoveCycle_C = MoveCycle();
            StartCoroutine(MoveCycle_C);
        }

        [ContextMenu("FadeIn")]
        public void TestFadeIn()
        {
            Fade(true);
        }
        [ContextMenu("FadeOut")]
        public void TestFadeOut()
        {
            Fade(false);
        }

        WaitForSeconds MoveCycle_WFS;
        IEnumerator MoveCycle_C;
        IEnumerator MoveCycle()
        {
            m_fading = true;
            yield return MoveCycle_WFS;

            m_fading = false;
            OnFadeEnded.Invoke();
            OnFadeEnded = null;
        }

        private void Awake()
        {
            OnFadeEnded = new UnityEvent();
        }
    }
}