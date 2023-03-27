using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBall_Temp : MonoBehaviour, IStickyBall
{
    public float BallSize()
    {
        return transform.localScale.z;
    }

    public void Roll(float speed)
    {
        throw new System.NotImplementedException();
    }
}
