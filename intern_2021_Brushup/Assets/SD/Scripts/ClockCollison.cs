/*--------------------------------------------------------
 * 
 *      [ClockCollsion.cs]
 *      時計塔の処理
 *      Author : 出合翔太
 * 
 --------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class ClockCollison : MonoBehaviour
    {
        private GameObject _gameManager;
        private SE _se; 
        // 加算する時間
        private float _addTime;     

        // Start is called before the first frame update
        void Start()
        {
            _gameManager = GameObject.Find("GameManager");
            _se = GameObject.FindWithTag("Manager_SEManager").GetComponent<SE>();

            _addTime = 10; // 10秒
        }

        // Update is called once per frame
        void Update()
        {

            
        }

        private void FixedUpdate()
        {
            
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                // 時間を加算する
                _gameManager.GetComponent<TK.GameManager>().AddTime(_addTime);
                if (_se != null)
                {
                    // SEを鳴らす
                    _se.Play(6);
                }
                // コライダーを削除
                Destroy(this.gameObject);
            }
        }
    }
}