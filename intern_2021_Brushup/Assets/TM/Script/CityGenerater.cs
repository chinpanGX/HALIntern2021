using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
//using UnityEditor.PackageManager;
using Random = UnityEngine.Random;

public class CityGenerater : MonoBehaviour
{
    private enum RoadType
    {
        None,
        Straight,
        Curve,
        Junction,
        Intersection,
        Max
    }

    [System.Serializable]
    private struct Road
    {
        public GameObject prefab;
        public RoadType type;
    }

    private struct GridArea
    {
        public Vector2Int leftUp, rightBottom;
    }

    [SerializeField] private List<Road> _roads; // 道路の詳細
    [SerializeField] private float _roadWidth;  // 道路の幅  
    [SerializeField] private int _gridWidth;    // 街のグリッドの幅
    [SerializeField] private int _seed;         // 乱数のシード値

    [SerializeField] private int _numDivisions; // 街の分割数

    private int[] _numPerRoadTypes = new int[(int)RoadType.Max];

    private void Start()
    {
        // 再現性を持たせるためランダムシードをセット
        Random.InitState(_seed);

        // ループで回しやすいように道の種類でソート
        _roads.Sort((a, b) => a.type - b.type);

        // 道の種類ごとの数を求める
        foreach (var road in _roads)
        {

            Debug.Assert(
                road.type == RoadType.None || road.type == RoadType.Max,
                "道の種類をNoneとMaxに設定できません。");

            ++_numPerRoadTypes[(int) road.type];
        }

        // 街のグリッド
        var grid = GenerateGrid();

        var innerWidth = _gridWidth - 1;


        for (var i = 0; i < _gridWidth; ++i)
        {
            for (var j = 0; j < _gridWidth; j++)
            {
                var pos = new Vector3((float)i * _roadWidth, 0.0f, (float)j * _roadWidth);
                var obj = _roads[Random.Range(0, _roads.Count)].prefab;
                Instantiate(obj, pos, Quaternion.identity, transform);

                //if (grid[i, j])
                //{
                //    var pos = new Vector3((float) i * _roadWidth, 0.0f, (float) j * _roadWidth);
                //    var obj = _roads[0].prefab;
                //    Instantiate(obj, pos, Quaternion.identity, transform);
                //}
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool[,] GenerateGrid()
    {
        var grid = new bool[_gridWidth, _gridWidth];

        // 街のグリッドの端を全て道路にする
        for (var i = 0; i < _gridWidth; i++)
        {
            for (var j = 0; j < _gridWidth; j++)
            {
                if (i % (_gridWidth - 1) == 0 || 
                    j % (_gridWidth - 1) == 0)
                {
                    grid[i, j] = true;
                }
            }
        }

        return grid;
    }

    // 分割したグリッドをランダムに２分割して返す
    // horizontalがtrueの時は上下、falseなら左右に分割
    private GridArea[] RandomAreaDivision(GridArea area, bool horizontal)
    {
        var divided = new GridArea[2];

        int w;
        if (horizontal)
        {
            w = area.rightBottom.y - area.leftUp.y - 2;
        }
        else
        {
            w = area.rightBottom.x - area.leftUp.x - 2;
        }
        


        return divided;
    }
}
