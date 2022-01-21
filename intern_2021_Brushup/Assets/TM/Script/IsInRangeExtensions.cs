using System.Linq;
using UnityEngine;

namespace Assets.TM.Script
{
    public static class IsInRangeExtensions
    {
        // 値が最小値以上と最大値未満か調べる(float用)
        public static bool IsInRange(this float value, float min, float max)
        {
            return value >= min && value < max;
        }
        // 各軸の値が最小値以上と最大値未満か調べる(Vector2用)
        public static bool IsInRange(this Vector2 value, float min, float max)
        {
            return 
                value.x.IsInRange(min, max) && 
                value.y.IsInRange(min, max);
        }
        // 各軸の値が最小値以上と最大値未満か調べる(Vector3用)
        public static bool IsInRange(this Vector3 value, float min, float max)
        {
            return 
                value.x.IsInRange(min, max) &&
                value.y.IsInRange(min, max) && 
                value.z.IsInRange(min, max);
        }
        // 各軸の値が最小値以上と最大値未満か調べる(Vector4用)
        public static bool IsInRange(this Vector4 value, float min, float max)
        {
            return 
                value.x.IsInRange(min, max) &&
                value.y.IsInRange(min, max) &&
                value.z.IsInRange(min, max) &&
                value.w.IsInRange(min, max);
        }
        // 値が最小値以上と最大値未満か調べる(int用)
        public static bool IsInRange(this int value, int min, int max)
        {
            return value >= min && value < max;
        }
        // 各軸の値が最小値以上と最大値未満か調べる(Vector2Int用)
        public static bool IsInRange(this Vector2Int value, int min, int max)
        {
            return
                value.x.IsInRange(min, max) &&
                value.y.IsInRange(min, max);
        }
        // 各軸の値が最小値以上と最大値未満か調べる(Vector3Int用)
        public static bool IsInRange(this Vector3Int value, int min, int max)
        {
            return
                value.x.IsInRange(min, max) &&
                value.y.IsInRange(min, max) &&
                value.z.IsInRange(min, max);
        }
    }

    public static class IsAnyOfExtensions
    {
        // 値がいずれかの値と一致するか調べる
        public static bool IsAnyOf(this int value, params int[] comps)
        {
            return comps.Any(x => value == x);
        }
    }
}
