using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TM
{
    public class GroundInitializer : MonoBehaviour
    {
        [SerializeField] private string _playerTag = "Player";
        private RayFire.RayfireRigid _rfRigid;

        private void Start()
        {
            _rfRigid = GetComponent<RayFire.RayfireRigid>();
            if (_rfRigid == null)
            {
                Destroy(this);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag(_playerTag))
            {
                return;
            }

            if (_rfRigid.initialized)
            {
                _rfRigid.Initialize();
            }

            Destroy(this);
        }
    }
}