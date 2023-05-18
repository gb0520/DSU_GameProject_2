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
        //��ġ�ʱ�ȭ
        m_ballMove.SetPlayerPosition(resetPosition.position);

        //�������� Ÿ��ī��Ʈ ��ȣ�ֵ���
        m_ballMove.OnPlayerEnterReset();

        //���̽�ƽ ��ȣ�ۿ� �������
        m_ballMove.ControlActive(true);
    }

    public void OnExitStage()
    {
        //���̽�ƽ ��ȣ�ۿ� ���
        m_ballMove.ControlActive(false);
    }

    private void Awake()
    {
        m_ballMove = FindObjectOfType<BallMove>();
    }
}
