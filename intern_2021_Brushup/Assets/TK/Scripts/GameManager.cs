using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TK
{
    public class GameManager : MonoBehaviour
    {
        public float _timeCount { get; set; }

        public bool _gameEnd=false;
        //Start is called before the first frame update
        private void Start()
        {
            //初期
            _timeCount = 62.0f;
        }

        // Update is called once per frame
        private void Update()
        {
            if (!IsGameEnd())
            {
                if (_timeCount <= 0.0f)
                {
                    _timeCount = 0.0f;
                    GameStop();
                }
                else
                {
                    _timeCount -= Time.deltaTime;
                }
            }
        }

        public void AddTime(float time)
        {
            if (!IsGameEnd())
            {
                _timeCount += time;
            }
        }

        public void GameStop()
        {
            _gameEnd = true;
        }

        public bool IsGameEnd()
        {
            return _gameEnd;
        }
    }
}
