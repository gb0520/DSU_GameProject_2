using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ZB.Drag
{
    public class Screen : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] RectTransform m_rtf_inputStart;
        [SerializeField] RectTransform m_rtf_inputEnd;
        [SerializeField] ScreenDrag m_screenDrag;

        public bool m_Dragging { get => m_dragging; }
        public Vector2 m_DragVector { get => m_rtf_inputEnd.position - m_rtf_inputStart.position; }

        bool m_dragging;

        public void OnBeginDrag(PointerEventData eventData)
        {
            m_rtf_inputStart.position = eventData.position;
            m_dragging = true;
            m_screenDrag.m_UEvent_OnEnter.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            m_rtf_inputEnd.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            m_dragging = false;
            m_screenDrag.m_UEvent_OnExit.Invoke();
        }

        private void OnDisable()
        {
            m_dragging = false;
        }
    }
}