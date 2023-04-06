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

        [SerializeField] private bool isClear;

        private void Awake()
        {
            StartCoroutine(CheckUpdate());
        }

        public bool ClearCheck()
        {
            return isClear;
        }

        public void OnEnterStage()
        {
            isClear = false;
        }

        public void OnExitStage()
        {
            isClear = true;
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
