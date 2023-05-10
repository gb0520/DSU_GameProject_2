using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ZB.Drag
{
    public class ScreenDrag : MonoBehaviour
    {
        public bool ShowDebug { get => showDebug; }
        [SerializeField] bool showDebug;

        /// <summary>
        /// �巡�� ������ �� �̺�Ʈ
        /// </summary>
        public UnityEvent m_UEvent_OnEnter;

        /// <summary>
        /// �巡�� ���� �� �̺�Ʈ
        /// </summary>
        public UnityEvent m_UEvent_OnExit;

        /// <summary>
        /// �ֱ� �巡���� ����
        /// </summary>
        public Vector2 m_DragVector { get => m_screen.m_DragVector; }

        /// <summary>
        /// �ѹ� �˻��ϴ� �ҵ��� �̵��� ����
        /// </summary>
        public Vector2 m_DragVector_OneFrame { get => m_screen.m_DragVector_OneFrame; }

        /// <summary>
        /// �ֱ� �巡���� ���� ũ��
        /// </summary>
        public float m_Magnitude { get => m_screen.m_DragVector.magnitude; }

        /// <summary>
        /// �巡�� ���� ��
        /// </summary>
        public bool m_Dragging { get => m_screen.m_Dragging; }

        /// <summary>
        /// �巡�� ���� �߿�, ����
        /// </summary>
        public bool m_DragStop { get => m_screen.m_DragStop; }

        [SerializeField] Screen m_screen;

        /// <summary>
        /// �巡�� ��ũ�� Ȱ��ȭ
        /// </summary>
        /// <param name="Ȱ��ȭ ����"></param>
        public void Active(bool active)
        {
            m_screen.gameObject.SetActive(active);
        }
    }
}