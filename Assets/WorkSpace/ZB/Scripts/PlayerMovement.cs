using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using ZB;

namespace ZB
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] Rigidbody rb;
        [SerializeField] Transform tf;
        [Space]
        [Header("이동")]
        [SerializeField] float moveSpeed;
        [SerializeField] float moveMaxSpeed;
        [Header("회전")]
        [SerializeField] float rotSpeed;
        [SerializeField] float rotMul_magnitude;
        [Space]
        [SerializeField] Vector3 nowVelocity;
        [SerializeField] float asdf;

        [SerializeField] Vector2 temp;

        public void OnMoveInput(Vector2 dir)
        {
            float rotResult = 0;

            rotResult = (-(ZBMath.GetAngleByVector2(dir) - 90) * dir.magnitude * rotMul_magnitude) * Time.deltaTime * rotSpeed;

            ZBMath.AddRotationY(tf, rotResult);
        }
        private void Update()
        {
            Debug.Log(-(ZBMath.GetAngleByVector2(temp) - 90));
        }
    }
}