using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JH
{
    public class Attach : MonoBehaviour
    {
        [SerializeField] private int index;
        [SerializeField] private float size;

        public int Index { get => index; set => index = value; }
        public float Size { get => size; set => size = value; }
    }
}

