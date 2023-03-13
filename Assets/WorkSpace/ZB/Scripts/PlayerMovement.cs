using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform tf;
    [Space]
    [SerializeField] float moveSpeed;
    [SerializeField] float moveMaxSpeed;
    [Space]
    [SerializeField] Vector3 currentMoveInputDir;
    [SerializeField] Vector3 nowVelocity;

    public void Move(Vector2 dir)
    {
        currentMoveInputDir = dir;

        rb.AddForce(new Vector3(dir.x, 0, dir.y) * moveSpeed);
    }

    float h, v;
    void Update()
    {
        //현재 속도에 맞춰 회전
        nowVelocity = rb.velocity;
        tf.rotation = Quaternion.Euler(0, Mathf.Atan2(nowVelocity.x, nowVelocity.z) * Mathf.Rad2Deg, 0);

        //최대속도 제한
        {
            if (rb.velocity.x > moveMaxSpeed)
            {
                rb.velocity = new Vector3(moveMaxSpeed, rb.velocity.y, rb.velocity.z);
            }
            else if (rb.velocity.x < -moveMaxSpeed)
            {
                rb.velocity = new Vector3(-moveMaxSpeed, rb.velocity.y, rb.velocity.z);
            }

            if (rb.velocity.y > moveMaxSpeed)
            {
                rb.velocity = new Vector3(rb.velocity.x, moveMaxSpeed, rb.velocity.z);
            }
            else if (rb.velocity.y < -moveMaxSpeed)
            {
                rb.velocity = new Vector3(rb.velocity.x, -moveMaxSpeed, rb.velocity.z);
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

        //임시 인풋
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Move(new Vector2(h, v));
    }
}
