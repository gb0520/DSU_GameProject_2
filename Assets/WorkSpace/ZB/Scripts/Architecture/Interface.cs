using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZB.Architecture;

namespace ZB.Architecture
{
    interface IClearCheck
    {
        bool ClearCheck();
        void OnEnterStage();
        void OnExitStage();
    }

    interface IOverCheck
    {
        bool OverCheck();
        void OnEnterStage();
        void OnExitStage();
    }

    interface IHandlers
    {
        void OnEnterStage();
        void OnExitStage();
    }

    interface IPerfection
    {
        int Score();
    }

    interface ITimer
    {
        int nowTime();
        int timeLimit();
    }
}