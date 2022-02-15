using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TItleVirtualCamera : MonoBehaviour
{
    private float _time=0.0f;
    private TitleUIController _uiCtrl;
    // Start is called before the first frame update
    void Start()
    {
        _uiCtrl = GameObject.Find("UIController").GetComponent<TitleUIController>();
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if (_uiCtrl.GetTime() > 4.0f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
