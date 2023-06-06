using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZB.OutLineResize
{
    public class Resize : MonoBehaviour
    {
        public static JH.BallState BallState;

        public bool Resizeable;

        [Space]
        [SerializeField] private Outline targetOutLine;
        [SerializeField] private JH.Attach targetAttach;
        [SerializeField] private float lineSize;
        [SerializeField] private float resizeSpeed;

        [Space]
        [SerializeField] private bool entering;

        private void Awake()
        {
            if (BallState == null)
                BallState = FindObjectOfType<JH.BallState>();

            targetOutLine.OutlineWidth = 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 25 &&
                targetOutLine != null &&
                Resizeable)
            {
                OutLineActive(true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == 25 &&
                targetOutLine != null &&
                Resizeable)
            {
                OutLineActive(false);
            }
        }

        public void OutLineActive(bool active)
        {
            entering = active;
            if (ResizeCycle_C == null)
            {
                ResizeCycle_C = ResizeCycle();
                StartCoroutine(ResizeCycle_C);
            }
        }
        public void Active(bool active)
        {
            Debug.LogError(active);
            Resizeable = active;
            OutLineActive(active);
        }

        IEnumerator ResizeCycle_C;
        IEnumerator ResizeCycle()
        {
            int dir = 0;
            while (true)
            {
                //먹을 수 있는 조건 되는지 확인
                bool canAttach = targetAttach.Least <= BallState.CurrentScore;

                if (canAttach && entering)
                    dir = 1;
                else if (!entering)
                    dir = -1;

                targetOutLine.OutlineWidth += Time.deltaTime * resizeSpeed * dir;
                if (dir == 1 && targetOutLine.OutlineWidth >= lineSize)
                {
                    targetOutLine.OutlineWidth = lineSize;
                    break;
                }
                else if (dir == -1 && targetOutLine.OutlineWidth <= 0)
                {
                    targetOutLine.OutlineWidth = 0;
                    break;
                }

                yield return null;
            }
            ResizeCycle_C = null;
        }
    }
}