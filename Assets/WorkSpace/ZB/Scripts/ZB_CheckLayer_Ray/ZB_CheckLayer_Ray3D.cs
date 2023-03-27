using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZB_CheckLayer_Ray3D : MonoBehaviour
{
    [SerializeField] Transform lastTouchedTransform;

    [SerializeField] Color rayColor;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Vector3 dir;           //����
    [SerializeField] float maxDistance;     //���� ��Ÿ�
    [SerializeField] bool drawRay;

    [SerializeField] UnityEvent enterEvent;
    [SerializeField] UnityEvent stayEvent;
    [SerializeField] UnityEvent exitEvent;

    [SerializeField] bool touching;
    [SerializeField] bool rayOnOff;

    [SerializeField] Vector3 lastTouchedPosition;

    RaycastHit hit;

    Vector3     original_Dir;
    float       original_MaxDistance;

    bool _firstCheck = true;

    //�������� ������ ������Ʈ�� Transform ��������
    public Transform GetLastTouchedTransform()
    {
        return lastTouchedTransform;
    }

    public Vector3 GetLastTouchedPosition()
    {
        return lastTouchedPosition;
    }

    //���˻��� Ȯ���ϱ�
    public bool GetTouching()
    {
        return touching;
    }

    //����ĳ��Ʈ�� ����ٲٱ� : ���⸸
    public void ChangeDir(Vector3 dir)
    {
        this.dir = dir.normalized;
    }

    //����ĳ��Ʈ�� ����ٲٱ� : ����, ����
    public void ChangeDir(Vector2 dir, float maxDistance)
    {
        this.dir = dir.normalized;
        this.maxDistance = maxDistance;
    }

    //���� ���� Ű�� / true: Ű�� / false: ����
    public void RaySwitch(bool rayOnOff)
    {
        this.rayOnOff = rayOnOff;
        if(!rayOnOff)
        {
            dir = original_Dir;
            maxDistance = original_MaxDistance;
            touching = false;
            _firstCheck = true;
        }
    }

    //���� (���� ���ʹ�������, ���� ���ʱ��̷�)
    public void ResetState()
    {
        dir = original_Dir;
        maxDistance = original_MaxDistance;
        touching = false;
        _firstCheck = true;
        rayOnOff = true;
    }

    private void Awake()
    {
        GetComponent<SpriteRenderer>().color = Color.clear;

        hit = new RaycastHit();
        original_Dir = dir;
        original_MaxDistance = maxDistance;
    }
    private void Update()
    {
        if (rayOnOff)
        {
            if(drawRay)
                Debug.DrawRay(transform.position, dir * maxDistance, rayColor, 0.3f);

            if (Physics.Raycast(transform.position, dir, out hit, maxDistance, layerMask))
            {
                lastTouchedPosition = hit.point;
                if (_firstCheck)
                {
                    lastTouchedTransform = hit.transform;
                    touching = true;
                    enterEvent.Invoke();
                    _firstCheck = false;
                }
                else
                {
                    stayEvent.Invoke();
                }
            }
            else
            {
                if (!_firstCheck)
                {
                    touching = false;
                    exitEvent.Invoke();
                    _firstCheck = true;
                    lastTouchedTransform = null;
                }
            }
        }
    }
    void OnDisable()
    {
        ResetState();
    }
}
