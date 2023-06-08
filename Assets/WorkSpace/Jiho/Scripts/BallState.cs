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
        [SerializeField] private LayerMask wallLayer;
        [SerializeField] private GameObject danger_obj;
        [SerializeField] private GameObject ray_obj;
        [SerializeField] private ParticleSystem particle;

        private Ray ray;
        RaycastHit hit;
        public BallMove ball;
        public float tempSize;

        private Queue<Attach> attaches;

        public float CurrentScore { get => currentScore; set => currentScore = value; }
        public float CompleteScore { get => completeScore; set => completeScore = value; }

        private void Awake()
        {
            attaches = new Queue<Attach>();
            ray = new Ray();
            
        }

        private void Update()
        {
            WallDistanceCheck();
        }

        private void WallDistanceCheck()
        {
            ray.origin = ray_obj.transform.position;
            ray.direction = ray_obj.transform.forward;

            if (Physics.Raycast(ray.origin, ray.direction, out hit, 10, wallLayer))
            {
                danger_obj.SetActive(true);

                if(hit.transform.CompareTag("wall_horizontal"))
                {
                    danger_obj.transform.localScale = new Vector3(0.01f, 2.5f * transform.localScale.y, 2.5f * transform.localScale.z);
                    danger_obj.transform.position = new Vector3(hit.point.x, transform.position.y, transform.position.z);
                }
                else if(hit.transform.CompareTag("wall_vertical"))
                {
                    danger_obj.transform.localScale = new Vector3(2.5f * transform.localScale.x, 2.5f * transform.localScale.y, 0.01f);
                    danger_obj.transform.position = new Vector3(transform.position.x, transform.position.y, hit.point.z);
                }
            }
            else
            {
                if(danger_obj != null) danger_obj.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "fieldItem")
            {
                Attach attach = other.gameObject.GetComponent<Attach>();

                if (attach.Least <= currentScore) //object_getPoint <= currentScore
                {
                    Managers.instance.SoundMaster.Play(Managers.instance.SoundMaster.audioDictionary["plus"], false);
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

                    //아웃라인 비활성화
                    ZB.OutLineResize.Resize resize;
                    if (attach.TryGetComponent(out resize))
                        resize.Active(false);
                }
            }

            if (other.gameObject.tag == "unAttach")
            {
                Managers.instance.SoundMaster.Play(Managers.instance.SoundMaster.audioDictionary["minus"], false);
                Attach attach = other.gameObject.GetComponent<Attach>();

                PopAttach(attach.Count);
                ObjectPool.instance.ReturnPool(attach.gameObject, attach.Index);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.gameObject.layer == 24)
            {
                if (collision.transform.CompareTag("Floor")) return;
                ContactPoint contact = collision.contacts[0];
                Vector3 pos = contact.point;

                particle.transform.position = pos;
                particle.Play();
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

                //아웃라인 활성화
                ZB.OutLineResize.Resize resize;
                if (temp.TryGetComponent(out resize))
                    resize.Active(true);
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



