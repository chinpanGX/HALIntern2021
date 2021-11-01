﻿/*-------------------------------------------------------
 * 
 *      [Snow.cs]
 *      ゲームイベント：大雪
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class Snow : EventBase
    {
        private GameObject _playerCollider; // プレイヤー
        private GameObject _ui;
        private SnowSpawn _spawn;
        private float _timeLimit;

        public override void Begin()
        {
            _spawn = GameObject.Find("SnowSpawn").GetComponent<SnowSpawn>(); ;
            // イベント発生Ui
            _ui = GameObject.Find("Snow");            

            _playerCollider = GameObject.Find("Player/Collider");
            // 物理マテリアルの切り替える
            _playerCollider.GetComponent<PhysicMatList>().Change(2);

            _nowTime = 0;
            _timeLimit = Random.Range(5, 20);

            // サウンド再生
            SE _se = GameObject.Find("SEManager").GetComponent<SE>();
            _se.Play(0);
        }

        public override void Tick(GameEvent gameEvent)
        {            
            _nowTime += Time.deltaTime;
            _spawn.Spawn();
            if (_nowTime <= 3)
            {
                _ui.GetComponent<EventUi>().Blink();
            }
            else
            {
                _ui.GetComponent<EventUi>().End();
            }
            if (_nowTime > _timeLimit)
            {
                _nowTime = 0;
                gameEvent.ChangeEvent(new EventNone());
            }            
        }

        public override void End(GameEvent gameEvent)
        {
            _ui.GetComponent<EventUi>().End();
            gameEvent.ChangeEvent(new EventNone()); // イベント終了
            _nowTime = 0;
        }
    }
}