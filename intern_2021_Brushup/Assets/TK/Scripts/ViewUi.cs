using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TK
{
    public class ViewUi : MonoBehaviour
    {

        private bool _isViewing = false;

        private bool _isSelected = false;
        [SerializeField]
        private string _message;

        public void ShowWindow()
        {
            _isViewing = true;
            var target = GetComponent<Text>();
            target.DOText(_message, 1.0f);
        }

        public void InstantShow()
        {
            _isViewing = true;
            var target = GetComponent<Text>();
            target.text = _message;
        }

        public void SetMessage(string s)
        {
            _message = s;
        }

        [ContextMenu("HideMessage")]
        public void HideMessage()
        {
            if (_isViewing)
            {
                var target = GetComponent<Text>();
                target.DOText("          ", 0.2f, false, ScrambleMode.Custom, _message);
                _isViewing = false;
            }
        }
        [ContextMenu("ShowTest")]
        public void ShowTest()
        {
            _message = "TESTTEST";
            var target = GetComponent<Text>();
            target.DOText(_message, 1.0f);
        }

    }
}
