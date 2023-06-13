using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ZB
{
    public class TutorialPage : MonoBehaviour
    {
        [SerializeField] Transform target;

        public void OnBtnClicked()
        {
            target.DOScale(Vector3.zero, 1).SetEase(Ease.InQuart).OnComplete(() => target.gameObject.SetActive(false));
        }
    }
}