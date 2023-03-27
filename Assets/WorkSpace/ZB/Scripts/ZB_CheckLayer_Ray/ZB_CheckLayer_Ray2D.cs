using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZB_CheckLayer_Ray2D : MonoBehaviour
{
    [SerializeField] Color rayColor;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Vector2 dir;           //����
    [SerializeField] float maxDistance;     //���� ��Ÿ�

    [SerializeField] UnityEvent enterEvent;
    [SerializeField] UnityEvent stayEvent;
    [SerializeField] UnityEvent exitEvent;

    [SerializeField] bool touching;

    [SerializeField] bool rayOnOff;

    RaycastHit2D hit;

    Transform   lastTouchedTransform;

    Vector2     original_Dir;
    float       original_MaxDistance;

    bool _firstCheck = true;

    ////////////////////////////////////////////////////////////////////////

    //�������� ������ ������Ʈ�� Transform ��������
    public Transform GetLastTouchedTransform()
    {
        return lastTouchedTransform;
    }

    //���˻��� Ȯ���ϱ�
    public bool GetTouching()
    {
        return touching;
    }

    ////////////////////////////////////////////////////////////////////////

    //����ĳ��Ʈ�� ����ٲٱ� : ���⸸
    public void ChangeDir(Vector2 dir)
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

    ////////////////////////////////////////////////////////////////////////

    private void Awake()
    {
        GetComponent<SpriteRenderer>().color = Color.clear;

        hit = new RaycastHit2D();
        original_Dir = dir;
        original_MaxDistance = maxDistance;
    }
    private void Update()
    {
        if (rayOnOff)
        {
            Debug.DrawRay(transform.position, dir * maxDistance, rayColor, 0.3f);
            hit = Physics2D.Raycast(transform.position, dir, maxDistance, layerMask);

            if (hit)
            {
                if (_firstCheck)
                {
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
                }
            }
        }
    }

    void OnEnable()
    {
        ResetState();
    }
    void OnDisable()
    {
        ResetState();
    }
}
