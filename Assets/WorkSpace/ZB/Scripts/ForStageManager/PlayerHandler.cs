using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ZB.Architecture;

public class PlayerHandler : MonoBehaviour, IHandlers
{
    BallMove m_ballMove;
    [SerializeField] Transform resetPosition;

    public void OnEnterStage()
    {
        //위치초기화
        m_ballMove.SetPlayerPosition(resetPosition.position);

        //땅밟으면 타임카운트 신호주도록
        m_ballMove.OnPlayerEnterReset();

        //조이스틱 상호작용 잠금해제
        m_ballMove.ControlActive(true);
    }

    public void OnExitStage()
    {
        //조이스틱 상호작용 잠금
        m_ballMove.ControlActive(false);
    }

    private void Awake()
    {
        m_ballMove = FindObjectOfType<BallMove>();
    }
}
