using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckLayer3D : MonoBehaviour
{
    public Transform LastTouchedTransform { get { return lastTouchedTransform; } }

    [SerializeField] UnityEvent onEnter_Col;
    [SerializeField] UnityEvent onStay_Col;
    [SerializeField] UnityEvent onExiit_Col;

    [SerializeField] int targetLayer;

    [SerializeField] Transform lastTouchedTransform;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == targetLayer)
        {
            onEnter_Col.Invoke();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == targetLayer)
        {
            onStay_Col.Invoke();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == targetLayer)
        {
            onExiit_Col.Invoke();
        }
    }
}
