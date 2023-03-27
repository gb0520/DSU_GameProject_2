using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

namespace JH
{
    public class BallState : MonoBehaviour
    {
        [SerializeField] private float currentScore;
        [SerializeField] private float completeScore;

        [SerializeField] private ObjectSpawner spawner;
        [SerializeField] private TextMeshProUGUI test;
        [SerializeField] private BallMove ball;

        private Queue<Attach> attaches;

        private void Awake()
        {
            attaches = new Queue<Attach>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
                PopAttach(attaches.Count);

            Complete();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "fieldItem")
            {
                Attach attach = collision.gameObject.GetComponent<Attach>();

                if(attach.Least <= currentScore) //object_getPoint <= currentScore
                {
                    int temp = attach.Index;
                    float attachSize = attach.Size;
                    float attachScore = attach.Score;
                    attach.ball = this;

                    BallUpdate(transform.localScale.x + attachSize, attachScore);
                    collision.gameObject.tag = "pieceItem";
                    collision.gameObject.transform.position = this.gameObject.transform.position;
                    GetAttach(attach);
                }
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
            ball.OnBallSizeUp(_plus);
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

        
    }
}



