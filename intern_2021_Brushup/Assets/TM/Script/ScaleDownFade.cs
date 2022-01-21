using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDownFade : MonoBehaviour
{
    [SerializeField] private float _fadeTime = 10.0f;
    private float _elapced = 0.0f;
    private Vector3 _startScale;

    public float FadeTime { get => _fadeTime; set => _fadeTime = value; }

    private void Start()
    {
        _startScale = transform.localScale;
    }

    private void Update()
    {
        _elapced += Time.deltaTime;

        if (_fadeTime - _elapced < 0.0f)
        {
            Destroy(gameObject);
            return;
        }

        var scaleFactor = (_fadeTime - _elapced) / _fadeTime;
        transform.localScale = _startScale * scaleFactor;
    }
}
