using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ZB.Drag
{
    public class ScreenDrag : MonoBehaviour
    {
        /// <summary>
        /// 드래그 시작할 때 이벤트
        /// </summary>
        public UnityEvent m_UEvent_OnEnter;

        /// <summary>
        /// 드래그 끝날 때 이벤트
        /// </summary>
        public UnityEvent m_UEvent_OnExit;

        /// <summary>
        /// 최근 드래그한 벡터
        /// </summary>
        public Vector2 m_DragVector { get => m_screen.m_DragVector; }

        /// <summary>
        /// 최근 드래그한 벡터 크기
        /// </summary>
        public float m_Magnitude { get => m_screen.m_DragVector.magnitude; }

        /// <summary>
        /// 드래그 진행 중
        /// </summary>
        public bool m_Dragging { get => m_screen.m_Dragging; }

        [SerializeField] Screen m_screen;
        [SerializeField] Image m_img_screen;

        /// <summary>
        /// 드래그 스크린 활성화
        /// </summary>
        /// <param name="활성화 여부"></param>
        public void Active(bool active)
        {
            m_screen.gameObject.SetActive(active);
        }

        private void Awake()
        {
            m_img_screen.color = Color.clear;
        }
    }
}