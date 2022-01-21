using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TK
{
    public class SelectUi : MonoBehaviour
    {
        [SerializeField]
        private bool isSelected = false;

        private void Start()
        {
            if (!isSelected)
            {
                var target = GetComponent<Text>();
                target.transform.DORestart();
                target.transform.DOPause();
                isSelected = false;
                target.DOColor(Color.gray, 0.5f);
            }
            else
            {
                var target = GetComponent<Text>();
                transform.DOScale(0.2f, 0.5f)
       .SetRelative(true)
       .SetEase(Ease.OutQuart)
       .SetLoops(-1, LoopType.Yoyo);
                isSelected = true;
                target.DOColor(Color.white, 0.5f);
            }
        }

        private void FixedUpdate()
        {
            if (isSelected)
            {
                
            }
            return;
        }
        public void SetSelected()
        {
            if (!isSelected)
            {
                var target = GetComponent<Text>();
                transform.DOScale(0.2f, 0.5f)
       .SetRelative(true)
       .SetEase(Ease.OutQuart)
       .SetLoops(-1, LoopType.Yoyo);
                isSelected = true;
                target.DOColor(Color.white, 0.5f);
            }
        }

        public void SetUnSelected()
        {
            if (isSelected)
            {
                var target = GetComponent<Text>();
                target.transform.DORestart();
                target.transform.DOPause();
                isSelected = false;
                target.DOColor(Color.gray, 0.5f);
            }
        }

    }
}


