using JH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using ZB;
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
        Managers.instance.SoundMaster.Play(Managers.instance.SoundMaster.audioDictionary[SceneManager.GetActiveScene().name], true);
    }

    public void OnExitStage()
    {
        ballState.CurrentScore = 0;
        //ballState.BallSizeUp((ballState.tempSize) * -1);

        spawner.ReturnObject();
    }
}
