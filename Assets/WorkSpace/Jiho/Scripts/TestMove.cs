using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using UnityEngine.PlayerLoop;

namespace JH
{
    public class TestMove : MonoBehaviour, IStickyBall
    {
        [SerializeField] private VariableJoystick joy;
        [SerializeField] private float speed;
        [SerializeField] private float ballSize;

        private GameObject ball_obj;

        private Rigidbody rigid;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            ball_obj = this.gameObject;
        }
        
        private void Update()
        {
            if(joy.Vertical != 0 || joy.Horizontal != 0)
                Roll(speed);
        }

        /*private void MoveVecter()
        {

            moveVecX = joy.Horizontal;
            moveVecZ = joy.Vertical;
            moveVec = new Vector3(moveVecX, 0, moveVecZ) * speed * Time.deltaTime;
            rigid.MovePosition(rigid.position + moveVec);

            if (moveVec.sqrMagnitude == 0)
                return;

            Quaternion dir = Quaternion.LookRotation(moveVec);
            Quaternion move = Quaternion.Slerp(rigid.rotation, dir, 0.3f);
            rigid.MoveRotation(move);
            Debug.Log(moveVecX);
            Debug.Log(moveVecZ);
        }*/

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "d")
            {
                int temp = collision.gameObject.GetComponent<Attach>().Index;
                float test = collision.gameObject.GetComponent<Attach>().Size;
                ObjectSpawner.ObjReturn(collision.gameObject, temp);
                BallSizeUp(test);
                BallSize();
            }
        }

        private void BallSizeUp(float _plus)
        {
            Vector3 currentVec = ball_obj.transform.localScale;

            ballSize = ball_obj.transform.localScale.x + _plus;

            ball_obj.transform.DOKill();
            ball_obj.transform.DOScale(new Vector3(currentVec.x + _plus, currentVec.y + _plus, currentVec.z + _plus),1f).SetEase(Ease.OutBounce); 
        }

        public void Roll(float speed)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        public float BallSize()
        {
            Debug.Log(ballSize);
            return ballSize;
        }
    }
}

