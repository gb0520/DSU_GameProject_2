using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

namespace JH
{
    public class BallState : MonoBehaviour, IStickyBall
    {
        [SerializeField] private float speed;
        [SerializeField] private float ballSize;
        [SerializeField] private float currentScore;
        [SerializeField] private float completeScore;
        [SerializeField] private Transform ballTransform;

        [SerializeField] private FloatingJoystick joy;
        [SerializeField] private ObjectSpawner spawner;
        [SerializeField] private TextMeshProUGUI test;

        private Queue<Attach> attaches;

        private void Awake()
        {
            attaches = new Queue<Attach>();
        }

        private void Update()
        {
            if (joy.Vertical != 0 || joy.Horizontal != 0)
                Roll(speed);

            if (Input.GetKeyDown(KeyCode.F))
                PopAttach(attaches.Count);

            Complete();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "fieldItem")
            {
                Attach attach = collision.gameObject.GetComponent<Attach>();

                int temp = attach.Index;
                float attachSize = attach.Size;
                float attachScore = attach.Score;
                attach.ball = this;

                BallUpdate(attachSize, attachScore);
                BallSize();
                collision.gameObject.tag = "pieceItem";
                collision.gameObject.transform.position = this.gameObject.transform.position;
                GetAttach(attach);

            }
        }

        private void Complete()
        {
            if (currentScore >= completeScore)
                test.gameObject.SetActive(true);
        }

        private void GetAttach(Attach _attach)
        {
            attaches.Enqueue(_attach);
        }

        private void PopAttach(int _index)
        {
            for (int i = 0; i < _index; i++)
            {
                Attach temp = attaches.Dequeue();
                temp.gameObject.tag = "null";
                BallUpdate(temp.Size * -1, temp.Score * -1);
            }
        }

        private void BallSizeUp(float _plus)
        {
            Vector3 currentVec = transform.localScale;

            if (_plus > 0)
            {

                ballSize = transform.localScale.x + _plus;
                
                transform.DOScale(new Vector3(currentVec.x + _plus, currentVec.y + _plus, currentVec.z + _plus), 0.01f).SetEase(Ease.OutBounce);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x + _plus, transform.localScale.y + _plus, transform.localScale.z + _plus);
            }
        }

        private void BallScoreUp(float _score)
        {
            currentScore += _score;
        }

        private void BallUpdate(float _plus, float _score)
        {
            BallSizeUp(_plus);
            BallScoreUp(_score);
        }

        public float BallSize()
        {
            Debug.Log(ballSize);
            return ballSize;
        }

        public void Roll(float speed)
        {
            transform.Rotate(Vector3.right * Time.deltaTime * (speed * 300));
        }

        
    }
}



