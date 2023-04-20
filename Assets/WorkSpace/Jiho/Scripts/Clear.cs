using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ZB.Architecture;

namespace JH
{
    public class Clear : MonoBehaviour, IClearCheck, IPerfection
    {
        //ŷ��Ƽ �̺�Ʈ �Ἥ �ϴ� ������Ʈ�� �����
        [SerializeField] private UnityEvent OnCheckUpdate;
        [SerializeField] private UnityEvent OnCheckClear;
        [SerializeField] private ScoreCheck scoreCheck;

        [SerializeField] private bool isClear;

        private void Awake()
        {
            StartCoroutine(CheckUpdate());
        }

        public bool ClearCheck()
        {
            return scoreCheck.isClear;
        }

        public void OnEnterStage()
        {

        }

        public void OnExitStage()
        {
            Debug.LogError("EXIT");
            scoreCheck.isClear = false;
        }

        private IEnumerator CheckUpdate()
        {
            while(!ClearCheck())
            {
                OnCheckUpdate.Invoke();
                OnCheckClear.Invoke();
                yield return new WaitForFixedUpdate();
            }
            
        }

        public int Score()
        {
            return 0;
        }

    }

}
