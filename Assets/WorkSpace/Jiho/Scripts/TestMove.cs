using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JH
{
    public class TestMove : MonoBehaviour
    {
        [SerializeField] private VariableJoystick joy;
        [SerializeField] private float speed;

        private float moveVecX;
        private float moveVecZ;
        private Vector3 moveVec;

        private Rigidbody rigid;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }
        
        private void Update()
        {
            MoveVecter();
        }

        private void MoveVecter()
        {

            moveVecX = joy.Horizontal;
            moveVecZ = joy.Vertical;

            moveVec = new Vector3(moveVecX, 0, moveVecZ) * speed * Time.deltaTime;

            rigid.MovePosition(rigid.position + moveVec);

            if (moveVec.sqrMagnitude == 0)
                return;

            Quaternion dir = Quaternion.LookRotation(moveVec);
            Quaternion move = Quaternion.Slerp(rigid.rotation, dir, 0.3f);
            rigid.MoveRotation(move);
            Debug.Log(moveVecX);
            Debug.Log(moveVecZ);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "d")
            {
                int temp = collision.gameObject.GetComponent<Attach>().Index;
                ObjectSpawner.ObjReturn(collision.gameObject, temp);
            }
        }
    }
}

