using JH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using ZB.Architecture;

public class ObjectHandler : MonoBehaviour, IHandlers
{
    BallState ballState;
    ObjectSpawner spawner;
    ObjectPool objectPool;

    private void Awake()
    {
        ballState = FindObjectOfType<BallState>();
        spawner = FindObjectOfType<ObjectSpawner>();
        objectPool = FindObjectOfType<ObjectPool>();
        objectPool.ObjectPoolState();
    }

    public void OnEnterStage()
    {
        spawner.SpawnerInit();
        ballState.tempSize = 0;
    }

    public void OnExitStage()
    {
        ballState.CurrentScore = 0;
        //ballState.BallSizeUp((ballState.tempSize) * -1);

        spawner.ReturnObject();
    }
}
