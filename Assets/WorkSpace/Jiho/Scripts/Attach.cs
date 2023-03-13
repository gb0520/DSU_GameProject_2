using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JH
{
    public class Attach : MonoBehaviour
    {
        [SerializeField] private int index;
        public int Index { get => index; set => index = value; }
    }
}

