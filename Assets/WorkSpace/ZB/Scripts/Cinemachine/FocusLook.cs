using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace ZB.Drag
{
    public class FocusLook : MonoBehaviour
    {
        public Vector3 m_LookDir { get => m_cameraPoint.position - m_cameraPoint.position; }

        [Header("Flexible")]
        [SerializeField] private float m_dragSpeed;
        [SerializeField] private Transform m_realFollow;
        [SerializeField] private ScreenDrag m_screenDrag;
        [Header("Fixed")]
        [SerializeField] private CinemachineVirtualCamera m_vCam;
        [SerializeField] private Transform m_cameraPoint;
        [SerializeField] private bool m_camMoving;

        public void MoveStart()
        {
            m_camMoving = true;

            if (CamMove_C != null)
                StopCoroutine(CamMove_C);
            CamMove_C = CamMove();
            StartCoroutine(CamMove_C);
        }

        public void MoveEnd()
        {
            m_camMoving = false;

            if (CamMove_C != null)
                StopCoroutine(CamMove_C);
        }

        private void Move(float dir)
        {
            m_cameraPoint.Rotate(Vector3.up, dir);
        }
        private void Update()
        {
            m_cameraPoint.position = m_realFollow.position;
        }

        IEnumerator CamMove_C;
        IEnumerator CamMove()
        {
            while(true)
            {
                if (!m_screenDrag.m_DragStop)
                    Move(m_screenDrag.m_DragVector_OneFrame.x * m_dragSpeed);

                yield return null;
            }
        }
    }
}