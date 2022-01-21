using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TK
{
    //観覧車AIの倒れ検知用オブジェクト
    //地面に触れているかを判定する。
    public class Compass : MonoBehaviour
    {
        [SerializeField]
        public bool _isHitGround;
        // Start is called before the first frame update
        void Start()
        {
            _isHitGround = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("GroundInitializer"))
            {
                _isHitGround = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("GroundInitializer"))
            {
                _isHitGround = false;
            }
        }
    }
}
