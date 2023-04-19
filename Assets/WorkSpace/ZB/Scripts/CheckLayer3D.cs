using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckLayer3D : MonoBehaviour
{
    public Transform LastTouchedTransform { get { return lastTouchedTransform; } }

    public bool Touching { get => touching; }

    [SerializeField] UnityEvent onEnter_Col;
    [SerializeField] UnityEvent onStay_Col;
    [SerializeField] UnityEvent onExiit_Col;
    [SerializeField] UnityEvent onEnter_Trg;
    [SerializeField] UnityEvent onStay_Trg;
    [SerializeField] UnityEvent onExiit_Trg;

    [SerializeField] int targetLayer;

    [SerializeField] Transform lastTouchedTransform;

    [SerializeField] bool touching;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == targetLayer)
        {
            touching = true;
            lastTouchedTransform = collision.transform;
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
            touching = false;
            onExiit_Col.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == targetLayer)
        {
            touching = true;
            lastTouchedTransform = other.transform;
            onEnter_Trg.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == targetLayer)
        {
            onStay_Trg.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == targetLayer)
        {
            touching = false;
            onExiit_Trg.Invoke();
        }
    }
}
