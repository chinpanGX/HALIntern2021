/*-------------------------------------------------------
 * 
 *      [MiniMapCamera.cs]
 *      ミニマップ用のカメラの制御
 *      Author : deaishota
 * 
 -------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SD
{
    public class MiniMapCamera : MonoBehaviour
    {
        [SerializeField] private GameObject _dirBeaconObject;
        private TK.GameManager _gameManager;
        private GameObject _nearBeaconObject; // 近いビーコン
        private List<GameObject> _beacons = new List<GameObject>();
        private GameObject _player;
        private Vector3 _playerPosition;
        const float _offsetHeight = 40.0f;

        // Start is called before the first frame update
        void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _gameManager = GameObject.Find("GameManager").GetComponent<TK.GameManager>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        
        private void FixedUpdate()
        {
            // カメラの位置をプレイヤーの位置に合わせる
            _playerPosition = _player.transform.position;

            // 近いビーコンの位置を取得
            var nearBeaconPosition = SerchBeaconNearPosition();

            _dirBeaconObject.transform.position = _playerPosition;
            _dirBeaconObject.transform.LookAt(nearBeaconPosition, Vector3.up);

            // y座標を補正する
            _playerPosition.y += _offsetHeight;

            // 位置を更新
            this.gameObject.transform.position = _playerPosition;

            // ゲーム終了で非アクティブにする
            if(_gameManager.IsGameEnd())
            {
                this.gameObject.SetActive(false);
            }
        }

        // プレイヤーから近いビーコンを探す
        private Vector3 SerchBeaconNearPosition()
        {

            float tmpdist = 0.0f;
            float neardist = 0.0f;
            Vector3 nearPosition = new Vector3();
            
            // Beaconのタグを持つゲームオブジェクトを取得
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Beacon"))
            {
                // 距離を測る   
                tmpdist = Vector3.Distance(obj.transform.position, _playerPosition);
                if(neardist == 0.0f || neardist > tmpdist)
                {
                    neardist = tmpdist;
                    nearPosition = obj.transform.position;
                }
            }
            // 近い位置を返す
            return nearPosition;
        }

    }
}