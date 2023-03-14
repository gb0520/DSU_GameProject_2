using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZB;

namespace ZB
{
    public class ZBMath
    {
        public static float GetAngleByVector2(Vector2 vector2)
        {
            return Mathf.Atan2(vector2.y, vector2.x) * Mathf.Rad2Deg;
        }


        public static void AddRotationY(Transform targetTf, float y)
        {
            targetTf.rotation = Quaternion.Euler(0, targetTf.eulerAngles.y + y, 0);
        }
    }
}