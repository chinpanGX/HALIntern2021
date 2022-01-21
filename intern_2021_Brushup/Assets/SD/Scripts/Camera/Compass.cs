/*-------------------------------------------------------
 * 
 *      [Conpass.cs]
 *      Author: 出合翔太
 *      
 *      コンパスの動きの処理
 * 
 --------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    private GameObject _miniMapCameraObject;
    // Start is called before the first frame update
    void Start()
    {
        _miniMapCameraObject = GameObject.Find("MiniMap/MiniMapCameraPivot");
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = new Vector3(_miniMapCameraObject.transform.position.x, -50.0f, _miniMapCameraObject.transform.position.z);
    }
}
