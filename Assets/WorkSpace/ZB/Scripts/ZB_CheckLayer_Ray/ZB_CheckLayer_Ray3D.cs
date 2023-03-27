using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZB_CheckLayer_Ray3D : MonoBehaviour
{
    [SerializeField] Transform lastTouchedTransform;

    [SerializeField] Color rayColor;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Vector3 dir;           //방향
    [SerializeField] float maxDistance;     //레이 사거리
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

    //마지막을 접촉한 오브젝트의 Transform 가져오기
    public Transform GetLastTouchedTransform()
    {
        return lastTouchedTransform;
    }

    public Vector3 GetLastTouchedPosition()
    {
        return lastTouchedPosition;
    }

    //접촉상태 확인하기
    public bool GetTouching()
    {
        return touching;
    }

    //레이캐스트의 방향바꾸기 : 방향만
    public void ChangeDir(Vector3 dir)
    {
        this.dir = dir.normalized;
    }

    //레이캐스트의 방향바꾸기 : 방향, 길이
    public void ChangeDir(Vector2 dir, float maxDistance)
    {
        this.dir = dir.normalized;
        this.maxDistance = maxDistance;
    }

    //레이 끄고 키기 / true: 키기 / false: 끄기
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

    //리셋 (방향 최초방향으로, 길이 최초길이로)
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
