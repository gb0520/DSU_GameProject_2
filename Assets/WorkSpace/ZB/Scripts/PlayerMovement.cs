using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using ZB;

namespace ZB
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] ZB_CheckLayer_Ray3D ray;

        //구체
        [SerializeField] Transform iballTarget_TF;
        IStickyBall iballTarget;

        //이동 (물리)
        [SerializeField] FloatingJoystick joyStick;
        [SerializeField] Transform lookAt;
        [SerializeField] Transform moveTargetTf;
        [SerializeField] Rigidbody rbTarget;

        [Header("커지는 구체")]
        [SerializeField] Transform sphereRB_TF;
        [SerializeField] Transform sphereModel_TF;

        [Header("회전대상")]
        [SerializeField] Transform rotTarget_TF;

        [Space]
        [Header("이동")]
        [SerializeField] float moveSpeed;
        [SerializeField] float moveMaxSpeed;
        [Header("회전")]
        [SerializeField] float rotSpeed;
        [SerializeField] float rotMul_magnitude;
        [Space]
        [SerializeField] Vector2 joyStickInput;
        [SerializeField] Vector3 nowVelocity;

        [Space]
        [SerializeField] Transform playerBody;

        float rotResult;

        //공크기에 따른 위치조정
        [Space]
        [SerializeField] float minBallDist;
        [SerializeField] Vector3 ballDist_Result;       //아래 값들을 모두 더한 값
        [SerializeField] Vector3 ballDist_BaseOffset;
        [SerializeField] Vector3 ballDist_BySize;
        [SerializeField] Vector3 ballDist_BySlop;

        private void Awake()
        {
            iballTarget_TF.TryGetComponent(out iballTarget);
        }
        
        private void Update()
        {
            //조이스틱 입력, 입력반응
            joyStickInput = new Vector2(joyStick.Horizontal, joyStick.Vertical);
            
            //rotResult에 회전해야하는 값 전달
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

            //회전시켜야할 대상 회전시킴
            ZBMath.AddRotationY(rotTarget_TF, rotResult);

            //힘
            Vector3 movePower = (lookAt.position - moveTargetTf.position) * joyStickInput.magnitude * moveSpeed;
            //rbTarget.velocity = new Vector3(movePower.x, rbTarget.velocity.y, movePower.z);
            rbTarget.AddForce(new Vector3(movePower.x, rbTarget.velocity.y, movePower.z));

            //최대속도 제한
            {
                if (rbTarget.velocity.x > moveMaxSpeed)
                {
                    rbTarget.velocity = new Vector3(moveMaxSpeed, rbTarget.velocity.y, rbTarget.velocity.z);
                }
                else if (rbTarget.velocity.x < -moveMaxSpeed)
                {
                    rbTarget.velocity = new Vector3(-moveMaxSpeed, rbTarget.velocity.y, rbTarget.velocity.z);
                }

                if (rbTarget.velocity.z > moveMaxSpeed)
                {
                    rbTarget.velocity = new Vector3(rbTarget.velocity.x, rbTarget.velocity.y, moveMaxSpeed);
                }
                else if (rbTarget.velocity.z < -moveMaxSpeed)
                {
                    rbTarget.velocity = new Vector3(rbTarget.velocity.x, rbTarget.velocity.y, -moveMaxSpeed);
                }
            }
            nowVelocity = rbTarget.velocity;

            sphereModel_TF.transform.position = sphereRB_TF.transform.position;

            ray.transform.position = sphereRB_TF.position + (sphereRB_TF.position - lookAt.position).normalized * 30;
            ray.transform.position = new Vector3(ray.transform.position.x,
                iballTarget_TF.position.y - iballTarget.BallSize() * 0.5f + 1.5f, ray.transform.position.z);

            Vector3 rayDir = (sphereRB_TF.position - ray.transform.position);
            rayDir = new Vector3(rayDir.x, 0, rayDir.z);
            ray.ChangeDir(rayDir);

            if (Input.GetKeyDown(KeyCode.J))
            {
                sphereModel_TF.localScale += Vector3.one;
                sphereRB_TF.localScale += Vector3.one;
            }

            ControlPlayerPos(playerBody, iballTarget.BallSize());
        }
        void ControlPlayerPos(Transform tfTarget, float size)
        {
            //Vector3 offset = ray.GetLastTouchedPosition() - iballTarget_TF.position;
            //offset = new Vector3(offset.x, ray.GetLastTouchedPosition().y - 2, offset.z);
            //ballDist_BySize = offset;

            tfTarget.position = ray.GetLastTouchedPosition();
        }
    }
}