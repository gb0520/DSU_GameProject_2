using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZB;
using ZB.Architecture;

namespace JH
{
    public class Clear : MonoBehaviour, IClearCheck, IPerfection
    {
        

        public bool ClearCheck()
        {
            return true;
        }

        public void OnEnterStage()
        {
            
        }

        public void OnExitStage()
        {
            
        }

        public int Score()
        {
            return 0;
        }
    }
}

