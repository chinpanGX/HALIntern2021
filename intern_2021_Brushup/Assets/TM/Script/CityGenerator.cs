using System.Collections.Generic;
using System.Linq;
using Assets.TM.Script;
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;
using Sirenix.OdinInspector;

public class CityGenerator : SerializedMonoBehaviour
{
    enum GridType
    {
        None,
        Road,
        Object
    }

    enum GridRoadType
    {
        Straight,
        Curve,
        TJunction,
        Intersection
    }

    enum GridObjectType
    {
        Building,
    }

    struct GridObject
    {
        public GameObject prefab;
        public Vector2Int size;
    }

    [SerializeField] private int _seed;
    [SerializeField] private Vector2Int _width;
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField, Range(1, 15)] private int _maxSplit;
    [SerializeField] private Dictionary<GridRoadType, List<GameObject>> _gridRoadDatas;
    [SerializeField] private Dictionary<GridObjectType, List<GridObject>> _gridObjectDatas;

    private void Start()
    {
        //Generate();
    }

    public void SetSeedFromTime()
    {
        _seed = System.DateTime.Now.Millisecond;
    }

    public void Generate()
    {
        Random.InitState(_seed);
        
        Clear();

        var grids = new Grids2D<GridType>(_width.x, _width.y, GridType.None);
        grids.Fill(GridType.None);

        GenerateRoad(ref grids);
        GenerateObjects(ref grids);
    }

    private void GenerateRoad(ref Grids2D<GridType> grids)
    {
        {
            var blocks = new List<RectInt>();
            blocks.Add(new RectInt(0, 0, _width.x, _width.y));

            SplitDivision(ref blocks, _maxSplit, Random.Range(0, 2) == 0);

            foreach (var block in blocks)
            {
                grids.FillOutsideRect(block, GridType.Road);
            }
        }

        for (var j = 0; j < grids.Height; j++)
        {
            for (var i = 0; i < grids.Width; i++)
            {
                if (grids[j, i] == 0) continue;

                var neighbor = new bool[4];
                for (var k = 0; k < 4; k++)
                {
                    var rad = Mathf.PI * 0.5f * k;
                    var x = Mathf.RoundToInt(i + Mathf.Cos(rad));
                    var y = Mathf.RoundToInt(j + Mathf.Sin(rad));

                    neighbor[k] = grids[y, x] != 0;
                }

                var neighborCount = neighbor.Count(x => x);
                var roadType = GridRoadType.Straight;
                var rot = Quaternion.identity;

                switch (neighborCount)
                {
                    case 2:
                    {
                        if (neighbor[0] && neighbor[2])
                        {
                            roadType = GridRoadType.Straight;
                            rot = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                        }
                        else if (neighbor[1] && neighbor[3])
                        {
                            roadType = GridRoadType.Straight;
                        }
                        else
                        {
                            roadType = GridRoadType.Curve;

                            for (var k = 0; k < 4; k++)
                            {
                                if (!neighbor[k] || !neighbor[(k + 1) % 4]) continue;

                                var deg = 90.0f * (-k + 1);
                                rot = Quaternion.Euler(0.0f, deg, 0.0f);

                                break;
                            }
                        }

                        break;
                    }
                    case 3:
                    {
                        roadType = GridRoadType.TJunction;

                        for (var k = 0; k < 4; k++)
                        {
                            if (neighbor[k]) continue;

                            var deg = 90.0f * -k;
                            rot = Quaternion.Euler(0.0f, deg, 0.0f);

                            break;
                        }
                        break;
                    }
                    case 4:
                    {
                        roadType = GridRoadType.Intersection;
                        break;
                    }
                    default:
                    {
                        goto SkipGridInstantiate;
                    }
                }
                
                var prefab = _gridRoadDatas[roadType].Random();
                var pos = new Vector3(
                    (i - _width.x * 0.5f + 0.5f) * _gridSize.x,
                    0.0f, 
                    (j - _width.y * 0.5f + 0.5f) * _gridSize.y);
                Instantiate(prefab, pos, rot, transform);

                SkipGridInstantiate:
                    continue;
            }
        }
    }

    private void GenerateObjects(ref Grids2D<GridType> grids)
    {

        for (var j = 0; j < grids.Height; j++)
        {
            for (var i = 0; i < grids.Width; i++)
            {
                if (grids[j, i] != GridType.None) continue;

                var objData = new GridObject();
                var foundObj = false;

                // 接地オブジェクトをシャッフル
                var randOrder = _gridObjectDatas[GridObjectType.Building].OrderBy(x => Random.value);
                var rot = Quaternion.identity;

                foreach (var data in randOrder)
                {
                    var groundable = true;

                    for (var k = 1; k < data.size.x * data.size.y; k++)
                    {
                        var x = k % data.size.x;
                        var y = k / data.size.x;

                        if (grids[j + y, i + x] == GridType.None) continue;

                        groundable = false;
                        break;
                    }

                    var nextToRoad = false;

                    for (var k = 0; k < 4; k++)
                    {
                        var rad = Mathf.PI * 0.5f * k;
                        var x = Mathf.RoundToInt(i + Mathf.Cos(rad)) + (data.size.x - 1) * (k % data.size.x);
                        var y = Mathf.RoundToInt(j + Mathf.Sin(rad)) + (data.size.y - 1) * (k / data.size.x);

                        if (grids[y, x] == GridType.Road)
                        {
                            nextToRoad = true;

                            if (k % 2 == 0)
                                rot = Quaternion.Euler(0.0f, rad * Mathf.Rad2Deg + 90.0f, 0.0f);
                            else
                                rot = Quaternion.Euler(0.0f, rad * Mathf.Rad2Deg - 90.0f, 0.0f);

                            break;
                        }
                    }

                    if (!nextToRoad) continue;
                    if (!groundable) continue;

                    objData = data;
                    foundObj = true;
                    break;
                }

                if (!foundObj) continue;

                var pos = new Vector3(
                    (i - (_width.x - (objData.size.x - 1)) * 0.5f + 0.5f) * _gridSize.x,
                    0.0f,
                    (j - (_width.y - (objData.size.y - 1)) * 0.5f + 0.5f) * _gridSize.y);
                Instantiate(objData.prefab, pos, rot, transform);

                for (var k = 0; k < objData.size.y; k++)
                {
                    for (var m = 0; m < objData.size.x; m++)
                    {
                        grids[j + k, i + m] = GridType.Object;
                    }
                }
            }
        }
    }

    // 全ての生成済みグリッドを削除
    public void Clear()
    {
        // これで本当にあってる？
        while (transform.childCount > 0)
        {
            for (var i = 0; i < transform.childCount; ++i)
            {
                var child = transform.GetChild(i);

                if (Application.isEditor)
                    DestroyImmediate(child.gameObject);
                else
                    Destroy(child.gameObject);
            }
        }
    }

    private void SplitDivision(ref List<RectInt> blocks, int numSplit, bool horizontal)
    {
        if (numSplit-- == 0) return;

        var block = blocks.Last();

        int min, max;
        RectInt[] split = new RectInt[2];

        if (horizontal)
        {
            min = block.xMin;
            max = block.xMax;
        }
        else
        {
            min = block.yMin;
            max = block.yMax;
        }

        if (max - min < 5) return;

        var t = Random.Range(min + 3, max - 1);

        if (horizontal)
        {
            split[0] = block;
            split[0].xMax = t;

            split[1] = block;
            split[1].xMin = t - 1;
        }
        else
        {
            split[0] = block;
            split[0].yMax = t;

            split[1] = block;
            split[1].yMin = t - 1;
        }

        foreach (var b in split)
        {
            blocks.Add(b);
            SplitDivision(ref blocks, numSplit, !horizontal);
        }
    }
}