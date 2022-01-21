using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.TM.Script
{
    class Grids2D<T>
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public T OutOfRange { get; set; }

        private readonly T[,] _grids;
        public T this[int y, int x]
        {
            get => IsInRange(x, y) ? _grids[y, x] : OutOfRange;
            set
            {
                if (IsInRange(x, y))
                {
                    _grids[y, x] = value;
                }
            }
        }

        public Grids2D(int width, int height, T outOfRange = default)
        {
            Debug.Assert(width > 0 && height > 0);

            Width = width;
            Height = height;
            OutOfRange = outOfRange;
            _grids = new T[Width, Height];
        }

        private bool IsInRange(int x, int y)
        {
            return x.IsInRange(0, Width) &&
                   y.IsInRange(0, Height);
        }

        public void Fill(T value)
        {
            for (var j = 0; j < Height; j++)
            {
                for (var i = 0; i < Width; i++)
                {
                    _grids[j, i] = value;
                }
            }
        }

        public void Line(Vector2Int from, Vector2Int to, T value)
        {
            Vector2 curt = from + new Vector2(0.5f, 0.5f);
            Vector2 vec = (to - from);
            vec.Normalize();

            if(vec.SqrMagnitude() == 0.0f) return;

            while (IsInRange(from.x, from.y))
            {
                _grids[from.y, from.x] = value;
                curt += vec;
                from = new Vector2Int(Mathf.RoundToInt(curt.x), Mathf.RoundToInt(curt.y));
            }
        }

        public void FillRect(RectInt rect, T value)
        {
            var xMax = rect.xMax;
            var yMax = rect.yMax;

            for (var j = rect.yMin; j < yMax; j++)
            {
                for (var i = rect.xMin; i < xMax; i++)
                {
                    this[j, i] = value;
                }
            }
        }

        public void FillOutsideRect(RectInt rect, T value)
        {
            var xMin = rect.xMin;
            var yMin = rect.yMin;
            var xMax = rect.xMax;
            var yMax = rect.yMax;

            for (var i = xMin; i < xMax; i++)
            {
                this[yMin, i] = value;
                this[yMax - 1, i] = value;
            }
            for (var i = yMin + 1; i < yMax - 1; i++)
            {
                this[i, xMin] = value;
                this[i, xMax - 1] = value;
            }
        }
    }
}
