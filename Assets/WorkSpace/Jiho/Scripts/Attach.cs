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
        [SerializeField] private float size;
        [SerializeField] private float score;
        [SerializeField] private float least;
        [SerializeField] private float test;
        [SerializeField] private bool isShoot;

        //----------------------ÆøÆÄ º¯¼ö
        private Vector3 startPos, endPos;
        private float timer;
        private float dist;
        //------------------------------

        public BallState ball;

        public int Index { get => index; set => index = value; }
        public float Size { get => size; set => size = value; }
        public float Score { get => score; set => score = value; }
        public float Least { get => least; set => least = value; }
        private void Update()
        {
            if(this.gameObject.tag == "pieceItem")
            {
                dist = Vector3.Distance(this.transform.position, ball.transform.position);
                if (dist > test)
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
            isShoot = false;
        }

        private void Shoot()
        {
            this.gameObject.layer = 6;
            isShoot = true;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            startPos = transform.position;
            endPos = transform.forward * 1 / 1.5f;
            StartCoroutine("BulletMove");
        }
    }
}

