using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using System.Linq;
using ZB;

namespace JH
{
    public class BallState : MonoBehaviour
    {
        [SerializeField] private float currentScore;
        [SerializeField] private float completeScore;

        [SerializeField] private ObjectSpawner spawner;
        [SerializeField] private TextMeshProUGUI test;
        public BallMove ball;
        public float tempSize;

        private Queue<Attach> attaches;

        public float CurrentScore { get => currentScore; set => currentScore = value; }
        public float CompleteScore { get => completeScore; set => completeScore = value; }

        private void Awake()
        {
            attaches = new Queue<Attach>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
                PopAttach(attaches.Count);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "fieldItem")
            {
                Attach attach = collision.gameObject.GetComponent<Attach>();

                if(attach.Least <= currentScore) //object_getPoint <= currentScore
                {
                    Managers.instance.SoundMaster.Play(Managers.instance.SoundMaster.clips[1], false);
                    int temp = attach.Index;
                    float attachSize = attach.Size;
                    float attachScore = attach.Score;
                    attach.ball = this;

                    BallUpdate(attachSize, attachScore);
                    tempSize += attachSize;
                    attach.gameObject.tag = "pieceItem";
                    attach.gameObject.layer = 8;
                    attach.gameObject.transform.position = this.gameObject.transform.position;
                    GetAttach(attach);
                }
            }

            if(collision.gameObject.tag == "unAttach")
            {
                Managers.instance.SoundMaster.Play(Managers.instance.SoundMaster.clips[2], false);
                Attach attach = collision.gameObject.GetComponent<Attach>();

                PopAttach(attach.Count);
                ObjectPool.instance.ReturnPool(attach.gameObject, attach.Index);
            }
        }

        private void GetAttach(Attach _attach)
        {
            attaches.Enqueue(_attach);
        }

        private void PopAttach(int _index)
        {
            if (_index > attaches.Count)
                _index = attaches.Count;

            for (int i = 0; i < _index; i++)
            {
                Attach temp = attaches.Dequeue();
                temp.gameObject.tag = "null";
                //temp.gameObject.layer = 0;
                BallUpdate(temp.Size * -1, temp.Score * -1);
            }
        }

        public void BallSizeUp(float _plus)
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

        public void BoomButton()
        {
            PopAttach(attaches.Count);
        }

        
    }
}



