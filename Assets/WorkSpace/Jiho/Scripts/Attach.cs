using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JH
{
    public class Attach : MonoBehaviour
    {
        [SerializeField] private int index;
        [SerializeField] private float size;
        [SerializeField] private float test;

        private float dist;
        private BallState ball;

        public BallState Ball { get => ball; set => ball = value; }
        public int Index { get => index; set => index = value; }
        public float Size { get => size; set => size = value; }

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
        }


    }
}

