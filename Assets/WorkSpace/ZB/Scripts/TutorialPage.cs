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
            target.DOScale(Vector3.zero, 0.6f).SetEase(Ease.InBack).OnComplete(() => target.gameObject.SetActive(false));
        }
    }
}