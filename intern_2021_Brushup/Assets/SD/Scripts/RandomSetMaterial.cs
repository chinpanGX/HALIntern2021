/*-------------------------------------------------------
 * 
 *      [RandomSetMaterial.cs]
 *      Author : 出合翔太
 *      
 *      乱数でマテリアルをセットする
 * 
 --------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class RandomSetMaterial : MonoBehaviour
    {
        [Header("セットしたいマテリアル")]
        [SerializeField] private Material[] _materialList;

        // Start is called before the first frame update
        void Start()
        {
            // 変更先のマテリアルを乱数で選ぶ
            int setMat = Random.Range(0, _materialList.Length);

            // MeshRendererのマテリアル配列の0番目の色を下変更する
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

            // 配列を取得 -> 0番目を変更 -> 配列を返す
            var tmp = meshRenderer.materials;
            tmp[0] = _materialList[setMat];
            meshRenderer.materials = tmp;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
