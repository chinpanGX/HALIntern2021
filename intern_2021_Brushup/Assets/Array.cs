using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TM
{
    public class Array : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Vector3Int _width;
        [SerializeField] private Vector3 _offset;

        private void Start()
        {
            for (var z = 0; z < _width.z; ++z)
            {
                for (var y = 0; y < _width.y; ++y)
                {
                    for (var x = 0; x < _width.x; ++x)
                    {
                        var pos = new Vector3(
                            (float)x * _offset.x, 
                            (float)y * _offset.y, 
                            (float)z * _offset.z);
                        Instantiate(_prefab, pos, Quaternion.identity);
                    }
                }
            }

            Destroy(this);
        }
    }
}