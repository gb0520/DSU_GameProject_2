using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZB;

public class BallMove : MonoBehaviour
{
    [SerializeField] Joystick joyStick;
    [SerializeField] Rigidbody rb;

    [SerializeField] Transform ballTf;
    [SerializeField] Transform ballMoveAssist;
    [SerializeField] Transform moveDir;
    [SerializeField] Transform modelPivot;
    [SerializeField] SphereCollider ballMoveAssist_Col;
    [SerializeField] ZB_CheckLayer_Ray3D ray;

    [Header("�̵����ɿ���")]
    [SerializeField] bool groundTouching;

    [Header("ȸ��")]
    [SerializeField] float rotSpeed;
    [SerializeField] float rotMul_magnitude;

    [Header("�ӵ�")]
    [SerializeField] float moveSpeed;
    [SerializeField] float moveMaxSpeed;

    [Header("ȸ���Է�")]
    [SerializeField] Vector2 joyStickInput;
    [SerializeField] float rotResult;

    [Header("�𵨸� Ű")]
    [SerializeField] float modelHeight;
    [SerializeField] float modelWidth;

    Vector3 rotDir;

    // Update is called once per frame
    void Update()
    {
        if (groundTouching)
        {
            //* ���̽�ƽ �Է�
            //���̽�ƽ �Է�, �Է¹���
            joyStickInput = new Vector2(joyStick.Horizontal, joyStick.Vertical);
            if (joyStickInput.x >= 0)
            {
                rotResult =
                    (-(ZBMath.GetAngleByVector2(joyStickInput) - 90) * joyStickInput.magnitude * rotMul_magnitude)
                    * Time.deltaTime * rotSpeed;
            }
            else
            {
                rotResult = ZBMath.GetAngleByVector2(joyStickInput);
                if (rotResult >= 0)
                {
                    rotResult = rotResult - 90;
                }
                else
                {
                    rotResult = rotResult + 270;
                }
                rotResult = ((-rotResult) * joyStickInput.magnitude * rotMul_magnitude) * Time.deltaTime * rotSpeed;
            }
            ZBMath.AddRotationY(ballMoveAssist, rotResult);
            rotDir = moveDir.transform.position - ballMoveAssist.transform.position;

            //* ���̵�
            rb.AddForce(rotDir.normalized * moveSpeed * joyStickInput.magnitude);

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
        }
        ballMoveAssist.transform.position = ballTf.transform.position;

    }

    public void OnGroundTouched(bool active)
    {
        groundTouching = active;
    }

    public void OnBallSizeUp(float size)
    {
        float radius = size / 2;
        ballMoveAssist_Col.radius = radius;
        ray.transform.localPosition = new Vector3(0, (-radius) + (modelHeight) - 0.1f, -75);

        //��ũ�Ⱑ ĳ���� �𵨸� ũ�� * 1.5f ���� �������
        if (radius < modelHeight * 1.5f)
        {
            modelPivot.localPosition = new Vector3(0, -radius, -radius - modelWidth / 2);
        }

        //��ũ�Ⱑ ĳ���� �𵨸� ũ�� * 1.5f ���� Ŭ���
        else
        {
            StartCoroutine(delayNRePos());
        }

        IEnumerator delayNRePos()
        {
            yield return null;
            modelPivot.position = ray.GetLastTouchedPosition()
                + (ray.GetLastTouchedPosition() - ballMoveAssist.transform.position).normalized * modelWidth * 0.5f
                + new Vector3(0, -radius, 0);
            modelPivot.position = ray.GetLastTouchedPosition()
                + new Vector3(((ray.GetLastTouchedPosition() - ballMoveAssist.transform.position).normalized * modelWidth * 0.5f).x, 0,
                ((ray.GetLastTouchedPosition() - ballMoveAssist.transform.position).normalized * modelWidth * 0.5f).z)
                + new Vector3(0, -modelHeight, 0);
        }
    }
}