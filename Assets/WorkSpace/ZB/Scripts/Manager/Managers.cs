using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZB;
using JH;

namespace ZB
{
    public class Managers : MonoBehaviour
    {
        public static Managers instance;

        public ScreenSwapVisualizer ScreenSwap { get => screenSwapVisualizer; }
        public SoundMaster SoundMaster { get => soundMaster; }

        [SerializeField] private ScreenSwapVisualizer screenSwapVisualizer;
        [SerializeField] private SoundMaster soundMaster;



        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);
        }
    }
}