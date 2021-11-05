﻿/*-------------------------------------------------------
 * 
 *      [CameraLook.cs]
 *      カメラが見る位置
 *      Author : 出合翔太
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class CameraLook : MonoBehaviour
    {
        // プレイヤーの設定
        [Header("プレイヤーのオブジェクト")]
        [SerializeField] private GameObject _player;

        private Transform _playerTransform;
        private Vector3 _forward;
        private Vector3 _position;

        private float _LookHeight;  // 高さ
        private float _LookForward; // 前方向 

        // Start is called before the first frame update
        void Start()
        {
            // 位置を初期化
            _playerTransform = _player.transform;
            _forward = Vector3.forward;
            _LookHeight = 1.0f;
            _LookForward = 6.0f;
            this.transform.position = new Vector3(0.0f, 3.0f + _LookHeight, 0.0f);     
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void FixedUpdate()
        {
            // プレイヤーのnullチェック
            var nullCheck = _player?.activeInHierarchy; 
            
            // 進む方向
            _forward = _player.GetComponent<TM.PlayerController>().CameraForward;

            // 位置を更新
            _position = _playerTransform.position; // プレイヤーの位置
            this.transform.position = new Vector3(_position.x, _position.y + _LookHeight, _playerTransform.position.z);

            // このオブジェクトの前ベクトルが進む方向を向くように回転させる
            Quaternion quaternion = Quaternion.identity;
            // 前ベクトルが更新されたら、回転する
            if (_forward != Vector3.zero)
            {
                quaternion = Quaternion.LookRotation(_forward);
            }
            // 線形補間を使った回転
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, quaternion, 1.0f);
        }
    }
}
