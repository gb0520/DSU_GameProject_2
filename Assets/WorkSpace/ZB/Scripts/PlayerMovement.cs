using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using ZB;

namespace ZB
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] FloatingJoystick joyStick;
        [SerializeField] Rigidbody rb;
        [SerializeField] Transform tf;
        [SerializeField] Transform lookAt;
        [Space]
        [Header("�̵�")]
        [SerializeField] float moveSpeed;
        [SerializeField] float moveMaxSpeed;
        [Header("ȸ��")]
        [SerializeField] float rotSpeed;
        [SerializeField] float rotMul_magnitude;
        [Space]
        [SerializeField] Vector2 joyStickInput;
        [SerializeField] Vector3 nowVelocity;
        [SerializeField] float rotResult;

        public void OnMoveInput(Vector2 dir)
        {
            //ȸ��
            //float rotResult;
            if (dir.x >= 0)
            {
                rotResult = (-(ZBMath.GetAngleByVector2(dir) - 90) * dir.magnitude * rotMul_magnitude) * Time.deltaTime * rotSpeed;
            }
            else
            {
                rotResult = ZBMath.GetAngleByVector2(dir);
                if (rotResult >= 0)
                {
                    rotResult = rotResult - 90;
                }
                else
                {
                    rotResult = rotResult + 270;
                }
                rotResult = ((-rotResult) * dir.magnitude * rotMul_magnitude) * Time.deltaTime * rotSpeed;
            }
            ZBMath.AddRotationY(tf, rotResult);

            //��
            Vector3 movePower = (lookAt.position - tf.position) * dir.magnitude * moveSpeed;
            rb.velocity = new Vector3(movePower.x, rb.velocity.y, movePower.z);

            //�ִ�ӵ� ����
            {
                if (rb.velocity.x > moveMaxSpeed)
                {
                    rb.velocity = new Vector3(moveMaxSpeed, rb.velocity.y, rb.velocity.z);
                }
                else if (rb.velocity.x < -moveMaxSpeed)
                {
                    rb.velocity = new Vector3(-moveMaxSpeed, rb.velocity.y, rb.velocity.z);
                }

                if (rb.velocity.z > moveMaxSpeed)
                {
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, moveMaxSpeed);
                }
                else if (rb.velocity.z < -moveMaxSpeed)
                {
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -moveMaxSpeed);
                }
            }
            nowVelocity = rb.velocity;
        }
        private void Update()
        {
            joyStickInput = new Vector2(joyStick.Horizontal, joyStick.Vertical);
            OnMoveInput(joyStickInput);
        }
    }
}