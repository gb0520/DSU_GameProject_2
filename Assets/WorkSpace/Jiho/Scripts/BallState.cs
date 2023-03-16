using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace JH
{
    public class BallState : MonoBehaviour, IStickyBall
    {
        [SerializeField] private float speed;
        [SerializeField] private float ballSize;
        [SerializeField] private FloatingJoystick joy;

        private void Update()
        {
            if (joy.Vertical != 0 || joy.Horizontal != 0)
                Roll(speed);
        }

        private void BallSizeUp(float _plus)
        {
            Vector3 currentVec = transform.localScale;

            ballSize = transform.localScale.x + _plus;

            transform.DOKill();
            transform.DOScale(new Vector3(currentVec.x + _plus, currentVec.y + _plus, currentVec.z + _plus), 1f).SetEase(Ease.OutBounce);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "fieldItem")
            {
                Attach attach = collision.gameObject.GetComponent<Attach>();

                int temp = attach.Index;
                float test = attach.Size;
                attach.Ball = this;

                //ObjectSpawner.ObjReturn(collision.gameObject, temp);
                BallSizeUp(test);
                BallSize();
                collision.gameObject.tag = "pieceItem";
                collision.gameObject.transform.position = this.gameObject.transform.position;
            }
        }

        public void Roll(float speed)
        {
            transform.Rotate(Vector3.right * Time.deltaTime * (speed * 300));
        }

        public float BallSize()
        {
            Debug.Log(ballSize);
            return ballSize;
        }
    }
}



