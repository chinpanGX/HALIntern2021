﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WE
{
	public class BuildingScript : MonoBehaviour
	{
		[SerializeField] private uint _health = 0;  //耐久値
		[SerializeField] private uint _peopleNumber = 1;  //得られる人数
		[SerializeField] private GameObject _peoplePrefab;  // 人のプレハブ
		[SerializeField] private float _peopleGenerationRange;  // 人の生成範囲の半径

		// 生成するエフェクト
		[SerializeField] private GameObject _effect = null;
		private GameObject _gameObject;

		// ビーコン
		[SerializeField] private GameObject _beacon = null;

		private TK.GameManager _gm;
		private TM.Player _player;  //プレイヤーのスクリプト
		private Rigidbody _rb;
		public bool _isBreak;  //壊れたかフラグ
		private bool _peopleGenerated = false;

		private void OnCollisionEnter(Collision other)
		{
			if(other.gameObject.tag == "Player")
			{
				if (_player.CrewCount > _health)
				{
					gameObject.layer = 9;  //レイヤーを9番にしてプレイヤーと当たらないようにする

					// 人を生成
					if (!_peopleGenerated)
					{
						//for (int i = 0; i < _peopleNumber; ++i)
						//{
						Instantiate(_peoplePrefab, transform.position + Random.insideUnitSphere * _peopleGenerationRange, Quaternion.identity);
						//}
						if (!_gm.IsGameEnd())
						{
							_player.CrewCount += _peopleNumber;  //乗客数を増加
						}
					}
					_peopleGenerated = true;

                    _isBreak = true;
				}
			}
		}

		// Start is called before the first frame update
		private void Start()
		{
			// スクリプトの参照
			_gm = GameObject.Find("GameManager").GetComponent<TK.GameManager>();
			var playerObj = GameObject.Find("Player");
			if (playerObj == null)
            {
				return;
            }

			if(_effect != null)
            {
				_gameObject = Instantiate(_effect, new Vector3(this.transform.position.x, 0.0f, this.transform.position.z), Quaternion.identity);
			}

			_player = playerObj.GetComponent<TM.Player>();
			_rb = GetComponent<Rigidbody>();
			_rb.isKinematic = true;
			_isBreak = false;
		}

		// Update is called once per frame
		private void Update()
		{

		}

		private void FixedUpdate()
		{
			if (gameObject.layer == 9)
			{
				Destroy(this.gameObject, 3);  //3秒後に消す
			}

			if (_player.CrewCount > _health)  //建物の耐久値を超えたらキネマティックを外す
			{
				_rb.isKinematic = false;
				if(_effect != null)
                {
					Destroy(_gameObject);
					Destroy(_beacon);
                }
			}
		}
	}
}