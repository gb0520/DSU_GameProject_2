using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private BallMove ball;

    private void Awake()
    {
        ball = GetComponent<BallMove>();
    }

    private void Update()
    {
        AnimChange();
    }

    public void AnimChange()
    {
        if (ball.JoyStickInput != Vector2.zero)
        {
            if(!animator.GetBool("isMove")) animator.SetBool("isMove", true);
            
        }
        else if (animator.GetBool("isMove")) animator.SetBool("isMove", false);
    }

}
