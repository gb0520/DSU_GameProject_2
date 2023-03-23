using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace JH
{
    public class Attach : MonoBehaviour
    {
        [SerializeField] private int index;
        [SerializeField] private float size;
        [SerializeField] private float test;

        //-------------------------오브젝트 팝 포물선 변수
        [SerializeField] private float angle = 45f;
        [SerializeField] private float gravity = 9.8f;
        [SerializeField] private bool isShoot;

        [SerializeField] private Transform projectile;
        [SerializeField] private Transform myTransform;
        private Vector3 target;
        //---------------------------------------------


        private float dist;
        private BallState ball;

        public BallState Ball { get => ball; set => ball = value; }
        public int Index { get => index; set => index = value; }
        public float Size { get => size; set => size = value; }

        private void Awake()
        {
            
            
        }

        private void Update()
        {
            if(this.gameObject.tag == "pieceItem")
            {
                dist = Vector3.Distance(this.transform.position, ball.transform.position);
                if (dist > (ball.BallSize() / test))
                {
                    transform.position = ball.transform.position;
                }
            }
            else if (this.gameObject.tag == "null" && !isShoot)
            {
                Shoot();
                
            }
        }

        private void Shoot()
        {
            this.gameObject.layer = 6;
            isShoot = true;
            target = new Vector3(transform.position.x * 3, transform.position.y, transform.position.z * 3);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 3, this.transform.position.z);
            myTransform = this.transform;
            StartCoroutine(PopCoroutine());
            
        }

        private IEnumerator PopCoroutine()
        {
            projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);
            float target_Distance = Vector3.Distance(projectile.position, target);
            float projectile_Velocity = target_Distance / (Mathf.Sin(2 * angle * Mathf.Deg2Rad) / gravity);

            float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(angle * Mathf.Deg2Rad);
            float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(angle * Mathf.Deg2Rad);

            float flightDuration = target_Distance / Vx;

            projectile.rotation = Quaternion.LookRotation(target - projectile.position);

            float elapse_time = 0f;

            while (elapse_time < flightDuration)
            {
                projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

                elapse_time += Time.deltaTime;

                dist = Vector3.Distance(this.transform.position, ball.transform.position);

                yield return null;
            }
            this.gameObject.tag = "fieldItem";
            isShoot = false;
            this.gameObject.layer = 0;
        }

    }
}

