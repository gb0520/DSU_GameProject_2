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
        public enum State { None, fadeIn, load, fadeOut}

        [SerializeField] Transform m_tf_Mask;
        [SerializeField] Transform m_tf_Masked;

        [Header("확인용")]
        [SerializeField] State nowState;
        [SerializeField] float m_current_duration_In;
        [SerializeField] float m_current_duration_Out;
        [SerializeField] Ease m_current_ease_In;
        [SerializeField] Ease m_current_ease_Out;

        [Header("변경가능")]
        [SerializeField] float m_circleSize;

        public void Fade(UnityEvent fadeEndEvent, float duration_In = 1, float duration_Out = 1, Ease ease_In = Ease.OutQuart, Ease ease_Out = Ease.OutQuart)
        {
            if (nowState == State.None || nowState == State.fadeIn)
            {
                m_current_duration_In = duration_In;
                m_current_duration_Out = duration_Out;
                m_current_ease_In = ease_In;
                m_current_ease_Out = ease_Out;

                m_tf_Mask.gameObject.SetActive(true);
                m_tf_Masked.gameObject.SetActive(true);

                //FadeIn
                nowState = State.fadeIn;
                m_tf_Mask.DOKill();
                m_tf_Mask.localScale = new Vector2(m_circleSize, m_circleSize);
                m_tf_Mask.DOScale(Vector2.zero, m_current_duration_In).SetUpdate(true).SetEase(m_current_ease_In).OnComplete(() =>
                {
                    fadeEndEvent.Invoke();
                    nowState = State.load;
                });

                //FadeOut
                transform.DOLocalMove(Vector2.zero, m_current_duration_In + 1).OnComplete(() =>
                {
                    nowState = State.fadeOut;
                    m_tf_Mask.DOKill();
                    m_tf_Mask.localScale = Vector2.zero;
                    m_tf_Mask.DOScale(new Vector2(m_circleSize, m_circleSize), m_current_duration_Out).SetUpdate(true).SetEase(m_current_ease_Out).OnComplete(() =>
                    {
                        nowState = State.None;

                        m_tf_Mask.gameObject.SetActive(false);
                        m_tf_Masked.gameObject.SetActive(false);
                    });
                });
            }
        }
        public void FadeCancel()
        {
            //fadein 일때만 취소가능
            if (nowState == State.fadeIn)
            {
                nowState = State.fadeOut;
                transform.DOKill();
                m_tf_Mask.DOKill();
                m_tf_Mask.DOScale(new Vector2(m_circleSize, m_circleSize), m_current_duration_Out).SetUpdate(true).SetEase(m_current_ease_Out).OnComplete(() =>
                {
                    nowState = State.None;

                    m_tf_Mask.gameObject.SetActive(false);
                    m_tf_Masked.gameObject.SetActive(false);
                });
            }
        }

        private void Awake()
        {
            m_tf_Mask.gameObject.SetActive(false);
            m_tf_Masked.gameObject.SetActive(false);
        }
    }
}