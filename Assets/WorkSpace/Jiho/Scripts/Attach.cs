using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using System;
namespace JH
{
    public class Attach : MonoBehaviour
    {
        [SerializeField] private int index;
        [SerializeField] private int count;
        [SerializeField] private float size;
        [SerializeField] private float score;
        [SerializeField] private float least;
        [SerializeField] private float test;
        [SerializeField] private bool isShoot;

        public BallState ball;

        //----------------------���� ����
        private Vector3 startPos, endPos;
        private float timer;
        private float dist;
        //------------------------------

        public int Index { get => index; set => index = value; }
        public int Count { get => count; set => count = value; }
        public float Size { get => size; set => size = value; }
        public float Score { get => score; set => score = value; }
        public float Least { get => least; set => least = value; }

        private void Update()
        {
            if(this.gameObject.tag == "pieceItem")
            {
                dist = Vector3.Distance(this.transform.position, ball.transform.position);
                if (dist > ball.gameObject.transform.localScale.x / 2.3f)
                {
                    transform.position = ball.transform.position;
                }
            }
            else if (this.gameObject.tag == "null" && !isShoot)
            {
                Shoot();
            }
        }

        private Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
        {
            Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

            var mid = Vector3.Lerp(start, end, t);

            return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
        }

        private IEnumerator BulletMove()
        {
            timer = 0;
            while (transform.position.y >= startPos.y)
            {
                dist = Vector3.Distance(this.transform.position, ball.transform.position);
                timer += Time.deltaTime;
                Vector3 tempPos = Parabola(startPos, endPos, 5, timer);
                transform.position = tempPos;
                yield return new WaitForEndOfFrame();

                if (dist >= (5 / 3))
                    this.gameObject.layer = 0;
            }
            this.gameObject.tag = "fieldItem";
            this.gameObject.layer = 0;
            isShoot = false;
        }

        private void Shoot()
        {
            float random = UnityEngine.Random.Range(transform.position.x - (transform.localScale.x * 5), transform.position.x + (transform.localScale.x * 5));
            float random2 = UnityEngine.Random.Range(transform.position.z - (transform.localScale.z * 5), transform.position.z + (transform.localScale.z * 5));

            this.gameObject.layer = 6;
            isShoot = true;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            startPos = transform.position;
            endPos = new Vector3(random, transform.position.y, random2);

            StartCoroutine("BulletMove");
        }
    }
}

