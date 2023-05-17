using JH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ZB;
using ZB.Architecture;

public class BallMove : MonoBehaviour
{
    [SerializeField] ZB.Drag.FocusLook focusLook;
    [SerializeField] Joystick joyStick;
    [SerializeField] Rigidbody rb;

    [SerializeField] Transform ballTf;
    [SerializeField] Transform ballMoveAssist;
    [SerializeField] Transform moveDir;
    [SerializeField] Transform joyStickDir;
    [SerializeField] Transform modelPivot;
    [SerializeField] Transform ballAttatchRbTf;
    [SerializeField] Transform cameraPoint;
    [SerializeField] SphereCollider ballMoveAssist_Col;
    [SerializeField] ZB_CheckLayer_Ray3D ray;

    [Header("이동가능여부")]
    [SerializeField] bool controlActive;
    [SerializeField] bool groundTouching;

    [Header("회전")]
    [SerializeField] float rotSpeed;

    [Header("속도")]
    [SerializeField] float moveSpeed;
    [SerializeField] float moveMaxSpeed;
    [SerializeField] float breakPower;
    bool breaked;

    [Header("회전입력")]
    [SerializeField] Vector2 joyStickInput;
    [SerializeField] float rotResult;

    [Header("모델링 키")]
    [SerializeField] float modelHeight;
    [SerializeField] float modelWidth;

    Vector3 nowLookDir { get => moveDir.position - transform.position; }
    Vector3 rotDir;

    public Vector2 JoyStickInput { get => joyStickInput; }

    [Header("땅 밟으며 타임카운트 시작")]
    [SerializeField] bool m_timeCountStartFocusing;
    UnityEvent m_uEvent_TimeCountStart;

    private void Update()
    {
        if (controlActive)
        {
            //* 조이스틱 입력
            //조이스틱 입력, 입력반응
            joyStickInput = new Vector2(joyStick.Horizontal, joyStick.Vertical);

            if (joyStickInput != Vector2.zero)
            {
                breaked = false;

                float angle_stick_focus = Vector2.SignedAngle(joyStickInput, Vector2.up);
                float radian = (angle_stick_focus + focusLook.m_YAxisAngle) * Mathf.Deg2Rad;
                Vector2 direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
                joyStickDir.position = ballTf.transform.position + new Vector3(direction.y, 0, direction.x);

                rotResult = Vector3.SignedAngle(moveDir.localPosition.normalized, joyStickDir.localPosition.normalized, Vector3.up);

                ZBMath.AddRotationY(ballMoveAssist, rotResult * rotSpeed * 0.1f);
                rotDir = moveDir.transform.position - ballMoveAssist.transform.position;
            }

            if (groundTouching)
            {
                //* 공이동
                rb.AddForce(rotDir.normalized * moveSpeed * joyStickInput.magnitude);

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

            //브레이크
            if (groundTouching && joyStickInput == Vector2.zero && !breaked)
            {
                breaked = true;
                rb.velocity *= breakPower;
            }

            ballMoveAssist.transform.position = ballTf.transform.position;
            ray.ChangeDir(moveDir.transform.position - ballMoveAssist.transform.position);
        }
    }
    private void Awake()
    {
        m_uEvent_TimeCountStart = new UnityEvent();
        try
        {
            m_uEvent_TimeCountStart.AddListener(FindObjectOfType<OverChecker>().TimeCountStart);
        }
        catch { }
    }

    public void OnGroundTouched(bool active)
    {
        if (m_timeCountStartFocusing)
        {
            m_timeCountStartFocusing = false;
            m_uEvent_TimeCountStart.Invoke();
        }

        groundTouching = active;
    }
    public void OnBallSizeUp(float size)
    {
        float radius = (ballAttatchRbTf.localScale.x + size) / 2;
        ballMoveAssist_Col.radius = radius;
        ray.transform.localPosition = new Vector3(0, (-radius) + (modelHeight) - 0.1f, -75);
        ballAttatchRbTf.localScale = new Vector3(radius * 2, radius * 2, radius * 2);
        cameraPoint.localPosition = new Vector3(0, radius * radius * 0.2f, 0);

        ballTf.localScale = ballAttatchRbTf.localScale;

        //공크기가 캐릭터 모델링 크기 * 1.5f 보다 작을경우
        if (radius < modelHeight * 1.5f)
        {
            modelPivot.localPosition = new Vector3(0, -radius, -radius - modelWidth / 2);
        }

        //공크기가 캐릭터 모델링 크기 * 1.5f 보다 클경우
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

    public void SetPlayerPosition(Vector3 position)
    {
        ballTf.position = position;
    }
    //게임시작 시 초기화 (땅밟으면 타임카운트 시작 가능하도록한다.)
    public void OnPlayerEnterReset()
    {
        StartCoroutine(delay_N_timeCountStartFocus());
    }
    //조이스틱으로 상호작용 On Off
    public void ControlActive(bool active)
    {
        controlActive = active;
    }


    IEnumerator delay_N_timeCountStartFocus()
    {
        yield return null;
        m_timeCountStartFocusing = true;
    }
}