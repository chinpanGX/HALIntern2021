﻿/*-------------------------------------------------------
 * 
 *      [MeteoSpawn.cs]
 *      隕石のスポーン
 *      Author : 出合翔太
 * 
 --------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class MeteoSpawn : MonoBehaviour
    {
        // スポーンさせるオブジェクト
        [Header("スポーンさせるオブジェクト")]
        [SerializeField] private GameObject _meteo;
        private GameObject _player;

        // Start is called before the first frame update
        void Start()
        {
            _player = GameObject.Find("Player");
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            var playerNullCheck = _player?.activeSelf;

            // プレイヤーの方向を見る
            this.transform.LookAt(_player.transform);
        }

        public void Spawn()
        {
            // インスタンスの生成
            GameObject meteo = Instantiate(_meteo, this.transform);

            // 方向の補正
            Vector3 random = new Vector3();
            random.x = Random.Range(-0.1f, 0.1f);
            random.y = 0.0f;
            random.z = Random.Range(-0.1f, 0.1f);

            // 隕石を飛ばす方向を決める
            Vector3 dir = meteo.transform.forward + random;

            // 力を加えて、飛ばす
            meteo.GetComponent<Rigidbody>().AddForce(dir * 5000.0f);
        }
    }
}