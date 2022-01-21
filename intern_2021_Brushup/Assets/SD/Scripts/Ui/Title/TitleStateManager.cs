/*-------------------------------------------------------
 * 
 *      [TitleStateManager.cs]
 *      称号のステート
 *      Author : 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SD
{
    public class TitleStateManager : MonoBehaviour
    {
        [SerializeField] private Text _text;
        private GameObject _player;
        private TitleStateBase _state;

        // 表示位置
        private Vector3 _defualtPosition; // デフォルトの位置
        private Vector3 _center; // 画面中央

        // タイムライン
        private float _time;
        private float _diff;
        private bool _isMoving; // 移動中かどうか

        private float _wait;
        private SE _se;

        public int TitleRank { get; private set; } // 現在の階級番号

        // Start is called before the first frame update
        void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _se = GameObject.FindWithTag("Manager_SEManager").GetComponent<SE>();

            // タイムライン
            _isMoving = false;
            _time = 1.0f;

            // 位置の初期化
            _defualtPosition = _text.transform.position;
            _center = new Vector3(Screen.width / 2, (Screen.height / 2) * 1.36f, 0);

            // テキスト
            _state = new TitleState1st();
            _state.Begin();
            _text.text = _state.GetTitle();

            // 階級番号の初期化
            TitleRank = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            // テキストを移動させる
            if (_isMoving)
            {
                _wait += Time.deltaTime;
                if (_wait > 2.0)
                {
                    _diff += Time.deltaTime;
                    float rate = _diff / _time;
                    // 移動 
                    MovingPosition(rate);
                    if (_diff > _time)
                    {
                        _isMoving = false;
                        _text.transform.position = _defualtPosition;
                        _diff = 0;
                        _wait = 0;
                    }
                }
            }
            // ステートとテキストの更新
            _state.Tick(this, _player);             
        }

        // 称号を変える
        public void ChangeTitle(TitleStateBase nextState)
        {            
            // ステートを切り替える
            _state = nextState;
            _state.Begin();

            // テキストを更新
            _text.text = _state.GetTitle();

            // テキストを中心に移動
            MoveAroundTxet();
            _isMoving = true;

            _se.Play(2);

            ++TitleRank;
        }

        // テキストを中心に移動
        private void MoveAroundTxet()
        {
            _text.transform.position = _center;
            _text.transform.localScale = new Vector3(1.1f, 1.1f, 1.3f);
        }

        // デフォルトの位置に移動
        private void MovingPosition(float rate)
        {
            _text.transform.position = Vector3.Lerp(_center, _defualtPosition, rate);
            _text.transform.localScale = Vector3.Lerp(new Vector3(1.1f, 1.1f, 1), new Vector3(0.3f, 0.3f, 1), rate);
        }
    }
}
