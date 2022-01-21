using System.Collections;
using System.Collections.Generic;
using Assets.TM.Script;
using RayFire;
using UnityEngine;

namespace TM
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Destructable : MonoBehaviour
    {
        [SerializeField] private GameObject _fragmentsPrefab;

        [SerializeField] private string _targetTag;

        //[SerializeField] private string _fragmentsLayer;
        [SerializeField] private float _fragmentsMass;
        [SerializeField] private float _fragmentsFadeTime;
        [SerializeField] private List<AudioClip> _destructSounds;
        [SerializeField] private float _destructVolume = 0.1f;
        private RayfireShatter _rfShatter;

        private void Start()
        {
            if (!_fragmentsPrefab)
            {
                Debug.Assert(!_fragmentsPrefab);
                Destroy(this);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(_targetTag))
            {
                if (_destructSounds.Count != 0)
                {
                    AudioSource.PlayClipAtPoint(_destructSounds.Random(), transform.position, _destructVolume);
                }

                var root = Instantiate(_fragmentsPrefab, transform.position, transform.rotation);

                foreach (Transform child in root.transform)
                {
                    var fragment = child.gameObject;

                    //fragment.layer = LayerMask.NameToLayer(_fragmentsLayer);

                    var rigidBodyComp = fragment.AddComponent<Rigidbody>();
                    rigidBodyComp.mass = _fragmentsMass;

                    var fadeComp = fragment.AddComponent<ScaleDownFade>();
                    fadeComp.FadeTime = _fragmentsFadeTime;
                }

                Destroy(gameObject);
            }
        }
    }
}